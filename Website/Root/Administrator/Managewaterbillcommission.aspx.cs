using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Root_Admin_Managewaterbillcommission : System.Web.UI.Page
{

    #region [Properties]
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_connection objconnection = new cls_connection();
    #endregion


    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillPackage();
        }
    }
    #endregion
    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        Int32 intresult = 0;
       int i = objconnection.delete_data("delete from Tbl_waterbill_comm where PackageID=" + Convert.ToInt32(ddlPackage.SelectedValue));
        foreach (GridViewRow row in gvOperator.Rows)
        {
            string ServiceName = row.Cells[1].Text;
           // string servicekey = row.Cells[2].Text;
            TextBox txtCommission = (TextBox)row.FindControl("txtCommission");
            decimal Commission;
            if (txtCommission.Text == "" || txtCommission.Text == null)
            {
                Commission = Convert.ToDecimal(0.00);
            }
            else
            {
                Commission = Convert.ToDecimal(txtCommission.Text);
            }
            CheckBox chkFlat = (CheckBox)row.FindControl("chkFlat");
            intresult = objconnection.insert_data("EXEC PROC_water_COM @Action='I',@PackageId=" + Convert.ToInt32(ddlPackage.SelectedValue) + ",@ServiceName='" + ServiceName + "',@Amount='" + Commission + "',@IsFlat='" + chkFlat.Checked + "'");
        }
        if (intresult > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|There are some problem, Please try again !');", true);
        }
        #endregion
    }
    #endregion

    #region [All Functions]

    public void fillPackage()
    {
        DataTable dtPackage = new DataTable();
        dtPackage = objPackage.ManagePackage("GetByMsrNo", 1);
        ddlPackage.DataSource = dtPackage;
        ddlPackage.DataValueField = "PackageID";
        ddlPackage.DataTextField = "PackageName";
        ddlPackage.DataBind();
        ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
    }
    #endregion
    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlPackage.SelectedItem.Value) == 0)
        {
            gvOperator.DataSource = dtOperator;
            gvOperator.DataBind();
        }
        else
        {
            dtOperator = objconnection.select_data_dt("Exec PROC_water_COM @Action='ADMIN',@PackageId='" + ddlPackage.SelectedItem.Value + "'");
            if (dtOperator.Rows.Count > 0)
            {
                gvOperator.DataSource = dtOperator;
                gvOperator.DataBind();
            }
            else
            {
                dtOperator = objconnection.select_data_dt("Exec PROC_water_COM @Action='NPKG'");
                gvOperator.DataSource = dtOperator;
                gvOperator.DataBind();
            }
        }
    }
}