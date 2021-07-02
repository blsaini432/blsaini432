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

public partial class Root_Distributor_busnew : System.Web.UI.Page
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
            if (Session["dtDistributor"] != null)
            {
                ViewState["Column"] = "availableSeats";
                ViewState["Sortorder"] = "ASC";
                Bind_Source();
                Bind_destination();
                dtMember = (DataTable)Session["dtDistributor"];
                Session["DistributorMsrNo"] = dtMember.Rows[0]["MsrNo"].ToString();
                ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"].ToString();
                //lblretailerID.Text = dtMember.Rows[0]["MemberID"].ToString();
                ViewState["MemberId"] = dtMember.Rows[0]["MemberID"].ToString();
                ViewState["MobileNo"] = dtMember.Rows[0]["Mobile"].ToString();
                clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
                DataTable dtEWalletBalance = new DataTable();
                dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(dtMember.Rows[0]["MsrNo"]));
               
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }

    #region methods
    private void Bind_Source()
    {
        string filePath = Server.MapPath("~/bus_sources.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddl_leavingfrom.DataSource = ds.Tables[2];
            ddl_leavingfrom.DataTextField = "name";
            ddl_leavingfrom.DataValueField = "id";
            ddl_leavingfrom.DataBind();
        }
    }

    private void Bind_destination()
    {
        string filePath = Server.MapPath("~/bus_sources.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddl_goingto.DataSource = ds.Tables[2];
            ddl_goingto.DataTextField = "name";
            ddl_goingto.DataValueField = "id";
            ddl_goingto.DataBind();
        }
    }
    public class cancelation
    {
        public string Description { get; set; }
        public string Charges { get; set; }
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

    private object FormatDate(DateTime input)
    {
        return String.Format("{0:MM/dd/yy}", input);
    }
    #endregion

    #region clickevents
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string source = ddl_leavingfrom.SelectedValue;
            string destination = ddl_goingto.SelectedValue;
            string result = ibus.Search_Buses(source, destination, TextBox1.Text);
            if (result.Contains("timed out") == true)
            {
                rptrCustomer.DataSource = null;
                rptrCustomer.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + result + "');", true);
            }
            else
            {
                //string[] output = result.Split('<');
                dynamic myObject = JsonConvert.DeserializeObject(result);
                //Using DataTable with JsonConvert.DeserializeObject, here you need to import using System.Data;  
                DataSet myObjectDT = Deserialize(result);
                string txn = myObjectDT.Tables[0].Rows[0]["statuscode"].ToString();
                string errorstatus = myObjectDT.Tables[0].Rows[0]["status"].ToString();
                if (txn == "TXN")
                {
                    DataTable dsmerge = new DataTable();
                    dsmerge = myObjectDT.Tables[5];
                    DataTable dsd = new DataTable();
                    Session["BoardingPoints"] = myObjectDT.Tables[2];
                    Session["DroppingPoints"] = myObjectDT.Tables[3];
                    Session["fares"] = myObjectDT.Tables[5];
                    Session["maint"] = myObjectDT.Tables[1];
                    DataView dv = new DataView(myObjectDT.Tables[1]);
                    PagedDataSource pageds = new PagedDataSource();
                    dv.Sort = ViewState["Column"] + " " + ViewState["Sortorder"];
                    pageds.DataSource = dv;
                    rptrCustomer.DataSource = pageds;
                    rptrCustomer.DataBind();
                    t1.Visible = true;
                    lbldestination.Text = ddl_goingto.SelectedItem.ToString();
                    lblsource.Text = ddl_leavingfrom.SelectedItem.ToString();
                    lbldoj.Text = TextBox1.Text;
                    pnlsearchbus.Visible = false;
                }
                else
                {
                    rptrCustomer.DataSource = null;
                    rptrCustomer.DataBind();
                    t1.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + errorstatus + "');", true);
                }
            }
        }
        catch (Exception exm)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + exm + "');", true);
        }
    }

    protected void linkbtncancelpolicy_Click(object sender, CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        if (Session["BoardingPoints"] != null)
        {
            DataTable dtboardingtbl = (DataTable)Session["maint"];
            DataTable tblFiltered = dtboardingtbl.AsEnumerable().Where(row => row.Field<Int32>("data_Id") == id).CopyToDataTable();
            //int departuretimetime = Convert.ToInt32(tblFiltered.Rows[0]["departureTime"]);
            string[] doj = tblFiltered.Rows[0]["doj"].ToString().Split('T');
            string date = doj[0];
            var btn = (LinkButton)sender;
            var item = (RepeaterItem)btn.NamingContainer;
            var ddl = (Label)item.FindControl("lbldeparturedate");
            Label lab = ddl;
            string a = lab.Text;
            string cpolicy = tblFiltered.Rows[0]["cancellationPolicy"].ToString();
            string particalcancel = tblFiltered.Rows[0]["partialCancellationAllowed"].ToString();

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
                        DateTime time = Convert.ToDateTime(a).AddHours(-Convert.ToDouble(totime));
                        description = "After" + " " + string.Format("{0:f}", time);
                        string pt = crate + "%";
                        _listcancel.Add(new cancelation() { Description = description, Charges = pt });
                        //dt.Rows.Add(description);
                        // dt.Rows.Add(pt);
                    }
                    else if (totime == "-1")
                    {
                        DateTime time = Convert.ToDateTime(a).AddHours(-Convert.ToDouble(fromtime));
                        description = "Before" + " " + string.Format("{0:f}", time);
                        string pt = crate + "%";
                        _listcancel.Add(new cancelation() { Description = description, Charges = pt });
                        //dt.Rows.Add(description);
                        //dt.Rows.Add(pt);
                    }
                    else
                    {
                        DateTime time1 = Convert.ToDateTime(a).AddHours(-Convert.ToDouble(fromtime));
                        DateTime time2 = Convert.ToDateTime(a).AddHours(-Convert.ToDouble(totime));
                        description = "Between" + " " + string.Format("{0:f}", time2) + " and" + " " + string.Format("{0:f}", time1);
                        string pt = crate + "%";
                        _listcancel.Add(new cancelation() { Description = description, Charges = pt });
                        //dt.Rows.Add(description);
                    }
                }
            }

            if (particalcancel == "false")
            {
                _listcancel.Add(new cancelation() { Description = "* Partial Cancellation Not Allowed", Charges = "" });
            }

            rptcancelpolicy.DataSource = _listcancel;
            rptcancelpolicy.DataBind();
            mpcancelpopup.Show();

        }
    }


    protected void linkbtnboardingpoints_Click(object sender, CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        if (Session["BoardingPoints"] != null)
        {
            DataTable dtboardingtbl = (DataTable)Session["BoardingPoints"];
            DataTable tblFiltered = dtboardingtbl.AsEnumerable().Where(row => row.Field<Int32>("data_Id") == id).CopyToDataTable();
            Repeater2.DataSource = tblFiltered;
            Repeater2.DataBind();
            mpboardingpoints.Show();
        }
        else
        {
            Repeater2.DataSource = null;
            Repeater2.DataBind();
        }
    }

    protected void linkbtndroppingpoints_Click(object sender, CommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        if (Session["DroppingPoints"] != null)
        {
            DataTable dtdropingtbl = (DataTable)Session["DroppingPoints"];
            DataTable tblFiltereds = dtdropingtbl.AsEnumerable().Where(row => row.Field<Int32>("data_Id") == id).CopyToDataTable();
            Repeater3.DataSource = tblFiltereds;
            Repeater3.DataBind();
            mpdroppingpoints.Show();
        }
        else
        {
            Repeater3.DataSource = null;
            Repeater3.DataBind();
        }
    }


    protected void btnviewseats_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string args = btn.CommandArgument;
        string[] tokens = args.Split('-');
        Hidden1.Value = tokens[0];

        var item = (RepeaterItem)btn.NamingContainer;
        var ddl = (Label)item.FindControl("lbldeparturedate");
        Session["traveldate"] = ddl.ToString();
        string dataid = tokens[1];
        string travelername = tokens[2];
        Session["travelername"] = travelername;
        Session["tripnewid"] = tokens[0];

        DataTable dtboardingtblnew = (DataTable)Session["maint"];
        DataTable tblFilterednew = dtboardingtblnew.AsEnumerable().Where(row => row.Field<Int32>("data_Id") == Convert.ToInt32(dataid)).CopyToDataTable();
        //int departuretimetime = Convert.ToInt32(tblFiltered.Rows[0]["departureTime"]);
        Session["cpolicy"] = tblFilterednew.Rows[0]["cancellationPolicy"].ToString();
        //Fill Boarding Points  
        if (Session["BoardingPoints"] != null)
        {
            DataTable dtboardingpoints = (DataTable)Session["BoardingPoints"];
            DataTable tblFiltered = dtboardingpoints.AsEnumerable().Where(row => row.Field<Int32>("data_Id") == Convert.ToInt32(dataid)).CopyToDataTable();
            ddboardingpoint.DataSource = tblFiltered;
            ddboardingpoint.DataTextField = "bpName";
            ddboardingpoint.DataValueField = "bpId";
            ddboardingpoint.DataBind();
            ddboardingpoint.Items.Insert(0, "Boarding Point");
        }
        //Fill Dropping Points
        if (Session["DroppingPoints"] != null)
        {
            DataTable dtdropingpoints = (DataTable)Session["DroppingPoints"];
            DataTable tblFiltereds = dtdropingpoints.AsEnumerable().Where(row => row.Field<Int32>("data_Id") == Convert.ToInt32(dataid)).CopyToDataTable();
            dddroppingpoint.DataSource = tblFiltereds;
            dddroppingpoint.DataTextField = "bpName";
            dddroppingpoint.DataValueField = "bpId";
            dddroppingpoint.DataBind();
            dddroppingpoint.Items.Insert(0, "Dropping Point");
        }
        pnlseatlayout.Attributes.Add("style", "display:block;");
        pnlseatlayout.Visible=true;
        ModalPopupseat.Show();


    }

    protected void btnHide_Click(object sender, EventArgs e)
    {
        hftotalFare.Value = "";
        hfselectedseats.Value = null;
        hfselectedseats1.Value = null;
        hfselectedseats2.Value = null;
        hfselectedseats3.Value = null;
        hfselectedseats4.Value = null;
        hfselectedseats5.Value = null;
        ModalPopupseat.Hide();
    }
    protected void btnnextseat_Click(object sender, EventArgs e)
    {
        if (hfselectedseats.Value == "" && hfselectedseats1.Value == "" && hfselectedseats2.Value == "" && hfselectedseats3.Value == "" && hfselectedseats4.Value == "" && hfselectedseats5.Value == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select at least one seat to continue');", true);
            ModalPopupseat.Show();
        }
        else
        {
            clearpassangerlist();
            Session["boardingpointid"] = ddboardingpoint.SelectedValue;
            Session["droppingpointid"] = dddroppingpoint.SelectedValue;
            string totalfare = hftotalFare.Value;
            string seat1 = hfselectedseats.Value;
            string seat2 = hfselectedseats1.Value;
            string seat3 = hfselectedseats2.Value;
            string seat4 = hfselectedseats3.Value;
            string seat5 = hfselectedseats4.Value;
            string seat6 = hfselectedseats5.Value;
            Session["totalfare"] = totalfare;
            if (seat1 != "")
            {
                lblseat1.Text = seat1;
                pnlmain.Visible = true;
            }
            else
            {
                hfselectedseats.Value = null;
                pnlmain.Visible = false;
            }
            if (seat2 != "")
            {
                lblcoseat1.Text = seat2;
                pnlcopsg1.Visible = true;
            }
            else
            {
                hfselectedseats1.Value = null;
                pnlcopsg1.Visible = false;
            }
            if (seat3 != "")
            {
                lblcoseat2.Text = seat3;
                pnlcopsg2.Visible = true;
            }
            else
            {
                hfselectedseats2.Value = null;
                pnlcopsg2.Visible = false;
            }
            if (seat4 != "")
            {
                lblcoseat3.Text = seat4;
                pnlcopsg3.Visible = true;
            }
            else
            {
                hfselectedseats3.Value = null;
                pnlcopsg3.Visible = false;
            }
            if (seat5 != "")
            {
                lblcoseat4.Text = seat5;
                pnlcopsg4.Visible = true;
            }
            else
            {
                hfselectedseats4.Value = null;
                pnlcopsg4.Visible = false;
            }
            if (seat6 != "")
            {
                lblcoseat5.Text = seat6;
                pnlcopsg5.Visible = true;
            }
            else
            {
                hfselectedseats5.Value = null;
                pnlcopsg5.Visible = false;
            }
            mppassanger.Show();
            ModalPopupseat.Hide();
        }


    }
    public void clearpassangerlist()
    {
        txtprimarypassanger.Text = "";
        txtage.Text = "";
        txtcopaasanger1.Text = "";
        txtcopassanger2.Text = "";
        txtcopassanger3.Text = "";
        txtcopassanger4.Text = "";
        txtcopassanger5.Text = "";
        txtage1.Text = "";
        txtage2.Text = "";
        txtage3.Text = "";
        txtage4.Text = "";
        txtage5.Text = "";
        txtmobile.Text = "";
        txtemail.Text = "";
        RadioButtonList1.ClearSelection();
        rbtcopsg1.ClearSelection();
        rbtcopsg2.ClearSelection();
        rbtcopsg3.ClearSelection();
        rbtcopsg4.ClearSelection();
        rbtcopsg5.ClearSelection();
    }

    protected void btnhideboarding0_Click(object sender, EventArgs e)
    {
        hftotalFare.Value = "";
        hfselectedseats.Value = "";
        hfselectedseats1.Value = "";
        hfselectedseats2.Value = "";
        hfselectedseats3.Value = "";
        hfselectedseats4.Value = "";
        hfselectedseats5.Value = "";
        mppassanger.Hide();
    }
    #endregion

    protected void btnnextprocess_Click(object sender, EventArgs e)
    {
        string sourceid = ddl_leavingfrom.SelectedValue;
        string destinationid = ddl_goingto.SelectedValue;
        string doj = TextBox1.Text;
        string tripid = Session["tripnewid"].ToString();
        string bpid = Session["boardingpointid"].ToString();
        string dpid = Session["droppingpointid"].ToString();
        string returnjson = "";
        DataTable dtCustmer = new DataTable();
        dtCustmer.Columns.Add("seatname", typeof(string));
        dtCustmer.Columns.Add("passengertitle", typeof(string));
        dtCustmer.Columns.Add("passengername", typeof(string));
        dtCustmer.Columns.Add("passengergender", typeof(string));
        dtCustmer.Columns.Add("passengerage", typeof(string));
        string mobile = txtmobile.Text;
        string email = txtemail.Text;
        Session["email"] = email;
        Session["mobile"] = mobile;
        if (txtprimarypassanger.Text != "" && txtage.Text != "" && RadioButtonList1.SelectedValue != "")
        {
            string seatname = lblseat1.Text;
            Session["seatname"] = seatname;
            string title = RadioButtonList1.SelectedItem.ToString();
            string name = txtprimarypassanger.Text;
            string age = txtage.Text;
            Session["page"] = txtage.Text;
            Session["pname"] = txtprimarypassanger.Text;
            dtCustmer.Rows.Add(seatname, title, name, "", age);
        }
        if (hfselectedseats1.Value != "")
        {
            if (txtcopaasanger1.Text != "" && txtage1.Text != "" && rbtcopsg1.SelectedValue != "")
            {
                string coseat1 = lblcoseat1.Text;
                string cotitle = rbtcopsg1.SelectedItem.ToString();
                string coname = txtcopaasanger1.Text;
                string coage = txtage1.Text;
                dtCustmer.Rows.Add(coseat1, cotitle, coname, "", coage);
            }
            else
            {

            }
        }
        if (hfselectedseats2.Value != "")
        {
            if (txtcopassanger2.Text != "" && txtage2.Text != "" && rbtcopsg2.SelectedValue != "")
            {
                string coseat2 = lblcoseat2.Text;
                string cotitle2 = rbtcopsg2.SelectedItem.ToString();
                string coname2 = txtcopassanger2.Text;
                string coage2 = txtage2.Text;
                dtCustmer.Rows.Add(coseat2, cotitle2, coname2, "", coage2);
            }
            else
            {

            }
        }
        if (hfselectedseats3.Value != "")
        {
            if (txtcopassanger3.Text != "" && txtage3.Text != "" && rbtcopsg3.SelectedValue != "")
            {
                string coseat3 = lblcoseat3.Text;
                string cotitle3 = rbtcopsg3.SelectedItem.ToString();
                string coname3 = txtcopassanger3.Text;
                string coage3 = txtage3.Text;
                dtCustmer.Rows.Add(coseat3, cotitle3, coname3, "", coage3);
            }
            else
            {

            }
        }
        if (hfselectedseats4.Value != "")
        {
            if (txtcopassanger4.Text != "" && txtage4.Text != "" && rbtcopsg4.SelectedValue != "")
            {
                string coseat4 = lblcoseat4.Text;
                string cotitle4 = rbtcopsg4.SelectedItem.ToString();
                string coname4 = txtcopassanger4.Text;
                string coage4 = txtage4.Text;
                dtCustmer.Rows.Add(coseat4, cotitle4, coname4, "", coage4);
            }
            else
            {

            }
        }
        if (hfselectedseats5.Value != "")
        {
            if (txtcopassanger5.Text != "" && txtage5.Text != "" && rbtcopsg5.SelectedValue != "")
            {
                string coseat5 = lblcoseat5.Text;
                string cotitle5 = rbtcopsg5.SelectedItem.ToString();
                string coname5 = txtcopassanger5.Text;
                string coage5 = txtage5.Text;
                dtCustmer.Rows.Add(coseat5, cotitle5, coname5, "", coage5);
            }
            else
            {

            }

        }
        returnjson = ConvertDataTabletoString(dtCustmer);
        string json = returnjson;

        string result = ibus.Seat_Block(sourceid, destinationid, doj, tripid, bpid, dpid, mobile, email, "", "", "", json);
        if (result.Contains("timed out") == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + result + "');", true);
        }
        else
        {
            dynamic myObject = JsonConvert.DeserializeObject(result);
            DataSet myObjectDT = Deserialize(result);
            string txn = myObjectDT.Tables[0].Rows[0]["statuscode"].ToString();
            string errorstatus = myObjectDT.Tables[0].Rows[0]["status"].ToString();
            if (txn == "TXN")
            {
                string bookingid = myObjectDT.Tables[1].Rows[0]["booking_id"].ToString();
                string booking_status = myObjectDT.Tables[1].Rows[0]["booking_status"].ToString();
                string sourcenew = ddl_leavingfrom.SelectedItem.ToString();
                string destination = ddl_goingto.SelectedItem.ToString();
                string totalfare = Session["totalfare"].ToString();
                btnpaynow.Text = "PAY" + " " + totalfare;
                lblsource0.Text = sourcenew;
                lblsource1.Text = destination;
                lblblockseats1.Text = (hfselectedseats.Value + "," + hfselectedseats1.Value + "," + hfselectedseats2.Value + "," + hfselectedseats3.Value + "," + hfselectedseats4.Value + "," + hfselectedseats5.Value);
                string[] loginNames = lblblockseats1.Text.Split(',');

                string aa = string.Empty;
                foreach (string loginName in loginNames)
                {
                    if (loginName != ",") aa = aa + loginName + ",";
                }
                aa = aa.TrimEnd(',');
                lblblockseats1.Text = aa;
                Session["bookingid"] = bookingid;
                Session["SeatsBlocked"] = lblblockseats1.Text.TrimEnd(',');
                lbltravellernames.Text = Session["travelername"].ToString();
                //string travelername = (item.FindControl("lbltravelsname") as Label).Text;
                //Label lab = item.FindControl("lbltravelsname") as Label;
                //lbltravellernames.Text = lab.Text;
                mpbookedsummary.Show();
                pnlbookedsummary.Attributes.Add("style", "display:block;");
                mppassanger.Hide();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + errorstatus + "');", true);
            }
        }

    }
    public string ConvertDataTabletoString(DataTable dt)
    {
        string Json = JsonConvert.SerializeObject(dt);
        return Json;
    }
    protected void btnpaynow_Click(object sender, EventArgs e)
    {
        if (Session["bookingid"] != null)
        {
            string recomfirmfare = ibus.Reconfirm_fare(Session["bookingid"].ToString());
            dynamic myObjectfare = JsonConvert.DeserializeObject(recomfirmfare);
            DataSet myObjectDTfare = Deserialize(recomfirmfare);
            string txnfare = myObjectDTfare.Tables[0].Rows[0]["statuscode"].ToString();
            string errorstatusfare = myObjectDTfare.Tables[0].Rows[0]["status"].ToString();
            if (txnfare == "TXN")
            {
                string ETranId = string.Empty;
                ETranId = clsm.Cyrus_GetTransactionID_New();
                decimal NetAmount = Convert.ToDecimal(Session["totalfare"].ToString());
                int res = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), Convert.ToInt32(ViewState["MsrNo"]));
                if (res == 1)
                {
                    int tra = clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal("-" + NetAmount.ToString().Trim()), "Dr", " Bus Ticket Txn:- " + ETranId + "");
                    if (tra > 0)
                    {
                        string result = ibus.Book_Ticket(Session["bookingid"].ToString(), ETranId, NetAmount.ToString(), ViewState["MemberId"].ToString());
                        cls.select_data_dt("INSERT INTO bustest VALUES ('" + result + "')");
                        if (result.Contains("timed out") == true)
                        {
                            clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount.ToString().Trim()), "Cr", "  Fail Bus Ticket Txn :- " + ETranId + "");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + result + "');", true);
                        }
                        else
                        {                           
                            double comamount = TotupAmount(Convert.ToDouble(NetAmount), ViewState["MemberId"].ToString());
                            dynamic myObject = JsonConvert.DeserializeObject(result);
                            DataSet myObjectDT = Deserialize(result);
                            string txn = myObjectDT.Tables[0].Rows[0]["statuscode"].ToString();
                            string errorstatus = myObjectDT.Tables[0].Rows[0]["status"].ToString();
                            if (txn == "TXN")
                            {
                                clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(comamount.ToString().Trim()), "Cr", " Bus Ticket Comission Txn:- " + ETranId + "");
                                string bookingid = myObjectDT.Tables[1].Rows[0]["booking_id"].ToString();
                                string booking_status = myObjectDT.Tables[1].Rows[0]["booking_status"].ToString();
                                string charged_amt = myObjectDT.Tables[1].Rows[0]["charged_amt"].ToString();
                                string ipay_id = myObjectDT.Tables[1].Rows[0]["ipay_id"].ToString();
                                string pemail = Session["email"].ToString();
                                string mobile = Session["mobile"].ToString();
                                string page = Session["page"].ToString();
                                string pname = Session["pname"].ToString();
                                List<ParmList> _lstparm = new List<ParmList>();
                                _lstparm.Add(new ParmList() { name = "@Action", value = 'I' });
                                _lstparm.Add(new ParmList() { name = "@booking_id", value = bookingid });
                                _lstparm.Add(new ParmList() { name = "@byMsrno ", value = Convert.ToInt32(ViewState["MsrNo"]) });
                                _lstparm.Add(new ParmList() { name = "@bookingstatus", value = booking_status });
                                _lstparm.Add(new ParmList() { name = "@ipayid", value = ipay_id });
                                _lstparm.Add(new ParmList() { name = "@pnrno", value = "" });
                                _lstparm.Add(new ParmList() { name = "@transactionid", value = ETranId });
                                _lstparm.Add(new ParmList() { name = "@bookingfee", value = charged_amt });
                                _lstparm.Add(new ParmList() { name = "@Amount", value = NetAmount.ToString() });
                                _lstparm.Add(new ParmList() { name = "@pickuplocationaddress", value = "" });
                                _lstparm.Add(new ParmList() { name = "@pickupcontact", value = "" });
                                _lstparm.Add(new ParmList() { name = "@pname", value = pname });
                                _lstparm.Add(new ParmList() { name = "@pemail", value = pemail });
                                _lstparm.Add(new ParmList() { name = "@page", value = page });
                                _lstparm.Add(new ParmList() { name = "@pgender", value = "" });
                                _lstparm.Add(new ParmList() { name = "@mobile", value = mobile });
                                _lstparm.Add(new ParmList() { name = "@seatname", value = Session["SeatsBlocked"].ToString() });
                                _lstparm.Add(new ParmList() { name = "@inventoryid", value = "" });
                                _lstparm.Add(new ParmList() { name = "@droplocation", value = "" });
                                _lstparm.Add(new ParmList() { name = "@droptime", value = "" });
                                _lstparm.Add(new ParmList() { name = "@doj", value = Session["traveldate"].ToString() });
                                _lstparm.Add(new ParmList() { name = "@cancellationpolicy", value = Session["cpolicy"].ToString() });
                                _lstparm.Add(new ParmList() { name = "@Updatedatetime", value = DateTime.Now });
                                _lstparm.Add(new ParmList() { name = "@travelername", value = Session["travelername"].ToString() });
                                _lstparm.Add(new ParmList() { name = "@commission", value = Convert.ToDecimal(comamount) });
                                cls.select_data_dtNew("[ezulix_busbooking]", _lstparm);
                                string[] valueArray = new string[2];
                                valueArray[0] = ViewState["MemberId"].ToString();
                                valueArray[1] = myObjectDT.Tables[1].Rows[0]["booking_id"].ToString();
                                SMS.SendWithVar(mobile, 99, valueArray, 1);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Ticket Has Been booked Successfully');", true);
                            }

                            else if (txn == "EZX")
                            {
                                clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount.ToString().Trim()), "Cr", "  Fail Bus Ticket Txn :- " + ETranId + "");

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + errorstatus + "');", true);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');window.location ='DashBoard.aspx';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');window.location ='DashBoard.aspx';", true);
                }
            }
        }
    }
    public double TotupAmount(double amount, string memberid)
    {
       // int msrNo = Convert.ToInt32(ViewState["Msrno"].ToString());
        double NetAmount = 0;
        double surcharge_amt = 0; double Commission = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            DataTable dtMemberMaster = cls.select_data_dt("select * from tblmlm_membermaster where memberid='"+ memberid+"'");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt("select * from Ezulix_Bus_Commission where PackageID='" + PackageID + "' order by id");
            if (dtsr.Rows.Count > 0)
            {
                Commission = Convert.ToDouble(dtsr.Rows[0]["surcharge"].ToString());

                if (Commission > 0)
                {

                    Commission = (Convert.ToDouble(amount) * Commission) / 100;

                }
                //txttopupAmount_Charges.Text = surcharge_amt.ToString();
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
    #region repeaterevents
    protected void rptrCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblprice = (Label)e.Item.FindControl("lblprice");
            string dataid = lblprice.ToolTip;
            DataTable dtfare = (DataTable)Session["fares"];
            var minLPData = dtfare.AsEnumerable().Where(row => row.Field<Int32>("data_Id") == Convert.ToInt32(dataid))
                  .Select(fa => fa.Field<string>("fares_text"))
                  .Min(fa => fa);

            lblprice.Text = "INR" + " " + minLPData.ToString().Split('.')[0];

        }

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbldeparturetime = (Label)e.Item.FindControl("lbldeparturetime");
            int departuretimetime = Convert.ToInt32(lbldeparturetime.Text);
            int departuretimeQuotient = departuretimetime / 60;
            int departuretimeremanider = departuretimetime % 60;
            int departuretimejourneyday = departuretimeQuotient / 24;
            int departuretimehours = departuretimeQuotient % 24;
            int departuretimeminutes = departuretimeremanider;
            if (departuretimejourneyday == 0)
            {
                string departuretimeoutputs = departuretimehours + ":" + departuretimeminutes;
                TimeSpan departuretimetimespan = new TimeSpan(departuretimehours, departuretimeminutes, 00);
                DateTime departuretimeoutputtime = Convert.ToDateTime(TextBox1.Text).Add(departuretimetimespan);
                string departuretimedisplayTime = departuretimeoutputtime.ToString("hh:mm tt");
                TimeSpan departuretimeduration = Convert.ToDateTime(departuretimedisplayTime) - Convert.ToDateTime(departuretimedisplayTime);
                lbldeparturetime.Text = departuretimedisplayTime;
                Label lbldeparturedate = (Label)e.Item.FindControl("lbldeparturedate");
                lbldeparturedate.Text = departuretimeoutputtime.ToString();
            }
            Label lblarivaltime = (Label)e.Item.FindControl("lblarivaltime");
            Label lblarivaldate = (Label)e.Item.FindControl("lblarivaldate");
            int arivaltimetime = Convert.ToInt32(lblarivaltime.Text);
            int arivaltimeQuotient = arivaltimetime / 60;
            int arivaltimeremanider = arivaltimetime % 60;
            int arivaltimejourneyday = arivaltimeQuotient / 24;
            int arivaltimehours = arivaltimeQuotient % 24;
            int arivaltimeminutes = arivaltimeremanider;
            if (arivaltimejourneyday == 0)
            {
                string arivaltimeoutputs = arivaltimehours + ":" + arivaltimeminutes;
                TimeSpan arivaltimespan = new TimeSpan(arivaltimehours, arivaltimeminutes, 00);
                DateTime arivaltimeoutputtime = Convert.ToDateTime(TextBox1.Text).Add(arivaltimespan);
                string arivaltimedisplayTime = arivaltimeoutputtime.ToString("hh:mm tt");
                TimeSpan arivaltimeduration = Convert.ToDateTime(arivaltimedisplayTime) - Convert.ToDateTime(arivaltimedisplayTime);
                lblarivaltime.Text = arivaltimedisplayTime;
                lblarivaldate.Text = arivaltimeoutputtime.ToString();
            }
            else
            {
                string arivaltimeoutputs = arivaltimehours + ":" + arivaltimeminutes;
                TimeSpan arivaltimespan = new TimeSpan(arivaltimehours, arivaltimeminutes, 00);
                DateTime arivaltimeoutputtime = Convert.ToDateTime(TextBox1.Text).Add(arivaltimespan);
                DateTime newDate = arivaltimeoutputtime.AddDays(1);
                string arivaltimedisplayTime = arivaltimeoutputtime.ToString("hh:mm tt");
                TimeSpan arivaltimeduration = Convert.ToDateTime(arivaltimedisplayTime) - Convert.ToDateTime(arivaltimedisplayTime);
                lblarivaldate.Text = newDate.ToString();
                lblarivaltime.Text = arivaltimedisplayTime;
            }
            Label lblduration = (Label)e.Item.FindControl("lblduration");
            string starttime = lbldeparturetime.Text;
            string endtime = lblarivaltime.Text;
            DateTime start = DateTime.Parse(starttime);
            DateTime end = DateTime.Parse(endtime);
            if (start > end)
                end = end.AddDays(1);
            TimeSpan duration = end.Subtract(start);
            string[] a = duration.ToString().Split(':');
            lblduration.Text = a[0] + "h" + " " + a[1] + "m";
        }
    }
    protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbldeparturetime = (Label)e.Item.FindControl("lbltime");
            int departuretimetime = Convert.ToInt32(lbldeparturetime.Text);
            int departuretimeQuotient = departuretimetime / 60;
            int departuretimeremanider = departuretimetime % 60;
            int departuretimejourneyday = departuretimeQuotient / 24;
            int departuretimehours = departuretimeQuotient % 24;
            int departuretimeminutes = departuretimeremanider;
            if (departuretimejourneyday == 0)
            {
                string departuretimeoutputs = departuretimehours + ":" + departuretimeminutes;
                TimeSpan departuretimetimespan = new TimeSpan(departuretimehours, departuretimeminutes, 00);
                DateTime departuretimeoutputtime = DateTime.Today.Add(departuretimetimespan);
                string departuretimedisplayTime = departuretimeoutputtime.ToString("hh:mm tt");
                TimeSpan departuretimeduration = Convert.ToDateTime(departuretimedisplayTime) - Convert.ToDateTime(departuretimedisplayTime);
                lbldeparturetime.Text = departuretimedisplayTime;
            }
            else
            {
                string departuretimeoutputs = departuretimehours + ":" + departuretimeminutes;
                TimeSpan departuretimetimespan = new TimeSpan(departuretimehours, departuretimeminutes, 00);
                DateTime departuretimeoutputtime = DateTime.Today.Add(departuretimetimespan);
                string departuretimedisplayTime = departuretimeoutputtime.ToString("hh:mm tt");
                TimeSpan departuretimeduration = Convert.ToDateTime(departuretimedisplayTime) - Convert.ToDateTime(departuretimedisplayTime);
                lbldeparturetime.Text = departuretimedisplayTime;
            }
        }

    }

    protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbldeparturetime = (Label)e.Item.FindControl("lbltime0");
            int departuretimetime = Convert.ToInt32(lbldeparturetime.Text);
            int departuretimeQuotient = departuretimetime / 60;
            int departuretimeremanider = departuretimetime % 60;
            int departuretimejourneyday = departuretimeQuotient / 24;
            int departuretimehours = departuretimeQuotient % 24;
            int departuretimeminutes = departuretimeremanider;
            if (departuretimejourneyday == 0)
            {
                string departuretimeoutputs = departuretimehours + ":" + departuretimeminutes;
                TimeSpan departuretimetimespan = new TimeSpan(departuretimehours, departuretimeminutes, 00);
                DateTime departuretimeoutputtime = DateTime.Today.Add(departuretimetimespan);
                string departuretimedisplayTime = departuretimeoutputtime.ToString("hh:mm tt");
                TimeSpan departuretimeduration = Convert.ToDateTime(departuretimedisplayTime) - Convert.ToDateTime(departuretimedisplayTime);
                lbldeparturetime.Text = departuretimedisplayTime;
            }
            else
            {
                string departuretimeoutputs = departuretimehours + ":" + departuretimeminutes;
                TimeSpan departuretimetimespan = new TimeSpan(departuretimehours, departuretimeminutes, 00);
                DateTime departuretimeoutputtime = DateTime.Today.Add(departuretimetimespan);
                string departuretimedisplayTime = departuretimeoutputtime.ToString("hh:mm tt");
                TimeSpan departuretimeduration = Convert.ToDateTime(departuretimedisplayTime) - Convert.ToDateTime(departuretimedisplayTime);
                lbldeparturetime.Text = departuretimedisplayTime;
            }
        }

    }
    #endregion
}