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
                string respHashKey = "8c1cbeea8561751135";
             //   string respHashKey = "KEYRESP123657234";
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
                            string strimage = profilepicupload(fupmppic);
                            DataTable dtresult = new DataTable();
                            int pwd4digit = random.Next(1000, 9999);
                            int transpin = random.Next(1000, 9999);
                            string password = pwd4digit.ToString();
                            string transpassord = transpin.ToString();
                            string hdfvalue = "";
                            string Name = Session["FirstName"].ToString() + "" + Session["LastName"].ToString();
                            string ss = "Received";
                            DateTime dd = DateTime.Now;
                            cls.update_data("insert into regpaymentdetails(Payment,Name,RequestDate,Amount,txnID,membertype,mobile)values(1,'" + Name + "','" + dd + "','" + Convert.ToDecimal(Session["Amount"]) + "','" + tx + "','" + Session["type"].ToString() + "','" + Session["mobile"].ToString() + "')");
                            dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + Session["FirstName"].ToString() + "','" + Session["LastName"].ToString() + "','" + Session["Email"].ToString() + "','" + Function.changedatetommddyy(MDOB) + "','','" + password + "','" + transpassord + "','" + Session["mobile"].ToString() + "','" + Session["Stdcode"].ToString() + "','" + Session["landline"].ToString() + "','" + Session["address"].ToString() + "','" + Session["type"].ToString() + "','" + Convert.ToInt32(Session["country"]) + "','" + Convert.ToInt32(Session["state"]) + "','" + Convert.ToInt32(Session["city"]) + "','" + Session["cityname"].ToString() + "','" + Session["zip"].ToString() + "','','0', '" + hdfvalue + "', '0', '" + tx + "'");
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

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        //if (Session["AmountPayable"] != null && Session["Wanttobe"] != null)
        //{
        //    double totalamount = Convert.ToDouble(Session["AmountPayable"]);
        //    Session["txtAmount"] = totalamount.ToString();
        //    string tx = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
        //    Random random = new Random();
        //    int id = random.Next(100000000, 999999999);
        //    Session["txnid"] = tx;
        //    Session["Returnurl"] = "addwallet";
        //    Session["checkout"] = "yes";
        //    Session["type"] = DropDownList1.SelectedItem.ToString();
        //    Response.Redirect("Payment.aspx");
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select What do you want to be than proceed');", true);
        //}
        #region [Insert]
        Random randoms = new Random();
        int tx = randoms.Next(100000000, 999999999);
        Session["txnid"] = tx;
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
            //string strimage = profilepicupload(fupmppic);
            string strimage = "";
            DataTable dtresult = new DataTable();
            int pwd4digit = random.Next(1000, 9999);
            int transpin = random.Next(1000, 9999);
            string password = pwd4digit.ToString();
            string transpassord = transpin.ToString();
            string hdfvalue = "";
            string firstname = Session["FirstName"].ToString();
            string lastname = Session["LastName"].ToString();
            string Name = firstname + "" + lastname;
            string email = Session["Email"].ToString();
            string mobile = Session["mobile"].ToString();
            string stdcode = Session["Stdcode"].ToString();
            string landline = Session["landline"].ToString();
            string address = Session["address"].ToString();
            string country = Session["country"].ToString();
            string state = Session["state"].ToString();
            string city = Session["city"].ToString();
            string cityname = Session["cityname"].ToString();
            string type = DropDownList1.SelectedItem.ToString();
            string zip = Session["zip"].ToString();
            string ss = "Received";
            DateTime dd = DateTime.Now;
            dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + firstname + "','" + lastname + "','" + email + "','" + "" + "','','" + password + "','" + transpassord + "','" + mobile + "','" + stdcode + "','" + landline + "','" + address + "','" + type + "','" + Convert.ToInt32(country) + "','" + Convert.ToInt32(state) + "','" + Convert.ToInt32(city) + "','" + cityname + "','" + zip + "','','0', '" + hdfvalue + "', '0', '" + tx + "'");
          //  cls.update_data("insert into regpaymentdetails(Payment,Name,RequestDate,Amount,txnID,membertype,mobile)values(1,'" + Name + "','" + dd + "','" + amount + "','" + tx + "','" + type + "','" + mobile + "')");
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Success|Your request has been sent to admin for approval. Concerning team will contact you soon.');location.replace('Signup.aspx');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');location.replace('Signup.aspx');", true);

            }
        }
        catch (Exception ex)
        {
            cls.select_data_dt("insert into mtest values('" + ex.ToString() + "')");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !')location.replace('Signup.aspx');", true);
        }
        #endregion

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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex > 0)
        {
            Session["AmountPayable"] = DropDownList1.SelectedValue;
            Session["Wanttobe"] = DropDownList1.SelectedItem.ToString();
            double totalamount = Convert.ToDouble(Session["AmountPayable"]) + (Convert.ToDouble(Session["AmountPayable"]) * 18) / 100;
            lblpayamount.Text = "You will be charged:" + " " + DropDownList1.SelectedValue ;
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