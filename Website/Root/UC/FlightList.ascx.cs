using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FlightList : System.Web.UI.UserControl
{
    #region Properties
    public static int JourneyType;
    private EzulixAir eAir = new EzulixAir();
    private string Result = string.Empty;
    private string Result2 = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            mpe_ModifySearch.Hide();
            if (!IsPostBack)
            {
                if (Session["FlightList"] != null)
                {
                    JourneyType = Convert.ToInt32(Session["JourneyType"]);
                    if (JourneyType == 2)
                    {
                        dv_ReturnBooking.Visible = true;
                        dv_ReturnHeader.Visible = true;
                        acc_ReturnFilters.Visible = true;
                    }
                    if (Session["SearchCriteria"] != null)
                    {
                        lbl_SearchCriteria.Text = Session["SearchCriteria"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Search Criteria Session');location.replace('FlightSearch.aspx');", true);
                    }
                    GetFlightList();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.location ='FlightSearch.aspx';", true);
                }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    #region Method
    private void GetFlightList()
    {
        try
        {
            List<EzulixAir.AirList> Airdata = new List<EzulixAir.AirList>();
            List<EzulixAir.AirList> RetrunAirdata = new List<EzulixAir.AirList>();
            StringBuilder strLuggageDetail = new StringBuilder();
            if (Session["FlightList"] != null)
            {
                DataSet dsAirLine = (DataSet)Session["FlightList"];
                Session["Traceid"] = null;
                Session["Traceid"] = dsAirLine.Tables["Response"].Rows[0]["TraceId"].ToString();
                for (int i = 0; i < dsAirLine.Tables["Results"].Rows.Count; i++)
                {
                    if (dsAirLine.Tables["Results"].Rows[i]["ResultIndex"].ToString() != string.Empty && dsAirLine.Tables["Results"].Rows[i]["Results_id_0"].ToString() == "0")
                    {
                        DataRow[] drSegments = dsAirLine.Tables["Segments"].Select("Results_Id=" + dsAirLine.Tables["Results"].Rows[i]["Results_Id"].ToString());
                        var Segments_Id = drSegments[0]["Segments_Id"];
                        drSegments = dsAirLine.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString());
                        var count = dsAirLine.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString()).Count();
                        DataTable dtSegments = dsAirLine.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString()).CopyToDataTable();
                        DataRow[] CountOnerow = dsAirLine.Tables["Airline"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                        var OriginRow = dsAirLine.Tables["Origin"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                        var OriginAirportRow = dsAirLine.Tables["AirPort"].Select("Origin_Id=" + OriginRow[0]["Origin_Id"].ToString());
                        var DestinationRow = dsAirLine.Tables["Destination"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                        var DestinationAirportRow = dsAirLine.Tables["AirPort"].Select("Destination_Id=" + DestinationRow[0]["Destination_Id"].ToString());
                        string TimeDuration = "";
                        StringBuilder strStopDetail = new StringBuilder();
                        StringBuilder strOriginDetail = new StringBuilder();
                        StringBuilder strDestinationDetail = new StringBuilder();
                        strLuggageDetail.Clear();
                        strStopDetail.Clear();
                        strStopDetail.Clear();
                        strDestinationDetail.Clear();
                        if (count > 1)
                        {
                            strLuggageDetail.Append("<table style='width:100%'>");
                            strLuggageDetail.Append("<thead>");
                            strLuggageDetail.Append("<tr>");
                            strLuggageDetail.Append("<th>Sector</th>");
                            strLuggageDetail.Append("<th>Cabin</th>");
                            strLuggageDetail.Append("<th>Check-in</th>");
                            strLuggageDetail.Append("<th>Operatedby</th>");
                            strLuggageDetail.Append("</tr>");
                            strLuggageDetail.Append("</thead>");
                            strLuggageDetail.Append("<tbody>");
                            int duration = 0;
                            for (int j = 0; j < dtSegments.Rows.Count; j++)
                            {
                                if (dtSegments.Rows[j]["AccumulatedDuration"].ToString() != string.Empty)
                                {
                                    duration = Convert.ToInt32(dtSegments.Rows[j]["AccumulatedDuration"]);
                                }
                                DataRow[] StopDetailRow = dsAirLine.Tables["Airline"].Select("Segments_Id=" + dtSegments.Rows[j]["Segments_Id"].ToString());
                                var OriginStopRow = dsAirLine.Tables["Origin"].Select("Segments_Id=" + dtSegments.Rows[j]["Segments_Id"].ToString());
                                var OriginStopAirportRow = dsAirLine.Tables["AirPort"].Select("Origin_Id=" + OriginStopRow[0]["Origin_Id"].ToString());
                                var DestinationStopRow = dsAirLine.Tables["Destination"].Select("Segments_Id=" + dtSegments.Rows[j]["Segments_Id"].ToString());
                                var DestinationStopAirportRow = dsAirLine.Tables["AirPort"].Select("Destination_Id=" + DestinationStopRow[0]["Destination_Id"].ToString());
                                strStopDetail.Append("" + StopDetailRow[0]["AirlineCode"].ToString() + "-");
                                strStopDetail.Append("" + StopDetailRow[0]["FlightNumber"].ToString() + "");
                                strStopDetail.Append("&nbsp;" + StopDetailRow[0]["FareClass"].ToString() + "");
                                strStopDetail.Append("&emsp;" + OriginStopAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(OriginStopRow[0]["DepTime"]).ToString("HH:mm tt") + ")&nbsp;&nbsp;<img src='/flight/Images/flight/arrow.gif' runat='server' style='float:right; width: 27px; height: 27px;'></img>");
                                strStopDetail.Append("&nbsp;" + DestinationStopAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(DestinationStopRow[0]["ArrTime"]).ToString("HH:mm tt") + ")");
                                strStopDetail.Append("</br>");
                                strOriginDetail.Append("" + OriginStopAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(OriginStopRow[0]["DepTime"]).ToString("HH:mm tt") + ")&nbsp;&nbsp;<img src='/flight/Images/flight/arrow.gif' runat='server' style='float:right; width: 27px; height: 27px;'></img></br>");
                                strDestinationDetail.Append("&nbsp;" + DestinationStopAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(DestinationStopRow[0]["ArrTime"]).ToString("HH:mm tt") + ")</br>");
                                #region LuggageBindingTwo
                                strLuggageDetail.Append("<tr>");
                                strLuggageDetail.Append("<td>" + OriginStopAirportRow[0]["AirPortCode"].ToString() + "- " + DestinationStopAirportRow[0]["AirPortCode"].ToString() + "</td>");
                                strLuggageDetail.Append("<td>" + dtSegments.Rows[j]["CabinBaggage"].ToString() + "</td>");
                                strLuggageDetail.Append("<td>" + dtSegments.Rows[j]["Baggage"].ToString() + "</td>");
                                strLuggageDetail.Append("<td>" + StopDetailRow[0]["OperatingCarrier"].ToString() + "</td>");
                                strLuggageDetail.Append("</tr>");
                                #endregion
                            }
                            strLuggageDetail.Append("</tbody>");
                            strLuggageDetail.Append("</table>");
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
                            int duration = Convert.ToInt32(dtSegments.Rows[0]["Duration"]);
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
                            strOriginDetail.Append("" + OriginAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("HH:mm tt") + ")&nbsp;&nbsp;<img src='/flight/Images/flight/arrow.gif' runat='server' style='float:right; width: 27px; height: 27px;'></img></br>");
                            strDestinationDetail.Append("&nbsp;" + DestinationAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(DestinationRow[0]["ArrTime"]).ToString("HH:mm tt") + ")</br>");
                            #region LuggageBindingOne
                            strLuggageDetail.Append("<table style='width:100%'>");
                            strLuggageDetail.Append("<thead>");
                            strLuggageDetail.Append("<tr>");
                            strLuggageDetail.Append("<th>Sector</th>");
                            strLuggageDetail.Append("<th>Cabin</th>");
                            strLuggageDetail.Append("<th>Check-in</th>");
                            strLuggageDetail.Append("<th>Operatedby</th>");
                            strLuggageDetail.Append("</tr>");
                            strLuggageDetail.Append("</thead>");
                            strLuggageDetail.Append("<tbody>");
                            strLuggageDetail.Append("<tr>");
                            strLuggageDetail.Append("<td>" + OriginAirportRow[0]["AirPortCode"].ToString() + "- " + DestinationAirportRow[0]["AirPortCode"].ToString() + "</td>");
                            strLuggageDetail.Append("<td>" + dtSegments.Rows[0]["CabinBaggage"].ToString() + "</td>");
                            strLuggageDetail.Append("<td>" + dtSegments.Rows[0]["Baggage"].ToString() + "</td>");
                            strLuggageDetail.Append("<td>" + CountOnerow[0]["OperatingCarrier"].ToString() + "</td>");
                            strLuggageDetail.Append("</tr>");
                            strLuggageDetail.Append("</tbody>");
                            strLuggageDetail.Append("</table>");
                            #endregion
                        }
                        for (int j = 0; j < dtSegments.Columns.Count; j++)
                        {
                            if (dtSegments.Columns[j].ColumnName == "NoOfSeatAvailable")
                            {
                                Airdata.Add(new EzulixAir.AirList
                                {
                                    AirlineCode = CountOnerow[0]["AirlineCode"].ToString(),
                                    AirlineName = CountOnerow[0]["AirlineName"].ToString(),
                                    FlightNumber = CountOnerow[0]["FlightNumber"].ToString(),
                                    FareClass = CountOnerow[0]["FareClass"].ToString(),
                                    OriginTime = Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("HH:mm tt"),
                                    Origin = OriginAirportRow[0]["AirPortCode"].ToString(),
                                    DestinationTime = Convert.ToDateTime(DestinationRow[0]["ArrTime"]).ToString("HH:mm tt"),
                                    Destination = DestinationAirportRow[0]["AirPortCode"].ToString(),
                                    Duration = TimeDuration,

                                    NoofSeatAvailable = dtSegments.Rows[0]["NoofSeatAvailable"].ToString(),
                                    PubPrice = Math.Round(Convert.ToDecimal(dsAirLine.Tables["Fare"].Rows[i - 1]["PublishedFare"].ToString()), MidpointRounding.AwayFromZero),
                                    StopCount = count - 1,
                                    StopDetail = strStopDetail.ToString(),
                                    OriginDetail = strOriginDetail.ToString(),
                                    DestinationDetail = strDestinationDetail.ToString(),
                                    AirllineFullName = CountOnerow[0]["AirlineName"].ToString() + "(" + CountOnerow[0]["AirlineCode"].ToString() + ")",
                                    IsRefundable = Convert.ToBoolean(dsAirLine.Tables["Results"].Rows[i]["IsRefundable"]),
                                    LuggageDetail = strLuggageDetail.ToString(),
                                    Resultid = dsAirLine.Tables["Results"].Rows[i]["ResultIndex"].ToString(),
                                    IsLCC = dsAirLine.Tables["Results"].Rows[i]["IsLCC"].ToString(),
                                });

                            }
                        }
                    }
                    else if (dsAirLine.Tables["Results"].Rows[i]["Results_id_0"].ToString() != string.Empty)
                    {
                        DataRow[] drSegments = dsAirLine.Tables["Segments"].Select("Results_Id=" + dsAirLine.Tables["Results"].Rows[i]["Results_Id"].ToString());
                        var Segments_Id = drSegments[0]["Segments_Id"];
                        drSegments = dsAirLine.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString());
                        var count = dsAirLine.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString()).Count();
                        DataTable dtSegments = dsAirLine.Tables["Segments"].Select("Segments_Id_0=" + Segments_Id.ToString()).CopyToDataTable();
                        DataRow[] CountOnerow = dsAirLine.Tables["Airline"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                        var OriginRow = dsAirLine.Tables["Origin"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                        var OriginAirportRow = dsAirLine.Tables["AirPort"].Select("Origin_Id=" + OriginRow[0]["Origin_Id"].ToString());
                        var DestinationRow = dsAirLine.Tables["Destination"].Select("Segments_Id=" + dtSegments.Rows[0]["Segments_Id"].ToString());
                        var DestinationAirportRow = dsAirLine.Tables["AirPort"].Select("Destination_Id=" + DestinationRow[0]["Destination_Id"].ToString());
                        string TimeDuration = "";
                        StringBuilder strStopDetail = new StringBuilder();
                        StringBuilder strOriginDetail = new StringBuilder();
                        StringBuilder strDestinationDetail = new StringBuilder();
                        if (count > 1)
                        {
                            strLuggageDetail.Append("<table>");
                            strLuggageDetail.Append("<thead>");
                            strLuggageDetail.Append("<tr>");
                            strLuggageDetail.Append("<th>Sector</th>");
                            strLuggageDetail.Append("<th>Cabin</th>");
                            strLuggageDetail.Append("<th>Check-in</th>");
                            strLuggageDetail.Append("<th>Operatedby</th>");
                            strLuggageDetail.Append("</tr>");
                            strLuggageDetail.Append("</thead>");
                            strLuggageDetail.Append("<tbody>");
                            int duration = 0;
                            for (int j = 0; j < dtSegments.Rows.Count; j++)
                            {
                                if (dtSegments.Rows[j]["AccumulatedDuration"].ToString() != string.Empty)
                                {
                                    duration = Convert.ToInt32(dtSegments.Rows[j]["AccumulatedDuration"]);
                                }
                                DataRow[] StopDetailRow = dsAirLine.Tables["Airline"].Select("Segments_Id=" + dtSegments.Rows[j]["Segments_Id"].ToString());
                                var OriginStopRow = dsAirLine.Tables["Origin"].Select("Segments_Id=" + dtSegments.Rows[j]["Segments_Id"].ToString());
                                var OriginStopAirportRow = dsAirLine.Tables["AirPort"].Select("Origin_Id=" + OriginStopRow[0]["Origin_Id"].ToString());
                                var DestinationStopRow = dsAirLine.Tables["Destination"].Select("Segments_Id=" + dtSegments.Rows[j]["Segments_Id"].ToString());
                                var DestinationStopAirportRow = dsAirLine.Tables["AirPort"].Select("Destination_Id=" + DestinationStopRow[0]["Destination_Id"].ToString());
                                strStopDetail.Append("" + StopDetailRow[0]["AirlineCode"].ToString() + "-");
                                strStopDetail.Append("" + StopDetailRow[0]["FlightNumber"].ToString() + "");
                                strStopDetail.Append("&nbsp;" + StopDetailRow[0]["FareClass"].ToString() + "");
                                strStopDetail.Append("&emsp;" + OriginStopAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(OriginStopRow[0]["DepTime"]).ToString("HH:mm tt") + ")&nbsp;&nbsp;<img src='/flight/Images/flight/arrow.gif' runat='server'></img>");
                                strStopDetail.Append("&nbsp;" + DestinationStopAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(DestinationStopRow[0]["ArrTime"]).ToString("HH:mm tt") + ")");
                                strStopDetail.Append("</br>");
                                strOriginDetail.Append("" + OriginStopAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(OriginStopRow[0]["DepTime"]).ToString("HH:mm tt") + ")&nbsp;&nbsp;<img src='/flight/Images/flight/arrow.gif' runat='server' style='float:right; width: 27px; height: 27px;'></img></br>");
                                strDestinationDetail.Append("&nbsp;" + DestinationStopAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(DestinationStopRow[0]["ArrTime"]).ToString("HH:mm tt") + ")</br>");
                                #region LuggageBindingTwo
                                strLuggageDetail.Append("<tr>");
                                strLuggageDetail.Append("<td>" + OriginStopAirportRow[0]["AirPortCode"].ToString() + "- " + DestinationStopAirportRow[0]["AirPortCode"].ToString() + "</td>");
                                strLuggageDetail.Append("<td>" + dtSegments.Rows[j]["CabinBaggage"].ToString() + "</td>");
                                strLuggageDetail.Append("<td>" + dtSegments.Rows[j]["Baggage"].ToString() + "</td>");
                                strLuggageDetail.Append("<td>" + StopDetailRow[0]["OperatingCarrier"].ToString() + "</td>");
                                strLuggageDetail.Append("</tr>");
                                #endregion
                            }
                            strLuggageDetail.Append("</tbody>");
                            strLuggageDetail.Append("</table>");
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
                            int duration = Convert.ToInt32(dtSegments.Rows[0]["Duration"]);
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
                            strOriginDetail.Append("" + OriginAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("HH:mm tt") + ")&nbsp;&nbsp;<img src='/flight/Images/flight/arrow.gif' runat='server' style='float:right; width: 27px; height: 27px;'></img></br>");
                            strDestinationDetail.Append("&nbsp;" + DestinationAirportRow[0]["AirPortCode"].ToString() + "(" + Convert.ToDateTime(DestinationRow[0]["ArrTime"]).ToString("HH:mm tt") + ")</br>");
                            #region LuggageBindingOne
                            strLuggageDetail.Append("<table>");
                            strLuggageDetail.Append("<thead>");
                            strLuggageDetail.Append("<tr>");
                            strLuggageDetail.Append("<th>Sector</th>");
                            strLuggageDetail.Append("<th>Cabin</th>");
                            strLuggageDetail.Append("<th>Check-in</th>");
                            strLuggageDetail.Append("<th>Operatedby</th>");
                            strLuggageDetail.Append("</tr>");
                            strLuggageDetail.Append("</thead>");
                            strLuggageDetail.Append("<tbody>");
                            strLuggageDetail.Append("<tr>");
                            strLuggageDetail.Append("<td>" + OriginAirportRow[0]["AirPortCode"].ToString() + "- " + DestinationAirportRow[0]["AirPortCode"].ToString() + "</td>");
                            strLuggageDetail.Append("<td>" + dtSegments.Rows[0]["CabinBaggage"].ToString() + "</td>");
                            strLuggageDetail.Append("<td>" + dtSegments.Rows[0]["Baggage"].ToString() + "</td>");
                            strLuggageDetail.Append("<td>" + CountOnerow[0]["OperatingCarrier"].ToString() + "</td>");
                            strLuggageDetail.Append("</tr>");
                            strLuggageDetail.Append("</tbody>");
                            strLuggageDetail.Append("</table>");
                            #endregion
                        }
                        for (int k = 0; k < dtSegments.Columns.Count; k++)
                        {
                            if (dtSegments.Columns[k].ColumnName == "NoOfSeatAvailable")
                            {
                                RetrunAirdata.Add(new EzulixAir.AirList
                                {
                                    AirlineCode = CountOnerow[0]["AirlineCode"].ToString(),
                                    AirlineName = CountOnerow[0]["AirlineName"].ToString(),
                                    FlightNumber = CountOnerow[0]["FlightNumber"].ToString(),
                                    FareClass = CountOnerow[0]["FareClass"].ToString(),
                                    OriginTime = Convert.ToDateTime(OriginRow[0]["DepTime"]).ToString("HH:mm tt"),
                                    Origin = OriginAirportRow[0]["AirPortCode"].ToString(),
                                    DestinationTime = Convert.ToDateTime(DestinationRow[0]["ArrTime"]).ToString("HH:mm tt"),
                                    Destination = DestinationAirportRow[0]["AirPortCode"].ToString(),
                                    Duration = TimeDuration,
                                    NoofSeatAvailable = dtSegments.Rows[0]["NoofSeatAvailable"].ToString(),
                                    PubPrice = Math.Round(Convert.ToDecimal(dsAirLine.Tables["Fare"].Rows[i - 2]["PublishedFare"].ToString()), MidpointRounding.AwayFromZero),
                                    StopCount = count - 1,
                                    StopDetail = strStopDetail.ToString(),
                                    OriginDetail = strOriginDetail.ToString(),
                                    DestinationDetail = strDestinationDetail.ToString(),
                                    AirllineFullName = CountOnerow[0]["AirlineName"].ToString() + "(" + CountOnerow[0]["AirlineCode"].ToString() + ")",
                                    IsRefundable = Convert.ToBoolean(dsAirLine.Tables["Results"].Rows[i]["IsRefundable"]),
                                    LuggageDetail = strLuggageDetail.ToString(),
                                    Resultid = dsAirLine.Tables["Results"].Rows[i]["ResultIndex"].ToString(),
                                    IsLCC = dsAirLine.Tables["Results"].Rows[i]["IsLCC"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Session!');location.replace('FlightSearch.aspx');", true);
            }
            if (RetrunAirdata.Count > 0)
            {
                Session["ReturnAirDataStorage"] = null;
                Session["ReturnAirDataStorage"] = RetrunAirdata;
                gv_RetrunFlightList.DataSource = RetrunAirdata;
                gv_RetrunFlightList.DataBind();
                #region BindReturnAirlineList
                chk_ReturnAirline.DataSource = RetrunAirdata.Select(x => new { x.AirllineFullName }).Distinct();
                chk_ReturnAirline.DataTextField = "AirllineFullName";
                chk_ReturnAirline.DataValueField = "AirllineFullName";
                chk_ReturnAirline.DataBind();
                #endregion
            }
            #region BindAirlineList
            chk_Airline.DataSource = Airdata.Select(x => new { x.AirllineFullName }).Distinct();
            chk_Airline.DataTextField = "AirllineFullName";
            chk_Airline.DataValueField = "AirllineFullName";
            chk_Airline.DataBind();
            #endregion
            Session["AirDataStorage"] = null;
            Session["AirDataStorage"] = Airdata;
            gv_FlightList.DataSource = Airdata;
            gv_FlightList.DataBind();
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
            if (Session["dtMember"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopupnew()", true);
            }
            else
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
                        if (ds.Tables.Contains("Response"))
                        {
                            if (ds.Tables["Response"].Rows[0]["ResponseStatus"].ToString() == "1")
                            {
                                if (ds.Tables.Contains("Results"))
                                {
                                    var source = ds.Tables["Results"].Rows[0]["Source"].ToString();
                                    Session["Sourcenum"] = ds.Tables["Results"].Rows[0]["Source"].ToString();
                                    //Session["Flight"] = ds.Tables["Segments"].Rows[0]["AirlineCode"].ToString() + ds.Tables["Segments"].Rows[0]["AirlineName"].ToString() + ds.Tables["Segments"].Rows[0]["FlightNumber"].ToString() ;
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
                                string SSRResult = eAir.SSR(Resultindex, Session["Traceid"].ToString());
                                if (SSRResult != null)
                                {
                                    DataSet dsSSR = eAir.Deserialize(SSRResult);
                                    Session["SSR"] = dsSSR;
                                    //System.Threading.Thread.Sleep(2000);
                                    //System.Threading.Thread.ResetAbort();
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.location ='FlightPassengerDetails.aspx';", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + "Empty SSR Response!" + "');location.replace('FlightSearch.aspx');", true);
  }
                                //return;
                            }
                            else if (ds.Tables["Response"].Rows[0]["ResponseStatus"].ToString() == "4")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);
   }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);
  }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Result + "');location.replace('FlightSearch.aspx');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + "Try after some time" + "');location.replace('FlightSearch.aspx');", true);
                        
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Response!');location.replace('FlightSearch.aspx');", true);
                }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    #endregion

    #region Evetns
    protected void gv_FlightList_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {

    }

    protected void gv_FlightList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "rule")
        {
            try
            {
                if (Session["Traceid"] != null)
                {
                    Result = string.Empty;
                    string Resultindex = e.CommandArgument.ToString();
                    Result = FareRules(Resultindex, Session["Traceid"].ToString());
                    if (Result != string.Empty)
                    {
                        DataSet ds = eAir.Deserialize(Result);
                        if (ds.Tables.Contains("Response"))
                        {
                            if (ds.Tables["Response"].Rows[0]["ResponseStatus"].ToString() == "1")
                            {
                                mpe_FareRules.Show();
                                lbl_FareRules.Text = ds.Tables["FareRules"].Rows[0]["FareRuleDetail"].ToString();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('"+Result+"');location.replace('FlightSearch.aspx');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Result!');location.replace('FlightSearch.aspx');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Time Out, Please search again');location.replace('FlightSearch.aspx');", true);
                    
                    return;
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('"+ err.Message + "');location.replace('FlightSearch.aspx');", true);
                
            }
        }

        else if (e.CommandName == "book")
        {
            try
            {
                if (Session["Traceid"] != null)
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string Resultindex = commandArgs[0];
                    string IsLCC = commandArgs[1];
                    Session["IsLCC"] = IsLCC;
                    FlightBook(Resultindex);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + "Time Out, Please search again" + "');location.replace('FlightSearch.aspx');", true);
                    
                    return;
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);
                
                return;
            }
        }
    }

    #region Flight_Filters
    protected void chk_Stops_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ChkboxlistValue = "";
            foreach (ListItem val in chk_Stops.Items)
            {
                if (val.Selected)
                {
                    ChkboxlistValue += val.Value;
                }
            }
            List<EzulixAir.AirList> Obj = (List<EzulixAir.AirList>)Session["AirDataStorage"];
            if (ChkboxlistValue == "0")
            {
                gv_FlightList.DataSource = Obj.Where(f => f.StopCount == 0).ToList();
                gv_FlightList.DataBind();
            }
            else if (ChkboxlistValue == "1")
            {
                gv_FlightList.DataSource = Obj.Where(f => f.StopCount == 1).ToList();
                gv_FlightList.DataBind();
            }
            else if (ChkboxlistValue == "2")
            {
                gv_FlightList.DataSource = Obj.Where(f => f.StopCount >= 2).ToList();
                gv_FlightList.DataBind();
            }
            else if (ChkboxlistValue == "01")
            {
                gv_FlightList.DataSource = Obj.Where(f => f.StopCount <= 1).ToList();
                gv_FlightList.DataBind();
            }
            else if (ChkboxlistValue == "12")
            {
                gv_FlightList.DataSource = Obj.Where(f => f.StopCount >= 1).ToList();
                gv_FlightList.DataBind();
            }
            else if (ChkboxlistValue == "02")
            {
                gv_FlightList.DataSource = Obj.Where(f => f.StopCount == 0 || f.StopCount >= 2).ToList();
                gv_FlightList.DataBind();
            }
            else if (ChkboxlistValue == "012")
            {
                gv_FlightList.DataSource = Obj;
                gv_FlightList.DataBind();
            }
            else
            {
                gv_FlightList.DataSource = Obj;
                gv_FlightList.DataBind();
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);
            
        }
    }

    protected void chk_Airline_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ChkboxlistValue = "";
            foreach (ListItem val in chk_Airline.Items)
            {
                if (val.Selected)
                {
                    ChkboxlistValue += val.Value + ",";
                }
            }
            List<EzulixAir.AirList> Obj = (List<EzulixAir.AirList>)Session["AirDataStorage"];
            var data = ChkboxlistValue.Split(',').ToList();
            var Filterdata = data.Take(data.Count() - 1).ToArray();
            gv_FlightList.DataSource = Obj.Where(o => Filterdata.Any(s => o.AirllineFullName.Contains(s))).ToList();
            gv_FlightList.DataBind();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    protected void chk_Faretype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ChkboxlistValue = "";
            foreach (ListItem val in chk_Faretype.Items)
            {
                if (val.Selected)
                {
                    ChkboxlistValue += val.Value;
                }
            }
            List<EzulixAir.AirList> Obj = (List<EzulixAir.AirList>)Session["AirDataStorage"];
            if (ChkboxlistValue == "0")
            {
                gv_FlightList.DataSource = Obj.Where(f => f.IsRefundable == true).ToList();
                gv_FlightList.DataBind();
            }
            else if (ChkboxlistValue == "1")
            {
                gv_FlightList.DataSource = Obj.Where(f => f.IsRefundable == false).ToList();
                gv_FlightList.DataBind();
            }
            else if (ChkboxlistValue == "01")
            {
                gv_FlightList.DataSource = Obj.Where(f => f.IsRefundable == true || f.IsRefundable == false).ToList();
                gv_FlightList.DataBind();
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    #endregion

    #region Retunr_Flight_Filters
    protected void chk_ReturnStops_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ChkboxlistValue = "";
            foreach (ListItem val in chk_ReturnStops.Items)
            {
                if (val.Selected)
                {
                    ChkboxlistValue += val.Value;
                }
            }
            List<EzulixAir.AirList> Obj = (List<EzulixAir.AirList>)Session["ReturnAirDataStorage"];
            if (ChkboxlistValue == "0")
            {
                gv_RetrunFlightList.DataSource = Obj.Where(f => f.StopCount == 0).ToList();
                gv_RetrunFlightList.DataBind();
            }
            else if (ChkboxlistValue == "1")
            {
                gv_RetrunFlightList.DataSource = Obj.Where(f => f.StopCount == 1).ToList();
                gv_RetrunFlightList.DataBind();
            }
            else if (ChkboxlistValue == "2")
            {
                gv_RetrunFlightList.DataSource = Obj.Where(f => f.StopCount >= 2).ToList();
                gv_RetrunFlightList.DataBind();
            }
            else if (ChkboxlistValue == "01")
            {
                gv_RetrunFlightList.DataSource = Obj.Where(f => f.StopCount <= 1).ToList();
                gv_RetrunFlightList.DataBind();
            }
            else if (ChkboxlistValue == "12")
            {
                gv_RetrunFlightList.DataSource = Obj.Where(f => f.StopCount >= 1).ToList();
                gv_RetrunFlightList.DataBind();
            }
            else if (ChkboxlistValue == "02")
            {
                gv_RetrunFlightList.DataSource = Obj.Where(f => f.StopCount == 0 || f.StopCount >= 2).ToList();
                gv_RetrunFlightList.DataBind();
            }
            else if (ChkboxlistValue == "012")
            {
                gv_RetrunFlightList.DataSource = Obj;
                gv_RetrunFlightList.DataBind();
            }
            else
            {
                gv_RetrunFlightList.DataSource = Obj;
                gv_RetrunFlightList.DataBind();
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    protected void chk_ReturnAirline_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ChkboxlistValue = "";
            foreach (ListItem val in chk_ReturnAirline.Items)
            {
                if (val.Selected)
                {
                    ChkboxlistValue += val.Value + ",";
                }
            }
            List<EzulixAir.AirList> Obj = (List<EzulixAir.AirList>)Session["ReturnAirDataStorage"];
            var data = ChkboxlistValue.Split(',').ToList();
            var Filterdata = data.Take(data.Count() - 1).ToArray();
            gv_RetrunFlightList.DataSource = Obj.Where(o => Filterdata.Any(s => o.AirllineFullName.Contains(s))).ToList();
            gv_RetrunFlightList.DataBind();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    protected void chk_ReturnFaretype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ChkboxlistValue = "";
            foreach (ListItem val in chk_ReturnFaretype.Items)
            {
                if (val.Selected)
                {
                    ChkboxlistValue += val.Value;
                }
            }
            List<EzulixAir.AirList> Obj = (List<EzulixAir.AirList>)Session["ReturnAirDataStorage"];
            if (ChkboxlistValue == "0")
            {
                gv_RetrunFlightList.DataSource = Obj.Where(f => f.IsRefundable == true).ToList();
                gv_RetrunFlightList.DataBind();
            }
            else if (ChkboxlistValue == "1")
            {
                gv_RetrunFlightList.DataSource = Obj.Where(f => f.IsRefundable == false).ToList();
                gv_RetrunFlightList.DataBind();
            }
            else if (ChkboxlistValue == "01")
            {
                gv_RetrunFlightList.DataSource = Obj.Where(f => f.IsRefundable == true || f.IsRefundable == false).ToList();
                gv_RetrunFlightList.DataBind();
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    #endregion

    protected void btn_ModifySearch_Click(object sender, EventArgs e)
    {
        try
        {
            mpe_ModifySearch.Show();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    protected void rbtn_Outflight_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            List<EzulixAir.AirList> Obj = (List<EzulixAir.AirList>)Session["AirDataStorage"];
            RadioButton rbtn_Outflight = (sender as RadioButton);
            HiddenField Hf_ResultId = (HiddenField)rbtn_Outflight.FindControl("Hf_ResultId");
            gv_OneFlightlist.DataSource = Obj.Where(f => f.Resultid == Hf_ResultId.Value).ToList();
            gv_OneFlightlist.DataBind();
            Session["OutFlight"] = null;
            Session["OutFlight"] = Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.Resultid).SingleOrDefault();
            Session["OutFlightDtl"] = null;
            var Row = Obj.Where(f => f.Resultid == Hf_ResultId.Value).ToList();
            StringBuilder StrbuildOutFlightDtl = new StringBuilder();
            StrbuildOutFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.Origin).SingleOrDefault() + "-");
            StrbuildOutFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.Destination).SingleOrDefault() + ",");
            StrbuildOutFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.AirlineCode).SingleOrDefault() + "-");
            StrbuildOutFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.FlightNumber).SingleOrDefault());
            StrbuildOutFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.IsLCC).SingleOrDefault());
            Session["IsLCC"] = Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.IsLCC).SingleOrDefault();
            StrbuildOutFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.FareClass).SingleOrDefault() + "<br>");
            StrbuildOutFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.TravelDate).SingleOrDefault() + ",");
            StrbuildOutFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.OriginTime).SingleOrDefault() + "-");
            StrbuildOutFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.DestinationTime).SingleOrDefault());
            Session["OutFlightDtl"] = StrbuildOutFlightDtl.ToString();
            ViewState["OneAmount"] = 0;
            ViewState["OneAmount"] = Obj.Where(f => f.Resultid == Hf_ResultId.Value).Select(f => f.PubPrice).SingleOrDefault().ToString();
            lbl_ReturnTotalPrice.Text = (Convert.ToDecimal(ViewState["OneAmount"]) + Convert.ToDecimal(ViewState["TwoAmount"])).ToString();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);
            
            return;
        }
    }

    protected void rbtn_Inflight_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            List<EzulixAir.AirList> Obj = (List<EzulixAir.AirList>)Session["ReturnAirDataStorage"];
            RadioButton rbtn_Inflight = (sender as RadioButton);
            HiddenField Hf_ReturnResultId = (HiddenField)rbtn_Inflight.FindControl("Hf_ReturnResultId");
            gv_TwoFlightlist.DataSource = Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).ToList();
            gv_TwoFlightlist.DataBind();
            Session["InFlight"] = null;
            Session["InFlight"] = Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.Resultid).SingleOrDefault();
            Session["InFlightDtl"] = null;
            var Row = Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).ToList();
            StringBuilder StrbuildInFlightDtl = new StringBuilder();
            StrbuildInFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.Origin).SingleOrDefault() + "-");
            StrbuildInFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.Destination).SingleOrDefault() + ",");
            StrbuildInFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.AirlineCode).SingleOrDefault() + "-");
            StrbuildInFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.FlightNumber).SingleOrDefault());
            StrbuildInFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.IsLCC).SingleOrDefault());
            Session["IsLCC2"] = Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.IsLCC).SingleOrDefault();
            StrbuildInFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.FareClass).SingleOrDefault() + "<br>");
            StrbuildInFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.TravelDate).SingleOrDefault() + ",");
            StrbuildInFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.OriginTime).SingleOrDefault() + "-");
            StrbuildInFlightDtl.Append(Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.DestinationTime).SingleOrDefault());
            Session["InFlightDtl"] = StrbuildInFlightDtl.ToString();
            ViewState["TwoAmount"] = 0;
            ViewState["TwoAmount"] = Obj.Where(f => f.Resultid == Hf_ReturnResultId.Value).Select(f => f.PubPrice).SingleOrDefault().ToString();
            lbl_ReturnTotalPrice.Text = (Convert.ToDecimal(ViewState["OneAmount"]) + Convert.ToDecimal(ViewState["TwoAmount"])).ToString();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + err.Message + "');location.replace('FlightSearch.aspx');", true);


            return;
        }
    }

    protected void btn_ReturnBookflight_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["OutFlight"] != null)
            {
                FlightBook(Session["OutFlight"].ToString());
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    protected void btn_Modify_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "location.replace('FlightSearch.aspx');", true);
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    #endregion
}