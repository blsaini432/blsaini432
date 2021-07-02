using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.IO;

public partial class Root_Admin_ManageCommission : System.Web.UI.Page
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
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "D" });
        _lstparm.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
        cls_connection cls = new cls_connection();
        cls.select_data_dtNew("sp_set_RechargeCommission", _lstparm);
        foreach (GridViewRow row in gvOperator.Rows)
        {
            int OperatorID = Convert.ToInt32(gvOperator.DataKeys[row.RowIndex].Value);
            TextBox txtCommission = (TextBox)row.FindControl("txtCommission");
            decimal Commission = Convert.ToDecimal(txtCommission.Text);
            CheckBox chkSurcharge = (CheckBox)row.FindControl("chkSurcharge");
            CheckBox chkFlat = (CheckBox)row.FindControl("chkFlat");
            int APIID = Convert.ToInt32(row.Cells[3].Text);
            List<ParmList> _lstparmS = new List<ParmList>();
            _lstparmS.Add(new ParmList() { name = "@Action", value = "I" });
            _lstparmS.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
            _lstparmS.Add(new ParmList() { name = "@OperatorID", value = OperatorID });
            _lstparmS.Add(new ParmList() { name = "@Amount", value = Commission });
            _lstparmS.Add(new ParmList() { name = "@ActiveAPI", value = APIID });
            _lstparmS.Add(new ParmList() { name = "@IsFlat", value = chkFlat.Checked });
            _lstparmS.Add(new ParmList() { name = "@IsSurcharge", value = chkSurcharge.Checked });
            cls.select_data_dtNew("sp_set_RechargeCommission ", _lstparmS);
          
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');location.replace('Recharge_ManageCommission.aspx');", true);
        #endregion
    }
    #endregion

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

    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlPackage.SelectedItem.Value) == 0)
        {
            gvOperator.DataSource = dtOperator;
            gvOperator.DataBind();
        }
        else
        {
           
            dtOperator = objconnection.select_data_dt("Exec Ravi_ManageCommission_List 0,'" + ddlPackage.SelectedValue + "','admin'");
            DataView dv = dtOperator.DefaultView;
            dv.Sort = "ServiceType desc";
            DataTable sortedDT = dv.ToTable();
            gvOperator.DataSource = sortedDT;
            gvOperator.DataBind();
        }
    }

  
}