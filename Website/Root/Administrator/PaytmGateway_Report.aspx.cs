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
using Newtonsoft.Json;
using Paytm;
using System.Net;
using System.IO;
using System.Xml;

public partial class Root_Administrator_PaytmGateway_Report : System.Web.UI.Page
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
            dtExport = cls.select_data_dtNew("paytmgateway_report", _lstparm);
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
    public static List<Customer> fillreport()
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
            cust.orderid = dtrow["POrderid"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.GATEWAYNAME = dtrow["PAYMENTMODE"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.BANKNAME = dtrow["BANKNAME"].ToString();
            cust.callbackstatus = dtrow["callback_status"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.totalamount = dtrow["totalamount"].ToString();
            cust.feerate = dtrow["feerate"].ToString();
            cust.RESPMSG = dtrow["RESPMSG"].ToString();
            cust.TransactionId = dtrow["TransactionId"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> fillreportbydate(string fromdate, string todate)
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
            cust.orderid = dtrow["POrderid"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.GATEWAYNAME = dtrow["PAYMENTMODE"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.callbackstatus = dtrow["callback_status"].ToString();
            cust.BANKNAME = dtrow["BANKNAME"].ToString();
            cust.BANKTXNID = dtrow["BANKTXNID"].ToString();
            cust.feerate = dtrow["feerate"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.totalamount = dtrow["totalamount"].ToString();
            cust.feerate = dtrow["feerate"].ToString();
            cust.TransactionId = dtrow["TransactionId"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Customer> CheckStatus(string txnid)
    {
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        string ORDERID = txnid;
        DataTable orderid = cls.select_data_dt("select * from tbl_paytm_paymentGateway where TransactionId='" + ORDERID + "' and statuss='Pending'");
        if (orderid.Rows.Count > 0)
        {
          
            cls_myMember clsm = new cls_myMember();
            string userid = orderid.Rows[0]["Memberid"].ToString();
            Dictionary<string, string> body = new Dictionary<string, string>();
            Dictionary<string, string> head = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();
            body.Add("mid", "Fritwa43748969852592");
            body.Add("orderId", txnid);
            string paytmchecksums = Checksum.generateSignature(JsonConvert.SerializeObject(body), "%o621mn%#j#9D@L#");
            head.Add("signature", paytmchecksums);
            requestBody.Add("body", body);
            requestBody.Add("head", head);
            string post_data = JsonConvert.SerializeObject(requestBody);
            string url = "https://securegw.paytm.in/v3/order/status";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = post_data.Length;
            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(post_data);
            }
            string responseData = string.Empty;
            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();
                if (responseData != string.Empty)
                {
                    DataSet ds = new DataSet();
                    XmlDocument doc = JsonConvert.DeserializeXmlNode(responseData, "root");
                    StringReader theReader = new StringReader(doc.InnerXml.ToString());
                    ds.ReadXml(theReader);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["signature"] != null)
                        {
                            if (ds.Tables[2].Rows[0]["resultStatus"].ToString() == "TXN_SUCCESS")
                            {
                                
                                decimal amount = Convert.ToDecimal(ds.Tables[1].Rows[0]["TXNAMOUNT"]);
                                string  TXNID = ds.Tables[1].Rows[0]["txnId"].ToString();
                                string GATEWAYNAME = ds.Tables[1].Rows[0]["gatewayName"].ToString(); ;
                                string  PAYMENTMODE = ds.Tables[1].Rows[0]["paymentMode"].ToString();
                                string  TXNDATE = ds.Tables[1].Rows[0]["txnDate"].ToString();
                                string BANKTXNID = ds.Tables[1].Rows[0]["bankTxnId"].ToString();
                                string RESPMSG = ds.Tables[2].Rows[0]["resultStatus"].ToString();
                                string BANKNAME = "";
                                if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "DC" && amount <= 2000)
                                {

                                    DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                    if (wallet.Rows.Count > 0)
                                    {
                                        return custList;
                                    }
                                    else
                                    {
                                      
                                        decimal NetAmount = amount;
                                        string memberid = userid;
                                        DataTable dtresult = new DataTable();
                                        cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + 0 + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                        int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                        if (balance > 0)
                                        {
                                            return custList;
                                        }
                                        else
                                        {
                                            return custList;
                                        }
                                    }
                                }
                                else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "DC" && amount > 2000)
                                {
                                    DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                    if (wallet.Rows.Count > 0)
                                    {
                                        return custList;
                                    }
                                    else
                                    {
                                        decimal NetAmount = amount;
                                        string memberid = userid;
                                        DataTable dtresult = new DataTable();
                                        cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + 0 + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                        int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);

                                        if (balance > 0)
                                        {
                                            return custList;
                                        }
                                        else
                                        {
                                            return custList;
                                        }
                                    }
                                }
                                else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "CC")
                                {
                                    DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                    if (wallet.Rows.Count > 0)
                                    {
                                        return custList;
                                    }
                                    else
                                    {
                                       
                                        decimal NetAmount = amount;
                                        string memberid = userid;
                                        DataTable dtresult = new DataTable();
                                        cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + 0 + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                        int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                        if (balance > 0)
                                        {
                                            return custList;
                                        }
                                        else
                                        {
                                            return custList;
                                        }
                                    }
                                }
                                else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "PPI")
                                {
                                    DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                    if (wallet.Rows.Count > 0)
                                    {
                                        return custList;
                                    }
                                    else
                                    {
                                       
                                        decimal NetAmount = amount;
                                        string memberid = userid;
                                        DataTable dtresult = new DataTable();
                                       cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + 0 + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                        int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                        if (balance > 0)
                                        {
                                            return custList;
                                        }
                                        else
                                        {
                                            return custList; 
                                        }
                                    }

                                }
                                else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "NB")
                                {
                                    DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                    if (wallet.Rows.Count > 0)
                                    {
                                        return custList;
                                       
                                    }
                                    else
                                    {

                                        
                                        decimal NetAmount = amount;
                                        string memberid = userid;
                                        DataTable dtresult = new DataTable();
                                        cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + 0 + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                        int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                        if (balance > 0)
                                        {
                                            return custList;
                                           
                                        }
                                        else
                                        {
                                            return custList;
                                          
                                        }
                                    }

                                }
                                else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "UPI")
                                {
                                    DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                    if (wallet.Rows.Count > 0)
                                    {
                                        return custList;
                                       
                                    }
                                    else
                                    {

                                        decimal NetAmount = amount;
                                        string memberid = userid;
                                        DataTable dtresult = new DataTable();
                                       cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + 0 + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                        int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);

                                        if (balance > 0)
                                        {
                                            return custList;
                                            
                                        }
                                        else
                                        {
                                           // return custList;
                                            
                                        }
                                    }

                                }
                                else
                                {
                                    DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                    if (wallet.Rows.Count > 0)
                                    {
                                        return custList;
                                        
                                    }
                                    else
                                    {

                                        decimal NetAmount = amount;
                                        string memberid = userid;
                                        DataTable dtresult = new DataTable();
                                       cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + 0 + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                        int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                        if (balance > 0)
                                        {
                                            return custList;
                                           
                                        }
                                        else
                                        {
                                            return custList;
                                           
                                        }
                                    }

                                }
                            }
                            else if(ds.Tables[2].Rows[0]["resultStatus"].ToString() == "PENDING")
                            {
                                cls.select_data_dt(@"Update tbl_paytm_paymentgateway set  Statuss='" + "Pending" + "' Where  TransactionId='" + ORDERID + "'");
                            }
                            else
                            {
                                cls.select_data_dt(@"Update tbl_paytm_paymentgateway set  Statuss='" + "Fail" + "' Where  TransactionId='" + ORDERID + "'");
                                
                            }
                        }
                        else
                        {
                            return custList;
                           
                        }
                    }
                    else
                    {
                        return custList;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Data Found Please Check After Some Time!');window.location ='DashBoard.aspx';", true);
                    }
                }
                else
                {
                    return custList;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Please Try After Some Time!');window.location ='DashBoard.aspx';", true);
                }
            }
        }
        return custList;

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Customer> approved(string txnid)
    {
        cls_connection cls = new cls_connection();
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        if (txnid != "")
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from tbl_paytm_paymentgateway where TransactionId='" + txnid + "'and Statuss='Pending'");
            String Result = string.Empty;
            if (dt.Rows.Count > 0)
            {
                string memberid = (dt.Rows[0]["memberid"].ToString());
                string amount = (dt.Rows[0]["amount"].ToString());
                cls_myMember clsm = new cls_myMember();
                string statuss = "Success";
                cls.select_data_dt(@"Update tbl_paytm_paymentgateway set Statuss='" + statuss + "' Where  TransactionId='" + txnid + "'");
                clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(amount), "Cr", "Force Add Fund PG TXN ID : " + txnid);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Member Wallet Amount Add Successfull!');location.replace('paymentgateway_report.aspx');", true);
                return custList;
            }
            else
            {
                //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Found!');location.replace('paymentgateway_report.aspx');", true);
            }
            return custList;
        }

        return custList;
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
        public string GATEWAYNAME { get; set; }
        public string TxnID { get; set; }
        public string TransactionId { get; set; }
        public string method { get; set; }
        public string Amount { get; set; }
        public string feerate { get; set; }
        public string payid { get; set; }
        public string BANKNAME { get; set; }
        public string orderid { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
        public string callbackstatus { get; set; }
        public string TxnDate { get; set; }
        public string RESPMSG { get; set; }
        public string BANKTXNID { get; set; }
        public string amount { get; set; }
        public string totalamount { get; set; }
        public string createdate { get; set; }
        public string email { get; set; }
    }


    #endregion
    public double TotupAmount(double amount, double rate)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0;
        if (amount > 0)
        {
            surcharge_rate = rate;
            if (surcharge_rate > 0)
            {
                surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
            }
            NetAmount = surcharge_amt;
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }

    public double TotupAmountGST(double amount, double rate)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0;
        if (amount > 0)
        {
            surcharge_rate = rate;
            if (surcharge_rate > 0)
            {
                surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
            }
            NetAmount = surcharge_amt;
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }

}