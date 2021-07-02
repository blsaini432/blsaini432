using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using BLL;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Events
    protected void btn_login_Click(object sender, EventArgs e)
    {
        try
        {

            if (txt_mobile.Text != "" && txt_pin.Text != "")
            {

                string mobile = txt_mobile.Text;
                string pin = txt_pin.Text;
                //  sendsms(mobile, EmployeeName, otp);
                sendemail(mobile, pin);
                Session["message"] = "Your Message Send to My Team";
               
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your details Send Admin Team ');", true);


            }
            else
            {
                Session["message"] = "Some Error In Server side";
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(" + ex.ToString() + ");", true);
        }
    }

    public static void sendemail(string mobile, string pin)
    {
        try
        {
            string[] valueArray = new string[2];
            valueArray[0] = mobile;
            valueArray[1] = pin;
            FlexiMail objSendMail = new FlexiMail();
            objSendMail.To = "support@apexmart.in";
            objSendMail.CC = "";
            objSendMail.BCC = "support@apexmart.in";
            objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
            objSendMail.FromName = "Enquery";
            objSendMail.MailBodyManualSupply = true;
            objSendMail.EmailTemplateFileName = "enqueryform.htm";
            objSendMail.Subject = "Enquery";
            objSendMail.ValueArray = valueArray;
            objSendMail.Send();

        }
        catch (Exception ex)
        {

        }
    }
    #endregion

}