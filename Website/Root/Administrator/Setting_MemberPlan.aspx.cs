using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
public partial class Root_Admin_Setting_MemberPlan : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    DataTable dtExport = new DataTable();

    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();

    clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();

    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    string condition = " SerialNo > 0";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fillOperator(10);
            cls.fill_MemberType(ddlSourceMember, "");
            cls.fill_MemberType(ddlmymembertype, "");
            //fillPackage();
        }
    }
    protected void ddlSourceMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        cls.fill_MemberType(ddlmymembertype, ddlSourceMember.SelectedItem.Text);
    }
    protected void ddlMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select * from tblmlm_memberplan_settings where membertype='" + ddlSourceMember.SelectedValue + "' and targetmembertype='" + ddlmymembertype.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {

            DSO1_tot.Text = (Convert.ToDouble(dt.Rows[0]["Netamount"]).ToString()).ToString();
        }
        else
        {
            DSO1_tot.Text = "0";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlSourceMember.SelectedValue) > 0 && Convert.ToInt32(ddlmymembertype.SelectedValue) > 0 && DSO1_tot.Text != "")
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("Exec AddEdit_MemberPlan_Settings '" + ddlSourceMember.SelectedValue + "','" + ddlmymembertype.SelectedValue + "','" + DSO1_tot.Text.Trim() + "'");
            if (dt.Rows.Count > 0)
            {
                int result = Convert.ToInt32(dt.Rows[0]["idno"].ToString());
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Setting not updated !!');", true);
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error in process !!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Configuration error !!');", true);
        }
    }
}