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

public partial class Portals_Admin_Managensdlsetting : System.Web.UI.Page
{

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillPackage();
            package();
        }
    }

    public void package()
    {
        cls_connection objconnection = new cls_connection();
        DataTable dd = new DataTable();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "getL" });
        cls_connection cls = new cls_connection();
        dd = cls.select_data_dtNew("Sp_ManageNSDLSetting ", _lstparm);
        if (dd.Rows.Count > 0)
        {
            GridView1.DataSource = dd;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        try
        {
            DataTable dt = new DataTable();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Amount", value = txtcomission.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
            _lstparm.Add(new ParmList() { name = "@Action", value = "L" });
            cls_connection cls = new cls_connection();
            dt = cls.select_data_dtNew("Sp_ManageNSDLSetting ", _lstparm);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDecimal(txtcomission.Text) > 0)
                {
                    List<ParmList> _lstparms = new List<ParmList>();
                    _lstparms.Add(new ParmList() { name = "@Amount", value = txtcomission.Text.Trim() });
                    _lstparms.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
                    _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                    cls.select_data_dtNew("Sp_ManageNSDLSetting ", _lstparms);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    package();
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('basic');", true);
                }

            }
            else
            {
                if (Convert.ToDecimal(txtcomission.Text) > 0)
                {
                    List<ParmList> _lstparms = new List<ParmList>();
                    _lstparms.Add(new ParmList() { name = "@Amount", value = txtcomission.Text.Trim() });
                    _lstparms.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
                    _lstparms.Add(new ParmList() { name = "@Action", value = "I" });
                    cls.select_data_dtNew("Sp_ManageNSDLSetting ", _lstparms);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    package();
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('basic');", true);
                }

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('basic');", true);
        }
        #endregion
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
        txtcomission.Text = "";
        ddlPackage.SelectedIndex = 0;
    }


    public void fillPackage()
    {
        clsMLM_Package objPackage = new clsMLM_Package();
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
        DataTable dt = new DataTable();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
        _lstparm.Add(new ParmList() { name = "@Action", value = "L" });
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dtNew("Sp_ManageNSDLSetting ", _lstparm);
        if (dt.Rows.Count > 0)
        {
            decimal commision = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
            txtcomission.Text = commision.ToString();
        }
        else
        {
            txtcomission.Text = "0.00";
        }
    }
}