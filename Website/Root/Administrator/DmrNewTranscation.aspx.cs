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
using System.Xml;
using Newtonsoft.Json;
using System.IO;

public partial class Root_Admin_DmrNewTranscation : System.Web.UI.Page
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
            dtExport = cls.select_data_dtNew("MM_transactionREport_Ezulix_PayOut", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "NewDMR_Report");
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
    public static string CheckStatus(string fundid)
    {
        cls_connection cls = new cls_connection();
        List<Customer> custList = new List<Customer>();
        Customer cust = new Customer();
        string actions = string.Empty;
        string Result = string.Empty;
        string AgentOrderid = fundid;
        DataTable dtTxn = cls.select_data_dt(@"SELECT * FROM t_Ezulix_PayOut_MoneyTransfer WHERE  AgentOrderId='" + AgentOrderid + "' and status!='FAILURE' and status!='SUCCESS'");
        if (dtTxn.Rows.Count > 0)
        {
            int msrno = Convert.ToInt32(dtTxn.Rows[0]["MsrNo"]);
            string memberid = dtTxn.Rows[0]["MemberId"].ToString();
            EzulixAepsPayOut eDmrPayOut = new EzulixAepsPayOut();
            Result = eDmrPayOut.Check_Staus(AgentOrderid, Convert.ToInt32(dtTxn.Rows[0]["amount"]).ToString());
            DataSet ds = Deserialize(Result);
            if (ds.Tables.Count > 0 && ds.Tables.Contains("Response") && ds.Tables.Contains("Result"))
            {
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@status", value = ds.Tables["Response"].Rows[0]["status"].ToString() });
                _lstparm.Add(new ParmList() { name = "@statusCode", value = ds.Tables["Response"].Rows[0]["statusCode"].ToString() });
                _lstparm.Add(new ParmList() { name = "@statusMessage", value = ds.Tables["Response"].Rows[0]["statusMessage"].ToString() });
                _lstparm.Add(new ParmList() { name = "@EzulixorderId", value = ds.Tables["Result"].Rows[0]["EzulixorderId"].ToString() });
                _lstparm.Add(new ParmList() { name = "@BankOrderId", value = ds.Tables["Result"].Rows[0]["BankOrderId"].ToString() });
                _lstparm.Add(new ParmList() { name = "@NetAmountEzulix", value = Convert.ToDecimal(ds.Tables["Result"].Rows[0]["NetAmount"].ToString()) });
                if (ds.Tables[0].Rows[0]["status"].ToString() == "FAILURE")
                    _lstparm.Add(new ParmList() { name = "@rrn", value = "" });
                else
                    _lstparm.Add(new ParmList() { name = "@rrn", value = ds.Tables["Result"].Rows[0]["rrn"].ToString() });
                _lstparm.Add(new ParmList() { name = "@AgentOrderId", value = ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString() });
                _lstparm.Add(new ParmList() { name = "@Action", value = "U" });
                cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer", _lstparm);
                if (ds.Tables["Response"].Rows[0]["status"].ToString() == "FAILURE")
                {
                    string agentoid = AgentOrderid;
                    if (agentoid.Contains("PA"))
                    {
                        DataTable dd = new DataTable();
                        DataTable dm = new DataTable();
                        string a = "Fail Xpress AEPS Payoyut Topup Txn:-" + AgentOrderid;
                        cls_myMember clsm = new cls_myMember();
                        dd = cls.select_data_dt("select * from tblmlm_rwallettransaction where narration='" + a + "' and Factor='Cr' and msrno=" + msrno + "");
                        if (dd.Rows.Count == 0)
                        {
                            clsm.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(dtTxn.Rows[0]["NetAmount"].ToString()), "Cr", a);
                            actions = ds.Tables["Response"].Rows[0]["statusMessage"].ToString();

                        }
                    }
                    else
                    {
                        DataTable dm = new DataTable();
                        string b = "DMR Fail Txn:-" + AgentOrderid;
                        cls_myMember clsm = new cls_myMember();
                        dm = cls.select_data_dt("select * from tblmlm_ewallettransaction where narration='" + b + "' and Factor='Cr' and msrno=" + msrno + "");
                        if (dm.Rows.Count == 0)
                        {
                            clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(dtTxn.Rows[0]["NetAmount"].ToString()), "Cr", b);
                            actions = ds.Tables["Response"].Rows[0]["statusMessage"].ToString();

                        }

                    }
                }

                actions = ds.Tables["Response"].Rows[0]["statusMessage"].ToString();

            }
            else
            {

                actions = ds.Tables["Response"].Rows[0]["statusMessage"].ToString();

            }

        }
        else
        {

            actions = "Transaction Status Already Updated";

        }

        return actions;
    }

    [WebMethod]
    public static string FailStatus(string fundid)
    {
        cls_connection cls = new cls_connection();
        List<Customer> custList = new List<Customer>();
        Customer cust = new Customer();
        string actions = string.Empty;
        string Result = string.Empty;
        string AgentOrderid = fundid;
        DataTable dtTxn = cls.select_data_dt(@"SELECT * FROM t_Ezulix_PayOut_MoneyTransfer WHERE  AgentOrderId='" + AgentOrderid + "' and status='Pending'");
        if (dtTxn.Rows.Count > 0)
        {
            int msrno = Convert.ToInt32(dtTxn.Rows[0]["MsrNo"]);
            string memberid = dtTxn.Rows[0]["MemberId"].ToString();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@status", value = "Failed" });
            _lstparm.Add(new ParmList() { name = "@statusCode", value = "00" });
            _lstparm.Add(new ParmList() { name = "@statusMessage", value = "ForceFailedbyadmin" });
            _lstparm.Add(new ParmList() { name = "@EzulixorderId", value = "" });
            _lstparm.Add(new ParmList() { name = "@BankOrderId", value = "" });
            _lstparm.Add(new ParmList() { name = "@NetAmountEzulix", value = Convert.ToDecimal(0) });
            _lstparm.Add(new ParmList() { name = "@rrn", value = "" });

            _lstparm.Add(new ParmList() { name = "@AgentOrderId", value = AgentOrderid });
            _lstparm.Add(new ParmList() { name = "@Action", value = "U" });
            cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer", _lstparm);
            string agentoid = AgentOrderid;
            if (agentoid.Contains("PA"))
            {
                DataTable dd = new DataTable();
                DataTable dm = new DataTable();
                string a = "Force Fail Xpress AEPS Payoyut Topup Txn:-" + AgentOrderid;
                cls_myMember clsm = new cls_myMember();
                dd = cls.select_data_dt("select * from tblmlm_rwallettransaction where narration='" + a + "' and Factor='Cr' and msrno=" + msrno + "");
                if (dd.Rows.Count == 0)
                {
                    clsm.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(dtTxn.Rows[0]["NetAmount"].ToString()), "Cr", a);
                    actions = "ForceFailedbyadmin";
                }
            }
            else
            {
                DataTable dm = new DataTable();
                string b = "Force Xpress DMR Fail Txn:-" + AgentOrderid;
                cls_myMember clsm = new cls_myMember();
                dm = cls.select_data_dt("select * from tblmlm_ewallettransaction where narration='" + b + "' and Factor='Cr' and msrno=" + msrno + "");
                if (dm.Rows.Count == 0)
                {
                    clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(dtTxn.Rows[0]["NetAmount"].ToString()), "Cr", b);
                    actions = "ForceFailedbyadmin";

                }

            }
        }
        else
        {

            actions = "Transaction Status Already Updated";

        }

        return actions;
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
        dtEWalletTransaction = cls.select_data_dtNew("MM_transactionREport_Ezulix_PayOut", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.BeneAC = dtrow["BeneAC"].ToString();
            cust.Ifsc = dtrow["Ifsc"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.TxnID = dtrow["TxnID"].ToString();
            cust.BankOrderId = dtrow["BankOrderId"].ToString();
            cust.SurchargeTaken = dtrow["SurchargeTaken"].ToString();
            cust.ApiTxnID = dtrow["ApiTxnID"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.AdminCost = dtrow["AdminCost"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
           // cust.StateName = dtrow["StateName"].ToString();
            cust.msg = dtrow["msg"].ToString();
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
        dtEWalletTransaction = cls.select_data_dtNew("MM_transactionREport_Ezulix_PayOut", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.BeneAC = dtrow["BeneAC"].ToString();
            cust.Ifsc = dtrow["Ifsc"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.TxnID = dtrow["TxnID"].ToString();
            cust.BankOrderId = dtrow["BankOrderId"].ToString();
            cust.SurchargeTaken = dtrow["SurchargeTaken"].ToString();
            cust.ApiTxnID = dtrow["ApiTxnID"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.StateName = dtrow["StateName"].ToString();
            cust.AdminCost = dtrow["AdminCost"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.msg = dtrow["msg"].ToString();
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
        public string BeneAC { get; set; }
        public string Ifsc { get; set; }
        public string StateName { get; set; }
        public string Amount { get; set; }
        public string TxnID { get; set; }
        public string BankOrderId { get; set; }
        public string SurchargeTaken { get; set; }
        public string ApiTxnID { get; set; }
        public string Status { get; set; }
        public string AdminCost { get; set; }
        public string Createdate { get; set; }
        public string msg { get; set; }

    }

    #endregion

    private static DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
}