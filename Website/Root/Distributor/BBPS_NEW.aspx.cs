using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Root_Distributor_BBPS_NEW : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    cls_connection Cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                DataTable dtMemberMaster = new DataTable();
                dtMemberMaster = (DataTable)Session["dtDistributor"];
                //dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='BBPSNEW', @msrno=" + dtMemberMaster.Rows[0]["MsrNo"] + "");
                //if (dt.Rows.Count > 0)
                //{
                //    if (Convert.ToBoolean(dt.Rows[0]["New_BBPS"]) == false)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('BBPS Service is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                //    }
                //    else
                //    {
                //    }

                //}
               // else
               // {
               //     Response.Redirect("~/userlogin.aspx");
                //}
            }
        }
    }
}

    