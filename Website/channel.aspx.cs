using System;
using System.Collections;
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


public partial class channel : System.Web.UI.Page
{
    clsPage objPage = new clsPage();
    cls_connection objConnection = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        
            DisplayMetaTags();
       
    }

    protected void DisplayMetaTags()
    {

        DataTable dtPage = new DataTable();
        dtPage = objPage.ManagePage("Get", 59);
        if (dtPage.Rows.Count > 0)
        {

            //((HtmlTitle)Master.FindControl("MetaTitle")).Text = dtPage.Rows[0]["MetaTitle"].ToString();
            //((HtmlMeta)Master.FindControl("MetaKeywords")).Attributes.Add("Content", dtPage.Rows[0]["MetaKeywords"].ToString());
            //((HtmlMeta)Master.FindControl("MetaDescription")).Attributes.Add("Content", dtPage.Rows[0]["MetaDesc"].ToString());

           // repPage.DataSource = dtPage;
           // repPage.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            sendMail(txt_name.Text, txt_email.Text, txt_pin.Text, txt_message.Text);
            txt_name.Text = "";
            txt_email.Text = "";
            txt_pin.Text = "";
            txt_message.Text = "";
            Session["message3"] = "Your Query Send Successfully";
           
        }
        catch (Exception ex)
        {
            Session["message3"] = "Server Error, Please try again !";
        }
    }
    private void sendMail( string name, string email, string pin, string message)
    {
        string[] valueArray = new string[5];
        valueArray[0] = name;
        valueArray[1] = email;
        valueArray[2] = pin;
        valueArray[3] = message;
       
        FlexiMail objSendMail = new FlexiMail();
        objSendMail.To = Convert.ToString(ConfigurationManager.AppSettings["mailTo"]);
        objSendMail.CC = "";
        objSendMail.BCC = "";
        objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
        objSendMail.FromName = "Enquiry";
        objSendMail.MailBodyManualSupply = false;
        objSendMail.EmailTemplateFileName = "channel.htm";
        objSendMail.Subject = "Enquiry";
        objSendMail.ValueArray = valueArray;
        objSendMail.Send();
    }
}