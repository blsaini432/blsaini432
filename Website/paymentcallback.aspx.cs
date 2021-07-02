using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;
using System.Collections.Specialized;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
using System.Xml;
public partial class paymentcallback : System.Web.UI.Page
{
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    public string status;
    public string wallet;
    public string method;
    public string cardid;
    public string vpa;
    private static string keyid = "rzp_live_2vjii3tMpb3IJt";
    private static string secretkey = "OUZhdVGkLJIkqu52V9nKjbrx";
   // private static string keyid = "rzp_test_kElMhQa7Ic114m";
   // private static string secretkey = "AW4JdqXW7YX6lPQ4Yv2uhfBY";
    protected void Page_Load(object sender, EventArgs e)
    {
        string strPreviousPage = "";
        if (Request.UrlReferrer != null)
        {
            strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        }
        if (strPreviousPage == "")
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/Signup.aspx");
        }
        if (Session["PayOrderId"] != null && Session["txnid"] != null && Request.Form["razorpay_payment_id"] != null)
        {
            var paymentId = Request.Form["razorpay_payment_id"];
            string razorpay_order_id = Request.Form["razorpay_order_id"];
            string razorpay_signature = Request.Form["razorpay_signature"];
            Dictionary<string, object> input = new Dictionary<string, object>();
            decimal amount = Convert.ToDecimal(Session["txtAmount"].ToString());
            string finalamount = amount + "00";
            decimal pgamount = Convert.ToDecimal(finalamount);
            input.Add("amount", pgamount); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", "order_rcptid_11");
            input.Add("payment_capture", 1);// this amount should be same as transaction amount
            RazorpayClient client = new RazorpayClient(keyid, secretkey);
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("razorpay_payment_id", paymentId);
            attributes.Add("razorpay_order_id", razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay_signature);
            Utils.verifyPaymentSignature(attributes);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768;
            Payment payment = client.Payment.Fetch(paymentId);
            status = payment["status"].ToString();
            cardid = payment["card_id"].ToString();
            vpa = payment["vpa"].ToString();
            method = payment["method"].ToString();         
            if (Session["Returnurl"].ToString() == "addwallet")
            {
                string tx = Convert.ToString(Session["tx"]);
                int id = Convert.ToInt32(Session["id"]);
                #region [Insert]
                string membertypeid;
                int packageid;
                Random random = new Random();
                int SixDigit = random.Next(100000, 999999);
                string MemberID = "";
                string membertype = Session["Wanttobe"].ToString();
                if (membertype == "Retailer")
                {
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select * from tblmlm_package where Packagename='RT'");
                    packageid = Convert.ToInt32(dt.Rows[0]["PackageID"]);
                    membertypeid = "5";
                    Session["membertypeid"] = membertypeid;
                    Session["packageid"] = packageid;                   
                }
                else if (membertype == "Distributor")
                {
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select * from tblmlm_package where Packagename='DT'");
                    packageid = Convert.ToInt32(dt.Rows[0]["PackageID"]);
                    membertypeid = "4";
                    Session["membertypeid"] = membertypeid;
                    Session["packageid"] = packageid;                
                }
                else if (membertype == "Master Distributor")
                {
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select * from tblmlm_package where Packagename='MD'");
                    packageid = Convert.ToInt32(dt.Rows[0]["PackageID"]);
                    membertypeid = "3";
                    Session["membertypeid"] = membertypeid;
                    Session["packageid"] = packageid;
                    
                }
                else if (membertype == "State Head")
                {
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select * from tblmlm_package where Packagename='STATE HEAD'");
                    packageid = Convert.ToInt32(dt.Rows[0]["PackageID"]);
                    membertypeid = "2";
                    Session["membertypeid"] = membertypeid;
                    Session["packageid"] = packageid;                  
                }
                DataTable dtm = new DataTable();
                List<ParmList> _lstparms = new List<ParmList>();
                _lstparms.Add(new ParmList() { name = "@Action", value = "getmembership" });
                dtm = cls.select_data_dtNew("ProcMLM_AddNewMemberMaster", _lstparms);
                if (dtm.Rows.Count > 0)
                {
                    for (int j = 0; j < dtm.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(Session["membertypeid"]) == Convert.ToInt32(dtm.Rows[j]["membertypeid"]))
                        {
                            MemberID = dtm.Rows[j]["mcode"].ToString() + SixDigit;
                        }
                    }
                }
                string DOJ = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
                string MDOB = "";
                MDOB = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
                try
                {                  
                    string strimage = "";
                    DataTable dtresult = new DataTable();
                    int pwd6digit = random.Next(10000, 99999);
                    int transpin = random.Next(1000, 9999);
                    string password = pwd6digit.ToString();
                    string transactionpin = transpin.ToString();                 
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
                    string type = Session["type"].ToString();
                    string zip = Session["zip"].ToString();
                    string ss = "Received";
                    DateTime dd = DateTime.Now;
                    //   dtresult = cls.select_data_dt("Exec ProcMLM_AddNewMemberMaster 0,'" + MemberID + "','" + firstname + "','" + lastname + "','" + email + "','" + "" + "','','" + password + "','" + transpassord + "','" + mobile + "','" + stdcode + "','" + landline + "','" + address + "','" + type + "','" + Convert.ToInt32(country) + "','" + Convert.ToInt32(state) + "','" + Convert.ToInt32(city) + "','" + cityname + "','" + zip + "','','0', '" + hdfvalue + "', '0', '" + tx + "'");
                    DataTable dt = new DataTable();
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@MemberID", value = MemberID });
                    _lstparm.Add(new ParmList() { name = "@FirstName", value = firstname });
                    _lstparm.Add(new ParmList() { name = "@ShopName", value = "" });
                    _lstparm.Add(new ParmList() { name = "@PackageID", value = Session["packageid"] });
                    _lstparm.Add(new ParmList() { name = "@LastName", value = lastname });
                    _lstparm.Add(new ParmList() { name = "@Email", value = email });
                    _lstparm.Add(new ParmList() { name = "@DOB", value = "" });
                    _lstparm.Add(new ParmList() { name = "@Gender", value = "" });
                    _lstparm.Add(new ParmList() { name = "@Password", value = password });
                    _lstparm.Add(new ParmList() { name = "@TransactionPassword", value = transactionpin });
                    _lstparm.Add(new ParmList() { name = "@Mobile", value = mobile });
                    _lstparm.Add(new ParmList() { name = "@STDCode", value = stdcode });
                    _lstparm.Add(new ParmList() { name = "@Landline", value = landline });
                    _lstparm.Add(new ParmList() { name = "@Address", value = address });
                    _lstparm.Add(new ParmList() { name = "@CountryID", value = country });
                    _lstparm.Add(new ParmList() { name = "@StateID", value = state });
                    _lstparm.Add(new ParmList() { name = "@CityID", value = city });
                    _lstparm.Add(new ParmList() { name = "@CityName", value = cityname });
                    _lstparm.Add(new ParmList() { name = "@ZIP", value = zip });
                    _lstparm.Add(new ParmList() { name = "@MemberType", value = membertype });
                    _lstparm.Add(new ParmList() { name = "@MemberTypeID", value = Session["membertypeid"] });
                    _lstparm.Add(new ParmList() { name = "@ParentMsrNo", value = Session["ParentMsrNo"] });
                    _lstparm.Add(new ParmList() { name = "@memberImage", value = strimage });
                    _lstparm.Add(new ParmList() { name = "@aadhar", value = "" });
                    _lstparm.Add(new ParmList() { name = "@pan", value = "" });
                    _lstparm.Add(new ParmList() { name = "@companypan", value = "" });
                    _lstparm.Add(new ParmList() { name = "@gstno", value = "" });
                    _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                    dt = cls.select_data_dtNew("ProcMLM_AddNewMemberMaster ", _lstparm);
                    cls.update_data("insert into regpaymentdetails(Payment,Name,RequestDate,Amount,txnID,membertype,mobile)values(1,'" + Name + "','" + dd + "','" + Convert.ToDecimal(Session["AmountPayable"]) + "','" + tx + "','" + type + "','" + mobile + "')");
                    if (dt.Rows.Count > 0)
                    {
                        List<ParmList> _lstparmss = new List<ParmList>();
                        _lstparmss.Add(new ParmList() { name = "@ID", value = 2 });
                        _lstparmss.Add(new ParmList() { name = "@Action", value = "GetAll" });
                        DataTable dtCompany = cls.select_data_dtNew("Proc_ManageCompany ", _lstparmss);
                        if (dtCompany.Rows.Count > 0)
                        {
                            string CompanyName = dtCompany.Rows[0]["CompanyName"].ToString();
                            string WebSiteURL = dtCompany.Rows[0]["Website"].ToString() + "/userlogin";
                            RegisterMail.SendRegistrationMail(MemberID + " - " + firstname + " " + lastname, CompanyName, email, mobile, password, transactionpin);
                            string[] valueArray = new string[6];
                            valueArray[0] = firstname + " " + lastname;
                            valueArray[1] = CompanyName;
                            valueArray[2] = MemberID;
                            valueArray[3] = password;
                            valueArray[4] = transactionpin;
                            valueArray[5] = WebSiteURL;
                            SMS.SendWithVar(mobile, 26, valueArray, 1);
                            clear();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Success|Your Registration Successfully');location.replace('Signup.aspx');", true);
                        }
                        clear();

                    }
                }
                catch (Exception ex)
                {
                    cls.select_data_dt("insert into mtest values('" + ex.ToString() + "')");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !')location.replace('Signup.aspx');", true);
                }
                #endregion
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='Signup.aspx';", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Again Payment process');window.location ='Signup.aspx';", true);
        }

    }



    public double TotupAmount(double amount, double rate)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0;
        if (amount > 0)
        {
            surcharge_rate = rate;
            if (surcharge_rate > 0)
            {
                surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
            }
            NetAmount = surcharge_amt;
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }
    public double netbankingrate(double amount, double rate)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0;
        if (amount > 0)
        {
            surcharge_rate = rate;
            if (surcharge_rate > 0)
            {
                surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
            }
            NetAmount = surcharge_amt;
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }
    private DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
    private void setControl()
    {
        Session["PayOrderId"] = null;
        Session["tx"] = null;
        Session["txtAmount"] = null;
        Session["Returnurl"] = null;
    }

    public class RegisterMail
    {
        public static void SendRegistrationMail(string Name, string companyname, string Email, string MobileNo, string Password, string TransactionPassword)
        {
            try
            {
                string[] valueArray = new string[7];
                valueArray[0] = Name;
                valueArray[1] = companyname;
                valueArray[2] = Email;
                valueArray[3] = MobileNo;
                valueArray[4] = Password;
                valueArray[5] = TransactionPassword;
                valueArray[6] = Name;
                FlexiMail objSendMail = new FlexiMail();
                objSendMail.To = Email;
                objSendMail.CC = "";
                objSendMail.BCC = "support@fritware.com";
                objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                objSendMail.FromName = "fritware.com";
                objSendMail.MailBodyManualSupply = false;
                objSendMail.EmailTemplateFileName = "welcome.htm";
                objSendMail.Subject = "Registration";
                objSendMail.ValueArray = valueArray;
                objSendMail.Send();
            }
            catch (Exception ex)
            {

            }
        }

        public static void Customer(string Name, string Email, string MobileNo, string Password, string TransactionPassword, string memberid)
        {
            try
            {
                string[] valueArray = new string[6];
                valueArray[0] = Name;
                valueArray[1] = Email;
                valueArray[2] = MobileNo;
                valueArray[3] = Password;
                valueArray[4] = TransactionPassword;
                valueArray[5] = memberid;
                FlexiMail objSendMail = new FlexiMail();
                objSendMail.To = Email;
                objSendMail.CC = "";
                objSendMail.BCC = "";
                objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                objSendMail.FromName = "support@fritware.com";
                objSendMail.MailBodyManualSupply = false;
                objSendMail.EmailTemplateFileName = "welcome.htm";
                objSendMail.Subject = "Registration";
                objSendMail.ValueArray = valueArray;
                objSendMail.Send();
            }
            catch (Exception ex)
            { }
        }

        public static void FundRequest(string Name, string Email, string FromMemberID, string Amount)
        {
            try
            {
                string[] valueArray = new string[3];
                valueArray[0] = Name;
                valueArray[1] = FromMemberID;
                valueArray[2] = Amount;
                FlexiMail objSendMail = new FlexiMail();
                objSendMail.To = Email;
                objSendMail.CC = "";
                objSendMail.BCC = "";
                objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                objSendMail.FromName = "support@fritware.com";
                objSendMail.MailBodyManualSupply = false;
                objSendMail.EmailTemplateFileName = "../../../EmailTemplates/Fund.htm";
                objSendMail.Subject = "Fund Request";
                objSendMail.ValueArray = valueArray;
                objSendMail.Send();
            }
            catch (Exception ex)
            { }
        }
    }

    private void clear()
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
        Session["membertypeid"] = null;
        Session["packageid"] = null;
        Session["ParentMsrNo"] = null;

    }


}