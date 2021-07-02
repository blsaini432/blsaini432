using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web;
using System.Data;
public static class DLTSMS
{
    static string[] arrTemplate = new string[]
    {
         "Zero",
        "Dear Bilal Ahamad , Your one time password is : @v1@ Team Payonclik",//1
       "Dear @v0@ , Welcome to Payonclik You have registered successfully, your username-@v2@ Password-@v3@  MPin-@v4@ Now you can login on http://www.payonclik.com",//2
       "You have successfully credit Rs. @v0@ , Now your total amount is Rs. @v1@ . Thanks Team Payonclik",//3
       "You have successfully Debit with Rs. @v0@ , Now your total amount is Rs. @v1@ . Thanks Team Payonclik",//4
       "You have a new fund request of Rs. @v0@  from @v1@ . Thanks Team Payonclik",//5
       "Your fund  request has been approved by Admin with Amount  Rs. @v0@  . Your current balance is Rs. @v1@   . Thanks!. Team Payonclik",//6
    };

    public static void SendWithVar(string Mobile, int Template, string[] ValueArray, int MsrNo)
    {
        try
        {
            if (Mobile.Length == 10)
            {
                Mobile = "91" + Mobile;
            }
            cls_connection cls = new cls_connection();
            int ApiID = cls.select_data_scalar_int("select SMSAPI from tblMLM_MemberMaster where MsrNo=" + MsrNo);
            cls.insert_data("exec ProcSMS_updateCount '" + Mobile + "'");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string smsMessage = GetString(Template, ValueArray);
            string baseurl = get_SMSBaseURL(Mobile, smsMessage, ApiID, "T", Template);
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
    public static void Send(string Mobile, string smsMessage, int MsrNo)
    {
        try
        {
            cls_connection cls = new cls_connection();
            int ApiID = cls.select_data_scalar_int("select SMSAPI from tblMLM_MemberMaster where MsrNo=" + MsrNo);
            cls.insert_data("exec ProcSMS_updateCount '" + Mobile + "'");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string baseurl = get_SMSBaseURL(Mobile, smsMessage, ApiID, "T",1);
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
    public static string GetString(int Template, string[] ValueArray)
    {
        string fileData = arrTemplate[Template];
        if ((ValueArray == null))
        {
            fileData = HttpUtility.UrlEncode(fileData);
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
    public static string get_SMSBaseURL(string Mobile, string smsMessage, int ApiID, string Route,int Template)
    {
        cls_connection cls = new cls_connection();
        DataTable dtAPI = new DataTable();
        DataTable DLTID = new DataTable();
        dtAPI = cls.select_data_dt("Proc_Recharge_SMSApi 'getDataById'," + ApiID + "");
        DLTID = cls.select_data_dt("Proc_DLT_TEMPLATE 'template'," + Template + "");
        string str = "";
        str = dtAPI.Rows[0]["URL"].ToString() + dtAPI.Rows[0]["prm1"].ToString() + "=" + dtAPI.Rows[0]["prm1val"].ToString() + "&";
        if (dtAPI.Rows[0]["prm2"].ToString() != "" && dtAPI.Rows[0]["prm2val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm2"].ToString() + "=" + dtAPI.Rows[0]["prm2val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm3"].ToString() != "" && dtAPI.Rows[0]["prm3val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm3"].ToString() + "=" + dtAPI.Rows[0]["prm3val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm4"].ToString() != "" && dtAPI.Rows[0]["prm4val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm4"].ToString() + "=" + dtAPI.Rows[0]["prm4val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm5"].ToString() != "" && dtAPI.Rows[0]["prm5val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm5"].ToString() + "=" + dtAPI.Rows[0]["prm5val"].ToString() + "&";
        }
        if (DLTID.Rows[0]["DLT_KEY"].ToString() != "" && DLTID.Rows[0]["DLT_ID"].ToString() != "")
        {
            str = str + DLTID.Rows[0]["DLT_KEY"].ToString() + "=" + DLTID.Rows[0]["DLT_ID"].ToString() + "&";
        }
       
        if (dtAPI.Rows[0]["prm6"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm6"].ToString() + "=" + Mobile + "&";
        }
        if (dtAPI.Rows[0]["prm7"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm7"].ToString() + "=" + smsMessage + "";
        }

        //str = str + "route=" + Route;

        return str;
    }
}