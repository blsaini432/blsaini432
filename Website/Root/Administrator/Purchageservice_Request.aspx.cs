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
using System.Web.Script.Services;
public partial class Root_Administrator_Purchageservice_Request : System.Web.UI.Page
{
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    string condition = " ewallettransactionid > 0";
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
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(1);
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("adminpaymentgateway_report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "paymentgateway_report");
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
    #region [Function]
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    [WebMethod]
    public static List<Customer> filldmrreport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        int MsrNo = Convert.ToInt32(1);
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("adminpaymentgateway_report", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["contact"].ToString();
            cust.vpa = dtrow["vpa"].ToString();
            cust.orderid = dtrow["POrderid"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.payid = dtrow["payid"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.method = dtrow["method"].ToString();
            cust.cardid = dtrow["cardid"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.totalamount = dtrow["totalamount"].ToString();
            cust.feerate = dtrow["feerate"].ToString();
            cust.servicename = dtrow["servicename"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    [WebMethod]
    public static List<Customer> filldmrreportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(1);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("adminpaymentgateway_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["contact"].ToString();
            cust.vpa = dtrow["vpa"].ToString();
            cust.orderid = dtrow["POrderid"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.payid = dtrow["payid"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.method = dtrow["method"].ToString();
            cust.cardid = dtrow["cardid"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.totalamount = dtrow["totalamount"].ToString();
            cust.feerate = dtrow["feerate"].ToString();
            cust.TransactionId = dtrow["TransactionId"].ToString();
            cust.servicename = dtrow["servicename"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Customer> approved(string txnid)
    {
      
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        string txn = txnid;
        HttpContext.Current.Session["txn"] = txn;
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@action", value = "u" });
        _lstparm.Add(new ParmList() { name = "@TransactionId", value = txnid });
        dtEWalletTransaction = cls.select_data_dtNew("adminpaymentgateway_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["contact"].ToString();
            cust.vpa = dtrow["vpa"].ToString();
            cust.orderid = dtrow["POrderid"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.payid = dtrow["payid"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.method = dtrow["method"].ToString();
            cust.cardid = dtrow["cardid"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.totalamount = dtrow["totalamount"].ToString();
            cust.feerate = dtrow["feerate"].ToString();
            cust.TransactionId = dtrow["TransactionId"].ToString();
            cust.servicename = dtrow["servicename"].ToString();
            custList.Add(cust);
        }

        return custList;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        cls_connection cls = new cls_connection();
        string method = droplist.SelectedItem.ToString();
        string amount = txt_amount.Text;
        string txnid = HttpContext.Current.Session["txn"].ToString();
        if (txnid != "" )
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from tbl_paymentGateway where TransactionId='" + txnid + "'");
            String Result = string.Empty;
            if (dt.Rows.Count > 0)
            {
                string  memberid = (dt.Rows[0]["memberid"].ToString());
                cls_myMember clsm = new cls_myMember();
                string statuss = "captured";
                cls.select_data_dt(@"Update tbl_paymentGateway set method='" + method + "', Statuss='"+ statuss + "', totalamount='"+ amount + "' Where  TransactionId='" + txnid + "'");
                clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(amount), "Cr", "Force Add Found PG TXN ID : " + txnid);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Member Wallet Amount Add Successfull!');location.replace('paymentgateway_report.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Found!');location.replace('paymentgateway_report.aspx');", true);
            }
        }
       

    }

    #endregion

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string cardid { get; set; }
        public string SenderMobile { get; set; }
        public string Name { get; set; }
        public string RefNo { get; set; }
        public string TxnID { get; set; }
        public string servicename { get; set; }
        public string method { get; set; }
        public string Amount { get; set; }
        public string feerate { get; set; }
        public string payid { get; set; }
        public string vpa { get; set; }
        public string orderid { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
        public string TxnDate { get; set; }
        public string logo { get; set; }
        public string mode { get; set; }
        public string amount { get; set; }
        public string totalamount { get; set; }
        public string createdate { get; set; }
        public string email { get; set; }
        public string TransactionId { get; set; }
    }


    #endregion

    
}