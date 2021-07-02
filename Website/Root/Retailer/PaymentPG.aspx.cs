using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;
using System.Net;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;


public partial class paymentPG : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public string orderId;
    public decimal amount;
    public string number;
    public string emailsend;
    public string name;
    string keyid = "rzp_live_Fw9lbW17NoC0iT";
    string secretkey = "3K00zGFiqn7LLMxjR1eDhydr";

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
        if (Session["checkout"] != null  && Session["txtAmount"] != null && Session["Returnurl"] != null)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            decimal amt = Convert.ToDecimal(Session["txtAmount"].ToString());
            decimal namt = amt;
            string finalamount = namt.ToString();
            string finalamounts = namt.ToString("N0");
            string amount = finalamounts + "00";
            decimal pgamount = Convert.ToDecimal(amount);
            input.Add("amount", pgamount); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "order_rcptid_11");
            input.Add("payment_capture", 1);
            string key = keyid.ToString() ;
            string secret = secretkey.ToString();
            RazorpayClient client = new RazorpayClient(key, secret);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768;
            Razorpay.Api.Order order = client.Order.Create(input);
            orderId = order["id"].ToString();
            Session["PayOrderId"] = orderId;
            DataTable dtMemberMaster = (DataTable)Session["dtRetailer"];
            number = dtMemberMaster.Rows[0]["Mobile"].ToString();
            emailsend = dtMemberMaster.Rows[0]["Email"].ToString();
            name = dtMemberMaster.Rows[0]["FirstName"].ToString();
            if (Session["Returnurl"].ToString() == "addwallet")
            {
                var txnid = Session["txnid"].ToString();
                cls.insert_data("Exec dbo.insertpaymentGateway null,'" + dtMemberMaster.Rows[0]["MemberID"].ToString() + "', '" + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "' , '" + orderId + "','" + null + "', '" + null + "' , '" + "Pending" + "','" + amt + "','" + "null" + "', '" + "null" + "' , '" + "null" + "' , '" + "AddWallet" + "', '" + dtMemberMaster.Rows[0]["Mobile"].ToString() + "','" + dtMemberMaster.Rows[0]["Email"].ToString() + "','" + txnid + "' ");
            }
            var amm = Convert.ToDecimal(Session["txtAmount"].ToString());
            amount = finalamount;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Session logout restart process')", true);
        }

    }
}