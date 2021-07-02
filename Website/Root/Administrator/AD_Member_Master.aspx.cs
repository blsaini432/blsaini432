using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Web.Services;
using System.Text;
using System.IO;


public partial class Root_Admin_AD_Member_New : System.Web.UI.Page
{
    #region [Properties]

    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();
    clsCountry objCountry = new clsCountry();
    clsState objState = new clsState();
    clsCity objCity = new clsCity();
    clsMLM_Package objPackage = new clsMLM_Package();
    clsMLM_MemberTree objMemberTree = new clsMLM_MemberTree();
    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();
    cls_connection cls = new cls_connection();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            cls.fill_MemberType(ddlMemberType, "");
            fillCountry();
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update MemberMaster";
            }
            else
            {
                lblAddEdit.Text = "Add MemberMaster";
            }
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            cls_connection cls = new cls_connection();
            if (Page.IsValid == false)
            {
                return;
            }

            if (Request.QueryString["id"] == null)
            {
                #region [Insert]
                DataTable Checkstatus = cls.select_data_dt("Exec Proc_Check 'Email','" + txtEmail.Text + "',''");
                if (Checkstatus.Rows[0][0].ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('Duplicate Mail ID');", true);
                    return;
                }
                Checkstatus = cls.select_data_dt("Exec Proc_Check 'Mobile','" + txtMobile.Text + "',''");
                if (Checkstatus.Rows[0][0].ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('Duplicate Mobile');", true);
                    return;
                }
                Random random = new Random();
                int SixDigit = random.Next(100000, 999999);
                string MemberID = "";
                string strimage = profilepicupload(fupmppic);
                string password = CreateRandomPassword();
                int pin = genratepin();
                DataTable dtm = new DataTable();
                List<ParmList> _lstparms = new List<ParmList>();
                _lstparms.Add(new ParmList() { name = "@Action", value = "getmembership" });
                dtm = cls.select_data_dtNew("ProcMLM_AddNewMemberMaster", _lstparms);
                if (dtm.Rows.Count > 0)
                {
                    for (int j = 0; j < dtm.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(ddlMemberType.SelectedItem.Value) == Convert.ToInt32(dtm.Rows[j]["membertypeid"]))
                        {
                            MemberID = dtm.Rows[j]["mcode"].ToString() + SixDigit;
                        }
                    }
                }
                // insert code start
                //if (txtMsignupdob.Text == "")
                //{
                //    txtMsignupdob.Text = System.DateTime.Now.Date.ToString();
                //}

                DataTable dt = new DataTable();
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@MemberID", value = MemberID });
                _lstparm.Add(new ParmList() { name = "@FirstName", value = txtFirstName.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@ShopName", value = txt_ShopName.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@PackageID", value = Convert.ToInt32(ddlPackage.SelectedValue) });
                _lstparm.Add(new ParmList() { name = "@LastName", value = txtLastName.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Email", value = txtEmail.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@DOB", value = txtMsignupdob.Text });
                _lstparm.Add(new ParmList() { name = "@Gender", value = ddlgender.SelectedValue });
                _lstparm.Add(new ParmList() { name = "@Password", value = password });
                _lstparm.Add(new ParmList() { name = "@TransactionPassword", value = pin.ToString() });
                _lstparm.Add(new ParmList() { name = "@Mobile", value = txtMobile.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@STDCode", value = txtSTDCode.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Landline", value = txtLadline.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Address", value = txtAddress.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@CountryID", value = Convert.ToInt32(ddlCountryName.SelectedValue) });
                _lstparm.Add(new ParmList() { name = "@StateID", value = Convert.ToInt32(ddlStateName.SelectedValue) });
                _lstparm.Add(new ParmList() { name = "@CityID", value = Convert.ToInt32(ddlCityName.SelectedValue) });
                _lstparm.Add(new ParmList() { name = "@CityName", value = ddlCityName.SelectedItem.ToString() });
                _lstparm.Add(new ParmList() { name = "@ZIP", value = txtZIP.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@MemberType", value = ddlMemberType.SelectedItem.ToString() });
                _lstparm.Add(new ParmList() { name = "@MemberTypeID", value = Convert.ToInt32(ddlMemberType.SelectedValue) });
                _lstparm.Add(new ParmList() { name = "@ParentMsrNo", value = 1 });
                _lstparm.Add(new ParmList() { name = "@memberImage", value = strimage });
                _lstparm.Add(new ParmList() { name = "@aadhar", value = txt_aadhar.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@pan", value = txt_PAN.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@companypan", value = txt_businesspan.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@gstno", value = txt_gstnumber.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
               // dt = cls.select_data_dtNew("ProcMLM_AddNewMemberMaster ", _lstparm);
              //  if (dt.Rows.Count > 0)
                {
                    List<ParmList> _lstparmss = new List<ParmList>();
                    _lstparmss.Add(new ParmList() { name = "@ID", value = 2 });
                    _lstparmss.Add(new ParmList() { name = "@Action", value = "GetAll" });
                    DataTable dtCompany = cls.select_data_dtNew("Proc_ManageCompany ", _lstparmss);
                    if (dtCompany.Rows.Count > 0)
                    {
                        string CompanyName = dtCompany.Rows[0]["CompanyName"].ToString();
                        string WebSiteURL = dtCompany.Rows[0]["Website"].ToString() + "/userlogin.aspx";
                        RegisterMail.SendRegistrationMail(MemberID + " - " + txtFirstName.Text + " " + txtLastName.Text, CompanyName, txtEmail.Text, txtMobile.Text, password, pin.ToString());
                        string[] valueArray = new string[6];
                        valueArray[0] = txtFirstName.Text + " " + txtLastName.Text;
                        valueArray[1] = CompanyName;
                        valueArray[2] = MemberID;
                        valueArray[3] = password;
                        valueArray[4] = pin.ToString();
                        valueArray[5] = WebSiteURL;
                        DLTSMS.SendWithVar(txtMobile.Text, 2, valueArray, 1);
                        clear();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    }
                    clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                }
                // insert code end
                #endregion
            }
            else
            {

            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Occured !! Please Try Later');", true);
        }
    }
    #endregion


    #region [Clear Button]
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }
    #endregion

    #region [All Function-FillDate,Clear]

    private static string CreateRandomPassword(int length = 8)
    {
        // Create a string of characters, numbers, special characters that allowed in the password  
        string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
        Random random = new Random();

        // Select one random character at a time from the string  
        // and create an array of chars  
        char[] chars = new char[length];
        for (int i = 0; i < length; i++)
        {
            chars[i] = validChars[random.Next(0, validChars.Length)];
        }
        return new string(chars);
    }

    public int genratepin()
    {
        Random random = new Random();
        int num = random.Next(1000, 9999);
        return num;
    }

    private string profilepicupload(FileUpload fpup)
    {
        if (fpup.HasFile == true)
        {
            if (fpup.PostedFile.FileName != "")
            {
                string filename = System.IO.Path.GetFileName(fpup.FileName);
                string opath = Server.MapPath("../../Uploads/User/Profile/");
                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(fpup.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                {
                    string FileName = DateTime.Now.Ticks.ToString() + Extension;
                    fpup.PostedFile.SaveAs(opath + FileName);
                    return filename;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload valid file type');", true);
                    return filename;
                }
            }
        }
        return "";
    }

    private void FillData(int id)
    {

        DataTable dt = new DataTable();
        dt = objMemberMaster.ManageMemberMaster("Get", id);
        if (dt.Rows.Count > 0)
        {
            txtFirstName.Text = Convert.ToString(dt.Rows[0]["FirstName"]);
            txtLastName.Text = Convert.ToString(dt.Rows[0]["LastName"]);
            txtEmail.Text = Convert.ToString(dt.Rows[0]["Email"]);
            txtMobile.Text = Convert.ToString(dt.Rows[0]["Mobile"]);
            txtSTDCode.Text = Convert.ToString(dt.Rows[0]["STDCode"]);
            txtLadline.Text = Convert.ToString(dt.Rows[0]["Ladline"]);
            txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
            txtZIP.Text = Convert.ToString(dt.Rows[0]["ZIP"]);
            ddlCountryName.SelectedValue = Convert.ToString(dt.Rows[0]["CountryID"]);
            fillState(Convert.ToInt32(dt.Rows[0]["CountryID"]));
            ddlStateName.SelectedValue = Convert.ToString(dt.Rows[0]["StateID"]);
            fillCity(Convert.ToInt32(dt.Rows[0]["StateID"]));
            ddlCityName.SelectedValue = Convert.ToString(dt.Rows[0]["CityID"]);
            ddlMemberType.SelectedValue = Convert.ToString(dt.Rows[0]["MemberTypeID"]);
            txtMsignupdob.Text = Convert.ToString(dt.Rows[0]["DOB"]);
            if (Convert.ToInt32(dt.Rows[0]["MemberTypeID"].ToString()) == 1)
            {
                rfvMemberType.Enabled = false;
            }
            ddlMemberType.Enabled = false;
         
            //ddlPackage.SelectedValue = Convert.ToString(dt.Rows[0]["PackageID"]);
            try
            {
                ddlPackage.SelectedValue = Convert.ToString(dt.Rows[0]["PackageID"]);
            }
            catch
            {
                rfvPackage.Enabled = false;
                ddlPackage.SelectedValue = "0";
                ddlPackage.Visible = false;
            }
            if (ddlPackage.SelectedValue == "0")
            {
                rfvPackage.Enabled = false;
                ddlPackage.SelectedValue = "0";
                ddlPackage.Visible = false;
            }
        }
    }

    private void clear()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        txt_aadhar.Text = "";
        txt_businesspan.Text = "";
        txt_gstnumber.Text = "";
        txt_shopaddress.Text = "";
        txt_ShopName.Text = "";
        txtMobile.Text = "";
        txtSTDCode.Text = "";
        txtLadline.Text = "";
        txtAddress.Text = "";
        txtMsignupdob.Text = "";
        txt_businesspan.Text = "";
        txt_ShopName.Text = "";
        ddlCountryName.SelectedIndex = 0;
        ddlStateName.SelectedIndex = 0;
        ddlCityName.SelectedIndex = 0;
        txtZIP.Text = "";
        ddlMemberType.SelectedIndex = 0;
        ddlPackage.SelectedIndex = 0;
        
    }

    public void fillPackage()
    {
        DataTable dtPackage = new DataTable();
        dtPackage = objPackage.ManagePackage("GetByMsrNo", 1);
        ddlPackage.DataSource = dtPackage;
        ddlPackage.DataValueField = "PackageID";
        ddlPackage.DataTextField = "PackageName";
        ddlPackage.DataBind();
        ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
    }
    public void fillCountry()
    {
        DataTable dtCountry = new DataTable();
        dtCountry = objCountry.ManageCountry("Get", 0);
        ddlCountryName.DataSource = dtCountry;
        ddlCountryName.DataValueField = "CountryID";
        ddlCountryName.DataTextField = "CountryName";
        ddlCountryName.DataBind();
        fillState(1);
    }
    public void fillState(int CountryID)
    {
        DataTable dtState = new DataTable();
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
        dtCity = objCity.ManageCity("GetByStateID", StateID);
        ddlCityName.DataSource = dtCity;
        ddlCityName.DataValueField = "CityID";
        ddlCityName.DataTextField = "CityName";
        ddlCityName.DataBind();
        ddlCityName.Items.Insert(0, new ListItem("Select City", "0"));
    }

    #endregion

    #region ChangedMethod
    protected void ddlCountryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillState(Convert.ToInt32(ddlCountryName.SelectedValue));
    }
    protected void ddlStateName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCity(Convert.ToInt32(ddlStateName.SelectedValue));
    }
    protected void FillPackagebyTypeid(string xx)
    {
        DataTable dtPackage = new DataTable();
        dtPackage = cls.select_data_dt("Exec getPackagebyTypeid 1," + xx + "");
        ddlPackage.DataSource = dtPackage;
        ddlPackage.DataValueField = "PackageID";
        ddlPackage.DataTextField = "PackageName";
        ddlPackage.DataBind();
        ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
    }
    protected void ddlMemberType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        FillPackagebyTypeid(ddlMemberType.SelectedValue);
    }
    #endregion
}