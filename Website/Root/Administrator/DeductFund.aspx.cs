using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
public partial class Root_Admin_DeductFund : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    clsMLM_Mix objMix = new clsMLM_Mix();

    DataTable dtEWalletBalance = new DataTable();
    DataTable dtEWalletTransaction = new DataTable();
    DataTable dtMix = new DataTable();
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                BindDropdown();
            }
            else
            {
                Response.Redirect("~/adminlogin.aspx");
            }
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        try
        {
            if (Session["dtEmployee"] != null)
            {
                int cnt = 0;
                DataTable dtemployee = (DataTable)Session["dtEmployee"];
                if (dtemployee.Rows.Count > 0)
                {
                    if (ddl_members.SelectedIndex > 0)
                    {
                        if (Convert.ToDecimal(txtAmount.Text) > 0)
                        {
                            string narration = "Admin(" + dtemployee.Rows[0]["EmployeeName"].ToString() + ") - Deduct Fund " + txtNarration.Text;
                            cnt = clsm.Wallet_Deductfund(Convert.ToInt32(hidMsrNo.Value), ddl_members.SelectedValue, Convert.ToDecimal(txtAmount.Text), narration, hidMobile.Value);
                            if (cnt > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');;location.replace('deductFund.aspx');", true);
                                clear();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Member Id not exists !');", true);
                            }


                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Amount Should Be greater than Zero');", true);
                        }



                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select MemberId');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Please try Later !');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error| Please try Later');", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Database Error !');", true);
        }
        #endregion
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
        ddl_members.SelectedIndex = 0;
        hidMsrNo.Value = "";
        lblMemberName.Text = "";
        lblEWalletBalance.Text = "0.0";
        txtAmount.Text = "";
        txtNarration.Text = "";
        lblEWalletBalance.Text = "";
    }

    
    #endregion

    public void BindDropdown()
    {
        string data = "";
        DataTable dtEWalletTransaction = new DataTable();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@data", value = data });
        dtEWalletTransaction = cls.select_data_dtNew("get_member", _lstparm);
        ddl_members.DataSource = dtEWalletTransaction;
        ddl_members.DataTextField = "MemberName";
        ddl_members.DataValueField = "MemberId";
        ddl_members.DataBind();
        ddl_members.Items.Insert(0, new ListItem("Select Member", "0"));
    }

    protected void ddl_members_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_members.SelectedValue!="" && ddl_members.SelectedIndex > 0)
        {
            dtMix = objMix.MLM_UniversalSearch("GetMemberMasterByMemberID", ddl_members.SelectedValue, "", "", 0, 0, 0);
            if (dtMix.Rows.Count > 0)
            {
                lblMemberName.Text = Convert.ToString(dtMix.Rows[0]["MemberName"]);
                hidMsrNo.Value = Convert.ToString(dtMix.Rows[0]["MsrNo"]);
                hidMobile.Value = Convert.ToString(dtMix.Rows[0]["Mobile"]);
                #region GetBalanceByMsrNo
                dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(hidMsrNo.Value));

                if (dtEWalletBalance.Rows.Count > 0)
                {
                    lblEWalletBalance.Text = Convert.ToString(dtEWalletBalance.Rows[0]["Balance"]);
                }
                #endregion
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Please Enter Valid MemberID !');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Please Select MemberID !');", true);

        }
    }
}