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

public partial class SendEmail : System.Web.UI.Page
{

    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();

    clsState objState = new clsState();
    cls_connection cls = new cls_connection();
    string condition = " MsrNo > 0";
  


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cls.fill_MemberType(ddlMemberType, "");
            fillState(1);
        }
    }

 

  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string str = "";
            if (txtToEmail.Text.EndsWith(","))
                str = txtToEmail.Text.Substring(0, txtToEmail.Text.Length - 1);
            else
                str = txtToEmail.Text;
            FlexiMail objSendMail = new FlexiMail();
            objSendMail.To = "support@srdealonline.com";
            objSendMail.CC = "";
            objSendMail.BCC = str;
            objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
            objSendMail.FromName = "srdealonline.com";
            objSendMail.MailBodyManualSupply = true;
            objSendMail.Subject = txtSubject.Text;
            objSendMail.MailBody = fckBody.Text;
            objSendMail.Send();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Mail Send Successfully !');", true);
            clear();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('There is a problem in MailBox, Please try again later !');", true);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
  


    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
    }



    private void clear()
    {
        txtToEmail.Text = "";
        txtSubject.Text = "";
        fckBody.Text = "";
    }

    private void FillData()
    {
        dtMemberMaster = objMemberMaster.ManageMemberMaster("GetAll", 0);
        if (dtMemberMaster.Rows.Count > 0)
        {
            for (int i = 0; i < dtMemberMaster.Rows.Count; i++)
            {
                if (Convert.ToString(dtMemberMaster.Rows[i]["Email"]) != "")
                {
                    txtToEmail.Text = txtToEmail.Text + Convert.ToString(dtMemberMaster.Rows[i]["Email"]) + ",";
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


    protected void rdbAll_CheckedChanged(object sender, EventArgs e)
    {
        //FillData();
        tdGrid.Visible = false;
        txtToEmail.Text = "";
        FillData();
    }
    protected void rdbSelected_CheckedChanged(object sender, EventArgs e)
    {
        FillGrid();
        tdGrid.Visible = true;
        txtToEmail.Text = "";
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
                if (Convert.ToString(gvMemberMaster.Rows[grow.RowIndex].Cells[3].Text) != "&nbsp;")
                {
                    txtToEmail.Text = txtToEmail.Text + gvMemberMaster.Rows[grow.RowIndex].Cells[3].Text + ",";
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