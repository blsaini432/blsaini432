using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
public partial class Root_Retailer_bbpsnew : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    EzulixBBPSAPI eBbps = new EzulixBBPSAPI();
    cls_connection oBJCONNECTION = new cls_connection();
    DataTable dt = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dtMemberMaster = new DataTable();
                dtMemberMaster = (DataTable)Session["dtRetailer"];
                //dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='bbps', @msrno=" + dtMemberMaster.Rows[0]["MsrNo"] + "");
                //if (dt.Rows.Count > 0)
                //{
                //    if (Convert.ToBoolean(dt.Rows[0]["setbbps"]) == false)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('BBPS Service is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                //    }
                //    else
                //    {

                {
                    string Memberid = dtMemberMaster.Rows[0]["MemberId"].ToString();
                    int Msrno = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                    ViewState["Msrno"] = Msrno;
                    ViewState["Memberid"] = Memberid;
                    FilleBoard();

                    string mac = GetMACAddress();
                    string ipaddress = GetIPAddress();
                }
                //}

            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }


    protected void btn_Getbill_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Eboard.SelectedValue == "DGE" || ddl_Eboard.SelectedValue == "MGE" || ddl_Eboard.SelectedValue == "PGE" || ddl_Eboard.SelectedValue == "UGE")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Bill Fetch facility is not avaliable for this operator');", true);
            }
            else
            {
                Session["Paybill"] = null;
                string Result = string.Empty;
                string agentid = clsm.Cyrus_GetTransactionID_New();
                string servicekey = ddl_Eboard.SelectedValue;
                string account = txt_servicenum.Text.Trim();
                string mac = GetMACAddress();
                string ipaddress = GetIPAddress();
                string json = txt_servicenum.Text;
                if (ddl_Eboard.SelectedValue == "MDE")
                {
                    json = txtconsumernumbermaharshtra.Text.Trim();
                    Result = eBbps.bill_fetch_Maharshtra(servicekey, agentid, txt_custmermobilemaharshtra.Text.Trim(), "AGT", ipaddress, mac, "Cash", "bill", "", "", "24.1568,78.5263", "1878", json, txtbillingunitmahashtra.Text.Trim());
                }
                else if (ddl_Eboard.SelectedValue == "JBE")
                {
                    json = txt_servicejha.Text.Trim();
                    Result = eBbps.bill_fetch_Jha(servicekey, agentid, txt_custmermobilejha.Text.Trim(), "AGT", ipaddress, mac, "Cash", "bill", "", "", "24.1568,78.5263", "1878", json, ddl_jhasub.SelectedValue);
                }
                else if (ddl_Eboard.SelectedValue == "AEE" || ddl_Eboard.SelectedValue == "ASE")
                {

                    Result = eBbps.bill_fetch(servicekey, agentid, txt_customermobile.Text.Trim(), "AGT", ipaddress, mac, "Wallet", "Wallet|" + txt_customermobile.Text.Trim() + "", "", "", "24.1568,78.5263", "1878", json);
                }
                else
                {
                    Result = eBbps.bill_fetch(servicekey, agentid, txt_customermobile.Text.Trim(), "AGT", ipaddress, mac, "Cash", "bill", "", "", "24.1568,78.5263", "1878", json);
                }
                if (Result != string.Empty)
                {
                    DataSet dsres = Deserialize(Result);
                    if (dsres.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                    {
                        GridView1.Visible = true;
                        DataTable dm = dsres.Tables[1];
                        GridView1.DataSource = dm;
                        GridView1.DataBind();
                        Session["Paybill"] = dsres.Tables[1];

                    }
                    else if (dsres.Tables[0].Rows[0]["statuscode"].ToString() == "ETO")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Pending');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsres.Tables[0].Rows[0]["status"].ToString() + "');", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Service Provider Error! Contact your admin');", true);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "pay")
            {

                string agentid = clsm.Cyrus_GetTransactionID_New();
                string servicekey = ddl_Eboard.SelectedValue;
                string account = txt_servicenum.Text.Trim();
                string refrenceno = e.CommandArgument.ToString();
                string mac = GetMACAddress();
                string ipaddress = GetIPAddress();
                string json = txt_servicenum.Text;
                DataTable dtbill = (DataTable)Session["Paybill"];
                DataTable tblFiltered = dtbill.AsEnumerable().Where(row => row.Field<String>("reference_id") == refrenceno).CopyToDataTable();
                DataTable dtchkm = new DataTable();
                dtchkm = Cls.select_data_dt("select * from tbl_ezulix_ele where account_no='" + account + "' and sp_key='" + servicekey + "' and trans_amt='" + tblFiltered.Rows[0]["dueamount"].ToString() + "' and statu in('SUCCESS','Pending')");
                if (dtchkm.Rows.Count >= 0)
                {
                    string Result = string.Empty;
                    int cHKBALANSE = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(tblFiltered.Rows[0]["dueamount"].ToString()), Convert.ToInt32(ViewState["Msrno"]));
                    if (cHKBALANSE == 1)
                    {

                        int a = clsm.Wallet_MakeTransaction(ViewState["Memberid"].ToString(), Convert.ToDecimal("-" + tblFiltered.Rows[0]["dueamount"].ToString()), "Dr", "PayEleBill:" + agentid + "");
                        if (a == 1)
                        {
                            List<ParmList> _lstparm = new List<ParmList>();
                            _lstparm.Add(new ParmList() { name = "@ipay_id", value = "" });
                            _lstparm.Add(new ParmList() { name = "@agent_id", value = agentid });
                            _lstparm.Add(new ParmList() { name = "@opr_id", value = "" });
                            _lstparm.Add(new ParmList() { name = "@account_no", value = tblFiltered.Rows[0]["billnumber"].ToString() });
                            _lstparm.Add(new ParmList() { name = "@sp_key", value = servicekey });
                            _lstparm.Add(new ParmList() { name = "@trans_amt", value = tblFiltered.Rows[0]["dueamount"].ToString() });
                            _lstparm.Add(new ParmList() { name = "@statu", value = "Pending" });
                            _lstparm.Add(new ParmList() { name = "@res_code", value = "" });
                            _lstparm.Add(new ParmList() { name = "@duedate", value = (tblFiltered.Rows[0]["duedate"]) });
                            _lstparm.Add(new ParmList() { name = "@customername", value = tblFiltered.Rows[0]["customername"].ToString() });
                            _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(ViewState["Msrno"]) });
                            _lstparm.Add(new ParmList() { name = "@memberid", value = ViewState["Memberid"].ToString() });
                            Cls.select_data_dtNew("Set_Ezulix_Ele", _lstparm);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Successfull!');location.replace('Electricitytranscation.aspx');", true);
                            clear();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficient Wallet Balance!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficient Wallet Balance!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficient Wallet Balance!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to admin');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('contact your admin!');", true);
            clear();
        }
    }
    #region methods

    protected void ddl_Eboard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_Eboard.SelectedIndex > 0)
            {
                string Result = string.Empty;
                string sp_key = ddl_Eboard.SelectedValue;
                DataTable dtservice = (DataTable)Session["Eboard"];
                DataTable tblFiltered = dtservice.AsEnumerable().Where(row => row.Field<String>("servicename") == ddl_Eboard.SelectedItem.Text).CopyToDataTable();
                txt_servicenum.Text = string.Empty;
                tr_service.Visible = true;
                btn_Getbill.Visible = true;
                lbl_servicetag.Text = tblFiltered.Rows[0]["txtplace"].ToString();
                GridView1.Visible = false;
                //  btn_PaybillGujrat.Visible = false;
                dvGujrat.Visible = false;
                dvMaharashtra.Visible = false;
                if (ddl_Eboard.SelectedValue == "DGE" || ddl_Eboard.SelectedValue == "MGE" || ddl_Eboard.SelectedValue == "PGE" || ddl_Eboard.SelectedValue == "UGE")
                {
                    dvGujrat.Visible = true;
                    dvMaharashtra.Visible = false;
                    tr_service.Visible = false;
                    btn_Getbill.Visible = false;
                    //  btn_PaybillGujrat.Visible = true;
                    lblservicetaggujrat.Text = tblFiltered.Rows[0]["txtplace"].ToString();
                }
                else if (ddl_Eboard.SelectedValue == "MDE")
                {
                    dvGujrat.Visible = false;
                    dvMaharashtra.Visible = true;
                    dv_jha.Visible = false;
                    tr_service.Visible = false;
                    lblserviceMaharashtra.Text = tblFiltered.Rows[0]["txtplace"].ToString();
                }

                else if (ddl_Eboard.SelectedValue == "JBE")
                {
                    dvGujrat.Visible = false;
                    dvMaharashtra.Visible = false;
                    dv_jha.Visible = true;
                    tr_service.Visible = false;
                    lbl_servicetagjha.Text = tblFiltered.Rows[0]["txtplace"].ToString();
                    FillJha();
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Electricity Board to proceed');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact your admin');", true);
        }
    }


    public void clear()
    {
        ddl_Eboard.SelectedIndex = 0;
        GridView1.Visible = false;
        txt_servicenum.Text = string.Empty;
        tr_service.Visible = false;
        btn_Getbill.Visible = false;
        GridView1.Visible = false;
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
    private void FillJha()
    {
        DataTable dt = Cls.select_data_dt(@"EXEC Set_Electricity @action='jha'");
        ddl_jhasub.DataSource = dt;
        ddl_jhasub.DataTextField = "sub_name";
        ddl_jhasub.DataValueField = "sub_code";
        ddl_jhasub.DataBind();
        ddl_jhasub.Items.Insert(0, new ListItem("Select sub division", "0"));
    }
    void FilleBoard()
    {
        Session["Eboard"] = null;
        dt = Cls.select_data_dt(@"EXEC Set_Electricity @action='ddlboard'");
        Session["Eboard"] = dt;
        ddl_Eboard.DataSource = dt;
        ddl_Eboard.DataTextField = "servicename";
        ddl_Eboard.DataValueField = "servicekey";
        ddl_Eboard.DataBind();
        ddl_Eboard.Items.Insert(0, new ListItem("Select electricity board", "0"));
    }
    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
    public string GetMACAddress()
    {
        string macAddresses = "";

        foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }
    #endregion
}