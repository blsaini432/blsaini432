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

public partial class Root_Distributor_ListHistory : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    DataTable dtExport = new DataTable();
    public static string mssrno { get; set; }
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();
    clsRecharge_Dispute objDispute = new clsRecharge_Dispute();
    DataTable dtDispute = new DataTable();
    cls_connection cls = new cls_connection();
    string condition = " MsrNo > 0";
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
                mssrno = dtmembermaster.Rows[0]["MsrNo"].ToString();
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }

    #endregion

    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillrechargereport()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetAllRavi" });
        _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("Recharge_MyHistory", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MobileNo = dtrow["MobileNo"].ToString();
            cust.RechargeAmount = dtrow["RechargeAmount"].ToString();
            cust.OperatorName = dtrow["OperatorName"].ToString();
            cust.TransID = dtrow["TransID"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.ErrorMsg = dtrow["ErrorMsg"].ToString();
            cust.APIMessage = dtrow["APIMessage"].ToString();
            cust.ReceiptLink = dtrow["ReceiptLink"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.HistoryID = dtrow["HistoryID"].ToString();

            
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
            DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
            DataTable dtExport = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "GetAllRavi" });
            _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Recharge_MyHistory", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "RechargeReportDS_Report");
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
    public static List<Customer> fillrechargereportbydate(string fromdate, string todate)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetAllRavi" });
        _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Recharge_MyHistory", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MobileNo = dtrow["MobileNo"].ToString();
            cust.RechargeAmount = dtrow["RechargeAmount"].ToString();
            cust.OperatorName = dtrow["OperatorName"].ToString();
            cust.TransID = dtrow["TransID"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.ErrorMsg = dtrow["ErrorMsg"].ToString();
            cust.APIMessage = dtrow["APIMessage"].ToString();
            cust.ReceiptLink = dtrow["ReceiptLink"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.HistoryID = dtrow["HistoryID"].ToString();
            custList.Add(cust);
        }
        return custList;
    }




    [WebMethod]
    public static List<Customer> loadrechargereceipt(string txnid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "loadrechargereceipt" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.receiptno = dtrow["HistoryID"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.MobileNo = dtrow["MobileNo"].ToString();
            cust.caNumber = dtrow["caNumber"].ToString();
            cust.TransID = dtrow["TransID"].ToString();
            cust.APIMessage = dtrow["APIMessage"].ToString();
            cust.RechargeAmount = dtrow["RechargeAmount"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.address = dtrow["address"].ToString();
            cust.mymobile = dtrow["mymobile"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.logo = string.IsNullOrEmpty(Convert.ToString(dtrow["Companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["Companylogo"]);
            custList.Add(cust);
        }
        return custList;
    }

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MobileNo { get; set; }
        public string RechargeAmount { get; set; }
        public string OperatorName { get; set; }
        public string TransID { get; set; }
        public string Status { get; set; }
        public string ErrorMsg { get; set; }
        public string APIMessage { get; set; }
        public string AddDate { get; set; }
        public string ReceiptLink { get; set; }
        public string logo { get; set; }
        public string receiptno { get; set; }
        public string agent_id { get; set; }
        public string caNumber { get; set; }
        public string MemberName { get; set; }
        public string address { get; set; }
        public string mymobile { get; set; }
        public string Email { get; set; }
        public string HistoryID { get; set; }
    }

    #endregion

}