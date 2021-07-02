using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web.Services;

public partial class ForgotPassowrd : System.Web.UI.Page
{

    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();

    clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();

    cls_connection objConnection = new cls_connection();

    cls_Universal objUniversal = new cls_Universal();
    DataTable dtUniversal = new DataTable();

    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();

    clsMLM_MemberMasterLoginDetail objMemberMasterLoginDetail = new clsMLM_MemberMasterLoginDetail();
    DataTable dtMemberMasterLoginDetail = new DataTable();

    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["UserEmail"] != null)
        {            
            txtFGEmailForgot.Text = Request.Cookies["UserEmail"].Value;
        }
    }


    protected void btnFGSend_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = objConnection.select_data_dt("select * from tblMLM_MemberMaster where Email=" + "'" + txtFGEmailForgot.Text + "'");
        if (dt.Rows.Count > 0)
        {
            string password = Convert.ToString(dt.Rows[0]["Password"]);
            try
            {
                sendMail(txtFGEmailForgot.Text, password);
                string[] valueArray = new string[3];
                valueArray[0] = Convert.ToString(dt.Rows[0]["FirstName"]) + " " + Convert.ToString(dt.Rows[0]["LastName"]);
                valueArray[1] = txtFGEmailForgot.Text;
                valueArray[2] = password;
                SMS.SendWithVar(Convert.ToString(dt.Rows[0]["Mobile"]), 12, valueArray, 1);
                txtFGEmailForgot.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('Your password sent on this email id !');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('Server Error, Please try again !');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('This Email ID does not exists !');", true);
        }
    }


    private void sendMail(string Email, string Password)
    {
        string[] valueArray = new string[2];
        valueArray[0] = Email;
        valueArray[1] = Password;
        FlexiMail objSendMail = new FlexiMail();
        objSendMail.To = Email;
        objSendMail.CC = "";
        objSendMail.BCC = "";
        objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
        objSendMail.FromName = "PAYUJUNC.com";
        objSendMail.MailBodyManualSupply = false;
        objSendMail.EmailTemplateFileName = "ForgetPassword.htm";
        objSendMail.Subject = "Password Recovery";
        objSendMail.ValueArray = valueArray;
        objSendMail.Send();
    }
}