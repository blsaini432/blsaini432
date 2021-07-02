using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;

public partial class Balance : System.Web.UI.Page
{
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();

    cls_connection objconnection = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["memberid"] != null && Request.QueryString["password"] != null)
            {
                string memberid = Request.QueryString["memberid"];
                string password = Request.QueryString["password"];
                DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where MemberID='" + memberid + "' and Password='" + password + "'");
                if (dtMemberMaster.Rows.Count > 0)
                {
                    int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                    dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", MsrNo);
                    Response.Write(Convert.ToString(dtEWalletBalance.Rows[0]["Balance"]));
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