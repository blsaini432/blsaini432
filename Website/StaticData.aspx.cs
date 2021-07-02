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

public partial class StaticData : System.Web.UI.Page
{
    clsPage objPage = new clsPage();
    cls_connection objConnection = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["id"] != null)
        {
            DisplayMetaTags();
        }
    }

    protected void DisplayMetaTags()
    {

        DataTable dtPage = new DataTable();
        dtPage = objPage.ManagePage("Get", Convert.ToInt32(Request["id"]));
        if (dtPage.Rows.Count > 0)
        {

            //((HtmlTitle)Master.FindControl("MetaTitle")).Text = dtPage.Rows[0]["MetaTitle"].ToString();
            //((HtmlMeta)Master.FindControl("MetaKeywords")).Attributes.Add("Content", dtPage.Rows[0]["MetaKeywords"].ToString());
            //((HtmlMeta)Master.FindControl("MetaDescription")).Attributes.Add("Content", dtPage.Rows[0]["MetaDesc"].ToString());

            repPage.DataSource = dtPage;
            repPage.DataBind();
        }
    }
}