using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using BLL;

public partial class Root_Distributor_busticketcancel : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection();
    cls_connection cls = new cls_connection();
    DataTable dtMember = new DataTable();
    clsMLM_MemberMaster objMember = new clsMLM_MemberMaster();
    cls_myMember clsm = new cls_myMember();
    EzulixBus ibus = new EzulixBus();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request["bookingid"] != null)
            {
                string Bookingid = Request["bookingid"].ToString();
                string result = ibus.Get_Ticket(Bookingid);
                DataSet ds = new DataSet();
                ds = Deserialize(result);
                string txn = ds.Tables[0].Rows[0]["statuscode"].ToString();
                string errorstatus = ds.Tables[0].Rows[0]["status"].ToString();
                if (txn == "TXN")
                {
                    string bustype = ds.Tables[1].Rows[0]["bustype"].ToString();
                    string cpolicy = ds.Tables[1].Rows[0]["cancellationPolicy"].ToString();
                    string destinationcity = ds.Tables[1].Rows[0]["destinationcity"].ToString();
                    string doj = ds.Tables[1].Rows[0]["doj"].ToString();
                    string droplocation = ds.Tables[1].Rows[0]["dropLocation"].ToString();
                    string droptime = ds.Tables[1].Rows[0]["droptime"].ToString();
                    string partialcancel = ds.Tables[1].Rows[0]["partialCancellationAllowed"].ToString();
                    string pickupcontact = ds.Tables[1].Rows[0]["PickupContactNo"].ToString();
                    string pickuplocation = ds.Tables[1].Rows[0]["pickupLocation"].ToString();
                    string pickupLocationLandmark = ds.Tables[1].Rows[0]["pickupLocationLandmark"].ToString();
                    string pickupTime = ds.Tables[1].Rows[0]["pickupTime"].ToString();
                    string pnr = ds.Tables[1].Rows[0]["pnr"].ToString();
                    string sourcecity = ds.Tables[1].Rows[0]["sourcecity"].ToString();
                    string status = ds.Tables[1].Rows[0]["status"].ToString();
                    string travels = ds.Tables[1].Rows[0]["travels"].ToString();
                    string primeDepartureTime = ds.Tables[1].Rows[0]["primeDepartureTime"].ToString();
                    string ticketid = ds.Tables[1].Rows[0]["tin"].ToString();
                    string dateofissue = ds.Tables[1].Rows[0]["dateOfissue"].ToString();
                    DataTable dtinventory = new DataTable();
                    dtinventory = ds.Tables[2];
                    decimal fare = Convert.ToDecimal(dtinventory.Rows[0]["fare"].ToString());
                    var netAmount = dtinventory.AsEnumerable().Sum(x => Convert.ToDecimal(x["fare"]));
                    lbltotalfare.Text = netAmount.ToString();
                    DataTable dtpassenger = new DataTable();
                    dtpassenger = ds.Tables[3];

                    var minLPData = (from x in dtinventory.AsEnumerable()
                                     join y in dtpassenger.AsEnumerable() on x.Field<int>("inventoryItems_Id") equals y.Field<int>("inventoryItems_Id")
                                     select new
                                     {
                                         name = y.Field<string>("name"),
                                         age = y.Field<string>("age"),
                                         mobile = y.Field<string>("mobile"),
                                         seats = x.Field<string>("seatName")

                                     }).ToList();

                    GridView1.DataSource = minLPData;
                    GridView1.DataBind();
                    lblfrom.Text = sourcecity;
                    lblto.Text = destinationcity;
                    string[] dojm = doj.Split('T');
                    lbldoj.Text = string.Format("{0:f}", dojm[0]);
                    lbltravelername.Text = travels;
                    lblorderid.Text = Bookingid;
                    lblpnr.Text = pnr;
                    lblcontact.Text = pickupcontact;
                    lbllocation.Text = pickuplocation;
                    lbllandmark.Text = pickupLocationLandmark;
                    lblticket.Text = ticketid;

                    string lblarivaltime = primeDepartureTime;
                    int arivaltimetime = Convert.ToInt32(lblarivaltime);
                    int arivaltimeQuotient = arivaltimetime / 60;
                    int arivaltimeremanider = arivaltimetime % 60;
                    int arivaltimejourneyday = arivaltimeQuotient / 24;
                    int arivaltimehours = arivaltimeQuotient % 24;
                    int arivaltimeminutes = arivaltimeremanider;
                    if (arivaltimejourneyday == 0)
                    {
                        string arivaltimeoutputs = arivaltimehours + ":" + arivaltimeminutes;
                        TimeSpan arivaltimespan = new TimeSpan(arivaltimehours, arivaltimeminutes, 00);
                        DateTime arivaltimeoutputtime = Convert.ToDateTime(lbldoj.Text).Add(arivaltimespan);
                        string arivaltimedisplayTime = arivaltimeoutputtime.ToString("hh:mm tt");
                        lbldeparturetime.Text = arivaltimedisplayTime;
                    }
                    else
                    {
                        string arivaltimeoutputs = arivaltimehours + ":" + arivaltimeminutes;
                        TimeSpan arivaltimespan = new TimeSpan(arivaltimehours, arivaltimeminutes, 00);
                        DateTime arivaltimeoutputtime = Convert.ToDateTime(lbldoj.Text).Add(arivaltimespan);
                        DateTime newDate = arivaltimeoutputtime.AddDays(1);
                        string arivaltimedisplayTime = arivaltimeoutputtime.ToString("hh:mm tt");
                        TimeSpan arivaltimeduration = Convert.ToDateTime(arivaltimedisplayTime) - Convert.ToDateTime(arivaltimedisplayTime);
                        lbldeparturetime.Text = arivaltimedisplayTime;
                    }


                    string lbldeparturetimse = pickupTime;
                    int departuretimetime = Convert.ToInt32(lbldeparturetimse);
                    int departuretimeQuotient = departuretimetime / 60;
                    int departuretimeremanider = departuretimetime % 60;
                    int departuretimejourneyday = departuretimeQuotient / 24;
                    int departuretimehours = departuretimeQuotient % 24;
                    int departuretimeminutes = departuretimeremanider;
                    if (departuretimejourneyday == 0)
                    {
                        string departuretimeoutputs = departuretimehours + ":" + departuretimeminutes;
                        TimeSpan departuretimetimespan = new TimeSpan(departuretimehours, departuretimeminutes, 00);
                        // DateTime departuretimeoutputtime = DateTime.Today.Add(departuretimetimespan);
                        DateTime departuretimeoutputtime = Convert.ToDateTime(lbldoj.Text).Add(departuretimetimespan);
                        string departuretimedisplayTime = departuretimeoutputtime.ToString("hh:mm tt");
                        lblreportingtime.Text = departuretimedisplayTime;
                    }



                    string[] aa = dateofissue.Split('T');
                    string[] gg = aa[1].Split('+');
                    lblbookingdate.Text = string.Format("{0:f}", aa[0] + gg[0]);
                    lblbustype.Text = bustype;
                    string main = cpolicy.Remove(cpolicy.Length - 1);
                    string[] ssplit = main.Split(';');
                    List<cancelation> _listcancel = new List<cancelation>();
                    foreach (string s in ssplit)
                    {
                        if (s != string.Empty)
                        {
                            string[] spart = s.Split(':');
                            string fromtime = spart[0];
                            string totime = spart[1];
                            string crate = spart[2];
                            string percentage = spart[3];
                            string description = string.Empty;
                            string percent = string.Empty;
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Description");
                            dt.Columns.Add("Charges");
                            if (fromtime == "0")
                            {
                                DateTime time = Convert.ToDateTime(lbldoj.Text + " " + lblreportingtime.Text).AddHours(-Convert.ToDouble(totime));
                                description = "After" + " " + string.Format("{0:f}", time);
                                string pt = crate + "%";
                                _listcancel.Add(new cancelation() { Description = description, Charges = pt });
                                //dt.Rows.Add(description);
                                // dt.Rows.Add(pt);
                            }
                            else if (totime == "-1")
                            {
                                DateTime time = Convert.ToDateTime(lbldoj.Text + " " + lblreportingtime.Text).AddHours(-Convert.ToDouble(fromtime));
                                description = "Before" + " " + string.Format("{0:f}", time);
                                string pt = crate + "%";
                                _listcancel.Add(new cancelation() { Description = description, Charges = pt });
                                //dt.Rows.Add(description);
                                //dt.Rows.Add(pt);
                            }
                            else
                            {
                                DateTime time1 = Convert.ToDateTime(lbldoj.Text + " " + lblreportingtime.Text).AddHours(-Convert.ToDouble(fromtime));
                                DateTime time2 = Convert.ToDateTime(lbldoj.Text + " " + lblreportingtime.Text).AddHours(-Convert.ToDouble(totime));
                                description = "Between" + " " + string.Format("{0:f}", time2) + " and" + " " + string.Format("{0:f}", time1);
                                string pt = crate + "%";
                                _listcancel.Add(new cancelation() { Description = description, Charges = pt });
                                //dt.Rows.Add(description);
                            }
                        }
                    }

                    if (partialcancel == "false")
                    {
                        _listcancel.Add(new cancelation() { Description = "* Partial Cancellation Not Allowed", Charges = "" });
                    }

                    GridView2.DataSource = _listcancel;
                    GridView2.DataBind();

                }
                else if (txn == "ERR")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + errorstatus + "');", true);
                }
            }
            else
            {

            }
        }
    }
    private DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
    public class cancelation
    {
        public string Description { get; set; }
        public string Charges { get; set; }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        string seats = string.Empty;
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                if (chkRow.Checked)
                {
                    seats += row.Cells[4].Text + ',';
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select at least one Ticket to cancel ');", true);
                }
            }
        }
        string output = string.Empty;
        if (seats != "")
        {
            output = seats.Remove(seats.Length - 1, 1);
          string result= ibus.Cancel_Ticket(Request["bookingid"].ToString(), output);
              DataSet ds = new DataSet();
                ds = Deserialize(result);
                string txn = ds.Tables[0].Rows[0]["statuscode"].ToString();
                string errorstatus = ds.Tables[0].Rows[0]["status"].ToString();
                if (txn == "TXN")
                {
                    cls.update_data("update tbl_busbook_psgdetails_main set bookingstatus='cancelled' where booking_id='" + Request["bookingid"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Canceled Successfully!');", true);
                }
        }
    }
}