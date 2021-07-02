using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for AEPS_Wallet
/// </summary>
public class AEPS_Wallet
{
    public string M_Uri = "https://ezulix.in/";
    //public string M_Uri = "http://localhost:49530/Website/";
    private static string mm_token = "1567CC1ACE";
    private static string mm_api_memberid = "EZ198646";

    #region HTTP_POST
    public static string HTTP_POST(string Url, string rtype, string Data)
    {
        string Out = String.Empty;
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("memberid", mm_api_memberid);
            httpWebRequest.Headers.Add("apikey", mm_token);
            httpWebRequest.Headers.Add("rtype", rtype);
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
        catch (WebException e)
        {
            return e.ToString();
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    #endregion

    #region Method
    #region Ewallet
    public string AepsWallet_EWallet(string memberid, decimal amount)
    {
        string Json = "{\"request\": {\"memberid\": \"" + memberid + "\",\"amount\": \"" + amount + "\"}";
        string reqjson = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "aepswallet", "ewallet", reqjson);

    }

    public string AepsWallet_Bank(string memberid, string bank, string ifsc, string ac, decimal amount, string orderid)
    {
        string Json = "{\"request\": {\"memberid\": \"" + memberid + "\",\"bank\": \"" + bank + "\",\"ifsc\": \"" + ifsc + "\",\"ac\": \"" + ac + "\",\"amount\": \"" + amount + "\",\"orderid\": \"" + orderid + "\"}";
        string reqjson = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "aepswallet", "bank", reqjson);
    }
    #endregion
    #endregion
}

