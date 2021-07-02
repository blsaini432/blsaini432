using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ManageBankerMaster : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_BankerMaster objBankerMaster = new clsMLM_BankerMaster();
    DataTable dtBankerMaster = new DataTable();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update Banker Master";
            }
            else
            {
                lblAddEdit.Text = "Add Banker Master";
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
            intresult = objBankerMaster.AddEditBankerMaster(0, txtBankName.Text);
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Bank Name Already Exists !');", true);
                
            }
            #endregion
        }
        else
        {
            #region [Update]
            Int32 intresult = 0;
            intresult = objBankerMaster.AddEditBankerMaster(Convert.ToInt32(Request.QueryString["id"]), txtBankName.Text);
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Bank Name Already Exists !');", true);
                
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
        txtBankName.Text = "";
    }

    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objBankerMaster.ManageBankerMaster("GetAll", id);

        if (dt.Rows.Count > 0)
        {
            txtBankName.Text = Convert.ToString(dt.Rows[0]["BankerMasterName"]);
        }
    }
    #endregion
}