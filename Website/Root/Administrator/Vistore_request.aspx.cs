using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Root_Admin_Vistore_request : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
            {
                txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
    }
    #region [Function]
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillfundrequest()
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "admin" });
        // _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
       _lstparm.Add(new ParmList() { name = "@datefrom", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
       _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        dtEWalletTransaction = cls.select_data_dtNew("pro_vistore", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["memberid"].ToString();
            cust.Custmer_Number = dtrow["member_name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.product_name = dtrow["product_name"].ToString();
            cust.msrno = dtrow["MsrNo"].ToString();         
            cust.Date = dtrow["Date"].ToString();

            custList.Add(cust);
        }
        return custList;
    }


    [WebMethod]
    public static List<Customer> fillfundrequestbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "admin" });
        // _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
       _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
       _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("pro_vistore", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["memberid"].ToString();
            cust.Custmer_Number = dtrow["member_name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.product_name = dtrow["product_name"].ToString();
            cust.msrno = dtrow["MsrNo"].ToString();
            cust.Date = dtrow["Date"].ToString();

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
            int MsrNo = Convert.ToInt32(0);
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "admin" });
            // _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
           // _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
          //  _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("pro_vistore", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "vistore");
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
    public static List<Customer> ShowFundImage(string fundid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "image" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = fundid });
        dtEWalletTransaction = cls.select_data_dtNew("pro_vistore", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["memberid"].ToString();
            cust.Custmer_Number = dtrow["member_name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.product_name = dtrow["product_name"].ToString();
            cust.msrno = dtrow["MsrNo"].ToString();
            cust.Date = dtrow["Date"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    #endregion


    #region class
    public class Customer
    {
        public string memberid { get; set; }
        public string father_name { get; set; }
        public string Custmer_Number { get; set; }
        public string mobile { get; set; }
        public string product_name { get; set; }
        public string msrno { get; set; }
        public string type { get; set; }
        public string HP { get; set; }
        public string Date { get; set; }
        public string msg { get; set; }
        public string bankpassbook { get; set; }
        public string status { get; set; }
        public string pan { get; set; }
        public string aadhar { get; set; }
        public string TxnID { get; set; }
        public string puc { get; set; }
        public string Vicialrc { get; set; }
        public string Vicialinsurance { get; set; }
        public string form29 { get; set; }
        public string form30 { get; set; }

    }

    #endregion
  

}
