using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Root_UC_FlightReviewBooking : System.Web.UI.UserControl
{
    #region Properties
    private EzulixAir eAir = new EzulixAir();
    cls_myMember clsm = new cls_myMember();
    private DataSet ds = new DataSet();
    private string Result = string.Empty;
    cls_connection Cls = new cls_connection();
    private static int JourneyType;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try { 
        if (!IsPostBack)
        {
            if (Session["BookingRespone"] != null)
            {
                ds = (DataSet)Session["BookingRespone"];
                FlightInformation();
                PassengerDetails();
                SaleSummary();
                lbl_FareRules.Text = Session["FareRule"].ToString();
                ViewState["TraceId"] = null;
                ViewState["TraceId"] = ds.Tables["Response"].Rows[0]["TraceId"].ToString();
                ViewState["PNR"] = null;
                ViewState["PNR"] = ds.Tables["Response"].Rows[1]["PNR"].ToString();
                ViewState["BookingId"] = null;
                ViewState["BookingId"] = ds.Tables["Response"].Rows[1]["BookingId"].ToString();
                ViewState["isLcc"] = null;
                ViewState["isLcc"] = ds.Tables["FlightItinerary"].Rows[0]["IsLcc"].ToString();
                Session["TicketRespone"] = null;
                JourneyType = Convert.ToInt32(Session["JourneyType"]);
                if (JourneyType == 2)
                {
                   
                    dv_ReturnFlight.Visible = true;
                    lbl_OutFlight.Text = "";
                    Lbl_InFlight.Text = "";
                    if (Session["OutFlightDtl"] != null)
                    {
                        if (Session["OutFlightDtl"].ToString() != "") {
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
                    if (Session["InFlightDtl"] != null )
                    {
                        if (Session["InFlightDtl"].ToString() != "")
                        {
                            Lbl_InFlight.Text = Session["InFlightDtl"].ToString();
                        }
                        else { Lbl_InFlight.Text = ""; }
                                             
                    }
                    else
                    {
                        Lbl_InFlight.Text = "";
                    }

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
    public static void AjaxMessageBox(Control page, string msg)
    {
        try { 
        string script = "alert('" + msg + "')";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "UserSecurity", script, true);
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    private void FlightInformation()
    {
        try { 
        if(ds.Tables["FlightItinerary"].Rows.Count > 0)
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
       }
       }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    private void PassengerDetails()
    {
        try { 
        if (!ds.Tables["Passenger"].Columns.Contains("DateOfBirth"))
        {
            ds.Tables["Passenger"].Columns.Add("DateOfBirth", typeof(string));
        }
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
        try { 
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
               //     PublishedYQTax = Math.Round(Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["YQTax"].ToString()), MidpointRounding.AwayFromZero).ToString(),
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
         //       PublishedYQTax = SObj.Where(f => f.PaxType == "1").Sum(f => Convert.ToDouble(f.PublishedYQTax)).ToString(),
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
            //    PublishedYQTax = SObj.Where(f => f.PaxType == "3").Sum(f => Convert.ToDouble(f.PublishedYQTax)).ToString(),
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
    #endregion

    #region PropertiesClass
    #region Ticket_Request_for_LCC
    public class TicketRequestLCC
    {
        public object PreferredCurrency { get; set; }
        public string ResultIndex { get; set; }
        public string AgentReferenceNo { get; set; }
        public Passenger[] Passengers { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
    }

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
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public string FFAirlineCode { get; set; }
        public string FFNumber { get; set; }
        public Baggage[] Baggage { get; set; }
        public Mealdynamic[] MealDynamic { get; set; }
        public object[] SeatDynamic { get; set; }
        public string GSTCompanyAddress { get; set; }
        public string GSTCompanyContactNumber { get; set; }
        public string GSTCompanyName { get; set; }
        public string GSTNumber { get; set; }
        public string GSTCompanyEmail { get; set; }
    }

    public class Fare
    {
        public float BaseFare { get; set; }
        public float Tax { get; set; }
        public float YQTax { get; set; }
        public float AdditionalTxnFeePub { get; set; }
        public float AdditionalTxnFeeOfrd { get; set; }
        public float OtherCharges { get; set; }
    }

    public class Baggage
    {
        public int WayType { get; set; }
        public string Code { get; set; }
        public int Description { get; set; }
        public int Weight { get; set; }
        public float BaseCurrencyPrice { get; set; }
        public string BaseCurrency { get; set; }
        public string Currency { get; set; }
        public float Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

    public class Mealdynamic
    {
        public int WayType { get; set; }
        public string Code { get; set; }
        public int Description { get; set; }
        public string AirlineDescription { get; set; }
        public int Quantity { get; set; }
        public string BaseCurrency { get; set; }
        public float BaseCurrencyPrice { get; set; }
        public string Currency { get; set; }
        public float Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
    #endregion
    #region Ticket_Request_for_NON_LCC_WithoutPassport
    public class TicketRequestNONLCCnOTPP
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public string PNR { get; set; }
        public int BookingId { get; set; }
    }
    #endregion

    #region Ticket_Request_for_NON_LCC_WithPassport
    public class TicketRequestNONLCCiSPP
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public Passport[] Passport { get; set; }
        public string PNR { get; set; }
        public int BookingId { get; set; }
    }

    public class Passport
    {
        public int PaxId { get; set; }
        public string PassportNo { get; set; }
        public DateTime PassportExpiry { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    #endregion
    #endregion

    #region Events
    public static string GetIPAddress()
    {
        string Requestipaddress;
        Requestipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (Requestipaddress == "" || Requestipaddress == null)
            Requestipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        return Requestipaddress;
    }
    protected void btn_Getticket_Click(object sender, EventArgs e)
    {
        try
        {
            string TokenId = EzulixAir.Air_TokenId;
            if (TokenId == string.Empty)
            {
                eAir.GetTokenId();
                TokenId = EzulixAir.Air_TokenId;
            }
            if (Convert.ToBoolean(ViewState["isLcc"]))
            {
                List<TicketRequestLCC> TObj = new List<TicketRequestLCC>
                {
                    new TicketRequestLCC
                    {
                        EndUserIp="172.107.166.241",
                        TokenId=TokenId,
                        TraceId=ViewState["TraceId"].ToString(),
                        //ResultIndex=
                    }
                };
            }
            else
                {
                List<TicketRequestNONLCCnOTPP> TObj = new List<TicketRequestNONLCCnOTPP>
                    {
                        new TicketRequestNONLCCnOTPP
                        {
                            EndUserIp="172.107.166.241",
                            TokenId=TokenId,
                            TraceId=ViewState["TraceId"].ToString(),
                            PNR=ViewState["PNR"].ToString(),
                            BookingId=Convert.ToInt32(ViewState["BookingId"]),
                        }
                    };
                string Json = JsonConvert.SerializeObject(TObj);
                Session["Baggage"] = null;
                Session["MealDynamic"] = null;
                cls_myMember clsm = new cls_myMember();
                string agentid = clsm.Cyrus_GetTransactionID_New();
                string Result = string.Empty;
                //decimal amount = Convert.ToDecimal(ReplaceCode(Request.Form["amount"].ToString().Trim()));
                decimal PublishFee = Convert.ToDecimal(Session["FlightFee"].ToString());
                Session["agentid"] = agentid;
                Session["FlightFee"] = PublishFee;
                decimal NetPayable = Convert.ToDecimal(Session["NetPayable"].ToString());
                string OperationName = string.Empty;
                string resu = string.Empty;

                int chkbal = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(PublishFee, Convert.ToInt32(Session["MsrNoLog"].ToString()));
                if (chkbal == 1)
                {
                 
                    int tra = clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal("-" + PublishFee), "Dr", "FlightPublishedFee:- '" + agentid + "'");
                    if (tra > 0)
                    {
                        var msrno = Session["MsrNoLog"].ToString();
                        Result = eAir.FlightTicket(Json.Substring(1, (Json.Length) - 1), PublishFee, agentid, NetPayable, msrno);
                        if (Result != string.Empty)
                        {
                            Session["BookingRespone"] = null;
                            DataSet dss = eAir.Deserialize(Result);
                            Session["BookingRespone"] = dss;
                            Session["Ticket"] = null;
                            Session["Ticket"] = dss;
                            if (dss.Tables.Contains("Response"))
                            {
                                if (dss.Tables["Response"].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dss.Tables["Response"].Columns.Count; i++)
                                    {

                                        if (dss.Tables["Response"].Columns[i].ColumnName == "TicketStatus")
                                        {
                                            if (dss.Tables["Response"].Rows[1]["TicketStatus"].ToString() == "1" || dss.Tables["Response"].Rows[1]["TicketStatus"].ToString() == "5")
                                            {
                                                decimal amt = 0;
                                                decimal pubfee1 = Convert.ToDecimal(PublishFee);

                                                decimal pubfee2 = Convert.ToDecimal(dss.Tables["Fare"].Rows[0]["PublishedFare"].ToString());
                                                double comamount = GetCommissionAmt(Convert.ToDouble(pubfee2), Session["MemberIdLog"].ToString());
                                                clsm.Wallet_MakeTransaction(Session["MemberIdLog"].ToString(), Convert.ToDecimal(comamount.ToString().Trim()), "Cr", " Flight Ticket Comission Txn:- " + agentid + "");

                                                if (pubfee1 != pubfee2)
                                                {

                                                    if (pubfee1 > pubfee2)
                                                    {
                                                        amt = pubfee1 - pubfee2;
                                                        clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), amt, "Cr", "Flight Return Extra:- '" + agentid + "'");
                                                    }
                                                    else
                                                    {
                                                        amt = pubfee2 - pubfee1;
                                                        clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal("-" + amt), "Dr", "Flight Increased Fee:- '" + agentid + "'");
                                                    }
                                                }
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.location ='FlightBookingConfirmation.aspx';", true);

                                            }
                                            else
                                            {
                                                clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal(PublishFee), "Cr", "Flight Empty Result:- '" + agentid + "'");
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + dss.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);
                                                
                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal(PublishFee), "Cr", "Flight Empty Result:- '" + agentid + "'");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + dss.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('"+ Result +"');location.replace('FlightSearch.aspx');", true);
                            }
                        }
                        else
                        {
                            clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal(PublishFee), "Cr", "Flight Empty Result:- '" + agentid + "'");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Result!');location.replace('FlightSearch.aspx');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Unsuccessfull!');location.replace('FlightSearch.aspx');", true);
                    }
                  
                    }
                    else
                    {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Wallet Balance!');location.replace('FlightSearch.aspx');", true);
                    
                    }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    public double GetCommissionAmt(double amount, string memberid)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double Commission = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            DataTable dtMemberMaster = cls.select_data_dt("select * from tblmlm_membermaster where memberid='" + memberid + "'");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixCommission @amount='" + Convert.ToDecimal(amount) + "',@packageid='" + PackageID + "'");
            if (dtsr.Rows.Count > 0)
            {
                Commission = Convert.ToDouble(dtsr.Rows[0]["commission"].ToString());
                if (Commission > 0)
                {
                    Commission = (Convert.ToDouble(amount) * Commission) / 100;
                }
                NetAmount = Commission;
            }
            else
            {
                NetAmount = 0;
            }
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }
    protected void btn_cancelbook_Click(object sender, EventArgs e)
    {
        try
        {
            string TokenId = EzulixAir.Air_TokenId;
            if (TokenId == string.Empty)
            {
                eAir.GetTokenId();
                TokenId = EzulixAir.Air_TokenId;
            }

            List<BookingDetails> BObj = new List<BookingDetails>
                {
                    new BookingDetails
                    {
                        EndUserIp="172.107.166.241",
                        TokenId=TokenId,
                        BookingId=ViewState["BookingId"].ToString(),
                        Source = Session["Sourcenum"].ToString()
                    }
                };
            string BookingId = ViewState["BookingId"].ToString();
            string Json = JsonConvert.SerializeObject(BObj);
            Result = eAir.BookCancel(Json.Substring(1, (Json.Length) - 2));
            DataSet dss = eAir.Deserialize(Result);
            Session["CancelBook"] = dss;
            if (Result != string.Empty)
            {
                if (dss.Tables.Contains("Response"))
                {
                    if (dss.Tables["Response"].Rows[0]["ResponseStatus"].ToString() == "1")
                    {
                       Cls.update_data("update tbl_Flight_Api_Response set amountstatus='" + "Cancelled" + "' where BookingId='" + BookingId + "'");
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Booking Cancelled');location.replace('FlightSearch.aspx');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Could Not Cancelled!');location.replace('FlightSearch.aspx');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Result + "');location.replace('FlightSearch.aspx');", true);
                }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    #endregion
    public class BookingDetails
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string PNR { get; set; }
        public string BookingId { get; set; }
        public string TraceId { get; set; }
        public string Source { get; set; }
    }
    #region Dictionary
    private static string BookingStatus(string Value)
    {
        string Status = string.Empty;
        Dictionary<string, string> dicBookingStatus = new Dictionary<string, string>
            {
                { "0", "NotSet" },
                { "1", "Successful" },
                { "2", "Failed" },
                { "3", "OtherFare" },
                { "5", "OtherClass" },
                { "5", "BookedOther" },
                { "6", "NotConfirmed" }
            };
        return Status = dicBookingStatus.FirstOrDefault(x => x.Key == Value).Value;
    }
    #endregion
}