using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ManageDept : System.Web.UI.Page
{
    #region [Properties]
    clsTSS_Dept objDept = new clsTSS_Dept();
   
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update Department";
            }
            else
            {
                lblAddEdit.Text = "Add Department";
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
            intresult = objDept.AddEditDept(0, txtDeptName.Text);
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Dept Name Already Exists !');", true);
            }
            #endregion
        }
        else
        {
            #region [Update]
            Int32 intresult = 0;
            intresult = objDept.AddEditDept(Convert.ToInt32(Request.QueryString["id"]), txtDeptName.Text);
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Dept Name Already Exists !');", true);
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
        txtDeptName.Text = "";
    }

    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objDept.ManageDept("GetAll", id);

        if (dt.Rows.Count > 0)
        {
            txtDeptName.Text = Convert.ToString(dt.Rows[0]["DeptName"]);
        }
    }
    #endregion
}