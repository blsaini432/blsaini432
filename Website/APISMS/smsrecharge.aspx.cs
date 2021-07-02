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

public partial class smsrecharge : System.Web.UI.Page
{
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();

    clsRecharge_History objHistory = new clsRecharge_History();

    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();

    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();

    clsRecharge_Profile objProfile = new clsRecharge_Profile();
    DataTable dtProfile = new DataTable();

    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();

    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();

    cls_connection objconnection = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if ((Request.QueryString["from"] != null && Request.QueryString["from"] != "") && (Request.QueryString["message"] != null && Request.QueryString["message"] != ""))
                {
                    string from = Request.QueryString["from"].Trim();
                    if (from.Length == 12)
                    {
                        from = from.Substring(2, 10);
                    }
                    string message = Request.QueryString["message"].Trim();
                    string[] messageArray = message.Split(' ');

                    DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where Mobile='" + from + "'");
                    if (dtMemberMaster.Rows.Count > 0)
                    {
                        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                        int PackageID = Convert.ToInt32(dtMemberMaster.Rows[0]["PackageID"]);
                        string memberid = Convert.ToString(dtMemberMaster.Rows[0]["MemberID"]);
                        dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", MsrNo);

                        if (Convert.ToBoolean(dtMemberMaster.Rows[0]["IsActive"]) == true)
                        {
                            if (messageArray[1].Trim().ToUpper() == "BAL")
                            {
                                // CY BAL
                                #region Balance
                                string[] valueArray = new string[1];
                                valueArray[0] = Convert.ToString(dtEWalletBalance.Rows[0]["Balance"]);
                                SMS.SendWithVar(from, 1, valueArray,1);
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "BALT")
                            {
                                // CY BALT 9983554400 100
                                #region Balance Transfer
                                if (messageArray.Length == 4)
                                {
                                    string amount = messageArray[3].Trim();
                                    if (amount != null && amount != "")
                                    {
                                        if (Convert.ToDecimal(amount) > Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                        {
                                            SMS.Send(from, "You have insufficient fund, please credit fund from MasterDistributor or company.",1);
                                        }
                                        else
                                        {
                                            string ToNumber = messageArray[2].Trim();
                                            dtMemberMaster.Clear();
                                            dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where Mobile='" + ToNumber + "'");
                                            if (dtMemberMaster.Rows.Count > 0)
                                            {
                                                string ToMemberID= Convert.ToString(dtMemberMaster.Rows[0]["MemberID"]);
                                                objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Transfer to Member - " + ToMemberID);
                                                objEWalletTransaction.EWalletTransaction(ToMemberID, Convert.ToDecimal(amount), "Cr", "Receive fund from Member - " + memberid);
                                                string[] valueArray = new string[2];
                                                valueArray[0] = ToNumber;
                                                valueArray[1] = amount;
                                                SMS.SendWithVar(from, 5, valueArray,1);

                                                DataTable dtmyWallet = new DataTable();
                                                dtmyWallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(dtMemberMaster.Rows[0]["msrno"].ToString()));
                                                string[] valueArray1 = new string[2];
                                                valueArray1[0] = dtmyWallet.Rows[0]["Balance"].ToString();
                                                valueArray1[1] = amount;
                                                SMS.SendWithVar(ToNumber, 15, valueArray1,1);
                                            }
                                            else
                                            {
                                                SMS.Send(from, "You have entered wrong mobile-no, please check the mobile-no and try again.",1);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        SMS.Send(from, "You have entered wrong amount, please check the amount and try again.",1);
                                    }
                                }
                                else
                                {
                                    SMS.Send(from, "You have entered wrong message, please check the message and try again.",1);
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "LAST")
                            {
                                // CY LAST
                                #region Last Recharges
                                DataTable dt = objconnection.select_data_dt("select top 5 * from tblRecharge_History where MsrNo=" + MsrNo + " and Status<>'Queued' order by HistoryID desc");
                                string str = "";
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    str = str + Convert.ToString(dt.Rows[i]["MobileNo"]) + " - " + Convert.ToString(dt.Rows[i]["RechargeAmount"]) + " - " + Convert.ToString(dt.Rows[i]["Status"]) + "<br>";
                                }
                                string[] valueArray = new string[1];
                                valueArray[0] = str;
                                SMS.SendWithVar(from, 8, valueArray,1);
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "LAST10")
                            {
                                // CY LAST
                                #region Last Recharges
                                DataTable dt = objconnection.select_data_dt("select top 10 * from tblRecharge_History where MsrNo=" + MsrNo + " and Status<>'Queued' order by HistoryID desc");
                                string str = "";
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    str = str + Convert.ToString(dt.Rows[i]["MobileNo"]) + " - " + Convert.ToString(dt.Rows[i]["RechargeAmount"]) + " - " + Convert.ToString(dt.Rows[i]["Status"]) + "<br>";
                                }
                                string[] valueArray = new string[1];
                                valueArray[0] = str;
                                SMS.SendWithVar(from, 8, valueArray,1);
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "CHANGE")
                            {
                                //CY CHANGE 9983554400
                                #region Change Mobile No
                                if (messageArray.Length == 3)
                                {
                                    string NewNumber = messageArray[2].Trim();
                                    if (NewNumber != null && NewNumber != "")
                                    {
                                        int i = objconnection.update_data("update tblMLM_MemberMaster set Mobile ='" + NewNumber + "' where MsrNo=" + MsrNo);
                                        string[] valueArray = new string[1];
                                        valueArray[0] = NewNumber;
                                        SMS.SendWithVar(from, 6, valueArray,1);
                                    }
                                    else
                                    {
                                        SMS.Send(from, "You have entered wrong mobile-no, please check the mobile-no and try again.",1);
                                    }
                                }
                                else
                                {
                                    SMS.Send(from, "You have entered wrong message, please check the message and try again.",1);
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "STATUS")
                            {
                                //CY STATUS B21454DD45
                                #region STATUS
                                if (messageArray.Length == 3)
                                {
                                    string TransID = messageArray[2].Trim();
                                    if (TransID != null && TransID != "")
                                    {
                                        DataTable dt = objconnection.select_data_dt("select * from tblRecharge_History where TransID='" + TransID + "'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            string[] valueArray = new string[2];
                                            valueArray[0] = TransID;
                                            valueArray[1] = Convert.ToString(dt.Rows[0]["Status"]);
                                            SMS.SendWithVar(from, 7, valueArray,1);
                                        }
                                        else
                                        {
                                            SMS.Send(from, "You have entered wrong TxID, please check the TxID and try again.",1);
                                        }
                                    }
                                    else
                                    {
                                        SMS.Send(from, "You have entered wrong TxID, please check the TxID and try again.",1);
                                    }
                                }
                                else
                                {
                                    SMS.Send(from, "You have entered wrong message, please check the message and try again.",1);
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "REG")
                            {
                                if (dtMemberMaster.Rows[0]["MembertypeID"].ToString() != "5")
                                {
                                    //CY STATUS B21454DD45
                                    #region REG
                                    if (messageArray.Length == 7)
                                    {
                                        string Member_Type = (messageArray[2].Trim().ToString().ToUpper() == "D" ? "4" : "5").ToString();
                                        string packageID = messageArray[3].Trim().ToString();
                                        string First_Name = messageArray[4].Trim().ToString();
                                        string last_Name = messageArray[5].Trim().ToString();
                                        string Mobile = messageArray[6].Trim().ToString();
                                        if (Member_Type == "4" && dtMemberMaster.Rows[0]["MembertypeID"].ToString() != "4")
                                        {
                                            SMS.Send(from, "Invalid Request, Please try again.",1);
                                            return;
                                        }
                                        if (Member_Type != null && Member_Type != "" && packageID != null && packageID != "" && First_Name != null && First_Name != "" && last_Name != null && last_Name != "" && Mobile != null && Mobile != "")
                                        {
                                            if (Mobile.Length != 10)
                                            {
                                                SMS.Send(from, "You have entered wrong mobile-no, please check the mobile-no and try again.",1);
                                                return;
                                            }
                                            Int32 intresult = 0; string Member_type_d = "";
                                            Random random = new Random();
                                            int SixDigit = random.Next(100000, 999999);
                                            string MemberID = "";
                                            if (Convert.ToInt32(Member_Type) == 4)
                                            {
                                                Member_type_d = "Distributor";
                                                MemberID = "DT" + SixDigit;
                                            }
                                            else if (Convert.ToInt32(Member_Type) == 5)
                                            {
                                                Member_type_d = "Retailer";
                                                MemberID = "RT" + SixDigit;
                                            }
                                            SixDigit = random.Next(111111, 999999);
                                            string fourdigit = random.Next(1111, 9999).ToString();
                                            DateTime DOJ = DateTime.Now;
                                            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
                                            intresult = objMemberMaster.AddEditMemberMaster(0, MemberID, First_Name,"", last_Name, "", DOJ, "", SixDigit.ToString(), fourdigit, Mobile, "", "", "", "", 1, Convert.ToInt32(dtMemberMaster.Rows[0]["Stateid"].ToString()), Convert.ToInt32(dtMemberMaster.Rows[0]["CityID"].ToString()), dtMemberMaster.Rows[0]["CityName"].ToString(), dtMemberMaster.Rows[0]["ZIP"].ToString(), Member_type_d, Convert.ToInt32(Member_Type), Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]), Convert.ToInt32(packageID),"");
                                            if (intresult > 0)
                                            {
                                                string[] valueArray = new string[3];
                                                valueArray[0] = First_Name + " " + last_Name;
                                                valueArray[1] = MemberID;
                                                valueArray[2] = SixDigit.ToString();
                                                SMS.SendWithVar(Mobile, 14, valueArray,1);
                                            }
                                            else
                                            {
                                                SMS.Send(from, "System Error, Please try again later.",1);
                                            }
                                        }
                                        else
                                        {
                                            SMS.Send(from, "Invalid Request, Please try again.",1);
                                        }
                                    }
                                    else
                                    {
                                        SMS.Send(from, "Invalid Request, Please try again.",1);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    SMS.Send(from, "You are not a valid user.",1);
                                }
                            }
                            else
                            {
                                // CY OPERATORCODE ProfileCODE NUMBER AMOUNT RCTP TT 1
                                #region Recharge
                                if (messageArray.Length >= 4)
                                {
                                    int smshistoryID = objconnection.select_data_scalar_int("insert into SMS_Recharge_History values ('" + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "','" + from + "','" + message + "',0); Select scope_identity();");
                                    string amount = messageArray[3].Trim();
                                    if (amount != null && amount != "")
                                    {
                                        string number = messageArray[2].Trim();
                                        if (number != null && number != "")
                                        {
                                            string OperatorCode = messageArray[1].Trim();
                                            if (OperatorCode != null && OperatorCode != "")
                                            {
                                                string Trantype = ""; string mycirclecode = "";
                                                if (messageArray.Length > 4)
                                                {
                                                    if (messageArray[4].Trim().ToLower() == "dth")
                                                    { Trantype = "rcdth"; mycirclecode = "Talktime"; }
                                                    else
                                                    {
                                                        Trantype = "rc";
                                                        if (messageArray.Length >= 6)
                                                        {
                                                            if (messageArray[5].Trim().ToLower() == "sr")
                                                                mycirclecode = "SpecialRecharge";
                                                            else if (messageArray[5].Trim().ToLower() == "vl")
                                                                mycirclecode = "Validity";
                                                            else if (messageArray[5].Trim().ToLower() == "tt")
                                                                mycirclecode = "TalkTime";
                                                        }
                                                        else
                                                        {
                                                            mycirclecode = "Talktime";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Trantype = "rc";
                                                    if (messageArray.Length >= 6)
                                                    {
                                                        if (messageArray[5].Trim().ToLower() == "sr")
                                                            mycirclecode = "SpecialRecharge";
                                                        else if (messageArray[5].Trim().ToLower() == "vl")
                                                            mycirclecode = "Validity";
                                                        else if (messageArray[5].Trim().ToLower() == "tt")
                                                            mycirclecode = "TalkTime";
                                                    }
                                                    else
                                                    {
                                                        mycirclecode = "Talktime";
                                                    }
                                                }
                                                int ProfileID = 0;
                                                if (messageArray.Length == 7)
                                                {
                                                    ProfileID = Convert.ToInt32(messageArray[6].Trim().ToLower());
                                                }
                                                else
                                                {
                                                    ProfileID = 19;
                                                }
                                                DataTable dtOperator = objconnection.select_data_dt("select OperatorID from tblRecharge_Operator where OperatorCode='" + OperatorCode + "'");
                                                int OperatorID = Convert.ToInt32(dtOperator.Rows[0]["OperatorID"]);
                                                //string ProfileCode = messageArray[2].Trim();
                                                //if (ProfileCode != null && ProfileCode != "")
                                                //{
                                                //DataTable dtProfile = objconnection.select_data_dt("select ProfileID from tblRecharge_Profile where ProfileCode='" + ProfileCode + "'");
                                                
                                                //if (dtProfile.Rows.Count > 0)
                                                //ProfileID = Convert.ToInt32(dtProfile.Rows[0]["ProfileID"]);
                                                if (Convert.ToDecimal(amount) > Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                                {
                                                    SMS.Send(from, "You have insufficient fund, please credit fund from MasterDistributor or company.",1);
                                                }
                                                else
                                                {
                                                    Random rnd = new Random();
                                                    Int64 month = rnd.Next(1000, 9999);
                                                    month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(1000, 9999)));
                                                    month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(1000, 9999)));
                                                    month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(1000, 9999)));
                                                    string TransID = Convert.ToString(month.ToString() + Convert.ToString(rnd.Next(1000, 9999)));
                                                    //string TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20).ToUpper();
                                                    //string TransID = Convert.ToString(month);
                                                    int i = objHistory.AddEditHistory(0, MsrNo, number, "", Convert.ToDecimal(amount), OperatorID, ProfileID, TransID, "", "", "Queued");
                                                    objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Recharge to " + number);
                                                    objconnection.update_data("Update SMS_Recharge_History set historyid='" + i.ToString() + "' where id='" + smshistoryID + "'");
                                                    RechargeDone(i, MsrNo, memberid, PackageID, from, OperatorID, ProfileID, amount, number, Trantype, TransID);
                                                }
                                                //}
                                                //else
                                                //{
                                                //    SMS.Send(from, "You have entered wrong ProfileCode, please check the ProfileCode and try again.");
                                                //}
                                            }
                                            else
                                            {
                                                SMS.Send(from, "You have entered wrong OperatorCode, please check the OperatorCode and try again.",1);
                                            }
                                        }
                                        else
                                        {
                                            SMS.Send(from, "You have entered wrong mobile-no, please check the mobile-no and try again.",1);
                                        }
                                    }
                                    else
                                    {
                                        SMS.Send(from, "You have entered wrong amount, please check the amount and try again.",1);
                                    }
                                }
                                else
                                {
                                    SMS.Send(from, "You have entered wrong message, please check the message and try again.",1);
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            SMS.Send(from, "Your account has been suspended, please contact to your MasterDistributor or company.",1);
                        }
                    }
                    else
                    {
                        SMS.Send(from, "You are not a valid user.",1);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    private void RechargeDone(int HistoryID, int MsrNo, string memberid, int PackageID, string from, int OperatorID, int ProfileID, string amount, string number, string account, string TransID)
    {
        try
        {
            //mom clsmom = new mom();
            DataTable dtHistory = new DataTable();
            dtHistory = objHistory.ManageHistory("Get", HistoryID);
            //DataTable dttAPI = objconnection.select_data_dt("select ActiveAPI from tblRecharge_Commission where OperatorID=" + OperatorID + "");//" and PackageID=" + PackageID);
            int APIID = Convert.ToInt32(dtHistory.Rows[0]["APIID"].ToString());//Convert.ToInt32(dttAPI.Rows[0]["ActiveAPI"]);
            DataTable dtOperatorCode = objconnection.select_data_dt("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + OperatorID + " and APIID=" + APIID);
            //DataTable dtProfileCode = objconnection.select_data_dt("select ProfileCode from tblRecharge_ProfileCode where ProfileID=" + ProfileID + "and APIID=" + APIID);
            string OperatorCode = Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"]);
            if (Convert.ToInt32(APIID) == 10)
                //OperatorCode = clsmom.MOM_operatorCode(Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"])).Trim();
                OperatorCode = "";
            //else
            //    OperatorCode = Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"]).Trim();

            string ProfileCode = Convert.ToString(ProfileID);
            //if (dtProfileCode.Rows.Count > 0)
            //ProfileCode = "";//Convert.ToString(dtProfileCode.Rows[0]["ProfileCode"]);

            if (APIID == 11)
            {
                if (account == "rc")
                    account = "MOBILE";
                else if (account == "rcdth")
                    account = "DTH";
                else if (account == "Landline")
                    account = "BILL";
            }

            if (APIID == 0)
            {
                DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Success", "", "", "");                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Bill Payment Successful !');location.replace('Recharge_ListHistory.aspx');", true);
            }
            else
            {
                //string TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
                dtAPI = objAPI.ManageAPI("Get", APIID);
                string str = "";

                //Append Code by ravi 13-08-2014
                if (dtAPI.Rows[0]["URL"].ToString() != "http://rechargeapi.click4mlm.com/api/recharge.aspx?")
                {
                    number = number.Replace("-", "");
                }
                //Append Code ends here by ravi

                str = dtAPI.Rows[0]["URL"].ToString() + dtAPI.Rows[0]["prm1"].ToString() + "=" + dtAPI.Rows[0]["prm1val"].ToString() + "&";
                if (dtAPI.Rows[0]["prm2"].ToString() != "" && dtAPI.Rows[0]["prm2val"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm2"].ToString() + "=" + dtAPI.Rows[0]["prm2val"].ToString() + "&";
                }
                if (dtAPI.Rows[0]["prm3"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm3"].ToString() + "=" + number + "&";
                }
                if (dtAPI.Rows[0]["prm4"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm4"].ToString() + "=" + OperatorCode + "&";
                }
                if (dtAPI.Rows[0]["prm5"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm5"].ToString() + "=" + ProfileCode + "&";
                }
                if (dtAPI.Rows[0]["prm6"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm6"].ToString() + "=" + amount + "&";
                }
                if (dtAPI.Rows[0]["prm7"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm7"].ToString() + "=" + account + "&";
                }
                if (dtAPI.Rows[0]["prm8"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm8"].ToString() + "=" + TransID + "&";
                }
                if (dtAPI.Rows[0]["prm9"].ToString() != "" && dtAPI.Rows[0]["prm9val"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm9"].ToString() + "=" + dtAPI.Rows[0]["prm9val"].ToString() + "&";
                }
                if (dtAPI.Rows[0]["prm10"].ToString() != "" && dtAPI.Rows[0]["prm10val"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm10"].ToString() + "=" + dtAPI.Rows[0]["prm10val"].ToString() + "&";
                }
                if (str.EndsWith("&"))
                    str = str.Substring(0, str.Length - 1);

                string Success = Convert.ToString(dtAPI.Rows[0]["Success"]);
                string Failed = Convert.ToString(dtAPI.Rows[0]["Failed"]);
                string Pending = Convert.ToString(dtAPI.Rows[0]["Pending"]);

                var TxID = "";
                var Status = "";
                var OperatorRef = "";
                var ErrorCode = "";

                string result = apicall(str);

                char Splitter = Convert.ToChar(dtAPI.Rows[0]["Splitter"]);
                string[] split = result.Split(Splitter);
                if (Convert.ToString(dtAPI.Rows[0]["TxIDPosition"]) != "")
                {
                    try
                    {
                        int TxIDPosition = Convert.ToInt32(dtAPI.Rows[0]["TxIDPosition"]);
                        TxID = split[TxIDPosition];
                    }
                    catch { }
                }
                if (Convert.ToString(dtAPI.Rows[0]["StatusPosition"]) != "")
                {
                    try
                    {
                        int StatusPosition = Convert.ToInt32(dtAPI.Rows[0]["StatusPosition"]);
                        Status = split[StatusPosition];
                    }
                    catch { }
                }
                if (Convert.ToString(dtAPI.Rows[0]["OperatorRefPosition"]) != "")
                {
                    try
                    {
                        int OperatorRefPosition = Convert.ToInt32(dtAPI.Rows[0]["OperatorRefPosition"]);
                        OperatorRef = split[OperatorRefPosition];
                    }
                    catch { }
                }
                if (Convert.ToString(dtAPI.Rows[0]["ErrorCodePosition"]) != "")
                {
                    try
                    {
                        int ErrorCodePosition = Convert.ToInt32(dtAPI.Rows[0]["ErrorCodePosition"]);
                        ErrorCode = split[ErrorCodePosition];
                    }
                    catch { }
                }


                if (Status.ToLower() == Success.ToLower())
                {
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, MsrNo, 0, 0, 0, 0, "", "", "Success", TxID, "", OperatorRef);
                    //Response.Write(TransID + ",Success," + ErrorCode + "," + OperatorRef + "," + DateTime.Now);
                    DataTable dtmybalance = new DataTable();
                    dtmybalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", MsrNo);
                    string[] valueArray = new string[4];
                    valueArray[0] = number;
                    valueArray[1] = amount;
                    valueArray[2] = TransID;
                    valueArray[3] = dtmybalance.Rows[0]["Balance"].ToString();
                    SMS.SendWithVar(from, 16, valueArray,1);
                }
                else if (Status.ToLower() == Failed.ToLower() || Status.ToLower() == "103" || Status.IndexOf("last 3 Hour") > 0)
                {
                    objEWalletTransaction.EWalletTransaction(memberid, Convert.ToDecimal(amount), "Cr", "Return amount");
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Failed", TxID, ErrorCode, OperatorRef);
                    //Response.Write(TransID + ",Failure,RQF," + OperatorRef + "," + DateTime.Now);
                    string[] valueArray = new string[3];
                    valueArray[0] = number;
                    valueArray[1] = amount;
                    valueArray[2] = TransID;
                    SMS.SendWithVar(from, 3, valueArray,1);
                }
                else if (Status.ToLower() == Pending.ToLower())
                {
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Pending", TxID, "", OperatorRef);
                    //Response.Write(TransID + ",Pending,PEN," + OperatorRef + "," + DateTime.Now);
                    string[] valueArray = new string[3];
                    valueArray[0] = number;
                    valueArray[1] = amount;
                    valueArray[2] = TransID;
                    SMS.SendWithVar(from, 4, valueArray,1);
                }
                else
                {
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Pending", TxID, "", OperatorRef);
                    //Response.Write(TransID + ",Pending,PEN," + OperatorRef + "," + DateTime.Now);
                    string[] valueArray = new string[3];
                    valueArray[0] = number;
                    valueArray[1] = amount;
                    valueArray[2] = TransID;
                    SMS.SendWithVar(from, 4, valueArray,1);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        httpreq.Timeout = 30000;
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        catch(Exception ex)
        {
            return "0";
        }
    }
}