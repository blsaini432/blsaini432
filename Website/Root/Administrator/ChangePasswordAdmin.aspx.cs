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

public partial class Root_Admin_ChangePasswordAdmin : System.Web.UI.Page
{
    #region [Properties]
    cls_Universal objUniversal = new cls_Universal();
    DataTable dtUniversal = new DataTable();

    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();

    DataTable dtMemberMaster = new DataTable();
    clsEmployee objEmployee = new clsEmployee();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int id = 0;
            if (Session["IdNo"] != null)
            {
                id = Convert.ToInt32(Session["IdNo"]);
            }
            else if (Session["EmployeeID"] != null)
            {
                id = Convert.ToInt32(Session["EmployeeID"]);
            }
            Int32 intresult = 0;
            intresult = objUniversal.UpdatePassword("UpdateAdminSelfPassword", id, txtConfirmPassword.Text, txtCurrentPassword.Text);
            if (intresult > 0)
            {

                dtMemberMaster = objEmployee.ManageEmployee("Get", id);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Current password is not valid !');", true);

               
            }
        }
        catch (Exception ex)
        {
         
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