using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.IO;
public partial class Root_Admin_ManageBanner : System.Web.UI.Page
{
    #region [Properties]
  
  
    #endregion
    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                FillData(30);
                lblAddEdit.Text = "Add Banner";
            }
            else
            {
                FillData(30);
                lblAddEdit.Text = "Add Banner";
            }
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        clsBanner objBanner = new clsBanner();
     
        string bannerImage = uploadBannerImage();
        if (bannerImage != "")
        {
            if (Request.QueryString["id"] == null)
            {
                #region [Insert]
                Int32 intresult = 0;
                intresult = objBanner.AddEditBanner(0, txtBannerName.Text, bannerImage, "", "", "");
                if (intresult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Banner Name Already Exists !');", true);
                }
                #endregion
            }
            else
            {
                #region [Update]
                Int32 intresult = 0;
                intresult = objBanner.AddEditBanner(Convert.ToInt32(Request.QueryString["id"]), txtBannerName.Text, bannerImage, "", "", "");
                if (intresult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Banner Name Already Exists !');", true);
                }
                #endregion
            }
        }
    }
    #endregion

    #region [Reset]
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();

    }
    #endregion

    #region [All Functions]
    private void clear()
    {
        txtBannerName.Text = "";
    }

    private void FillData(int id)
    {
        clsBanner objBanner = new clsBanner();
        DataTable dt = new DataTable();
        dt = objBanner.ManageBanner("GetAll", id);
        if (dt.Rows.Count > 0)
        {
            txtBannerName.Text = Convert.ToString(dt.Rows[0]["BannerName"]);
        }
    }


    private string uploadBannerImage()
    {
        if (FileUploadBannerImage.HasFile == true)
        {
            if (FileUploadBannerImage.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Uploads/Company/BackBanner/actual/");
                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
                string Extension = System.IO.Path.GetExtension(FileUploadBannerImage.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                {
                    string FileName = DateTime.Now.Ticks + FileUploadBannerImage.FileName.ToString();
                    FileUploadBannerImage.PostedFile.SaveAs(opath + FileName);
                    return FileName;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Please select valid image !');", true);
                  
                }
            }
        }

        return "";
    }
    #endregion
}