using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ManagePackage : System.Web.UI.Page
{

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fillmembership();
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update Package";
            }
            else
            {
                lblAddEdit.Text = "Add Package";
            }
        }
    }
    protected void Fillmembership()
    {
        cls_connection objconnection = new cls_connection();
        DataTable dt = new DataTable();
        dt = objconnection.select_data_dt("Select * from tblmlm_membership where isactive=1 order by membertypeid");
        ddlMemberType.DataSource = dt;
        ddlMemberType.DataTextField = "membertype";
        ddlMemberType.DataValueField = "id";
        ddlMemberType.DataBind();
        ddlMemberType.Items.Insert(0, new ListItem("Select Member Type", "0"));
    }
    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        clsMLM_Package objPackage = new clsMLM_Package();
        DataTable dtOperator = new DataTable();
        clsRecharge_Operator objOperator = new clsRecharge_Operator();
        cls_connection objconnection = new cls_connection();
        if (Request.QueryString["id"] == null)
        {
            #region [Insert]
            Int32 intresult = 0;
            intresult = objPackage.AddEditPackage(0, txtPackageName.Text, Convert.ToDecimal(0), 1, Convert.ToDecimal(0), Convert.ToDecimal(ddlMemberType.SelectedValue), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), "", "", "",true, Convert.ToDecimal(0));
            if (intresult > 0)
            {
                dtOperator = objOperator.ManageOperator("GetAll", 0);
                for (int i = 0; i < dtOperator.Rows.Count; i++)
                {
                    int j = objconnection.insert_data("insert into tblRecharge_Commission (OperatorID, Commission, PackageID, ActiveAPI) values (" + Convert.ToInt32(dtOperator.Rows[i]["OperatorID"]) + "," + 0 + "," + intresult + "," + 1 + ")");
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);

                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Package Name Already Exists !');", true);
                
            }
            #endregion
        }
        else
        {
            #region [Update]
            Int32 intresult = 0;
            intresult = objPackage.AddEditPackage(Convert.ToInt32(Request.QueryString["id"]), txtPackageName.Text, Convert.ToDecimal(0), 1, Convert.ToDecimal(0), Convert.ToDecimal(ddlMemberType.SelectedValue), Convert.ToDecimal(0), Convert.ToDecimal(0), Convert.ToDecimal(0), "", "", "", true, Convert.ToDecimal(0));

            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);

                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Package Name Already Exists !');", true);
              
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
        txtPackageName.Text = "";
      
    }

    private void FillData(int id)
    {
        clsMLM_Package objPackage = new clsMLM_Package();
        DataTable dt = new DataTable();
        dt = objPackage.ManagePackage("GetAll", id);

        if (dt.Rows.Count > 0)
        {
            
            txtPackageName.Text = Convert.ToString(dt.Rows[0]["PackageName"]);
        }
    }
    #endregion
}