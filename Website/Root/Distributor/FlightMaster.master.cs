using System;
using System.Web.UI;
using System.Data;

public partial class Customer_FlightMaster : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFooter();
        }
    }

    #region Events
    protected void btn_Dashboard_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.location ='DashBoard.aspx';", true);
        }
        catch (Exception)
        {
            AjaxMessageBox(this, "!Error");
        }
    }

    protected void btn_Logout_Click(object sender, EventArgs e)
    {
        try
        {
            Session["dtDistributor"] = null;
            Response.Redirect("~/Root/login");
        }
        catch (Exception)
        {
            AjaxMessageBox(this, "!Error");
        }
    }
    #endregion

    #region Method
    public static void AjaxMessageBox(Control page, string msg)
    {
        string script = "alert('" + msg + "')";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "UserSecurity", script, true);
    }

    private void BindFooter()
    {
        try
        {
            cls_connection Cls = new cls_connection();
            DataTable dt = Cls.select_data_dt(@"select Copyright from tblCompany WHERE CompanyID=2");
            Lbl_Copyright.Text = dt.Rows[0]["Copyright"].ToString();
        }
        catch (Exception)
        {
            AjaxMessageBox(this, "!Error");
        }
    }
    #endregion


}
