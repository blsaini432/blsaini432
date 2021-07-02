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
using BLL;


public partial class cms_ManagePage : System.Web.UI.Page
{
    #region [Properties]
    clsPage objPage = new clsPage();
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillPage();
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update Page";
            }
            else
                lblAddEdit.Text = "Add Page";
        }
    }

    #endregion

    #region [Submit Button]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string PageIcon = "";
            string PageBanner = "";
            if (Request.QueryString["id"] == null)
            {
                Int32 intresult = 0;
                intresult = objPage.AddEditPage(0, txtPageName.Text, txtPageHeading.Text, ckPageDesc.Text, PageBanner, PageIcon, txtMetaTitle.Text, txtMetaKeywords.Text, txtMetaDesc.Text, Convert.ToInt32(ddlPage.SelectedItem.Value), 2, 0, true, true);
                if (intresult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Page Name Already Exists !');", true);
                    
                }

            }
            else
            {
                Int32 intresult = 0;
                intresult = objPage.AddEditPage(Convert.ToInt32(Request.QueryString["id"]), txtPageName.Text, txtPageHeading.Text, ckPageDesc.Text, PageBanner, PageIcon, txtMetaTitle.Text, txtMetaKeywords.Text, txtMetaDesc.Text, Convert.ToInt32(ddlPage.SelectedItem.Value), 2, 0, true,true);
                if (intresult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Page Name Already Exists !');", true);
                }
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Sorry for the inconvenience caused, DataBase Error !');", true);
           
        }
    }
    #endregion

    #region [Clear Button]
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    #endregion

    #region [All Function-FillDate,Clear,UplaodImageUrl]
    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objPage.ManagePage("GetAll", id);

        if (dt.Rows.Count > 0)
        {
            txtPageName.Text = Convert.ToString(dt.Rows[0]["PageName"]);
            txtPageHeading.Text = Convert.ToString(dt.Rows[0]["PageHeading"]);
            ckPageDesc.Text = Convert.ToString(dt.Rows[0]["PageDesc"]);
            txtMetaTitle.Text = Convert.ToString(dt.Rows[0]["MetaTitle"]);
            txtMetaKeywords.Text = Convert.ToString(dt.Rows[0]["MetaKeywords"]);
            txtMetaDesc.Text = Convert.ToString(dt.Rows[0]["MetaDesc"]);
            ddlPage.SelectedValue = Convert.ToString(dt.Rows[0]["ParentID"]);
           
        }
    }

    private void clear()
    {
        txtPageName.Text = "";
        ckPageDesc.Text = "";
        txtPageHeading.Text = "";
        txtMetaTitle.Text = "";
        txtMetaKeywords.Text = "";
        txtMetaDesc.Text = "";
        ddlPage.SelectedValue = "0";
       
    }

    private void fillPage()
    {
        DataTable dt = new DataTable();
        dt = objPage.ManagePage("GetAll", 0);
        ddlPage.DataSource = dt;
        ddlPage.DataValueField = "PageID";
        ddlPage.DataTextField = "PageName";
        ddlPage.DataBind();
        ddlPage.Items.Insert(0, new ListItem("Select Page", "0"));
    }

    #endregion


}
