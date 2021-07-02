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
using System.Net;

public partial class Root_Admin_YesBankDmrReport : System.Web.UI.Page
{
    public static string M_Uri = "http://uat5yesmoney.easypay.co.in:5050/";
    //public string M_Uri = "http://localhost:49530/Website/";
    private static string mm_token = "9724a998-78e9-4870-a934-1cbf396d7625";
    #region Properties
    cls_connection Cls = new cls_connection();
    DataTable dtEWalletTransaction = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    YesBankdmr yesdmr = new YesBankdmr();
    private static string AID = "RS00789";
    private static string OP = "DMTNUR";
    private static string ST = "REMDOMESTIC";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (Txt_FromDate.Text.Trim() == "" || txttodate.Text.Trim() == "")
            {
                txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                Txt_FromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
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
    public static List<Customer> filldmrreport()
    {

        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("yesbank_DMR_transaction_admin", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {

            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["SenderMobile"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.BeneAC = dtrow["BeneAC"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.RefNo = dtrow["RefNo"].ToString();
            cust.TxnID = dtrow["TxnID"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.Bank = dtrow["Bank"].ToString();
            cust.bankrefno = dtrow["bankreferenceid"].ToString();
            cust.Bank = dtrow["Bank"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> filldmrreportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("yesbank_DMR_transaction_admin", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["SenderMobile"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.BeneAC = dtrow["BeneAC"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.RefNo = dtrow["RefNo"].ToString();
            cust.TxnID = dtrow["TxnID"].ToString();
            cust.Status = dtrow["Status"].ToString();

            cust.Bank = dtrow["Bank"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> loaddmrreceipt(string txnid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "loadinstantdmrreceipt" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.AgentOrderId = dtrow["EzulixTranid"].ToString();
            cust.TxnDate = dtrow["created"].ToString();
            cust.beneficiaryAccount = dtrow["beni_account"].ToString();
            cust.amount = dtrow["amount"].ToString();
            cust.logo = string.IsNullOrEmpty(Convert.ToString(dtrow["Companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["Companylogo"]);
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
            DataTable dtEWalletTransaction = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("RP_DMR_transaction_admin", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "Dmrreport_Report");
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
    #endregion


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequest(string fundid, string bankid)
    {

        string actions = "";
        cls_connection cls = new cls_connection();
        string id = fundid;
        string mobileno = bankid;
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\":\"" + mobileno + "\",\"REQUEST_REFERENCE_NO\":\"" + id + "\"}";
        string Request_Json = Json.Replace(@"\", "");
      string Result =  HTTP_POST(M_Uri + "epMoney/transaction-status/v1.0", Request_Json);

        if (Result != string.Empty)
        {
            DataSet dsa = new DataSet();
            dsa = Deserialize(Result);
            if (dsa.Tables[0].Rows[0]["RESP_MSG"].ToString() == "")
            {

              string status = dsa.Tables["DATA"].Rows[0]["TRANSACTION_STATUS"].ToString();
                cls_myMember clsm = new cls_myMember();
                cls.select_data_dt(@"Update tbl_yesmoney_Fund_Tra set opr_id='" + status + "' Where  opr_id='" + id + "'");
            }
            else
            {
               
            }
        }


        return actions;
    }

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string Bank { get; set; }
        public string SenderMobile { get; set; }
        public string Name { get; set; }
        public string RefNo { get; set; }
        public string TxnID { get; set; }
        public string BeneAC { get; set; }
        public string Ifsc { get; set; }
        public string Amount { get; set; }
        public string bankrefno { get; set; }
        public string RRNNO { get; set; }
        public string BankTxnId { get; set; }
        public string AgentOrderId { get; set; }
        public string EzulixorderId { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
        public string TxnDate { get; set; }
        public string beneficiaryAccount { get; set; }
        public string amount { get; set; }
        public string logo { get; set; }
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
    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            httpWebRequest.Headers.Add("Authorization", "bearer" + "" + mm_token);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
        }
        catch (WebException e)
        {
            if (e.Status == WebExceptionStatus.Timeout)
            {
                Out = "{\"statuscode\": \"ETO\",\"status\":\"Transcation is Pending\"}";
            }
            else throw;
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    #endregion
}