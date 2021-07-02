using System;
using Common;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script;
using System.Web.Services;
using BLL;
using System.Xml;
using System.Linq;

public partial class MemberSignup : System.Web.UI.Page
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
    static string Firstname = "";
    static string lastname = "";
    static string Email = "";
    static string mobile = "";
    static string Stdcode = "";
    static string landline = "";
    static string address = "";
    static string type = "";
    static string country = "";
    static string state = "";
    static string city = "";
    static string cityname = "";
    static string zip = "";
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
            if (Request.Params["mmp_txn"] != null)
            {
                string postingmmp_txn = Request.Params["mmp_txn"].ToString();
                string postingmer_txn = Request.Params["mer_txn"];
                string postinamount = Request.Params["amt"].ToString();
                string postingprod = Request.Params["prod"].ToString();
                string postingdate = Request.Params["date"].ToString();
                string postingbank_txn = Request.Params["bank_txn"].ToString();
                string postingf_code = Request.Params["f_code"].ToString();
                string postingbank_name = Request.Params["bank_name"].ToString();
                string signature = Request.Params["signature"].ToString();
                string postingdiscriminator = Request.Params["discriminator"].ToString();
                string respHashKey = "239305a215bcc43f04";
               // string respHashKey = "KEYRESP123657234";
                string ressignature = "";
                string strsignature = postingmmp_txn + postingmer_txn + postingf_code + postingprod + postingdiscriminator + postinamount + postingbank_txn;
                byte[] bytes = Encoding.UTF8.GetBytes(respHashKey);
                byte[] b = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                ressignature = byteToHexString(b).ToLower();

                if (signature == ressignature)
                {
                    if (postingf_code == "C")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Cancelled');location.replace('MemberSignup.aspx');", true);
                    }
                    else if (postingf_code == "Ok")
                    {
                        string tx = Convert.ToString(Session["tx"]);
                        int id = Convert.ToInt32(Session["id"]);
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
                            //string strimage = profilepicupload(fupmppic);
                            string strimage = "";
                            DataTable dtresult = new DataTable();
                            int pwd4digit = random.Next(1000, 9999);
                            int transpin = random.Next(1000, 9999);
                            string password = pwd4digit.ToString();
                            string transpassord = transpin.ToString();
                            string hdfvalue = "";
                            string Name = Firstname + "" + lastname;
                            string ss = "Received";
                            DateTime dd = DateTime.Now;
                            dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + Firstname + "','" + lastname + "','" + Email + "','" + Function.changedatetommddyy(MDOB) + "','','" + password + "','" + transpassord + "','" + mobile + "','" + Stdcode + "','" + landline + "','" + address + "','" + type + "','" + Convert.ToInt32(country) + "','" + Convert.ToInt32(state) + "','" + Convert.ToInt32(city) + "','" + cityname + "','" + zip + "','','0', '" + hdfvalue + "', '0', '" + tx + "'");
                            cls.update_data("insert into regpaymentdetails(Payment,Name,RequestDate,Amount,txnID,membertype,mobile)values(1,'" + Name + "','" + dd + "','" + Convert.ToDecimal(Session["Amount"]) + "','" + tx + "','" + type + "','" + mobile + "')");
                            intresult = Convert.ToInt32(dtresult.Rows[0][0]);
                            if (intresult > 0)
                            {

                                Session["FirstName"] = null;
                                Session["LastName"] = null;
                                Session["Email"] = null;
                                Session["mobile"] = null;
                                Session["Stdcode"] = null;
                                Session["landline"] = null;
                                Session["address"] = null;
                                Session["type"] = null;
                                Session["country"] = null;
                                Session["state"] = null;
                                Session["city"] = null;
                                Session["cityname"] = null;
                                Session["zip"] = null;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Success|Your request has been sent to admin for approval. Concerning team will contact you soon.');location.replace('MemberSignup.aspx');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');", true);

                            }
                        }
                        catch (Exception ex)
                        {
                            cls.select_data_dt("insert into mtest values('"+ ex.ToString()+ "')");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');", true);
                        }
                        #endregion



                    }
                    else if (postingf_code == "F")
                    {
                        Session["FirstName"] = null;
                        Session["LastName"] = null;
                        Session["Email"] = null;
                        Session["mobile"] = null;
                        Session["Stdcode"] = null;
                        Session["landline"] = null;
                        Session["address"] = null;
                        Session["type"] = null;
                        Session["country"] = null;
                        Session["state"] = null;
                        Session["city"] = null;
                        Session["cityname"] = null;
                        Session["zip"] = null;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Failed');location.replace('MemberSignup.aspx');", true);
                    }

                }
                else
                {
                    Session["FirstName"] = null;
                    Session["LastName"] = null;
                    Session["Email"] = null;
                    Session["mobile"] = null;
                    Session["Stdcode"] = null;
                    Session["landline"] = null;
                    Session["address"] = null;
                    Session["type"] = null;
                    Session["country"] = null;
                    Session["state"] = null;
                    Session["city"] = null;
                    Session["cityname"] = null;
                    Session["zip"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Denied');location.replace('MemberSignup.aspx');", true);
                }

            }


        }
    }

    #endregion
    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
    #region [Insert | Update]

    //protected void btnSubmit1_Click(object sender, EventArgs e)
    //{
    //    if (Session["AmountPayable"] != null && Session["Wanttobe"] != null)
    //    {
    //        double totalamount = Convert.ToDouble(Session["AmountPayable"]);
    //        Session["Amount"] = totalamount.ToString();
    //        Session["type"] = DropDownList1.SelectedItem.ToString();
    //        type= DropDownList1.SelectedItem.ToString();
    //        Response.Redirect("AddCash.aspx");
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select What do you want to be than proceed');", true);
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

            MDOB = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
            try
            {
                string strimage = "";
                DataTable dtresult = new DataTable();

                int pwd4digit = random.Next(1000, 9999);
                int transpin = random.Next(1000, 9999);

                string password = pwd4digit.ToString();
                string transpassord = transpin.ToString();
                string hdfvalue = "";
                //dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + Session["FirstName"].ToString() + "','" + Session["LastName"].ToString() + "','" + Session["Email"].ToString() + "','" + Function.changedatetommddyy(MDOB) + "','','" + password + "','" + transpassord + "','" + Session["mobile"].ToString() + "','" + Session["Stdcode"].ToString() + "','" + Session["landline"].ToString() + "','" + Session["address"].ToString() + "','" + Session["type"].ToString() + "','" + Convert.ToInt32(Session["country"]) + "','" + Convert.ToInt32(Session["state"]) + "','" + Convert.ToInt32(Session["city"]) + "','" + Session["cityname"].ToString() + "','" + Session["zip"].ToString() + "','','0', '" + hdfvalue + "', '0', '" + tx + "'");
                dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtEmail.Text + "','" + Function.changedatetommddyy(MDOB) + "','','" + password + "','" + transpassord + "','" + txtMobile.Text + "','','','" + txtAddress.Text + "','" + txtLandmark.Text + "','" + Convert.ToInt32(ddlCountryName.SelectedValue) + "','" + Convert.ToInt32(ddlStateName.SelectedValue) + "','" + Convert.ToInt32(ddlCityName.SelectedValue) + "','" + ddlCityName.SelectedItem.Text + "','" + txtZIP.Text + "','','0', '" + hdfvalue + "', '0', '" + strimage + "'");
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

    protected void btnSubmit1_razorpay(object sender, EventArgs e)
    {
        if (Session["AmountPayable"] != null && Session["Wanttobe"] != null)
        {
            double totalamount = Convert.ToDouble(Session["AmountPayable"]);
            Session["txtAmount"] = totalamount.ToString();
            Session["type"] = DropDownList1.SelectedItem.ToString();
            type = DropDownList1.SelectedItem.ToString();
            string tx = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            Random random = new Random();
            int id = random.Next(100000000, 999999999);
            Session["tx"] = tx;
            Session["txnid"] = id;
            Session["txtAmount"] = totalamount;
            Session["Returnurl"] = "addwallet";
            Session["checkout"] = "yes";
            Response.Redirect("payment.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select What do you want to be than proceed');", true);
        }

    }

    private string profilepicupload(FileUpload fpup)
    {
        clsImageResize objImageResize = new clsImageResize();
        string filename = System.IO.Path.GetFileName(fpup.FileName);
        //hdnregimg.Value = filename;


        string opath = Server.MapPath("~/Images/Profile/actual/");
     

        //Check file extension (must be JPG)
        string Extension = System.IO.Path.GetExtension(fpup.FileName).ToLower();
        if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
        {
            string FileName = DateTime.Now.Ticks.ToString() + Extension;

            //hdnregimg.Value = FileName;
            fpup.PostedFile.SaveAs(opath + FileName);
           

        }

        return filename;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex > 0)
        {
            Session["AmountPayable"] = DropDownList1.SelectedValue;
            Session["Wanttobe"] = DropDownList1.SelectedItem.ToString();
            double totalamount = Convert.ToDouble(Session["AmountPayable"]);
            lblpayamount.Text = "Online recharge amount " + " " + DropDownList1.SelectedValue + " " + "For" + " " + Session["Wanttobe"].ToString();
            Session["FirstName"] = txtFirstName.Text;
            Session["LastName"] = txtLastName.Text;
            Session["Email"] = txtEmail.Text;
            Session["mobile"] = txtMobile.Text;
            Session["Stdcode"] = txtSTDCode.Text;
            Session["landline"] = txtLadline.Text;
            Session["address"] = txtAddress.Text;
            Session["country"] = ddlCountryName.SelectedValue;
            Session["state"] = ddlStateName.SelectedValue;
            Session["city"] = ddlCityName.SelectedValue;
            Session["cityname"] = ddlCityName.SelectedItem.Text;
            Session["zip"] = txtZIP.Text;
        }
        else
        {
            Session["AmountPayable"] = null;
            Session["Wanttobe"] = null;
            lblpayamount.Text = "";
        }
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

    }

    private void clear()
    {


        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        txtMobile.Text = "";
        txtSTDCode.Text = "";
        txtLadline.Text = "";
        txtAddress.Text = "";
        txtLandmark.Text = "";
        ddlCountryName.SelectedIndex = 0;
        ddlStateName.SelectedIndex = 0;
        ddlCityName.SelectedIndex = 0;
        txtZIP.Text = "";
    }

    public void fillPackage()
    {

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
    protected void btnNext_Click(object sender, EventArgs e)
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


        Session["FirstName"] = txtFirstName.Text;
        Session["LastName"] = txtLastName.Text;
        Session["Email"] = txtEmail.Text;
        Session["mobile"] = txtMobile.Text;
        Session["Stdcode"] = txtSTDCode.Text;
        Session["landline"] = txtLadline.Text;
        Session["address"] = txtAddress.Text;
        Session["country"] = ddlCountryName.SelectedValue;
        Session["state"] = ddlStateName.SelectedValue;
        Session["city"] = ddlCityName.SelectedValue;
        Session["cityname"] = ddlCityName.SelectedItem.Text;
        Session["zip"] = txtZIP.Text;



        Firstname = txtFirstName.Text;
        lastname = txtLastName.Text;
       Email= txtEmail.Text;
        mobile= txtMobile.Text;
        Stdcode = txtSTDCode.Text;
       landline = txtLadline.Text;
        address = txtAddress.Text;
        country = ddlCountryName.SelectedValue;
        state = ddlStateName.SelectedValue;
        city= ddlCityName.SelectedValue;
       cityname = ddlCityName.SelectedItem.Text;
        zip = txtZIP.Text;


        // hdnparentid.Value = parentMsrno.ToString();
        Random random = new Random();
        int SixDigit = random.Next(100000, 999999);
        string FName = txtFirstName.Text.Trim();
        string[] valueArray = new string[2];
        valueArray[0] = FName;
        valueArray[1] = SixDigit.ToString();
        Session["OTP"] = SixDigit.ToString();
        //SMS.SendWithVar(txtMobile.Text, 21, valueArray, 1);
        mv.ActiveViewIndex = 2;
        //}
        //else
        //{
        //    Response.Redirect("~/Default.aspx");
        //}
    }

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
}