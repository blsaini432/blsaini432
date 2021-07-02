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
using BLL;

public partial class Root_Distributor_Offlinebill_payder : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    //EzulixBBPSAPI eBbps = new EzulixBBPSAPI();
    cls_connection oBJCONNECTION = new cls_connection();
    clsState objState = new clsState();
    DataTable dt = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                DataTable dtMemberMaster = new DataTable();
                dtMemberMaster = (DataTable)Session["dtDistributor"];
                string Memberid = dtMemberMaster.Rows[0]["MemberId"].ToString();
                int Msrno = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                ViewState["Msrno"] = Msrno;
                ViewState["Memberid"] = Memberid;
                FilleBoard();
                fillState();

                string mac = GetMACAddress();
                string ipaddress = GetIPAddress();
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
            int Msrno = Convert.ToInt32(ViewState["Msrno"]);
            string agentid = clsm.Cyrus_GetTransactionID_New();
            string servicekey = ddl_Eboard.SelectedValue;
            string knumber = txt_knumber.Text;
            string amount =txt_amount.Text;
            string state = ddlStateName.SelectedItem.ToString();
            DataTable dtchkm = new DataTable();
            dtchkm = Cls.select_data_dt("select * from Tbl_Ezulix_offlinebill where account_no='" + knumber + "' and sp_key='" + servicekey + "' and trans_amt='" + txt_amount.Text+ "' and statu in('SUCCESS','Pending')");
            if (dtchkm.Rows.Count >= 0)
            {
                string Result = string.Empty;
                int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), Msrno);
                if (result > 0)
                {
                    int a = clsm.Wallet_MakeTransaction(ViewState["Memberid"].ToString(), Convert.ToDecimal("-" + txt_amount.Text), "Dr", "PayElecBill:" + agentid + "");
                    if (a == 1)
                    {
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = "" });
                        _lstparm.Add(new ParmList() { name = "@agent_id", value = agentid });
                        _lstparm.Add(new ParmList() { name = "@opr_id", value = "" });
                        _lstparm.Add(new ParmList() { name = "@account_no", value = knumber });
                        _lstparm.Add(new ParmList() { name = "@sp_key", value = servicekey });
                        _lstparm.Add(new ParmList() { name = "@trans_amt", value = amount });
                        _lstparm.Add(new ParmList() { name = "@statu", value = "Pending" });
                        _lstparm.Add(new ParmList() { name = "@res_code", value = "" });
                        _lstparm.Add(new ParmList() { name = "@duedate", value = txt_date.Text });
                        _lstparm.Add(new ParmList() { name = "@customername", value = txt_name.Text });
                        _lstparm.Add(new ParmList() { name = "@customermobile", value = txt_mobile.Text});
                        _lstparm.Add(new ParmList() { name = "@state", value = state });
                        _lstparm.Add(new ParmList() { name = "@billunit", value = txt_billunite.Text });
                        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(ViewState["Msrno"]) });
                        _lstparm.Add(new ParmList() { name = "@memberid", value = ViewState["Memberid"].ToString() });
                        Cls.select_data_dtNew("Set_Ezulix_offlinebill", _lstparm);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Successfull!');location.replace('Electricity_report_payder.aspx');", true);
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
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('contact your admin!');", true);
            clear();
        }
    }

  


    #region methods

   

    public void clear()
    {
        ddl_Eboard.SelectedIndex = 0;
       // GridView1.Visible = false;
       // txt_servicenum.Text = string.Empty;
      //  tr_service.Visible = false;
       // btn_Getbill.Visible = false;
       // GridView1.Visible = false;
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

    public void fillState()
    {
        DataTable dtState = new DataTable();
        dtState = objState.ManageState("GetByCountryID", 1);
        ddlStateName.DataSource = dtState;
        ddlStateName.DataValueField = "StateID";
        ddlStateName.DataTextField = "StateName";
        ddlStateName.DataBind();
        ddlStateName.Items.Insert(0, new ListItem("Select State", "0"));
    }
    #endregion
}