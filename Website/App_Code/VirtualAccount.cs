using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Collections;
using System.Xml;
using Newtonsoft.Json;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

/// <summary>
/// 
/// </summary>
public class VirtualAccount
{
    public string orderId;
    private static string M_Uri = "http://api.razorpay.com/v1/virtual_accounts";
    private static string secret = "qYqGduo0XTIyv3tfJyfeaps5";
    private static string api_key = "rzp_test_URWNybLYvGAURx";
    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {


            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            //httpWebRequest.UseDefaultCredentials = true;
            //httpWebRequest.PreAuthenticate = true;
            //httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
            String YOUR_KEY_ID = "rzp_test_URWNybLYvGAURx";
            String YOUR_KEY_SECRET = "qYqGduo0XTIyv3tfJyfeaps5";
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(YOUR_KEY_ID + ":" + YOUR_KEY_SECRET));
            httpWebRequest.Headers.Add("Authorization", "Basic" + " " + encoded);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }

        }
        catch (WebException ex)
        {
            // List<ParmList> _lstparm = new List<ParmList>();
            //_lstparm.Add(new ParmList() { name = "@Action", value = "I" });
            // Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer_new", _lstparm);
            var httpResponse = (HttpWebResponse)ex.Response;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    #endregion
    public static string HTTP_Request_BBPS(string requestJson, string URL)
    {
        string response = String.Empty;
        string Out = String.Empty;
        cls_connection objconnection = new cls_connection();
        try
        {
            //System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            //// var webrequest = (HttpWebRequest)WebRequest.Create(URL);
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768;
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.razorpay.com/v1/virtual_accounts");
            //request.Method = "POST";
            //request.ContentLength = 0;
            //request.ContentType = "application/json";
            //String key = "rzp_test_URWNybLYvGAURx";
            //String secrets = "qYqGduo0XTIyv3tfJyfeaps5";
            //string authString = string.Format("{0}:{1}", key, secrets);
            //request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
            //var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(requestJson));
            //request.ContentLength = data.Length;
            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}
            //var orderResponse = (HttpWebResponse)request.GetResponse();
            //using (var streamReader = new StreamReader(orderResponse.GetResponseStream()))
            //{
            //    Out = streamReader.ReadToEnd();
            //}

            //string Json = "{\"receivers\": {\"types\": [\"" + OP + "\"]},\"description\": \"" + description + "\",\"customer_id\": \"" + customer_id + "\",\"close_by\":  1681615838  ,\"notes\": {\"project_name \": \"" + project + "\"}}";
            //Dictionary<string, object> body = new Dictionary<string, object>();
           // List<object> payPaymentModeArray = new List<object>();
           // body.Add("description", "Virtual Account created for Raftar Soft");
          //  body.Add("customer_id", "cust_CaVDm8eDRSXYME");
           // body.Add("close_by", "1681615838");
          //  body.Add("types", "bank_account");
           // body.Add("channels", "UPIPUSH");
          //  body.Add(PaymentMode);
           // body.Add("PaymentMode", payPaymentModeArray);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.razorpay.com/v1/virtual_accounts");
            request.Method = "POST";
            request.ContentLength = 0;
            request.ContentType = "application/json";
            String key = "rzp_test_URWNybLYvGAURx";
            String secrets = "qYqGduo0XTIyv3tfJyfeaps5";
            string authString = string.Format("{0}:{1}", key, secrets);
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(requestJson));
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var orderResponse = (HttpWebResponse)request.GetResponse();
            orderResponse = (HttpWebResponse)request.GetResponse();

           // JObject orderData = ParseResponse(orderResponse);
           //var orderId = orderData["id"].ToString();
        }
        catch (WebException ex)
        {
            if (ex.Status == WebExceptionStatus.Timeout)
            {

            }
            else
            {

            }
        }
        string myresponse = Out.ToString();
        return response = myresponse;
    }

    #region HTTP_POST
    public static string HTTP_POSTpay(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            // WebRequest httpWebRequest = WebRequest.Create(Url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.razorpay.com/v1/virtual_accounts/" + "va_G4YpEUCsFVPMBG");
            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "application/json";
            String key = "rzp_test_URWNybLYvGAURx";
            String secrets = "qYqGduo0XTIyv3tfJyfeaps5";
            string authString = string.Format("{0}:{1}", key, secrets);
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
            var paymentResponse = (HttpWebResponse)request.GetResponse();
            // public string paymentStatus;
            using (var streamReader = new StreamReader(paymentResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }

            //JObject paymentStatusData = ParseResponse(paymentResponse);
            // paymentStatus = paymentStatusData["status"].ToString();


        }
        catch (WebException ex)
        {
            var httpResponse = (HttpWebResponse)ex.Response;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
        }
        string myresponse = Out.ToString();
        return myresponse;
        // DataSet ds = Deserialize(myresponse);
    }
    #endregion

    #region Method

    public string VirtualAccounts(string OP, string description, string customer_id, string project)
    {
        string Json = "{\"receivers\": {\"types\": [\"" + OP + "\"]},\"description\": \"" + description + "\",\"customer_id\": \"" + customer_id + "\",\"close_by\":  1681615838  ,\"notes\": {\"project_name \": \"" + project + "\"}}";
        // string Json = "{\"HEADER\": {\"OP\": \"" + OP + "\",\"ST\": \"" + ST + "\",\"PAYABLE_AMOUNT\": \"" + PAYABLE_AMOUNT + "\",\"TXN_AMOUNT\": \"" + TXN_AMOUNT + "\",\"AID\": \"" + AID + "\"},\"DATA\":{\"CUSTOMER_MOBILE\": \"" + mobile + "\",\"BENE_ID\": \"" + BENE_ID + "\",\"CN\": \"" + CN + "\",\"ORDER_ID\": \"" + ORDER_ID + "\",\"KEY_KYC_STATUS\": \"" + KEY_KYC_STATUS + "\",\"TRANSFER_TYPE\": \"" + TRANSFER_TYPE + "\",\"BENE_MOBILENO\": \"" + BENE_MOBILENO + "\",\"BENE_NAME\": \"" + beniname + "\",\"BANKIFSC_CODE\": \"" + Bankifsc + "\",\"BANK_ACCOUNTNO\": \"" + beniacccount + "\",\"BENE_BANKNAME\": \"" + bankname + "\"}}";
        //  string Json = "{\"receivers\": {\"types\": \"" + sp_key + "\",\"customer_mobile\": \"" + customer_mobile + "\",\"customer_params\":[\"" + returnjson + "\",\"\"],\"init_channel\": \"" + init_channel + "\",\"endpoint_ip\": \"" + endpoint_ip + "\",\"mac\": \"" + mac + "\",\"payment_mode\": \"" + payment_mode + "\",\"payment_info\": \"" + payment_info + "\",\"amount\": \"" + amount + "\",\"reference_id\": \"" + reference_id + "\",\"latlong\": \"" + latlong + "\",\"outletid\": \"" + outletid + "\"}}";
        //  string Json = "{\"receivers\": {\"types\": [\"bank_account"" \"" + ST + "\",\"AID\": \"" + AID + "\"},\"DATA\":{\"CUSTOMER_MOBILE\": \"" + mobile + "\",\"BENE_ID\": \"" + BENE_ID + "\",\"CN\": \"" + CN + "\",\"ORDER_ID\": \"" + ORDER_ID + "\",\"KEY_KYC_STATUS\": \"" + KEY_KYC_STATUS + "\",\"TRANSFER_TYPE\": \"" + TRANSFER_TYPE + "\",\"BENE_MOBILENO\": \"" + BENE_MOBILENO + "\",\"BENE_NAME\": \"" + beniname + "\",\"BANKIFSC_CODE\": \"" + Bankifsc + "\",\"BANK_ACCOUNTNO\": \"" + beniacccount + "\",\"BENE_BANKNAME\": \"" + bankname + "\"}}";
        string jsonData = Json.Replace(@"\", "");
        //    //string jsonDataa = Json.Replace("}", "}}");
        return HTTP_Request_BBPS(jsonData, M_Uri);
        //return HTTP_POST(M_Uri, jsonData);
    }

    private static string ComputeHash(string secret, string Request_Json)
    {
        var key = Encoding.UTF8.GetBytes(Request_Json);
        string signature;
        using (var hmac = new HMACSHA256(key))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(secret));
            signature = BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        return signature;
    }

    private JObject ParseResponse(HttpWebResponse response)
    {
        string responseValue = string.Empty;
        using (var responseStream = response.GetResponseStream())
        {
            if (responseStream != null)
                using (var reader = new StreamReader(responseStream))
                {
                    responseValue = reader.ReadToEnd();
                }
        }

        JObject responseObject = JObject.Parse(responseValue);

        return responseObject;
    }
    #endregion


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