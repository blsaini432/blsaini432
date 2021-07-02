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
public partial class paymentcallbackAeps : System.Web.UI.Page
{
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    public string status;
    public string wallet;
    public string method;
    public string cardid;
    public string vpa;

   // private static string key = "rzp_live_OH8svmlbLLNzzU";
  //  private static string secret = "cVkUy5KyIaHScj40ZQzrRkoQ";
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
            string key ="rzp_live_NY32WiX9K7Kzyy";
            string secret ="j041nv8KuZT8rEipmFXzUG8h";
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
                       
                    double rate = 1.5;
                    var memberid = Session["MemberId"].ToString();
                    var transid = Session["txnid"].ToString();
                    var payid = Session["PayOrderId"].ToString();
                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                    double NetAmount = Convert.ToDouble(amount) - feeamount;
                    int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + NetAmount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                    clsm.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Cash on Credit Aeps TXN ID : " + payid);
                    setControl();
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Found Add AEPS Wallet sucessfully');window.location ='paymentPgAeps.aspx';", true);                                   
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='paymentPgAeps.aspx';", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='paymentPgAeps.aspx';", true);
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