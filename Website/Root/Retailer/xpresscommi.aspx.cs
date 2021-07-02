using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using BLL;

public partial class Root_Retailer_xpresscommi : System.Web.UI.Page
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
            dtOperator = objconnection.select_data_dt("Exec MM_getSurcharge 0,'" + Packageid + "','0',0");
            DataView dv = dtOperator.DefaultView;
            DataTable sortedDT = dv.ToTable();
            Session["mySurcharge"] = sortedDT;
            gvOperator.DataSource = sortedDT;
            gvOperator.DataBind();

        }
    }
    protected void gvOperator_RowCommand(object sender, GridViewCommandEventArgs e)
    {



    }
}




