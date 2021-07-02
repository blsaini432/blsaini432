using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Configuration;
using System.Net;

public partial class adminlogin : System.Web.UI.Page
{
   // IPLog lg = new IPLog();

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpBrowserCapabilities httpBrowser = Request.Browser;
            bool enableJavascript = httpBrowser.JavaScript;
            if (enableJavascript == true)
            {
                loaddata(2);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Must enabled javascript from your browser !!');", true);
            }
        }
    }
    #endregion

    #region Events
    protected void btn_login_Click(object sender, EventArgs e)
    {
        try
        {
            string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string HostIp = HttpContext.Current.Request.UserHostAddress;
            HttpBrowserCapabilities httpBrowser = Request.Browser;
            bool enableJavascript = httpBrowser.JavaScript;
            if (enableJavascript == true)
            {
                if (txt_username.Text != "" && txt_userpassword.Text != "")
                {
                    //lg.Log(txt_username.Text, txt_userpassword.Text, domainName, HostIp, "");
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@LoginID", value = txt_username.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@Password", value = txt_userpassword.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@Action", value = "LoginEmployee" });
                    cls_connection cls = new cls_connection();
                    DataTable dt = cls.select_data_dtNew("Proc_UniversalLogin", _lstparm);
                    if (dt.Rows.Count > 0)
                    {
                        //string islockedcount = dt.Rows[0]["islockedcount"].ToString();
                        //string islocked = dt.Rows[0]["islocked"].ToString();
                        //if (Convert.ToBoolean(islocked) == true || Convert.ToInt32(islockedcount) == Convert.ToInt32(3))
                        //{
                        //    Session.Clear();
                        //    Session.RemoveAll();
                        //    Session.Abandon();
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Account Has been Tempararoy Locked. Please Contact to administrator !!');window.location ='adminlogin.aspx';", true);
                        //}
                     //   if
                        {
                            string ip = dt.Rows[0]["LastLoginIP"].ToString();
                            string email = dt.Rows[0]["email"].ToString();
                            string mobile = dt.Rows[0]["mobile"].ToString();
                            string EmployeeName = dt.Rows[0]["EmployeeName"].ToString();
                            if (ip != Request.UserHostAddress)
                            {
                                int otp = genraterandom();
                            //   sendsms("9166396947", EmployeeName, otp);
                                Session["OTP"] = otp;
                                Session["LoginID"] = txt_username.Text.Trim();
                                Session["PWD"] = txt_userpassword.Text.Trim();
                                divlogin.Visible = false;
                                divotp.Visible = true;
                                sendemail(email, Convert.ToString(otp));
                                Session["OTP"] = otp;
                                divlogin.Visible = false;
                                divotp.Visible = true;
                                List<ParmList> _lstpaaarmss = new List<ParmList>();
                                _lstpaaarmss.Add(new ParmList() { name = "@Action", value = "upotpcount" });
                                _lstpaaarmss.Add(new ParmList() { name = "@LoginID", value = txt_username.Text.Trim() });
                                cls.select_data_dtNew("Sp_LockedAccount", _lstpaaarmss);

                            }
                            else
                            {
                                InsertEmployeeLoginDetail(Convert.ToInt32(dt.Rows[0]["EmployeeID"].ToString()));
                                UpdateEmployeeLastLogin(Convert.ToInt32(dt.Rows[0]["EmployeeID"].ToString()));
                                Session.Add("dtEmployee", dt);
                                Session.Add("EmployeeID", dt.Rows[0]["EmployeeID"].ToString());
                                Response.Redirect("~/Root/Administrator/DashBoard.aspx");
                            }
                        }
                    }
                    else
                    {
                        List<ParmList> _lstparmss = new List<ParmList>();
                        _lstparmss.Add(new ParmList() { name = "@Action", value = "Invalid" });
                        _lstparmss.Add(new ParmList() { name = "@LoginID", value = txt_username.Text.Trim() });
                        cls.select_data_dtNew("Sp_LockedAccount", _lstparmss);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Details is invalid kindly check and try again');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Valid Data');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Must enabled javascript from your browser !!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(" + ex.ToString() + ");", true);
        }
    }
    #endregion

    #region Utility Functions

    public void loaddata(int ID)
    {
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@ID", value = ID });
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetAll" });
        cls_connection cls = new cls_connection();
        DataTable dtCompany = cls.select_data_dtNew("Proc_ManageCompany ", _lstparm);
        if (dtCompany.Rows.Count > 0)
        {
            imglogo.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtCompany.Rows[0]["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtCompany.Rows[0]["companylogo"]);
            imglogo.AlternateText = dtCompany.Rows[0]["CompanyName"].ToString();
            lblCopyright.Text = dtCompany.Rows[0]["Copyright"].ToString();

        }
    }

    public int genraterandom()
    {
        Random random = new Random();
        int SixDigit = random.Next(1000, 9999);
        return SixDigit;
    }
    public int sendsms(string mobile, string empname, int otp)
    {
        int i = 0;
        string[] valueArray = new string[2];
        valueArray[0] = empname;
        valueArray[1] = otp.ToString();
        DLTSMS.SendWithVar(mobile, 1, valueArray, 1);
        return i;
    }

    public static void sendemail(string email, string password)
    {
        try
        {
            string[] valueArray = new string[1];
            valueArray[0] = password;
            FlexiMail objSendMail = new FlexiMail();
            objSendMail.To = email;
            objSendMail.CC = "";
            objSendMail.BCC = "";
            objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
            objSendMail.FromName = "OTP From Admin";
            objSendMail.MailBodyManualSupply = false;
            objSendMail.EmailTemplateFileName = "Code.htm";
            objSendMail.Subject = "OTP For Login admin account";
            objSendMail.ValueArray = valueArray;
            objSendMail.Send();
        }
        catch (Exception ex)
        {

        }
    }

    private void InsertEmployeeLoginDetail(int EmployeeID)
    {
        Int32 intresult = 0;
        clsEmployeeLoginDetail objEmployeeLoginDetail = new clsEmployeeLoginDetail();
        intresult = objEmployeeLoginDetail.AddEditEmployeeLoginDetail(0, Convert.ToString(Request.UserHostAddress), EmployeeID);
    }

    private void UpdateEmployeeLastLogin(int EmployeeID)
    {
        cls_Universal objUniversal = new cls_Universal();
        objUniversal.UpdateLastLogin("UpdateEmployeeLastLogin", EmployeeID, Convert.ToString(Request.UserHostAddress));
    }
    #endregion

    protected void btn_LoginVerify_Click(object sender, EventArgs e)
    {
        try
        {
            HttpBrowserCapabilities httpBrowser = Request.Browser;
            bool enableJavascript = httpBrowser.JavaScript;
            if (enableJavascript == true)
            {
                if (txt_otp.Text != "" && Session["LoginID"] != null)
                {
                    List<ParmList> _lstpaarm = new List<ParmList>();
                    _lstpaarm.Add(new ParmList() { name = "@LoginID", value = Convert.ToString(Session["LoginID"]) });
                    _lstpaarm.Add(new ParmList() { name = "@Action", value = "ckcount" });
                    cls_connection cls = new cls_connection();
                    DataTable dsst = cls.select_data_dtNew("Sp_LockedAccount", _lstpaarm);
                    if (dsst.Rows.Count > 0)
                    {
                        int chkcount = Convert.ToInt32(dsst.Rows[0]["isotpcount"]);
                        if (chkcount == Convert.ToInt32(3))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('OTP Limit Exceed !!');window.location ='adminlogin.aspx';", true);
                        }
                        else
                        {
                            if (txt_otp.Text == Convert.ToString(Session["OTP"]))
                            {
                                List<ParmList> _lstparm = new List<ParmList>();
                                _lstparm.Add(new ParmList() { name = "@LoginID", value = Convert.ToString(Session["LoginID"]) });
                                _lstparm.Add(new ParmList() { name = "@Password", value = Convert.ToString(Session["PWD"]) });
                                _lstparm.Add(new ParmList() { name = "@Action", value = "LoginEmployee" });
                                
                                DataTable dt = cls.select_data_dtNew("Proc_UniversalLogin", _lstparm);
                                if (dt.Rows.Count > 0)
                                {
                                    Session.Add("dtEmployee", dt);
                                    Session.Add("EmployeeID", dt.Rows[0]["EmployeeID"].ToString());
                                    Session["OTP"] = null;
                                    InsertEmployeeLoginDetail(Convert.ToInt32(Session["EmployeeID"]));
                                    UpdateEmployeeLastLogin(Convert.ToInt32(Session["EmployeeID"]));
                                    Session["LoginID"] = null;
                                    Session["PWD"] = null;
                                    List<ParmList> _lstsparsm = new List<ParmList>();
                                    _lstsparsm.Add(new ParmList() { name = "@Action", value = "Ok" });
                                    _lstsparsm.Add(new ParmList() { name = "@LoginID", value = Convert.ToString(Session["LoginID"]) });
                                    cls.select_data_dtNew("Sp_LockedAccount", _lstsparsm);
                                    Response.Redirect("~/Root/Administrator/DashBoard.aspx");
                                }
                                else
                                {
                                    List<ParmList> _lstparsm = new List<ParmList>();
                                    _lstparsm.Add(new ParmList() { name = "@Action", value = "Invalidotp" });
                                    _lstparsm.Add(new ParmList() { name = "@LoginID", value = Convert.ToString(Session["LoginID"]) });
                                 
                                    cls.select_data_dtNew("Sp_LockedAccount", _lstparsm);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid Session !!');", true);
                                }
                            }
                            else
                            {
                                List<ParmList> _lstparmss = new List<ParmList>();
                                _lstparmss.Add(new ParmList() { name = "@Action", value = "Invalidotp" });
                                _lstparmss.Add(new ParmList() { name = "@LoginID", value = Convert.ToString(Session["LoginID"]) });
                               
                                cls.select_data_dtNew("Sp_LockedAccount", _lstparmss);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid OTP !!');", true);
                            }
                        }
                    }              
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter OTP');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Must enabled javascript from your browser !!');", true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Occured !! Please Try Again');", true);
        }
    }

    protected void btn_forgot_Click(object sender, EventArgs e)
    {

    }
}