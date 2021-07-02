using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Root_Admin_SMSApiMaster : System.Web.UI.Page
{

  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["SMSID"] != null)
            {
                DataTable dtAPI = new DataTable();
                cls_connection cls = new cls_connection();
                dtAPI = cls.select_data_dt("Proc_Recharge_SMSApi 'getDataById'," + Request.QueryString["SMSID"].ToString());
                txtAPIName.Text = dtAPI.Rows[0]["APIName"].ToString();
                txtURL.Text = dtAPI.Rows[0]["URL"].ToString();
                txtprm1.Text = dtAPI.Rows[0]["prm1"].ToString();
                txtprm1val.Text = dtAPI.Rows[0]["prm1val"].ToString();
                txtprm2.Text = dtAPI.Rows[0]["prm2"].ToString();
                txtprm2val.Text = dtAPI.Rows[0]["prm2val"].ToString();
                txtprm3.Text = dtAPI.Rows[0]["prm3"].ToString();
                txtprm3val.Text = dtAPI.Rows[0]["prm3val"].ToString();
                txtprm4.Text = dtAPI.Rows[0]["prm4"].ToString();
                txtprm4val.Text = dtAPI.Rows[0]["prm4val"].ToString();
                txtprm5.Text = dtAPI.Rows[0]["prm5"].ToString();
                txtprm5val.Text = dtAPI.Rows[0]["prm5val"].ToString();
                Txtprm6.Text = dtAPI.Rows[0]["prm6"].ToString();
                txtprm7.Text = dtAPI.Rows[0]["prm7"].ToString();
                Txtprm8.Text = dtAPI.Rows[0]["prm8"].ToString();
                btnSubmit.Text = "Update";
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Update")
        {
            UpdateData();
        }
        else
        {
            InsertData();
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
    }

    public void clear()
    {
        txtAPIName.Text = "";
        txtURL.Text = "";
        txtprm1val.Text = "";
        txtprm2val.Text = "";
        txtprm3val.Text = "";
        txtprm4val.Text = "";
        txtprm5val.Text = "";
        Txtprm6.Text = "";
        txtprm7.Text = "";
        Txtprm8.Text = "";
    }
    private void InsertData()
    {
        string str = @"Proc_Recharge_SMSApi 'InsertData',0,'" + txtAPIName.Text.Trim() + "','" + txtURL.Text.Trim() + "'," +
            "'" + txtprm1.Text.Trim() + "','" + txtprm1val.Text.Trim() + "','" + txtprm2.Text.Trim() + "','" + txtprm2val.Text.Trim() + "','" + txtprm3.Text.Trim() + "'," +
            "'" + txtprm3val.Text.Trim() + "','" + txtprm4.Text.Trim() + "','" + txtprm4val.Text.Trim() + "','" + txtprm5.Text.Trim() + "'," +
            "'" + txtprm5val.Text.Trim() + "','" + Txtprm6.Text.Trim() + "','" + txtprm7.Text.Trim() + "','" + Txtprm8.Text.Trim() + "'";
        cls_connection cls = new cls_connection();
        cls.insert_data(str);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
        clear();

    }


    private void UpdateData()
    {
        string str = @"Proc_Recharge_SMSApi 'UpdateData'," + Request.QueryString["SMSID"].ToString() + ",'" + txtAPIName.Text.Trim() + "','" + txtURL.Text.Trim() + "'," +
            "'" + txtprm1.Text.Trim() + "','" + txtprm1val.Text.Trim() + "','" + txtprm2.Text.Trim() + "','" + txtprm2val.Text.Trim() + "','" + txtprm3.Text.Trim() + "'," +
            "'" + txtprm3val.Text.Trim() + "','" + txtprm4.Text.Trim() + "','" + txtprm4val.Text.Trim() + "','" + txtprm5.Text.Trim() + "'," +
            "'" + txtprm5val.Text.Trim() + "','" + Txtprm6.Text.Trim() + "','" + txtprm7.Text.Trim() + "','" + Txtprm8.Text.Trim() + "'";
        cls_connection cls = new cls_connection();
        cls.insert_data(str);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
        clear();
    }
}