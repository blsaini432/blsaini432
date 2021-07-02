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
using System.IO;
public partial class userlogin : System.Web.UI.Page
{
    #region Load
    public static string adminurl = ConfigurationManager.AppSettings["adminurl"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            // bindbanner();
            loaddata(2);
        }
    }
    #endregion

    #region Events
    protected void btn_login_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_username.Text != "" && txt_userpassword.Text != "")
            {
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@LoginID", value = txt_username.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Password", value = txt_userpassword.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Action", value = "bizLogin" });
                cls_connection cls = new cls_connection();
                DataTable dt = cls.select_data_dtNew("Proc_UniversalLogin ", _lstparm);
                if (dt.Rows.Count > 0)
                {
                    Session["tpassword"] = dt.Rows[0]["TransactionPassword"].ToString();
                    Session["membertypeid"] = dt.Rows[0]["MemberTypeId"].ToString();
                    divlogin.Visible = false;
                    divotp.Visible = true;
                    Session.Add("dtMember", dt);
                    InsertMemberMasterLoginDetail(Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString()), 0);
                    UpdateMemberMasterLastLogin(Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString()));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Account is not active. Please Contact to administrator !!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Valid Data');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(" + ex.ToString() + ");", true);
        }
    }
    protected void btn_LoginVerify_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["membertypeid"] != null)
            {
                if (txt_otp.Text != "")
                {
                    if (txt_otp.Text == Convert.ToString(Session["tpassword"]))
                    {
                        if (Convert.ToString(Session["membertypeid"]) == "5" || Convert.ToString(Session["membertypeid"]) == "6")
                        {
                            Session["tpassword"] = null;
                            Session["membertypeid"] = null;
                            Session.Add("dtRetailer", Session["dtMember"]);
                            Response.Redirect("~/Root/Retailer/DashBoard.aspx");
                        }
                        else if (Convert.ToString(Session["membertypeid"]) == "2" || Convert.ToString(Session["membertypeid"]) == "3" || Convert.ToString(Session["membertypeid"]) == "4")
                        {
                            Session["tpassword"] = null;
                            Session["membertypeid"] = null;
                            Session.Add("dtDistributor", Session["dtMember"]);
                            Response.Redirect("~/Root/Distributor/DashBoard.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid Transaction Pin !!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enter Transaction Pin');", true);
                }
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Occured !! Please Try Again');", true);
        }
    }
    protected void btn_sendforgetpin_Click(object sender, EventArgs e)
    {
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@LoginID", value = txt_forgetpin.Text.Trim() });
        _lstparm.Add(new ParmList() { name = "@Password", value = "" });
        _lstparm.Add(new ParmList() { name = "@Action", value = "forgetLogin" });
        cls_connection cls = new cls_connection();
        DataTable dt = cls.select_data_dtNew("Proc_UniversalLogin ", _lstparm);
        if (dt.Rows.Count > 0)
        {
            string mobile = "919166396947";
            //string mobile = "91" + dt.Rows[0]["Mobile"].ToString();
            string token = genratestring();
            int id = Convert.ToInt32(dt.Rows[0]["msrno"].ToString());
            cls.update_data("update tblmlm_membermaster set pintoken='" + token + "' where msrno=" + id + "");
            string Token = token;
            string aa = adminurl + "pinrset.aspx?utken=" + Token;
            string bb = ShrinkURL(aa);
            string sms = "Dear Member, Welcome to our Company, Your Pin reset link is: http://" + bb + " ThanksTeam PayMaster";
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=349678AzMGs1chj5fdc6473P1&route=4&sender=PYMSTR&DLT_TE_ID=1207162220306040162&mobiles=" + mobile + "&message=" + sms + "";
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            string Email = dt.Rows[0]["Email"].ToString();
            SendPinMail(Token, Email);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Pin Reset Link has been sent to your registerd mobile or email');window.location ='userlogin.aspx';", true);


        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This User Is not registered with us');", true);
        }
    }


    protected void btn_forgot_Click(object sender, EventArgs e)
    {
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@LoginID", value = txt_email.Text.Trim() });
        _lstparm.Add(new ParmList() { name = "@Password", value = "" });
        _lstparm.Add(new ParmList() { name = "@Action", value = "forgetLogin" });
        cls_connection cls = new cls_connection();
        DataTable dt = cls.select_data_dtNew("Proc_UniversalLogin ", _lstparm);
        if (dt.Rows.Count > 0)
        {
            string mobile = "919166396947";
            string token = genratestring();
            int id = Convert.ToInt32(dt.Rows[0]["msrno"].ToString());
            cls.update_data("update tblmlm_membermaster set passwordtoken='" + token + "' where msrno=" + id + "");
            string Token = token;
            string aa = adminurl + "pwdrset.aspx?utken=" + Token;
            string bb = ShrinkURL(aa);
            string sms = "Dear Member, Welcome to our Company, Your password reset link is: http://" + bb + " ThanksTeam PayMaster";
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=349678AzMGs1chj5fdc6473P1&route=4&sender=PYMSTR&DLT_TE_ID=1207162220269571623&mobiles=" + mobile + "&message=" + sms + "";
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
            string Email = dt.Rows[0]["Email"].ToString();
            SendForgetMail(Token, Email);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password Reset Link has been sent to your registerd mobile or email');window.location ='userlogin.aspx';", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This User Is not registered with us');", true);
        }
    }
    public static void SendForgetMail(string Token, string Email)
    {
        try
        {
            string[] valueArray = new string[1];
            valueArray[0] = adminurl + "pwdrset.aspx?utken=" + Token;
            FlexiMail objSendMail = new FlexiMail();
            objSendMail.To = Email;
            objSendMail.CC = "";
            objSendMail.BCC = "support@DigitalPaymentSolution.c";
            objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
            objSendMail.FromName = "digitalpaymentsolution.in";
            objSendMail.MailBodyManualSupply = false;
            objSendMail.EmailTemplateFileName = "ForgetPassword.htm";
            objSendMail.Subject = "Password Recovery Mail";
            objSendMail.ValueArray = valueArray;
            objSendMail.Send();
        }
        catch (Exception ex)
        {

        }
    }

    public static void SendPinMail(string Token, string Email)
    {
        try
        {
            string[] valueArray = new string[1];
            valueArray[0] = adminurl + "pinrset.aspx?utken=" + Token;
            FlexiMail objSendMail = new FlexiMail();
            objSendMail.To = Email;
            objSendMail.CC = "";
            objSendMail.BCC = "support@DigitalPaymentSolution.c ";
            objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
            objSendMail.FromName = "digitalpaymentsolution.in";
            objSendMail.MailBodyManualSupply = false;
            objSendMail.EmailTemplateFileName = "ForgetPin.htm";
            objSendMail.Subject = "Transaction Pin Recovery Mail";
            objSendMail.ValueArray = valueArray;
            objSendMail.Send();
        }
        catch (Exception ex)
        {

        }
    }
    #endregion

    #region Utility Functions
    public static string genratestring()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";
        string characters = numbers;
        characters += alphabets + small_alphabets + numbers;
        int length = int.Parse("25");
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }
        return otp;
    }


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
            Session["email"] = dtCompany.Rows[0]["Email"].ToString();
            Session["website"] = dtCompany.Rows[0]["website"].ToString();
            Session["Mobile"] = dtCompany.Rows[0]["Mobile"].ToString();

        }
    }

    private void InsertMemberMasterLoginDetail(int MsrNo, int isverified)
    {
        Int32 intresult = 0;
        cls_connection cls = new cls_connection();
        intresult = cls.insert_data("Exec ProcMLM_AddEditMemberMasterLoginDetail_otp 0,'" + Convert.ToString(Request.UserHostAddress) + "','" + MsrNo.ToString() + "','" + isverified.ToString() + "'");

    }

    private void UpdateMemberMasterLastLogin(int MsrNo)
    {
        cls_Universal objUniversal = new cls_Universal();
        objUniversal.UpdateLastLogin("UpdateMemberLastLogin", MsrNo, Convert.ToString(Request.UserHostAddress));
    }
    #endregion

    private string ShrinkURL(string strURL)
    {

        string URL;
        URL = "http://tinyurl.com/api-create.php?url=" + strURL.ToLower();
        System.Net.HttpWebRequest objWebRequest;
        System.Net.HttpWebResponse objWebResponse;

        System.IO.StreamReader srReader;
        string strHTML;
        objWebRequest = (System.Net.HttpWebRequest)System.Net
           .WebRequest.Create(URL);
        objWebRequest.Method = "GET";
        objWebResponse = (System.Net.HttpWebResponse)objWebRequest
           .GetResponse();
        srReader = new System.IO.StreamReader(objWebResponse
           .GetResponseStream());
        strHTML = srReader.ReadToEnd();
        srReader.Close();
        objWebResponse.Close();
        objWebRequest.Abort();
        return (strHTML);
    }

    public void bindbanner()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblbanner where isactive=1 and isdelete=0 order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
             repeater1.DataSource = dt;
              repeater1.DataBind();
        }

    }
    protected void linkbtnpin_Click(object sender, EventArgs e)
    {
        div_forgotpassword.Visible = false;
        divotp.Visible = false;
        divlogin.Visible = false;
        div_forgotpin.Visible = true;

    }
    protected void linkbtnforgot_Click(object sender, EventArgs e)
    {
        div_forgotpassword.Visible = true;
        divotp.Visible = false;
        divlogin.Visible = false;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        div_forgotpin.Visible = false;
        div_forgotpassword.Visible = false;
        divotp.Visible = false;
        divlogin.Visible = true;
    }
}