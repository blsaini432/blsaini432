using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;

public partial class api_rechargestatus : System.Web.UI.Page
{
    cls_connection objconnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["memberid"] != null && Request.QueryString["password"] != null && Request.QueryString["transid"] != null)
            {
                string memberid = Request.QueryString["memberid"];
                string password = Request.QueryString["password"];
                DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where MemberID='" + memberid + "' and Password='" + password + "'");
                if (dtMemberMaster.Rows.Count > 0)
                {
                    int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                    string TransID = Request.QueryString["transid"];
                    DataTable dt = objconnection.select_data_dt("select * from tblRecharge_History where MsrNo=" + MsrNo + " and TransID='" + TransID + "'");
                    if (dt.Rows.Count > 0)
                    {
                        Response.Write(Convert.ToString(dt.Rows[0]["Status"]));
                    }
                }
                else
                {
                    Response.Write("0");
                }
            }
            else
            {
                Response.Write("0");
            }
        }
        else
        {
            Response.Write("0");
        }
    }
}