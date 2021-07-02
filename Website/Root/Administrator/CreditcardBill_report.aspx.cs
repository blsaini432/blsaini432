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

public partial class Root_Admin_CreditcardBill_report : System.Web.UI.Page
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
            dtExport = cls.select_data_dtNew("sp_creditcard_trans", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "credit card bill payment");
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
        dtEWalletTransaction = cls.select_data_dtNew("sp_creditcard_trans", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["NAME"].ToString();
            cust.policy_number = dtrow["Creditcard_number"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.mode = dtrow["mode"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.duedate = dtrow["DOB"].ToString();
            cust.TxnID = dtrow["Txn"].ToString();
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
        dtEWalletTransaction = cls.select_data_dtNew("sp_creditcard_trans", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["NAME"].ToString();
            cust.policy_number = dtrow["Creditcard_number"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.mode = dtrow["mode"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.duedate = dtrow["DOB"].ToString();
            cust.TxnID = dtrow["Txn"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    #endregion
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequests(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        string id = fundid;
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select * from tbl_credit_cards where txn='" + id + "' and status='pending'");
        string amount = dt.Rows[0]["AMOUNT"].ToString();
        string memberid = dt.Rows[0]["memberID"].ToString();
        string Msrno = dt.Rows[0]["Msrno"].ToString();
        // string NetAmount = TotupAmount(Convert.ToDecimal(amount), memberid);
        DataTable dtsr = new DataTable();
        DataTable dtMemberMaster = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='chk', @msrno=" + Msrno + "");
        string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
        dtsr = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='CRE',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
        if (dtsr.Rows.Count > 0)
        {
            surcharge_rate = Convert.ToDouble(dtsr.Rows[0]["surcharge"].ToString());
            isFlat = Convert.ToInt32(dtsr.Rows[0]["isflat"].ToString());
            if (surcharge_rate > 0)
            {
                if (isFlat == 0)
                    surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
                else
                    surcharge_amt = surcharge_rate;
            }
            NetAmount = surcharge_amt;
        }
        if (dt.Rows.Count > 0)
        {
            cls_myMember clsm = new cls_myMember();
            cls.select_data_dt(@"Update tbl_credit_cards set status='SUCCESS' Where txn='" + id + "'");
           
            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(NetAmount)), "Cr", "Credit Card Commission TxnID:-" + id);
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
    public static string RejectRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        string txn = fundid;
        DataTable dt = new DataTable();
        DataTable dts = new DataTable();
        dt = cls.select_data_dt("select * from tbl_credit_cards where txn='" + txn + "'");      
        string txnid = string.Empty;
        string amounts = string.Empty;
        string memberid = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
            amounts = dt.Rows[0]["amount"].ToString();
            memberid = dt.Rows[0]["memberid"].ToString();
            cls_myMember clsm = new cls_myMember();
            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(amounts)), "Cr", "Reverse Credit Card TxnID:-" + txn);
            //clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal("-" + amounts), "Dr", "Reverse Cradit Card Commission:'" + txn + "'");
            cls.select_data_dt(@"Update tbl_credit_cards set status='Fail' Where txn='" + txn + "'");
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
        public string duedate { get; set; }
        public string TxnID { get; set; }
        
    }

    #endregion
}