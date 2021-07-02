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

public partial class paymentAeps : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public string orderId;
    public decimal amount;
    public string number;
    public string emailsend;
    public string name;

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
        if (Session["checkout"] != null  && Session["txtAmount"] != null && Session["Returnurl"] != null)
        {
            Dictionary<string, object> input = new Dictionary<string, object>();
            decimal amt = Convert.ToDecimal(Session["txtAmount"].ToString());
            decimal namt = amt;
            string finalamount = 1 + "00";
            decimal pgamount = Convert.ToDecimal(finalamount);
            input.Add("amount", pgamount); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "order_rcptid_11");
            input.Add("payment_capture", 1);
            string key ="rzp_live_NY32WiX9K7Kzyy";
            string secret ="j041nv8KuZT8rEipmFXzUG8h";
            RazorpayClient client = new RazorpayClient(key, secret);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768;
            Razorpay.Api.Order order = client.Order.Create(input);
            orderId = order["id"].ToString();
            Session["PayOrderId"] = orderId;
            DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
            number = dtMemberMaster.Rows[0]["Mobile"].ToString();
            emailsend = dtMemberMaster.Rows[0]["Email"].ToString();
            name = dtMemberMaster.Rows[0]["FirstName"].ToString();
            if (Session["Returnurl"].ToString() == "addwallet")
            {

                var txnid = Session["txnid"].ToString();
                cls.insert_data("Exec dbo.insertpaymentGateway null,'" + dtMemberMaster.Rows[0]["MemberID"].ToString() + "', '" + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "' , '" + orderId + "','" + null + "', '" + null + "' , '" + "Pending" + "','" + amt + "','" + "null" + "', '" + "null" + "' , '" + "null" + "' , '" + "AddWallet" + "', '" + dtMemberMaster.Rows[0]["Mobile"].ToString() + "','" + dtMemberMaster.Rows[0]["Email"].ToString() + "','" + txnid + "' ");
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