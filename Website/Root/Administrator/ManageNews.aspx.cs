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

public partial class cms_ManageNews : System.Web.UI.Page
{
    #region [Properties]
    clsNews objNews = new clsNews();
    clsImageResize objImageResize = new clsImageResize();
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {          
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update News";
            }
            else
            lblAddEdit.Text = "Add News";
        }
    }
    #endregion

    #region [Submit Button]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
          
            DateTime date = DateTime.Now;
            if (Request.QueryString["id"] == null)
            {
                Int32 intresult = 0;
                intresult = objNews.AddEditNews(0, txtNewsName.Text, "", "", txtnewsdescription.Text, "", date);
                if (intresult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);

                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|News Name Already Exists !');", true); 
                }
            }
            else
            {

                Int32 intresult = 0;
                intresult = objNews.AddEditNews(Convert.ToInt32(Request.QueryString["id"]), txtNewsName.Text, "", "", txtnewsdescription.Text, "", date);
                if (intresult > 0)
                {
                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|News Name Already Exists !');", true);
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
        dt = objNews.ManageNews("GetAll", id);
        if (dt.Rows.Count > 0)
        {
            txtNewsName.Text = Convert.ToString(dt.Rows[0]["NewsName"]);
            txtnewsdescription.Text = Convert.ToString(dt.Rows[0]["NewsDesc"]);
        }
    }

    private void clear()
    {
        txtNewsName.Text = "";
        txtnewsdescription.Text = "";
       
    }
    #endregion

  
}
