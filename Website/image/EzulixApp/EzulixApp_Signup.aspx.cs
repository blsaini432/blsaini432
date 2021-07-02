using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Net;
using System.IO;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Xml;
using System.Globalization;
using BLL.MLM;

public partial class EzulixApp_Signup : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    clsMLM_RWalletTransaction objRWalletTransaction = new clsMLM_RWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    private static int limitamount = 5000;
    public string ConvertDataTabletoString(DataTable dt)
    {

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);

    }
    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }
    protected string ReplaceCode(string str)
    {
        return str.Replace("'", "").Replace("-", "").Replace(";", "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["operationname"] != null)
        {
            string OperationName = Request.Form["operationname"].ToString();
            if (OperationName == "signup")
            {
                #region signup
                if (Request.Form["mobile"] != null && Request.Form["email"] != null && Request.Form["refid"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    string refealcode = ReplaceCode(Request.Form["refealcode"].ToString().Trim());
                    string membertype = ReplaceCode(Request.Form["wanttobe"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    string email = ReplaceCode(Request.Form["email"].ToString().Trim());
                    string refID = ReplaceCode(Request.Form["refid"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    Random random = new Random();
                    if (refealcode != "")
                    {
                        if (membertype == "Retailer")
                        {
                            int membertypeid = 5;
                            Session["membertypeids"] = membertypeid;
                        }
                        else if (membertype == "Distributor")
                        {
                            int membertypeid = 4;
                            Session["membertypeids"] = membertypeid;
                        }
                        else if (membertype == "Master Distributor")
                        {
                            int membertypeid = 3;
                            Session["membertypeids"] = membertypeid;
                        }

                        DataTable dt = new DataTable();
                        dt = cls.select_data_dt("select * from tblmlm_membermaster where MemberID='" + refealcode + "'");
                        if (dt.Rows.Count > 0)
                        {
                            int memberid = Convert.ToInt32(Session["membertypeids"]);
                            int msrno = Convert.ToInt32(dt.Rows[0]["Msrno"]);
                            int MembertypeID = Convert.ToInt32(dt.Rows[0]["MembertypeID"]);
                            if (MembertypeID < memberid)
                            {
                                if (cls.select_data_scalar_int("Select count(*) from tblmlm_membermaster where mobile='" + mobile + "' or Email='" + email + "'") == 0)
                                {
                                    string[] namezz = email.ToString().Split('@');
                                    string FName = namezz[0];
                                    int Parentmsrno = msrno;
                                    if (refID == "") { refID = "_"; }
                                    dt = cls.select_data_dt("Select 1 as ResponseCode,'" + Parentmsrno + "' as Parentmsrno");
                                    string output = ConvertDataTabletoString(dt);
                                    Response.Write("{ " + OperationName + ":" + output + "}");
                                }
                                else
                                {

                                    ReturnError("Member already exists in system !!", "signup");
                                }
                            }
                            else
                            {
                                ReturnError("Please Enter Valide referral Code !!", "signup");
                            }
                        }
                        else
                        {

                            ReturnError(" referral Code not valid! !!", "signup");
                        }

                    }
                    else
                    {
                        if (cls.select_data_scalar_int("Select count(*) from tblmlm_membermaster where mobile='" + mobile + "' or Email='" + email + "'") == 0)
                        {
                            string[] namezz = email.ToString().Split('@');
                            string FName = namezz[0];
                            int Parentmsrno = 1;
                            if (refID == "") { refID = "_"; }
                            DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'" + mobile + "' as mobile,'" + email + "' as email,'" + refID + "' as refID,'" + Parentmsrno + "' as Parentmsrno");
                            string output = ConvertDataTabletoString(dt);
                            Response.Write("{ " + OperationName + ":" + output + "}");
                        }
                        else
                        {
                            ReturnError("Member already exists in system !!", "signup");
                        }
                    }
                }
                else
                {

                }
                #endregion
            }
            if (OperationName == "signupcallback")
            {
                #region  signcallback
                if (Request.Form["mobile"] != null && Request.Form["email"] != null && Request.Form["refid"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    string refealcode = ReplaceCode(Request.Form["refealcode"].ToString().Trim());
                    string membertype = ReplaceCode(Request.Form["wanttobe"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    string email = ReplaceCode(Request.Form["email"].ToString().Trim());
                    string refID = ReplaceCode(Request.Form["refid"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    #region [Insert]
                    string membertypeid;
                    int packageid;
                    string tx = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
                    Random random = new Random();
                    int SixDigit = random.Next(100000, 999999);
                    string MemberID = "";
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
                        string firstname = ReplaceCode(Request.Form["Firstname"].ToString().Trim());
                        string lastname = ReplaceCode(Request.Form["lastname"].ToString().Trim());
                        string ParentMsrNo = ReplaceCode(Request.Form["ParentMsrNo"].ToString().Trim());
                        string Name = firstname + "" + lastname;
                        string address = ReplaceCode(Request.Form["address"].ToString().Trim());
                        string country = "1";
                        string state = ReplaceCode(Request.Form["state"].ToString().Trim());
                        string city = ReplaceCode(Request.Form["cityid"].ToString().Trim());
                        string cityname = ReplaceCode(Request.Form["cityname"].ToString().Trim());
                        string type = ReplaceCode(Request.Form["wanttobe"].ToString().Trim());
                        string zip = ReplaceCode(Request.Form["zipcode"].ToString().Trim());
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
                        _lstparm.Add(new ParmList() { name = "@Address", value = address });
                        _lstparm.Add(new ParmList() { name = "@CountryID", value = country });
                        _lstparm.Add(new ParmList() { name = "@StateID", value = state });
                        _lstparm.Add(new ParmList() { name = "@CityID", value = city });
                        _lstparm.Add(new ParmList() { name = "@CityName", value = cityname });
                        _lstparm.Add(new ParmList() { name = "@ZIP", value = zip });
                        _lstparm.Add(new ParmList() { name = "@MemberType", value = membertype });
                        _lstparm.Add(new ParmList() { name = "@MemberTypeID", value = Session["membertypeid"] });
                        _lstparm.Add(new ParmList() { name = "@ParentMsrNo", value = ParentMsrNo });
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
                                ReturnError("Member Registration Successfully !!", "signup");
                            }
                         }
                    }
                    catch (Exception ex)
                    {
                        cls.select_data_dt("insert into mtest values('" + ex.ToString() + "')");
                        ReturnError("some error found !!", "signup");
                    }
                    #endregion
                    #endregion
                }
                else
                {
                    ReturnError("Valide data !!", "signup");
                }
            }
            
        }
    }
    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }
}