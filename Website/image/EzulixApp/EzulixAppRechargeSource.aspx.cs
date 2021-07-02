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
public partial class EzulixApp_AppRechargeSource : System.Web.UI.Page
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

            #region Login
            if (OperationName == "login")
            {
                #region login
                if (Request.Form["mobile"] != null && Request.Form["password"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    string mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    string password = ReplaceCode(Request.Form["password"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    clsMLM_MemberMasterLoginDetail objMemberMasterLoginDetail = new clsMLM_MemberMasterLoginDetail();
                    cls_Universal objUniversal = new cls_Universal();
                    DataTable dtUniversal = objUniversal.UniversalLogin("AppLogin", mobile, password);
                    if (dtUniversal.Rows.Count > 0)
                    {
                        string tt = dtUniversal.Rows[0]["TransactionPassword"].ToString();
                        int id = objMemberMasterLoginDetail.AddEditMemberMasterLoginDetail(0, loginip, Convert.ToInt32(dtUniversal.Rows[0]["MsrNo"]));
                        objUniversal.UpdateLastLogin("UpdateMemberLastLogin_App", Convert.ToInt32(dtUniversal.Rows[0]["MsrNo"]), loginip);
                        objUniversal.UniversalLogin("UpdateDevice", deviceid, dtUniversal.Rows[0]["Memberid"].ToString());
                        dtUniversal = cls.select_data_dt("Exec Proc_UniversalLogin 'AppLogin','" + mobile + "','" + password + "'");
                        string output = ConvertDataTabletoString(dtUniversal);
                        Response.Write("{ " + Request.Form["operationname"].ToString() + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("Invalid Login Details !!", "login");
                    }
                }
                else
                {
                    ReturnError("Invalid Login Details", "login");
                }
                #endregion
            }
            if (OperationName == "mpin")
            {
                #region login
                if (Request.Form["mobile"] != null && Request.Form["password"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["tranpwd"] != null)
                {
                    string mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    string password = ReplaceCode(Request.Form["password"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string mpin = ReplaceCode(Request.Form["tranpwd"].ToString().Trim());
                    //string tranpin = ReplaceCode(Request.Form["transpin"].ToString().Trim());
                    clsMLM_MemberMasterLoginDetail objMemberMasterLoginDetail = new clsMLM_MemberMasterLoginDetail();
                    cls_Universal objUniversal = new cls_Universal();
                    DataTable dtUniversal = objUniversal.UniversalLogin("AppLogin", mobile, password);
                    string tt = dtUniversal.Rows[0]["TransactionPassword"].ToString();
                    if (tt == mpin)
                    {
                        if (dtUniversal.Rows.Count > 0)
                        {
                            int id = objMemberMasterLoginDetail.AddEditMemberMasterLoginDetail(0, loginip, Convert.ToInt32(dtUniversal.Rows[0]["MsrNo"]));
                            objUniversal.UpdateLastLogin("UpdateMemberLastLogin_App", Convert.ToInt32(dtUniversal.Rows[0]["MsrNo"]), loginip);
                            objUniversal.UniversalLogin("UpdateDevice", deviceid, dtUniversal.Rows[0]["Memberid"].ToString());
                            dtUniversal = cls.select_data_dt("Exec Proc_UniversalLogin 'AppLogin','" + mobile + "','" + password + "'");
                            string output = ConvertDataTabletoString(dtUniversal);
                            Response.Write("{ " + Request.Form["operationname"].ToString() + ":" + output + "}");
                        }
                        else
                        {
                            ReturnError("Invalid Mpin !!", "mpin");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Mpin !!", "mpin");
                    }
                }
                else
                {
                    ReturnError("Invalid Mpin", "mpin");
                }
                #endregion
            }

            if (OperationName == "LocationData")
            {
                #region LocationData
                if (Request.Form["MsrNo"] != null && Request.Form["MemberId"] != null && Request.Form["LoginIP"] != null && Request.Form["Lat"] != null && Request.Form["Long"] != null)
                {
                    string MsrNo = ReplaceCode(Request.Form["MsrNo"].ToString().Trim());
                    string MemberId = ReplaceCode(Request.Form["MemberId"].ToString().Trim());
                    string LoginIP = ReplaceCode(Request.Form["LoginIP"].ToString().Trim());
                    string Lat = ReplaceCode(Request.Form["Lat"].ToString().Trim());
                    string Long = ReplaceCode(Request.Form["Long"].ToString().Trim());
                    List<ParmList> _lstparmss = new List<ParmList>();
                    _lstparmss.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(MsrNo) });
                    _lstparmss.Add(new ParmList() { name = "@Latitude", value = Lat });
                    _lstparmss.Add(new ParmList() { name = "@Logitude", value = Long });
                    _lstparmss.Add(new ParmList() { name = "@LoginIp", value = LoginIP });
                    _lstparmss.Add(new ParmList() { name = "@memberid", value = MemberId });
                    _lstparmss.Add(new ParmList() { name = "@action", value = "I" });
                    cls.select_data_dtNew("MobileApp_Locationdata ", _lstparmss);
                    string output = "Success";
                    Response.Write("{ " + Request.Form["operationname"].ToString() + ":" + output + "}");

                }
                else
                {
                    ReturnError("Invalid Request", "LocationData");
                }
                #endregion
            }


            #endregion

            #region ForgotDetails
            if (OperationName == "changempin")
            {
                #region forgotmpin
                if (Request.Form["mobile"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["oldpin"] != null && Request.Form["newpin"] != null)
                {
                    string Loginid = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@LoginID", value = Loginid });
                    _lstparm.Add(new ParmList() { name = "@Password", value = "" });
                    _lstparm.Add(new ParmList() { name = "@Action", value = "forgetLogin" });
                    cls_connection cls = new cls_connection();
                    DataTable dt = cls.select_data_dtNew("Proc_UniversalLogin ", _lstparm);
                    if (dt.Rows.Count > 0)
                    {
                        string mobile = "91" + "9166396947";
                        string token = genratestring();
                        int id = Convert.ToInt32(dt.Rows[0]["msrno"].ToString());
                        cls.update_data("update tblmlm_membermaster set passwordtoken='" + token + "' where msrno=" + id + "");
                        string Token = token;
                        string adminurl = ConfigurationManager.AppSettings["adminurl"];
                        string aa = adminurl + "pwdrset.aspx?utken=" + Token;
                        string bb = ShrinkURL(aa);
                        string sms = "Dear Member, Welcome to Payonclik, Your Password reset link is: " + bb + " Thanks.Payonclik";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        WebClient client = new WebClient();
                        string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=265172AmebjTdKa5c778715&route=4&sender=BSDGTL&DLT_TE_ID=1207161950018423988&mobiles=" + mobile + "&message=" + sms + "";
                        Stream data = client.OpenRead(baseurl);
                        StreamReader reader = new StreamReader(data);
                        string Email = dt.Rows[0]["Email"].ToString();
                        SendForgetMail(Token, Email);
                        DataTable dnt = cls.select_data_dt("Select 1 as ResponseCode,'Password Reset Link has been sent to your registerd mobile or email.' as ResponseStatus");
                        string output = ConvertDataTabletoString(dnt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("This User Is not registered with us", "changempin");

                    }
                }
                #endregion
            }
            if (OperationName == "forgotpassword")
            {
                #region forgotpassword
                if (Request.Form["mobile"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    string Loginid = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@LoginID", value = Loginid });
                    _lstparm.Add(new ParmList() { name = "@Password", value = "" });
                    _lstparm.Add(new ParmList() { name = "@Action", value = "forgetLogin" });
                    cls_connection cls = new cls_connection();
                    DataTable dt = cls.select_data_dtNew("Proc_UniversalLogin ", _lstparm);
                    if (dt.Rows.Count > 0)
                    {

                        string mobile = "91" + "9166396947";
                        string token = genratestring();
                        int id = Convert.ToInt32(dt.Rows[0]["msrno"].ToString());
                        cls.update_data("update tblmlm_membermaster set passwordtoken='" + token + "' where msrno=" + id + "");
                        string Token = token;
                        string adminurl = ConfigurationManager.AppSettings["adminurl"];
                        string aa = adminurl + "pwdrset.aspx?utken=" + Token;
                        string bb = ShrinkURL(aa);
                        string sms = "Dear Member, Welcome to Payonclik, Your Password reset link is: " + bb + " Thanks.Payonclik";
                        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        WebClient client = new WebClient();
                        string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=265172AmebjTdKa5c778715&route=4&sender=BSDGTL&DLT_TE_ID=1207161950018423988&mobiless=" + mobile + "&message=" + sms + "";
                        Stream data = client.OpenRead(baseurl);
                        StreamReader reader = new StreamReader(data);
                        string Email = dt.Rows[0]["Email"].ToString();
                        SendForgetMail(Token, Email);
                        DataTable dnt = cls.select_data_dt("Select 1 as ResponseCode,'Password Reset Link has been sent to your registerd mobile or email.' as ResponseStatus");
                        string output = ConvertDataTabletoString(dnt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("This User Is not registered with us", "changempin");

                    }


                }
                #endregion
            }

            #endregion

            #region SignUP
            if (OperationName == "signup")
            {
                #region signup
                if (Request.Form["mobile"] != null && Request.Form["email"] != null && Request.Form["refid"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberimage"] != null)
                {
                    string mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    string email = ReplaceCode(Request.Form["email"].ToString().Trim());
                    string refID = ReplaceCode(Request.Form["refid"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string memberImage = ReplaceCode(Request.Form["memberimage"].ToString().Trim());
                    Random random = new Random();
                    int SixDigit = random.Next(100000, 999999);
                    if (cls.select_data_scalar_int("Select count(*) from tblmlm_membermaster where mobile='" + mobile + "' or Email='" + email + "'") == 0)
                    {
                        string[] namezz = email.ToString().Split('@');
                        string FName = namezz[0];
                        string[] valueArray = new string[2];
                        valueArray[0] = FName;
                        valueArray[1] = SixDigit.ToString();
                        Session["OTP"] = SixDigit.ToString();
                        SMS.SendWithVar(mobile, 21, valueArray, 1);
                        if (refID == "") { refID = "_"; }
                        if (memberImage == "") { memberImage = "_"; }
                        DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'" + mobile + "' as mobile,'" + email + "' as email,'" + refID + "' as refID,'" + memberImage + "' as memberImage,'" + SixDigit + "' as OTP");
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
                    ReturnError("Invalid request format !!", "signup");
                }
                #endregion
            }

            #endregion

            #region Recharge
            if (OperationName == "optrs")
            {
                #region optrs
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    #region ListOperators
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,OperatorCode,OperatorName,ServiceTypeID,opcname from tblRecharge_Operator where ServiceTypeID=case when 0>0 then 0 else ServiceTypeID end and  IsActive='true' and  IsDelete='false' order by OperatorName asc");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{\"" + OperationName + "\":" + output + ",");
                    #endregion

                    #region Listcircle
                    dt = cls.select_data_dt("Select 1 as ResponseCode,CircleID,Circlecode,CircleName from tblRecharge_Circle where IsDelete='false' order by CircleID desc");
                    output = ConvertDataTabletoString(dt);
                    output = ConvertDataTabletoString(dt);
                    Response.Write("\"circle\":" + output + ",");
                    #endregion

                    #region ListFbank
                    dt = cls.select_data_dt("Exec App_getDetails 'fbank','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    output = ConvertDataTabletoString(dt);
                    Response.Write("\"fbank\":" + output + ",");
                    #endregion

                    #region toBank
                    dt = cls.select_data_dt("Exec App_getDetails 'TBANK','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    output = ConvertDataTabletoString(dt);
                    Response.Write("\"tbank\":" + output + ",");
                    #endregion

                    #region News
                    dt = cls.select_data_dt("select NewsName,(select  (tblnews.NewsDesc)) as NewsDesc,CONVERT(varchar,NewsDate,106) as NewsDate from tblnews where IsDelete=0 and IsActive=1 order by NewsDate desc");
                    output = ConvertDataTabletoString(dt);
                    Response.Write("\"news\":" + Regex.Replace(output, "<.*?>", String.Empty) + ",");
                    #endregion

                    #region MemberDetails
                    dt = cls.select_data_dt("SELECT 1 as ResponseCode,Mobile,Memberid,Email,firstname+'_'+lastname as MemberName,CONVERT(VARCHAR(10), lastlogindate, 101) + ' ' + LTRIM(RIGHT(CONVERT(CHAR(20), lastlogindate, 22), 11)) as LastLogindate,membertypeid from dbo.tblMLM_MemberMaster where memberdesc='" + mcode + "'");
                    output = ConvertDataTabletoString(dt);
                    Response.Write("\"member\":" + output + "}");
                    #endregion
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            if (OperationName == "last")
            {
                #region last
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'last','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "recharge")
            {
                #region recharge
                if (Request.Form["msrno"] != null && Request.Form["amount"] != null && Request.Form["operatorcode"] != null && Request.Form["number"] != null && Request.Form["circlecode"] != null && Request.Form["account"] != null && Request.Form["cycle"] != null && Request.Form["unit"] != null)
                {
                    OperationName = "Recharge";
                    string mmsrno = ReplaceCode(Request.Form["msrno"].ToString().Trim());
                    string amount = ReplaceCode(Request.Form["amount"].ToString().Trim());
                    string operatorcode = ReplaceCode(Request.Form["operatorcode"].ToString().Trim());
                    string number = Request.Form["number"].ToString().Trim();
                    string circlecode = ReplaceCode(Request.Form["circlecode"].ToString().Trim());
                    string account = ReplaceCode(Request.Form["account"].ToString().Trim());
                    string cycle = ReplaceCode(Request.Form["cycle"].ToString().Trim());
                    string unit = ReplaceCode(Request.Form["unit"].ToString().Trim());
                    if (circlecode == "") { circlecode = "19"; }

                    if (amount == "" || amount == null)
                    {
                        ReturnError("Invalid Amount", OperationName);
                        return;
                    }
                    else if (Convert.ToDecimal(amount) < 4)
                    {
                        ReturnError("Invalid Amount", OperationName);
                        return;
                    }
                    else if (operatorcode == "" || operatorcode == null)
                    {
                        ReturnError("Invalid Operator", OperationName);
                        return;
                    }
                    DataTable chkotcode = new DataTable();
                    chkotcode = cls.select_data_dt("select OperatorID from tblrecharge_operator where OperatorCode='" + operatorcode + "'");
                    int opertcode = Convert.ToInt32(chkotcode.Rows[0]["OperatorID"].ToString());
                    DataTable dd = new DataTable();
                    dd = cls.select_data_dt("select * from tblRecharge_History where CONVERT(date, adddate)=CONVERT(date, GETDATE()) and MobileNo='" + number + "' and OperatorID='" + opertcode + "' and RechargeAmount='" + amount + "' and Status in('Success','Pending')");
                    if (dd.Rows.Count > 0)
                    {
                        DateTime ddat = Convert.ToDateTime(dd.Rows[0]["AddDate"].ToString());
                        if (ddat.ToString("dd/MM/YYYY") == DateTime.Now.ToString("dd/MM/YYYY"))
                        {
                            TimeSpan duration = DateTime.Now.Subtract(ddat);
                            double totalMinutes = duration.TotalMinutes;
                            if (totalMinutes > Convert.ToDouble(15))
                            {
                                DataTable dtOperator = cls.select_data_dt("Exec MobileApp_ValidateRequest_New '" + operatorcode + "','" + circlecode + "'," + mmsrno + "");
                                if (Convert.ToInt32(dtOperator.Rows[0][0].ToString()) > 0 && Convert.ToInt32(dtOperator.Rows[0][1].ToString()) > 0 && Convert.ToInt32(dtOperator.Rows[0][2].ToString()) > 0)
                                {
                                    string Trantype = ""; string mycirclecode = ""; string msrno = ""; int Operatorid; int Circleid; string Memberid; int PackageID;
                                    Operatorid = Convert.ToInt32(dtOperator.Rows[0]["operatorid"]);
                                    Circleid = Convert.ToInt32(dtOperator.Rows[0]["circleid"]);
                                    msrno = dtOperator.Rows[0]["msrno"].ToString();
                                    Memberid = dtOperator.Rows[0]["memberid"].ToString();
                                    PackageID = Convert.ToInt32(dtOperator.Rows[0]["Packageid"]);
                                    DataTable dst = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='recharge', @msrno=" + msrno + "");
                                    if (dst.Rows.Count > 0)
                                    {
                                        if (Convert.ToBoolean(dst.Rows[0]["isrecharge"]) == true)
                                        {
                                            cls_myMember clsm = new cls_myMember();
                                            if (clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), Convert.ToInt32(msrno)) == 1)
                                            {
                                                string TransID = clsm.Cyrus_GetTransactionID_New();
                                                clsRecharge_History objHistory = new clsRecharge_History();
                                                clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
                                                int i = objHistory.AddEditHistory(0, Convert.ToInt32(msrno), number, account, Convert.ToDecimal(amount), Operatorid, Circleid, TransID, cycle, unit, "Queued");
                                                if (i > 0)
                                                {
                                                    objEWalletTransaction.EWalletTransaction(Memberid, -Convert.ToDecimal(amount), "Dr", "Recharge to " + Convert.ToString(number) + " TxnID : " + TransID + "");
                                                    RechargeDone(i, Convert.ToInt32(msrno), Memberid, PackageID, "", Operatorid, Circleid, amount, number, Trantype, TransID, "", "", "");
                                                }
                                            }
                                            else
                                                ReturnError("Insufficient Wallet Balance", OperationName);
                                        }
                                        else
                                        {
                                            ReturnError("Recharge Service Is not active", OperationName);
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Recharge Service Is not active", OperationName);
                                    }
                                }
                                else
                                {
                                    ReturnError("Invalid Operator", OperationName);
                                    return;
                                }

                            }
                            else
                            {
                                ReturnError("Same Recharge Can not be done during 15 minutes from last recharge ", OperationName);
                            }
                        }
                    }

                    else
                    {

                        DataTable dtOperator = cls.select_data_dt("Exec MobileApp_ValidateRequest_New '" + operatorcode + "','" + circlecode + "'," + mmsrno + "");
                        if (Convert.ToInt32(dtOperator.Rows[0][0].ToString()) > 0 && Convert.ToInt32(dtOperator.Rows[0][1].ToString()) > 0 && Convert.ToInt32(dtOperator.Rows[0][2].ToString()) > 0)
                        {
                            string Trantype = ""; string mycirclecode = ""; string msrno = ""; int Operatorid; int Circleid; string Memberid; int PackageID;
                            Operatorid = Convert.ToInt32(dtOperator.Rows[0]["operatorid"]);
                            Circleid = Convert.ToInt32(dtOperator.Rows[0]["circleid"]);
                            msrno = dtOperator.Rows[0]["msrno"].ToString();
                            Memberid = dtOperator.Rows[0]["memberid"].ToString();
                            PackageID = Convert.ToInt32(dtOperator.Rows[0]["Packageid"]);
                            DataTable dst = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='recharge', @msrno=" + msrno + "");
                            if (dst.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dst.Rows[0]["isrecharge"]) == true)
                                {
                                    cls_myMember clsm = new cls_myMember();
                                    if (clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), Convert.ToInt32(msrno)) == 1)
                                    {
                                        string TransID = clsm.Cyrus_GetTransactionID_New();
                                        clsRecharge_History objHistory = new clsRecharge_History();
                                        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
                                        int i = objHistory.AddEditHistory(0, Convert.ToInt32(msrno), number, account, Convert.ToDecimal(amount), Operatorid, Circleid, TransID, cycle, unit, "Queued");
                                        if (i > 0)
                                        {
                                            objEWalletTransaction.EWalletTransaction(Memberid, -Convert.ToDecimal(amount), "Dr", "Recharge to " + Convert.ToString(number) + " TxnID : " + TransID + "");
                                            RechargeDone(i, Convert.ToInt32(msrno), Memberid, PackageID, "", Operatorid, Circleid, amount, number, Trantype, TransID, "", "", "");
                                        }
                                    }
                                    else
                                        ReturnError("Insufficient Wallet Balance", OperationName);
                                }
                                else
                                {
                                    ReturnError("Recharge Service Is not active", OperationName);
                                }
                            }
                            else
                            {
                                ReturnError("Recharge Service Is not active", OperationName);
                            }
                        }
                        else
                        {
                            ReturnError("Invalid Operator", OperationName);
                            return;
                        }
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            #endregion

            #region Instant PayDMR
            if (OperationName == "remitdtl")
            {
                #region Remitter_Details
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["mobile"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'remitdtl','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    string Result = string.Empty;
                                    string mobile = string.Empty;
                                    mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                                    EzulixDmr eDmr = new EzulixDmr();
                                    Result = eDmr.Remitter_Details(mobile);
                                    if (Result != string.Empty)
                                    {
                                        DataSet ds = Deserialize(Result);
                                        if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                                        {
                                            Response.Write("{ " + OperationName + ":" + Result + "}");
                                        }
                                        else
                                        {
                                            Response.Write("{ " + OperationName + ":" + Result + "}");
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Invalid Request Format", "Unknown");
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Login to another device Contact your admin", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "downbank")
            {
                #region DownLineBank
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'remitdtl','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    EzulixDmr eDmr = new EzulixDmr();
                                    string Result = string.Empty;
                                    Result = eDmr.Get_Bank_Details();
                                    Response.Write("{ " + OperationName + ":" + Result + "}");
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Login to another device Contact your admin", "Unknown");
                        }

                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "remitregval")
            {
                #region Remitter_Validate
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["remitterid"] != null && Request.Form["mobile"] != null && Request.Form["otp"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'remitreg','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    string Result = string.Empty;
                                    string remitterid = string.Empty;
                                    string mobile = string.Empty;
                                    string otp = string.Empty;
                                    remitterid = ReplaceCode(Request.Form["remitterid"].ToString().Trim());
                                    mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                                    otp = ReplaceCode(Request.Form["otp"].ToString().Trim());
                                    EzulixDmr eDmr = new EzulixDmr();
                                    Result = eDmr.Remitter_Validate(remitterid, mobile, otp);
                                    if (Result != string.Empty)
                                    {
                                        Response.Write("{ " + Request.Form["operationname"].ToString() + ":" + Result + "}");

                                    }
                                    else
                                    {
                                        ReturnError("Invalid Request Format", "Unknown");
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Login to another device Contact your admin", "Unknown");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", "Unknown");
                    }
                }
                #endregion
            }
            if (OperationName == "remitreg")
            {
                #region Remitter_Registration
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["mobile"] != null && Request.Form["name"] != null && Request.Form["surname"] != null && Request.Form["pin"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'remitreg','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    string Result = string.Empty;
                                    string mobile = string.Empty;
                                    string name = string.Empty;
                                    string pin = string.Empty;
                                    string surnmae = string.Empty;
                                    mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                                    name = ReplaceCode(Request.Form["name"].ToString().Trim());
                                    pin = ReplaceCode(Request.Form["pin"].ToString().Trim());
                                    surnmae = ReplaceCode(Request.Form["surname"].ToString().Trim());
                                    EzulixDmr eDmr = new EzulixDmr();
                                    Result = eDmr.Remitter_Registration(mobile, name, surnmae, pin);
                                    if (Result != string.Empty)
                                    {
                                        Response.Write("{ " + Request.Form["operationname"].ToString() + ":" + Result + "}");

                                    }
                                    else
                                    {
                                        ReturnError("Invalid Request Format", "Unknown");
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Login to another device Contact your admin", "Unknown");
                        }

                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "acval")
            {
                #region Beneficiary_Account_Verification
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["mobile"] != null && Request.Form["beniifsc"] != null && Request.Form["beniac"] != null && Request.Form["remitid"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'acval','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    string Result = string.Empty;
                                    string beniifsc = string.Empty;
                                    string beniac = string.Empty;
                                    string mobile = string.Empty;
                                    string remitid = string.Empty;
                                    beniifsc = ReplaceCode(Request.Form["beniifsc"].ToString().Trim());
                                    beniac = ReplaceCode(Request.Form["beniac"].ToString().Trim());
                                    mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                                    remitid = ReplaceCode(Request.Form["remitid"].ToString().Trim());
                                    int msrno = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + dt.Rows[0]["MemberID"].ToString() + "'");
                                    cls_myMember Clsm = new cls_myMember();
                                    int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(4), msrno);
                                    if (res == 1)
                                    {
                                        string ETranId = string.Empty;
                                        ETranId = Clsm.Cyrus_GetTransactionID_New();
                                        int tra = Clsm.Wallet_MakeTransaction(Convert.ToString(dt.Rows[0]["MemberID"].ToString()), Convert.ToDecimal("-" + 4), "Dr", "DMR AC Verify Txn:- '" + ETranId + "'");
                                        if (tra > 0)
                                        {
                                            EzulixDmr eDmr = new EzulixDmr();
                                            Result = eDmr.Beneficiary_Account_Verification(mobile, beniac, beniifsc, remitid);
                                            if (Result != string.Empty)
                                            {
                                                Response.Write("{ " + OperationName + ":" + Result + "}");
                                            }
                                            else
                                            {
                                                ReturnError("Invalid Request Format", "Unknown");
                                            }
                                        }
                                        else
                                        {
                                            ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Login to another device Contact your admin", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "benireg")
            {
                #region Beneficiary_Registration
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["mobile"] != null && Request.Form["beniname"] != null && Request.Form["benimob"] != null && Request.Form["beniifsc"] != null && Request.Form["beniac"] != null && Request.Form["remitid"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'benireg','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    string mobile = string.Empty;
                                    string beniname = string.Empty;
                                    string benimob = string.Empty;
                                    string beniifsc = string.Empty;
                                    string beniac = string.Empty;
                                    string Result = string.Empty;
                                    string remitid = string.Empty;
                                    mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                                    beniname = ReplaceCode(Request.Form["beniname"].ToString().Trim());
                                    benimob = ReplaceCode(Request.Form["benimob"].ToString().Trim());
                                    beniifsc = ReplaceCode(Request.Form["beniifsc"].ToString().Trim());
                                    beniac = ReplaceCode(Request.Form["beniac"].ToString().Trim());
                                    remitid = ReplaceCode(Request.Form["remitid"].ToString().Trim());
                                    cls_connection Cls = new cls_connection();
                                    DataTable dtbenichk = Cls.select_data_dt(@"select * from tbl_Instant_Beni_Reg where beni_mobile='" + benimob + "' and beni_ac='" + beniac + "' and beni_ifsc='" + beniifsc + "' and remitId='" + remitid + "' and isactive=1");
                                    if (dtbenichk.Rows.Count > 0)
                                    {
                                        Response.Write("{ " + OperationName + ":" + Result + "}");
                                    }
                                    else
                                    {
                                        EzulixDmr eDmr = new EzulixDmr();
                                        Result = eDmr.Beneficiary_Registration(remitid, beniname, benimob, beniifsc, beniac);
                                        if (Result != string.Empty)
                                        {
                                            Response.Write("{ " + OperationName + ":" + Result + "}");
                                        }
                                        else
                                        {
                                            ReturnError("Invalid Request Format", "Unknown");
                                        }
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Loggined to another device Contact your admin", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "otpsenddmr")
            {
                #region OTPsendDMR
                if (Request.Form["Amount"] != null && Request.Form["Mobile"] != null)
                {
                    string amount = ReplaceCode(Request.Form["Amount"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["Mobile"].ToString().Trim());
                    Random random = new Random();
                    int SixDigit = random.Next(1000, 9999);
                    string[] valueArray = new string[2];
                    valueArray[0] = amount;
                    valueArray[1] = SixDigit.ToString();
                    SendWithVarpan(mobile, 1, valueArray);
                    Response.Write("{ " + OperationName + ":" + SixDigit.ToString() + "}");
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "tra")
            {
                #region Fund_Transfer
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["mobile"] != null && Request.Form["beniid"] != null && Request.Form["remitid"] != null && Request.Form["amount"] != null && Request.Form["mode"] != null && Request.Form["beniname"] != null && Request.Form["beniac"] != null && Request.Form["beniifsc"] != null && Request.Form["bankname"] != null)
                {
                    EzulixDmr eDmr = new EzulixDmr();
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'tra','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    string Result = string.Empty;
                                    string mobile = string.Empty;
                                    string beniid = string.Empty;
                                    string remitid = string.Empty;
                                    string amount = string.Empty;
                                    string mode = string.Empty;
                                    string beniname = string.Empty;
                                    string beniac = string.Empty;
                                    string beniifsc = string.Empty;
                                    string bankname = string.Empty;
                                    mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                                    beniid = ReplaceCode(Request.Form["beniid"].ToString().Trim());
                                    remitid = ReplaceCode(Request.Form["remitid"].ToString().Trim());
                                    amount = ReplaceCode(Request.Form["amount"].ToString().Trim());
                                    mode = ReplaceCode(Request.Form["mode"].ToString().Trim());
                                    beniname = ReplaceCode(Request.Form["beniname"].ToString().Trim());
                                    beniac = ReplaceCode(Request.Form["beniac"].ToString().Trim());
                                    beniifsc = ReplaceCode(Request.Form["beniifsc"].ToString().Trim());
                                    bankname = ReplaceCode(Request.Form["bankname"].ToString().Trim());
                                    int msrno = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + dt.Rows[0]["MemberID"].ToString() + "'");
                                    cls_myMember Clsm = new cls_myMember();
                                    if (Convert.ToInt32(amount) <= 25000)
                                    {
                                        string bundleid = Clsm.Cyrus_GetTransactionID_New();
                                        int totaltra;
                                        decimal TraAmt = Convert.ToDecimal(amount);
                                        decimal totalamt = Convert.ToDecimal(amount);
                                        decimal RemainAmt = 0;
                                        if (Convert.ToInt32(amount) % limitamount == 0)
                                            totaltra = Convert.ToInt32(amount) / limitamount;
                                        else
                                            totaltra = (Convert.ToInt32(amount) / limitamount) + 1;
                                        for (int i = 1; i <= totaltra; i++)
                                        {
                                            if (totalamt <= limitamount && TraAmt > 0)
                                                if (i > 1)
                                                    TraAmt = RemainAmt;
                                                else
                                                    TraAmt = Convert.ToDecimal(amount);
                                            else
                                                TraAmt = Convert.ToDecimal(limitamount);
                                            double NetAmount = TotupAmount(Convert.ToDouble(TraAmt), dt.Rows[0]["MemberID"].ToString());
                                            if (NetAmount > Convert.ToDouble(TraAmt))
                                            {
                                                int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), msrno);
                                                if (res == 1)
                                                {
                                                    string ETranId = string.Empty;
                                                    ETranId = Clsm.Cyrus_GetTransactionID_New();
                                                    int tra = Clsm.Wallet_MakeTransaction(Convert.ToString(dt.Rows[0]["MemberID"].ToString()), Convert.ToDecimal("-" + NetAmount.ToString()), "Dr", "DMR Topup Txn:- '" + ETranId + "'");
                                                    if (tra != 0)
                                                    {
                                                        Result = eDmr.Fund_Transfer(mobile, beniid, ETranId, Convert.ToDecimal(TraAmt), mode, dt.Rows[0]["MemberID"].ToString(), ETranId, beniname, bankname, beniac, bundleid);
                                                        if (Result != string.Empty)
                                                        {
                                                            DataSet ds = Deserialize(Result);
                                                            if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "TXN")
                                                            {
                                                                List<ParmList> _lstparm = new List<ParmList>();
                                                                _lstparm.Add(new ParmList() { name = "@remit_mobile", value = mobile });
                                                                _lstparm.Add(new ParmList() { name = "@beni_id", value = beniid });
                                                                _lstparm.Add(new ParmList() { name = "@agent_id", value = ETranId });
                                                                _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                                _lstparm.Add(new ParmList() { name = "@mode", value = mode });
                                                                _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["data"].Rows[0]["ipay_id"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["data"].Rows[0]["ref_no"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["data"].Rows[0]["opr_id"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["charged_amt"].ToString()) });
                                                                _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["locked_amt"].ToString()) });
                                                                _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["response"].Rows[0]["statuscode"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["response"].Rows[0]["status"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                                _lstparm.Add(new ParmList() { name = "@Memberid", value = dt.Rows[0]["MemberID"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@beni_name", value = beniname });
                                                                _lstparm.Add(new ParmList() { name = "@beni_account", value = beniac });
                                                                _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = beniifsc });
                                                                _lstparm.Add(new ParmList() { name = "@modetra", value = "App" });
                                                                _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                                _lstparm.Add(new ParmList() { name = "@bank", value = bankname });
                                                                _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                                _lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                                Cls.select_data_dtNew("Set_Instant_Fund_Tra", _lstparm);
                                                                Cls.select_data_dt(@"EXEC SET_DIST_Commission '" + dt.Rows[0]["MemberID"].ToString() + "','" + Convert.ToDecimal(amount) + "','" + ETranId + "','dmr'");
                                                                RemainAmt = totalamt - TraAmt;
                                                                TraAmt = RemainAmt;
                                                                totalamt = RemainAmt;
                                                            }
                                                            else if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "TUP")
                                                            {
                                                                List<ParmList> _lstparm = new List<ParmList>();
                                                                _lstparm.Add(new ParmList() { name = "@remit_mobile", value = mobile });
                                                                _lstparm.Add(new ParmList() { name = "@beni_id", value = beniid });
                                                                _lstparm.Add(new ParmList() { name = "@agent_id", value = ETranId });
                                                                _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                                _lstparm.Add(new ParmList() { name = "@mode", value = mode });
                                                                _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["data"].Rows[0]["ipay_id"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["data"].Rows[0]["ref_no"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["data"].Rows[0]["opr_id"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["charged_amt"].ToString()) });
                                                                _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["locked_amt"].ToString()) });
                                                                _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["response"].Rows[0]["statuscode"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["response"].Rows[0]["status"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                                _lstparm.Add(new ParmList() { name = "@Memberid", value = dt.Rows[0]["MemberID"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@beni_name", value = beniname });
                                                                _lstparm.Add(new ParmList() { name = "@beni_account", value = beniac });
                                                                _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = beniifsc });
                                                                _lstparm.Add(new ParmList() { name = "@modetra", value = "App" });
                                                                _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                                _lstparm.Add(new ParmList() { name = "@bank", value = bankname });
                                                                _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                                _lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                                Cls.select_data_dtNew("Set_Instant_Fund_Tra", _lstparm);
                                                                Response.Write("{ " + OperationName + ":" + Result + "}");
                                                                break;
                                                            }
                                                            else if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "IAN" || ds.Tables["response"].Rows[0]["statuscode"].ToString() == "SPE")
                                                            {
                                                                List<ParmList> _lstparm = new List<ParmList>();
                                                                _lstparm.Add(new ParmList() { name = "@remit_mobile", value = mobile });
                                                                _lstparm.Add(new ParmList() { name = "@beni_id", value = beniid });
                                                                _lstparm.Add(new ParmList() { name = "@agent_id", value = ETranId });
                                                                _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                                _lstparm.Add(new ParmList() { name = "@mode", value = mode });
                                                                _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["data"].Rows[0]["ipay_id"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["data"].Rows[0]["ref_no"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["data"].Rows[0]["opr_id"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["charged_amt"].ToString()) });
                                                                _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["locked_amt"].ToString()) });
                                                                _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["response"].Rows[0]["statuscode"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["response"].Rows[0]["status"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                                _lstparm.Add(new ParmList() { name = "@Memberid", value = dt.Rows[0]["MemberID"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@beni_name", value = beniname });
                                                                _lstparm.Add(new ParmList() { name = "@beni_account", value = beniac });
                                                                _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = beniifsc });
                                                                _lstparm.Add(new ParmList() { name = "@modetra", value = "App" });
                                                                _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                                _lstparm.Add(new ParmList() { name = "@bank", value = bankname });
                                                                _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                                _lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                                Cls.select_data_dtNew("Set_Instant_Fund_Tra", _lstparm);
                                                                Clsm.Wallet_MakeTransaction(dt.Rows[0]["MemberID"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                                break;
                                                            }
                                                            else if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "ERR" || ds.Tables["response"].Rows[0]["statuscode"].ToString() == "EZX" || ds.Tables["response"].Rows[0]["statuscode"].ToString() == "DTX" || ds.Tables["response"].Rows[0]["statuscode"].ToString() == "IAB")
                                                            {
                                                                Clsm.Wallet_MakeTransaction(dt.Rows[0]["MemberID"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                                break;

                                                            }
                                                            else
                                                            {
                                                                cls.select_data_dt(@"insert into Instant_bug values ('Transcation','" + Result + ",ETranId:" + ETranId + "," + DateTime.Now.ToString() + "')");
                                                                break;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                ReturnError("Error | Dmr Surcharge not define", OperationName);
                                                break;
                                            }
                                        }
                                        Response.Write("{ " + OperationName + ":" + Result + "}");
                                    }
                                    else
                                    {
                                        ReturnError("Error | Amount could not be grater than 25000!", OperationName);
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Loggined to another device Contact your admin", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "beniregReotp")
            {
                #region Beneficiary_Registration_Resend_OTP
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["remitid"] != null && Request.Form["benid"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'beniregReotp','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    string Result = string.Empty;
                                    string beniid = string.Empty;
                                    string remitid = string.Empty;
                                    beniid = ReplaceCode(Request.Form["benid"].ToString().Trim());
                                    remitid = ReplaceCode(Request.Form["remitid"].ToString().Trim());
                                    EzulixDmr eDmr = new EzulixDmr();
                                    Result = eDmr.Beneficiary_Registration_Resend_OTP(remitid, beniid);
                                    if (Result != string.Empty)
                                    {
                                        Response.Write("{ " + OperationName + ":" + Result + "}");
                                    }
                                    else
                                    {
                                        ReturnError("Invalid Request Format", "Unknown");
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Logged into another device Contact your admin", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            if (OperationName == "benisub")
            {
                #region Beneficiary_Registration_Validate
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["mobile"] != null && Request.Form["benid"] != null && Request.Form["otp"] != null && Request.Form["beniname"] != null && Request.Form["benimob"] != null && Request.Form["beniifsc"] != null && Request.Form["beniac"] != null && Request.Form["remitid"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'benisub','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    string Result = string.Empty;
                                    string beniid = string.Empty;
                                    string otp = string.Empty;
                                    string mobile = string.Empty;
                                    string beniname = string.Empty;
                                    string benimob = string.Empty;
                                    string beniifsc = string.Empty;
                                    string beniac = string.Empty;
                                    string remitid = string.Empty;
                                    beniid = ReplaceCode(Request.Form["benid"].ToString().Trim());
                                    mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                                    beniname = ReplaceCode(Request.Form["beniname"].ToString().Trim());
                                    benimob = ReplaceCode(Request.Form["benimob"].ToString().Trim());
                                    beniifsc = ReplaceCode(Request.Form["beniifsc"].ToString().Trim());
                                    beniac = ReplaceCode(Request.Form["beniac"].ToString().Trim());
                                    otp = ReplaceCode(Request.Form["otp"].ToString().Trim());
                                    remitid = ReplaceCode(Request.Form["remitid"].ToString().Trim());
                                    cls_connection Cls = new cls_connection();
                                    EzulixDmr eDmr = new EzulixDmr();
                                    Result = eDmr.Beneficiary_Registration_Validate(remitid, beniid, otp);
                                    if (Result != string.Empty)
                                    {
                                        DataSet ds = Deserialize(Result);
                                        if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                                        {
                                            DataTable dtchk = Cls.select_data_dt(@"Exec Set_Instant_Beni_Reg '" + beniname + "','" + benimob + "','" + beniac + "','" + beniifsc + "','" + beniid + "','" + dt.Rows[0]["MemberID"].ToString() + "','" + remitid + "',@mode='App'");
                                            if (dtchk.Rows.Count > 0)
                                            {
                                                string output = ConvertDataTabletoString(dtchk);
                                                Response.Write("{ " + OperationName + ":" + output + "}");
                                            }
                                            else
                                            {
                                                Response.Write("{ " + OperationName + ":" + Result + "}");
                                            }
                                        }
                                        else
                                        {
                                            Response.Write("{ " + OperationName + ":" + Result + "}");
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Invalid Request Format", "Unknown");
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            if (OperationName == "benidel")
            {
                #region Beneficiary_Delete
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["beniid"] != null && Request.Form["remitid"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'benidel','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {

                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {

                                    string Result = string.Empty;
                                    string beniid = string.Empty;
                                    string remitid = string.Empty;
                                    beniid = ReplaceCode(Request.Form["beniid"].ToString().Trim());
                                    remitid = ReplaceCode(Request.Form["remitid"].ToString().Trim());
                                    EzulixDmr eDmr = new EzulixDmr();
                                    Result = eDmr.Beneficiary_Delete(beniid, remitid);
                                    if (Result != string.Empty)
                                    {
                                        DataSet ds = Deserialize(Result);
                                        if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                                        {
                                            Response.Write("{ " + OperationName + ":" + Result + "}");
                                        }
                                        else
                                        {
                                            Response.Write("{ " + OperationName + ":" + Result + "}");
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Invalid Request Format", "Unknown");
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Logged into anthoer device Contact your admin", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            if (OperationName == "benidelvel")
            {
                #region Beneficiary_Delete_Validate
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["beniid"] != null && Request.Form["remitid"] != null && Request.Form["otp"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'benidelvel','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {

                        DataTable dtmmember = cls.select_data_dt("select * from tblmlm_membermaster where Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "' and memberdesc='" + mcode + "' and isactive=1 and isdelete=0");
                        if (dtmmember.Rows.Count > 0)
                        {
                            DataTable dmmchk = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + Convert.ToInt32(dtmmember.Rows[0]["MsrNo"]) + "");
                            if (dmmchk.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dmmchk.Rows[0]["isemailverify"]) == true)
                                {
                                    string Result = string.Empty;
                                    string benid = string.Empty;
                                    string remitid = string.Empty;
                                    string otp = string.Empty;
                                    benid = ReplaceCode(Request.Form["beniid"].ToString().Trim());
                                    remitid = ReplaceCode(Request.Form["remitid"].ToString().Trim());
                                    otp = ReplaceCode(Request.Form["otp"].ToString().Trim());
                                    EzulixDmr eDmr = new EzulixDmr();
                                    Result = eDmr.Beneficiary_Delete_Validate(benid, remitid, otp);
                                    if (Result != string.Empty)
                                    {
                                        Response.Write("{ " + OperationName + ":" + Result + "}");
                                    }
                                    else
                                    {
                                        ReturnError("Invalid Request Format", "Unknown");
                                    }
                                }
                                else
                                {
                                    ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("DMR Service is inactive Contact your admin", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Member Logged into anthoer device Contact your admin", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "memreport")
            {
                #region MemberReport
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'remitreport','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        string memberid = string.Empty;
                        memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                        int msrNo = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + memberid + "'");
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@msrno", value = msrNo });
                        _lstparm.Add(new ParmList() { name = "@datefrom", value = "01-01-1990" });
                        _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
                        DataTable dttra = Cls.select_data_dtNew(@"RP_DMR_transaction", _lstparm);
                        if (dttra.Rows.Count > 0)
                        {
                            string output = ConvertDataTabletoString(dttra);
                            Response.Write("{ " + OperationName + ":" + output + "}");
                        }
                        else
                        {
                            ReturnError("No Transcation found", "Unknown");
                        }
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            #endregion

            #region AEPS
            if (OperationName == "aepskyccheckstatus")
            {
                #region aepskyccheckstatus
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["msrno"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    string msrno = ReplaceCode(Request.Form["MsrNo"].ToString().Trim());
                    DataTable dtChk = Cls.select_data_dt(@"SELECT * FROM Tbl_Aeps_Reg WHERE MsrNo='" + msrno + "'");
                    if (dtChk.Rows.Count > 0)
                    {
                        string output = ConvertDataTabletoString(dtChk);
                        Response.Write("{ " + Request.Form["operationname"].ToString() + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("no data found", "Unknown");
                    }
                }
                #endregion
            }
            if (OperationName == "aepskyc")
            {
                #region aepskyc
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["msrno"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string firstname = ReplaceCode(Request.Form["firstname"].ToString().Trim());
                    string lastname = "Ji";
                    string shopname = ReplaceCode(Request.Form["shopname"].ToString().Trim());
                    string Pan_Number = ReplaceCode(Request.Form["Pan_Number"].ToString().Trim());
                    string Contact_Number = ReplaceCode(Request.Form["Contact_Number"].ToString().Trim());
                    string Email = ReplaceCode(Request.Form["Email"].ToString().Trim());
                    string state = ReplaceCode(Request.Form["state"].ToString().Trim());
                    string city = ReplaceCode(Request.Form["city"].ToString().Trim());
                    string primaryaddress = ReplaceCode(Request.Form["primaryaddress"].ToString().Trim());
                    string primarypin = ReplaceCode(Request.Form["primarypin"].ToString().Trim());
                    string secondstate = ReplaceCode(Request.Form["secondstate"].ToString().Trim());
                    string secondcity = ReplaceCode(Request.Form["secondcity"].ToString().Trim());
                    string secondaddress = ReplaceCode(Request.Form["secondaddress"].ToString().Trim());
                    string secondpin = ReplaceCode(Request.Form["secondpin"].ToString().Trim());
                    string idprooftype = ReplaceCode(Request.Form["idprooftype"].ToString().Trim());
                    string idproofnumber = ReplaceCode(Request.Form["idproofnumber"].ToString().Trim());
                    string IdenFileName = ReplaceCode(Request.Form["IdenFileName"].ToString().Trim());
                    string addrprooftype = ReplaceCode(Request.Form["addrprooftype"].ToString().Trim());
                    string addproofnumber = ReplaceCode(Request.Form["addproofnumber"].ToString().Trim());
                    string addprooffilename = ReplaceCode(Request.Form["addprooffilename"].ToString().Trim());
                    string Self_Decl_Num = ReplaceCode(Request.Form["Self_Decl_Num"].ToString().Trim());
                    string selffilename = ReplaceCode(Request.Form["selffilename"].ToString().Trim());
                    string msrno = ReplaceCode(Request.Form["MsrNo"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());

                    string identityurl1 = string.Empty;
                    string adpthurl1 = string.Empty;
                    string adminurl = Convert.ToString(ConfigurationManager.AppSettings["adminurl"]);
                    string adminmemberid = Convert.ToString(ConfigurationManager.AppSettings["adminmemberid"]);
                    if (IdenFileName != null && addprooffilename != null)
                    {
                        if (addprooffilename != "")
                        {
                            adpthurl1 = uploadaepskyc(addprooffilename);
                            adpthurl1 = adminurl + "Uploads/AEPS/" + adpthurl1;
                        }
                        else
                        {
                            adpthurl1 = "";

                        }
                        if (IdenFileName != "")
                        {
                            identityurl1 = uploadaepskyc(IdenFileName);
                            identityurl1 = adminurl + "Uploads/AEPS/" + identityurl1;
                        }

                        else
                        {
                            identityurl1 = "";
                        }
                        if (selffilename != "")
                        {
                            selffilename = uploadaepskyc(selffilename);
                        }
                        else
                        {
                            selffilename = "";
                        }


                        string selfdecurl = string.Empty;
                        if (selffilename != null)
                        {

                            selfdecurl = adminurl + "Uploads/AEPS/" + selffilename;
                        }
                        else
                        {

                            selfdecurl = null;
                        }


                        DataTable dt = Cls.select_data_dt(@"SELECT * FROM Tbl_Aeps_Reg WHERE MsrNo=" + Convert.ToInt32(msrno) + "");
                        if (dt.Rows.Count > 0)
                        {
                            Cls.select_data_dt(@"DELETE FROM Tbl_Aeps_Reg WHERE MsrNo=" + Convert.ToInt32(msrno) + "");
                        }

                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@F_Name", value = firstname });
                        _lstparm.Add(new ParmList() { name = "@L_Name", value = lastname });
                        _lstparm.Add(new ParmList() { name = "@Shop_Name", value = shopname });
                        _lstparm.Add(new ParmList() { name = "@Pan_Number", value = Pan_Number });
                        _lstparm.Add(new ParmList() { name = "@Contact_Number", value = Contact_Number });
                        _lstparm.Add(new ParmList() { name = "@Email", value = Email });
                        _lstparm.Add(new ParmList() { name = "@P_State", value = state });
                        _lstparm.Add(new ParmList() { name = "@P_City", value = city });
                        _lstparm.Add(new ParmList() { name = "@P_Address", value = primaryaddress });
                        _lstparm.Add(new ParmList() { name = "@P_Pin", value = primarypin });
                        _lstparm.Add(new ParmList() { name = "@S_State", value = secondstate });
                        _lstparm.Add(new ParmList() { name = "@S_City", value = secondcity });
                        _lstparm.Add(new ParmList() { name = "@S_Address", value = secondaddress });
                        _lstparm.Add(new ParmList() { name = "@S_Pin", value = secondpin });
                        _lstparm.Add(new ParmList() { name = "@Iden_Proof_Type", value = idprooftype });
                        _lstparm.Add(new ParmList() { name = "@Iden_Proof_Num", value = idproofnumber });
                        _lstparm.Add(new ParmList() { name = "@Iden_Proof_Filename", value = identityurl1 });
                        _lstparm.Add(new ParmList() { name = "@Addr_Proof_Type", value = addrprooftype });
                        _lstparm.Add(new ParmList() { name = "@Addr_Proof_Num", value = addproofnumber });
                        _lstparm.Add(new ParmList() { name = "@Addr_Proof_Filename", value = adpthurl1 });
                        _lstparm.Add(new ParmList() { name = "@Self_Decl_Num", value = Self_Decl_Num });
                        if (selffilename != null)

                            _lstparm.Add(new ParmList() { name = "@Self_Decl_Filename", value = adminurl + "Uploads/AEPS/" + selffilename });
                        else
                            _lstparm.Add(new ParmList() { name = "@Self_Decl_Filename", value = "" });
                        _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(msrno) });
                        _lstparm.Add(new ParmList() { name = "@MemberID", value = memberid });


                        Cls.select_data_dtNew("Set_Aeps_Reg", _lstparm);
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.AddHeader("content-type", "application/json");
                        Response.Write("{\"MSG\": \"AEPS Registrantion is successfully, Your Registrantion status is pending!\"}");
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", "Unknown");
                    }

                }
                #endregion
            }




            if (OperationName == "aepsbal")
            {
                #region AEPSBalance
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'aepsbal','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtres = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + dt.Rows[0]["MsrNo"].ToString() + ",@action='sum'");
                        string output = ConvertDataTabletoString(dtres);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "aeps")
            {
                #region AEPS_Panel
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'aeps','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtmembermaster = cls.select_data_dt("select * from tblmlm_membermaster where memberid='" + memberid + "'");
                        if (dtmembermaster.Rows.Count > 0)
                        {
                            int MsrNo = Convert.ToInt32(dtmembermaster.Rows[0]["MsrNo"]);
                            DataTable ddt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='AEPS', @msrno=" + MsrNo + "");
                            if (ddt.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(ddt.Rows[0]["isaeps"]))
                                {
                                    EzulixAeps eAEPS = new EzulixAeps();
                                    string res = eAEPS.Aeps(memberid);
                                    string Json = "{\"" + OperationName + "\": {\"url\": \"" + res + "\"}}";
                                    string Request_Json = Json.Replace(@"\", "");
                                    Response.Clear();
                                    Response.Write(Request_Json);
                                }
                                else
                                {
                                    ReturnError("Your Aeps Service is not active!", "Unknown");

                                }
                            }
                            else
                            {
                                ReturnError("Your Aeps Service is not active!", "Unknown");
                            }
                        }
                        else
                        {
                            string output = ConvertDataTabletoString(dt);
                            Response.Write("{ " + OperationName + ":" + output + "}");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", "Unknown");
                    }

                }
                #endregion
            }
            if (OperationName == "tramwallet")
            {
                #region Transfer AEPS to main wallet
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null && Request.Form["amount"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    decimal traamount = Convert.ToDecimal(ReplaceCode(Request.Form["amount"].ToString().Trim()));
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'tramwallet','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable ddd = new DataTable();
                        ddd = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='wallett', @msrno=0");
                        if (ddd.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(ddd.Rows[0]["iswallettransfer"]) == true)
                            {

                                DataTable dtchkbal = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + dt.Rows[0]["MsrNo"] + ",@action='sum'");
                                if (Convert.ToDecimal(dtchkbal.Rows[0]["usebal"].ToString()) >= traamount)
                                {
                                    string txn = clsm.Cyrus_GetTransactionID_New();
                                    int resaeps = clsm.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal("-" + traamount), "Dr", "E-Wallet Topup TXN:" + txn + "");
                                    if (resaeps == 1)
                                    {

                                        int resewallet = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(traamount), "Cr", "AEPS Wallet Topup TXN:" + txn + "");
                                        if (resewallet == 1)
                                        {
                                            Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"SUCCESS\", \"RESP_MSG\": \"Transcation Success\"}]}");
                                        }
                                        else
                                        {
                                            ReturnError("Error", "Unknown");
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Error", "Unknown");
                                    }

                                }
                                else
                                {
                                    ReturnError("Unsufficiant wallet balance", "Unknown");
                                }
                            }
                            else
                            {
                                ReturnError("Wallet Transfer Service Inactive!!!", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Wallet Transfer Service Inactive!!!", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }

                #endregion
            }
            else if (OperationName == "trabank")
            {
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null && Request.Form["amount"] != null && Request.Form["bankname"] != null && Request.Form["bankifsc"] != null && Request.Form["bankac"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    string bankname = ReplaceCode(Request.Form["bankname"].ToString().Trim());
                    string bankifsc = ReplaceCode(Request.Form["bankifsc"].ToString().Trim());
                    string bankac = ReplaceCode(Request.Form["bankac"].ToString().Trim());
                    decimal traamount = Convert.ToDecimal(ReplaceCode(Request.Form["amount"].ToString().Trim()));
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'trabank','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtchkbal = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + dt.Rows[0]["MsrNo"] + ",@action='sum'");
                        if (Convert.ToDecimal(dtchkbal.Rows[0]["usebal"].ToString()) >= traamount)
                        {
                            DataTable dtchkrequest = Cls.select_data_dt(@"EXEC AEPS_Wallet @action='chkwd',@msrno=" + dt.Rows[0]["MsrNo"] + "");
                            if (dtchkrequest.Rows.Count == 0)
                            {
                                string txn = clsm.Cyrus_GetTransactionID_New();
                                List<ParmList> _listparam = new List<ParmList>();
                                _listparam.Add(new ParmList() { name = "@msrno", value = dt.Rows[0]["MsrNo"] });
                                _listparam.Add(new ParmList() { name = "@memberid", value = memberid });
                                _listparam.Add(new ParmList() { name = "@bank", value = bankname });
                                _listparam.Add(new ParmList() { name = "@ifsc", value = bankifsc });
                                _listparam.Add(new ParmList() { name = "@ac", value = bankac });
                                _listparam.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(traamount) });
                                _listparam.Add(new ParmList() { name = "@txnid", value = txn });
                                _listparam.Add(new ParmList() { name = "@action", value = "wd" });
                                Cls.select_data_dtNew(@"AEPS_Wallet", _listparam);

                                Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"SUCCESS\", \"RESP_MSG\": \"Bank withdrawal request placed successfully\"}]}");
                            }
                            else
                            {
                                ReturnError("Your one withdrawal request is under processing !", "Unknown");
                            }
                        }
                        else
                        {
                            ReturnError("Unsufficiant wallet balance", "Unknown");
                        }
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
            }
            if (OperationName == "aepswallethistory")
            {
                #region aepswallethistory
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["msrno"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string msrno = ReplaceCode(Request.Form["msrno"].ToString().Trim());
                    clsMLM_RWalletTransaction objRWalletTransaction = new clsMLM_RWalletTransaction();
                    DataTable dtAEPSEWalletTransaction = objRWalletTransaction.ManageRWalletTransaction("GetByMsrNo", Convert.ToInt32(msrno));

                    if (dtAEPSEWalletTransaction.Rows.Count > 0)
                    {
                        string output = ConvertDataTabletoString(dtAEPSEWalletTransaction);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("No Transaction", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "aepswallet")
            {
                #region AEPSWAllet
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'aepswallet','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");

                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "listtrabank")
            {
                #region ListAepsBankTransactions
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'listtrabank','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            if (OperationName == "getbankdtl")
            {
                #region GETBANKDETAILS
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'getbankdtl','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtres = Cls.select_data_dt(@"EXEC AEPS_Wallet @action='get',@msrno=" + dt.Rows[0]["MsrNo"].ToString() + "");
                        string output = ConvertDataTabletoString(dtres);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "ubankdtl")
            {
                #region Update_Bank_details
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null && Request.Form["bankname"] != null && Request.Form["bankifsc"] != null && Request.Form["bankac"] != null && Request.Form["branchname"] != null && Request.Form["custmername"] != null && Request.Form["bankpassbook"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    string bankname = ReplaceCode(Request.Form["bankname"].ToString().Trim());
                    string bankifsc = ReplaceCode(Request.Form["bankifsc"].ToString().Trim());
                    string bankac = ReplaceCode(Request.Form["bankac"].ToString().Trim());
                    string branchname = ReplaceCode(Request.Form["branchname"].ToString().Trim());
                    string custmername = ReplaceCode(Request.Form["custmername"].ToString().Trim());
                    string bankpassbook = ReplaceCode(Request.Form["bankpassbook"].ToString().Trim());
                    if (bankpassbook != "")
                        bankpassbook = uploadbankpassbook(bankpassbook);
                    string host = ConfigurationManager.AppSettings["adminurl"] + "/Uploads/AEPS/";
                    bankpassbook = host + bankpassbook;
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'ubankdtl','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        List<ParmList> _listparam = new List<ParmList>();
                        _listparam.Add(new ParmList() { name = "@msrno", value = dt.Rows[0]["MsrNo"] });
                        _listparam.Add(new ParmList() { name = "@bankname", value = bankname });
                        _listparam.Add(new ParmList() { name = "@bankifsc", value = bankifsc });
                        _listparam.Add(new ParmList() { name = "@bankac", value = bankac });
                        _listparam.Add(new ParmList() { name = "@bankps", value = bankpassbook });
                        _listparam.Add(new ParmList() { name = "@action", value = "up" });
                        Cls.select_data_dtNew(@"sp_updatebank", _listparam);
                        Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"SUCCESS\", \"RESP_MSG\": \"Update bank details\"}]}");
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "companybank")
            {
                #region Companybank

                DataTable dt = cls.select_data_dt("Select  1 as ResponseCode,* from ViewMLM_MemberBanker where MsrNo= 1 and  IsDelete='false' order by MemberBankerID desc  ");
                string output = ConvertDataTabletoString(dt);
                Response.Write("{ " + OperationName + ":" + output + "}");

                #endregion
            }

            #endregion

            #region AepsPayout
            if (OperationName == "otpsendpayoutdmr")
            {
                #region PAyoutDMR
                if (Request.Form["Amount"] != null && Request.Form["Mobile"] != null && Request.Form["MemberId"] != null)
                {
                    string amount = ReplaceCode(Request.Form["Amount"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["Mobile"].ToString().Trim());
                    string Memberid = ReplaceCode(Request.Form["MemberId"].ToString().Trim());
                    cls_connection Cls = new cls_connection();
                    DataTable dtmember = cls.select_data_dt("select * from tblmlm_membermaster where MemberId='" + Memberid + "'");
                    if (dtmember.Rows.Count > 0)
                    {
                        DataTable sdt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='payout', @msrno=" + dtmember.Rows[0]["MsrNo"] + "");
                        if (sdt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(sdt.Rows[0]["isaepspayout"]) == false)
                            {
                                ReturnError("Aeps Payout Service Inactive", "Unknown");
                            }
                            else
                            {
                                DataTable dtm = new DataTable();
                                dtm = Cls.select_data_dt("select * from tblmlm_membermaster where msrno=" + dtmember.Rows[0]["MsrNo"] + " and isbankps=1");
                                if (dtm.Rows.Count > 0)
                                {

                                    DataTable dt = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(dtmember.Rows[0]["MsrNo"]) + ",@action='sum'");
                                    if (dt.Rows.Count > 0)
                                    {
                                        string usbal = dt.Rows[0]["usebal"].ToString();
                                        double NetAmount = PayoutTotupAmount(Convert.ToDouble(amount), dtmember.Rows[0]["MemberId"].ToString());
                                        if (NetAmount > Convert.ToDouble(amount))
                                        {
                                            if (Convert.ToDecimal(usbal) >= Convert.ToDecimal(NetAmount))
                                            {
                                                string result = string.Empty;
                                                EzulixAepsPayOut EPayout = new EzulixAepsPayOut();
                                               // result = EPayout.checkxpresspayout("100");
                                                result = "";
                                                if (result != string.Empty)
                                                {
                                                    DataSet ds = new DataSet();
                                                    ds = Deserialize(result);
                                                    if (ds.Tables.Count > 0)
                                                    {
                                                        if (ds.Tables.Contains("data"))
                                                        {
                                                            string isaepspayout = ds.Tables["data"].Rows[0]["Isaepspayout"].ToString();
                                                            string Balance = ds.Tables["data"].Rows[0]["Balance"].ToString();
                                                            if (Convert.ToBoolean(isaepspayout) == true && Convert.ToDecimal(Balance) > Convert.ToDecimal(amount))
                                                            {
                                                                Random random = new Random();
                                                                int SixDigit = random.Next(1000, 9999);
                                                                string[] valueArray = new string[2];
                                                                valueArray[0] = amount;
                                                                valueArray[1] = SixDigit.ToString();
                                                                SendWithVarpan(mobile, 1, valueArray);
                                                                Response.Write("{ " + OperationName + ":" + SixDigit.ToString() + "}");
                                                            }
                                                            else
                                                            {
                                                                ReturnError("Aeps Payout Service Inactive", "Unknown");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ReturnError("Aeps Payout Service Inactive", "Unknown");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ReturnError("Aeps Payout Service Inactive", "Unknown");
                                                    }
                                                }
                                                else
                                                {
                                                    ReturnError("Aeps Payout Service Inactive", "Unknown");
                                                }
                                            }
                                            else
                                            {
                                                ReturnError("Wallet balance is insufficent for this transcation !", "Unknown");
                                            }
                                        }
                                        else
                                        {
                                            ReturnError("Surcharge not define.", "Unknown");
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Wallet balance is insufficent for this transcation !", "Unknown");
                                    }
                                }

                                else
                                {
                                    ReturnError("Bank Details Not Updated Or Verified Contact Your Admin !", "Unknown");
                                }

                            }
                        }
                        else
                        {
                            ReturnError("Aeps Payout Service Inactive", "Unknown");
                        }
                    }

                    else
                    {
                        ReturnError("No Member Found", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            else if (OperationName == "trapayoutdmr")
            {
                #region TransactionPayout
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["account"] != null && Request.Form["ifsc"] != null && Request.Form["amount"] != null)
                {
                    EzulixAepsPayOut EPayout = new EzulixAepsPayOut();
                    cls_connection Cls = new cls_connection();
                    cls_myMember Clsm = new cls_myMember();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string amount = ReplaceCode(Request.Form["amount"].ToString().Trim());
                    string account = ReplaceCode(Request.Form["account"].ToString().Trim());
                    string ifsc = ReplaceCode(Request.Form["ifsc"].ToString().Trim());
                    DataTable dst = cls.select_data_dt("Exec App_getDetails 'aepspt','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dst.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable sdt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='payout', @msrno=" + dst.Rows[0]["MsrNo"] + "");
                        if (sdt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(sdt.Rows[0]["isaepspayout"]) == false)
                            {
                                ReturnError("Aeps Payout Service Inactive", "Unknown");
                            }
                            else
                            {
                                DataTable dt = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(dst.Rows[0]["MsrNo"]) + ",@action='sum'");
                                if (dt.Rows.Count > 0)
                                {
                                    string usbal = dt.Rows[0]["usebal"].ToString();
                                    if (Convert.ToDecimal(usbal) >= Convert.ToDecimal(amount))
                                    {
                                        string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                        timestamp = timestamp.Replace("-", "");
                                        timestamp = timestamp.Replace(":", "");
                                        timestamp = timestamp.Replace(".", "");
                                        timestamp = timestamp.Replace(" ", "");
                                        string orderId = "PA" + timestamp;
                                        string beneficiaryAccount = account;
                                        string beneficiaryIFSC = ifsc;
                                        string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                                        string Result = string.Empty;
                                        double NetAmount = PayoutTotupAmount(Convert.ToDouble(amount), dst.Rows[0]["MemberId"].ToString());
                                        if (NetAmount > Convert.ToDouble(amount))
                                        {
                                            string resulst = string.Empty;
                                            //resulst = EPayout.checkxpresspayout("100");
                                            resulst = "";
                                            if (resulst != string.Empty)
                                            {
                                                DataSet sds = new DataSet();
                                                sds = Deserialize(resulst);
                                                if (sds.Tables.Count > 0)
                                                {
                                                    if (sds.Tables.Contains("data"))
                                                    {
                                                        string isaepspayout = sds.Tables["data"].Rows[0]["Isaepspayout"].ToString();
                                                        string Balance = sds.Tables["data"].Rows[0]["Balance"].ToString();
                                                        if (Convert.ToBoolean(isaepspayout) == true && Convert.ToDecimal(Balance) > Convert.ToDecimal(amount))
                                                        {
                                                            int tra = Clsm.AEPSWallet_MakeTransaction_Ezulix(dst.Rows[0]["MemberId"].ToString(), Convert.ToDecimal("-" + NetAmount.ToString().Trim()), "Dr", "Xpress AEPS Payoyut Topup Txn:-" + orderId + "");
                                                            if (tra > 0)
                                                            {
                                                                List<ParmList> _lstparm = new List<ParmList>();
                                                                _lstparm.Add(new ParmList() { name = "@MemberId", value = dst.Rows[0]["MemberId"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(dst.Rows[0]["MsrNo"]) });
                                                                _lstparm.Add(new ParmList() { name = "@AgentOrderId", value = orderId });
                                                                _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(amount) });
                                                                _lstparm.Add(new ParmList() { name = "@beneficiaryAccount", value = beneficiaryAccount });
                                                                _lstparm.Add(new ParmList() { name = "@beneficiaryIFSC", value = beneficiaryIFSC });
                                                                _lstparm.Add(new ParmList() { name = "@status", value = "Pending" });
                                                                _lstparm.Add(new ParmList() { name = "@statusCode", value = "00" });
                                                                _lstparm.Add(new ParmList() { name = "@statusMessage", value = "Pending" });
                                                                _lstparm.Add(new ParmList() { name = "@NetAmount", value = Convert.ToDecimal(NetAmount) });
                                                                _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                                                                Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer", _lstparm);
                                                                Result = EPayout.WithDrawal_Ammount(orderId, amount, beneficiaryAccount, beneficiaryIFSC, date);
                                                                DataSet ds = Deserialize(Result);
                                                                List<ParmList> _lstparms = new List<ParmList>();
                                                                _lstparms.Add(new ParmList() { name = "@AgentOrderId", value = orderId });
                                                                _lstparms.Add(new ParmList() { name = "@status", value = ds.Tables[0].Rows[0]["status"].ToString() });
                                                                _lstparms.Add(new ParmList() { name = "@statusCode", value = ds.Tables[0].Rows[0]["statusCode"].ToString() });
                                                                _lstparms.Add(new ParmList() { name = "@statusMessage", value = ds.Tables[0].Rows[0]["statusMessage"].ToString() });
                                                                _lstparms.Add(new ParmList() { name = "@Action", value = "Ub" });
                                                                Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer", _lstparms);
                                                                if (ds.Tables[0].Rows[0]["status"].ToString() == "FAILURE")
                                                                {
                                                                    Clsm.AEPSWallet_MakeTransaction_Ezulix(dst.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "Fail Xpress AEPS Payoyut Topup Txn:-" + orderId + "");
                                                                }
                                                                Response.Write("{ " + OperationName + ":" + Result + "}");

                                                            }
                                                            else
                                                            {
                                                                ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ReturnError("Aeps Payout Service Inactive", "Unknown");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ReturnError("Aeps Payout Service Inactive", "Unknown");
                                                    }
                                                }
                                                else
                                                {
                                                    ReturnError("Aeps Payout Service Inactive", "Unknown");
                                                }
                                            }
                                            else
                                            {
                                                ReturnError("Aeps Payout Service Inactive", "Unknown");
                                            }
                                        }
                                        else
                                        {
                                            ReturnError("Error | Surcharge not define", OperationName);
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);

                                    }
                                }
                                else
                                {
                                    ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                }
                            }
                        }
                        else
                        {
                            ReturnError("Aeps Payout Service Inactive", "Unknown");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            #endregion


            #region xpressdmr
            if (OperationName == "otpsendxpressdmr#")
            {
                if (Request.Form["Amount"] != null && Request.Form["Mobile"] != null && Request.Form["MemberId"] != null)
                {
                    string amount = ReplaceCode(Request.Form["Amount"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["Mobile"].ToString().Trim());
                    string Memberid = ReplaceCode(Request.Form["MemberId"].ToString().Trim());
                    cls_connection Cls = new cls_connection();
                    DataTable dtmember = cls.select_data_dt("select * from tblmlm_membermaster where MemberId='" + Memberid + "'");
                    if (dtmember.Rows.Count > 0)
                    {
                        DataTable dst = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='xpressdmr', @msrno=" + dtmember.Rows[0]["MsrNo"] + "");
                        if (dst.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(dst.Rows[0]["isxpressdmr"]) == false)
                            {
                                ReturnError("Xpress DMR Is Inactive Contact your admin!", "Unknown");
                            }
                            else
                            {
                                cls_myMember Clsm = new cls_myMember();
                                double NetAmount = XpressDMRTotupAmount(Convert.ToDouble(amount), dtmember.Rows[0]["MemberId"].ToString());
                                if (NetAmount > Convert.ToDouble(amount))
                                {
                                    int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), Convert.ToInt32(dst.Rows[0]["MsrNo"]));
                                    if (res == 1)
                                    {

                                        Random random = new Random();
                                        int SixDigit = random.Next(1000, 9999);
                                        string[] valueArray = new string[2];
                                        valueArray[0] = amount;
                                        valueArray[1] = SixDigit.ToString();
                                        SendWithVarpan(mobile, 1, valueArray);
                                        Response.Write("{ " + OperationName + ":" + SixDigit.ToString() + "}");
                                    }
                                    else
                                    {
                                        ReturnError("Wallet balance is insufficent for this transcation !", "Unknown");
                                    }
                                }
                                else
                                {

                                    ReturnError("Surcharge not define.", "Unknown");
                                }


                            }
                        }




                    }

                    else
                    {
                        ReturnError("No Member Found", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
            }



            else if (OperationName == "xpressdmrreport")
            {
                #region MemberReport
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'xpresspt','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        string memberid = string.Empty;
                        memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                        int msrNo = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + memberid + "'");
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@msrno", value = msrNo });
                        _lstparm.Add(new ParmList() { name = "@datefrom", value = "01-01-1990" });
                        _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
                        DataTable dttra = Cls.select_data_dtNew(@"SET_t_Ezulix_PayOut_MoneyTransfer_Report", _lstparm);
                        if (dttra.Rows.Count > 0)
                        {
                            string output = ConvertDataTabletoString(dttra);
                            Response.Write("{ " + OperationName + ":" + output + "}");
                        }
                        else
                        {
                            ReturnError("No Transcation found", "Unknown");
                        }
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            else if (OperationName == "traxpressdmr#")
            {
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["account"] != null && Request.Form["ifsc"] != null && Request.Form["amount"] != null)
                {
                    EzulixDmrPayOut EsPayout = new EzulixDmrPayOut();
                    cls_connection Cls = new cls_connection();
                    cls_myMember Clsm = new cls_myMember();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string amount = ReplaceCode(Request.Form["amount"].ToString().Trim());
                    string account = ReplaceCode(Request.Form["account"].ToString().Trim());
                    string ifsc = ReplaceCode(Request.Form["ifsc"].ToString().Trim());
                    DataTable dst = cls.select_data_dt("Exec App_getDetails 'xpresspt','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dst.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dsst = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='xpressdmr', @msrno=" + dst.Rows[0]["MsrNo"] + "");
                        if (dsst.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(dsst.Rows[0]["isxpressdmr"]) == false)
                            {
                                ReturnError("Xpress DMR Is Inactive Contact your admin!", "Unknown");
                            }
                            else
                            {
                                string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                timestamp = timestamp.Replace("-", "");
                                timestamp = timestamp.Replace(":", "");
                                timestamp = timestamp.Replace(".", "");
                                timestamp = timestamp.Replace(" ", "");
                                string orderId = timestamp;
                                string beneficiaryAccount = account;
                                string beneficiaryIFSC = ifsc;
                                string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                                string Result = string.Empty;
                                double NetAmount = XpressDMRTotupAmount(Convert.ToDouble(amount), dst.Rows[0]["MemberId"].ToString());
                                if (NetAmount > Convert.ToDouble(amount))
                                {
                                    int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), Convert.ToInt32(dst.Rows[0]["MsrNo"]));
                                    if (res == 1)
                                    {
                                        int tra = Clsm.Wallet_MakeTransaction(dst.Rows[0]["MemberId"].ToString(), Convert.ToDecimal("-" + NetAmount.ToString().Trim()), "Dr", "DMR Topup Txn:-" + orderId + "");
                                        if (tra > 0)
                                        {
                                            Result = EsPayout.WithDrawal_Ammount(orderId, amount, beneficiaryAccount, beneficiaryIFSC, date);
                                            DataSet ds = Deserialize(Result);
                                            List<ParmList> _lstparm = new List<ParmList>();
                                            _lstparm.Add(new ParmList() { name = "@MemberId", value = dst.Rows[0]["MemberId"].ToString() });
                                            _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(dst.Rows[0]["MsrNo"]) });
                                            _lstparm.Add(new ParmList() { name = "@AgentOrderId", value = orderId });
                                            _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(amount) });
                                            _lstparm.Add(new ParmList() { name = "@beneficiaryAccount", value = beneficiaryAccount });
                                            _lstparm.Add(new ParmList() { name = "@beneficiaryIFSC", value = beneficiaryIFSC });
                                            _lstparm.Add(new ParmList() { name = "@status", value = ds.Tables[0].Rows[0]["status"].ToString() });
                                            _lstparm.Add(new ParmList() { name = "@statusCode", value = ds.Tables[0].Rows[0]["statusCode"].ToString() });
                                            _lstparm.Add(new ParmList() { name = "@statusMessage", value = ds.Tables[0].Rows[0]["statusMessage"].ToString() });
                                            _lstparm.Add(new ParmList() { name = "@NetAmount", value = Convert.ToDecimal(NetAmount) });
                                            _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                                            Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer", _lstparm);
                                            if (ds.Tables[0].Rows[0]["status"].ToString() == "FAILURE")
                                            {
                                                Clsm.Wallet_MakeTransaction(dst.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:-" + orderId + "");
                                            }
                                            Response.Write("{ " + OperationName + ":" + Result + "}");
                                        }
                                        else
                                        {
                                            ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                    }
                                }
                                else
                                {
                                    ReturnError("Error | Surcharge not define", OperationName);
                                }

                            }
                        }
                        else
                        {
                            ReturnError("Xpress DMR Is Inactive Contact your admin!", "Unknown");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }

            }

            #endregion

            #region FundManagement
            if (OperationName == "listfundrequest")
            {
                #region listfundrequest
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'listfundrequest','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "addfund")
            {
                #region addfund
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["amount"] != null && Request.Form["remark"] != null && Request.Form["BankName"] != null && Request.Form["PaymentMode"] != null && Request.Form["PaymentProof"] != null && Request.Form["ChequeNumber"] != null && Request.Form["fromBank"] != null && Request.Form["toBank"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string Remark = ReplaceCode(Request.Form["remark"].ToString().Trim());

                    string BankName = ReplaceCode(Request.Form["BankName"].ToString().Trim());
                    string PaymentMode = ReplaceCode(Request.Form["PaymentMode"].ToString().Trim());
                    string PaymentProof = ReplaceCode(Request.Form["PaymentProof"].ToString().Trim());
                    string ChequeNumber = ReplaceCode(Request.Form["ChequeNumber"].ToString().Trim());
                    string fromBank = ReplaceCode(Request.Form["fromBank"].ToString().Trim());
                    string toBank = ReplaceCode(Request.Form["toBank"].ToString().Trim());
                    string amount = ReplaceCode(Request.Form["amount"].ToString().Trim());

                    if (PaymentProof != "")
                        PaymentProof = uploadFundRequestImage_profile(PaymentProof, "FundRequest");
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'addfund','" + mcode + "','" + loginip + "','" + deviceid + "','','" + amount + "',0,'" + BankName + "','" + PaymentMode + "','" + PaymentProof + "','" + ChequeNumber + "','" + Remark + "','" + fromBank + "','" + toBank + "'");
                    DataTable dtMember = cls.select_data_dt("Select MemberId from tblMLM_MemberMaster where Lastloginip='" + loginip + " and memberdesc'" + mcode + "'");
                    DataTable dtd = cls.select_data_dt("Select * from tblMLM_MemberMaster where MsrNo=1");
                    string[] valueArray = new string[2];
                    valueArray[0] = amount;
                    valueArray[1] = dtMember.Rows[0]["MemberId"].ToString();
                    DLTSMS.SendWithVar(dtd.Rows[0]["Mobile"].ToString(), 5, valueArray, 1);
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            if (OperationName == "trf")
            {
                #region trf
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["tonumber"] != null && Request.Form["amount"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string tonumber = ReplaceCode(Request.Form["tonumber"].ToString().Trim());
                    string amount = ReplaceCode(Request.Form["amount"].ToString().Trim());
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@mcode", value = mcode });
                    _lstparm.Add(new ParmList() { name = "@deviceid", value = deviceid });
                    _lstparm.Add(new ParmList() { name = "@loginip", value = loginip });
                    DataTable dtmm = cls.select_data_dtNew("sp_getmemberdata", _lstparm);
                    if (dtmm.Rows.Count > 0)
                    {
                        clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
                        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
                        cls_myMember Clsm = new cls_myMember();
                        string msrno = dtmm.Rows[0]["MsrNo"].ToString();
                        string frommemberid = dtmm.Rows[0]["MemberId"].ToString();
                        List<ParmList> _lstparms = new List<ParmList>();
                        _lstparms.Add(new ParmList() { name = "@msrno", value = msrno });
                        _lstparms.Add(new ParmList() { name = "@mobile", value = tonumber });
                        DataTable dtg = cls.select_data_dtNew("sp_getparentdata", _lstparms);
                        if (dtg.Rows.Count > 0)
                        {
                            string tomemberid = dtg.Rows[0]["MemberId"].ToString();
                            string tomsrno = dtg.Rows[0]["msrno"].ToString();

                            DataTable dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("RGetBalanceByMsrNo", Convert.ToInt32(msrno));
                            if (dtEWalletBalance.Rows.Count > 0)
                            {
                                if (Convert.ToDecimal(amount) <= Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                {
                                    string txn = Clsm.Cyrus_GetTransactionID_New();
                                    string narratiom = "Transfer Fund to " + tomemberid + " from " + frommemberid + " with " + txn + "";
                                    DataTable dt = objEWalletTransaction.EWalletTransaction_Ezulix(frommemberid, -Convert.ToDecimal(amount), "Dr", narratiom);
                                    if (dt.Rows[0]["idno"].ToString() == "1")
                                    {
                                        DataTable dttomember = objEWalletTransaction.EWalletTransaction_Ezulix(Convert.ToString(tomemberid), Convert.ToDecimal(amount), "Cr", narratiom);
                                        if (dttomember.Rows[0]["idno"].ToString() == "1")
                                        {
                                            Response.Write("{" + OperationName + ":[{\"ResponseCode\": \"1\", \"ResponseStatus\": \"Fund Transfered Successfully\"}]}");
                                        }
                                        else
                                        {
                                            Response.Write("{" + OperationName + ":[{\"ResponseCode\": \"0\", \"ResponseStatus\": \"Fund Not Transfer due to some error\"}]}");
                                        }

                                    }
                                    else
                                    {
                                        Response.Write("{" + OperationName + ":[{\"ResponseCode\": \"0\", \"ResponseStatus\": \"Insufficient Wallet Balance\"}]}");
                                    }
                                }
                                else
                                {
                                    Response.Write("{" + OperationName + ":[{\"ResponseCode\": \"0\", \"ResponseStatus\": \"Insufficient Wallet Balance\"}]}");
                                }
                            }
                            else
                            {
                                Response.Write("{" + OperationName + ":[{\"ResponseCode\": \"0\", \"ResponseStatus\": \"Insufficient Wallet Balance\"}]}");
                            }

                        }
                        else
                        {
                            Response.Write("{" + OperationName + ":[{\"ResponseCode\": \"0\", \"ResponseStatus\": \"Member Inactive\"}]}");
                        }
                    }
                    else
                    {
                        Response.Write("{" + OperationName + ":[{\"ResponseCode\": \"0\", \"ResponseStatus\": \"Member Inactive\"}]}");
                    }

                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            #endregion

            #region memberprofile
            else if (OperationName == "profile")
            {
                #region Updateprofile
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["name"] != null && Request.Form["memberimage"] != null && Request.Form["address"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string name = ReplaceCode(Request.Form["name"].ToString().Trim());
                    string memberimage = ReplaceCode(Request.Form["memberimage"].ToString().Trim());
                    string address = ReplaceCode(Request.Form["address"].ToString().Trim());

                    if (cls.select_data_scalar_int("Select count(*) from tblmlm_membermaster where memberdesc='" + mcode + "' and Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "'") > 0 && name != "" && name.Trim().Length >= 4)
                    {
                        if (memberimage != "")
                            memberimage = uploadFundRequestImage_profile(memberimage, "Profile");
                        cls.update_data("Update tblmlm_membermaster set firstname='" + name.Trim() + "',email='" + address + "',memberimage=case when '" + memberimage + "'<>'' then '" + memberimage + "' else memberimage end where  memberdesc='" + mcode + "' and Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "'");
                        DataTable dt = cls.select_data_dt("select firstname,email,memberimage  from tblmlm_membermaster where  memberdesc='" + mcode + "' and Lastloginip='" + loginip + "' and s_landmark='" + deviceid + "'");

                        // DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'Profile Updated Successfully' as ResponseStatus");
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("Validation error !! full Name required", "profile");
                    }
                }
                else
                {
                    ReturnError("Invalid request format !!", "Signup");
                }
                #endregion
            }
            if (OperationName == "contact")
            {
                #region contact
                DataTable dt = cls.select_data_dt("select 1 as ResponseCode,CompanyName, Phone, Mobile,Email,Website from tblcompany where companyid=2");
                string output = ConvertDataTabletoString(dt);
                Response.Write("{ " + OperationName + ":" + output + "}");
                #endregion
            }
            if (OperationName == "getdisti")
            {
                #region getdistributordetails
                if (Request.Form["MsrNo"] != null)
                {
                    int msrno = Convert.ToInt32(ReplaceCode(Request.Form["MsrNo"].ToString()));
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select parentmsrno from tblmlm_membermaster where msrno='" + msrno + "'");
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dm = new DataTable();
                        dm = cls.select_data_dt("select * from tblmlm_membermaster where msrno='" + Convert.ToInt32(dt.Rows[0]["parentmsrno"]) + "'");
                        string output = ConvertDataTabletoString(dm);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "help")
            {
                #region Help
                if (Request.Form["Mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["category"] != null && Request.Form["subcategory"] != null && Request.Form["detail"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string category = ReplaceCode(Request.Form["category"].ToString().Trim().Replace("$", " "));
                    string subcategory = ReplaceCode(Request.Form["subcategory"].ToString().Trim().Replace("$", " "));
                    string detail = ReplaceCode(Request.Form["detail"].ToString().Trim());
                    string RC_complain = detail.Replace("$", " ");
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'member','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (Convert.ToInt32(dt.Rows[0]["ResponseCode"]) > 0)
                    {
                        string DistName = dt.Rows[0]["memberid"].ToString();
                        cls_myMember clsm = new cls_myMember();
                        string str = clsm.Cyrus_GetTransactionID_New(); ;
                        FlexiMail objSendMail = new FlexiMail();
                        objSendMail.To = Convert.ToString(ConfigurationManager.AppSettings["mailTo"]);
                        objSendMail.CC = "";
                        objSendMail.BCC = "";
                        objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                        objSendMail.FromName = "Recharge Issue " + category + ">" + subcategory;
                        objSendMail.MailBodyManualSupply = true;
                        objSendMail.Subject = "Recharge Issue " + category + ">" + subcategory;
                        objSendMail.MailBody = RC_complain;
                        objSendMail.Send();
                        dt = cls.select_data_dt("Select 1 as ResponseCode,'Complain Registered Successfully.' as ResponseStatus,'" + str + "' as ReferenceNumber");
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("Invalid Request", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }


            #endregion


            #region NewSignup
            if (OperationName == "updatertcount")
            {
                #region bal
                if (Request.Form["totalcount"] != null && Request.Form["memberid"] != null && Request.Form["membertypeid"] != null)
                {
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    int totalcount = Convert.ToInt32(Request.Form["totalcount"].ToString());
                    int membertypeid = Convert.ToInt32(Request.Form["membertypeid"].ToString());
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select msrno from tblmlm_membermaster where memberid='" + memberid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        int msrno = Convert.ToInt32(dt.Rows[0]["Msrno"]);
                        int sum = totalcount - 1;
                        cls.update_data("update tblmlm_memberplans_adminapprove set Remaningcount='" + sum + "',Lastmodifieddate='" + DateTime.Now + "' where MsrNo='" + msrno + "' and membertype='" + membertypeid + "'");
                        Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"SUCCESS\", \"MSG\": \"Request Sent successfully\"}]}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            if (OperationName == "package")
            {
                #region Package
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null && Request.Form["newmmid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    string newmmid = ReplaceCode(Request.Form["newmmid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'package','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        clsMLM_Package objPackage = new clsMLM_Package();
                        DataTable dtPackage = new DataTable();
                        dtPackage = cls.select_data_dt("Exec getPackagebyTypeidapp " + dt.Rows[0]["msrno"].ToString() + "," + newmmid + "");
                        string output = ConvertDataTabletoString(dtPackage);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "mmtype")
            {
                #region MemberType
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'mmtype','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable dtres = cls.select_data_dt(@"exec procmlm_GetActiveMemberType '" + dt.Rows[0]["MemberType"].ToString() + "'");
                        string output = ConvertDataTabletoString(dtres);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "listreg")
            {
                #region ListMemberRegistration
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'listreg','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
                        DataTable dtMemberMaster = objMemberMaster.ManageMemberMaster("GetByView", Convert.ToInt32(dt.Rows[0]["msrno"].ToString()));
                        dtMemberMaster.Columns.Remove("ParentMsrNo");
                        dtMemberMaster.Columns.Remove("IsActive");
                        dtMemberMaster.Columns.Remove("DOJ");
                        dtMemberMaster.PrimaryKey = null;
                        dtMemberMaster.Columns.Remove("MsrNo");
                        dtMemberMaster.Columns.Remove("LastLoginDate");
                        string output = ConvertDataTabletoString(dtMemberMaster);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "newreg")
            {
                #region MemberRegistration
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["address"] != null && Request.Form["email"] != null && Request.Form["memberid"] != null && Request.Form["mobile"] != null && Request.Form["newmmid"] != null && Request.Form["newmmname"] != null && Request.Form["fname"] != null && Request.Form["sid"] != null && Request.Form["sname"] != null && Request.Form["cid"] != null && Request.Form["cname"] != null && Request.Form["packageid"] != null && Request.Form["lastname"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    string newmmid = ReplaceCode(Request.Form["newmmid"].ToString().Trim());
                    string newmmname = ReplaceCode(Request.Form["newmmname"].ToString().Trim());
                    string firstname = ReplaceCode(Request.Form["fname"].ToString().Trim());
                    string lastname = ReplaceCode(Request.Form["lastname"].ToString().Trim());
                    string sid = ReplaceCode(Request.Form["sid"].ToString().Trim());
                    string sname = ReplaceCode(Request.Form["sname"].ToString().Trim());
                    string cid = ReplaceCode(Request.Form["cid"].ToString().Trim());
                    string cname = ReplaceCode(Request.Form["cname"].ToString().Trim());
                    string packageid = ReplaceCode(Request.Form["packageid"].ToString().Trim());
                    string address = ReplaceCode(Request.Form["address"].ToString().Trim());
                    string email = ReplaceCode(Request.Form["email"].ToString().Trim());

                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'newreg','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {

                        DataTable Checkstatus = cls.select_data_dt("Exec Proc_Check 'Mobile','" + mobile + "',''");
                        if (Checkstatus.Rows[0][0].ToString() == "0")
                        {
                            string output = ConvertDataTabletoString(Checkstatus);
                            Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"Fail\", \"RESP_MSG\": \"Member Already Exists !\"}]}");
                        }
                        else
                        {
                            Int32 intresult = 0;
                            Random random = new Random();
                            int SixDigit = random.Next(100000, 999999);
                            string NewMemberID = "";
                            if (Convert.ToInt32(newmmid) == 4)
                            {
                                NewMemberID = "DT" + SixDigit;
                            }
                            else if (Convert.ToInt32(newmmid) == 5)
                            {
                                NewMemberID = "RT" + SixDigit;
                            }

                            else if (Convert.ToInt32(newmmid) == 3)
                            {
                                NewMemberID = "MD" + SixDigit;
                            }
                            DateTime DOJ = DateTime.Now;
                            DateTime MDOB = DateTime.Now;
                            Random Rand = new Random();
                            int Password = random.Next(100000, 999999);
                            int Pin = random.Next(1000, 9999);
                            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();

                         
                            List<ParmList> _lstparm = new List<ParmList>();
                            _lstparm.Add(new ParmList() { name = "@MemberID", value = NewMemberID.ToString() });
                            _lstparm.Add(new ParmList() { name = "@FirstName", value = firstname.ToString() });
                            _lstparm.Add(new ParmList() { name = "@ShopName", value = "" });
                            _lstparm.Add(new ParmList() { name = "@PackageID", value = Convert.ToInt32(packageid) });
                            _lstparm.Add(new ParmList() { name = "@LastName", value = lastname.ToString() });
                            _lstparm.Add(new ParmList() { name = "@Email", value = email.ToString() });
                            _lstparm.Add(new ParmList() { name = "@DOB", value = MDOB.ToString() });
                            _lstparm.Add(new ParmList() { name = "@Gender", value = "" });
                            _lstparm.Add(new ParmList() { name = "@Password", value = Password.ToString() });
                            _lstparm.Add(new ParmList() { name = "@TransactionPassword", value = Pin.ToString() });
                            _lstparm.Add(new ParmList() { name = "@Mobile", value = mobile.ToString() });
                            _lstparm.Add(new ParmList() { name = "@STDCode", value = "" });
                            _lstparm.Add(new ParmList() { name = "@Landline", value = "" });
                            _lstparm.Add(new ParmList() { name = "@Address", value = address.ToString() });
                            _lstparm.Add(new ParmList() { name = "@CountryID", value = Convert.ToInt32(1) });
                            _lstparm.Add(new ParmList() { name = "@StateID", value = Convert.ToInt32(sid) });
                            _lstparm.Add(new ParmList() { name = "@CityID", value = Convert.ToInt32(cid) });
                            _lstparm.Add(new ParmList() { name = "@CityName", value = cname.ToString() });
                            _lstparm.Add(new ParmList() { name = "@ZIP", value = "" });
                            _lstparm.Add(new ParmList() { name = "@MemberType", value = newmmname.ToString() });
                            _lstparm.Add(new ParmList() { name = "@MemberTypeID", value = Convert.ToInt32(newmmid) });
                            _lstparm.Add(new ParmList() { name = "@ParentMsrNo", value = Convert.ToInt32(dt.Rows[0]["Msrno"].ToString()) });
                            _lstparm.Add(new ParmList() { name = "@memberImage", value = "" });
                            _lstparm.Add(new ParmList() { name = "@aadhar", value = "" });
                            _lstparm.Add(new ParmList() { name = "@pan", value = "" });
                            _lstparm.Add(new ParmList() { name = "@companypan", value = "" });
                            _lstparm.Add(new ParmList() { name = "@gstno", value = "" });
                            _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                            dt = cls.select_data_dtNew("ProcMLM_AddNewMemberMaster ", _lstparm);
                            if (dt.Rows.Count > 0)
                            {
                                List<ParmList> _lstparmss = new List<ParmList>();
                                _lstparmss.Add(new ParmList() { name = "@ID", value = 2 });
                                _lstparmss.Add(new ParmList() { name = "@Action", value = "GetAll" });
                                DataTable dtCompany = cls.select_data_dtNew("Proc_ManageCompany ", _lstparmss);
                                if (dtCompany.Rows.Count > 0)
                                {
                                    string CompanyName = dtCompany.Rows[0]["CompanyName"].ToString();
                                    string WebSiteURL = dtCompany.Rows[0]["Website"].ToString() + "/userlogin.aspx";
                                    string[] valueArray = new string[6];
                                    valueArray[0] = firstname + " " + lastname;
                                    valueArray[1] = CompanyName;
                                    valueArray[2] = NewMemberID;
                                    valueArray[3] = Password.ToString();
                                    valueArray[4] = Pin.ToString();
                                    valueArray[5] = WebSiteURL;
                                 //   SMS.SendWithVar(mobile, 26, valueArray, 1);
                                    DLTSMS.SendWithVar(mobile, 2, valueArray, 1);
                                    Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"SUCCESS\", \"RESP_MSG\": \"Success|Record inserted successfully\"}]}");
                                }
                            }
                            else
                            {
                                Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"Fail\", \"RESP_MSG\": \"Member Already Exists !\"}]}");

                            }


                        }

                    }
                    else
                    {
                        string output = ConvertDataTabletoString(dt);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }

                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "newsignup")
            {
                #region newsignup
                if (Request.Form["Name"] != null && Request.Form["mobile"] != null && Request.Form["address"] != null && Request.Form["email"] != null)
                {

                    string Name = ReplaceCode(Request.Form["Name"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    string address = ReplaceCode(Request.Form["address"].ToString().Trim());
                    string email = ReplaceCode(Request.Form["email"].ToString().Trim());
                    DataTable dt = new DataTable();
                    cls_myMember clsm = new cls_myMember();
                    string str = clsm.Cyrus_GetTransactionID_New(); ;
                    FlexiMail objSendMail = new FlexiMail();
                    objSendMail.To = Convert.ToString(ConfigurationManager.AppSettings["mailTo"]);
                    objSendMail.CC = "";
                    objSendMail.BCC = "";
                    objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                    objSendMail.FromName = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                    objSendMail.MailBodyManualSupply = true;
                    objSendMail.Subject = "New Signup";
                    objSendMail.MailBody = "Name;" + Name + " " + "mobile" + mobile + " " + "address" + address + " " + "email" + email;
                    objSendMail.Send();
                    dt = cls.select_data_dt("Select 1 as ResponseCode,'Registered Successfully.' as ResponseStatus,'" + str + "' as ReferenceNumber");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");

                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            #endregion

            #region Wallet
            if (OperationName == "bal")
            {
                #region bal
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'bal','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "ewtran")
            {
                #region ewtran
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'ewtran','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "cpass")
            {
                #region cpass
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["oldpass"] != null && Request.Form["newpass"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string oldpass = ReplaceCode(Request.Form["oldpass"].ToString().Trim());
                    string newpass = ReplaceCode(Request.Form["newpass"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec App_getDetails 'cpass','" + mcode + "','" + loginip + "','" + deviceid + "','" + oldpass + "',0,0,'" + newpass + "'");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            #endregion

            #region MemberPlans
            if (OperationName == "checkrtid")
            {
                #region checkrtid
                if (Request.Form["memberid"] != null && Request.Form["membertypeid"] != null)
                {
                    string memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());

                    int membertypeid = Convert.ToInt32(Request.Form["membertypeid"].ToString());
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select msrno from tblmlm_membermaster where memberid='" + memberid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        int msrno = Convert.ToInt32(dt.Rows[0]["Msrno"]);
                        DataTable dtm = new DataTable();
                        dtm = cls.select_data_dt("select isnull(sum(Remaningcount),0) as Remaningcount from tblmlm_memberplans_adminapprove where MsrNo='" + msrno + "' and membertype='" + membertypeid + "' and isactive=1");
                        if (dtm.Rows.Count > 0)
                        {
                            string output = ConvertDataTabletoString(dtm);
                            Response.Write("{ " + OperationName + ":" + output + "}");
                        }
                        else
                        {
                            ReturnError("Id not exists", "Unknown");
                        }
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }

            if (OperationName == "checkplanfees")
            {
                #region checkplanfees
                if (Request.Form["msrno"] != null && Request.Form["targetmembertype"] != null && Request.Form["MemberTypeId"] != null)
                {
                    int targetmembertype = Convert.ToInt32(Request.Form["targetmembertype"].ToString());
                    int msrno = Convert.ToInt32(Request.Form["msrno"].ToString());
                    int membertypeid = Convert.ToInt32(Request.Form["MemberTypeId"].ToString());
                    DataTable dd = new DataTable();

                    dd = cls.select_data_dt("select Netamount from tblmlm_memberplan_settings_mm where membertype='" + membertypeid + "' and targetmembertype='" + targetmembertype + "' and msrno='" + msrno + "'");
                    if (dd.Rows.Count > 0)
                    {
                        string output = ConvertDataTabletoString(dd);
                        Response.Write("{ " + OperationName + ":" + output + "}");

                    }
                    else
                    {
                        DataTable ddd = new DataTable();
                        ddd = cls.select_data_dt("select Netamount from tblmlm_memberplan_settings where membertype='" + membertypeid + "' and targetmembertype='" + targetmembertype + "'");
                        if (ddd.Rows.Count > 0)
                        {
                            string output = ConvertDataTabletoString(ddd);
                            Response.Write("{ " + OperationName + ":" + output + "}");
                        }
                        else
                        {
                            Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"SUCCESS\", \"MSG\": \"fees not set\"}]}");
                        }

                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "purchaseplans")
            {
                #region purchaseplans
                if (Request.Form["msrno"] != null && Request.Form["memberid"] != null && Request.Form["MemberTypeId"] != null && Request.Form["totalamount"] != null && Request.Form["noofid"] != null)
                {
                    string memberid = Request.Form["memberid"].ToString();
                    int targetmembertype = Convert.ToInt32(Request.Form["targetmembertype"].ToString());
                    int msrno = Convert.ToInt32(Request.Form["msrno"].ToString());
                    int noofid = Convert.ToInt32(Request.Form["noofid"].ToString());
                    int membertypeid = Convert.ToInt32(Request.Form["MemberTypeId"].ToString());
                    decimal amount = Convert.ToDecimal(Request.Form["totalamount"].ToString());
                    DataTable dd = new DataTable();

                    dd = cls.select_data_dt("select ParentMsrNo from tblmlm_membermaster  where msrno='" + msrno + "'");
                    if (dd.Rows.Count > 0)
                    {
                        int parentmsrno = Convert.ToInt32(dd.Rows[0]["ParentMsrNo"]);

                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(amount, msrno);
                        if (result > 0)
                        {
                            string TxnID = clsm.Cyrus_GetTransactionID_New();
                            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal("-" + amount), "Dr", "Member Plan Purchase TxnID:-" + TxnID);
                            cls.insert_data("insert into [tblmlm_memberplans_adminreq](membertype,idpurchase,amount,TranId,requestbymsrno,adminmsrno,RequestDate,ActiveStatus)values('" + targetmembertype + "','" + noofid + "','" + amount + "','" + TxnID + "','" + msrno + "',1 , '" + DateTime.Now + "','Pending')");
                            Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"SUCCESS\", \"MSG\": \"Request Sent Successfully\"}]}");
                        }
                        else
                        {
                            Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"Fail\", \"MSG\": \"Insufficient Wallet Balance\"}]}");

                        }


                    }
                    else
                    {
                        ReturnError("Invalid Request Format", "Unknown");
                    }


                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            if (OperationName == "getpurchaselist")
            {
                #region status
                if (Request.Form["msrno"] != null)
                {
                    int msrno = Convert.ToInt32(Request.Form["msrno"].ToString());
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select tblmlm_membership.membertype as membertype,tblmlm_memberplans_adminreq.membertype as mm,requestbymsrno,ReqId,adminmsrno,Convert(varchar,RequestDate,0) as RequestDate,Convert(varchar,ActiveDate,0) as ActiveDate,TranId,ActiveStatus,amount,idpurchase,MemberId,FirstName +LastName as MemberName from tblmlm_memberplans_adminreq inner join  tblmlm_membermaster on tblmlm_membermaster.MsrNo=tblmlm_memberplans_adminreq.RequestbyMsrno inner join  tblmlm_membership on tblmlm_membership.membertypeid=tblmlm_memberplans_adminreq.membertype where requestbymsrno='" + msrno + "'");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ " + OperationName + ":" + output + "}");
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            #endregion




            #region XpressPayout
            if (OperationName == "otpsendxpresspayout")
            {
                #region PAyoutDMR
                if (Request.Form["Amount"] != null && Request.Form["Mobile"] != null && Request.Form["MemberId"] != null)
                {
                    string amount = ReplaceCode(Request.Form["Amount"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["Mobile"].ToString().Trim());
                    string Memberid = ReplaceCode(Request.Form["MemberId"].ToString().Trim());
                    cls_connection Cls = new cls_connection();
                    DataTable dtmember = cls.select_data_dt("select * from tblmlm_membermaster where MemberId='" + Memberid + "'");
                    if (dtmember.Rows.Count > 0)
                    {
                        DataTable sdt = Cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='payout', @msrno=" + dtmember.Rows[0]["MsrNo"] + "");
                        if (sdt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(sdt.Rows[0]["isxpresspayout"]) == false)
                            {
                                ReturnError("Aeps Payout Service Inactive", "Unknown");
                            }
                            else
                            {
                                DataTable dtm = new DataTable();
                                dtm = Cls.select_data_dt("select * from tblmlm_membermaster where msrno=" + dtmember.Rows[0]["MsrNo"] + " and isbankps=1");
                                if (dtm.Rows.Count > 0)
                                {


                                    DataTable dt = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(dtmember.Rows[0]["MsrNo"]) + ",@action='sum'");
                                    if (dt.Rows.Count > 0)
                                    {
                                        string usbal = dt.Rows[0]["usebal"].ToString();
                                        double NetAmount = XpressTotupAmount(Convert.ToDouble(amount), dtmember.Rows[0]["MemberId"].ToString());
                                        if (NetAmount > Convert.ToDouble(amount))
                                        {
                                            if (Convert.ToDecimal(usbal) >= Convert.ToDecimal(NetAmount))
                                            {
                                                string result = string.Empty;
                                              //  EzulixXpressPayOut EPayout = new EzulixXpressPayOut();
                                             //   result = EPayout.checkxpresspayout("100");
                                                result = "";
                                                if (result != string.Empty)
                                                {
                                                    DataSet ds = new DataSet();
                                                    ds = Deserialize(result);
                                                    if (ds.Tables.Count > 0)
                                                    {
                                                        if (ds.Tables.Contains("data"))
                                                        {
                                                            string isxpresspayout = ds.Tables["data"].Rows[0]["isxpresspayout"].ToString();
                                                            string Balance = ds.Tables["data"].Rows[0]["Balance"].ToString();
                                                            if (Convert.ToBoolean(isxpresspayout) == true && Convert.ToDecimal(Balance) > Convert.ToDecimal(amount))
                                                            {
                                                                Random random = new Random();
                                                                int SixDigit = random.Next(1000, 9999);
                                                                string[] valueArray = new string[2];
                                                                valueArray[0] = amount;
                                                                valueArray[1] = SixDigit.ToString();
                                                                SendWithVarpan(mobile, 1, valueArray);
                                                                Response.Write("{ " + OperationName + ":" + SixDigit.ToString() + "}");
                                                            }
                                                            else
                                                            {
                                                                ReturnError("Xpress Payout Service Inactive", "Unknown");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ReturnError("Xpress Payout Service Inactive", "Unknown");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ReturnError("Xpress Payout Service Inactive", "Unknown");
                                                    }
                                                }
                                                else
                                                {
                                                    ReturnError("Xpress Payout Service Inactive", "Unknown");
                                                }
                                            }
                                            else
                                            {
                                                ReturnError("Wallet balance is insufficent for this transcation !", "Unknown");
                                            }
                                        }
                                        else
                                        {
                                            ReturnError("Surcharge not define.", "Unknown");
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Wallet balance is insufficent for this transcation !", "Unknown");
                                    }
                                }
                                else
                                {
                                    ReturnError("Bank Details Not Updated Or Verified Contact to Your Admin", "Unknown");

                                }
                            }
                        }
                        else
                        {
                            ReturnError("Xpress Payout Service Inactive", "Unknown");
                        }
                    }

                    else
                    {
                        ReturnError("No Member Found", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            else if (OperationName == "traxpresspayout")
            {
                #region TransactionPayout
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["account"] != null && Request.Form["ifsc"] != null && Request.Form["amount"] != null && Request.Form["benename"] != null)
                {
                    //EzulixXpressPayOut EPayout = new EzulixXpressPayOut();
                    EzulixXpressPayOut EPayout = new EzulixXpressPayOut();
                    cls_connection Cls = new cls_connection();
                    cls_myMember Clsm = new cls_myMember();
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string amount = ReplaceCode(Request.Form["amount"].ToString().Trim());
                    string account = ReplaceCode(Request.Form["account"].ToString().Trim());
                    string ifsc = ReplaceCode(Request.Form["ifsc"].ToString().Trim());
                    string benename = ReplaceCode(Request.Form["benename"].ToString().Trim());
                    DataTable dst = cls.select_data_dt("Exec App_getDetails 'aepspt','" + mcode + "','" + loginip + "','" + deviceid + "'");
                    if (dst.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        DataTable sdt = Cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='payout', @msrno=" + dst.Rows[0]["MsrNo"] + "");
                        if (sdt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(sdt.Rows[0]["isxpresspayout"]) == false)
                            {
                                ReturnError("xpress Payout Service Inactive", "Unknown");
                            }
                            else
                            {
                                DataTable dt = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(dst.Rows[0]["MsrNo"]) + ",@action='sum'");
                                if (dt.Rows.Count > 0)
                                {
                                    string usbal = dt.Rows[0]["usebal"].ToString();
                                    if (Convert.ToDecimal(usbal) >= Convert.ToDecimal(amount))
                                    {
                                        string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                        timestamp = timestamp.Replace("-", "");
                                        timestamp = timestamp.Replace(":", "");
                                        timestamp = timestamp.Replace(".", "");
                                        timestamp = timestamp.Replace(" ", "");
                                        string orderId = "XPA" + timestamp;
                                        string beneficiaryAccount = account;
                                        string beneficiaryIFSC = ifsc;
                                        string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                                        string Result = string.Empty;
                                        double NetAmount = XpressTotupAmount(Convert.ToDouble(amount), dst.Rows[0]["MemberId"].ToString());
                                        if (NetAmount > Convert.ToDouble(amount))
                                        {
                                            string resulst = string.Empty;
                                            // resulst = EPayout.checkxpresspayout("100");
                                            resulst = "";
                                            if (resulst != string.Empty)
                                            {
                                                DataSet sds = new DataSet();
                                                sds = Deserialize(resulst);
                                                if (sds.Tables.Count > 0)
                                                {
                                                    if (sds.Tables.Contains("data"))
                                                    {
                                                        string isxpresspayout = sds.Tables["data"].Rows[0]["isxpresspayout"].ToString();
                                                        string Balance = sds.Tables["data"].Rows[0]["Balance"].ToString();
                                                        if (Convert.ToBoolean(isxpresspayout) == true && Convert.ToDecimal(Balance) > Convert.ToDecimal(amount))
                                                        {
                                                            int tra = Clsm.AEPSWallet_MakeTransaction_Ezulix(dst.Rows[0]["MemberId"].ToString(), Convert.ToDecimal("-" + NetAmount.ToString().Trim()), "Dr", "Xpress AEPS Payoyut Topup Txn:-" + orderId + "");
                                                            if (tra > 0)
                                                            {
                                                                List<ParmList> _lstparm = new List<ParmList>();
                                                                _lstparm.Add(new ParmList() { name = "@MemberId", value = dst.Rows[0]["MemberId"].ToString() });
                                                                _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(dst.Rows[0]["MsrNo"]) });
                                                                _lstparm.Add(new ParmList() { name = "@AgentOrderId", value = orderId });
                                                                _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(amount) });
                                                                _lstparm.Add(new ParmList() { name = "@beneficiaryAccount", value = beneficiaryAccount });
                                                                _lstparm.Add(new ParmList() { name = "@beneficiaryIFSC", value = beneficiaryIFSC });
                                                                _lstparm.Add(new ParmList() { name = "@status", value = "Pending" });
                                                                _lstparm.Add(new ParmList() { name = "@statusCode", value = "00" });
                                                                _lstparm.Add(new ParmList() { name = "@statusMessage", value = "Pending" });
                                                                _lstparm.Add(new ParmList() { name = "@NetAmount", value = Convert.ToDecimal(NetAmount) });
                                                                _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                                                                _lstparm.Add(new ParmList() { name = "@Typetra", value = "XP" });
                                                                _lstparm.Add(new ParmList() { name = "@BeneName", value = benename });
                                                                Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer", _lstparm);
                                                                //  Result = EPayout.WithDrawal_Ammount(orderId, amount, beneficiaryAccount, beneficiaryIFSC, date, benename);
                                                                Result = "";
                                                                DataSet ds = Deserialize(Result);
                                                                List<ParmList> _lstparms = new List<ParmList>();
                                                                _lstparms.Add(new ParmList() { name = "@AgentOrderId", value = orderId });
                                                                _lstparms.Add(new ParmList() { name = "@status", value = ds.Tables[0].Rows[0]["status"].ToString() });
                                                                _lstparms.Add(new ParmList() { name = "@statusCode", value = ds.Tables[0].Rows[0]["statusCode"].ToString() });
                                                                _lstparms.Add(new ParmList() { name = "@statusMessage", value = ds.Tables[0].Rows[0]["statusMessage"].ToString() });
                                                                _lstparms.Add(new ParmList() { name = "@Action", value = "Ub" });
                                                                Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer", _lstparms);
                                                                if (ds.Tables[0].Rows[0]["status"].ToString() == "FAILURE")
                                                                {
                                                                    Clsm.AEPSWallet_MakeTransaction_Ezulix(dst.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "Fail Xpress AEPS Payoyut Topup Txn:-" + orderId + "");
                                                                }
                                                                Response.Write("{ " + OperationName + ":" + Result + "}");

                                                            }
                                                            else
                                                            {
                                                                ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ReturnError("Xpress Payout Service Inactive", "Unknown");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ReturnError("Xpress Payout Service Inactive", "Unknown");
                                                    }
                                                }
                                                else
                                                {
                                                    ReturnError("Xpress Payout Service Inactive", "Unknown");
                                                }
                                            }
                                            else
                                            {
                                                ReturnError("Xpress Payout Service Inactive", "Unknown");
                                            }
                                        }
                                        else
                                        {
                                            ReturnError("Error | Surcharge not define", OperationName);
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);

                                    }
                                }
                                else
                                {
                                    ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                }
                            }
                        }
                        else
                        {
                            ReturnError("Xpress Payout Service Inactive", "Unknown");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            #endregion









            else if (OperationName == "edgesignup")
            {
                #region edgesignup
                if (Request.Form["Firstname"] != null && Request.Form["businessname"] != null && Request.Form["lastname"] != null && Request.Form["email"] != null && Request.Form["mobile"] != null && Request.Form["state"] != null)
                {
                    string Firstname = ReplaceCode(Request.Form["Firstname"].ToString().Trim());
                    string businessname = ReplaceCode(Request.Form["businessname"].ToString().Trim());
                    string lastname = ReplaceCode(Request.Form["lastname"].ToString().Trim());
                    string email = ReplaceCode(Request.Form["email"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["mobile"].ToString().Trim());
                    string state = ReplaceCode(Request.Form["state"].ToString().Trim());
                    string cityid = ReplaceCode(Request.Form["cityid"].ToString().Trim());
                    string cityname = ReplaceCode(Request.Form["cityname"].ToString().Trim());
                    string zipcode = ReplaceCode(Request.Form["zipcode"].ToString().Trim());
                    string wanttobe = ReplaceCode(Request.Form["wanttobe"].ToString().Trim());
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
                        dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + Firstname + "','" + lastname + "','" + email + "','" + Function.changedatetommddyy(MDOB) + "','','" + password + "','" + transpassord + "','" + mobile + "','" + "" + "','" + "" + "','" + "" + "','" + "" + "','" + Convert.ToInt32(1) + "','" + Convert.ToInt32(state) + "','" + Convert.ToInt32(cityid) + "','" + cityname + "','" + zipcode + "','" + wanttobe + "','0', '" + hdfvalue + "', '0', '" + strimage + "'");
                        intresult = Convert.ToInt32(dtresult.Rows[0][0]);
                        if (intresult > 0)
                        {
                            DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,' Success|Your request has been sent to admin for approval. Concerning team will contact you soon.' as ResponseStatus");
                            string output = ConvertDataTabletoString(dt);
                            Response.Write("{ " + OperationName + ":" + output + "}");


                        }
                        else
                        {
                            ReturnError("Warning|Member Already Exists !!", "edgesignup");


                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("{ " + OperationName + ":" + ex.ToString() + "}");
                        //  ReturnError("Warning|Member Already Exists !!", "edgesignup");

                    }
                }
                #endregion
            }

        }
        else
        {
            ReturnError("Invalid Request Format", "Unknown");
        }
    }

    protected string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
        try
        {
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.UTF8.GetBytes(Data);
            req.ContentLength = sentData.Length;
            using (System.IO.Stream sendStream = req.GetRequestStream())
            {
                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();
            }
            System.Net.WebResponse res = req.GetResponse();
            System.IO.Stream ReceiveStream = res.GetResponseStream();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
            {
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);

                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    Out += str;
                    count = sr.Read(read, 0, 256);
                }
            }
        }
        catch (ArgumentException ex)
        {
            Out = string.Format("HTTP_ERROR :: The second HttpWebRequest object has raised an Argument Exception as 'Connection' Property is set to 'Close' :: {0}", ex.Message);
        }
        catch (WebException ex)
        {
            Out = string.Format("HTTP_ERROR :: WebException raised! :: {0}", ex.Message);
        }
        catch (Exception ex)
        {
            Out = string.Format("HTTP_ERROR :: Exception raised! :: {0}", ex.Message);
        }
        string myresponse = Out.ToString();

        return myresponse;
    }




    protected void ReturnSucess(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }



    private void RechargeDone(int HistoryID, int MsrNo, string memberid, int PackageID, string from, int OperatorID, int ProfileID, string amount, string number, string account, string TransID, string PromoTP, string discount, string PromoCode)
    {
        try
        {
            cls_myMember clsm = new cls_myMember();
            if (ValidateMemberRequest(MsrNo) == 0)
            {
                cls.insert_data("insert into tblrecharge_app values ('" + HistoryID.ToString() + "')");
                DataTable dtMemberMaster = cls.select_data_dt("select msrno,memberid,firstname + ' ' + lastname as Membername from tblMLM_MemberMaster where msrno='" + MsrNo.ToString() + "'");
                string Recharge_Result = clsm.Cyrus_RechargeProcess(HistoryID, Convert.ToString(ProfileID), account, dtMemberMaster);
                char Splitter = Convert.ToChar(",");
                string[] split = Recharge_Result.Split(Splitter);
                if (split[0] == "Recharge Successful !!")
                {
                    DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'" + TransID + "' as TransactionID,'Success' as Status,'" + split[2].ToString() + "' as ErrorCode,'" + split[3].ToString() + "' as OperatorRef,'" + DateTime.Now + "' as Date");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ Recharge:" + output + "}");
                }
                else if (split[0] == "Recharge Failed !!")
                {
                    DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'" + TransID + "' as TransactionID,'Failure' as Status,'" + split[2].ToString() + "' as ErrorCode,'" + split[3].ToString() + "' as OperatorRef,'" + DateTime.Now + "' as Date");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ Recharge:" + output + "}");
                }
                else if (split[0] == "Recharge Pending !!")
                {
                    DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'" + TransID + "' as TransactionID,'Requested' as Status,'" + split[2].ToString() + "' as ErrorCode,'" + split[3].ToString() + "' as OperatorRef,'" + DateTime.Now + "' as Date");
                    string output = ConvertDataTabletoString(dt);
                    Response.Write("{ Recharge:" + output + "}");
                }
            }
            else
            {
                DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'' as TransactionID,'Contact to Admin' as Status,'' as ErrorCode,'' as OperatorRef,'" + DateTime.Now + "' as Date");
                string output = ConvertDataTabletoString(dt);
                Response.Write("{ Recharge:" + output + "}");
            }
        }
        catch (Exception ex)
        {
            DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'" + TransID + "' as TransactionID,'Requested' as Status,'Operator Issue Found' as ErrorCode,'' as OperatorRef,'" + DateTime.Now + "' as Date");
            string output = ConvertDataTabletoString(dt);
            Response.Write("{ Recharge:" + output + "}");
        }
    }

    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }

    private string uploadFundRequestImage_profile(string myData, string FolderName)
    {
        string ff = DateTime.Now.Ticks + ".jpg";
        string filename = Server.MapPath("~/Uploads/FundRequest/Actual/") + ff;
        var fs = new BinaryWriter(new FileStream(filename, FileMode.Append, FileAccess.Write));
        fs.Write(Convert.FromBase64String(myData));
        fs.Close();
        return ff;
    }

    private string uploadbankpassbook(string myData)
    {
        string ff = DateTime.Now.Ticks + ".jpg";
        string filename = Server.MapPath("~/Uploads/AEPS/") + ff;
        var fs = new BinaryWriter(new FileStream(filename, FileMode.Append, FileAccess.Write));
        fs.Write(Convert.FromBase64String(myData));
        fs.Close();
        return ff;
    }

    protected void ReturnDMRSucess(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
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
    public static void SendWithVarpan(string Mobile, int Template, string[] ValueArray)
    {
        try
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string smsMessage = GetString(Template, ValueArray);
           string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=198296AFda5tMRgn5a854e41&route=4&sender=EZXDMT&DLT_TE_ID=1207160975824663033&mobiles=" + Mobile + "& message=" + smsMessage + "";
            // string baseurl = "http://www.panaceasms.in/new/api/api_http.php?username=ezulix&password=sky12345&senderid=SKYTLE&to=" + Mobile + "&text=" + smsMessage + "&route=Transaction&type=text";
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        catch (Exception ex)
        {

        }
    }
    protected int ChkRegLimit(int msrno, int newmmid)
    {
        int chk = 1;
        if (newmmid > 0)
        {
            int mtp = newmmid;
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("Exec GetMUSetting '" + msrno + "'");
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
                        newmmid = 0;
                        chk = 0;
                    }
                }
                else if (mtp == 5)
                {
                    if (RTcnt + 1 > RTlmt)
                    {
                        newmmid = 0;
                        chk = 0;
                    }
                }
                else if (mtp == 6)
                {
                    if (CTcnt + 1 > CTlmt)
                    {
                        newmmid = 0;
                        chk = 0;
                    }
                }
            }
            else
            {
                newmmid = 0;
            }
        }
        return chk;
    }
    public static string GetString(int Template, string[] ValueArray)
    {
        string fileData = arrTemplate[Template];
        if ((ValueArray == null))
        {
            return fileData;
        }
        else
        {
            for (int i = ValueArray.GetLowerBound(0); i <= ValueArray.GetUpperBound(0); i++)
            {
                fileData = fileData.Replace("@v" + i.ToString() + "@", (string)ValueArray[i]);
            }
            return fileData;
        }
    }

    private string uploadaepskyc(string myData)
    {
        cls_myMember clsm = new cls_myMember();
        string tt = clsm.Cyrus_GetTransactionID_New();
        string ff = tt + ".Png";
        string filename = Server.MapPath("~/Uploads/AEPS/") + ff;

        byte[] imageBytes = Convert.FromBase64String(myData);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

        ms.Write(imageBytes, 0, imageBytes.Length);
        System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        image.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
        return ff;

    }

    public static string[] arrTemplate = new string[]
    {
        "Zero",
        "@v1@ is your OTP to  access DMT Transaction for Rs. @v0@  and Never Share it with anyone. Bank Never calls to verify it."//1
    };
    #region Xpressdmrtopup

    #endregion


    #region UtilityFunctions
    protected int ValidateMemberRequest(int msrno)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Exec ValidateMember '" + msrno.ToString() + "'");
        int ss = Convert.ToInt32(dt.Rows[0][0]);
        return ss;
    }
    #region XpressDMRTOpup
    public double XpressDMRTotupAmount(double amount, string memberid)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            int msrNo = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + memberid + "'");
            DataTable dtMemberMaster = cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='chk', @msrno=" + Convert.ToInt32(msrNo) + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='xpressdmrsur',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
            if (dtsr.Rows.Count > 0)
            {
                surcharge_rate = Convert.ToDouble(dtsr.Rows[0]["surcharge"].ToString());
                isFlat = Convert.ToInt32(dtsr.Rows[0]["isflat"].ToString());
                if (surcharge_rate > 0)
                {
                    if (isFlat == 0)
                        surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
                    else
                        surcharge_amt = surcharge_rate;
                }
                NetAmount = amount + surcharge_amt;
            }
            else
            {
                NetAmount = 0;
            }
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }
    #endregion

    #region InstantpayDMRTopup
    public double TotupAmount(double amount, string memberid)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            int msrNo = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + memberid + "'");
            DataTable dtMemberMaster = cls.select_data_dt("select * from tblmlm_membermaster where msrno=" + msrNo);
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt("Select top 1 * from m_Ezulix_PayOut_Dmr_Surcharge where startval<=" + amount.ToString() + " and endval>=" + amount.ToString() + " and packageid='" + PackageID + "' order by id");
            if (dtsr.Rows.Count > 0)
            {
                surcharge_rate = Convert.ToDouble(dtsr.Rows[0]["surcharge"].ToString());
                isFlat = Convert.ToInt32(dtsr.Rows[0]["isflat"].ToString());
                if (surcharge_rate > 0)
                {
                    if (isFlat == 0)
                        surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
                    else
                        surcharge_amt = surcharge_rate;
                }
                //txttopupAmount_Charges.Text = surcharge_amt.ToString();
                NetAmount = amount + surcharge_amt;
            }
            else
            {
                NetAmount = 0;
            }
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }
    #endregion

    #region PayoutTopup
    public double PayoutTotupAmount(double amount, string memberid)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            int msrNo = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + memberid + "'");
            DataTable dtMemberMaster = cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='chk', @msrno=" + Convert.ToInt32(msrNo) + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='aepspayoutsur',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
            if (dtsr.Rows.Count > 0)
            {
                surcharge_rate = Convert.ToDouble(dtsr.Rows[0]["surcharge"].ToString());
                isFlat = Convert.ToInt32(dtsr.Rows[0]["isflat"].ToString());
                if (surcharge_rate > 0)
                {
                    if (isFlat == 0)
                        surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
                    else
                        surcharge_amt = surcharge_rate;
                }
                NetAmount = amount + surcharge_amt;
            }
            else
            {
                NetAmount = 0;
            }
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }
    #endregion
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

    #region SendForgotMailmsg
    public static void SendPinMail(string Token, string Email)
    {
        try
        {
            string[] valueArray = new string[1];
            valueArray[0] = ConfigurationManager.AppSettings["adminurl"].ToString() + "pinrset.aspx?utken=" + Token;
            FlexiMail objSendMail = new FlexiMail();
            objSendMail.To = Email;
            objSendMail.CC = "";
            objSendMail.BCC = "";
            objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
            objSendMail.FromName = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
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

    public static void SendForgetMail(string Token, string Email)
    {
        try
        {
            string[] valueArray = new string[1];
            valueArray[0] = ConfigurationManager.AppSettings["adminurl"].ToString() + "pwdrset.aspx?utken=" + Token;
            FlexiMail objSendMail = new FlexiMail();
            objSendMail.To = Email;
            objSendMail.CC = "";
            objSendMail.BCC = "";
            objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
            objSendMail.FromName = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
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
    #endregion
    #endregion


    public double XpressTotupAmount(double amount, string memberid)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            int msrNo = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + memberid + "'");
            DataTable dtMemberMaster = cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='chk', @msrno=" + Convert.ToInt32(msrNo) + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='xpresspayoutsur',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
            if (dtsr.Rows.Count > 0)
            {
                surcharge_rate = Convert.ToDouble(dtsr.Rows[0]["surcharge"].ToString());
                isFlat = Convert.ToInt32(dtsr.Rows[0]["isflat"].ToString());
                if (surcharge_rate > 0)
                {
                    if (isFlat == 0)
                        surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
                    else
                        surcharge_amt = surcharge_rate;
                }
                NetAmount = amount + surcharge_amt;
            }
            else
            {
                NetAmount = 0;
            }
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }


    #region PropertiesClass
    public class RequestKyc
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShopName { get; set; }
        public string PanNumber { get; set; }
        public string MobileNumber { get; set; }
        public string ParmamentState { get; set; }
        public string ParmamentCity { get; set; }
        public string ParmamentAddress { get; set; }
        public string ParmamentPin { get; set; }
        public string AddProofUrl { get; set; }
        public string AddProofNumber { get; set; }
        public string SelfDeclNumber { get; set; }
        public string SelfDeclUrl { get; set; }
        public int MsrNo { get; set; }
        public string MemberId { get; set; }
        public string AdminMemberId { get; set; }
    }

    public class MemberDetail
    {
        public string MemberId { get; set; }
    }
    #endregion

    #region HTTP_POST
    public static string HTTP_POST_KYC(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
        }
        catch (WebException e)
        {
            Out = "Error";
        }
        string myresponse = Out.ToString();
        return myresponse;
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
}