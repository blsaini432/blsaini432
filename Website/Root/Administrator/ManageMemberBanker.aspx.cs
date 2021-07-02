using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ManageMemberBanker : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_MemberBanker objMemberBanker = new clsMLM_MemberBanker();
    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();
    DataTable dtMemberBanker = new DataTable();

    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillBankName();
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update Member Banker";
            }
            else
            {
                lblAddEdit.Text = "Add Member Banker";
            }
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null)
        {
            #region [Insert]
            Int32 intresult = 0;
            DateTime ApproveDate = DateTime.Now;
            intresult = objMemberBanker.AddEditMemberBanker(0, Convert.ToInt32(ddlBankName.SelectedItem.Value), txtBankBranch.Text, ddlAccountType.SelectedItem.Text, txtAccountNumber.Text, txtIFSCCode.Text, txtBankDesc.Text, 1, ApproveDate, "", true);
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);

                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Banker Already Exists !');", true);
              
            }
            #endregion
        }
        else
        {
            #region [Update]
            Int32 intresult = 0;
            DateTime ApproveDate = DateTime.Now;
            intresult = objMemberBanker.AddEditMemberBanker(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(ddlBankName.SelectedItem.Value), txtBankBranch.Text, ddlAccountType.SelectedItem.Text, txtAccountNumber.Text, txtIFSCCode.Text, txtBankDesc.Text, 1, ApproveDate, "", true);
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Banker Already Exists !');", true);
            }
            #endregion
        }
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
        ddlBankName.SelectedIndex = 0;
        txtBankBranch.Text = "";
        ddlAccountType.SelectedIndex = 0;
        txtAccountNumber.Text = "";
        txtIFSCCode.Text = "";
        txtBankDesc.Text = "";
    }

    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objMemberBanker.ManageMemberBanker("GetAll", id);
        if (dt.Rows.Count > 0)
        {
            fillBankName();
            ddlBankName.SelectedValue = Convert.ToString(dt.Rows[0]["BankerMasterID"]);
            txtBankBranch.Text = Convert.ToString(dt.Rows[0]["BankBranch"]);
            ddlAccountType.SelectedValue = Convert.ToString(dt.Rows[0]["AccountType"]);
            ddlAccountType.SelectedItem.Text = Convert.ToString(dt.Rows[0]["AccountType"]);
            txtAccountNumber.Text = Convert.ToString(dt.Rows[0]["AccountNumber"]);
            txtIFSCCode.Text = Convert.ToString(dt.Rows[0]["IFSCCode"]);
            txtBankDesc.Text = Convert.ToString(dt.Rows[0]["BankDesc"]);
        }
    }

    public void fillBankName()
    {
        clsMLM_BankerMaster objBankerMaster = new clsMLM_BankerMaster();
        DataTable dtBankerMaster = new DataTable();
        dtBankerMaster = objBankerMaster.ManageBankerMaster("Get", 0);
        ddlBankName.DataSource = dtBankerMaster;
        ddlBankName.DataValueField = "BankerMasterID";
        ddlBankName.DataTextField = "BankerMasterName";
        ddlBankName.DataBind();
        ddlBankName.Items.Insert(0, new ListItem("Select Bank", "0"));
    }
    #endregion



}