using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class MemberSignups : System.Web.UI.Page
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
            rbtnUPS.Checked = true;
            ////Send SMS check
            //string[] valueArray = new string[3];
            //valueArray[0] = "Mahesh Sharma";
            //valueArray[1] = "MM001122";
            //valueArray[2] = "Noble000";
            //SMS.SendWithVar("9950612000", 14, valueArray, 1);
            ////SMS.SendWithVar(txtMobile.Text, 26, valueArray, 1);
            ////Send SMS check ends here
            mv.ActiveViewIndex = 0;
            fillCountry();
            fillPackage();

            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                if (rbtnUPS.Checked == true)
                {
                    lblAddEdit.Text = "Member Registration";
                    litsubtitlemenu.Text = "Member Registration";
                }
                else
                {
                    lblAddEdit.Text = "Member Registration";
                    litsubtitlemenu.Text = "Member Registration";
                }
            }
            else
            {
                if (rbtnUPS.Checked == true)
                {
                    lblAddEdit.Text = "Member Registration";
                    litsubtitlemenu.Text = "Member Registration";
                }
                else
                {
                    lblAddEdit.Text = "Member Registration";
                    litsubtitlemenu.Text = "Member Registration";
                }
            }

        }
    }

    #endregion

    #region [Insert | Update]
    //protected void btnSubmit1_Click(object sender, EventArgs e)
    //{
    //    if (Request.QueryString["id"] == null)
    //    {
    //        #region [Insert]
    //        Int32 intresult = 0;
    //        Random random = new Random();
    //        int SixDigit = random.Next(100000, 999999);
    //        string MemberID = "";
    //        if (Convert.ToInt32(ddlMemberType.SelectedItem.Value) == 4)
    //        {
    //            MemberID = "DT" + SixDigit;
    //        }
    //        else if (Convert.ToInt32(ddlMemberType.SelectedItem.Value) == 5)
    //        {
    //            MemberID = "RT" + SixDigit;
    //        }
    //        else if (Convert.ToInt32(ddlMemberType.SelectedItem.Value) == 2)
    //        {
    //            MemberID = "SD" + SixDigit;
    //        }
    //        else if (Convert.ToInt32(ddlMemberType.SelectedItem.Value) == 3)
    //        {
    //            MemberID = "MD" + SixDigit;
    //        }
    //        else if (Convert.ToInt32(ddlMemberType.SelectedItem.Value) == 6)
    //        {
    //            MemberID = "CT" + SixDigit;
    //        }
    //        else
    //            MemberID = SixDigit.ToString();
    //        string DOJ = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
    //        string MDOB = "";
    //        if (txtMsignupdob.Text.Trim()!="")
    //            MDOB = String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(txtMsignupdob.Text));
    //        else
    //            MDOB = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
    //        try
    //        {
    //            string strimage = profilepicupload(fupmppic);
    //            DataTable dtresult = new DataTable();

    //            dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtEmail.Text + "','" + Function.changedatetommddyy(MDOB) + "','','" + txtPassword.Text + "','" + txtTransactionPassword.Text + "','" + txtMobile.Text + "','" + txtSTDCode.Text + "','" + txtLadline.Text + "','" + txtAddress.Text + "','" + txtLandmark.Text + "','" + Convert.ToInt32(ddlCountryName.SelectedValue) + "','" + Convert.ToInt32(ddlStateName.SelectedValue) + "','" + Convert.ToInt32(ddlCityName.SelectedValue) + "','" + ddlCityName.SelectedItem.Text + "','" + txtZIP.Text + "','','0', '" + hdnparentid.Value + "', '0', '" + strimage + "'");
    //            intresult = Convert.ToInt32(dtresult.Rows[0][0]);
    //            if (intresult > 0)
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Your request has been sent to admin for approval. Concerning team will contact you soon.');", true);
    //                ////cls.select_data_dt("Exec Procmlm_memberKYC 0,'" + intresult + "','" + hdnPhotoName.Value + "','" + hdnPanName.Value + "','" + hdnshopname.Value + "','" + hdnaddressproof.Value + "','" + txtAddressProofNumber.Text.Trim() + "',0");
    //                //DataTable dtUniversal = new DataTable();
    //                //cls_Universal objUniversal = new cls_Universal();
    //                //dtUniversal = objUniversal.UniversalLogin("LoginUser", MemberID, txtPassword.Text.Trim().Replace("'", "").ToString());
    //                //if (dtUniversal.Rows.Count > 0)
    //                //{
    //                //    int msrno = Convert.ToInt32(dtUniversal.Rows[0]["MsrNo"].ToString());
    //                //    Session.Add("dtRetailer", dtUniversal);
    //                //    fillMenuUSER(msrno);
    //                //    Response.Redirect("~/Ezulix/USER/DashBoard.aspx");
    //                //}
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');", true);

    //            }
    //        }
    //        catch(Exception ex)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');", true);

    //        }
    //        #endregion
    //    }
    //    else
    //    {
    //        //#region [Update]
    //        //Int32 intresult = 0;
    //        //DateTime DOJ = DateTime.Now;
    //        //try
    //        //{
    //        //    intresult = objMemberMaster.AddEditMemberMaster(Convert.ToInt32(Request.QueryString["id"]), "", txtFirstName.Text, txtLastName.Text, txtEmail.Text, DOJ, "", txtPassword.Text, txtTransactionPassword.Text, txtMobile.Text, txtSTDCode.Text, txtLadline.Text, txtAddress.Text, txtLandmark.Text, Convert.ToInt32(ddlCountryName.SelectedValue), Convert.ToInt32(ddlStateName.SelectedValue), Convert.ToInt32(ddlCityName.SelectedValue), ddlCityName.SelectedItem.Text, txtZIP.Text, ddlMemberType.SelectedItem.Text, Convert.ToInt32(ddlMemberType.SelectedValue), 0, Convert.ToInt32(ddlPackage.SelectedValue), "");
    //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Record updated successfully');location.replace('ListMemberMaster.aspx');", true);
    //        //    clear();
    //        //}
    //        //catch
    //        //{
    //        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');", true);
    //        //}
    //        //#endregion
    //    }
    //}



    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null)
        {
            #region [Insert]
            Int32 intresult = 0;
            Random random = new Random();
            int SixDigit = random.Next(100000, 999999);
            string MemberID = "";

            MemberID = SixDigit.ToString();
            string DOJ = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
            string MDOB = "";
            if (txtMsignupdob.Text.Trim() != "")
                MDOB = String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(txtMsignupdob.Text));
            else
                MDOB = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
            try
            {
                string strimage = profilepicupload(fupmppic);
                DataTable dtresult = new DataTable();

                int pwd4digit = random.Next(1000, 9999);
                int transpin = random.Next(1000, 9999);

                string password = pwd4digit.ToString();
                string transpassord = transpin.ToString();
                string hdfvalue = "";
                dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtEmail.Text + "','" + Function.changedatetommddyy(MDOB) + "','','" + password + "','" + transpassord + "','" + txtMobile.Text + "','" + txtSTDCode.Text + "','" + txtLadline.Text + "','" + txtAddress.Text + "','" + txtLandmark.Text + "','" + Convert.ToInt32(ddlCountryName.SelectedValue) + "','" + Convert.ToInt32(ddlStateName.SelectedValue) + "','" + Convert.ToInt32(ddlCityName.SelectedValue) + "','" + ddlCityName.SelectedItem.Text + "','" + txtZIP.Text + "','','0', '" + hdfvalue + "', '0', '" + strimage + "'");
                intresult = Convert.ToInt32(dtresult.Rows[0][0]);
                if (intresult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Success|Your request has been sent to admin for approval. Concerning team will contact you soon.');location.replace('Default.aspx');", true);
                    ////cls.select_data_dt("Exec Procmlm_memberKYC 0,'" + intresult + "','" + hdnPhotoName.Value + "','" + hdnPanName.Value + "','" + hdnshopname.Value + "','" + hdnaddressproof.Value + "','" + txtAddressProofNumber.Text.Trim() + "',0");
                    //DataTable dtUniversal = new DataTable();
                    //cls_Universal objUniversal = new cls_Universal();
                    //dtUniversal = objUniversal.UniversalLogin("LoginUser", MemberID, txtPassword.Text.Trim().Replace("'", "").ToString());
                    //if (dtUniversal.Rows.Count > 0)
                    //{
                    //    int msrno = Convert.ToInt32(dtUniversal.Rows[0]["MsrNo"].ToString());
                    //    Session.Add("dtRetailer", dtUniversal);
                    //    fillMenuUSER(msrno);
                    //    Response.Redirect("~/Ezulix/USER/DashBoard.aspx");
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');", true);

            }
            #endregion
        }
        else
        {
            //#region [Update]
            //Int32 intresult = 0;
            //DateTime DOJ = DateTime.Now;
            //try
            //{
            //    intresult = objMemberMaster.AddEditMemberMaster(Convert.ToInt32(Request.QueryString["id"]), "", txtFirstName.Text, txtLastName.Text, txtEmail.Text, DOJ, "", txtPassword.Text, txtTransactionPassword.Text, txtMobile.Text, txtSTDCode.Text, txtLadline.Text, txtAddress.Text, txtLandmark.Text, Convert.ToInt32(ddlCountryName.SelectedValue), Convert.ToInt32(ddlStateName.SelectedValue), Convert.ToInt32(ddlCityName.SelectedValue), ddlCityName.SelectedItem.Text, txtZIP.Text, ddlMemberType.SelectedItem.Text, Convert.ToInt32(ddlMemberType.SelectedValue), 0, Convert.ToInt32(ddlPackage.SelectedValue), "");
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Record updated successfully');location.replace('ListMemberMaster.aspx');", true);
            //    clear();
            //}
            //catch
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');", true);
            //}
            //#endregion
        }
    }


    private string profilepicupload(FileUpload fpup)
    {
        clsImageResize objImageResize = new clsImageResize();
        string filename = System.IO.Path.GetFileName(fpup.FileName);
        //hdnregimg.Value = filename;


        string opath = Server.MapPath("~/Images/Profile/actual/");
        string mpath = Server.MapPath("~/Images/Profile/medium/");
        string spath = Server.MapPath("~/Images/Profile/small/");

        //Check file extension (must be JPG)
        string Extension = System.IO.Path.GetExtension(fpup.FileName).ToLower();
        if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
        {
            string FileName = DateTime.Now.Ticks.ToString() + Extension;

            //hdnregimg.Value = FileName;
            fpup.PostedFile.SaveAs(opath + FileName);
            objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 300);
            objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 100, 100);

        }

        return filename;
    }



    private void fillMenuUSER(int msrno)
    {
        clsMLM_MenuMember objMenuMember = new clsMLM_MenuMember();
        DataTable dtMenuMember = new DataTable();
        dtMenuMember = objMenuMember.ManageMenuMember("Get", 0);
        //if (cls.select_data_scalar_int("Select IsEmailVerify from tblmlm_membermaster where msrno='" + msrno.ToString() + "'") == 0)
        //{
        dtMenuMember.DefaultView.RowFilter = "menuid<>45 and parentid<>45 and menuid<>27";
        //}
        Session.Add("dtMenuMember", dtMenuMember.DefaultView.ToTable());
    }
    #endregion

    #region [Clear Button]
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }
    #endregion

    #region [All Function-FillDate,Clear]
    private void FillData(int id)
    {

        //DataTable dt = new DataTable();
        //dt = objMemberMaster.ManageMemberMaster("Get", id);
        //if (dt.Rows[0]["MemberTypeID"].ToString() == "7")
        //{
        //    Response.Redirect("Resessler_Add.aspx?id=" + Convert.ToString(id));
        //}
        //if (dt.Rows.Count > 0)
        //{
        //    txtFirstName.Text = Convert.ToString(dt.Rows[0]["FirstName"]);
        //    txtLastName.Text = Convert.ToString(dt.Rows[0]["LastName"]);
        //    txtEmail.Text = Convert.ToString(dt.Rows[0]["Email"]);
        //    txtLoginID.Text = Convert.ToString(dt.Rows[0]["LoginID"]);
        //    txtLoginID.Enabled = false;
        //    txtPassword.Attributes.Add("value", Convert.ToString(dt.Rows[0]["Password"]));
        //    txtRePassword.Attributes.Add("value", Convert.ToString(dt.Rows[0]["Password"]));
        //    txtTransactionPassword.Attributes.Add("value", Convert.ToString(dt.Rows[0]["TransactionPassword"]));
        //    txtMobile.Text = Convert.ToString(dt.Rows[0]["Mobile"]);
        //    txtSTDCode.Text = Convert.ToString(dt.Rows[0]["STDCode"]);
        //    txtLadline.Text = Convert.ToString(dt.Rows[0]["Ladline"]);
        //    txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
        //    txtLandmark.Text = Convert.ToString(dt.Rows[0]["Landmark"]);
        //    txtZIP.Text = Convert.ToString(dt.Rows[0]["ZIP"]);
        //    ddlCountryName.SelectedValue = Convert.ToString(dt.Rows[0]["CountryID"]);
        //    fillState(Convert.ToInt32(dt.Rows[0]["CountryID"]));
        //    ddlStateName.SelectedValue = Convert.ToString(dt.Rows[0]["StateID"]);
        //    fillCity(Convert.ToInt32(dt.Rows[0]["StateID"]));
        //    ddlCityName.SelectedValue = Convert.ToString(dt.Rows[0]["CityID"]);
        //    ddlMemberType.SelectedValue = Convert.ToString(dt.Rows[0]["MemberTypeID"]);
        //    if (Convert.ToInt32(dt.Rows[0]["MemberTypeID"].ToString()) == 1)
        //    {
        //        rfvMemberType.Enabled = false;
        //    }
        //    ddlMemberType.Enabled = false;
        //    lblowner.Text = Convert.ToString(dt.Rows[0]["Owner ID"]);
        //    //ddlPackage.SelectedValue = Convert.ToString(dt.Rows[0]["PackageID"]);
        //    try
        //    {
        //        ddlPackage.SelectedValue = Convert.ToString(dt.Rows[0]["PackageID"]);
        //    }
        //    catch
        //    {
        //        rfvPackage.Enabled = false;
        //        ddlPackage.SelectedValue = "0";
        //        ddlPackage.Visible = false;
        //    }
        //    if (ddlPackage.SelectedValue == "0")
        //    {
        //        rfvPackage.Enabled = false;
        //        ddlPackage.SelectedValue = "0";
        //        ddlPackage.Visible = false;
        //    }
        //}
    }

    private void clear()
    {
        // MemberID,DOB,MemberImage,IPAddress , MemberType,  MemberTypeID,  RegisterSources, LastLoginIP, LastLoginDate, TrackID,IntroMsrNo,ParentMsrNo,Leg,  DOJ, DOA, PaymentMode, MemberStatus, AutoDebitEMI, GroupLeaderMsrNo, GroupLeaderDate

        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        //txtLoginID.Text = "";
        //txtRePassword.Text = "";
        //txtTransactionPassword.Text = "";
        txtMobile.Text = "";
        txtSTDCode.Text = "";
        txtLadline.Text = "";
        txtAddress.Text = "";
        txtLandmark.Text = "";
        ddlCountryName.SelectedIndex = 0;
        ddlStateName.SelectedIndex = 0;
        ddlCityName.SelectedIndex = 0;
        txtZIP.Text = "";
        //ddlMemberType.SelectedIndex = 0;
        //ddlPackage.SelectedIndex = 0;
    }

    public void fillPackage()
    {
        //DataTable dtPackage = new DataTable();
        //dtPackage = objPackage.ManagePackage("GetByMsrNo", 1);
        //ddlPackage.DataSource = dtPackage;
        //ddlPackage.DataValueField = "PackageID";
        //ddlPackage.DataTextField = "PackageName";
        //ddlPackage.DataBind();
        //ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
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


    protected void ddlCountryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillState(Convert.ToInt32(ddlCountryName.SelectedValue));
    }
    protected void ddlStateName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCity(Convert.ToInt32(ddlStateName.SelectedValue));
    }

    //protected void txtPinNumber_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtPinNumber.Text != "")
    //    {
    //        ValidatePin();
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter Pin Number !');", true);
    //    }

    //}
    #region Validate Pin
    private void ValidatePin()
    {
        //if (txtPinNumber.Text != "" && txtSerialNumber.Text != "")
        //{
        //    clsMLM_PinMaster objPinMaster = new clsMLM_PinMaster();
        //    DataTable dtPinMaster = new DataTable();

        //    dtPinMaster = objPinMaster.PinValidate(Convert.ToInt32(ddlPackage.SelectedValue), Convert.ToInt64(txtSerialNumber.Text), txtPinNumber.Text.Trim().Replace("'", "").ToString());
        //    if (dtPinMaster.Rows.Count > 0)
        //    {

        //    }
        //    else
        //    {
        //        clear();
        //        Function.MessageBox("Invalid Pin Details !");
        //    }
        //}
    }
    #endregion
   
    private string uploadFundRequestImage(FileUpload ff)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (ff.HasFile == true)
        {
            if (ff.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Images/Vendor/Actual/");
                string mpath = Server.MapPath("~/Images/Vendor/Medium/");
                string spath = Server.MapPath("~/Images/Vendor/Small/");


                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(ff.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                {
                    string FileName = DateTime.Now.Ticks.ToString() + Extension;
                    ff.PostedFile.SaveAs(opath + FileName);
                    objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 300);
                    objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 100, 100);

                    return FileName;
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Please select valid image !');", true);
                    return "";
                }
            }
        }
        return "";
    }


    protected void rbtnUPS_CheckedChanged(object sender, EventArgs e)
    {

        rbtnUPS.Checked = true;

        if (Request.QueryString["id"] != null)
        {
            lblAddEdit.Text = "Update Utility Payment Station Profile";
            litsubtitlemenu.Text = "Update Utility Payment Station Profile";
        }
        else
        {
            lblAddEdit.Text = "Utility Payment Station Registration";
            litsubtitlemenu.Text = "Utility Payment Station Registration";
        }
    }
    protected void rbtnSeller_CheckedChanged(object sender, EventArgs e)
    {
        rbtnSeller.Checked = true;

        if (Request.QueryString["id"] != null)
        {
            lblAddEdit.Text = "Update Seller Profile";
            litsubtitlemenu.Text = "Update Seller Profile";
        }
        else
        {
            lblAddEdit.Text = "Seller Registration";
            litsubtitlemenu.Text = "Seller Registration";
        }
    }


    protected void btnNext_Click1(object sender, EventArgs e)
    {
        DataTable Checkstatus = cls.select_data_dt("Exec Proc_Check 'Email','" + txtEmail.Text + "'");
        if (Checkstatus.Rows[0][0].ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('Duplicate Mail ID');", true);
            return;
        }
        Checkstatus = cls.select_data_dt("Exec Proc_Check 'Mobile','" + txtMobile.Text + "'");
        if (Checkstatus.Rows[0][0].ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('Duplicate Mobile');", true);
            return;
        }

        //if (txtravi.Text == "1")
        //{
        int parentMsrno = 0; int membertypeid = 0;
        int countryid = Convert.ToInt32(ddlCountryName.SelectedValue);
        int stateid = Convert.ToInt32(ddlStateName.SelectedValue);
        int cityid = Convert.ToInt32(ddlCityName.SelectedValue);
        string str = "select top 1 * from tblmlm_membermaster where countryid='" + countryid.ToString() + "' and stateid='" + stateid.ToString() + "' and cityid='" + cityid.ToString() + "' and msrno>1 and isactive=1 and isdelete=0 order by membertypeid desc";
        DataTable dt = new DataTable();
        dt = cls.select_data_dt(str);
        if (dt.Rows.Count > 0)
        {
            parentMsrno = Convert.ToInt32(dt.Rows[0]["msrno"]);
            membertypeid = 0;
            // lblowner.Text = dt.Rows[0]["memberid"].ToString() + "<br>" + dt.Rows[0]["mobile"].ToString();
            ///  cls.fill_MemberType(ddlMemberType, "");
        }
        else
        {
            parentMsrno = 1;
            //cls.fill_MemberType(ddlMemberType, "");
        }
        // hdnparentid.Value = parentMsrno.ToString();
        Random random = new Random();
        int SixDigit = random.Next(100000, 999999);
        string FName = txtFirstName.Text.Trim();
        string[] valueArray = new string[2];
        valueArray[0] = FName;
        valueArray[1] = SixDigit.ToString();
        Session["OTP"] = SixDigit.ToString();
        //SMS.SendWithVar(txtMobile.Text, 21, valueArray, 1);
        mv.ActiveViewIndex = 1;
        //}
        //else
        //{
        //    Response.Redirect("~/Default.aspx");
        //}

    }
}