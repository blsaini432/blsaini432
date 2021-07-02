using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Root_Distributor_PosLogin : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    private int checksumValue;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtDistributor"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataTable dtMember = (DataTable)Session["dtDistributor"];
                    ViewState["MemberId"] = null;
                    ViewState["MsrNo"] = null;
                    ViewState["dmtmobile"] = null;
                    ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                    ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                    ViewState["mobile"] = dtMember.Rows[0]["mobile"];
                    DataTable dtChk = Cls.select_data_dt(@"SELECT * FROM tbl_posinsurance WHERE MsrNo='" + dtMember.Rows[0]["MsrNo"].ToString() + "'");
                    if (dtChk.Rows.Count > 0)
                    {
                        if (dtChk.Rows[0]["Status"].ToString() == "APPROVED")
                        {

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Pos Request Pending ');window.location ='PosInsurance.aspx';", true);
                        }
                    }
                }
            }

            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }

    }

}





