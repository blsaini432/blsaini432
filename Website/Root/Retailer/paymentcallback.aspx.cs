using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;
using System.Collections.Specialized;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
using System.Xml;
public partial class paymentcallback : System.Web.UI.Page
{
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    public string status;
    public string wallet;
    public string method;
    public string cardid;
    public string vpa;

    private static string key = "rzp_live_OH8svmlbLLNzzU";
    private static string secret = "cVkUy5KyIaHScj40ZQzrRkoQ";
    protected void Page_Load(object sender, EventArgs e)
    {
        string strPreviousPage = "";
        if (Request.UrlReferrer != null)
        {
            strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        }
        if (strPreviousPage == "")
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/paymentprocess.aspx");
        }
        if (Session["PayOrderId"] != null && Session["tx"] != null && Request.Form["razorpay_payment_id"] != null)
        {

            var paymentId = Request.Form["razorpay_payment_id"];
            string razorpay_order_id = Request.Form["razorpay_order_id"];
            string razorpay_signature = Request.Form["razorpay_signature"];
            Dictionary<string, object> input = new Dictionary<string, object>();
            decimal amount = Convert.ToDecimal(Session["txtAmount"].ToString());
            string finalamount = amount + "00";
            decimal pgamount = Convert.ToDecimal(finalamount);
            input.Add("amount", pgamount); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "order_rcptid_11");
            input.Add("payment_capture", 1);// this amount should be same as transaction amount
            string key = Convert.ToString(ConfigurationManager.AppSettings["key"]);
            string secret = Convert.ToString(ConfigurationManager.AppSettings["secret"]);
            RazorpayClient client = new RazorpayClient(key, secret);
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("razorpay_payment_id", paymentId);
            attributes.Add("razorpay_order_id", razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay_signature);
            Utils.verifyPaymentSignature(attributes);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768;
            Payment payment = client.Payment.Fetch(paymentId);
            status = payment["status"].ToString();
            cardid = payment["card_id"].ToString();
            vpa = payment["vpa"].ToString();
            method = payment["method"].ToString();
            // Card paymentt = client.Card.Fetch((string)paymentId);

            if (Session["Returnurl"].ToString() == "addwallet")
            {
                if (method == "upi")
                {
                    double rate = 1.5;
                    var memberid = Session["MemberId"].ToString();
                    var transid = Session["txnid"].ToString();
                    var payid = Session["PayOrderId"].ToString();
                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                    double NetAmount = Convert.ToDouble(amount) - feeamount;
                    int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                    clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found upi PG TXN ID : " + payid);
                    setControl();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Found Add Wallet sucessfully');window.location ='paymentgateway_report.aspx';", true);

                }
                else if (method == "wallet")
                {

                    double rate = 1.5;
                    var memberid = Session["MemberId"].ToString();
                    var transid = Session["txnid"].ToString();
                    var payid = Session["PayOrderId"].ToString();
                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                    double NetAmount = Convert.ToDouble(amount) - feeamount;
                    int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                    clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found wallet PG TXN ID : " + payid);
                    setControl();
                }
                else if (method == "card")
                {
                    string Out = String.Empty;
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.razorpay.com/v1/payments/" + paymentId + "/card");
                        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.razorpay.com/v1/payments/" + paymentId);
                        request.Method = "GET";
                        request.ContentLength = 0;
                        request.ContentType = "application/json";
                        string authString = string.Format("{0}:{1}", key, secret);
                        request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));

