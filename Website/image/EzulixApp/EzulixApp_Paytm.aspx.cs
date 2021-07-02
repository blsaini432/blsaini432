using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Net;
using System.IO;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Xml;
using System.Globalization;
using BLL.MLM;
using Paytm;
public partial class EzulixApp_Paytm : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    public static string adminurl = ConfigurationManager.AppSettings["adminurl"];
    clsMLM_RWalletTransaction objRWalletTransaction = new clsMLM_RWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    private static int limitamount = 5000;
    string mid = "Cybdee15771167977955";
    string key = "4tJ4XRfjoH&Ac5dP";
    public string ConvertDataTabletoString(DataTable dt)
    {

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);

    }
    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }
    protected string ReplaceCode(string str)
    {
        return str.Replace("'", "").Replace("-", "").Replace(";", "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["operationname"] != null)
        {
            string OperationName = Request.Form["operationname"].ToString();
            if (OperationName == "paytmchecksum")
            {
                #region signup
                if (Request.Form["AMOUNT"] != null && Request.Form["USER_ID"] != null)
                {
                    string AMOUNT = Request.Form["AMOUNT"].ToString();
                    string USER_ID = Request.Form["USER_ID"].ToString();
                    string orderid = Request.Form["orderid"].ToString();
                    string msrno = Request.Form["msrno"].ToString();
                    string mobile = Request.Form["mobile"].ToString();
                    string email = Request.Form["email"].ToString();
                    string callbackurl = adminurl + "EzulixApp/EzulixApp_Paytmcallback.aspx";
                    Dictionary<string, object> body = new Dictionary<string, object>();
                    Dictionary<string, string> head = new Dictionary<string, string>();
                    Dictionary<string, object> requestBody = new Dictionary<string, object>();
                    Dictionary<string, string> txnAmount = new Dictionary<string, string>();
                    Dictionary<string, string> userInfo = new Dictionary<string, string>();
                    Dictionary<string, string> PaymentMode = new Dictionary<string, string>();
                   // List<object> payPaymentModeArray = new List<object>();
                    txnAmount.Add("value", AMOUNT);
                    txnAmount.Add("currency", "INR");
                    userInfo.Add("custId", USER_ID);
                  //  PaymentMode.Add("mode", "UPI");
                  //  PaymentMode.Add("channels", "UPIPUSH");
                  //  payPaymentModeArray.Add(PaymentMode);
                   // body.Add("PaymentMode", payPaymentModeArray);
                    body.Add("requestType", "Payment");
                    body.Add("mid", mid);
                    body.Add("websiteName", "DEFAULT");
                    body.Add("orderId", orderid);
                    body.Add("txnAmount", txnAmount);
                    body.Add("userInfo", userInfo);
                    body.Add("callbackUrl", callbackurl);
                    string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), key);
                    head.Add("signature", paytmChecksum);
                    requestBody.Add("body", body);
                    requestBody.Add("head", head);
                    string post_data = JsonConvert.SerializeObject(requestBody);
                    //For  Staging
                    // string url = "https://securegw-stage.paytm.in/theia/api/v1/initiateTransaction?mid=aTfNnZ57644717475421&orderId=" + orderid + "";

                    //For  Production 
                    string url = "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=" + mid + "&orderId=" + orderid + "";
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
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["signature"] != null)
                                {
                                    if (ds.Tables[2].Rows[0]["resultStatus"].ToString() == "S")
                                    {
                                        string signature = ds.Tables[0].Rows[0]["signature"].ToString();
                                        string txnToken = ds.Tables[1].Rows[0]["txnToken"].ToString();
                                        ViewState["txnToken"] = txnToken;
                                        cls.insert_data("Exec dbo.Paytm_paymentgateway null,'" + USER_ID + "', '" + msrno + "' ,'" + txnToken + "', '" + "null" + "','" + null + "', '" + null + "' , '" + "Pending" + "','" + AMOUNT + "','" + "null" + "', '" + "null" + "' , '" + "null" + "' , '" + "AddWallet" + "', '" + mobile + "','" + email + "','" + orderid + "' ");
                                        string Requcest_Json = responseData.Replace(@"\", "");
                                        Response.Write(Requcest_Json);
                                    }
                                    else
                                    {
                                        ReturnError("Some Error Found exists in system !!", "token");
                                    }

                                }
                                else
                                {
                                    ReturnError("Some Error Found exists in system !!", "token");
                                }
                            }
                            else
                            {
                                ReturnError("Some Error Found exists in system !!", "token");
                            }

                        }
                        else
                        {
                            ReturnError("Some Error Found exists in system !!!", "token");
                        }
                      
                    }
                }
            }
            else
            {
                ReturnError("Member already exists in system !!", "signup");
            }

        }
        else
        {

        }
        #endregion
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
    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }


}


