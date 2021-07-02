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


public partial class Root_Admin_AccountOpening : System.Web.UI.Page
{

    cls_connection Cls = new cls_connection();
    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
               
                lblAddEdit.Text = "Account Opening";
            }
            else
            {
              
                lblAddEdit.Text = "Account Opening";
            }
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string bannerImage = uploadBannerImage();
        if (bannerImage != null)
        {
            List<ParmList> _list = new List<ParmList>();
            _list.Add(new ParmList() { name = "@BankName", value = txt_bankname.Text });
            _list.Add(new ParmList() { name = "@BannerImage", value = bannerImage });
            _list.Add(new ParmList() { name = "@NavigateURL", value = txt_link.Text });
          //  _list.Add(new ParmList() { name = "@payout", value = txt_payout.Text });
            _list.Add(new ParmList() { name = "@instructions", value = txt_instr.Text });
           // _list.Add(new ParmList() { name = "@reportlink", value = txt_report.Text });
            _list.Add(new ParmList() { name = "@Action", value = "I" });
            DataTable dt = new DataTable();
            dt = Cls.select_data_dtNew("Proc_Accountopen", _list);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
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
        txt_bankname.Text = "";
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