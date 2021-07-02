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
using System.Net;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

public partial class Root_Distributor_UPI_Payment_Report : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    DataTable dtEWalletTransaction = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                if (Txt_FromDate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    Txt_FromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
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
    #region [Function]
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    [WebMethod]
    public static List<Customer> fillreport()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("Ezulix_UPI_Payment_Report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {

            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.client_vpa = dtrow["bankref_no"].ToString();
            cust.Amount = dtrow["amount"].ToString(); 
            cust.RRNNO = dtrow["client_txn_id"].ToString();
            cust.mode = dtrow["mode"].ToString();
            cust.name = dtrow["client_name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.upi_txn_id = dtrow["upi_txn_id"].ToString();
            cust.EzulixorderId = dtrow["client_txn_id"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    [WebMethod]
    public static List<Customer> fillreportbydate(string fromdate, string todate)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Ezulix_UPI_Payment_Report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.client_vpa = dtrow["bankref_no"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.RRNNO = dtrow["client_txn_id"].ToString();
            cust.name = dtrow["client_name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.mode = dtrow["mode"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.upi_txn_id = dtrow["upi_txn_id"].ToString();
            cust.EzulixorderId = dtrow["client_txn_id"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    [WebMethod]
    public static List<Customer> loadreceipt(string txnid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "Payoutreceipt" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
        dtEWalletTransaction = cls.select_data_dtNew("Ezulix_UPI_Payment_Report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.client_vpa = dtrow["bankref_no"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.RRNNO = dtrow["client_txn_id"].ToString();
            cust.name = dtrow["client_name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.mode = dtrow["mode"].ToString();
            cust.upi_txn_id = dtrow["upi_txn_id"].ToString();
            cust.EzulixorderId = dtrow["client_txn_id"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
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
            DataTable dtEWalletTransaction = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Ezulix_UPI_Payment_Report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "upi_Report");
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
    public static List<Customer> checkstatus(string txnid)
    {
        cls_myMember Clsm = new cls_myMember();
        cls_connection Cls = new cls_connection();
        string token = "Vq6BphVCp17sLEkXd8ezBzh3GT6Fgp";
        string StartID = "API713068";
        List<Customer> custList = new List<Customer>();
        string Out = String.Empty;
        string success = string.Empty;
        string TxnID = txnid;
        DataTable orderid = Cls.select_data_dt("select * from tbl_UPI_paymentGateway where client_txn_id='" + TxnID + "' and statuss='Pending'");
        if (orderid.Rows.Count > 0)
        {
            string MemberId = orderid.Rows[0]["MemberId"].ToString();
            string MemberTypeID = orderid.Rows[0]["MemberTypeID"].ToString();
            string Json = "{\"Tokenkey\":\"" + token + "\",\"StartID\":\"" + StartID + "\",\"TxnID\":\"" + TxnID + "\"}";
            string Url = "https://payu.startrecharge.in/QRCollect/PaymentStatus";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
            string myresponse = Out.ToString();
          //  DataSet ds = Deserialize(myresponse);
            DataSet ds = new DataSet();
            ds.Clear();
            XmlDocument doc = JsonConvert.DeserializeXmlNode(myresponse, "root");
            StringReader theReader = new StringReader(doc.InnerXml.ToString());
            ds.ReadXml(theReader);
          //  return ds;
            if (ds.Tables["root"].Rows[0]["Statuscode"].ToString() == "1")
            {
                if (ds.Tables["root"].Rows[0]["Status"].ToString() == "Success")
                {
                    string amount = ds.Tables["root"].Rows[0]["txnAmount"].ToString();
                    List<ParmList> _lstparms = new List<ParmList>();
                    _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["root"].Rows[0]["orderId"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["root"].Rows[0]["Status"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["root"].Rows[0]["Message"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@client_txn_id", value = TxnID });
                    _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                    Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                    int balance = Clsm.Wallet_MakeTransaction_Ezulix(MemberId, Convert.ToDecimal(amount), "Cr", "Add Fund UPI PG Order ID : " + TxnID);
                    return custList;
                    // Session["TxnID"] = null;
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Success');window.location ='UPI_Payment_Report.aspx';", true);
                }
            }
            else
            {
                return custList;
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Status Pending. try Again');window.location ='DashBoard.aspx';", true);
            }
        }
        else
        {
            return custList;
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found !!');", true);
        }
        return custList;
    }



    [WebMethod]
    public static List<Customer> refundstatusupi(string txnid)
    {
        cls_myMember Clsm = new cls_myMember();
        cls_connection Cls = new cls_connection();
        string token = "Vq6BphVCp17sLEkXd8ezBzh3GT6Fgp";
        string StartID = "API713068";
        List<Customer> custList = new List<Customer>();
        string Out = String.Empty;
        string success = string.Empty;
        string TxnID = txnid;
        DataTable orderid = Cls.select_data_dt("select * from tbl_UPI_paymentGateway where client_txn_id='" + TxnID + "'");
        if (orderid.Rows.Count > 0)
        {
            string MemberId = orderid.Rows[0]["MemberId"].ToString();
            string MemberTypeID = orderid.Rows[0]["MemberTypeID"].ToString();
            string Json = "{\"Tokenkey\":\"" + token + "\",\"StartID\":\"" + StartID + "\",\"TxnID\":\"" + TxnID + "\"}";
            string Url = "https://payu.startrecharge.in/QRCollect/Refund";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
            string myresponse = Out.ToString();
            //  DataSet ds = Deserialize(myresponse);
            DataSet ds = new DataSet();
            ds.Clear();
            XmlDocument doc = JsonConvert.DeserializeXmlNode(myresponse, "root");
            StringReader theReader = new StringReader(doc.InnerXml.ToString());
            ds.ReadXml(theReader);
            //  return ds;
            if (ds.Tables["root"].Rows[0]["Statuscode"].ToString() == "1")
            {
                if (ds.Tables["root"].Rows[0]["Status"].ToString() == "Success")
                {
                    string amount = ds.Tables["root"].Rows[0]["txnAmount"].ToString();
                    List<ParmList> _lstparms = new List<ParmList>();
                    _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["root"].Rows[0]["orderId"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["root"].Rows[0]["Status"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["root"].Rows[0]["Message"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@client_txn_id", value = TxnID });
                    _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                    Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                    int balance = Clsm.Wallet_MakeTransaction_Ezulix(MemberId, Convert.ToDecimal(amount), "Cr", "Add Fund UPI PG Order ID : " + TxnID);
                    return custList;
                    // Session["TxnID"] = null;
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Success');window.location ='UPI_Payment_Report.aspx';", true);
                }
            }
            else
            {
                return custList;
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Status Pending. try Again');window.location ='DashBoard.aspx';", true);
            }
        }
        else
        {
            return custList;
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found !!');", true);
        }
        return custList;
    }


    [WebMethod]
    public static List<Customer> refundcheckstatus(string txnid)
    {
        cls_myMember Clsm = new cls_myMember();
        cls_connection Cls = new cls_connection();
        string token = "Vq6BphVCp17sLEkXd8ezBzh3GT6Fgp";
        string StartID = "API713068";
        List<Customer> custList = new List<Customer>();
        string Out = String.Empty;
        string success = string.Empty;
        string TxnID = txnid;
        DataTable orderid = Cls.select_data_dt("select * from tbl_UPI_paymentGateway where client_txn_id='" + TxnID + "'");
        if (orderid.Rows.Count > 0)
        {
            string MemberId = orderid.Rows[0]["MemberId"].ToString();
            string MemberTypeID = orderid.Rows[0]["MemberTypeID"].ToString();
            string Json = "{\"Tokenkey\":\"" + token + "\",\"StartID\":\"" + StartID + "\",\"TxnID\":\"" + TxnID + "\"}";
            string Url = "https://payu.startrecharge.in/QRCollect/RefundStatus";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
            string myresponse = Out.ToString();
            //  DataSet ds = Deserialize(myresponse);
            DataSet ds = new DataSet();
            ds.Clear();
            XmlDocument doc = JsonConvert.DeserializeXmlNode(myresponse, "root");
            StringReader theReader = new StringReader(doc.InnerXml.ToString());
            ds.ReadXml(theReader);
            //  return ds;
            if (ds.Tables["root"].Rows[0]["Statuscode"].ToString() == "1")
            {
                if (ds.Tables["root"].Rows[0]["Status"].ToString() == "Success")
                {
                    string amount = ds.Tables["root"].Rows[0]["txnAmount"].ToString();
                    List<ParmList> _lstparms = new List<ParmList>();
                    _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["root"].Rows[0]["orderId"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["root"].Rows[0]["Status"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["root"].Rows[0]["Message"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@client_txn_id", value = TxnID });
                    _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                    Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                    int balance = Clsm.Wallet_MakeTransaction_Ezulix(MemberId, Convert.ToDecimal(amount), "Cr", "Add Fund UPI PG Order ID : " + TxnID);
                    return custList;
                    // Session["TxnID"] = null;
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Success');window.location ='UPI_Payment_Report.aspx';", true);
                }
            }
            else
            {
                return custList;
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Status Pending. try Again');window.location ='DashBoard.aspx';", true);
            }
        }
        else
        {
            return custList;
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found !!');", true);
        }
        return custList;
    }

    #endregion

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
        public string client_vpa { get; set; }
        public string EzulixorderId { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
        public string open_transaction { get; set; }
        public string TxnDate { get; set; }
        public string upi_txn_id { get; set; }
        public string amount { get; set; }
        public string mobile { get; set; }
        public string txnstatus { get; set; }
        public string name { get; set; }
        public string  mode { get; set; }
    }

    #endregion
}