using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for EzulixPan
/// </summary>
public class EzulixPan
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

    public string PurchaseToken(string outletid, string mobile)
    {
        string Json = "{\"request\": {\"outletid\": \"" + outletid + "\",\"mobile\": \"" + mobile + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "tra?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion
}