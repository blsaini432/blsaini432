using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;
using System.Web.Script.Serialization;
using System.Web;

public class EzulixAir
{
    // live
     public string M_Uri = "https://ezulix.in/api/Flight/";
    private static string mm_token = "B6BB15C6FF";
    private static string mm_api_memberid = "EZ173888";

    // local
    // public string M_Uri = "http://localhost:49530/api/Flight/";
    //private static string mm_token = "89E513E538";
    //private static string mm_api_memberid = "EZ479539";



    ////local
    //private static readonly string ClientId = "ApiIntegrationNew";
    //private static readonly string UserName = "ezulix12";
    //private static readonly string Password = "ezulix@1234";
    //private static readonly string EndUserIp = "106.0.56.71";

    // live
    private static readonly string ClientId = "tboprod";
    private static readonly string UserName = "JAIE885";
    private static readonly string Password = "tbolive-885@#$";
    private static readonly string EndUserIp = "172.107.166.241";

    public static string Air_TokenAgencyId, Air_TokenMemberId, Air_TokenId, Air_memberid, Air_apikey, Air_agentid, Air_agencynetpay, Air_PublishFee, Api_publishfee, Api_agencynetpay, TicketStatus, BookStatus, amountstatus,FlightStatus, RequestMsrNo, EndUserIpp = EndUserIp, FareQuotee, SourceNumber, GetBookingDetail;

    
    #region WebServiceConsumeMethod
    public static string HTTP_POSTTokenId(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            cls_connection objconnection = new cls_connection();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 10000000;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
                EzulixAir eAir = new EzulixAir();
                DataSet ds = eAir.Deserialize(Out.ToString());
                if (ds.Tables.Contains("root"))
                {
                    string status = ds.Tables["root"].Rows[0]["statuscode"].ToString();
                    if (status == "true")
                    {
                        Air_TokenId = ds.Tables["root"].Rows[0]["status"].ToString();
                        Out = "true";
                    }
                    else
                    {
                        Out = ds.Tables["root"].Rows[0]["status"].ToString();
                    }
                }
                else
                {
                    Out = Out.ToString();
                }
            }
        }
        catch (WebException e)
        {
            if (e.Status == WebExceptionStatus.Timeout)
            {
                Out = Out.ToString();
            }
            else throw;
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    public static string HTTP_POST(string Url, object Data)
    {
       string Out = String.Empty;
        try
        {
            cls_connection objconnection = new cls_connection();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 10000000;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
                EzulixAir eAir = new EzulixAir();
                DataSet ds = eAir.Deserialize(Out.ToString());
                var BookingId = "";
                var TicketId = "";
                var PNR = "";
                var TraceId = "";
                if (ds.Tables.Contains("Response"))
                {
                    for (int i = 0; i < ds.Tables["Response"].Columns.Count; i++)
                    {
                        if (ds.Tables["Response"].Columns[i].ColumnName == "TraceId")
                        {
                            TraceId = ds.Tables["Response"].Rows[0]["TraceId"].ToString();
                        }
                       
                    }
                }
                if (BookStatus != "yes" && TicketStatus != "yes" && GetBookingDetail != "yes")
                {
                    DataSet dss = eAir.Deserialize(Data.ToString());
                    if (dss.Tables.Contains("root"))
                    {
                        for (int i = 0; i < dss.Tables["root"].Columns.Count; i++)
                        {
                            if (dss.Tables["root"].Columns[i].ColumnName == "EndUserIp")
                            {
                                EndUserIpp = dss.Tables["root"].Rows[0]["EndUserIp"].ToString();
                            }
                        }
                    }
                }
                if (FareQuotee == "yes")
                {
                    if (ds.Tables.Contains("Results"))
                    {
                        for (int i = 0; i < ds.Tables["Results"].Columns.Count; i++)
                        {
                            if (ds.Tables["Results"].Columns[i].ColumnName == "Source")
                            {
                                SourceNumber = ds.Tables["Results"].Rows[0]["Source"].ToString();
                            }
                        }
                    }
                }
                if (BookStatus == "yes")
                {
                    if (ds.Tables.Contains("FlightItinerary"))
                    {
                        for (int i = 0; i < ds.Tables["FlightItinerary"].Columns.Count; i++)
                        {
                            if (ds.Tables["FlightItinerary"].Columns[i].ColumnName == "BookingId")
                            {
                                BookingId = ds.Tables["FlightItinerary"].Rows[0]["BookingId"].ToString();
                                amountstatus = "Booked";
                            }
                        }
                    }
                }
                if (TicketStatus == "yes")
                {
                    if (ds.Tables.Contains("Response"))
                    {
                        for (int i = 0; i < ds.Tables["Response"].Columns.Count; i++)
                        {
                            if (ds.Tables["Response"].Columns[i].ColumnName == "PNR")
                            {
                                PNR = ds.Tables["Response"].Rows[1]["PNR"].ToString();
                            }
                            if (ds.Tables["Response"].Columns[i].ColumnName == "BookingId")
                            {
                                BookingId = ds.Tables["Response"].Rows[1]["BookingId"].ToString();
                                objconnection.update_data("update tbl_Flight_Api_Response set amountstatus=" + "'Confirmed'" + " where BookingId='" + BookingId + "'");
                            }
                        }
                    }
                    if (ds.Tables.Contains("Invoice"))
                    {
                        for (int i = 0; i < ds.Tables["Invoice"].Columns.Count; i++)
                        {
                            if (ds.Tables["Invoice"].Columns[i].ColumnName == "InvoiceAmount")
                            {
                                Api_agencynetpay = ds.Tables["Invoice"].Rows[0]["InvoiceAmount"].ToString();
                            }
                        }
                    }
                    if (ds.Tables.Contains("Fare"))
                    {
                        for (int i = 0; i < ds.Tables["Fare"].Columns.Count; i++)
                        {
                            if (ds.Tables["Fare"].Columns[i].ColumnName == "PublishedFare")
                            {
                                Api_publishfee = ds.Tables["Fare"].Rows[0]["PublishedFare"].ToString();
                                amountstatus = "Confirmed";
                                FlightStatus = "Confirmed";
                            }

                        }
                    }

                    if (ds.Tables.Contains("Ticket"))
                    {
                        for (int j = 0; j < ds.Tables["Ticket"].Rows.Count; j++)
                        {
                            for (int i = 0; i < ds.Tables["Ticket"].Columns.Count; i++)
                            {
                                if (ds.Tables["Ticket"].Columns[i].ColumnName == "TicketId")
                                {
                                    if (TicketId != null && TicketId != "")
                                    {
                                        TicketId = TicketId + "," + "" + ds.Tables["Ticket"].Rows[j]["TicketId"].ToString() + "";
                                    }
                                    else
                                    {
                                        TicketId = ds.Tables["Ticket"].Rows[j]["TicketId"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                string dsss = "No";
                string err = "Empty response";
                if (ds.Tables.Count > 0)
                {
                    dsss = "Yes";
                }
                if (ds.Tables.Contains("Error"))
                {
                    err = " + " + ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "  + ";
                    err = err.Replace("'", "");
                }
                else if (ds.Tables.Contains("TicketCRInfo"))
                {
                    dsss = ds.Tables["TicketCRInfo"].Rows[0]["Remarks"].ToString();
                    err = "Check Response";
                }
                objconnection.insert_data("insert into tbl_Flight_Api_Response (api, request, response, TicketId , TraceId, PNR ,BookingId ,errormessage,addedon,Air_TokenId, Air_memberid, Air_apikey, Air_agentid, Air_agencynetpay, Air_PublishFee, Api_publishfee, Api_agencynetpay,amountstatus, MemberMsrNo, EndUserIp,SourceNumber,FlightStatus) values ('" + Url + "','" + Data + "','" + dsss
                + "','" + TicketId
                + "','" + TraceId
                + "','" + PNR
                + "','" + BookingId
                + "','" + err + "', '" + DateTime.Now + "','" + Air_TokenId + "','" + Air_memberid + "','" + Air_apikey + "','" + Air_agentid + "','" + Air_agencynetpay + "','" + Air_PublishFee + "','" + Api_publishfee + "','" + Api_agencynetpay + "' ,'" + amountstatus + "' ,'" + RequestMsrNo + "','" + EndUserIpp + "','" + SourceNumber + "','" + FlightStatus + "')");

                Air_memberid = "";
                Air_apikey = "";
                Air_agentid = "";
                Air_agencynetpay = "";
                Air_PublishFee = "";
                Api_publishfee = "";
                Api_agencynetpay = "";
                amountstatus = "";
                RequestMsrNo = "";
                TicketId = "";
                TicketStatus = "";
                BookStatus = "";
                FareQuotee = "";
                GetBookingDetail = "";
            }
        }
        catch (WebException err)
        {
            if (err.Status == WebExceptionStatus.Timeout)
            {
                Out = err.Status.ToString();
            }
            else
            {
                Out = err.Message.ToString();

            };
        }
        string myresponse = Out.ToString();
        return myresponse;
    }

    #endregion

    #region deserialise method
    public DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        try
        {
            ds.Clear();
            XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
            StringReader theReader = new StringReader(doc.InnerXml.ToString());
            ds.ReadXml(theReader);
            return ds;
        }
        catch (Exception err)
        {
            return ds;
        }
    }


    #endregion

    #region GetTokenId/Authenticate
    public string GetTokenId()
    {
        string response = "";
        try
        {
            string res = HTTP_POSTTokenId(M_Uri + "GetTokenId.aspx", "");
            if (res != "true" && res != "")
            {
                return res;
            }
            if (Air_TokenId != null || Air_TokenId != "")
            {
                response = "true";
            }
            else
            {
                Air_TokenId = FlightAuthenticate(ClientId, UserName, Password, EndUserIp);
                cls_connection objconnection = new cls_connection();
                objconnection.insert_data("insert into t_TBO_token (Ip,TokenId,Date) values ('" + "" + "','" + Air_TokenId + "','" + DateTime.Now + "')");
                response = "true";
            }
            return response;
        }
        catch (Exception err)
        {
            response = err.Message.ToString();
            return response;
        }

    }

    public string FlightAuthenticate(string clientId, string userName, string password, string endUserIp)
    {
        try
        {
            string Json = "{\"ClientId\": \"" + ClientId + "\",\"UserName\":\"" + userName + "\",\"Password\":\"" + Password + "\",\"EndUserIp\":\"" + endUserIp + "\"}";
            string Request_Json = Json.Replace(@"\", "");
            return HTTP_POST(M_Uri + "FlightAuthenticate.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Request_Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }
    #endregion

    #region Method

    public string FlightSearch(int adult, int child, int infant, int journeytype, string origin, string destination, int cabinClass, string dateDeparture, string dateArrival, bool IsDirect, bool IsDomestic, bool IsOneStop)
    {
        try
        {
            // getting tokenid from database
            var response = GetTokenId();
            if (Air_TokenId == null || Air_TokenId == "")
            {
                if (response != "true" && response != "")
                {
                    return response;
                }
            }
            string Json = string.Empty;
            if (journeytype == 1)
            {
                Json = "{\"EndUserIp\": \"" + EndUserIp + "\",\"TokenId\":\"" + Air_TokenId + "\",\"AdultCount\":\"" + adult + "\",\"ChildCount\":\"" + child + "\",\"InfantCount\":\"" + infant + "\",\"JourneyType\":\"" + journeytype + "\",\"DirectFlight\":\"" + IsDirect.ToString().ToLower() + "\",\"OneStopFlight\":\"" + IsOneStop.ToString().ToLower() + "\", \"Segments\":[{\"Origin\":\"" + origin + "\",\"Destination\":\"" + destination + "\",\"FlightCabinClass\":\"" + cabinClass + "\",\"PreferredDepartureTime\":\"" + dateDeparture + "\",\"PreferredArrivalTime\":\"" + dateDeparture + "\"}]}";
            }
            else if (journeytype == 2)
            {
                Json = "{\"EndUserIp\": \"" + EndUserIp + "\",\"TokenId\":\"" + Air_TokenId + "\",\"AdultCount\":\"" + adult + "\",\"ChildCount\":\"" + child + "\",\"InfantCount\":\"" + infant + "\",\"JourneyType\":\"" + journeytype + "\",\"DirectFlight\":\"" + IsDirect.ToString().ToLower() + "\" ,\"OneStopFlight\":\"" + IsOneStop.ToString().ToLower() + "\", \"Segments\":[{\"Origin\":\"" + origin + "\",\"Destination\":\"" + destination + "\",\"FlightCabinClass\":\"" + cabinClass + "\",\"PreferredDepartureTime\":\"" + dateDeparture + "\",\"PreferredArrivalTime\":\"" + dateDeparture + "\"},{\"Origin\":\"" + destination + "\",\"Destination\":\"" + origin + "\",\"FlightCabinClass\":\"" + cabinClass + "\",\"PreferredDepartureTime\":\"" + dateArrival + "\",\"PreferredArrivalTime\":\"" + dateArrival + "\"}]}";
            }
            return HTTP_POST(M_Uri + "FlightSearch.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }

    public string FareRule(string ResultIndex, string TraceId)
    {
        try
        {
            // getting tokenid from database
            var response = GetTokenId();
            if (Air_TokenId == null || Air_TokenId == "")
            {
                if (response != "true")
                {
                    return response;
                }
            }
            string Json = string.Empty;
            Json = "{\"EndUserIp\": \"" + EndUserIp + "\", \"TokenId\": \"" + Air_TokenId + "\", \"TraceId\": \"" + TraceId + "\", \"ResultIndex\": \"" + ResultIndex + "\" }";
            return HTTP_POST(M_Uri + "FareRule.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }

    public string FareQuote(string ResultIndex, string TraceId)
    {
        try
        {
            // getting tokenid from database
            var response = GetTokenId();
            if (Air_TokenId == null || Air_TokenId == "")
            {
                if (response != "true")
                {
                    return response;
                }
            }
            FareQuotee = "yes";
            string Json = string.Empty;
            Json = "{\"EndUserIp\": \"" + EndUserIp + "\", \"TokenId\": \"" + Air_TokenId + "\", \"TraceId\": \"" + TraceId + "\", \"ResultIndex\": \"" + ResultIndex + "\" }";
            return HTTP_POST(M_Uri + "FareQuote.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }

    public string SSR(string ResultIndex, string TraceId)
    {
        try
        {
            // getting tokenid from database
            var response = GetTokenId();
            if (Air_TokenId == null || Air_TokenId == "")
            {
                if (response != "true")
                {
                    return response;
                }
            }
            string Json = string.Empty;
            Json = "{\"EndUserIp\": \"" + EndUserIp + "\", \"TokenId\": \"" + Air_TokenId + "\", \"TraceId\": \"" + TraceId + "\", \"ResultIndex\": \"" + ResultIndex + "\" }";
            return HTTP_POST(M_Uri + "SSR.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }

    public string FlightBook(object Json, string msrno, decimal PublishFee, decimal NetPayable)
    {
        try
        {
            // getting tokenid from database
            var response = GetTokenId();
            if (Air_TokenId == null || Air_TokenId == "")
            {
                if (response != "true")
                {
                    return response;
                }
            }
            string data = Json.ToString();
            RequestMsrNo = msrno;
            BookStatus = "yes";
            Air_PublishFee = PublishFee.ToString();
            Air_agencynetpay = NetPayable.ToString();
            return HTTP_POST(M_Uri + "FlightBook.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&PublishFee=" + PublishFee + "&NetPayable=" + NetPayable + "&MemberMsrNo=" + msrno + "&data= "+ data +" ", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }

    public string FlightTicket(object Json, decimal PublishFee, string agentid, decimal NetPayable, string msrno)
    {
        try
        {
            // getting tokenid from database
            var response = GetTokenId();
            if (Air_TokenId == null || Air_TokenId == "")
            {
                if (response != "true")
                {
                    return response;
                }
            }
            string data = Json.ToString();
            TicketStatus = "yes";
            Air_memberid = mm_api_memberid;
            Air_apikey = mm_token;
            Air_agentid = agentid;
            Air_agencynetpay = NetPayable.ToString();
            Air_PublishFee = PublishFee.ToString();
            RequestMsrNo = msrno;
            return HTTP_POST(M_Uri + "Ticket.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&PublishFee=" + PublishFee + "&NetPayable=" + NetPayable + "&agentid=" + agentid + "&MemberMsrNo=" + msrno + "&data=" + data +"", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }

    public string GetBookingDetails(object Json)
    {
        try
        {
            string data = Json.ToString();
            GetBookingDetail = "yes";
            return HTTP_POST(M_Uri + "GetBookingDetails.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&data="+ data +"", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }

    public string BookCancel(object Json)
    {
        try
        {
            string data = Json.ToString();
            return HTTP_POST(M_Uri + "ReleasePNRRequest.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&data=" + data + "", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }

    public string TicketCancel(string EndUserIp, string TokenIdd, string BookingId, string CancellationType, string Origin, string RequestType, string Destination, string TicketId, string Remarks, string PublishFee, string agentid, string NetPayable)
    {
        try
        {
            object Json = "{\"EndUserIp\": \"" + EndUserIp + "\",\"TokenId\":\"" + TokenIdd + "\",\"Remarks\":\"" + Remarks + "\",\"BookingId\":\"" + BookingId + "\",\"RequestType\":\"" + RequestType + "\",\"CancellationType\":\"" + CancellationType + "\"}";
            return HTTP_POST(M_Uri + "SendChangeRequest.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&PublishFee=" + PublishFee + "&NetPayable=" + NetPayable + "&agentid=" + agentid + "", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }
    public string GetCancellationCharges( string BookingId,  string agentid, string TokenIdd, string PublishFee)
    {
        try
        {
            object Json = "{\"BookingId\":\"" + BookingId + "\",\"TokenId\":\"" + TokenIdd + "\",\"PublishFee\":\"" + PublishFee + "\"}";
            return HTTP_POST(M_Uri + "RefundAmt.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&agentid=" + agentid + "&PublishFee=" + PublishFee + "", Json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }
    #endregion

    #region PropertiesClass
    [Serializable()]
    public class BookRequest
    {
        public string ResultIndex { get; set; }
        public List<Passenger> Passengers { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public string AgentReferenceNo { get; set; }
    }
    [Serializable()]
    public class BookConfirm
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public string PNR { get; set; }
        public string BookingId { get; set; }
    }
    [Serializable()]
    public class Bookdetail
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public int TripIndicator { get; set; }
    }
    [Serializable()]
    public class Passenger
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PaxType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportExpiry { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public Fare Fare { get; set; }
        public string City { get; set; }
        public List<Baggage> Baggage { get; set; }
        public List<MealDynamic> MealDynamic { get; set; }
        public Seat SeatDynamic { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public object FFAirlineCode { get; set; }
        public string FFNumber { get; set; }
        public string GSTCompanyAddress { get; set; }
        public string GSTCompanyContactNumber { get; set; }
        public string GSTCompanyName { get; set; }
        public string GSTNumber { get; set; }
        public string GSTCompanyEmail { get; set; }
    }
    [Serializable()]
    public class Fare
    {
        public string Currency { get; set; }
        public double BaseFare { get; set; }
        public double Tax { get; set; }
        public double YQTax { get; set; }
        public double AdditionalTxnFeePub { get; set; }
        public double AdditionalTxnFeeOfrd { get; set; }
        public double OtherCharges { get; set; }
        public double Discount { get; set; }
        public double PublishedFare { get; set; }
        public double OfferedFare { get; set; }
        public double TdsOnCommission { get; set; }
        public double TdsOnPLB { get; set; }
        public double TdsOnIncentive { get; set; }
        public double ServiceFee { get; set; }
    }


    [Serializable()]
    public class MealDynamic
    {
        public string WayType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string AirlineDescription { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

    }


    [Serializable()]
    public class Seat
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string CraftType { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string AvailablityType { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string RowNo { get; set; }
        public string SeatNo { get; set; }
        public string SeatType { get; set; }
        public string SeatWayType { get; set; }
        public string Compartment { get; set; }
        public string Deck { get; set; }
        public string Currency { get; set; }
        public string Price { get; set; }

    }

    [Serializable()]
    public class Meal
    {
        public string Text { get; set; }
        public string Code { get; set; }
        public string WayType { get; set; }
        public string Description { get; set; }
        public string AirlineDescription { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
    [Serializable()]
    public class Baggage
    {
        public string WayType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

    }




    [Serializable()]
    public class AirList
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string FlightNumber { get; set; }
        public string FareClass { get; set; }
        public string OriginTime { get; set; }
        public string Origin { get; set; }
        public string DestinationTime { get; set; }
        public string Destination { get; set; }
        public string Duration { get; set; }
        public string NoofSeatAvailable { get; set; }
        public decimal PubPrice { get; set; }
        public int StopCount { get; set; }
        public string StopDetail { get; set; }
        public string OriginDetail { get; set; }
        public string DestinationDetail { get; set; }
        public string AirllineFullName { get; set; }
        public bool IsRefundable { get; set; }
        public string LuggageDetail { get; set; }
        public string Resultid { get; set; }
        public string TravelDate { get; set; }
        public string IsLCC { get; set; }
    }

    [Serializable()]
    public class AirLineName
    {
        public string AirllineFullName { get; set; }
    }

    [Serializable]
    public class AirlineSummary
    {
        public string Date { get; set; }
        public string FlightNumber { get; set; }
        public string Dept { get; set; }
        public string DeptTime { get; set; }
        public string Arr { get; set; }
        public string ArrTime { get; set; }
    }

    [Serializable]
    public class Sale
    {
        public string PaxType { get; set; }
        public string PxnCount { get; set; }
        public string BasePublishedPrice { get; set; }
        public string PublishedOTTax { get; set; }
        public string PublishedYQTax { get; set; }
        public string PublishTS { get; set; }
        public string TotalPubPrice { get; set; }
    }
    #endregion
}
