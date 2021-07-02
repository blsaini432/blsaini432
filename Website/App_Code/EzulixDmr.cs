using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for EzulixDmr
/// </summary>
public class EzulixDmr
{
    public string M_Uri = "https://ezulix.in/";
    //public string M_Uri = "http://localhost:49530/Website/";
    private static string mm_token = "edad103d8c";
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

    #region Request_Type
    #region Remitter_Details
    public string Remitter_Details(string mobile)
    {
        string Json = "{\"request\": {\"mobile\": \"" + mobile + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "remitter_details?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Remitter_Registration
    public string Remitter_Registration(string mobile, string name, string surname, string pincode)
    {
        string Json = "{\"request\": {\"mobile\": \"" + mobile + "\",\"name\":\"" + name + "\",\"surname\":\"" + surname + "\",\"pincode\":\"" + pincode + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "remitter_registration?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region RemitterValidate
    public string Remitter_Validate(string remitterid, string mobile, string otp)
    {
        string Json = "{\"request\": {\"remitterid\": \"" + remitterid + "\",\"mobile\": \"" + mobile + "\",\"otp\": \"" + otp + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "remitter_validate?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Beneficiary_Registration
    public string Beneficiary_Registration(string remitter_id, string benificiary_name, string benificiary_mobile, string benificiary_ifsc, string benificiary_account_no)
    {
        string Json = "{\"request\": {\"remitterid\": \"" + remitter_id + "\",\"name\": \"" + benificiary_name + "\",\"mobile\": \"" + benificiary_mobile + "\",\"ifsc\": \"" + benificiary_ifsc + "\",\"account\": \"" + benificiary_account_no + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "beneficiary_register?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Beneficiary_Registration_Resend_OTP
    public string Beneficiary_Registration_Resend_OTP(string remitter_id, string beneficiary_id)
    {
        string Json = "{\"request\": {\"remitterid\": \"" + remitter_id + "\",\"beneficiaryid\": \"" + beneficiary_id + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "beneficiary_resend_otp?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Beneficiary_Registration_Validate
    public string Beneficiary_Registration_Validate(string remitter_id, string beneficiary_id, string otp)
    {
        string Json = "{\"request\": {\"remitterid\": \"" + remitter_id + "\",\"beneficiaryid\": \"" + beneficiary_id + "\",\"otp\": \"" + otp + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "beneficiary_register_validate?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Beneficiary_Account_Verification
    public string Beneficiary_Account_Verification(string mobile, string benificiary_account_no, string benificiary_ifsc, string agent_id)
    {
        string Json = "{\"request\":{\"remittermobile\":\"" + mobile + "\",\"account\":\"" + benificiary_account_no + "\",\"ifsc\":\"" + benificiary_ifsc + "\",\"agentid\":\"" + agent_id + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "account_validate?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Beneficiary_Delete
    public string Beneficiary_Delete(string beneficiary_id, string remitter_id)
    {
        string Json = "{\"request\": {\"beneficiaryid\": \"" + beneficiary_id + "\",\"remitterid\": \"" + remitter_id + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "beneficiary_remove?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Beneficiary_Delete_Validate
    public string Beneficiary_Delete_Validate(string beneficiary_id, string remitter_id, string otp)
    {
        string Json = "{\"request\": {\"beneficiaryid\": \"" + beneficiary_id + "\",\"remitterid\": \"" + remitter_id + "\",\"otp\": \"" + otp + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "beneficiary_remove_validate?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Fund_Transfer
    public string Fund_Transfer(string mobile, string beneficiaryid, string agentid, decimal amount, string mode, string tramemberid, string agenttraid, string beniname, string bankname, string beniac, string bundleid)
    {

  
        string Json = "{\"request\": {\"remittermobile\": \"" + mobile + "\",\"beneficiaryid\": \"" + beneficiaryid + "\",\"agentid\": \"" + agentid + "\",\"amount\": \"" + amount + "\",\"mode\": \"" + mode + "\",\"tramemberid\": \"" + tramemberid + "\",\"agenttraid\": \"" + agenttraid + "\",\"beniname\": \"" + beniname + "\",\"bankname\": \"" + bankname + "\",\"beniac\": \"" + beniac + "\",\"bundleid\": \"" + bundleid + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "transfer?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Fund_Transfer_Status
    public string Fund_Transfer_Status(string ipayid)
    {
        string Json = "{\"request\": {\"ipayid\": \"" + ipayid + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "transfer_status?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Get_Bank_Details
    public string Get_Bank_Details()
    {
        //string Json = "{\"request\": {\"account\": \"" + acno + "\"}";
        //string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "bank_details?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", "");
    }
    #endregion

    #region TraCheck
    public string TraCheck(string agentmemberid, decimal amount, string mode, string agenttraid)
    {
        string Json = "{\"request\": {\"adminmemberid\": \"" + mm_api_memberid + "\",\"agentmemberid\": \"" + agentmemberid + "\",\"amount\": \"" + amount + "\",\"mode\": \"" + mode + "\",\"agenttraid\": \"" + agenttraid + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "trachk?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
}
    #endregion
    #endregion