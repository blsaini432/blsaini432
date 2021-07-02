using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Root_Admin_Wallet_FundTransfer_Report : System.Web.UI.Page
{
    #region [Properties]
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public DataTable dtMemberMaster = new DataTable();
    cls_myMember Clsm = new cls_myMember();
    #endregion

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
    public static List<Customer> fillWalletTransferReport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(0) });
        _lstparm.Add(new ParmList() { name = "@rtype", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("Wallet_Transaction_Report", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberID = dtrow["memberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.amount = dtrow["amount"].ToString();
            cust.balance = dtrow["balance"].ToString();
            cust.Date = dtrow["Date"].ToString();
            cust.narration = dtrow["narration"].ToString();
            cust.factor = dtrow["factor"].ToString();
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
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@rtype", value = "admin" });
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Wallet_Transaction_Report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "Wallet_Transaction_Report");
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
    public static List<Customer> fillWalletTransferReportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@rtype", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Wallet_Transaction_Report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberID = dtrow["memberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.amount = dtrow["amount"].ToString();
            cust.balance = dtrow["balance"].ToString();
            cust.Date = dtrow["Date"].ToString();
            cust.narration = dtrow["narration"].ToString();
            cust.factor = dtrow["factor"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion

    #region class
    public class Customer
    {
        public string memberID { get; set; }
        public string MemberName { get; set; }
        public string mobile { get; set; }
        public string amount { get; set; }
        public string balance { get; set; }
        public string Date { get; set; }
        public string narration { get; set; }
        public string factor { get; set; }

    }

    #endregion
}