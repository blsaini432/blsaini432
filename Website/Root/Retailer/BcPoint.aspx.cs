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
public partial class Root_Retailer_BcPoint : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    private int checksumValue;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtRetailer"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataTable dtMember = (DataTable)Session["dtRetailer"];
                    ViewState["MemberId"] = null;
                  
                    ViewState["MsrNo"] = null;
                    ViewState["dmtmobile"] = null;
                    ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                    ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                    ViewState["mobile"] = dtMember.Rows[0]["mobile"];
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtMember = (DataTable)Session["dtRetailer"];
            string memberid = dtMember.Rows[0]["MemberID"].ToString();
            string name = dtMember.Rows[0]["Firstname"].ToString();
            string msrno = ViewState["MsrNo"].ToString();
            List<ParmList> _list = new List<ParmList>();
            _list.Add(new ParmList() { name = "@msrno", value = msrno });
            _list.Add(new ParmList() { name = "@memberid", value = memberid });
            _list.Add(new ParmList() { name = "@Name", value = name });
            _list.Add(new ParmList() { name = "@Action", value = "I" });
            DataTable dt = new DataTable();
            dt = Cls.select_data_dtNew("Proc_bcpoint", _list);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Bc Point Request has been Received my Team contact You  !');location.replace('bcpoint.aspx');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
}





