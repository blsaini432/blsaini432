using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web;
using System.Data;


public static class SMS
{
    static string[] arrTemplate = new string[] 
    {
        "Zero",
        "Your current balance is @v0@. Thanks",//1
        "Your Number @v0@ is successfully recharged with amount @v1@. TxID is @v2@. Thanks",//2
        "Recharge amount @v1@ for number @v0@ is failed. TxID is @v2@. Thanks",//3
        "Recharge amount @v1@ for number @v0@ is pending. TxID is @v2@. Thanks",//4
        "You have successfully transfer amount @v1@ to number @v0@. Thanks",//5
        "Your mobile no successfully changed. New mobile no is @v0@. Thanks",//6
        "Status of TxID @v0@ is @v1@ Thanks.",//7
        "Dear Member, Last recharges are @v0@. Thanks",//8--Done
        "Dear @v0@, Welcome to our Company, You have registered successfully, Now you can login.",//9
      //  "You have successfully credit Rs. @v0@, Now your total amount is Rs. @v1@. Thanks",//10
        "You have successfully credit Rs. @v0@ Now your total amount is Rs. @v0@ Thanks.Team Fritware",//10
        "Dear @v0@, Welcome to our Company, Your Student Registration successfully done, Login with userid:@v1@ and password:@v2@.",//11    
        "Dear Member, Welcome to our Company, Your password reset link is: @v0@ Thanks",//12
        "You have a new fund request of Rs. @v0@ from @v1@. Thanks",//13
        "Dear @v0@, Welcome to the Our Company, Your Id : @v1@, Password : @v2@. Thanks",//14
        "You have successfully credit Rs. @v1@, Now your total amount is Rs. @v0@. Thanks", //15
        "Dear Customer, Your Number @v0@ is successfully recharged with amount @v1@, TxID is @v2@, Current Bal @v3@. Thanks", //16
        "Invalid Request, Please try again.", //17
        "Dear Member, Welcome to our Company, Your Pin reset link is: @v0@ Thanks", //18
        "Your account has been debited with amount @v1@ by @v0@. Thanks",//19
        "You have successfully Debit with Rs. @v0@, Now your total amount is Rs. @v1@. Thanks",//20
        "Dear @v0@ , Your one time password is : @v1@",//21
        "Hello @v0@, I am using swipemoney.in Join to rechage fast and smoothly. Thanks by @v1@", //22
        "Congratulation! Your friend @v0@ successfully joined us. You are rewarded with @v1@ points.", //23
        "You have successfully credit Rs. @v0@ for redeem  @v1@ Points.", //24
        "Dear @v0@, Recahrge of Rs @v1@ on @v2@ has been refunded. Your current balance is @v3@.", //25
        "Dear @v0@ , Welcome to @v1@ You have registered sucessfully, your ID-@v2@, Password-@v3@, MPin-@v4@ Now you can login on @v5@",//26
        "Dear @v0@ , Your Coupon code @v1@ has been activated sucessfully. Please check your cashback wallet. Team @v2@",//27
        "Dear Member, Last Wallet Transaction are @v0@. Thanks",//28--Done
        "Dear Member, Your complain submitted successfully.",//29--Done
        "Account @v1@ has been credited with Rs @v0@ TXNID @v2@ IMPS Ref @v3@. In case of transaction failure you will get OTP and transaction status via SMS for Refund Process.",//30
        "Dear @v0@, You have successfully book a bus With PNR No : @v1@, DOJ : @v2@. Thanks ",//31
        "You have successfully Book Flight From @v0@ To  @v1@ .Your PNR No. Is -@v2@. Thanks ", //32
        "You need an OTP for add fund of Rs.@v0@ to @v1@. THE OTP is @v2@.",//33
        "You need an OTP for deduct fund of Rs.@v0@ from @v1@. THE OTP is @v2@.",//34
        "the member @v0@ has been credited with amount of Rs.@v1@.",//35
        "the member @v0@ has been debited with amount of Rs.@v1@.",//36
         "Dear @v0@, Your password recover successfully, Login with user-id:@v1@ and Password:@v2@ and Transaction-Pin @v3@. Thanks",//37
         "You have a new AEPS BANK request of Rs. @v0@ from @v1@. Thanks"//38
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
            string baseurl = get_SMSBaseURL(Mobile, smsMessage, ApiID, "T");
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
            string baseurl = get_SMSBaseURL(Mobile, smsMessage, ApiID, "T");
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
            //fileData = HttpUtility.UrlEncode(fileData);
            return fileData;
        }
    }


    public static string get_SMSBaseURL(string Mobile, string smsMessage, int ApiID, string Route)
    {
        cls_connection cls = new cls_connection();
        DataTable dtAPI = new DataTable();

        dtAPI = cls.select_data_dt("Proc_Recharge_SMSApi 'getDataById'," + ApiID + "");
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