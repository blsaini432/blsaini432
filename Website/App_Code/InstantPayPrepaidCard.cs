using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for InstantPayDMR
/// </summary>
public class InstantPayPrepaidCard
{
    public InstantPayPrepaidCard()
    {
       
    }
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
        catch (ArgumentException ex)
        {
            Out = string.Format("HTTP_ERROR :: The second HttpWebRequest object has raised an Argument Exception as 'Connection' Property is set to 'Close' :: {0}", ex.Message);
        }
        catch (WebException ex)
        {
            Out = string.Format("HTTP_ERROR :: WebException raised! :: {0}", ex.Message);
        }
        catch (Exception ex)
        {
            Out = string.Format("HTTP_ERROR :: Exception raised! :: {0}", ex.Message);
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    #endregion

    #region Request_Type

    #region Customer Login
    public string Customer_Login(string mobile, string otp, string ip, string email,string pan)
    {
        string Json = "{\"request\": {\"mobile\": \"" + mobile + "\",\"otp\": \"" + otp + "\",\"email\": \"" + email + "\",\"pan\": \"" + pan + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Prepaidcardlogin?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&ip=" + ip + "", Request_Json);
    }
    #endregion

    #region UpdateKYC
    public string UpdateKYC(string ip, string customertoken, string custid, string kyc_id, string base64_aadhaar, string file_ext)
    {
        string Json = "{\"request\": {\"ip\": \"" + ip + "\",\"customer_token\": \"" + customertoken + "\",\"customer_id\": \"" + custid + "\",\"kyc_id\": \"" + kyc_id + "\",\"base64_aadhaar\": \"" + base64_aadhaar + "\",\"file_ext\": \"" + file_ext + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "PrepaidcardKYC?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region ActivateCard
    public string ActivateCard(string ip, string customertoken, string custid, string scn,string kitno)
    {
        string Json = "{\"ip\": \"" + ip + "\",\"customer_token\": \"" + customertoken + "\",\"customer_id\": \"" + custid + "\",\"request\": {\"scn\": \"" + scn + "\",\"kitno\": \"" + kitno + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Prepaidcardactivation?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region CardMiniStatement
    public string CardMiniStatement(string ip, string customertoken, string custid, string scn)
    {
        string Json = "{\"ip\": \"" + ip + "\",\"customer_token\": \"" + customertoken + "\",\"customer_id\": \"" + custid + "\",\"request\": {\"scn\": \"" + scn + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Prepaidcardstatement?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

   
    #region CardBalance
    public string CardBalance(string mobile, string scn)
    {
        string Json = "{\"request\":{\"mobile\":\"" + mobile + "\",\"scn\":\"" + scn + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "PrepaidCardBalance?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region CardTopUp
    public string CardTopUp(string mobile, string scn, string amount,string agentid)
    {
        string Json = "{\"request\": {\"mobile\": \"" + mobile + "\",\"scn\": \"" + scn + "\",\"amount\": \"" + amount + "\",\"agentid\": \"" + agentid + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "Prepaidcardtopup?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region SendOTP
    public string SendOTP(string ip, string mobile)
    {
        string Json = "{\"request\": {\"mobile\": \"" + mobile + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "PrepaidcardSendotp?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&ip=" + ip + "", Request_Json);
    }
    #endregion

   #endregion
}