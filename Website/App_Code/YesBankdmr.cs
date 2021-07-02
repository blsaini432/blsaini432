using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for YesBankdmr
/// </summary>
public class YesBankdmr
{

    public string M_Uri = "https://yesmoney.yesbank.in/";
    //public string M_Uri = "http://localhost:49530/Website/";
    private static string mm_token = "83af3ace-f1cc-4ab7-b64e-969ebd0f95a9";
  


    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            httpWebRequest.Headers.Add("Authorization", "bearer" +""+ mm_token);
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
    #region HTTP_POST
    public static string HTTP_POSTtrans(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            var request = (HttpWebRequest)WebRequest.Create("https://yesmoney.yesbank.in/epMoney/cp/dmt/moneytransfer/v1.0");
            var postData = "jsonData=" + Uri.EscapeDataString(Data);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Authorization", "bearer" + "" + mm_token);
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
           
            using (var streamReader = new StreamReader(response.GetResponseStream()))
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


    #region SearchCustomer
    public string SearchCustomer(string mobile,string AID,string OP,string ST)
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\":\"" + mobile + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "epMoney/retvalcustomer/v1.0", Request_Json);
    }
    #endregion

    #region Customer  Verify
    public string Customerverify(string mobile, string AID, string OP, string ST,string request)
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\":\"" + mobile + "\",\"REQUEST_FOR\":\"" + request + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "/epMoney/generateotp/v1.0", Request_Json);
    }
    #endregion

    #region list
    public string listbenifical(string mobile, string AID, string OP, string ST)
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\":\"" + mobile + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "/epMoney/list-beneficiary/v1.0", Request_Json);
    }
    #endregion

    #region otpgenerateVerify 
    public string CustomerVerifyotp(string remitterid, string mobile, string otp, string AID, string OP, string ST, string request)
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\":\"" + mobile + "\",\"REQUEST_FOR\":\"" + remitterid + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "epMoney/generateotp/v1.0", Request_Json);
    }
    #endregion

    #region customer_otpverify
    public string cusotpverify(string AID, string OP, string ST, string remitterid, string mobile, string otp, string Responsecode, string request)
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\": \"" + mobile + "\",\"REQUEST_CODE\": \"" + Responsecode + "\",\"BENE_ID\": \"" + remitterid + "\",\"OTP\": \"" + otp + "\",\"REQUEST_FOR\": \"" + request + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "epMoney/verifyotp/v1.0?", Request_Json);
    }
    #endregion

    #region Customer_Registration
    public string Customer_Registration(string mobile, string name, string benfname, string account, string ifsc, string AID, string OP,string ST, string CUST_LNAME, string STATE,string CUST_ADDRESS,string  PINCODE,string  CUST_TITLE,string  CITY,string  CUST_EMAIL,string  CUST_ALTMOBILENO,string  BENE_MOBILENO,string  CUST_DOB)
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\":\"" + mobile + "\",\"CUST_LNAME\":\"" + CUST_LNAME + "\",\"STATE\":\"" + STATE + "\",\"BENE_NAME\":\"" + benfname + "\",\"CUST_ADDRESS\":\"" + CUST_ADDRESS + "\",\"PINCODE\":\"" + PINCODE + "\",\"BANK_ACCOUNTNO\":\"" + account + "\",\"CUST_TITLE\":\"" + CUST_TITLE + "\",\"CITY\":\"" + CITY + "\",\"CUST_EMAIL\":\"" + CUST_EMAIL + "\",\"CUST_FNAME\":\"" + name + "\",\"CUST_ALTMOBILENO\":\"" + CUST_ALTMOBILENO + "\",\"BANKIFSC_CODE\":\"" + ifsc + "\",\"BENE_MOBILENO\":\"" + BENE_MOBILENO + "\",\"CUST_DOB\":\"" + CUST_DOB + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "epMoney/add-customer/v1.0?", Request_Json);
    }
    #endregion

    #region Beneficiary_Registration
    public string Beneficiary_Registration(string AID, string OP, string ST, string benificiary_name, string benificiary_mobile, string benificiary_ifsc, string benificiary_account_no)
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"BENE_NAME\": \"" + benificiary_name + "\",\"CUSTOMER_MOBILE\": \"" + benificiary_mobile + "\",\"BANKIFSC_CODE\": \"" + benificiary_ifsc + "\",\"BANK_ACCOUNTNO\": \"" + benificiary_account_no + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "/epMoney/add-beneficiary/v1.0?", Request_Json);
    }
    #endregion


    #region Delete Beneficiary
    public string Beneficiarydelete(string OP, string ST, string AID, string mobile, string beni_id, string Responsecode,string  otp )
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\": \"" + mobile + "\",\"BENE_ID\": \"" + beni_id + "\",\"REQUEST_CODE\": \"" + Responsecode + "\",\"OTP\": \"" + otp + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "/epMoney/delete-beneficiary/v1.0?", Request_Json);
    }
    #endregion


    #region delete bebi otpgenerateVerify 
    public string deletebenigenerateotp( string OP, string ST , string AID, string mobile, string request)
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\":\"" + mobile + "\",\"REQUEST_FOR\":\"" + request + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "epMoney/generateotp/v1.0?", Request_Json);
    }
    #endregion

    #region Fund_Transfer
    public string Fund_Transfer(string OP, string ST, string AID, string mobile, string CN, string BENE_ID, string ORDER_ID, decimal TXN_AMOUNT, string KEY_KYC_STATUS, string TRANSFER_TYPE, string PAYABLE_AMOUNT, string BENE_MOBILENO, string beniname, string Bankifsc, string bankname, string beniacccount)
    {
        string Json = "{\"HEADER\": {\"OP\": \"" + OP + "\",\"ST\": \"" + ST + "\",\"PAYABLE_AMOUNT\": \"" + PAYABLE_AMOUNT + "\",\"TXN_AMOUNT\": \"" + TXN_AMOUNT + "\",\"AID\": \"" + AID + "\"},\"DATA\":{\"CUSTOMER_MOBILE\": \"" + mobile + "\",\"BENE_ID\": \"" + BENE_ID + "\",\"CN\": \"" + CN + "\",\"ORDER_ID\": \"" + ORDER_ID + "\",\"KEY_KYC_STATUS\": \"" + KEY_KYC_STATUS + "\",\"TRANSFER_TYPE\": \"" + TRANSFER_TYPE + "\",\"BENE_MOBILENO\": \"" + BENE_MOBILENO + "\",\"BENE_NAME\": \"" + beniname + "\",\"BANKIFSC_CODE\": \"" + Bankifsc + "\",\"BANK_ACCOUNTNO\": \"" + beniacccount + "\",\"BENE_BANKNAME\": \"" + bankname + "\"}}";
        string jsonData = Json.Replace(@"\", "");
        string jsonDataa = Json.Replace("}", "}}");
        return HTTP_POSTtrans(M_Uri + "epMoney/cp/dmt/moneytransfer/v1.0", jsonData);
    }
    #endregion


    #region checkstatus
    public string checkstatus(string AID, string OP, string ST, string mobile, string id)
    {
        string Json = "{\"OP\": \"" + OP + "\",\"ST\":\"" + ST + "\",\"AID\":\"" + AID + "\",\"CUSTOMER_MOBILE\":\"" + mobile + "\",\"REQUEST_REFERENCE_NO\":\"" + id + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "epMoney/transaction-status/v1.0", Request_Json);
    }
    #endregion
}