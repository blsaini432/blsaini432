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


public partial class paymentcallbackPG : System.Web.UI.Page
{
    cls_connection Cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    public string status;
    public string wallet;
    public string method;
    public string cardid;
    public string vpa;

    private static string keyid = "rzp_live_Fw9lbW17NoC0iT";
    private static string secretkey = "3K00zGFiqn7LLMxjR1eDhydr";
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
            Response.Redirect("~/paymentprocessPG.aspx");
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
            string key = keyid;
            string secret = secretkey;
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
                var memberid = Session["MemberId"].ToString();
                int Amount = Convert.ToInt32(amount);
                double feeAmount = TotupAmount(Convert.ToDouble(Amount));
                double NetAmount = Convert.ToDouble(amount) - feeAmount;
                var transid = Session["txnid"].ToString();
                var payid = Session["PayOrderId"].ToString();
                int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeAmount + "',totalamount ='" + amount + "',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                //int data = cls.update_data("update  tbl_paymentGateway set Updatedate='" + DateTime.Now + "', Statuss ='" + status + "' , amount ='" + amount + "', payid ='" + paymentId + "',Signid='" + Request.Form["razorpay_signature"].ToString() + "' , cardid ='" + payment["card_id"].ToString() + "', vpa ='" + payment["vpa"].ToString() + "',feerate ='" + feeamount + "',totalamount ='" + amount + "',servicename='"+ servicename + "',serviceid='1',method='" + payment["method"].ToString() + "' where POrderid ='" + payid + "'");
                clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund  PG TXN ID : " + payid);
                setControl();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction sucessfully');window.location ='PaymentGateway_report.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='paymentprocessPG.aspx';", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='paymentprocessPG.aspx';", true);
        }

    }
    //public double TotupAmount(double amount, double rate)
    //{
    //    double NetAmount = 0;
    //    double surcharge_amt = 0; double surcharge_rate = 0;
    //    if (amount > 0)
    //    {
    //        surcharge_rate = rate;
    //        if (surcharge_rate > 0)
    //        {
    //            surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
    //        }
    //        NetAmount = surcharge_amt;
    //    }
    //    else
    //    {
    //        NetAmount = 0;
    //    }
    //    return NetAmount;
    //}
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
    public double TotupAmount(double amount)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            DataTable dtMemberMaster = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='chk', @msrno=" + Session["MsrNo"] + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='PG',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
            if (dtsr.Rows.Count > 0)
            {
                surcharge_rate = Convert.ToDouble(dtsr.Rows[0]["commision"].ToString());
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
            else
            {
                NetAmount = 0;
            }
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }

}