using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Root_Retailer_payonclick : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    PayOut PayOuts = new PayOut();
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
                    Random random = new Random();
                    int SixDigit = random.Next(100000, 999999);
                    ViewState["id"] = SixDigit;
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
            string Result = string.Empty;
            string utm_source = "Payonclick";
            string utm_medium = "Payonclick";
            string utm_campaign = "Payonclick";
            string utm_term = "Payonclick";
            Random random = new Random();
            int SixDigit = random.Next(100000, 999999);
            int partnerleadid = SixDigit;
            string agentid = ViewState["MemberId"].ToString();
            string Text = utm_source + "-" + utm_medium + "-" + utm_campaign + "-" + utm_term + "-" + partnerleadid + "-" + agentid;
            string base64Decoded = Text;
            string base64Encoded;
            byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(base64Decoded);
            base64Encoded = System.Convert.ToBase64String(data);
            string URL = "https://www.easypolicy.com/epnew/landing/landing.aspx";
            URL = Page.ResolveClientUrl(URL);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Open", "window.open('" + URL + "');", true);

        }
        catch (Exception EX)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
        }
    }







}

