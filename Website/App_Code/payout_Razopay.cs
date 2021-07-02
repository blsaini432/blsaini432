using System;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class payout_Razopay
{

    public string Contact_Uri = "https://api.razorpay.com/v1/contacts";
    public string fund_accounts = "https://api.razorpay.com/v1/fund_accounts";
    public string payout = "https://api.razorpay.com/v1/payouts";

    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            string Username = "rzp_live_lYBacT7IMKqmn7";
            string password = "RWYl9L8w6gmA2eUmxDrLX4De";
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(Username + ":" + password));
            httpWebRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
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
    }
    #endregion

    #region fund_accounts
    public static string HTTP_POSTfund(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            string Username = "rzp_live_lYBacT7IMKqmn7";
            string password = "RWYl9L8w6gmA2eUmxDrLX4De";
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(Username + ":" + password));
            httpWebRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
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
    }
    #endregion

    #region fund_accounts
    public static string HTTP_POSTpayout(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            string Username = "rzp_live_lYBacT7IMKqmn7";
            string password = "RWYl9L8w6gmA2eUmxDrLX4De";
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(Username + ":" + password));
            httpWebRequest.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
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
    }
    #endregion

    #region HTTP_POST
    public static string HTTP_POSTpay(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            WebRequest request = WebRequest.Create(Url + Data);
            request.Method = "GET";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    XmlTextReader reader = new XmlTextReader(stream);
                }
            }
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
    }
    #endregion

    #region Method

    #region Payout
    public string Payout(string debit_account, string fund_account_id, string credit_amount, string currency, string mode, string purpose, string narration)
    {
        string request = "{\"account_number\":\"" + debit_account + "\",\"fund_account_id\":\"" + fund_account_id + "\",\"amount\":\"" + credit_amount + "\",\"currency\":\"" + currency + "\",\"mode\":\"" + mode + "\",\"purpose\":\"" + purpose + "\",\"narration\":\"" + narration + "\"}";
        string Request_Json = request.Replace(@"\", "");
        return HTTP_POSTpayout(payout, Request_Json);
    }
    #endregion

    #region fundPayout
    public string fundPayout(string contact_id, string account_type, string account_number, string ifsc, string Name)
    {
        string request = "{\"contact_id\": \"" + contact_id + "\",\"account_type\": \"" + account_type + "\",\"bank_account\":{\"name\":\"" + Name + "\",\"ifsc\":\"" + ifsc + "\",\"account_number\":\"" + account_number + "\"}}";
        string Request_Json = request.Replace(@"\", "");
        return HTTP_POSTfund(fund_accounts, Request_Json);
    }
    #endregion

    #region Contact

    public string ContactPayouts(string Name, string Email, string Mobile)
    {
        string request = "{\"name\":\"" + Name + "\",\"email\":\"" + Email + "\",\"contact\":\"" + Mobile + "\"}";
        string Request_Json = request.Replace(@"\", "");
        return HTTP_POST(Contact_Uri, Request_Json);
    }
    #endregion

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


    #endregion
}