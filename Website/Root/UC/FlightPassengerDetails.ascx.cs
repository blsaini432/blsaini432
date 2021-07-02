using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Root_UC_FlightPassengerDetails : System.Web.UI.UserControl
{
    #region Properties
    private EzulixAir eAir = new EzulixAir();
    private string Result = string.Empty;
    private string Resultt = string.Empty;
    private DataSet ds = new DataSet();
    private DataSet dsFare = new DataSet();
    private DataSet dsSSR = new DataSet();
    private List<EzulixAir.Passenger> PObj = new List<EzulixAir.Passenger>();
    private List<Meal> mObj = new List<Meal>();
    private List<EzulixAir.MealDynamic> mapiObj = new List<EzulixAir.MealDynamic>();
    private List<Baggage> bObj = new List<Baggage>();
    private List<EzulixAir.Baggage> bapiObj = new List<EzulixAir.Baggage>();
    private List<Seat> SObj = new List<Seat>();
    cls_connection Cls = new cls_connection();

    public static int JourneyType;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["BookingDetail"] != null)
                {
                    try
                    {
                        ds = (DataSet)Session["BookingDetail"];
                        if (ds.Tables["Results"].Rows[0]["IsGSTMandatory"].ToString() == "true")
                        {
                            dv_GstDetail.Visible = true;
                            chk_GstDetail.Visible = false;
                            chk_GstDetail.Checked = true;
                        }
                        else
                        {
                        }
                        dsFare = (DataSet)Session["FareRule"];
                        dsSSR = (DataSet)Session["SSR"];
                        ViewState["Lcc"] = ds.Tables["Results"].Rows[0]["isLcc"].ToString();

                        ViewState["AdultsCount"] = 0;
                        ViewState["ChildCount"] = 0;
                        ViewState["InfantCount"] = 0;
                        lbl_IsLcc.Text = Session["IsLCC"].ToString();
                        if (ds.Tables["Farebreakdown"].Select("PassengerType=1").Count() > 0)
                        {
                            ViewState["AdultsCount"] = ds.Tables["Farebreakdown"].Select("PassengerType=1")[0].ItemArray[2];
                            Session["AdultsCount"] = ViewState["AdultsCount"].ToString();
                        }
                        if (ds.Tables["Farebreakdown"].Select("PassengerType=2").Count() > 0)
                        {
                            ViewState["ChildCount"] = ds.Tables["Farebreakdown"].Select("PassengerType=2")[0].ItemArray[2];
                            Session["ChildCount"] = ViewState["ChildCount"].ToString();
                        }
                        if (ds.Tables["Farebreakdown"].Select("PassengerType=3").Count() > 0)
                        {
                            ViewState["InfantCount"] = ds.Tables["Farebreakdown"].Select("PassengerType=3")[0].ItemArray[2];
                            Session["InfantCount"] = ViewState["InfantCount"].ToString();
                        }
                        ViewState["Passenger"] = null;
                        JourneyType = Convert.ToInt32(Session["JourneyType"]);
                        if (JourneyType == 2)
                        {
                            Session["JourneyType"] = 2;
                            dv_ReturnFlight.Visible = true;
                            if (Session["OutFlightDtl"] != null)
                            {
                                if (Session["OutFlightDtl"].ToString() != "")
                                {
                                    lbl_OutFlight.Text = Session["OutFlightDtl"].ToString();
                                    Lbl_OutBooking.Text = "Out booking in process.";
                                }
                                else
                                {
                                    lbl_OutFlight.Text = "";
                                    Lbl_OutBooking.Text = "booking in process.";
                                }
                            }
                            else
                            {
                                lbl_OutFlight.Text = "";
                                Lbl_OutBooking.Text = "booking in process.";
                            }
                            if (Session["InFlightDtl"] != null)
                            {
                                if (Session["InFlightDtl"].ToString() != "")
                                {
                                    Lbl_InFlight.Text = Session["InFlightDtl"].ToString();
                                    Lbl_InBooking.Text = "In booking";
                                }
                                else
                                {
                                    Lbl_InFlight.Text = "";
                                    Lbl_InBooking.Text = "booking in process.";
                                }

                            }
                            else
                            {
                                Lbl_InFlight.Text = "";
                                Lbl_InBooking.Text = "booking in process.";
                            }


                            if (Session["Returnbooking"] != null)
                            {
                                Dv_OutBooking.Attributes.Add("style", "background-color: #aliceblue");
                                Dv_InBooking.Attributes.Add("style", "background-color: #5cb85cad");
                                Lbl_OutBooking.Text = "Out booking";
                                Lbl_InBooking.Text = "In booking in process.";
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);
                        
                    }
                    #region PageLoadBinding
                    BindCountryddl();
                    TravelDetail();
                    SaleSummary();
                    MealLcc();
                    FareRules();
                    BaggagesLCC();
                    if (dsSSR.Tables.Contains("SeatPreference"))
                    {
                        SeatPreference();
                    }
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "location.replace('FlightSearch.aspx');", true);

                }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    #region Method
    private void BaggagesLCC()
    {
        try
        {
            if (dsSSR.Tables.Contains("Baggage"))
            {
                //List<Baggage> bObj = new List<Baggage>();
                for (int i = 1; i < dsSSR.Tables["Baggage"].Rows.Count; i++)
                {
                    bObj.Add(new Baggage
                    {
                        Text = dsSSR.Tables["Baggage"].Rows[i]["Weight"].ToString() + "Kg Rs.-" + dsSSR.Tables["Baggage"].Rows[i]["Price"].ToString(),
                        Code = dsSSR.Tables["Baggage"].Rows[i]["Code"].ToString(),
                        WayType = dsSSR.Tables["Baggage"].Rows[i]["WayType"].ToString(),
                        Description = dsSSR.Tables["Baggage"].Rows[i]["Description"].ToString(),
                        Weight = dsSSR.Tables["Baggage"].Rows[i]["Weight"].ToString(),
                        Price = dsSSR.Tables["Baggage"].Rows[i]["Price"].ToString(),
                        Currency = dsSSR.Tables["Baggage"].Rows[i]["Currency"].ToString(),
                        Origin = dsSSR.Tables["Baggage"].Rows[i]["Origin"].ToString(),
                        Destination = dsSSR.Tables["Baggage"].Rows[i]["Destination"].ToString(),
                    });
                }
                dv_ExcessBaggageLcc.Visible = true;
                ddl_ExcessBaggageLcc.DataTextField = "Text";
                ddl_ExcessBaggageLcc.DataValueField = "Code";
                ddl_ExcessBaggageLcc.DataSource = bObj;
                ddl_ExcessBaggageLcc.DataBind();
                Session["Baggage"] = bObj;
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    private void FareRules()
    {
        try
        {
            if (dsFare.Tables.Contains("FareRules"))
            {
                lbl_FareRules.Text = dsFare.Tables["FareRules"].Rows[0]["FareRuleDetail"].ToString();
                Session["FareRule"] = null;
                Session["FareRule"] = dsFare.Tables["FareRules"].Rows[0]["FareRuleDetail"].ToString();
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    private void SeatPreference()
    {
        try
        {
            dv_SeatPreferences.Visible = true;
            ddl_SeatPreferences.DataTextField = "Description";
            ddl_SeatPreferences.DataValueField = "Code";
            ddl_SeatPreferences.DataSource = dsSSR.Tables["SeatPreference"];
            ddl_SeatPreferences.DataBind();
            ddl_SeatPreferences.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    private void MealLcc()
    {
        try
        {
            if (dsSSR.Tables.Contains("MealDynamic"))
            {
                for (int i = 1; i < dsSSR.Tables["MealDynamic"].Rows.Count; i++)
                {
                    mObj.Add(new Meal
                    {
                        Text = dsSSR.Tables["MealDynamic"].Rows[i]["Code"].ToString() + "Rs.-" + dsSSR.Tables["MealDynamic"].Rows[i]["Price"].ToString(),
                        Code = dsSSR.Tables["MealDynamic"].Rows[i]["Code"].ToString(),
                        WayType = dsSSR.Tables["MealDynamic"].Rows[i]["WayType"].ToString(),
                        Description = dsSSR.Tables["MealDynamic"].Rows[i]["Description"].ToString(),
                        AirlineDescription = dsSSR.Tables["MealDynamic"].Rows[i]["AirlineDescription"].ToString(),
                        Quantity = dsSSR.Tables["MealDynamic"].Rows[i]["Quantity"].ToString(),
                        Price = dsSSR.Tables["MealDynamic"].Rows[i]["Price"].ToString(),
                        Currency = dsSSR.Tables["MealDynamic"].Rows[i]["Currency"].ToString(),
                        Origin = dsSSR.Tables["MealDynamic"].Rows[i]["Origin"].ToString(),
                        Destination = dsSSR.Tables["MealDynamic"].Rows[i]["Destination"].ToString(),
                    });
                }
                dv_MealPerferencesLcc.Visible = true;
                ddl_MealPerferencesLcc.DataTextField = "Text";
                ddl_MealPerferencesLcc.DataValueField = "Code";
                ddl_MealPerferencesLcc.DataSource = mObj;
                ddl_MealPerferencesLcc.DataBind();
                Session["MealDynamic"] = mObj;
            }
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

    private DataRow[] CountOnerow;
    private void TravelDetail()
    {
        try
        {
            List<EzulixAir.AirlineSummary> AObj = new List<EzulixAir.AirlineSummary>();
            for (int i = 0; i < ds.Tables["Results"].Rows.Count; i++)
            {
                if (ds.Tables["Results"].Rows[i]["ResultIndex"].ToString() != string.Empty)
                {
                    Session["ResultIndex"] = null;
                    Session["ResultIndex"] = ds.Tables["Results"].Rows[i]["ResultIndex"].ToString();
                    DataRow[] drSegments = ds.Tables["Segments"].Select("Results_Id=" + ds.Tables["Results"].Rows[i]["Results_Id"].ToString());
                    var Segments_Id = drSegments[0]["Segments_Id"];
                    drSegments = ds.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString());
                    var count = ds.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString()).Count();
                    DataTable dtSegments = ds.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString()).CopyToDataTable();
                    CountOnerow = ds.Tables["Airline"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                    var OriginRow = ds.Tables["Origin"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                    var OriginAirportRow = ds.Tables["AirPort"].Select("Origin_Id=" + OriginRow[0]["Origin_Id"].ToString());
                    var DestinationRow = ds.Tables["Destination"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                    var DestinationAirportRow = ds.Tables["AirPort"].Select("Destination_Id=" + DestinationRow[0]["Destination_Id"].ToString());
                    img_Airline.Src = "../flight/Images/flight/" + CountOnerow[0]["AirlineCode"].ToString() + ".gif";
                    lbl_Airlie.Text = CountOnerow[0]["AirlineName"].ToString() + "&nbsp;" + CountOnerow[0]["Airlinecode"].ToString() + "-" + CountOnerow[0]["FlightNumber"].ToString() + "-" + CountOnerow[0]["FareClass"].ToString();
                    lbl_AirlineOrigin.Text = OriginAirportRow[0]["AirPortCode"].ToString() + "&nbsp;" + Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("dd/MM/yyyy") + "&nbsp;|&nbsp;" + Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("HH:mm tt");
                    lbl_AirlineDesignation.Text = DestinationAirportRow[0]["AirPortCode"].ToString() + "&nbsp;" + Convert.ToDateTime(DestinationRow[0]["ArrTime"]).ToString("dd/MM/yyyy") + "&nbsp;|&nbsp;" + Convert.ToDateTime(DestinationRow[0]["ArrTime"]).ToString("HH:mm tt");
                    int duration = 0;
                    string TimeDuration = "";
                    if (count > 1)
                    {
                        for (int j = 0; j < dtSegments.Rows.Count; j++)
                        {
                            if (dtSegments.Rows[j]["AccumulatedDuration"].ToString() != string.Empty)
                            {
                                duration = Convert.ToInt32(dtSegments.Rows[j]["AccumulatedDuration"]);
                            }
                            CountOnerow = ds.Tables["Airline"].Select("Segments_Id=" + dtSegments.Rows[j]["Segments_Id"].ToString());
                            var AirlineOriginRow = ds.Tables["Origin"].Select("Segments_Id=" + dtSegments.Rows[j]["Segments_Id"].ToString());
                            var AirlineOriginAirportRow = ds.Tables["AirPort"].Select("Origin_Id=" + OriginRow[0]["Origin_Id"].ToString());
                            var AirlineDestinationRow = ds.Tables["Destination"].Select("Segments_Id=" + dtSegments.Rows[j]["Segments_Id"].ToString());
                            var AirlineDestinationAirportRow = ds.Tables["AirPort"].Select("Destination_Id=" + AirlineDestinationRow[0]["Destination_Id"].ToString());
                            AObj.Add(new EzulixAir.AirlineSummary
                            {
                                Date = Convert.ToDateTime(AirlineOriginRow[0]["DepTime"]).ToString("dd/MM/yyyy"),
                                FlightNumber = CountOnerow[0]["FlightNumber"].ToString(),
                                Dept = AirlineOriginAirportRow[0]["AirPortCode"].ToString(),
                                DeptTime = Convert.ToDateTime(AirlineOriginRow[0]["DepTime"]).ToString("HH:mm tt"),
                                Arr = AirlineDestinationAirportRow[0]["AirPortCode"].ToString(),
                                ArrTime = Convert.ToDateTime(AirlineDestinationRow[0]["ArrTime"]).ToString("HH:mm tt"),
                            });
                        }
                        int minutes = 0, hours = 0;
                        hours = (duration - duration % 60) / 60;
                        minutes = (duration - hours * 60);
                        if (hours > 0)
                        {
                            if (minutes > 0)
                            {
                                TimeDuration = hours.ToString() + "h, " + minutes.ToString() + "m";
                            }
                            else
                            {
                                TimeDuration = hours.ToString() + "h";
                            }
                        }
                        else
                        {
                            TimeDuration = minutes.ToString() + "m";
                        }
                    }
                    else
                    {
                        duration = Convert.ToInt32(dtSegments.Rows[0]["Duration"]);
                        int minutes = 0, hours = 0;
                        hours = (duration - duration % 60) / 60;
                        minutes = (duration - hours * 60);
                        if (hours > 0)
                        {
                            if (minutes > 0)
                            {
                                TimeDuration = hours.ToString() + "h, " + minutes.ToString() + "m";
                            }
                            else
                            {
                                TimeDuration = hours.ToString() + "h";
                            }
                        }
                        else
                        {
                            TimeDuration = minutes.ToString() + "m";
                        }
                        AObj.Add(new EzulixAir.AirlineSummary
                        {
                            Date = Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("dd/MM/yyyy"),
                            FlightNumber = CountOnerow[0]["FlightNumber"].ToString(),
                            Dept = OriginAirportRow[0]["AirPortCode"].ToString(),
                            DeptTime = Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("HH:mm tt"),
                            Arr = DestinationAirportRow[0]["AirPortCode"].ToString(),
                            ArrTime = Convert.ToDateTime(DestinationRow[0]["ArrTime"]).ToString("HH:mm tt"),
                        });
                    }
                    lbl_AirlineDuration.Text = "Duration" + "&nbsp;" + ":" + TimeDuration;
                }
            }
            dtlist_AirlineSummary.DataSource = AObj;
            dtlist_AirlineSummary.DataBind();
            List<EzulixAir.AirList> obj = (List<EzulixAir.AirList>)Session["AirDataStorage"];
            lbl_BaggageDetails.Text = obj.Where(e => e.Resultid == Session["ResultIndex"].ToString()).Select(e => e.LuggageDetail).SingleOrDefault();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);
            
        }
    }

    private void SaleSummary()
    {
        try
        {
            List<EzulixAir.AirList> Airdata = new List<EzulixAir.AirList>();
            for (int i = 0; i < ds.Tables["Results"].Rows.Count; i++)
            {
                if (ds.Tables["Results"].Rows[i]["ResultIndex"].ToString() != string.Empty)
                {
                    ViewState["AdditionalTxnFeeofrd"] = null;
                    ViewState["AdditionalTxnFeeofrd"] = ds.Tables["Fare"].Rows[i]["AdditionalTxnFeeOfrd"].ToString();
                    ViewState["AdditionalTxnFeePub"] = null;
                    ViewState["AdditionalTxnFeePub"] = ds.Tables["Fare"].Rows[i]["AdditionalTxnFeePub"].ToString();
                    ViewState["OtherCharges"] = null;
                    ViewState["OtherCharges"] = ds.Tables["Fare"].Rows[i]["OtherCharges"].ToString();
                    ViewState["Discount"] = null;
                    ViewState["Discount"] = ds.Tables["Fare"].Rows[i]["Discount"].ToString();
                    ViewState["TdsOnIncentive"] = null;
                    ViewState["TdsOnIncentive"] = ds.Tables["Fare"].Rows[i]["TdsOnIncentive"].ToString();
                    ViewState["ServiceFee"] = null;
                    ViewState["ServiceFee"] = ds.Tables["Fare"].Rows[i]["ServiceFee"].ToString();
                    ViewState["TdsOnCommission"] = null;
                    ViewState["TdsOnCommission"] = ds.Tables["Fare"].Rows[i]["TdsOnCommission"].ToString();
                    ViewState["TdsOnPLB"] = null;
                    ViewState["TdsOnPLB"] = ds.Tables["Fare"].Rows[i]["TdsOnPLB"].ToString();
                    DataRow[] drSegments = ds.Tables["Segments"].Select("Results_Id=" + ds.Tables["Results"].Rows[i]["Results_Id"].ToString());
                    var Segments_Id = drSegments[0]["Segments_Id"];
                    drSegments = ds.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString());
                    var count = ds.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString()).Count();
                    DataTable dtSegments = ds.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString()).CopyToDataTable();
                    DataRow[] CountOnerow = ds.Tables["Airline"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                    var OriginRow = ds.Tables["Origin"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                    var OriginAirportRow = ds.Tables["AirPort"].Select("Origin_Id=" + OriginRow[0]["Origin_Id"].ToString());
                    var DestinationRow = ds.Tables["Destination"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                    var DestinationAirportRow = ds.Tables["AirPort"].Select("Destination_Id=" + DestinationRow[0]["Destination_Id"].ToString());
                    List<EzulixAir.Sale> SObj = new List<EzulixAir.Sale>();
                    string PublishedTS = string.Empty;
                    for (int j = 0; j < ds.Tables["Farebreakdown"].Rows.Count; j++)
                    {
                        if (Math.Round(Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["OtherCharges"].ToString()) - Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["CommissionEarned"].ToString()), MidpointRounding.AwayFromZero) < 0)
                        {
                            PublishedTS = "0";
                        }
                        else
                        {
                            PublishedTS = Math.Round((Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["OtherCharges"]) - Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["CommissionEarned"])) / ds.Tables["Farebreakdown"].Rows.Count, MidpointRounding.AwayFromZero).ToString();
                        }
                        SObj.Add(new EzulixAir.Sale
                        {
                            PaxType = ds.Tables["Farebreakdown"].Rows[j]["PassengerType"].ToString(),
                            PxnCount = ds.Tables["Farebreakdown"].Rows[j]["PassengerCount"].ToString(),
                            BasePublishedPrice = Math.Round(Convert.ToDecimal(ds.Tables["Farebreakdown"].Rows[j]["BaseFare"].ToString()), MidpointRounding.AwayFromZero).ToString(),
                            PublishedOTTax = Math.Round(Convert.ToDecimal(ds.Tables["Farebreakdown"].Rows[j]["Tax"].ToString()) - Convert.ToDecimal(ds.Tables["Farebreakdown"].Rows[j]["YQTax"].ToString()), MidpointRounding.AwayFromZero).ToString(),
                            PublishedYQTax = Math.Round(Convert.ToDecimal(ds.Tables["Farebreakdown"].Rows[j]["YQTax"].ToString()), MidpointRounding.AwayFromZero).ToString(),
                            PublishTS = PublishedTS,
                        });
                    }
                    ViewState["SaleSummarry"] = null;
                    ViewState["SaleSummarry"] = SObj;
                    Session["SaleSummarry"] = SObj;
                    dtlist_SaleSummary.DataSource = SObj;
                    dtlist_SaleSummary.DataBind();
                }
                string pubfee = Math.Round(Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["PublishedFare"].ToString()), MidpointRounding.AwayFromZero).ToString();
                lbl_TotalPayable.Text = Math.Round((Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["PublishedFare"].ToString()) - (Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["CommissionEarned"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["IncentiveEarned"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["PLBEarned"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["AdditionalTxnFeeOfrd"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["AdditionalTxnFeePub"]))) + (Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["TdsOnCommission"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["TdsOnIncentive"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["TdsOnPLB"])), MidpointRounding.AwayFromZero).ToString();
                lbl_Discount.Text = (Convert.ToDecimal(pubfee) - Convert.ToDecimal(lbl_TotalPayable.Text)).ToString();
                //lbl_TotalPayable.Text = Math.Round(Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["PublishedFare"].ToString()) - Convert.ToDecimal(lbl_Discount.Text), MidpointRounding.AwayFromZero).ToString();
                Session["PublishedFare"] = ds.Tables["Fare"].Rows[i]["PublishedFare"].ToString();
                Session["NetPay"] = lbl_TotalPayable.Text;
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    private void BindCountryddl()
    {
        try
        {
            DataTable dtCountry = Cls.select_data_dt(@"EXEC SET_EzulixFlight @Action='C'");
            ddl_Country.DataValueField = "Nationalty";
            ddl_Country.DataTextField = "CountryName";
            ddl_Country.DataSource = dtCountry;
            ddl_Country.DataBind();
            ddl_Country.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);
            
        }
    }

    private void ReseCtrl()
    {
        try
        {
            ddl_PassengerType.ClearSelection();
            ddl_Title.ClearSelection();
            txt_FirstName.Text = string.Empty;
            txt_LastName.Text = string.Empty;
            ddl_Gender.ClearSelection();
            txt_Mobile.Text = string.Empty;
            txt_Dob.Text = string.Empty;
            txt_Address.Text = string.Empty;
            txt_Email.Text = string.Empty;
            ddl_Country.ClearSelection();
            ddl_City.ClearSelection();
            txt_GstNumber.Text = string.Empty;
            txt_GstCompanyName.Text = string.Empty;
            txt_GstCompanyContact.Text = string.Empty;
            txt_GstCompanyAddress.Text = string.Empty;
            txt_GstCompanyEmail.Text = string.Empty;
            txt_CustmerMobile.Text = string.Empty;
            ddl_ExcessBaggageLcc.ClearSelection();
            ddl_SeatPreferences.ClearSelection();
            ddl_MealPerferencesLcc.ClearSelection();
            txt_PassportNo.Text = string.Empty;
            txt_PassportExp.Text = string.Empty;
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);
            
        }
    }
    #endregion

    #region Events
    protected void ddl_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CountryCode = ddl_Country.SelectedItem.Text.Substring(ddl_Country.SelectedItem.Text.IndexOf('(') + 1, 2);
            DataTable dtCity = Cls.select_data_dt(@"EXEC SET_EzulixFlight @Action='CC',@CountryCode='" + CountryCode + "'");
            ddl_City.DataValueField = "CityCode";
            ddl_City.DataTextField = "CityName";
            ddl_City.DataSource = dtCity;
            ddl_City.DataBind();
            ddl_City.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);
            
        }
    }

    protected void chk_GstDetail_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chk_GstDetail.Checked)
            {
                dv_GstDetail.Visible = true;
                rfv_GstNumber.Enabled = true;
                rfv_GstCompanyName.Enabled = true;
                rfv_GstCompanyEmail.Enabled = true;
                rfv_GstCompanyContact.Enabled = true;
                rfv_GstCompanyAddress.Enabled = true;
            }
            else
            {
                dv_GstDetail.Visible = false;
                rfv_GstNumber.Enabled = false;
                rfv_GstCompanyName.Enabled = false;
                rfv_GstCompanyEmail.Enabled = false;
                rfv_GstCompanyContact.Enabled = false;
                rfv_GstCompanyAddress.Enabled = false;
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    protected void chk_PushBookingRoamer_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chk_PushBookingRoamer.Checked)
            {
                dv_PushBookingRoamer.Visible = true;
            }
            else
            {
                dv_PushBookingRoamer.Visible = false;
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    private DateTime Dob;
    private DateTime PassportExp;
    private string GSTCompanyAddress = string.Empty;
    private string GSTCompanyContactNumber = string.Empty;
    private string GSTCompanyName = string.Empty;
    private string GSTNumber = string.Empty;
    private string GSTCompanyEmail = string.Empty;
    private bool IsLeadPaxVal = false;
    protected void btn_AddPaasengerDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string CalBaseFare = string.Empty;
            string PaxCount = string.Empty;
            string CalTax = string.Empty;
            string CalYQTax = string.Empty;
            string CalPublishedFare = string.Empty;
            List<EzulixAir.Sale> Sobj = (List<EzulixAir.Sale>)ViewState["SaleSummarry"];
            if (ViewState["Passenger"] != null)
            {
                PObj = (List<EzulixAir.Passenger>)ViewState["Passenger"];
                int EntryCount = PObj.Count(c => c.PaxType == Convert.ToInt32(ddl_PassengerType.SelectedValue));
                int PassengerType = Convert.ToInt32(ddl_PassengerType.SelectedValue);
                switch (PassengerType)
                {
                    case 1:
                        if (Convert.ToInt32(Session["AdultsCount"]) <= EntryCount)
                        {
                            ReseCtrl();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + "Adult Count details filled more then Selected" + "');location.replace('FlightSearch.aspx');", true);

                        }
                        break;
                    case 2:
                        if (Convert.ToInt32(Session["ChildCount"]) <= EntryCount)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + "Child Count details filled more then Selected" + "');location.replace('FlightSearch.aspx');", true);

                        }
                        break;
                    case 3:
                        if (Convert.ToInt32(Session["InfantCount"]) <= EntryCount)
                        {
                            ReseCtrl();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + "Infant Count details filled more then Selected" + "');location.replace('FlightSearch.aspx');", true);

                        }
                        break;
                }
            }
            int PassengerTypeS = Convert.ToInt32(ddl_PassengerType.SelectedValue);
            //if (ViewState["Passenger"] == null && PassengerTypeS == 1)
            //{
            //    IsLeadPaxVal = true;
            //}
            switch (PassengerTypeS)
            {
                case 1:
                    CalBaseFare = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.BasePublishedPrice).SingleOrDefault();
                    PaxCount = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.PxnCount).SingleOrDefault();
                    if (CalBaseFare != "0")
                    {
                        CalBaseFare = (Convert.ToInt32(CalBaseFare) / Convert.ToInt32(PaxCount)).ToString();
                    }
                    CalTax = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.PublishedOTTax).SingleOrDefault();
                    if (CalTax != "0")
                    {
                        CalTax = (Convert.ToInt32(CalTax) / Convert.ToInt32(PaxCount)).ToString();
                    }
                    CalYQTax = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.PublishedYQTax).SingleOrDefault();
                    if (CalYQTax != "0")
                    {
                        CalYQTax = (Convert.ToInt32(CalYQTax) / Convert.ToInt32(PaxCount)).ToString();
                    }
                    CalPublishedFare = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.TotalPubPrice).SingleOrDefault();
                    break;
                case 2:
                    CalBaseFare = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.BasePublishedPrice).SingleOrDefault();
                    PaxCount = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.PxnCount).SingleOrDefault();
                    if (CalBaseFare != "0")
                    {
                        CalBaseFare = (Convert.ToInt32(CalBaseFare) / Convert.ToInt32(PaxCount)).ToString();
                    }
                    CalTax = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.PublishedOTTax).SingleOrDefault();
                    if (CalTax != "0")
                    {
                        CalTax = (Convert.ToInt32(CalTax) / Convert.ToInt32(PaxCount)).ToString();
                    }
                    CalYQTax = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.PublishedYQTax).SingleOrDefault();
                    if (CalYQTax != "0")
                    {
                        CalYQTax = (Convert.ToInt32(CalYQTax) / Convert.ToInt32(PaxCount)).ToString();
                    }
                    CalPublishedFare = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.TotalPubPrice).SingleOrDefault();
                    break;
                case 3:
                    CalBaseFare = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.BasePublishedPrice).SingleOrDefault();
                    PaxCount = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.PxnCount).SingleOrDefault();
                    if (CalBaseFare != "0")
                    {
                        CalBaseFare = (Convert.ToInt32(CalBaseFare) / Convert.ToInt32(PaxCount)).ToString();
                    }
                    CalTax = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.PublishedOTTax).SingleOrDefault();
                    if (CalTax != "0")
                    {
                        CalTax = (Convert.ToInt32(CalTax) / Convert.ToInt32(PaxCount)).ToString();
                    }
                    CalYQTax = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.PublishedYQTax).SingleOrDefault();
                    if (CalYQTax != "0")
                    {
                        CalYQTax = (Convert.ToInt32(CalYQTax) / Convert.ToInt32(PaxCount)).ToString();
                    }
                    CalPublishedFare = Sobj.Where(s => s.PaxType == PassengerTypeS.ToString()).Select(s => s.TotalPubPrice).SingleOrDefault();
                    break;
            }
            if (txt_Dob.Text.Trim() != string.Empty)
            {
                Dob = Convert.ToDateTime(txt_Dob.Text.Trim().ToString());

            }
            if (txt_PassportExp.Text.Trim() != string.Empty)
            {
                PassportExp = Convert.ToDateTime(txt_PassportExp.Text.Trim().ToString());

            }
            if (chk_GstDetail.Checked)
            {
                GSTCompanyAddress = txt_GstCompanyAddress.Text.Trim();
                GSTCompanyContactNumber = txt_GstCompanyContact.Text.Trim();
                GSTCompanyName = txt_GstCompanyName.Text.Trim();
                GSTNumber = txt_GstCompanyAddress.Text.Trim();
                GSTCompanyEmail = txt_GstCompanyEmail.Text.Trim();
            }
            EzulixAir.Fare FObj = new EzulixAir.Fare
            {
                Currency = "INR",
                BaseFare = Convert.ToDouble(CalBaseFare),
                Tax = Convert.ToDouble(CalTax),
                YQTax = Convert.ToDouble(CalYQTax),
                AdditionalTxnFeePub = Convert.ToDouble(ViewState["AdditionalTxnFeePub"]),
                AdditionalTxnFeeOfrd = Convert.ToDouble(ViewState["AdditionalTxnFeeofrd"]),
                OtherCharges = Convert.ToDouble(ViewState["OtherCharges"]),
                Discount = Convert.ToDouble(ViewState["Discount"]),
                PublishedFare = Convert.ToDouble(CalPublishedFare),
                OfferedFare = Convert.ToDouble(lbl_TotalPayable.Text.Trim()),
                TdsOnCommission = Convert.ToDouble(ViewState["TdsOnCommission"]),
                TdsOnPLB = Convert.ToDouble(ViewState["TdsOnPLB"]),
                TdsOnIncentive = Convert.ToDouble(ViewState["TdsOnIncentive"]),
                ServiceFee = Convert.ToDouble(ViewState["ServiceFee"]),
            };
            if (Session["MealDynamic"] != null && Session["Baggage"] != null)
            {
                mObj = (List<Meal>)Session["MealDynamic"];
                var meald = mObj.Where(m => m.Code == ddl_MealPerferencesLcc.SelectedValue).FirstOrDefault();

                mapiObj.Add(new EzulixAir.MealDynamic
                {
                    WayType = meald.WayType,
                    Code = meald.Code,
                    Description = meald.Description,
                    AirlineDescription = meald.AirlineDescription,
                    Quantity = meald.Quantity,
                    Price = meald.Price,
                    Currency = meald.Currency,
                    Origin = meald.Origin,
                    Destination = meald.Destination,
                });
                //}

                bObj = (List<Baggage>)Session["Baggage"];
                var Baggage = bObj.Where(m => m.Code == ddl_ExcessBaggageLcc.SelectedValue).FirstOrDefault();
                bapiObj.Add(new EzulixAir.Baggage
                {
                    WayType = Baggage.WayType,
                    Code = Baggage.Code,
                    Description = Baggage.Description,
                    Weight = Baggage.Weight,
                    Price = Baggage.Price,
                    Currency = Baggage.Currency,
                    Origin = Baggage.Origin,
                    Destination = Baggage.Destination,
                });
                if (PObj.Count() == 0)
                {
                    PObj.Add(new EzulixAir.Passenger
                    {
                        Title = ddl_Title.SelectedItem.Text,
                        FirstName = txt_FirstName.Text.Trim(),
                        LastName = txt_LastName.Text.Trim(),
                        PaxType = int.Parse(ddl_PassengerType.SelectedValue),
                        DateOfBirth = Dob,
                        Gender = int.Parse(ddl_Gender.SelectedValue),
                        PassportNo = txt_PassportNo.Text.Trim(),
                        PassportExpiry = PassportExp,
                        AddressLine1 = txt_Address.Text.Trim(),
                        //AddressLine2====Not required
                        Fare = FObj,
                        City = ddl_City.SelectedItem.Text,
                        CountryCode = ddl_Country.SelectedItem.Text.Substring(ddl_Country.SelectedItem.Text.IndexOf('(') + 1, 2),
                        CountryName = ddl_Country.SelectedItem.Text,
                        Nationality = ddl_Country.SelectedValue,
                        ContactNo = txt_Mobile.Text.Trim(),
                        Email = txt_Email.Text.Trim(),
                        IsLeadPax = true,// ====== Discus to Ezulix and TBO
                                         //FFAirlineCode ===== Not required
                                         //FFNumber ===== Not required
                        GSTCompanyAddress = GSTCompanyAddress,
                        GSTCompanyContactNumber = GSTCompanyContactNumber,
                        GSTCompanyName = GSTCompanyName,
                        GSTNumber = GSTNumber,
                        GSTCompanyEmail = GSTCompanyEmail,
                        Baggage = bapiObj,
                        MealDynamic = mapiObj,
                    }

                     );
                }
                else
                {

                    PObj.Add(new EzulixAir.Passenger
                    {
                        Title = ddl_Title.SelectedItem.Text,
                        FirstName = txt_FirstName.Text.Trim(),
                        LastName = txt_LastName.Text.Trim(),
                        PaxType = int.Parse(ddl_PassengerType.SelectedValue),
                        DateOfBirth = Dob,
                        Gender = int.Parse(ddl_Gender.SelectedValue),
                        PassportNo = txt_PassportNo.Text.Trim(),
                        PassportExpiry = PassportExp,
                        AddressLine1 = txt_Address.Text.Trim(),
                        //AddressLine2====Not required
                        Fare = FObj,
                        City = ddl_City.SelectedItem.Text,
                        CountryCode = ddl_Country.SelectedItem.Text.Substring(ddl_Country.SelectedItem.Text.IndexOf('(') + 1, 2),
                        CountryName = ddl_Country.SelectedItem.Text,
                        Nationality = ddl_Country.SelectedValue,
                        ContactNo = txt_Mobile.Text.Trim(),
                        Email = txt_Email.Text.Trim(),
                        IsLeadPax = false,// ====== Discus to Ezulix and TBO
                                          //FFAirlineCode ===== Not required
                                          //FFNumber ===== Not required
                        GSTCompanyAddress = GSTCompanyAddress,
                        GSTCompanyContactNumber = GSTCompanyContactNumber,
                        GSTCompanyName = GSTCompanyName,
                        GSTNumber = GSTNumber,
                        GSTCompanyEmail = GSTCompanyEmail,
                        Baggage = bapiObj,
                        MealDynamic = mapiObj,
                    }
                );
                }
            }
            else
            {

                if (PObj.Count() == 0)
                {
                    PObj.Add(new EzulixAir.Passenger
                    {
                        Title = ddl_Title.SelectedItem.Text,
                        FirstName = txt_FirstName.Text.Trim(),
                        LastName = txt_LastName.Text.Trim(),
                        PaxType = int.Parse(ddl_PassengerType.SelectedValue),
                        DateOfBirth = Dob,
                        Gender = int.Parse(ddl_Gender.SelectedValue),
                        PassportNo = txt_PassportNo.Text.Trim(),
                        PassportExpiry = PassportExp,
                        AddressLine1 = txt_Address.Text.Trim(),
                        //AddressLine2====Not required
                        Fare = FObj,
                        City = ddl_City.SelectedItem.Text,
                        CountryCode = ddl_Country.SelectedItem.Text.Substring(ddl_Country.SelectedItem.Text.IndexOf('(') + 1, 2),
                        CountryName = ddl_Country.SelectedItem.Text,
                        Nationality = ddl_Country.SelectedValue,
                        ContactNo = txt_Mobile.Text.Trim(),
                        Email = txt_Email.Text.Trim(),
                        IsLeadPax = true,// ====== Discus to Ezulix and TBO
                                         //FFAirlineCode ===== Not required
                                         //FFNumber ===== Not required
                        GSTCompanyAddress = GSTCompanyAddress,
                        GSTCompanyContactNumber = GSTCompanyContactNumber,
                        GSTCompanyName = GSTCompanyName,
                        GSTNumber = GSTNumber,
                        GSTCompanyEmail = GSTCompanyEmail,

                    }

                     );
                }
                else
                {
                    PObj.Add(new EzulixAir.Passenger
                    {
                        Title = ddl_Title.SelectedItem.Text,
                        FirstName = txt_FirstName.Text.Trim(),
                        LastName = txt_LastName.Text.Trim(),
                        PaxType = int.Parse(ddl_PassengerType.SelectedValue),
                        DateOfBirth = Dob,
                        Gender = int.Parse(ddl_Gender.SelectedValue),
                        PassportNo = txt_PassportNo.Text.Trim(),
                        PassportExpiry = PassportExp,
                        AddressLine1 = txt_Address.Text.Trim(),
                        //AddressLine2====Not required
                        Fare = FObj,
                        City = ddl_City.SelectedItem.Text,
                        CountryCode = ddl_Country.SelectedItem.Text.Substring(ddl_Country.SelectedItem.Text.IndexOf('(') + 1, 2),
                        CountryName = ddl_Country.SelectedItem.Text,
                        Nationality = ddl_Country.SelectedValue,
                        ContactNo = txt_Mobile.Text.Trim(),
                        Email = txt_Email.Text.Trim(),
                        IsLeadPax = false,// ====== Discus to Ezulix and TBO
                                          //FFAirlineCode ===== Not required
                                          //FFNumber ===== Not required
                        GSTCompanyAddress = GSTCompanyAddress,
                        GSTCompanyContactNumber = GSTCompanyContactNumber,
                        GSTCompanyName = GSTCompanyName,
                        GSTNumber = GSTNumber,
                        GSTCompanyEmail = GSTCompanyEmail,

                    }
                    );
                }
                //}
            }


            ViewState["Passenger"] = PObj;
            ReseCtrl();
            if (PObj.Count() == Convert.ToInt32(Session["TotalPax"].ToString()))
            {
                btn_ProceedBooking_Click(sender, e);
            }
            else
            {
                AjaxMessageBox(this, "Saved successfully");
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);

            //int pxfilled = PObj.Count();
            //pxfilled = (pxfilled - 1);
            //PObj.Count() = pxfilled;
            
        }
    }
    //public string changedatetommddyy(string ddmmyy)
    //{
    //    string yymmdd = "";
    //    yymmdd = yymmdd.Substring(3, 2) + "-" + yymmdd.Substring(0, 2) + "-" + yymmdd.Substring(6, 4);
    //    return yymmdd;
    //}
    public static string GetIPAddress()
    {
        string Requestipaddress;
        Requestipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (Requestipaddress == "" || Requestipaddress == null)
            Requestipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        return Requestipaddress;
    }
    protected void btn_ProceedBooking_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["MemberIdLog"] != null && Session["MsrNoLog"] != null)
            {
                List<EzulixAir.Passenger> Obj = (List<EzulixAir.Passenger>)ViewState["Passenger"];
                double totalBaggageAmount = 0;

                for (int i = 0; i < Obj.Count; i++)
                {
                    for (int k = 0; k < bapiObj.Count; k++)
                    {
                        List<EzulixAir.Baggage> obj1 = Obj[i].Baggage.ToList();
                        for (int j = 0; j < obj1.Count; j++)
                        {
                            totalBaggageAmount += Convert.ToDouble(obj1[j].Price);
                        }
                    }
                }
                double totalMealAmount = 0;
                for (int i = 0; i < Obj.Count; i++)
                {
                    for (int k = 0; k < mapiObj.Count; k++)
                    {
                        List<EzulixAir.MealDynamic> obj1 = Obj[i].MealDynamic.ToList();
                        for (int j = 0; j < obj1.Count; j++)
                        {
                            totalMealAmount += Convert.ToDouble(obj1[j].Price);
                        }
                    }
                }

                double publishfare = Convert.ToDouble(Session["PublishedFare"].ToString());
                double totalpublishwithBM = totalBaggageAmount + totalMealAmount + publishfare;
                decimal PublishFee = Convert.ToDecimal(totalpublishwithBM);
                double NetPay = Convert.ToDouble(Session["NetPay"].ToString());
                decimal NetPayable = Convert.ToDecimal(totalBaggageAmount + totalMealAmount + NetPay);


                string TokenId = EzulixAir.Air_TokenId;
                if (TokenId == string.Empty)
                {
                    eAir.GetTokenId();
                    TokenId = EzulixAir.Air_TokenId;
                }
                List<EzulixAir.BookRequest> BObj = new List<EzulixAir.BookRequest>
            {
                new EzulixAir.BookRequest
                {
                    ResultIndex=Session["ResultIndex"].ToString(),
                    Passengers=Obj,
                    EndUserIp="172.107.166.241",
                    TokenId=TokenId,
                    TraceId=Session["Traceid"].ToString(),
                }
            };
                string Json = JsonConvert.SerializeObject(BObj);
                if (Session["IsLCC"].ToString() != null)
                {
                    cls_myMember clsm = new cls_myMember();
                    string agentid = clsm.Cyrus_GetTransactionID_New();
                    Session["agentid"] = agentid;
                    Session["FlightFee"] = PublishFee;
                    Session["NetPayable"] = NetPayable;
                    if (Session["IsLCC"].ToString() == "true")
                    {
                        List<EzulixAir.BookRequest> BObjj = new List<EzulixAir.BookRequest>
                  {
                   new EzulixAir.BookRequest
                   {
                    ResultIndex=Session["ResultIndex"].ToString(),
                    EndUserIp="172.107.166.241",
                    TokenId=TokenId,
                    TraceId=Session["Traceid"].ToString(),
                    Passengers=Obj
                   }
                  };
                        string Jsonn = JsonConvert.SerializeObject(BObjj);
                        Session["Baggage"] = null;
                        Session["MealDynamic"] = null;
                        string Result = string.Empty;
                        string OperationName = string.Empty;
                        string resu = string.Empty;
                        // int chkbal = 1;
                        int chkbal = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(PublishFee), Convert.ToInt32(Session["MsrNoLog"].ToString()));
                        if (chkbal == 1)
                        {
                            //  int tra = 1;
                            int tra = clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal("-" + PublishFee), "Dr", "FlightPublishedFee:- '" + agentid + "'");
                            if (tra > 0)
                            {
                                var msrno = Session["MsrNoLog"].ToString();
                                Resultt = eAir.FlightTicket(Jsonn.Substring(1, (Jsonn.Length) - 1), PublishFee, agentid, NetPayable, msrno);
                                if (Resultt != string.Empty)
                                {
                                    Session["BookingRespone"] = null;
                                    DataSet dss = eAir.Deserialize(Resultt);
                                    Session["BookingRespone"] = dss;
                                    Session["Ticket"] = null;
                                    Session["Ticket"] = dss;
                                    if (dss.Tables.Contains("Response"))
                                    {
                                        if (dss.Tables["Response"].Rows.Count > 0)
                                        {
                                            string status = "";
                                            for (int i = 0; i < dss.Tables["Response"].Columns.Count; i++)
                                            {
                                                if (dss.Tables["Response"].Columns[i].ColumnName == "TicketStatus")
                                                {
                                                    status = "TicketStatusExist";
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

                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + dss.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);

                                                    }
                                                    break;
                                                }
                                            }
                                            if (status == "")
                                            {
                                                clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal(PublishFee), "Cr", "Flight Fail:- '" + agentid + "'");
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + "Flight Fail!" + "');location.replace('FlightSearch.aspx');", true);
                                            }
                                        }
                                        else
                                        {
                                            clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal(PublishFee), "Cr", "Flight Empty Result:- '" + agentid + "'");
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + dss.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);

                                        }
                                    }
                                    else
                                    {
                                        clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal(PublishFee), "Cr", "Flight Empty Result:- '" + agentid + "'");
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + Resultt + "');location.replace('FlightSearch.aspx');", true);
                                    }
                                }
                                else
                                {
                                    clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberIdLog"].ToString()), Convert.ToDecimal(PublishFee), "Cr", "Flight Empty Result:- '" + agentid + "'");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + "Empty Result!" + "');location.replace('FlightSearch.aspx');", true);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + "Transaction Unsuccessfull!" + "');location.replace('FlightSearch.aspx');", true);

                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + "Insufficient Wallet Balance!" + "');location.replace('FlightSearch.aspx');", true);
                        }
                    }
                    else
                    {


                        string msrno = Session["MsrNoLog"].ToString();
                        Result = eAir.FlightBook(Json.Substring(1, (Json.Length) - 1), msrno, PublishFee, NetPayable);

                        if (Result != string.Empty)
                        {
                            DataSet ds = eAir.Deserialize(Result);
                            if (ds.Tables.Contains("Response"))
                            {
                                if (ds.Tables["Error"].Rows[0]["ErrorCode"].ToString() == "2" && ds.Tables["Response"].Rows[0]["ResponseStatus"].ToString() == "2" && ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() == "")
                                {
                                    Session["Ticket"] = null;
                                    Session["Ticket"] = ds;

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.location ='FlightBookingConfirmation.aspx';", true);
                                }
                                else
                                {
                                    Session["BookingRespone"] = null;
                                    Session["BookingRespone"] = ds;
                                    if (ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() == "Your session (TraceId) is expired.")
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);
                                       
                                    }
                                    else
                                    {
                                        if (ds.Tables["Error"].Rows[0]["ErrorCode"].ToString() == "0")
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.location ='FlightReviewBooking.aspx';", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);
                                            
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + Result + "');location.replace('FlightSearch.aspx');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('Empty Result from Book API');location.replace('FlightSearch.aspx');", true);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Login Session Empty!');location.replace('FlightSearch.aspx');", true);
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);
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

    public string ConvertDataTabletoJson(DataTable dt)
    {
        return JsonConvert.SerializeObject(dt);
    }

    protected void ReturnError(string message, string operationName)
    {
        try
        {
            DataTable dt = Cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
            string output = ConvertDataTabletoJson(dt);
            Response.Write("{ " + operationName + ":" + output + "}");
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    protected void ddl_PassengerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_PassengerType.SelectedValue == "2" || ddl_PassengerType.SelectedValue == "3")
            {
                dv_Passport.Visible = true;
                dv_Email.Visible = false;
                rfv_Email.Enabled = false;
                rgex_Email.Enabled = false;
                dv_Mobile.Visible = false;
                rfv_Mobile.Enabled = false;
            }
            else
            {
                dv_Passport.Visible = true;
                dv_Email.Visible = true;
                rfv_Email.Enabled = true;
                rgex_Email.Enabled = true;
                dv_Mobile.Visible = true;
                rfv_Mobile.Enabled = true;
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    #endregion


    #region PropertiesClass
    public class Ticket
    {
        public string TicketId { get; set; }

    }
    public class Baggage
    {
        public string Text { get; set; }
        public string Code { get; set; }
        public string WayType { get; set; }

        public string Description { get; set; }
        public string Weight { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }

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


    #region LccFlightRequest

    #endregion
    public class RootobjectLcc
    {
        public object PreferredCurrency { get; set; }
        public string ResultIndex { get; set; }
        public string AgentReferenceNo { get; set; }
        public PassengerLcc[] Passengers { get; set; }
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
    }

    public class PassengerLcc
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
        public FareLcc Fare { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public bool IsLeadPax { get; set; }
        public string FFAirlineCode { get; set; }
        public string FFNumber { get; set; }
        public BaggageLcc[] Baggage { get; set; }
        public MealdynamicLcc[] MealDynamic { get; set; }
        public object[] SeatDynamic { get; set; }
        public string GSTCompanyAddress { get; set; }
        public string GSTCompanyContactNumber { get; set; }
        public string GSTCompanyName { get; set; }
        public string GSTNumber { get; set; }
        public string GSTCompanyEmail { get; set; }
    }

    public class FareLcc
    {
        public float BaseFare { get; set; }
        public float Tax { get; set; }
        public float YQTax { get; set; }
        public float AdditionalTxnFeePub { get; set; }
        public float AdditionalTxnFeeOfrd { get; set; }
        public float OtherCharges { get; set; }
    }

    public class BaggageLcc
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

    public class MealdynamicLcc
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

    protected void txt_LastName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["Sourcename"] != null)
            {
                string Sourcename = Session["Sourcename"].ToString();
                if (Sourcename == "TruJet" || Sourcename == "Zoom Air")
                {
                    string[] lastname = txt_LastName.Text.ToString().Split(' ');
                    if (lastname.Length > 1)
                    {
                        txt_LastName.Text = string.Empty;
                        AjaxMessageBox(this, "Space in Last Name not allowed in case of Zoom Air or Tru Jet Source!");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.location ='FlightPassengerDetails.aspx';", true);
                    }

                }
                if (Sourcename == "SpiceJet")
                {
                    string firstname = txt_FirstName.Text.ToString();
                    string lasttname = txt_LastName.Text.ToString();
                    if (firstname == lasttname)
                    {
                        AjaxMessageBox(this, " In case of Spice Jet Passenger Name(First + Last) must be distinct");

                        txt_LastName.Text = string.Empty;
                        txt_FirstName.Text = string.Empty;

                    }
                }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

}