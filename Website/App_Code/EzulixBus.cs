using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for EzulixDmr
/// </summary>
public class EzulixBus
{
    public string M_Uri = "https://ezulix.in/";
    //public string M_Uri = "http://localhost:51686/Website/";
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


    #region Search_Buses
    public string Search_Buses(string source, string destination, string doj)
    {
        string Json = "{\"request\": {\"source\" : \"" + source + "\", \"destination\" : \"" + destination + "\", \"doj\" : \"" + doj + "\"}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "search_bus?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion



    #region Seat_Block
    public string Seat_Block(string source, string destination, string doj, string tripid, string bpid, string dpid, string mobile, string email, string idtype, string idnumber, string address, object returnjson)
    {
        string Json = "{\"request\": {\"source\" : \"" + source + "\",\"destination\" : \"" + destination + "\",\"doj\" : \"" + doj + "\",\"tripid\" : \"" + tripid + "\",\"bpid\" : \"" + bpid + "\",\"dpid\" : \"" + dpid + "\",\"mobile\" : \"" + mobile + "\",\"email\" : \"" + email + "\",\"idtype\" : \"" + idtype + "\",\"idnumber\" : \"" + idnumber + "\",\"address\" : \"" + address + "\", \"seats\" :" + returnjson + "}}"; 
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "seat_block?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion


    #region Book_Ticket
    public string Book_Ticket(string booking_id, string agentid, string traamount, string tramemberid)
    {
        string Json = "{\"request\": {\"booking_id\" : \"" + booking_id + "\",\"agentid\" : \"" + agentid + "\" , \"traamount\" : \"" + traamount + "\", \"tramemberid\" : \"" + tramemberid + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "bus_payment?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion


    #region Get_Ticket
    public string Get_Ticket(string booking_id)
    {
        string Json = "{\"request\": {\"booking_id\" : \"" + booking_id + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "get_ticket?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Cancel_Ticket
    public string Cancel_Ticket(string booking_id, string seats_to_cancel)
    {
        string Json = "{\"request\": {\"booking_id\" : \"" + booking_id + "\",\"seats_to_cancel\" : \"" + seats_to_cancel + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "cancelticket?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion


    #region Seat_Layout
    public string Seat_Layout(string tripid)
    {
        string Json = "{\"request\": {\"tripid\" : \"" + tripid + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "seat_layout?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

    #region Reconfirm_fare
    public string Reconfirm_fare(string booking_id)
    {
        string Json = "{\"request\": {\"booking_id\" : \"" + booking_id + "\"}}";
        string Request_Json = Json.Replace(@"\", "");
        return HTTP_POST(M_Uri + "reconfirm_fare?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
    }
    #endregion

}
