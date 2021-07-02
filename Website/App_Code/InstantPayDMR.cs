using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for InstantPayDMR
/// </summary>
public class InstantPayDMR
{
    public InstantPayDMR()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string M_Uri = "https://www.instantpay.in/ws/";
    public string token = "4976e74bf26153f1e6b4a1992aa87344";

    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            //string username = "test";
            //string password = "test";
            //string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            //httpWebRequest.Headers.Add("Authorization", "Basic " + svcCredentials);
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
    #region Remitter_Details
    public string Remitter_Details(string mobile)
    {
        string Json = "{\"token\": \"" + token + "\",\"request\": {\"mobile\": \"" + mobile + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/remitter_details", Request_Json);
    }
    #endregion

    #region Remitter_Registration
    public string Remitter_Registration(string mobile, string name, string pincode)
    {
        string Json = "{\"token\":\"" + token + "\",\"request\": {\"mobile\": \"" + mobile + "\",\"name\":\"" + name + "\",\"pincode\":\"" + pincode + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/remitter", Request_Json);
    }
    #endregion

    #region Beneficiary_Registration
    public string Beneficiary_Registration(string remitter_id, string benificiary_name, string benificiary_mobile, string benificiary_ifsc, string benificiary_account_no)
    {
        string Json = "{\"token\": \"" + token + "\", \"request\": {\"remitterid\": \"" + remitter_id + "\",\"name\": \"" + benificiary_name + "\",\"mobile\": \"" + benificiary_mobile + "\",\"ifsc\": \"" + benificiary_ifsc + "\",\"account\": \"" + benificiary_account_no + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/beneficiary_register", Request_Json);
    }
    #endregion

    #region Beneficiary_Registration_Resend_OTP
    public string Beneficiary_Registration_Resend_OTP(string remitter_id, string beneficiary_id)
    {
        string Json = "{\"token\": \"" + token + "\", \"request\": {\"remitterid\": \"" + remitter_id + "\",\"beneficiaryid\": \"" + beneficiary_id + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/beneficiary_resend_otp", Request_Json);
    }
    #endregion

    #region Beneficiary_Registration_Validate
    public string Beneficiary_Registration_Validate(string remitter_id, string beneficiary_id, string otp)
    {
        string Json = "{\"token\": \"" + token + "\",\"request\": {\"remitterid\": \"" + remitter_id + "\",\"beneficiaryid\": \"" + beneficiary_id + "\",\"otp\": \"" + otp + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/beneficiary_register_validate", Request_Json);
    }
    #endregion

    #region Beneficiary_Account_Verification
    public string Beneficiary_Account_Verification(string mobile, string benificiary_account_no, string benificiary_ifsc, string agent_id)
    {
        string Json = "{\"token\":\"" + token + "\",\"request\":{\"remittermobile\":\"" + mobile + "\",\"account\":\"" + benificiary_account_no + "\",\"ifsc\":\"" + benificiary_ifsc + "\",\"agentid\":\"" + agent_id + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "imps/account_validate", Request_Json);
    }
    #endregion

    #region Beneficiary_Delete
    public string Beneficiary_Delete(string beneficiary_id, string remitter_id)
    {
        string Json = "{\"token\": \"{{" + token + "}}\",\"request\": {\"beneficiaryid\": \"{{" + beneficiary_id + "}}\",\"remitterid\": \"{{" + remitter_id + "}}\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/beneficiary_remove", Request_Json);
    }
    #endregion

    #region Beneficiary_Delete_Validate
    public string Beneficiary_Delete_Validate(string beneficiary_id, string remitter_id, string otp)
    {
        string Json = "{\"token\": \"token\",\"request\": {\"beneficiaryid\": \"" + beneficiary_id + "\",\"remitterid\": \"" + remitter_id + "\",\"otp\": \"" + otp + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/beneficiary_remove_validate", Request_Json);
    }
    #endregion

    #region Fund_Transfer
    public string Fund_Transfer(string mobile, string beneficiaryid, string agentid, decimal amount, string mode)
    {
        string Json = "{\"token\": \"" + token + "\",\"request\": {\"remittermobile\": \"" + mobile + "\",\"beneficiaryid\": \"" + beneficiaryid + "\",\"agentid\": \"" + agentid + "\",\"amount\": \"" + amount + "\",\"mode\": \"" + mode + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/transfer", Request_Json);
    }
    #endregion

    #region Fund_Transfer_Status
    public string Fund_Transfer_Status(string ipayid)
    {
        string Json = "{\"token\": \"" + token + "\",\"request\": {\"ipayid\": \"" + ipayid + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/transfer_status", Request_Json);
    }
    #endregion

    #region Get_Bank_Details
    public string Get_Bank_Details(string acno)
    {
        string Json = "{\"token\": \"" + token + "\",\"request\": {\"account\": \"" + acno + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "dmi/bank_details", Request_Json);
    }
    #endregion
    #endregion
}