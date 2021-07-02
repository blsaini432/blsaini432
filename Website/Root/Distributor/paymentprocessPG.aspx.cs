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
public partial class Root_Distributor_paymentprocessPG : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();

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
                    Session["MemberId"] = dtMember.Rows[0]["MemberID"];
                    ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                    // ViewState["dmtmobile"] = dt.Rows[0]["Mobile"].ToString();


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
        decimal TXN_AMOUNT = Convert.ToDecimal(txt_Amount.Text);
        Session["Amount"] = TXN_AMOUNT + ".00";
        string tx = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
        Random random = new Random();
        int id = random.Next(100000000, 999999999);
        Session["tx"] = tx;
        Session["txnid"] = id;
        Session["txtAmount"] = TXN_AMOUNT;
        Session["Returnurl"] = "addwallet";
        Session["checkout"] = "yes";
        Session["MsrNo"] = ViewState["MsrNo"];
        Response.Redirect("paymentPG.aspx");
    }
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    public class Customer
    {
        public string MsrNo { get; set; }
        public string amount { get; set; }


    }
}