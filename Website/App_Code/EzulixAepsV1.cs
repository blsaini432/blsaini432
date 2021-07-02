using System;
using System.IO;
using System.Net;

/// <summary>
/// Summary description for EzulixAepsV1
/// </summary>
public class EzulixAepsV1
{
    public string M_Uri = "http://api.ezulix.in/";
    public string SSOUri = "https://ezulix.in/";
    private static string MemberId = "EZ198646";
    private static string ApiKey = "1567CC1ACE";

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
        catch (WebException ex)
        {
            if (ex.Status == WebExceptionStatus.Timeout)
            {
                Out = "{\"RESP_CODE\": \"404\",\"RESP_MSG\":\"Transcation time out, Try Again\"}";
            }
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    #endregion

    #region Method
    #region SearchCustomer
    public string searchcustmer(string Agentd_id, string CUSTOMER_MOBILE)
    {
        string Json = "{\"Agentd_id\": \"" + Agentd_id + "\",\"CUSTOMER_MOBILE\":\"" + CUSTOMER_MOBILE + "\"}";
        Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "aeps/searchcustmer?MemberId=" + MemberId + "&ApiKey=" + ApiKey + "", Json);
    }
    #endregion

    #region CustmerRegistration
    public string custmerregistration(string Agentd_id, string CUSTOMER_MOBILE, string CustmerName)
    {
        string Json = "{\"Agentd_id\": \"" + Agentd_id + "\",\"CUSTOMER_MOBILE\":\"" + CUSTOMER_MOBILE + "\",\"CustmerName\":\"" + CustmerName + "\"}";
        Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "aeps/custmerregistration?MemberId=" + MemberId + "&ApiKey=" + ApiKey + "", Json);
    }
    #endregion

    #region GetRdData
    public string rddata(string Agentd_id, string CUSTOMER_MOBILE, string Device)
    {
        string Json = "{\"Agentd_id\": \"" + Agentd_id + "\",\"CUSTOMER_MOBILE\":\"" + CUSTOMER_MOBILE + "\",\"Device\":\"" + Device + "\"}";
        Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "aeps/getrd?MemberId=" + MemberId + "&ApiKey=" + ApiKey + "", Json);
    }
    #endregion

    #region GetBalanseInfo
    public string balanseinfo(string Agentd_id, string IIN, string AadharNumber, string isAgree, string mobileNumber, string BiometricData, string Amount)
    {
        String Json = "{\"Agentd_id\":\"" + Agentd_id + "\",\"IIN\":\"" + IIN + "\",\"AadharNumber\":\"" + AadharNumber + "\",\"isAgree\":\"" + isAgree + "\",\"mobileNumber\":\"" + mobileNumber + "\",\"BiometricData\":\"" + BiometricData + "\",\"Amount\":\"" + Amount + "\"}";
        return HTTP_POST(M_Uri + "aeps/balanceinfo?MemberId=" + MemberId + "&ApiKey=" + ApiKey + "", Json);
    }
    #endregion

    #region AccountWithdrawal
    public string accountwithdrawal(string Agentd_id, string IIN, string AadharNumber, string isAgree, string mobileNumber, string BiometricData, string Amount)
    {
        String Json = "{\"Agentd_id\":\"" + Agentd_id + "\",\"IIN\":\"" + IIN + "\",\"AadharNumber\":\"" + AadharNumber + "\",\"isAgree\":\"" + isAgree + "\",\"mobileNumber\":\"" + mobileNumber + "\",\"BiometricData\":\"" + BiometricData + "\",\"Amount\":\"" + Amount + "\"}";
        return HTTP_POST(M_Uri + "aeps/accountwithdrawal?MemberId=" + MemberId + "&ApiKey=" + ApiKey + "", Json);
    }
    #endregion

    #region SSORequest
    public string SSORequest(string amount, string mobileNo, string st, string memberid)
    {
        string Json = "{\"request\": {\"clientmemberid\": \"" + memberid + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(SSOUri + "sso?memberid=" + MemberId + "&apikey=" + ApiKey + "&amount=" + amount + "&mobileNo=" + mobileNo + "&st=" + st + "", Request_Json);
    }
    #endregion
    #endregion
}