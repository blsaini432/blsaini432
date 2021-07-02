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
public partial class Root_Admin_Amazon_banner : System.Web.UI.Page
{
    #region [Properties]
    cls_connection Cls = new cls_connection();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                FillData(30);
                lblAddEdit.Text = "Update Banner";
            }
            else
            {
                FillData(30);
                lblAddEdit.Text = "Update Banner";
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
                List<ParmList> _list = new List<ParmList>();
              _list.Add(new ParmList() { name = "@Bannertype", value = ddlbanner.SelectedItem.ToString() });
                _list.Add(new ParmList() { name = "@BannerName", value = txtBannerName.Text });
                _list.Add(new ParmList() { name = "@BannerImage", value = bannerImage });
                _list.Add(new ParmList() { name = "@Action", value = "I" });
                DataTable dt = new DataTable();
                dt = Cls.select_data_dtNew("Proc_amazonbanner", _list);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["BannerID"]) > 0)
                    {

                        //  Clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "Credit_card Bill Payment Request:'" + TxnID + "'");
                        //Cls.select_data_dt(@"EXEC ProcMLM__EWalletTransaction '" + ViewState["MemberId"].ToString() + "','" + NetAmount + "','Cr','Credit Card Commission:" + TxnID + "'");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                        clear();
                    }

                }



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