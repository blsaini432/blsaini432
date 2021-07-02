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

public partial class paymentcallback_pe : System.Web.UI.Page
{
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    public string status;
    public string wallet;
    public string method;
    public string cardid;
    public string vpa;
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
        if (Session["PayOrderId"] != null && Session["txnid"] != null && Request.Form["razorpay_payment_id"] != null)
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
            RazorpayClient client = new RazorpayClient(keyid, secretkey);
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

            if (Session["Returnurl"].ToString() == "signup")
            {
                string tx = Convert.ToString(Session["txnid"]);
                int id = Convert.ToInt32(Session["id"]);
                #region [Insert]
                Int32 intresult = 0;
                Random random = new Random();
                int SixDigit = random.Next(100000, 999999);
                string MemberID = "";
                MemberID = SixDigit.ToString();
                string DOJ = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
                string MDOB = "";
                MDOB = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
                try
                {
                    //string strimage = profilepicupload(fupmppic);
                    string strimage = "";
                    DataTable dtresult = new DataTable();
                    int pwd4digit = random.Next(1000, 9999);
                    int transpin = random.Next(1000, 9999);
                    string password = pwd4digit.ToString();
                    string transpassord = transpin.ToString();
                    string hdfvalue = "";
                    string firstname = Session["FirstName"].ToString();
                    string lastname = Session["LastName"].ToString();
                    string Name = firstname + "" + lastname;
                    string email = Session["Email"].ToString();
                    string mobile = Session["mobile"].ToString();
                    string stdcode = Session["Stdcode"].ToString();
                    string landline = Session["landline"].ToString();
                    string address = Session["address"].ToString();
                    string country = Session["country"].ToString();
                    string state = Session["state"].ToString();
                    string city = Session["city"].ToString();
                    string cityname = Session["cityname"].ToString();
                    string type = Session["type"].ToString();
                    string zip = Session["zip"].ToString();
                    string ss = "Received";
                    DateTime dd = DateTime.Now;
                    dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + firstname + "','" + lastname + "','" + email + "','" + "" + "','','" + password + "','" + transpassord + "','" + mobile + "','" + stdcode + "','" + landline + "','" + address + "','" + type + "','" + Convert.ToInt32(country) + "','" + Convert.ToInt32(state) + "','" + Convert.ToInt32(city) + "','" + cityname + "','" + zip + "','','0', '" + hdfvalue + "', '0', '" + tx + "'");
                    cls.update_data("insert into regpaymentdetails(Payment,Name,RequestDate,Amount,txnID,membertype,mobile)values(1,'" + Name + "','" + dd + "','" + amount + "','" + tx + "','" + type + "','" + mobile + "')");
                    intresult = Convert.ToInt32(dtresult.Rows[0][0]);
                    if (intresult > 0)
                    {

                        Session["FirstName"] = null;
                        Session["LastName"] = null;
                        Session["Email"] = null;
                        Session["mobile"] = null;
                        Session["Stdcode"] = null;
                        Session["landline"] = null;
                        Session["address"] = null;
                        Session["type"] = null;
                        Session["country"] = null;
                        Session["state"] = null;
                        Session["city"] = null;
                        Session["cityname"] = null;
                        Session["zip"] = null;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Success|Your request has been sent to admin for approval. Concerning team will contact you soon.');location.replace('Signup.aspx');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');location.replace('Signup.aspx');", true);

                    }
                }
                catch (Exception ex)
                {
                    cls.select_data_dt("insert into mtest values('" + ex.ToString() + "')");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !')location.replace('Signup.aspx');", true);
                }
                #endregion
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='Signup.aspx';", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='Signup.aspx';", true);
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