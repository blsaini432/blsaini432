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
using System.Collections.Generic;

public partial class Contactus : System.Web.UI.Page
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

            repPage.DataSource = dtPage;
            repPage.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            cls_connection Cls = new cls_connection();
            // sendMail(txtEmail.Text, txtFirstName.Text + " " + txtLastName.Text, txtmobile.Text, "", txtIssue.Text);
            string name = txtFirstName.Text + " " + txtLastName.Text;
            List<ParmList> _list = new List<ParmList>();
            _list.Add(new ParmList() { name = "@Name", value = name });
            _list.Add(new ParmList() { name = "@email", value = txtEmail.Text });
            _list.Add(new ParmList() { name = "@mobile", value = txtmobile.Text });
            _list.Add(new ParmList() { name = "@issue", value = txtIssue.Text });
            _list.Add(new ParmList() { name = "@Action", value = "I" });
            DataTable dt = new DataTable();
            dt = Cls.select_data_dtNew("contact", _list);
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtIssue.Text = "";
            txtLastName.Text = "";
            txtmobile.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Query Send Successfully !');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Server Error, Please try again !');", true);
        }
    }
    private void sendMail(string Email, string name, string mobile, string helptopic, string issue)
    {
        string[] valueArray = new string[5];
        valueArray[0] = name;
        valueArray[1] = Email;
        valueArray[2] = mobile;
        valueArray[3] = helptopic;
        valueArray[4] = issue;
        FlexiMail objSendMail = new FlexiMail();
        objSendMail.To = Convert.ToString(ConfigurationManager.AppSettings["mailTo"]);
        objSendMail.CC = "";
        objSendMail.BCC = "";
        objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
        objSendMail.FromName = "Online Support";
        objSendMail.MailBodyManualSupply = false;
        objSendMail.EmailTemplateFileName = "Enquiry.htm";
        objSendMail.Subject = "online Support";
        objSendMail.ValueArray = valueArray;
        objSendMail.Send();
    }
}