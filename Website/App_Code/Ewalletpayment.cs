using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for Ewalletpayment
/// </summary>
public class Ewalletpayment
{
    private static string M_Uri = "http://203.153.46.10:8080/panonlineservices/paymentProcess.jsp";
    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
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
    #region ewalletpayment
    public string payment(string Status, string transID,  string application, string transid,string amount)
    {
        string parameters = "transID=" + transID + "&transStatus=" + Status + "&applicationNo=" + application + "&UTITSLTransID=" + transid + "&transAmt=" + amount;
        string url = "http://203.153.46.10:8080/panonlineservices/paymentProcess.jsp" + "?" + parameters;

        return HTTP_POST(M_Uri, parameters);
       
    }
    #endregion
}