                        var paymentResponse = (HttpWebResponse)request.GetResponse();
                        using (var streamReader = new StreamReader(paymentResponse.GetResponseStream()))
                        {
                            Out = streamReader.ReadToEnd();
                        }

                    }

                    catch (WebException ex)
                    {

                        using (WebResponse response = ex.Response)
                        {

                            HttpWebResponse httpResponse = (HttpWebResponse)response;
                            Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                            using (Stream aadata = response.GetResponseStream())
                            {

                                string ErrorText = new StreamReader(aadata).ReadToEnd();
                                Debug.WriteLine(ErrorText);
                            }
                        }
                    }
                    string myresponse = Out.ToString();
                    DataSet ds = Deserialize(myresponse);
                    string network = ds.Tables[0].Rows[0]["network"].ToString();
                    string type = ds.Tables[0].Rows[0]["type"].ToString();
                    if (type == "debit")
                    {
                        if (network == "Visa" && type == "debit" && amount > 2000)
                        {
                            double rate = 1.5;
                            var memberid = Session["MemberId"].ToString();
                            var transid = Session["txnid"].ToString();
                            var payid = Session["PayOrderId"].ToString();
                            double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                            double NetAmount = Convert.ToDouble(amount) - feeamount;
                            int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found visa PG TXN ID : " + payid);
                            setControl();
                        }
                        else if (network == "Visa" && type == "debit" && amount < 2000)
                        {
                            double rate = 1.5;
                            var memberid = Session["MemberId"].ToString();
                            var transid = Session["txnid"].ToString();
                            var payid = Session["PayOrderId"].ToString();
                            double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                            double NetAmount = Convert.ToDouble(amount) - feeamount;
                            int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found via PG TXN ID : " + payid);
                            setControl();
                        }
                        else if (network == "MasterCard" && type == "debit" && amount < 2000)
                        {
                            double rate = 1.5;
                            var memberid = Session["MemberId"].ToString();
                            var transid = Session["txnid"].ToString();
                            var payid = Session["PayOrderId"].ToString();
                            double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                            double NetAmount = Convert.ToDouble(amount) - feeamount;
                            int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found MasterCard PG TXN ID : " + payid);
                            setControl();
                        }
                        else if (network == "MasterCard" && type == "debit" && amount > 2000)
                        {
                            double rate = 1.5;
                            var memberid = Session["MemberId"].ToString();
                            var transid = Session["txnid"].ToString();
                            var payid = Session["PayOrderId"].ToString();
                            double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                            double NetAmount = Convert.ToDouble(amount) - feeamount;
                            int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found MasterCard PG TXN ID : " + payid);
                            setControl();
                        }
                        else if (network == "RuPay" && type == "debit")
                        {
                            double rate = 1.5;
                            var memberid = Session["MemberId"].ToString();
                            var transid = Session["txnid"].ToString();
                            var payid = Session["PayOrderId"].ToString();
                            double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                            double NetAmount = Convert.ToDouble(amount) - feeamount;
                            int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found RuPay PG TXN ID : " + payid);
                            setControl();
                        }
                    }
                    else if (type == "credit")
                    {
                        double rate = 1.5;
                        var memberid = Session["MemberId"].ToString();
                        var transid = Session["txnid"].ToString();
                        var payid = Session["PayOrderId"].ToString();
                        double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                        double NetAmount = Convert.ToDouble(amount) - feeamount;
                        int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                        clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found credit PG TXN ID : " + payid);
                        setControl();
                    }
                    else
                    {
                        var payid = Session["PayOrderId"].ToString();
                        int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + null + "',totalamount ='" + null + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                        setControl();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact Your Admin Team');window.location ='paymentgateway_report.aspx';", true);
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fund Add Wallet sucessfully');window.location ='paymentgateway_report.aspx';", true);

                }
                else if (method == "netbanking")
                {

                    double rate = 1.5;
                    var memberid = Session["MemberId"].ToString();
                    var transid = Session["txnid"].ToString();
                    var payid = Session["PayOrderId"].ToString();
                    double NetAmount = netbankingrate(Convert.ToDouble(amount), rate);
                    if (NetAmount < 12)
                    {

                        NetAmount = Convert.ToDouble(amount) - 12;
                        int adata = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + 12 + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                        clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found netbanking PG TXN ID : " + payid);
                        setControl();
                    }
                    else
                    {
                        double feeamount = NetAmount;
                        NetAmount = Convert.ToDouble(amount) - feeamount;
                        int adata = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                        clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found netbanking PG TXN ID : " + payid);
                        setControl();
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fund Add Wallet sucessfully');window.location ='paymentgateway_report.aspx';", true);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Fund Add Wallet sucessfully');window.location ='paymentgateway_report.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='paymentprocess.aspx';", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='paymentprocess.aspx';", true);
        }

    }
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
    public double netbankingrate(double amount, double rate)
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
    private DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
    private void setControl()
    {
        Session["PayOrderId"] = null;
        Session["tx"] = null;
        Session["txtAmount"] = null;
        Session["Returnurl"] = null;
    }


}