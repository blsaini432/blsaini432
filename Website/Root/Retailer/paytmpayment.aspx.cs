using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using Paytm;
using System.IO;
using System.Xml;

public partial class paytmpayment : System.Web.UI.Page
{
    #region 

    public static string adminurl = ConfigurationManager.AppSettings["adminurl"];
    cls_connection cls = new cls_connection();
    public string orderId;
    public decimal amount;
    public string name;
    public string txnToken;
    #endregion

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
            Response.Redirect("~/paytmgateway.aspx");
        }
        if (Session["userid"] != null && Session["Amount"] != null)
        {
            try
            {
                string callbackurl = adminurl + "Root/Retailer/Paytmcallback.aspx";
                string AMOUNT = (Session["Amount"].ToString());
                string USER_ID = (Session["userid"].ToString());
                string orderid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
                ViewState["orderId"] = orderid;
                Session["orderId"] = orderid;
                ViewState["AMOUNT"] = AMOUNT;
                Dictionary<string, object> body = new Dictionary<string, object>();
                Dictionary<string, string> head = new Dictionary<string, string>();
                Dictionary<string, object> requestBody = new Dictionary<string, object>();
                Dictionary<string, string> txnAmount = new Dictionary<string, string>();
                Dictionary<string, string> userInfo = new Dictionary<string, string>();
                Dictionary<string, string> PaymentMode = new Dictionary<string, string>();
                List<object> payPaymentModeArray = new List<object>();
                txnAmount.Add("value", AMOUNT);
                txnAmount.Add("currency", "INR");
                userInfo.Add("custId", USER_ID);
                PaymentMode.Add("mode", "UPI");
                PaymentMode.Add("channels", "UPIPUSH");
                payPaymentModeArray.Add(PaymentMode);
                body.Add("PaymentMode", payPaymentModeArray);
                body.Add("requestType", "Payment");
                body.Add("mid", "Cybdee15771167977955");
                body.Add("websiteName", "Website");
                body.Add("orderId", orderid);
                body.Add("txnAmount", txnAmount);
                body.Add("userInfo", userInfo);
                body.Add("callbackUrl", callbackurl);
                string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), "4tJ4XRfjoH&Ac5dP");
                head.Add("signature", paytmChecksum);
                requestBody.Add("body", body);
                requestBody.Add("head", head);
                string post_data = JsonConvert.SerializeObject(requestBody);
                //For  Staging
             //  string url = "https://securegw-stage.paytm.in/theia/api/v1/initiateTransaction?mid=Runfin68900715907061&orderId=" + orderid + "";

                //For  Production 
               string  url  = "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=Cybdee15771167977955&orderId=" + orderid + "";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = post_data.Length;
                using (var requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(post_data);
                }
                string responseData = string.Empty;
                using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    responseData = responseReader.ReadToEnd();
                    if (responseData != string.Empty)
                    {
                        DataSet ds = Deserialize(responseData);
                        if(ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["signature"] != null)
                            {
                                if (ds.Tables[2].Rows[0]["resultStatus"].ToString() == "S")
                                {
                                    string signature = ds.Tables[0].Rows[0]["signature"].ToString();
                                    txnToken = ds.Tables[1].Rows[0]["txnToken"].ToString();
                                    ViewState["txnToken"] = txnToken;
                                    DataTable dtMemberMaster = (DataTable)Session["dtRetailer"];
                                   cls.insert_data("Exec dbo.Paytm_paymentgateway null,'" + USER_ID + "', '" + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "' ,'" + txnToken + "', '" + orderId + "','" + null + "', '" + null + "' , '" + "Pending" + "','" + AMOUNT + "','" + "null" + "', '" + "null" + "' , '" + "null" + "' , '" + "AddWallet" + "', '" + dtMemberMaster.Rows[0]["Mobile"].ToString() + "','" + dtMemberMaster.Rows[0]["Email"].ToString() + "','" + orderid + "' ");
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Please Try After Some Time!');window.location ='DashBoard.aspx';", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Please Try After Some Time!');window.location ='DashBoard.aspx';", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Data Found Please Try After Some Time!');window.location ='DashBoard.aspx';", true);
                        }
                 
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Time Out!');window.location ='DashBoard.aspx';", true);
                    }
                }
            }
            catch (Exception ex)
            {
                cls.select_data_dt("insert into ErrorLog values('" + ex.ToString() + "')");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Time Out!');window.location ='DashBoard.aspx';", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Session logout restart process')", true);
        }
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
}


