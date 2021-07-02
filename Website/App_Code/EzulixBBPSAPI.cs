using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for EzulixDmr
/// </summary>
public class EzulixBBPSAPI
{
    public string M_Uri = "https://ezulix.in/";
    //public string M_Uri = "http://localhost:49530/Website/";
    private static string mm_token = "1567CC1ACE";
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
            if (e.Status == WebExceptionStatus.Timeout)
            {
                Out = "{\"statuscode\": \"ETO\",\"status\":\"Transcation is Pending\"}";
            }
            else throw;
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    #endregion
    #region Method
    #region fetch_bbps_biller
    public string bbps_biller(string sp_key)
    {
        string Json = "{\"request\": {\"sp_key\": \"" + sp_key + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "fetchbbpsbller?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region fetch_bbps_billdetails
    public string bill_fetch(string sp_key, string agentid, string customer_mobile, string init_channel, string endpoint_ip, string mac, string payment_mode, string payment_info, string amount, string reference_id, string latlong, string outletid, object returnjson)
    {
        string Json = "{\"request\": {\"sp_key\": \"" + sp_key + "\",\"agentid\": \"" + agentid + "\",\"customer_mobile\": \"" + customer_mobile + "\",\"customer_params\":[\"" + returnjson + "\",\"\"],\"init_channel\": \"" + init_channel + "\",\"endpoint_ip\": \"" + endpoint_ip + "\",\"mac\": \"" + mac + "\",\"payment_mode\": \"" + payment_mode + "\",\"payment_info\": \"" + payment_info + "\",\"amount\": \"" + amount + "\",\"reference_id\": \"" + reference_id + "\",\"latlong\": \"" + latlong + "\",\"outletid\": \"" + outletid + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "fetchbbpsbill?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region pay_bbps_bill
    public string bill_pay(string sp_key, string agentid, string customer_mobile, string init_channel, string endpoint_ip, string mac, string payment_mode, string payment_info, string amount, string reference_id, string latlong, string outletid, object returnjson)
    {
        string Json = "{\"request\": {\"sp_key\": \"" + sp_key + "\",\"agentid\": \"" + agentid + "\",\"customer_mobile\": \"" + customer_mobile + "\",\"customer_params\":[\"" + returnjson + "\",\"\"],\"init_channel\": \"" + init_channel + "\",\"endpoint_ip\": \"" + endpoint_ip + "\",\"mac\": \"" + mac + "\",\"payment_mode\": \"" + payment_mode + "\",\"payment_info\": \"" + payment_info + "\",\"amount\": \"" + amount + "\",\"reference_id\": \"" + reference_id + "\",\"latlong\": \"" + latlong + "\",\"outletid\": \"" + outletid + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "paybbpsbill?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion


    #region pay_bbps_bill
    public string checkadminbalahce(string amount)
    {
        string Json = "{\"request\": {\"amount\": \"" + amount + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "checkadminbalance?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region fetch_bbps_billdetails_Maharshtra
    public string bill_fetch_Maharshtra(string sp_key, string agentid, string customer_mobile, string init_channel, string endpoint_ip, string mac, string payment_mode, string payment_info, string amount, string reference_id, string latlong, string outletid, object returnjson, string billingunit)
    {
        string Json = "{\"request\": {\"sp_key\": \"" + sp_key + "\",\"agentid\": \"" + agentid + "\",\"customer_mobile\": \"" + customer_mobile + "\",\"customer_params\":[\"" + returnjson + "\",\"\"],\"init_channel\": \"" + init_channel + "\",\"endpoint_ip\": \"" + endpoint_ip + "\",\"mac\": \"" + mac + "\",\"payment_mode\": \"" + payment_mode + "\",\"payment_info\": \"" + payment_info + "\",\"amount\": \"" + amount + "\",\"reference_id\": \"" + reference_id + "\",\"latlong\": \"" + latlong + "\",\"outletid\": \"" + outletid + "\",\"billingunit\": \"" + billingunit + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "fetchbbpsbill?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region pay_bbps_bill_Maharshtra
    public string bill_pay_Maharshtra(string sp_key, string agentid, string customer_mobile, string init_channel, string endpoint_ip, string mac, string payment_mode, string payment_info, string amount, string reference_id, string latlong, string outletid, object returnjson, string billingunit)
    {
        string Json = "{\"request\": {\"sp_key\": \"" + sp_key + "\",\"agentid\": \"" + agentid + "\",\"customer_mobile\": \"" + customer_mobile + "\",\"customer_params\":[\"" + returnjson + "\",\"\"],\"init_channel\": \"" + init_channel + "\",\"endpoint_ip\": \"" + endpoint_ip + "\",\"mac\": \"" + mac + "\",\"payment_mode\": \"" + payment_mode + "\",\"payment_info\": \"" + payment_info + "\",\"amount\": \"" + amount + "\",\"reference_id\": \"" + reference_id + "\",\"latlong\": \"" + latlong + "\",\"outletid\": \"" + outletid + "\",\"billingunit\": \"" + billingunit + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "paybbpsbill?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region fetch_bbps_billdetails_Jha
    public string bill_fetch_Jha(string sp_key, string agentid, string customer_mobile, string init_channel, string endpoint_ip, string mac, string payment_mode, string payment_info, string amount, string reference_id, string latlong, string outletid, object returnjson, string subdivcode)
    {
        string Json = "{\"request\": {\"sp_key\": \"" + sp_key + "\",\"agentid\": \"" + agentid + "\",\"customer_mobile\": \"" + customer_mobile + "\",\"customer_params\":[\"" + returnjson + "\",\"\"],\"init_channel\": \"" + init_channel + "\",\"endpoint_ip\": \"" + endpoint_ip + "\",\"mac\": \"" + mac + "\",\"payment_mode\": \"" + payment_mode + "\",\"payment_info\": \"" + payment_info + "\",\"amount\": \"" + amount + "\",\"reference_id\": \"" + reference_id + "\",\"latlong\": \"" + latlong + "\",\"outletid\": \"" + outletid + "\",\"subdivcode\": \"" + subdivcode + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "fetchbbpsbill?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region pay_bbps_bill_Jha
    public string bill_pay_Jha(string sp_key, string agentid, string customer_mobile, string init_channel, string endpoint_ip, string mac, string payment_mode, string payment_info, string amount, string reference_id, string latlong, string outletid, object returnjson, string subdivcode)
    {
        string Json = "{\"request\": {\"sp_key\": \"" + sp_key + "\",\"agentid\": \"" + agentid + "\",\"customer_mobile\": \"" + customer_mobile + "\",\"customer_params\":[\"" + returnjson + "\",\"\"],\"init_channel\": \"" + init_channel + "\",\"endpoint_ip\": \"" + endpoint_ip + "\",\"mac\": \"" + mac + "\",\"payment_mode\": \"" + payment_mode + "\",\"payment_info\": \"" + payment_info + "\",\"amount\": \"" + amount + "\",\"reference_id\": \"" + reference_id + "\",\"latlong\": \"" + latlong + "\",\"outletid\": \"" + outletid + "\",\"subdivcode\": \"" + subdivcode + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "paybbpsbill?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion
    #endregion
}
