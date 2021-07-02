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

public partial class payment : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public string orderId;
    public decimal amount;
    public string mobile;
    public string email;
    public string name;
    private static string keyid = "rzp_live_2vjii3tMpb3IJt";
    private static string secretkey = "OUZhdVGkLJIkqu52V9nKjbrx";

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
            Response.Redirect("~/Signup.aspx");
        }
        if (Session["checkout"] != null && Session["txtAmount"] != null && Session["Returnurl"] != null)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            decimal amt = Convert.ToDecimal(Session["txtAmount"].ToString());
            decimal namt = amt;
            string finalamount = namt + "00";
            decimal pgamount = Convert.ToDecimal(finalamount);
            input.Add("amount", pgamount); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "order_rcptid_11");
            input.Add("payment_capture", 1);          
            RazorpayClient client = new RazorpayClient(keyid, secretkey);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768;
            Razorpay.Api.Order order = client.Order.Create(input);
            orderId = order["id"].ToString();
            Session["PayOrderId"] = orderId;
            // DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
             mobile = Session["mobile"].ToString();
             email = Session["Email"].ToString();
            name = Session["FirstName"].ToString();
            if (Session["Returnurl"].ToString() == "signup")
            {
                var txnid = Session["txnid"].ToString();
                cls.insert_data("Exec dbo.insertpaymentGateway null,'" + "" + "', '" + "" + "' , '" + orderId + "','" + null + "', '" + null + "' , '" + "Pending" + "','" + amt + "','" + "null" + "', '" + "null" + "' , '" + "null" + "' , '" + "signup" + "', '" + mobile + "','" + email + "','" + txnid + "' ");
            }
            var amm = Convert.ToDecimal(Session["txtAmount"].ToString());
            amount = namt;

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Session logout restart process')", true);
        }

    }
}