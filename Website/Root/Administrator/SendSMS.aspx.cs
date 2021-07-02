using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Configuration;
using System.Net;
using System.IO;
using ASPSnippets.SmsAPI;

public partial class SendSMS : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();

    clsState objState = new clsState();
    cls_connection cls = new cls_connection();
    string condition = " MsrNo > 0";
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cls.fill_MemberType(ddlMemberType, "");
            fillState(1);
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //SMS.get_SMSBaseURL(MobileNo, sms);
            string MobileNo = "";
            if (txtToMobile.Text.EndsWith(","))
                MobileNo = txtToMobile.Text.Substring(0, txtToMobile.Text.Length - 1);
            else
                MobileNo = txtToMobile.Text;
            string sms = txtSMS.Text;
            SMS.get_SMSBaseURL(MobileNo, sms, 1, "A");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //string pro = "2";
            //string baseurl = "http://sms.click4bulksms.in/sendsms?uname=paynewera&pwd=paynew@er&senderid=PAYERA&to=" + MobileNo + "&msg=" + sms + "&route=A";
            string baseurl = SMS.get_SMSBaseURL(MobileNo, sms, 1, "A");
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();


            //SMS.APIType = SMSGateway.Site2SMS;
            //SMS.MashapeKey = "jdF6O02O8QjwcJfzWhZCZecU1eQZeDEv";
            //SMS.Username = "9983554400";
            //SMS.Password = "XXXX";
            //if (txtToMobile.Text.Trim().IndexOf(",") == -1)
            //{
            //    //Single SMS
            //    SMS.SendSms(txtToMobile.Text.Trim(), sms);
            //}
            //else
            //{
            //    //Multiple SMS
            //    List<string> numbers = txtToMobile.Text.Trim().Split(',').ToList();
            //    SMS.SendSms(numbers, sms);
            //}


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('SMS Send Successfully !');", true);
            clear();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There is a problem in SMS server, Please try again later !');", true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    #endregion

    #region [Reset]
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
    }
    #endregion

    #region [All Functions]
    private void clear()
    {
        txtToMobile.Text = "";
        txtSMS.Text = "";
    }

    private void FillData()
    {
        dtMemberMaster = objMemberMaster.ManageMemberMaster("GetAll", 0);
        if (dtMemberMaster.Rows.Count > 0)
        {
            for (int i = 0; i < dtMemberMaster.Rows.Count; i++)
            {
                if (Convert.ToString(dtMemberMaster.Rows[i]["Mobile"]) != "")
                {

                    txtToMobile.Text = txtToMobile.Text + "91" + Convert.ToString(dtMemberMaster.Rows[i]["Mobile"]) + ",";
                }
            }
        }
    }
    private void FillGrid()
    {
        if (!(string.IsNullOrEmpty(txtfromdate.Text) | string.IsNullOrEmpty(txttodate.Text)))
        {
            condition = condition + " and ( LastLoginDate <= '" + txtfromdate.Text + "' OR LastLoginDate  >= '" + Convert.ToDateTime(txttodate.Text).AddDays(1) + "')";
        }
        if (txtmobile.Text.Trim() != "")
        {
            condition = condition + " and [Mobile] like '%" + txtmobile.Text.Trim() + "%'";
        }
        if (txtuse.Text.Trim() != "")
        {
            condition = condition + " and [MemberName] like '%" + txtuse.Text.Trim() + "%'";
        }
        if (txtemail.Text.Trim() != "")
        {
            condition = condition + " and [Email] like '%" + txtemail.Text.Trim() + "%'";
        }
        if (txtMemberID.Text.Trim() != "")
        {
            condition = condition + " and [MemberID] like '%" + txtMemberID.Text.Trim() + "%'";
        }
        if (ddlStateName.SelectedValue != "0")
        {
            condition = condition + " and [StateName]='" + ddlStateName.SelectedItem.Text + "'";
        }
        if (txtcity.Text.Trim() != "")
        {
            condition = condition + " and [CityName] like '%" + txtcity.Text.Trim() + "%'";
        }
        if (ddlMemberType.SelectedValue != "0")
        {
            condition = condition + " and [MemberType] ='" + ddlMemberType.SelectedItem.Text + "'";
        }
        dtMemberMaster = objMemberMaster.ManageMemberMaster("GetByView", 0);
        dtMemberMaster.DefaultView.RowFilter = condition;
        gvMemberMaster.DataSource = dtMemberMaster;
        gvMemberMaster.DataBind();
    }

    public void fillState(int CountryID)
    {
        DataTable dtState = new DataTable();
        dtState = objState.ManageState("GetByCountryID", CountryID);
        ddlStateName.DataSource = dtState;
        ddlStateName.DataValueField = "StateID";
        ddlStateName.DataTextField = "StateName";
        ddlStateName.DataBind();
        ddlStateName.Items.Insert(0, new ListItem("Select State Name", "0"));

    }

    #endregion
    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        FillData();
        tdGrid.Visible = false;
        txtToMobile.Text = "";
        FillData();
    }
    protected void rdbSelected_CheckedChanged(object sender, EventArgs e)
    {
        FillGrid();
        tdGrid.Visible = true;
        txtToMobile.Text = "";
    }

    protected void gvMemberMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMemberMaster.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void btnLeft_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow grow in gvMemberMaster.Rows)
        {
            CheckBox chkRow = (CheckBox)grow.FindControl("chkRow");
            if (chkRow.Checked)
            {
                if (Convert.ToString(gvMemberMaster.Rows[grow.RowIndex].Cells[4].Text) != "&nbsp;")
                {
                    txtToMobile.Text = txtToMobile.Text + gvMemberMaster.Rows[grow.RowIndex].Cells[4].Text + ",";
                }
            }
        }
    }
    protected void gvMemberMaster_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}