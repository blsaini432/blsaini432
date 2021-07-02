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


public partial class Root_Administrator_payout_report_mysun : System.Web.UI.Page
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
            dtExport = cls.select_data_dtNew("Ezulix_PayOut_Instant_ReportAdmin", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "Payout_Report");
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
    public static List<Customer> fillnewdmrreport()
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
        dtEWalletTransaction = cls.select_data_dtNew("Ezulix_PayOut_Instant_ReportAdmin", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.BeneAC = dtrow["beneficiaryAccount"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Ifsc = dtrow["beneficiaryIFSC"].ToString();
            cust.RRNNO = dtrow["credit_refid"].ToString();
            cust.AgentOrderId = dtrow["merchant_ref_id"].ToString();
            cust.name = dtrow["recepient_name"].ToString();
            cust.open_transaction = dtrow["open_transaction_ref_id"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.txnstatus = dtrow["txnstatus"].ToString();
            cust.EzulixorderId = dtrow["EzulixorderId"].ToString();
            cust.Createdate = dtrow["TxnDate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    [WebMethod]
    public static List<Customer> fillnewdmrreportbydate(string fromdate, string todate)
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
        dtEWalletTransaction = cls.select_data_dtNew("Ezulix_PayOut_Instant_ReportAdmin", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.BeneAC = dtrow["beneficiaryAccount"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Ifsc = dtrow["beneficiaryIFSC"].ToString();
            cust.RRNNO = dtrow["credit_refid"].ToString();
            cust.AgentOrderId = dtrow["merchant_ref_id"].ToString();
            cust.name = dtrow["recepient_name"].ToString();
            cust.open_transaction = dtrow["open_transaction_ref_id"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.txnstatus = dtrow["txnstatus"].ToString();
            cust.EzulixorderId = dtrow["EzulixorderId"].ToString();
            cust.Createdate = dtrow["TxnDate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    #endregion

    protected void btn_payouton(object sender, EventArgs e)
    {
        cls.update_data("update tblmlm_membermaster set isaepspayout='1'");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('payout service on');", true);
    }
    protected void btn_payoutoff(object sender, EventArgs e)
    {
        cls.update_data("update tblmlm_membermaster set isaepspayout='0'");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('payout service off');", true);
    }
    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string BeneAC { get; set; }
        public string Ifsc { get; set; }
        public string Amount { get; set; }
        public string RRNNO { get; set; }
        public string BankTxnId { get; set; }
        public string AgentOrderId { get; set; }
        public string EzulixorderId { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
        public string open_transaction { get; set; }
        public string TxnDate { get; set; }
        public string beneficiaryAccount { get; set; }
        public string amount { get; set; }
        public string logo { get; set; }
        public string txnstatus { get; set; }
        public string name { get; set; }

    }

    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        if (fundid != "")
        {
            string id = fundid;
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from t_Ezulix_PayOut_MoneyTransfernew where merchant_ref_id='" + id + "' and txnstatus='Pending'");
            string status = string.Empty;
            string txnid = string.Empty;
            string totalamt = string.Empty;
            string memberid = string.Empty;
            string spkey = string.Empty;
            String Result = string.Empty;
            if (dt.Rows.Count > 0)
            {
                //txnid = dt.Rows[0]["agent_id"].ToString();
                // cls_myMember clsm = new cls_myMember();
                cls.select_data_dt(@"Update t_Ezulix_PayOut_MoneyTransfernew set txnstatus='Success' Where  merchant_ref_id='" + id + "'");
                // cls.select_data_dt(@"EXEC PROC_BBPS_ELE_COM_New @txnamount=" + Convert.ToDecimal(dt.Rows[0]["trans_amt"].ToString()) + ",@CMemberId='" + memberid.ToString() + "',@TxnId='" + txnid + "',@ServiceKey='" + spkey + "'");
                actions = "success";
                return actions;
            }
            else
            {
                return actions;
            }
        }
        else
        {
            return actions;
        }

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string RejectRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        string txn = fundid;
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select * from t_Ezulix_PayOut_MoneyTransfernew where merchant_ref_id='" + txn + "' and txnstatus='Pending'");
        string status = string.Empty;
        string txnid = string.Empty;
        string totalamt = string.Empty;
        string memberid = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
           
            totalamt = dt.Rows[0]["NetAmount"].ToString();
            memberid = dt.Rows[0]["memberid"].ToString();
            cls_myMember clsm = new cls_myMember();
            clsm.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(totalamt), "Cr", "Payout Fail Txn:- " + txn + "");
            cls.select_data_dt(@"Update t_Ezulix_PayOut_MoneyTransfernew set txnstatus='adminFailed' Where  merchant_ref_id='" + txn + "'");
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string failApproveRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        if (fundid != "")
        {
            string id = fundid;
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from t_Ezulix_PayOut_MoneyTransfernew where merchant_ref_id='" + id + "' and txnstatus='Failed'");
            string status = string.Empty;
            string txnid = string.Empty;
            string totalamt = string.Empty;
            string memberid = string.Empty;
            string spkey = string.Empty;
            String Result = string.Empty;
            if (dt.Rows.Count > 0)
            {
                
                cls.select_data_dt(@"Update t_Ezulix_PayOut_MoneyTransfernew set txnstatus='adminSuccess' Where  merchant_ref_id='" + id + "'");
                // cls.select_data_dt(@"EXEC PROC_BBPS_ELE_COM_New @txnamount=" + Convert.ToDecimal(dt.Rows[0]["trans_amt"].ToString()) + ",@CMemberId='" + memberid.ToString() + "',@TxnId='" + txnid + "',@ServiceKey='" + spkey + "'");
                actions = "success";
                return actions;
            }
            else
            {
                return actions;
            }
        }
        else
        {
            return actions;
        }

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string failRejectRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        string txn = fundid;
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select * from t_Ezulix_PayOut_MoneyTransfernew where merchant_ref_id='" + txn + "' and txnstatus='Failed'");
        string status = string.Empty;
        string txnid = string.Empty;
        string totalamt = string.Empty;
        string memberid = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
           
            totalamt = dt.Rows[0]["NetAmount"].ToString();
            memberid = dt.Rows[0]["memberid"].ToString();
            cls_myMember clsm = new cls_myMember();
            clsm.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(totalamt), "Cr", "Payout Fail Txn:- " + txn + "");
            cls.select_data_dt(@"Update t_Ezulix_PayOut_MoneyTransfernew set txnstatus='adminFailed' Where  merchant_ref_id='" + txn + "'");
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }
    }
}