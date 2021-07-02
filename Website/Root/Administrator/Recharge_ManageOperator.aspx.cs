using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ManageOperator : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillServiceType();
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update Operator";
            }
            else
            {
                lblAddEdit.Text = "Add Operator";
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
            intresult = objOperator.AddEditOperator(0, txtOperatorName.Text, txtOperatorCode.Text, Convert.ToInt32(ddlServiceType.SelectedItem.Value), Convert.ToInt32(0));
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);

                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Operator Code Already Exists !');", true);
               
            }
            #endregion
        }
        else
        {
            #region [Update]
            Int32 intresult = 0;
            intresult = objOperator.AddEditOperator(Convert.ToInt32(Request.QueryString["id"]), txtOperatorName.Text, txtOperatorCode.Text, Convert.ToInt32(ddlServiceType.SelectedItem.Value), Convert.ToInt32(0));
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Operator Code Already Exists !');", true);
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
        txtOperatorName.Text = "";
        txtOperatorCode.Text = "";
        ddlServiceType.SelectedIndex = 0;
       
    }

    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objOperator.ManageOperator("GetAll", id);

        if (dt.Rows.Count > 0)
        {
            txtOperatorName.Text = Convert.ToString(dt.Rows[0]["OperatorName"]);
            txtOperatorCode.Text = Convert.ToString(dt.Rows[0]["OperatorCode"]);
            ddlServiceType.SelectedValue = Convert.ToString(dt.Rows[0]["ServiceTypeID"]);
            
        }
    }

    public void fillServiceType()
    {
        dtServiceType = objServiceType.ManageServiceType("Get", 0);
        ddlServiceType.DataSource = dtServiceType;
        ddlServiceType.DataValueField = "ServiceTypeID";
        ddlServiceType.DataTextField = "ServiceTypeName";
        ddlServiceType.DataBind();
        ddlServiceType.Items.Insert(0, new ListItem("Select Service Type", "0"));
    }
 
    #endregion
}