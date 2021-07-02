using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using BLL;

public partial class Root_Retailer_membercommission : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_connection objconnection = new cls_connection();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["myCommission"] = null;
            DataTable dtmembermaster = new DataTable();
            dtmembermaster = (DataTable)Session["dtRetailer"];
            DataTable dt = new DataTable();
            string msrno = string.Empty;
            string Packageid = string.Empty;
            msrno = dtmembermaster.Rows[0]["msrno"].ToString();
            Packageid = dtmembermaster.Rows[0]["PackageID"].ToString();
            //txnid = dt.Rows[0]["agent_id"].ToString();
            cls_myMember clsm = new cls_myMember();
            dtOperator = objconnection.select_data_dt("Exec MM_AEPScommision 0,'" + Packageid + "','0',0");
            DataView dv = dtOperator.DefaultView;
            DataTable sortedDT = dv.ToTable();
            Session["myCommission"] = sortedDT;
            gvOperator.DataSource = sortedDT;
            gvOperator.DataBind();
            //dvadd.Visible = true;
            gvOperator.Visible = true;
            //dvbutton.Visible = true;
        }
    }

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        try
        {

            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "D" });
           // _lstparm.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
            objconnection.select_data_dtNew("sp_set_aepsCommission ", _lstparm);
            DataTable dt = new DataTable();
            dt = (DataTable)Session["myCommission"];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                List<ParmList> _lstparmS = new List<ParmList>();
                _lstparmS.Add(new ParmList() { name = "@Action", value = "I" });
              //  _lstparmS.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
                _lstparmS.Add(new ParmList() { name = "@startval", value = Convert.ToDecimal(dt.Rows[j][2].ToString()) });
                _lstparmS.Add(new ParmList() { name = "@endval", value = Convert.ToDecimal(dt.Rows[j][3].ToString()) });
                _lstparmS.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(dt.Rows[j][4].ToString()) });
                _lstparmS.Add(new ParmList() { name = "@IsFlat", value = Convert.ToDecimal(dt.Rows[j][5].ToString()) });
                objconnection.select_data_dtNew("sp_set_aepsCommission ", _lstparmS);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
            clear();
        }
        catch (Exception ex)
        {

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
       
        gvOperator.Visible = false;
    }


    public void fillPackage()
    {
        DataTable dtPackage = new DataTable();
        dtPackage = objPackage.ManagePackage("GetByMsrNo", 1);
      
    }
    #endregion
    protected void gvOperator_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
       
          
        }
    }
