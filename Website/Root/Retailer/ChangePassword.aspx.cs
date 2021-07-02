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

public partial class Root_Member_ChangePassword : System.Web.UI.Page
{
    #region [Properties]
    cls_Universal objUniversal = new cls_Universal();
    DataTable dtUniversal = new DataTable();

    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();

    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();

    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] == null)
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtRetailer"] != null)
            {
                int id = 0;
                dtMemberMaster = (DataTable)Session["dtRetailer"];

                id = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                Int32 intresult = 0;
                intresult = objUniversal.UpdatePassword("UpdateMemberPassword", id, txtConfirmPassword.Text, txtCurrentPassword.Text);
                if (intresult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Password changed successfully !');", true);

                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Current password is not valid !');", true);

                }
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Occured');", true);
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
        txtConfirmPassword.Text = "";
        txtCurrentPassword.Text = "";
        txtNewPassword.Text = "";
    }


    #endregion
}