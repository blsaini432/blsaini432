using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ManageSS : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
            }
            else
            {
                
            }
        }
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
            List<ParmList> _list = new List<ParmList>();
            _list.Add(new ParmList() { name = "@Name", value = txt_servicename.Text });
            _list.Add(new ParmList() { name = "@Description", value = txt_description.Text });
            _list.Add(new ParmList() { name = "@Action", value = "SI" });
            cls.select_data_dtNew("Sp_ServiceFeeSettings", _list);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
            clear();
            #endregion
        }
        else
        {
            #region [Update]
            List<ParmList> _list = new List<ParmList>();
            _list.Add(new ParmList() { name = "@Name", value = txt_servicename.Text });
            _list.Add(new ParmList() { name = "@Description", value = txt_description.Text });
            _list.Add(new ParmList() { name = "@Id", value = Convert.ToInt32(Request.QueryString["id"]) });
            _list.Add(new ParmList() { name = "@Action", value = "SUI" });
            cls.select_data_dtNew("Sp_ServiceFeeSettings", _list);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
            clear();
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
        txt_servicename.Text = "";
      
    }

    private void FillData(int id)
    {
        clsMLM_Package objPackage = new clsMLM_Package();
        DataTable dt = new DataTable();
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Name", value = txt_servicename.Text });
        _list.Add(new ParmList() { name = "@Id", value = Convert.ToInt32(Request.QueryString["id"]) });
        _list.Add(new ParmList() { name = "@Action", value = "UI" });
        dt = cls.select_data_dtNew("Sp_ServiceFeeSettings", _list);
        if (dt.Rows.Count > 0)
        {
            txt_servicename.Text = Convert.ToString(dt.Rows[0]["Name"]);
            txt_description.Text = dt.Rows[0]["Description"].ToString();
        }
    }
    #endregion
}