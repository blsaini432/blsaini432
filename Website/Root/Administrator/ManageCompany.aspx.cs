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
using System.Collections.Generic;
using System.IO;

public partial class Root_SuperAdmin_ManageCompany : System.Web.UI.Page
{
    

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData(2);
            fillCountry();
        }
    }
    #endregion

    #region Methods
    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        clsCompany objCompany = new clsCompany();
        dt = objCompany.ManageCompany("GetAll", id);
        if (dt.Rows.Count > 0)
        {
            txtCompanyName.Text = Convert.ToString(dt.Rows[0]["CompanyName"]);
            rfvCompanyLogo.Enabled = false;
            hidCompanyLogo.Value = Convert.ToString(dt.Rows[0]["CompanyLogo"]);
            txtCompanyOwner.Text = Convert.ToString(dt.Rows[0]["CompanyOwner"]);
            txtPhone.Text = Convert.ToString(dt.Rows[0]["Phone"]);
            txtMobile.Text = Convert.ToString(dt.Rows[0]["Mobile"]);
            txtEmail.Text = Convert.ToString(dt.Rows[0]["Email"]);
            txtWebsite.Text = Convert.ToString(dt.Rows[0]["Website"]);
            txtFax.Text = Convert.ToString(dt.Rows[0]["Fax"]);
            txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
            txtPIN.Text = Convert.ToString(dt.Rows[0]["PIN"]);
            txtCopyright.Text = Convert.ToString(dt.Rows[0]["Copyright"]);
            ddlCountryName.SelectedValue = Convert.ToString(dt.Rows[0]["CountryID"]);
            fillState(Convert.ToInt32(dt.Rows[0]["CountryID"]));
            ddlStateName.SelectedValue = Convert.ToString(dt.Rows[0]["StateID"]);
            fillCity(Convert.ToInt32(dt.Rows[0]["StateID"]));
            ddlCityName.SelectedValue = Convert.ToString(dt.Rows[0]["CityID"]);
            txtCopyright.Text = Convert.ToString(dt.Rows[0]["Copyright"]);
            cls_connection cls = new cls_connection();
          
        }
    }
    public void fillCountry()
    {
        DataTable dtCountry = new DataTable();
        clsCountry objCountry = new clsCountry();
        dtCountry = objCountry.ManageCountry("Get", 0);
        ddlCountryName.DataSource = dtCountry;
        ddlCountryName.DataValueField = "CountryID";
        ddlCountryName.DataTextField = "CountryName";
        ddlCountryName.DataBind();
        ddlCountryName.Items.Insert(0, new ListItem("Select Country", "0"));

    }
    public void fillState(int CountryID)
    {
        DataTable dtState = new DataTable();
        clsState objState = new clsState();
        dtState = objState.ManageState("GetByCountryID", CountryID);
        ddlStateName.DataSource = dtState;
        ddlStateName.DataValueField = "StateID";
        ddlStateName.DataTextField = "StateName";
        ddlStateName.DataBind();
        ddlStateName.Items.Insert(0, new ListItem("Select State", "0"));

    }
    public void fillCity(int StateID)
    {
        DataTable dtCity = new DataTable();
        clsCity objCity = new clsCity();
        dtCity = objCity.ManageCity("GetByStateID", StateID);
        ddlCityName.DataSource = dtCity;
        ddlCityName.DataValueField = "CityID";
        ddlCityName.DataTextField = "CityName";
        ddlCityName.DataBind();
        ddlCityName.Items.Insert(0, new ListItem("Select City", "0"));

    }

    private string UploadCompanyLogo()
    {
        if (FileUploadCompanyLogo.HasFile == true)
        {
            if (FileUploadCompanyLogo.PostedFile.FileName != "")
            {

                string opath = Server.MapPath("~/Uploads/Company/Logo/actual/");
                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
                string Extension = System.IO.Path.GetExtension(FileUploadCompanyLogo.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                {
                    string FileName = DateTime.Now.Ticks + FileUploadCompanyLogo.FileName.ToString();
                    FileUploadCompanyLogo.PostedFile.SaveAs(opath + FileName);
                    return FileName;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select valid image.');", true);
                }
            }
        }
        else if (hidCompanyLogo.Value.Trim() != "")
        {
            return hidCompanyLogo.Value;
        }
        return "";
    }
    #endregion

    #region ChangeEvent

    protected void ddlCountryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillState(Convert.ToInt32(ddlCountryName.SelectedValue));
    }
    protected void ddlStateName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCity(Convert.ToInt32(ddlStateName.SelectedValue));
    }
    #endregion

    #region clickEvents
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string companylogo = UploadCompanyLogo();
            if (companylogo != "")
            {
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@CompanyID", value = Convert.ToInt32(2) });
                _lstparm.Add(new ParmList() { name = "@CompanyName", value = txtCompanyName.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@CompanyLogo", value = companylogo });
                _lstparm.Add(new ParmList() { name = "@CompanyOwner", value = txtCompanyOwner.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Phone", value = txtPhone.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Mobile", value = txtMobile.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Email", value = txtEmail.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Website", value = txtWebsite.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Fax", value = txtFax.Text.Trim() }); 
                _lstparm.Add(new ParmList() { name = "@Line1", value = "" });
                _lstparm.Add(new ParmList() { name = "@Line2", value ="" });
                _lstparm.Add(new ParmList() { name = "@Line3", value ="" });
                _lstparm.Add(new ParmList() { name = "@Description", value = "" });
                _lstparm.Add(new ParmList() { name = "@Copyright", value = txtCopyright.Text });
                _lstparm.Add(new ParmList() { name = "@Address", value = txtAddress.Text });
                _lstparm.Add(new ParmList() { name = "@PIN", value = txtPIN.Text });
                _lstparm.Add(new ParmList() { name = "@CountryID", value = Convert.ToInt32(ddlCountryName.SelectedValue) });
                _lstparm.Add(new ParmList() { name = "@StateID", value = Convert.ToInt32(ddlStateName.SelectedValue) });
                _lstparm.Add(new ParmList() { name = "@CityID", value = Convert.ToInt32(ddlCityName.SelectedValue) });
                cls_connection cls = new cls_connection();
                DataTable dt = cls.select_data_dtNew("Proc_AddEditCompany ", _lstparm);
                ScriptManager.RegisterStartupScript(this,this.GetType(), "Popup", "showSwal('success-message');", true);


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload Image !!!');", true);
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Occured Please Try Again !!!');", true);
        }
    }
    #endregion
}
