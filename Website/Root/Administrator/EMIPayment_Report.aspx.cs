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
public partial class Root_Admin_EMIPayment_Report : System.Web.UI.Page
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
            dtExport = cls.select_data_dtNew("sp_emipayment_trans", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "EMI PAYMENT_Report");
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
        dtEWalletTransaction = cls.select_data_dtNew("sp_emipayment_trans", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["NAME"].ToString();
            cust.policy_number = dtrow["ploicy_number"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.DOB = dtrow["DOB"].ToString();
            cust.mode = dtrow["mode"].ToString();
            cust.TxnID = dtrow["txn"].ToString();
            cust.policy_paylastdate = dtrow["policy_paylastdate"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();

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
        dtEWalletTransaction = cls.select_data_dtNew("sp_emipayment_trans", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["Name"].ToString();
            cust.policy_number = dtrow["ploicy_number"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.DOB = dtrow["DOB"].ToString();
            cust.mode = dtrow["mode"].ToString();
            cust.TxnID = dtrow["txn"].ToString();
            cust.policy_paylastdate = dtrow["policy_paylastdate"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    #endregion


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";

        string id = fundid;
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select * from tbl_EMIPAYMENTS where txn='" + id + "' and status='success'");
        string status = string.Empty;
        string txnid = string.Empty;
        string totalamt = string.Empty;
        string memberid = string.Empty;
        string spkey = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
           
            cls_myMember clsm = new cls_myMember();
            cls.select_data_dt(@"Update tbl_EMIPAYMENTS set status='SUCCESSS' Where txn='" + id + "'");
            // cls.select_data_dt(@"EXEC PROC_BBPS_ELE_COM_New @txnamount=" + Convert.ToDecimal(dt.Rows[0]["trans_amt"].ToString()) + ",@CMemberId='" + memberid.ToString() + "',@TxnId='" + txnid + "',@ServiceKey='" + spkey + "'");
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
    public static string RejectRequest(string fundid, string mobile, string date, string amount)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        string txn = fundid;
        DataTable dt = new DataTable();
        DataTable dts = new DataTable();
        dt = cls.select_data_dt("select * from tbl_EMIPAYMENTS where txn='" + txn + "' and status='success'");
        string msrno = string.Empty;
        msrno = dt.Rows[0]["msrno"].ToString();
        //dts = cls.select_data_dt("select * from tblMLM_EWalletTransaction where Narration='Life Insurance Commission:" + txn + "' and msrno='" + msrno + "'");
        string status = string.Empty;
        string txnid = string.Empty;
        string totalamt = string.Empty;
        string amounts = string.Empty;
        string memberid = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
           
            amounts = dt.Rows[0]["amount"].ToString();
            memberid = dt.Rows[0]["memberid"].ToString();
            cls_myMember clsm = new cls_myMember();
            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(amount)), "Cr", "Reverse life insurance  TxnID:-" + txn);
           // clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal("-" + amounts), "Dr", "Reverse Life insurance Commission:'" + txn + "'");
            cls.select_data_dt(@"Update tbl_EMIPAYMENTS set status='Fail' Where txn='" + txn + "'");
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }
    }



    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string policy_number { get; set; }
        public string policy_paylastdate { get; set; }
        public string Amount { get; set; }
        public string Name { get; set; }
        public string mode { get; set; }
        public string DOB { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
        public string msg { get; set; }
        public string TxnID { get; set; }
    }

    #endregion
}