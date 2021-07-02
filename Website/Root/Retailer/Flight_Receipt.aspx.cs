using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;
using System.IO;
using System.Text;
using Common;
using System.Net;
using Newtonsoft.Json;

public partial class Flight_Receipt : System.Web.UI.Page
{
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    cls_connection cls = new cls_connection();
    private DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["ReceiptDetail"] != null)
                {
                    ds = (DataSet)Session["ReceiptDetail"];
                    if (ds.Tables.Contains("FlightItinerary"))
                    {
                        if (ds.Tables["FlightItinerary"].Rows.Count > 0)
                        {
                            FlightInformation();
                            PassengerDetails();
                            Session["BCtraceId"] = null;
                            Session["BCtraceId"] = ds.Tables["Response"].Rows[0]["TraceId"].ToString();
                            
                            SaleSummary();
                            for (int i = 0; i < ds.Tables["FlightItinerary"].Rows.Count; i++)
                            {
                                List<EzulixAir.BookConfirm> BObjj = new List<EzulixAir.BookConfirm>
                                            {
                                                new EzulixAir.BookConfirm
                                                {
                                                    EndUserIp="172.107.166.241",
                                                    TokenId=Session["ReceiptTokenId"].ToString(),
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Data not Found');location.replace('FlightSearch.aspx');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Session Null!');location.replace('FlightTicket_List.aspx');", true);
                }
            }
        }
        catch(Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('"+ err.Message.ToString() +"');location.replace('FlightSearch.aspx');", true);
        }
    }
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
                    tbl_PublishFare.Text = "Rs. " + pubfee;
                    //lbl_TotalPayable.Text = Math.Round((Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["PublishedFare"].ToString()) - (Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["CommissionEarned"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["IncentiveEarned"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["PLBEarned"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["AdditionalTxnFeeOfrd"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["AdditionalTxnFeePub"]))) + (Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["TdsOnCommission"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["TdsOnIncentive"]) + Convert.ToDecimal(ds.Tables["Fare"].Rows[i]["TdsOnPLB"])), MidpointRounding.AwayFromZero).ToString();
                    //lbl_Discount.Text = (Convert.ToDecimal(pubfee) - Convert.ToDecimal(lbl_TotalPayable.Text)).ToString();


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
            //dtlist_SaleSummary.DataSource = FSboj;
            //dtlist_SaleSummary.DataBind();
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

}