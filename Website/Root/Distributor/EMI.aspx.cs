using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Web.Services;

public partial class Root_Admin_EMI : System.Web.UI.Page
{
    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtDistributor"] != null)
        {
            DataTable dtmember = (DataTable)Session["dtDistributor"];
            if (dtmember.Rows.Count > 0)
            {
                int msrno = Convert.ToInt32(dtmember.Rows[0]["MsrNo"]);
               
                Session["DistributorMsrNo"] = msrno;
                Session["MsrNoLog"] = msrno;
                Session["MemberIdLog"] = dtmember.Rows[0]["MemberId"].ToString();

            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }

    }
    #endregion

}