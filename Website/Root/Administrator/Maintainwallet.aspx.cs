using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Configuration;
using System.Web.Services;

public partial class Root_Admin_Maintainwallet : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    clsMLM_Mix objMix = new clsMLM_Mix();

    DataTable dtEWalletBalance = new DataTable();
    DataTable dtEWalletTransaction = new DataTable();
    DataTable dtMix = new DataTable();
    cls_myMember clsm = new cls_myMember();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                Fillmembership();
            }
            else
            {
                Response.Redirect("~/adminlogin.aspx");
            }
        }
    }
    #endregion


    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        try
        {
            cls_connection cls = new cls_connection();
            if (Session["dtEmployee"] != null)
            {
                DataTable dtemployee = (DataTable)Session["dtEmployee"];
                DataTable dtchklist = new DataTable();
                  List<ParmList> _lstparms = new List<ParmList>();
                _lstparms.Add(new ParmList() { name = "@membertype", value = ddl_membertype.SelectedValue });
                _lstparms.Add(new ParmList() { name = "@Wallet", value = ddl_wallet.SelectedValue });
                _lstparms.Add(new ParmList() { name = "@action", value = "L" });
                dtchklist = cls.select_data_dtNew("sp_maintainwallet", _lstparms);
                if(dtchklist.Rows.Count > 0)
                {
                    DataTable dtt = new DataTable();
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@membertype", value = ddl_membertype.SelectedValue });
                    _lstparm.Add(new ParmList() { name = "@Wallet", value = ddl_wallet.SelectedValue });
                    _lstparm.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(txtAmount.Text.Trim()) });
                    _lstparm.Add(new ParmList() { name = "@action", value = "U" });
                    dtt = cls.select_data_dtNew("sp_maintainwallet", _lstparm);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');;location.replace('Maintainwallet.aspx');", true);
                    clear();
                }
                else
                {
                    DataTable dtt = new DataTable();
                    List<ParmList> _lstparm = new List<ParmList>();

                    _lstparm.Add(new ParmList() { name = "@membertype", value = ddl_membertype.SelectedValue });
                    _lstparm.Add(new ParmList() { name = "@Wallet", value = ddl_wallet.SelectedValue });
                    _lstparm.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(txtAmount.Text.Trim()) });
                    _lstparm.Add(new ParmList() { name = "@action", value = "I" });
                    dtt = cls.select_data_dtNew("sp_maintainwallet", _lstparm);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');;location.replace('Maintainwallet.aspx');", true);
                    clear();
                }
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Database Error !');", true);
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
        ddl_wallet.SelectedIndex = 0;
        txtAmount.Text = "";
    }
    #endregion

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
    }
    #endregion


    protected void Fillmembership()
    {
        cls_connection objconnection = new cls_connection();
        DataTable dt = new DataTable();
        dt = objconnection.select_data_dt("Select * from tblmlm_membership where isactive=1 order by membertypeid");
        ddl_membertype.DataSource = dt;
        ddl_membertype.DataTextField = "membertype";
        ddl_membertype.DataValueField = "membertypeid";
        ddl_membertype.DataBind();
        ddl_membertype.Items.Insert(0, new ListItem("Select Member Type", "0"));
    }


    protected void ddl_wallet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddl_wallet.SelectedIndex >0)
        {
             cls_connection cls = new cls_connection();
             if (Session["dtEmployee"] != null)
             {
                 DataTable dtemployee = (DataTable)Session["dtEmployee"];
                 DataTable dtchklist = new DataTable();
                 List<ParmList> _lstparms = new List<ParmList>();

                 _lstparms.Add(new ParmList() { name = "@membertype", value = ddl_membertype.SelectedValue });
                 _lstparms.Add(new ParmList() { name = "@Wallet", value = ddl_wallet.SelectedValue });
                 _lstparms.Add(new ParmList() { name = "@action", value = "L" });
                 dtchklist = cls.select_data_dtNew("sp_maintainwallet", _lstparms);
                 if (dtchklist.Rows.Count > 0)
                 {
                     string Amount = dtchklist.Rows[0]["Amount"].ToString();
                     txtAmount.Text = Amount;
                 }
                 else
                 {
                     txtAmount.Text = "0.00";
                 }
             }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Please Select !');", true);
        }
    }
}