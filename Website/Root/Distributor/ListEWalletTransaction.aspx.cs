using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Web.Services;

public partial class Root_Distributor_ListEWalletTransaction : System.Web.UI.Page
{
    #region [Properties]

    DataTable dtEWalletTransaction = new DataTable();
    DataTable dtExport = new DataTable();
    string condition = " ewallettransactionid > 0";
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
              
                if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                }
                DataTable dtmembermaster = new DataTable();
                dtmembermaster = (DataTable)Session["dtDistributor"];
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }

    }
    #endregion


    #region [Function]
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    [WebMethod]
    public static List<Customer> fillEWalletTransaction()
    {
        int MsrNo = Convert.ToInt32(HttpContext.Current.Session["IdNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetByMsrNo" });
        _lstparm.Add(new ParmList() { name = "@Id", value = MsrNo });
        dtEWalletTransaction = cls.select_data_dtNew("ProcMLM_ManageEWalletTransaction", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Debit = dtrow["Debit"].ToString();
            cust.Credit = dtrow["Credit"].ToString();
            cust.Balance = dtrow["Balance"].ToString();
            cust.narration = dtrow["Narration"].ToString();
            cust.date = dtrow["createdate"].ToString();
            cust.Factor = dtrow["Factor"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(HttpContext.Current.Session["IdNo"]);
            //DataTable dt = (DataTable)HttpContext.Current.Session["IdNo"];
            //int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "GetBydatewise" });
            _lstparm.Add(new ParmList() { name = "@Id", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@fromdate", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@todate", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("ProcMLM_ManageEWalletTransaction", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "EwalletTransaction_Report");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select date range to genrate excel');", true);
        }
    }




    [WebMethod]
    public static List<Customer> fillEWalletTransactionbydate(string fromdate, string todate)
    {
      
      //  int MsrNo = Session["IdNo"];
        int MsrNo = Convert.ToInt32(HttpContext.Current.Session["IdNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetBydatewise" });
        _lstparm.Add(new ParmList() { name = "@Id", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@fromdate", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@todate", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("ProcMLM_ManageEWalletTransaction", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Debit = dtrow["Debit"].ToString();
            cust.Credit = dtrow["Credit"].ToString();
            cust.Balance = dtrow["Balance"].ToString();
            cust.narration = dtrow["Narration"].ToString();
            cust.date = dtrow["createdate"].ToString();
            cust.Factor = dtrow["Factor"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    #endregion

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string Balance { get; set; }
        public string narration { get; set; }
        public string date { get; set; }
        public string Factor { get; set; }
    }

    #endregion

}