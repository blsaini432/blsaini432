using Newtonsoft.Json;
using Razorpay.Api;
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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Root_Retailer_Purchageservice : System.Web.UI.Page
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
                    ViewState["PackageID"] = dtMember.Rows[0]["PackageID"];
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
            string tx = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            Random random = new Random();
            int id = random.Next(100000000, 999999999);
            Session["tx"] = tx;
            Session["txnid"] = tx;
            decimal amount = Convert.ToDecimal(Session["txtAmount"]);
            if (amount > 0)
            {
                Session["Returnurl"] = "addwallet";
                Session["checkout"] = "yes";
                Response.Redirect("paymentPG.aspx");
            }
            else
            {
                string service = Session["Servicename"].ToString();
                Cls.insert_data("Exec dbo.insertpaymentGateway null,'" + ViewState["MemberId"] + "', '" + ViewState["MsrNo"] + "' , '" + id + "','" + null + "', '" + null + "' , '" + "success" + "','" + amount + "','" + "null" + "', '" + "null" + "' , '" + "null" + "' , '" + "AddWallet" + "', '" + ViewState["mobile"] + "','" + null + "','" + tx + "','" + service + "' ");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Service Purchage sucessfully');window.location ='Purchageservice_Report.aspx';", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    protected void ddl_Eboard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            cls_connection objconnection = new cls_connection();
            string Result = string.Empty;
            string service = ddlservice.SelectedItem.ToString();
            string packageid = ViewState["PackageID"].ToString();
            dt = objconnection.select_data_dt("select *  from [tblmlm_manageservice] where PackageId='" + Convert.ToInt32(packageid) + "' and Servicename ='"+service+"'");
            if (dt.Rows.Count >0)
            {
                string AMOUNT = dt.Rows[0]["Amount"].ToString();
                string servicename = dt.Rows[0]["Servicename"].ToString();
                Session["Servicename"] = servicename;        
                Session["txtAmount"] = AMOUNT;
                tr_service.Visible = true;
                lbl_servicetag.Text = AMOUNT;               
              
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
            }
            else
            {
                tr_service.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Service Disable !');", true);
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
            }

          


            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Electricity Board to proceed');", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact your admin');", true);
        }
    }

}





