using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for InstantPrepaidCard
/// </summary>
public class InstantPrepaidCard
{
    public InstantPrepaidCard()
    {
       
    }
    public string M_Uri = "https://ezulix.in/";
    //public string M_Uri = "http://localhost:51686/";
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

    #region genrateotp
    public string genrateotp(string mobile_number, string email_address)
    {
        string Json = "{\"mobile_number\" : \"" + mobile_number + "\", \"email_address\" : \"" + email_address + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "ezulixPrepaidcardSendotp?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region accountcreate
    public string accountcreate(string mobile_number, string email_address, string mobile_otp, string email_otp, string pan, string card_type, string scn, string kit_number, string geo_lat, string geo_long)
    {
        string Json = "{\"mobile_number\" : \"" + mobile_number + "\", \"email_address\" : \"" + email_address + "\",\"mobile_otp\" : \"" + mobile_otp + "\",\"email_otp\" : \"" + email_otp + "\",\"pan\" : \"" + pan + "\",\"card_type\" : \"" + card_type + "\",\"scn\" : \"" + scn + "\",\"kit_number\" : \"" + kit_number + "\",\"geo_lat\" : \"" + geo_lat + "\",\"geo_long\" : \"" + geo_long + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "ezulixPrepaidcardaccount?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }




    #endregion
    #region uploadkyc
    public string uploadkyc(string mobile_number, string pan, string link, string filename)
    {
        string Json = "{\"mobile_number\" : \"" + mobile_number + "\", \"pan\" : \"" + pan + "\",\"link\" : \"" + link + "\",\"filename\" : \"" + filename + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "ezulixPrepaidcardKYC?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region accountstatus
    public string accountstatus(string mobile_number, string pan)
    {
        string Json = "{\"mobile\" : \"" + mobile_number + "\", \"pan\" : \"" + pan + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "ezulixPrepaidCardAccountfetch?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion


    #region accountbalance
    public string accountbalance(string mobile_number, string pan)
    {
        string Json = "{\"mobile\" : \"" + mobile_number + "\", \"pan\" : \"" + pan + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "ezulixPrepaidCardBalance?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion


    #region accounttopup
    public string accounttopup(string mobile_number, string pan,string agentid,string amount,string scn)
    {
        string Json = "{\"mobile_number\" : \"" + mobile_number + "\", \"pan\" : \"" + pan + "\",\"agentid\" : \"" + agentid + "\",\"amount\" : \"" + amount + "\",\"scn\" : \"" + scn + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "ezulixPrepaidcardtopup?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

   #endregion
}