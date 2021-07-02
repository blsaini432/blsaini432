using System;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for EzulixDmrPayOut
/// </summary>
public class EzulixDmrPayOut
{
    private static string M_Uri = "https://api.ezulix.in/";
    private static string ApiKey = "edad103d8c";
    private static string MemberId = "EZ198646";
    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data, string ChkSum)
    {
        string Out = String.Empty;
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            httpWebRequest.Headers.Add("MemberId", MemberId);
            httpWebRequest.Headers.Add("ChkSum", ChkSum);
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

    #region Method
    #region WithDrawalAmmount
    public string WithDrawal_Ammount(string AgentOrderId, string Amount, string BeneficiaryAccount, string BeneficiaryIFSC, string Date)
    {
        string Json = "{\"AgentOrderId\": \"" + AgentOrderId + "\",\"Amount\":\"" + Amount + "\",\"BeneficiaryAccount\":\"" + BeneficiaryAccount + "\",\"BeneficiaryIFSC\":\"" + BeneficiaryIFSC + "\",\"Date\":\"" + Date + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        string Text = MemberId + "|" + Amount + "|" + AgentOrderId;
        string Result = ChkSum.HmacSha512Digest_CheckSum(Text, ApiKey);
        return HTTP_POST(M_Uri + "payout/dmr/txn/bank", Request_Json, Result);
    }
    #endregion

    #region CheckStaus
    public string Check_Staus(string AgentOrderId, string Amount)
    {
        string Json = "{\"AgentOrderId\": \"" + AgentOrderId + "\",\"Amount\":\"" + Amount + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        string Text = MemberId + "|" + Amount + "|" + AgentOrderId;
        string Result = ChkSum.HmacSha512Digest_CheckSum(Text, ApiKey);
        return HTTP_POST(M_Uri + "payout/status", Request_Json, Result);
    }
    #endregion
    #endregion
}