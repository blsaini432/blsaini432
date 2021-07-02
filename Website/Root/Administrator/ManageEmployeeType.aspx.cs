using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_SuperAdmin_ManageEmployeeType : System.Web.UI.Page
{
    #region [Properties]
    clsEmployeeType objEmployeeType = new clsEmployeeType();

    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update EmployeeType";
            }
            else
            {
                lblAddEdit.Text = "Add EmployeeType";
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
            intresult = objEmployeeType.AddEditEmployeeType(0, txtEmployeeTypeName.Text, "");
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|EmployeeType Name Already Exists !');", true);
              
            }
            #endregion
        }
        else
        {
            #region [Update]
            Int32 intresult = 0;
            intresult = objEmployeeType.AddEditEmployeeType(Convert.ToInt32(Request.QueryString["id"]), txtEmployeeTypeName.Text, "");

            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|EmployeeType Name Already Exists !');", true);
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
        txtEmployeeTypeName.Text = "";
    }

    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objEmployeeType.ManageEmployeeType("GetAll", id);

        if (dt.Rows.Count > 0)
        {
            txtEmployeeTypeName.Text = Convert.ToString(dt.Rows[0]["EmployeeTypeName"]);
        }
    }
    #endregion
}