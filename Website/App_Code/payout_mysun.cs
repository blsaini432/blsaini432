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
public class payout_mysun
{
    cls_connection Cls = new cls_connection();
    public string M_Uri = "https://www.instantpay.in/ws/payouts/bank";
    public string token = "3eac6a6a775890e229348ebc2616a341";
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
            cls_connection Cls = new cls_connection();
            Cls.select_data_dt("insert into ErrorLog values('" + ex.ToString() + "')");
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

    public string InitiatePayouts(string sp_key, string debit_account, string external_ref, string credit_account, string ifs_code, string amount, string latitude, string longitude, string endpoint_ip, string bene_name, string remarks,int otp_auth)
    {
        string request = "{\"token\": \"" + token + "\",\"request\":{\"sp_key\":\"" + sp_key + "\",\"debit_account\":\"" + debit_account + "\",\"external_ref\":\"" + external_ref + "\",\"credit_account\":\"" + credit_account + "\",\"ifs_code\":\"" + ifs_code + "\",\"credit_amount\":\"" + amount + "\",\"latitude\":\"" + latitude + "\",\"longitude\":\"" + longitude + "\",\"endpoint_ip\":\"" + endpoint_ip + "\",\"bene_name\":\"" + bene_name + "\",\"remarks\":\"" + remarks + "\",\"otp_auth\":\"" + otp_auth + "\"}}";      
        string Request_Json = request.Replace(@"\", "");
        return HTTP_POST(M_Uri, Request_Json);
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