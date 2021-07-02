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
public class PayOut
{

    private static string M_Uri = "https://v2-api.bankopen.co/v1/payouts";
    private static string secret = "cc96c7124a4007fb1d17d9355bcd6882b5c27aae";
    private static string api_key = "b90906b0-77ea-11ea-ab7c-57eae7d6f9c3";
    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data, string signature, string Timestamp)
    {
        string Out = String.Empty;
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
          //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("X-O-Timestamp", Timestamp);
          
            httpWebRequest.Headers.Add("Authorization", "Bearer" + " " + api_key + ":" + signature);
            httpWebRequest.Timeout = 100000;
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


    #region WithDrawalAmount
    public string InitiatePayouts(string AgentOrderId, string Amount, string BeneficiaryAccount, string BeneficiaryIFSC, string mobile, string mer, string email, string name, string debitaccountnub, string purpose, int timestamp)
    {
        string Json = "{\"bene_account_number\":\"" + BeneficiaryAccount + "\",\"ifsc_code\":\"" + BeneficiaryIFSC + "\",\"recepient_name\":\"" + name + "\",\"email_id\":\"" + email + "\",\"mobile_number\":\"" + mobile + "\",\"debit_account_number\":\"" + debitaccountnub + "\",\"amount\":\"" + Amount + "\",\"transaction_types_id\":\"" + AgentOrderId + "\",\"merchant_ref_id\":\"" + mer + "\",\"purpose\":\"" + purpose + "\"}";
        string method = "POST";
        string Timestamp = timestamp.ToString();
        string Text = timestamp + method + Json;
        string Request_Jsonold = Json.Replace(@"\", "");
        string Request_Json = Text.Replace(@"\", "");
        string signature = ComputeHash(Request_Json, secret);
        return HTTP_POST(M_Uri, Request_Jsonold, signature, Timestamp);
    }
    #endregion


    //#region payonclick
    //public string payonclick(string name, string password, string agentid)
    //{
    //    string Timestamp = "455";
    //    string Json = "{\"username\": \"" + name + "\",\"password\":\"" + password + "\",\"agentId \":\"" + agentid + "\"}";
    //    string Request_Json = Json.Replace(@"\", "");
    //    return HTTP_POST(M_Uri + "payouts/otp", Request_Json, signature, Timestamp);
    //}
    //#endregion
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