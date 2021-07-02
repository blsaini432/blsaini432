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
using System.Net;

public partial class Root_Distributor_AddMemberMaster : System.Web.UI.Page
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
            fillCountry();
            if (Session["dtDistributor"] != null)
            {
                dtMemberMaster = (DataTable)Session["dtDistributor"];
                if (dtMemberMaster.Rows.Count > 0)
                {
                    dtMemberMaster = objMemberMaster.ManageMemberMaster("Get", Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]));
                    cls.fill_MemberType(ddlMemberType, dtMemberMaster.Rows[0]["membertype"].ToString());
                    fillPackage();

                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            cls_connection cls = new cls_connection();
            if (Page.IsValid == false)
            {
                return;
            }

            if (Request.QueryString["id"] == null && Session["dtDistributor"] != null)
            {

                dtMemberMaster = (DataTable)Session["dtDistributor"];
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

                #region WalletBalanceCheck
                DataTable dtt = new DataTable();
                dtt = cls.select_data_dt("Exec Apply_RegistrationCharges '" + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "','0','" + ddlMemberType.SelectedValue + "',0");
                if (dtt.Rows.Count > 0)
                {
                    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
                    DataTable dtEWalletBalance = new DataTable();
                    dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"].ToString()));
                    if (Convert.ToDouble(dtt.Rows[0]["adminfees"]) <= Convert.ToDouble(dtEWalletBalance.Rows[0]["Balance"]))
                    {

                        #region InsertCode
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
                        _lstparm.Add(new ParmList() { name = "@ParentMsrNo", value = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]) });
                        _lstparm.Add(new ParmList() { name = "@memberImage", value = strimage });
                        _lstparm.Add(new ParmList() { name = "@aadhar", value = txt_aadhar.Text.Trim() });
                        _lstparm.Add(new ParmList() { name = "@pan", value = txt_PAN.Text.Trim() });
                        _lstparm.Add(new ParmList() { name = "@companypan", value = txt_businesspan.Text.Trim() });
                        _lstparm.Add(new ParmList() { name = "@gstno", value = txt_gstnumber.Text.Trim() });
                        _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                      //  dt = cls.select_data_dtNew("ProcMLM_AddNewMemberMaster ", _lstparm);
                       // if (dt.Rows.Count > 0)
                        {
                            List<ParmList> _lstparmss = new List<ParmList>();
                            _lstparmss.Add(new ParmList() { name = "@ID", value = 2 });
                            _lstparmss.Add(new ParmList() { name = "@Action", value = "GetAll" });
                            DataTable dtCompany = cls.select_data_dtNew("Proc_ManageCompany ", _lstparmss);
                            if (dtCompany.Rows.Count > 0)
                            {
                                // string CompanyName = dtCompany.Rows[0]["CompanyName"].ToString();
                                // string WebSiteURL = dtCompany.Rows[0]["Website"].ToString() + "/userlogin";
                                // RegisterMail.SendRegistrationMail(MemberID + " - " + txtFirstName.Text + " " + txtLastName.Text, CompanyName, txtEmail.Text, txtMobile.Text, password, pin.ToString());
                                // string[] valueArray = new string[6];
                                // valueArray[0] = txtFirstName.Text + " " + txtLastName.Text;
                                // valueArray[1] = CompanyName;
                                // valueArray[2] = MemberID;
                                // valueArray[3] = password;
                                // valueArray[4] = pin.ToString();
                                // valueArray[5] = WebSiteURL;
                                //// SMS.SendWithVar(txtMobile.Text, 26, valueArray, 1);
                                // DLTSMS.SendWithVar(txtMobile.Text, 2, valueArray, 1);
                                // clear();
                                string CompanyName = dtCompany.Rows[0]["CompanyName"].ToString();
                                string WebSiteURL = dtCompany.Rows[0]["Website"].ToString() + "/userlogin.aspx";
                                RegisterMail.SendRegistrationMail(MemberID + " - " + txtFirstName.Text + " " + txtLastName.Text, CompanyName, txtEmail.Text, txtMobile.Text, password, pin.ToString());
                                string[] valueArray = new string[6];
                                string name = txtFirstName.Text + " " + txtLastName.Text;
                                valueArray[1] = CompanyName;
                                valueArray[2] = MemberID;
                                valueArray[3] = password;
                                valueArray[4] = pin.ToString();
                                valueArray[5] = WebSiteURL;
                                string sms = "Dear " + name + " , Welcome to DISCOUNTPAY You have registered sucessfully, your ID-" + MemberID + ", Password-" + password + ", MPin-" + pin.ToString() + " Now you can login on wayfastinfo.com/userlogin.aspx&templateid=1207162081744067576";
                                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                WebClient client = new WebClient();
                                string baseurl = "http://weberleads.in/http-tokenkeyapi.php?authentic-key=3833776179666173743138301579110354&senderid=DSCPAY&route=2&number=91" + txtMobile.Text + "&message=" + sms + "";
                                Stream data = client.OpenRead(baseurl);
                                StreamReader reader = new StreamReader(data);
                              //  DLTSMS.SendWithVar(txtMobile.Text, 2, valueArray, 1);
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Insufficient Wallet Balance !');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Plan Configuration error !');", true);
                    return;
                }
                #endregion
            }
            else
            {

            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Occured !! Please Try Later');", true);
        }
    }
    #region [All Function-FillDate,Clear]
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
    private static string CreateRandomPassword(int length = 8)
    {
        // Create a string of characters, numbers, special characters that allowed in the password  
        string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
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
            ddlPackage.SelectedValue = Convert.ToString(dt.Rows[0]["PackageID"]);
            txtMobile.Enabled = false;
            imgUser.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["MemberImage"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/User/Profile/" + Convert.ToString(dt.Rows[0]["MemberImage"]);
        }
    }

    public void fillPackage()
    {
        DataTable dtPackage = new DataTable();
        dtPackage = objPackage.ManagePackage("GetByMsrNo", Convert.ToInt32(Session["RetailerMsrNo"]));
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
        //ddlCountryName.Items.Insert(0, new ListItem("Select Country", "0"));
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
    private string profilepicupload(FileUpload fpup)
    {
        string fullpath = string.Empty;
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
                    fullpath = DateTime.Now.Ticks.ToString() + Extension;
                    fpup.PostedFile.SaveAs(opath + fullpath);
                    return fullpath;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload valid file type');", true);
                    return fullpath;
                }
            }
        }
        return "";
    }


    protected void ddlCountryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillState(Convert.ToInt32(ddlCountryName.SelectedValue));
    }

    protected void ddlMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMemberType.SelectedIndex > 0)
        {
            if (Session["dtDistributor"] != null)
            {
                dtMemberMaster = (DataTable)Session["dtDistributor"];

                DataTable dtt = new DataTable();
                dtt = cls.select_data_dt("Exec Apply_RegistrationCharges '" + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "','0','" + ddlMemberType.SelectedValue + "',0");
                if (dtt.Rows.Count > 0)
                {
                    lblChargeMsg.Text = "Your account will be charged Rs. " + dtt.Rows[0]["adminFees"].ToString() + ". You should charge Rs. " + dtt.Rows[0]["RegistrationCharges"].ToString() + " from member.";
                }



                int mtp = Convert.ToInt32(ddlMemberType.SelectedValue);
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("Exec GetMUSetting '" + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "'");
                if (dt.Rows.Count > 0)
                {

                    int DTcnt = Convert.ToInt32(dt.Rows[0]["DTN"].ToString());
                    int RTcnt = Convert.ToInt32(dt.Rows[0]["RTN"].ToString());
                    int CTcnt = Convert.ToInt32(dt.Rows[0]["CTN"].ToString());

                    int DTlmt = Convert.ToInt32(dt.Rows[0]["dtcount"].ToString());
                    int RTlmt = Convert.ToInt32(dt.Rows[0]["rtcount"].ToString());
                    int CTlmt = Convert.ToInt32(dt.Rows[0]["ctcount"].ToString());

                    if (mtp == 4)
                    {
                        if (DTcnt + 1 > DTlmt)
                        {
                            ddlMemberType.SelectedIndex = 0;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('DSO Reg Limit exceed !!. Can not register new Distributors.');", true);
                        }
                    }
                    else if (mtp == 5)
                    {
                        if (RTcnt + 1 > RTlmt)
                        {
                            ddlMemberType.SelectedIndex = 0;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Advisor Reg Limit exceed !!. Can not register new retailers.');", true);
                        }
                    }
                    else if (mtp == 6)
                    {
                        if (CTcnt + 1 > CTlmt)
                        {
                            ddlMemberType.SelectedIndex = 0;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Customer Reg Limit exceed !!. Can not register new Customers.');", true);
                        }
                    }
                    FillPackagebyTypeid(ddlMemberType.SelectedValue);
                }
                else
                {
                    ddlMemberType.SelectedIndex = 0;
                }
            }
        }
    }

    protected void FillPackagebyTypeid(string xx)
    {
        if (Session["dtDistributor"] != null)
        {
            dtMemberMaster = (DataTable)Session["dtDistributor"];
            DataTable dtPackage = new DataTable();
            dtPackage = cls.select_data_dt("Exec getPackagebyTypeid " + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "," + xx + "");
            ddlPackage.DataSource = dtPackage;
            ddlPackage.DataValueField = "PackageID";
            ddlPackage.DataTextField = "PackageName";
            ddlPackage.DataBind();
            ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
        }
    }

    protected void ddlStateName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlStateName.SelectedItem.Value) != 0)
        {
            fillCity(Convert.ToInt32(ddlStateName.SelectedValue));
        }
        else
        {
            ddlCityName.Items.Clear();
            ddlCityName.Items.Insert(0, new ListItem("Select City", "0"));
        }
    }
}