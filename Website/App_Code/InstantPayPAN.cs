using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for InstantPayPAN
/// </summary>
public class InstantPayPAN
{
    private string M_Uri = "https://www.instantpay.in/ws/";
    private string token = "1dee003c74483599e07f2b9df8ca0e79";

    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            //httpWebRequest.Timeout = 100000;
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

    #region HTTP_GET
    public static string HTTP_GET(string url)
    {
        string Out = String.Empty;
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        httpreq.Method = "GET";
        httpreq.Timeout = 100000;
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return Out = results;
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }
    #endregion

    #region Request_Type
    #region Verify_Outlet
    public string Verify_Outlet(string mobile)
    {
        string Json = "{\"token\": \"" + token + "\",\"request\": {\"mobile\": \"" + mobile + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "outlet/sendOTP", Request_Json);
    }
    #endregion

    #region Register_Outlet
    public string Register_Outlet(string mobile, string otp, string email, string store_type, string company, string name, string address, string pincode)
    {
        string Json = "{\"token\":\"" + token + "\",\"request\":{\"mobile\":\"" + mobile + "\",\"otp\":\"" + otp + "\",\"email\":\"" + email + "\",\"store_type\":\"" + store_type + "\",\"company\":\"" + company + "\",\"name\":\"" + name + "\",\"address\":\"" + address + "\",\"pincode\":\"" + pincode + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "outlet/register", Request_Json);
    }
    #endregion

    #region UpdatePan
    public string UpdatePan(string outletid, string pan_no)
    {
        string Json = "{\"token\":\"" + token + "\",\"request\":{\"outletid\":\"" + outletid + "\", \"pan_no\" : \"" + pan_no + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "outlet/updatePan", Request_Json);
    }
    #endregion

    #region Services
    public string Services(string outletid)
    {
        string Json = "{\"token\":\"" + token + "\",\"request\":{\"outletid\":\"" + outletid + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "utipan/psa_registration", Request_Json);
    }
    #endregion

    #region GetkycDoc
    public string GetKycDocument(string outletid, string pan)
    {
        string Json = "{\"token\":\""+token+"\", \"request\": {\"outletid\" : \""+outletid+"\", \"pan_no\" : \""+pan+"\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "outlet/requiredDocs", Request_Json);
    }
    #endregion

    #region UploadDoc
    public string UploadDoc(string outletid,string pan,string docid,string img,string filename)
    {
        string Json = "{\"token\":\"" + token + "\", \"request\": {\"outletid\" : \"" + outletid + "\", \"pan_no\" : \"" + pan + "\", \"document\": {\"id\" : \"" + docid + "\", \"base64\" : \"" + img + "\", \"filename\" : \"as."+filename+"\"}}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "outlet/uploadDocs", Request_Json);
    }
    #endregion

    #region PurchaseToken
    public string PurchaseToken(string TranId, string amount, string outletid,string mobile)
    {
        string Result = string.Empty;
        string Uri = "https://www.instantpay.in/ws/api/transaction?format=xml&token=" + token + "&spkey='PAN'&agentid=" + TranId + "&amount=" + amount + "&account=" + outletid + "&outletid=" + outletid + "&customermobile=" + mobile + "";
        return HTTP_GET(Uri);
    }
    #endregion
    #endregion
}