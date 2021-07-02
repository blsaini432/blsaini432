using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for EzulixBbps
/// </summary>
public class EzulixBbps
{
    public string M_Uri = "https://ezulix.in/";
    //public string M_Uri = "http://localhost:49530/Website/";

    private static string mm_token = "067CBB9B47";
    private static string mm_api_memberid = "EZ198646";
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
            throw;
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    #endregion

    #region Request_Type
    #region FetchEle_Details
    public string Fetch_Ele_Bill(string agentid, string servicekey, string account)
    {
        string Json = "{\"request\": {\"agentid\": \"" + agentid + "\",\"servicekey\":\"" + servicekey + "\",\"account\":\"" + account + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Api/Bbps/Bbps.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&type=ele", Request_Json);
    }
    #endregion
    #region Pay_Ele_Details
    public string Pay_Ele_Bill(string agentid, string servicekey, string account, decimal amount)
    {
        string Json = "{\"request\": {\"agentid\": \"" + agentid + "\",\"servicekey\":\"" + servicekey + "\",\"account\":\"" + account + "\",\"ammount\":\"" + amount + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Api/Bbps/Bbps.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&type=elepay", Request_Json);
    }
    #endregion
    #region FetchEle_Details_jharkhand
    public string Fetch_Ele_Bill_jharkhand(string agentid, string servicekey, string account, string subdivcode)
    {
        string Json = "{\"request\": {\"agentid\": \"" + agentid + "\",\"servicekey\":\"" + servicekey + "\",\"account\":\"" + account + "\",\"subdivcode\":\"" + subdivcode + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Api/Bbps/Bbps.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&type=ele", Request_Json);
    }
    #endregion
    #region Pay_Ele_Details_jharkhand
    public string Pay_Ele_Bill_jharkhand(string agentid, string servicekey, string account, decimal amount, string subdivcode)
    {
        string Json = "{\"request\": {\"agentid\": \"" + agentid + "\",\"servicekey\":\"" + servicekey + "\",\"account\":\"" + account + "\",\"ammount\":\"" + amount + "\",\"subdivcode\":\"" + subdivcode + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Api/Bbps/Bbps.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&type=elepay", Request_Json);
    }
    #endregion
    #region FetchEle_Details_Maharshtra
    public string Fetch_Ele_Bill_Maharshtra(string agentid, string servicekey, string account, string billingunit)
    {
        string Json = "{\"request\": {\"agentid\": \"" + agentid + "\",\"servicekey\":\"" + servicekey + "\",\"account\":\"" + account + "\",\"billingunit\":\"" + billingunit + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Api/Bbps/Bbps.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&type=ele", Request_Json);
    }
    #endregion
    #region Pay_Ele_Details_Maharshtra
    public string Pay_Ele_Bill_Maharshtra(string agentid, string servicekey, string account, decimal amount, string billingunit)
    {
        string Json = "{\"request\": {\"agentid\": \"" + agentid + "\",\"servicekey\":\"" + servicekey + "\",\"account\":\"" + account + "\",\"ammount\":\"" + amount + "\",\"billingunit\":\"" + billingunit + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Api/Bbps/Bbps.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&type=elepay", Request_Json);
    }
    #endregion
    #endregion
}
