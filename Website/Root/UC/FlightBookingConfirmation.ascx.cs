using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Root_UC_FlightBookingConfirmation : System.Web.UI.UserControl
{
    #region Properties
    private string Resultt = string.Empty;
    private EzulixAir eAir = new EzulixAir();
    cls_myMember clsm = new cls_myMember();
    cls_connection Cls = new cls_connection();

    private DataSet ds = new DataSet();
    private string Result = string.Empty;
    protected int JourneyType = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Ticket"] != null)
                {
                    if (Session["InFlightDtl"] != null)
                    {
                        if (Session["InFlightDtl"].ToString() != "")
                        {
                            id1.Visible = true;
                        }
                        else { id1.Visible = false; }
                    }
                    else
                    {
                        id1.Visible = false;
                    }
                    ds = (DataSet)Session["Ticket"];
                    string TokenId = EzulixAir.Air_TokenId;
                    if (TokenId == string.Empty)
                    {
                        eAir.GetTokenId();
                        TokenId = EzulixAir.Air_TokenId;
                    }
                    if (Session["TripIndicator"] != null)
                    {
                        int tripind = Convert.ToInt32(Session["TripIndicator"].ToString());
                        List<EzulixAir.Bookdetail> BObjjj = new List<EzulixAir.Bookdetail>
            {
                new EzulixAir.Bookdetail
                {
                    EndUserIp="172.107.166.241",
                    TokenId=TokenId,
                    TraceId = ds.Tables["Response"].Rows[0]["TraceId"].ToString(),
                    TripIndicator = tripind,
                }
            };
                        string Jsonnn = JsonConvert.SerializeObject(BObjjj);
                        Resultt = eAir.GetBookingDetails(Jsonnn.Substring(1, (Jsonnn.Length) - 1));
                        if (Resultt != null)
                        {
                            ds = eAir.Deserialize(Resultt);

                            if (ds.Tables.Contains("FlightItinerary"))
                            {
                                if (ds.Tables["FlightItinerary"].Rows.Count > 0)
                                {
                                    FlightInformation();
                                    PassengerDetails();
                                    Session["BCtraceId"] = null;
                                    Session["BCtraceId"] = ds.Tables["Response"].Rows[0]["TraceId"].ToString();
                                    if (Session["FareRule"] != null)
                                    {
                                        lbl_FareRules.Text = Session["FareRule"].ToString();
                                    }
                                    else
                                    {
                                        lbl_FareRules.Text = "";
                                    }
                                    SaleSummary();
                                    for (int i = 0; i < ds.Tables["FlightItinerary"].Rows.Count; i++)
                                    {
                                        List<EzulixAir.BookConfirm> BObjj = new List<EzulixAir.BookConfirm>
                                            {
                                                new EzulixAir.BookConfirm
                                                {
                                                    EndUserIp="172.107.166.241",
                                                    TokenId=TokenId,
                                                    PNR = ds.Tables["FlightItinerary"].Rows[i]["PNR"].ToString(),
                                                    BookingId = ds.Tables["FlightItinerary"].Rows[i]["BookingId"].ToString(),
                                            }
                                            };
                                        Session["BookingId"] = ds.Tables["FlightItinerary"].Rows[i]["BookingId"].ToString();
                                        Session["Source"] = ds.Tables["FlightItinerary"].Rows[i]["Source"].ToString();
                                        string Jsonn = JsonConvert.SerializeObject(BObjj);
                                        //Resultt = eAir.GetBookingDetails(Jsonn.Substring(1, (Jsonn.Length) - 1));
                                    }
                                    Session["BookingDetRespone"] = null;
                                    DataSet dss = eAir.Deserialize(Resultt);
                                    Session["BookingDetRespone"] = dss;
                                    if (Session["JourneyType"] != null)
                                    {
                                        if (Session["JourneyType"].ToString() == "2")
                                        {
                                            dv_ReturnFlight.Visible = true;
                                            if (Session["OutFlightDtl"] != null)
                                            {
                                                if (Session["OutFlightDtl"].ToString() != "")
                                                {
                                                    lbl_OutFlight.Text = Session["OutFlightDtl"].ToString();
                                                }
                                                else
                                                {
                                                    lbl_OutFlight.Text = "";
                                                }
                                            }
                                            else
                                            {
                                                lbl_OutFlight.Text = "";
                                            }
                                            if (Session["InFlightDtl"] != null)
                                            {
                                                if (Session["InFlightDtl"].ToString() != "")
                                                {
                                                    Lbl_InFlight.Text = Session["InFlightDtl"].ToString();
                                                }
                                                else
                                                {
                                                    Lbl_InFlight.Text = "";
                                                }
                                            }
                                            else
                                            {
                                                Lbl_InFlight.Text = "";
                                            }
                                            Lbl_OutBooking.Text = "Out booking in process.";
                                            Lbl_InBooking.Text = "In booking";
                                            if (Session["Returnbooking"] != null)
                                            {
                                                Dv_OutBooking.Attributes.Add("style", "background-color: #aliceblue");
                                                Dv_InBooking.Attributes.Add("style", "background-color: #5cb85cad");
                                                Lbl_OutBooking.Text = "Out booking";
                                                Lbl_InBooking.Text = "In booking in process.";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('"+ Resultt + "');location.replace('FlightSearch.aspx');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Result');location.replace('FlightSearch.aspx');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('tripindicator Session Empty!');location.replace('FlightSearch.aspx');", true);
                    }
                }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    #region Method
    private void FlightInformation()
    {
        try
        {
            for (int i = 0; i < ds.Tables["FlightItinerary"].Rows.Count; i++)
            {
                DataRow[] drSegments = ds.Tables["Segments"].Select("FlightItinerary_Id=" + ds.Tables["FlightItinerary"].Rows[i]["FlightItinerary_Id"].ToString());
                DataTable dtAirline = ds.Tables["Airline"].Select("Segments_Id=" + drSegments[0]["Segments_Id"].ToString()).CopyToDataTable();
                var OriginRow = ds.Tables["Origin"].Select("Segments_Id=" + drSegments[0]["Segments_Id"].ToString());
                var OriginAirportRow = ds.Tables["AirPort"].Select("Origin_Id=" + OriginRow[0]["Origin_Id"].ToString());
                var DestinationRow = ds.Tables["Destination"].Select("Segments_Id=" + drSegments[0]["Segments_Id"].ToString());
                var DestinationAirportRow = ds.Tables["AirPort"].Select("Destination_Id=" + DestinationRow[0]["Destination_Id"].ToString());
                lbl_FlightNo.Text = dtAirline.Rows[0]["Airlinecode"].ToString() + "-" + dtAirline.Rows[0]["FlightNumber"].ToString();
                lbl_Origin.Text = OriginAirportRow[0]["AirPortCode"].ToString();
                lbl_Destination.Text = DestinationAirportRow[0]["AirPortCode"].ToString();
                lbl_DepDatetime.Text = Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("dd/MM/yyyy") + "&nbsp;|&nbsp;" + Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("HH:mm tt");
                lbl_ArrDatetime.Text = Convert.ToDateTime(DestinationRow[0]["ArrTime"]).ToString("dd/MM/yyyy") + "&nbsp;|&nbsp;" + Convert.ToDateTime(DestinationRow[0]["ArrTime"]).ToString("HH:mm tt");
                lbl_Class.Text = dtAirline.Rows[0]["FareClass"].ToString();
                lbl_PNR.Text = ds.Tables["FlightItinerary"].Rows[i]["PNR"].ToString();
                lbl_Bookingid.Text = ds.Tables["FlightItinerary"].Rows[i]["BookingId"].ToString();
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    private void PassengerDetails()
    {
        try
        {
            gv_PassengerDetails.DataSource = ds.Tables["Passenger"];
            gv_PassengerDetails.DataBind();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    private void SaleSummary()
    {
        try
        {
            List<EzulixAir.Sale> SObj = new List<EzulixAir.Sale>();
            string PublishedTS = string.Empty;
            for (int i = 0; i < ds.Tables["Fare"].Rows.Count; i++)
            {
                if (ds.Tables["Fare"].Rows[i]["Passenger_id"].ToString() != string.Empty)
                {
                    DataRow[] DrPassenger = ds.Tables["Passenger"].Select("Passenger_id=" + ds.Tables["Fare"].Rows[i]["Passenger_id"]);
                    if (Math.Round(Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["OtherCharges"].ToString()) - Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["CommissionEarned"].ToString()), MidpointRounding.AwayFromZero) < 0)
                    {
                        PublishedTS = "0";
                    }
                    else
                    {
                        PublishedTS = Math.Round((Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["OtherCharges"]) - Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["CommissionEarned"])) / ds.Tables["Fare"].Rows.Count, MidpointRounding.AwayFromZero).ToString();
                    }
                    SObj.Add(new EzulixAir.Sale
                    {
                        PaxType = DrPassenger[0]["PaxType"].ToString(),
                        PxnCount = DrPassenger[0]["PaxType"].ToString(),
                        BasePublishedPrice = Math.Round(Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["BaseFare"].ToString()), MidpointRounding.AwayFromZero).ToString(),
                        PublishedOTTax = Math.Round(Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["Tax"].ToString()) - Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["YQTax"].ToString()), MidpointRounding.AwayFromZero).ToString(),
                        // PublishedYQTax = Math.Round(Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["YQTax"].ToString()), MidpointRounding.AwayFromZero).ToString(),

                        PublishTS = PublishedTS,
                    });
                }
                else
                {
                    string pubfee = Math.Round(Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["PublishedFare"].ToString()), MidpointRounding.AwayFromZero).ToString();
                    tbl_PublishFare.Text = pubfee;
                    lbl_TotalPayable.Text = Math.Round((Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["PublishedFare"].ToString()) - (Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["CommissionEarned"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["IncentiveEarned"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["PLBEarned"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["AdditionalTxnFeeOfrd"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["AdditionalTxnFeePub"]))) + (Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["TdsOnCommission"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["TdsOnIncentive"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["TdsOnPLB"])), MidpointRounding.AwayFromZero).ToString();
                    lbl_Discount.Text = (Convert.ToDecimal(pubfee) - Convert.ToDecimal(lbl_TotalPayable.Text)).ToString();


                }
            }
            List<EzulixAir.Sale> FSboj = new List<EzulixAir.Sale>();
            if (SObj.Where(f => f.PaxType == "1").Count() > 0)
            {
                FSboj.Add(new EzulixAir.Sale
                {
                    PaxType = SObj.Where(f => f.PaxType == "1").Sum(f => Convert.ToDouble(f.PaxType)).ToString(),
                    PxnCount = SObj.Where(f => f.PaxType == "1").Sum(f => Convert.ToDouble(f.PxnCount)).ToString(),
                    BasePublishedPrice = SObj.Where(f => f.PaxType == "1").Sum(f => Convert.ToDouble(f.BasePublishedPrice)).ToString(),
                    PublishedOTTax = SObj.Where(f => f.PaxType == "1").Sum(f => Convert.ToDouble(f.PublishedOTTax)).ToString(),
                    //    PublishedYQTax = SObj.Where(f => f.PaxType == "1").Sum(f => Convert.ToDouble(f.PublishedYQTax)).ToString(),
                    PublishTS = SObj.Where(f => f.PaxType == "1").Sum(f => Convert.ToDouble(f.PublishTS)).ToString(),
                });
            }
            if (SObj.Where(f => f.PaxType == "2").Count() > 0)
            {
                FSboj.Add(new EzulixAir.Sale
                {
                    PaxType = SObj.Where(f => f.PaxType == "2").Sum(f => Convert.ToDouble(f.PaxType)).ToString(),
                    PxnCount = SObj.Where(f => f.PaxType == "2").Sum(f => Convert.ToDouble(f.PxnCount)).ToString(),
                    BasePublishedPrice = SObj.Where(f => f.PaxType == "2").Sum(f => Convert.ToDouble(f.BasePublishedPrice)).ToString(),
                    PublishedOTTax = SObj.Where(f => f.PaxType == "2").Sum(f => Convert.ToDouble(f.PublishedOTTax)).ToString(),
                    //   PublishedYQTax = SObj.Where(f => f.PaxType == "2").Sum(f => Convert.ToDouble(f.PublishedYQTax)).ToString(),
                    PublishTS = SObj.Where(f => f.PaxType == "2").Sum(f => Convert.ToDouble(f.PublishTS)).ToString(),
                });
            }
            if (SObj.Where(f => f.PaxType == "3").Count() > 0)
            {
                FSboj.Add(new EzulixAir.Sale
                {
                    PaxType = SObj.Where(f => f.PaxType == "3").Sum(f => Convert.ToDouble(f.PaxType)).ToString(),
                    PxnCount = SObj.Where(f => f.PaxType == "3").Sum(f => Convert.ToDouble(f.PxnCount)).ToString(),
                    BasePublishedPrice = SObj.Where(f => f.PaxType == "3").Sum(f => Convert.ToDouble(f.BasePublishedPrice)).ToString(),
                    PublishedOTTax = SObj.Where(f => f.PaxType == "3").Sum(f => Convert.ToDouble(f.PublishedOTTax)).ToString(),
                    //  PublishedYQTax = SObj.Where(f => f.PaxType == "3").Sum(f => Convert.ToDouble(f.PublishedYQTax)).ToString(),
                    PublishTS = SObj.Where(f => f.PaxType == "3").Sum(f => Convert.ToDouble(f.PublishTS)).ToString(),
                });
            }
            dtlist_SaleSummary.DataSource = FSboj;
            dtlist_SaleSummary.DataBind();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    public static void AjaxMessageBox(Control page, string msg)
    {
        try
        {
            string script = "alert('" + msg + "')";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "UserSecurity", script, true);
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    private string FareRules(string Resultindex, string Traceid)
    {
        try
        {
            Result = eAir.FareRule(Resultindex, Session["Traceid"].ToString());
            return Result;
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
            return Result;
        }
    }

    private void FlightBook(string Resultindex)
    {
        try
        {
            if (Resultindex.Substring(0, 1).ToUpper() == "I")
            {
                Session["TripIndicator"] = "2";
            }
            else if (Resultindex.Substring(0, 1).ToUpper() == "O")
            {
                Session["TripIndicator"] = "1";
            }
            Result = string.Empty;
            Session["FareRule"] = null;
            string FareResult = FareRules(Resultindex, Session["Traceid"].ToString());
            if (FareResult != null)
            {
                DataSet dsFare = eAir.Deserialize(FareResult);
                Session["FareRule"] = dsFare;
                Result = eAir.FareQuote(Resultindex, Session["Traceid"].ToString());
                if (Result != string.Empty)
                {
                    DataSet ds = eAir.Deserialize(Result);
                    if (ds.Tables.Contains("Response")) { 
                    if (ds.Tables["Response"].Rows[0]["ResponseStatus"].ToString() == "1")
                    {
                        if (ds.Tables.Contains("Results"))
                        {
                            var source = ds.Tables["Results"].Rows[0]["Source"].ToString();
                            Session["Sourcenum"] = ds.Tables["Results"].Rows[0]["Source"].ToString();
                            DataTable dt = new DataTable();
                            cls_connection objconnection = new cls_connection();
                            dt = objconnection.select_data_dt("select * from Sources where Sources='" + source + "'");
                            if (dt.Rows.Count > 0)
                            {
                                Session["Sourcename"] = dt.Rows[0]["Airline"].ToString();
                                // AirAsia, AK, I5 is same (19)
                            }
                            else
                            {
                                Session["Sourcename"] = "";
                            }
                        }
                        Session["BookingDetail"] = null;
                        Session["BookingDetail"] = ds;
                        if (Session["Traceid"] != null)
                        {
                            string SSRResult = eAir.SSR(Resultindex, Session["Traceid"].ToString());
                            if (SSRResult != null)
                            {
                                DataSet dsSSR = eAir.Deserialize(SSRResult);
                                Session["SSR"] = dsSSR;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "location.replace('FlightPassengerDetails.aspx');", true);


                                return;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty SSR Response!');location.replace('FlightSearch.aspx');", true);
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Traceid!');location.replace('FlightSearch.aspx');", true);
                            return;
                        }

                    }
                    else if (ds.Tables["Response"].Rows[0]["ResponseStatus"].ToString() == "4")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Result + "');location.replace('FlightSearch.aspx');", true);
                }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Fare Quote Response!');location.replace('FlightSearch.aspx');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Rule Response!');location.replace('FlightSearch.aspx');", true);
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    #endregion

    #region PropertiesClass
    public class BookingDetails
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string PNR { get; set; }
        public string BookingId { get; set; }
        public string TraceId { get; set; }
        public string Source { get; set; }
    }

    public class TicketCancel
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string BookingId { get; set; }
        public string RequestType { get; set; }
        public string CancellationType { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string TicketId { get; set; }
        public string Remarks { get; set; }
    }
    #endregion

    #region Events
    protected void btn_downloadInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            string NewTokenId = EzulixAir.Air_TokenId;
            if (NewTokenId == string.Empty)
            {
                eAir.GetTokenId();
                NewTokenId = EzulixAir.Air_TokenId;
            }
            if (Session["BCtraceId"] != null && Session["MsrNoLog"] != null)
            {
                List<BookingDetails> BObj = new List<BookingDetails>
                {
                    new BookingDetails
                    {
                        EndUserIp="172.107.166.241",
                        TokenId=NewTokenId,
                        PNR=lbl_PNR.Text,
                        BookingId=lbl_Bookingid.Text,
                        TraceId=Session["BCtraceId"].ToString()
                    }
                };
                string Json = JsonConvert.SerializeObject(BObj);
                string msrno = Session["MsrNoLog"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + "Empty Session!" + "');location.replace('FlightSearch.aspx');", true);
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }


    protected void btn_cancelTicket_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Ticket"] != null && Session["BookingId"] != null && Session["agentid"] != null && Session["FlightFee"] != null && Session["NetPayable"] != null && Session["MemberIdLog"] != null)
            {
                string TokenId = EzulixAir.Air_TokenId;
                if (TokenId == string.Empty)
                {
                    eAir.GetTokenId();
                    TokenId = EzulixAir.Air_TokenId;
                }
                ds = (DataSet)Session["Ticket"];
                Session["BCtraceId"] = ds.Tables["Response"].Rows[0]["TraceId"].ToString();
                Session["Origin"] = ds.Tables["FlightItinerary"].Rows[0]["Origin"].ToString();
                Session["Destination"] = ds.Tables["FlightItinerary"].Rows[0]["Destination"].ToString();
                Session["TicketId"] = ds.Tables["Ticket"].Rows[0]["TicketId"].ToString();
                string EndUserIp = "172.107.166.241";
                string TokenIdd = TokenId;
                string BookingId = Session["BookingId"].ToString();
                string CancellationType = "3";
                string Origin = Session["Origin"].ToString();
                string RequestType = "1";
                string Destination = Session["Destination"].ToString();
                string TicketId = Session["TicketId"].ToString();
                string Remarks = "testing in integrating";
                string agentid = Session["agentid"].ToString();
                string PublishFee = Session["FlightFee"].ToString();
                string NetPayable = Session["NetPayable"].ToString();

                Result = eAir.TicketCancel(EndUserIp, TokenIdd, BookingId, CancellationType, Origin, RequestType, Destination, TicketId, Remarks, PublishFee, agentid, NetPayable);
                DataSet dss = eAir.Deserialize(Result);
                Session["CancelBook"] = dss;
                if (Result != string.Empty)
                {
                    if (dss.Tables.Contains("TicketCRInfo"))
                    {
                        if (dss.Tables["TicketCRInfo"].Rows[0]["Status"].ToString() == "1" && dss.Tables["TicketCRInfo"].Rows[0]["Remarks"].ToString() == "Successful")
                        {
                           
                            Cls.update_data("update tbl_Flight_Api_Response set FlightStatus='" + "Cancelled" + "' where BookingId='" + BookingId + "'");
                            AjaxMessageBox(this, "Ticket Cancelled Successfully");
                           
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.location ='FlightTicket_List.aspx';", true);
                        }
                    }
                    else
                    {
                        if (dss.Tables.Contains("Error"))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + dss.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightTicket_List.aspx');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Result + "');location.replace('FlightSearch.aspx');", true);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Session!');location.replace('FlightSearch.aspx');", true);
              
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    protected void Lnk_InFlight_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["InFlight"] != null)
            {
                Session["Returnbooking"] = null;
                Session["Returnbooking"] = 1;
                Session["IsLCC"] = Session["IsLCC2"].ToString();
                FlightBook(Session["InFlight"].ToString());
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    #endregion
}