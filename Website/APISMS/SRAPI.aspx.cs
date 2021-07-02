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

public partial class api_smsrecharge_A : System.Web.UI.Page
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
    cls_myMember clsm = new cls_myMember();
    cls_connection objconnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //objconnection.insert_data("insert into Recharge_skytele values ('" + Request.Url.ToString() + "','" + Request.ServerVariables["QUERY_STRING"].ToString() + "','','','','','','')");
                if ((Request.QueryString["msisdn"] != null && Request.QueryString["msisdn"] != "") && (Request.QueryString["msg"] != null && Request.QueryString["msg"] != ""))
                {
                    string from = Request.QueryString["msisdn"].Trim();
                    if (from.Length == 12)
                    {
                        from = from.Substring(2, 10);
                    }
                    string message = Request.QueryString["msg"].Trim();
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
                            if (messageArray[1].Trim().ToUpper() == "MBAL")
                            {
                                if (memberid == "100000")
                                {
                                    // CY BAL
                                    string DRmobile = messageArray[2].Trim();
                                    DataTable dtMemberMaster1 = new DataTable();
                                    dtMemberMaster1 = objconnection.select_data_dt("select * from tblMLM_MemberMaster where Mobile='" + DRmobile + "'");
                                    if (dtMemberMaster1.Rows.Count > 0)
                                    {
                                        #region Balance
                                        DataTable dtmyWallet = new DataTable();
                                        dtmyWallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(dtMemberMaster1.Rows[0]["Msrno"]));
                                        string[] valueArray = new string[1];
                                        valueArray[0] = dtMemberMaster1.Rows[0]["memberid"].ToString();
                                        valueArray[1] = Convert.ToString(dtEWalletBalance.Rows[0]["Balance"]);
                                        SMS.SendWithVar(from, 21, valueArray,1);
                                        #endregion
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
                            }
                            else if (messageArray[1].Trim().ToUpper() == "CPBAL")
                            {
                                // CY BALT 9983554400 100   anil with downline
                                #region Balance Deduct
                                if (messageArray.Length == 3)
                                {
                                    string ToNumber = messageArray[2].Trim();
                                    int tomsrn = objconnection.select_data_scalar_int("select msrno from tblmlm_membermaster where mobile='" + ToNumber + "' and isactive=1 and isdelete=0");
                                    int parentmsrno = objconnection.select_data_scalar_int("select parentmsrno from tblmlm_membermaster where msrno='" + tomsrn + "' and isactive=1 and isdelete=0");
                                    if (parentmsrno == MsrNo)
                                    {
                                        DataTable MemberdtEWalletBalance = new DataTable();
                                        DataTable dtMemberMaster1 = new DataTable();
                                        dtMemberMaster1 = objconnection.select_data_dt("select memberid from tblmlm_membermaster where mobile='" + ToNumber + "' and isactive=1 and isdelete=0");
                                        MemberdtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", tomsrn);
                                        string[] valueArray = new string[1];
                                        valueArray[0] = dtMemberMaster1.Rows[0]["memberid"].ToString();
                                        valueArray[1] = Convert.ToString(MemberdtEWalletBalance.Rows[0]["Balance"]);
                                        SMS.SendWithVar(from, 21, valueArray,1);
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
                            if (messageArray[1].Trim().ToUpper() == "BAL" || messageArray[1].Trim().ToUpper() == "CB")
                            {
                                // CY BAL
                                #region Balance
                                string[] valueArray = new string[1];
                                valueArray[0] = Convert.ToString(dtEWalletBalance.Rows[0]["Balance"]);
                                SMS.SendWithVar(from, 1, valueArray,1);
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "BALD")
                            {
                                // CY BALT 9983554400 100
                                #region Balance Transfer
                                if (messageArray.Length >= 4)
                                {
                                    string amount = messageArray[3].Trim();
                                    string tomobile = messageArray[2].Trim();
                                    int tomsrn = objconnection.select_data_scalar_int("select msrno from tblmlm_membermaster where mobile='" + tomobile + "' and isactive=1 and isdelete=0");
                                    if (amount != null && amount != "")
                                    {
                                        if (clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), tomsrn) == 0)
                                        {
                                            SMS.Send(from, "You have insufficient fund, please credit fund from MasterDistributor or company.", 1);
                                        }
                                        else
                                        {
                                            string ToNumber = messageArray[2].Trim();
                                            DataTable dtMemberMaster1 = new DataTable();
                                            dtMemberMaster1 = objconnection.select_data_dt("select a.* from tblMLM_MemberMaster as a,tblmlm_membertree as b where a.msrno=b.msrno and a.Mobile='" + ToNumber + "' and b.parentstr like '%," + dtMemberMaster.Rows[0]["Msrno"].ToString() + ",%'");

                                            if (dtMemberMaster1.Rows.Count > 0)
                                            {
                                                string MyFromMsrno = dtMemberMaster.Rows[0]["Msrno"].ToString();
                                                dtMemberMaster.Clear();
                                                dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where Mobile='" + ToNumber + "'");
                                                if (dtMemberMaster.Rows.Count > 0)
                                                {
                                                    DataTable dtmyWallet = new DataTable();
                                                    string ToMemberID = Convert.ToString(dtMemberMaster.Rows[0]["MemberID"]);
                                                    objEWalletTransaction.EWalletTransaction(ToMemberID, -Convert.ToDecimal(amount), "Dr", "Transfer to Member (Deduct fund) - " + ToMemberID);
                                                    objEWalletTransaction.EWalletTransaction(memberid, Convert.ToDecimal(amount), "Cr", "Receive fund from Member (Deduct fund) - " + memberid);
                                                    dtmyWallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(MyFromMsrno));
                                                    string[] valueArray = new string[2];
                                                    valueArray[0] = amount;
                                                    valueArray[1] = dtmyWallet.Rows[0]["Balance"].ToString();
                                                    SMS.SendWithVar(from, 15, valueArray, 1);

                                                    dtmyWallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(dtMemberMaster.Rows[0]["msrno"].ToString()));
                                                    string[] valueArray1 = new string[2];
                                                    valueArray1[0] = dtmyWallet.Rows[0]["Balance"].ToString();
                                                    valueArray1[1] = amount;
                                                    SMS.SendWithVar(ToNumber, 19, valueArray1, 1);
                                                }
                                                else
                                                {
                                                    SMS.Send(from, "You have entered wrong mobile-no, please check the mobile-no and try again.", 1);
                                                }
                                            }
                                            else
                                            {
                                                SMS.Send(from, "You have entered wrong mobile-no, please check the mobile-no and try again.", 1);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        SMS.Send(from, "You have entered wrong amount, please check the amount and try again.", 1);
                                    }
                                }
                                else
                                {
                                    SMS.Send(from, "You have entered wrong message, please check the message and try again.", 1);
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "TRF" || messageArray[1].Trim().ToUpper() == "BALT")
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
                                            DataTable dtMemberMaster1 = new DataTable();
                                            dtMemberMaster1 = objconnection.select_data_dt("select a.* from tblMLM_MemberMaster as a,tblmlm_membertree as b where a.msrno=b.msrno and a.Mobile='" + ToNumber + "' and b.parentstr like '%," + dtMemberMaster.Rows[0]["Msrno"].ToString() + ",%'");

                                            if (dtMemberMaster1.Rows.Count > 0)
                                            {
                                                string MyFromMsrno = dtMemberMaster.Rows[0]["Msrno"].ToString();
                                                dtMemberMaster.Clear();
                                                dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where Mobile='" + ToNumber + "'");
                                                if (dtMemberMaster.Rows.Count > 0)
                                                {
                                                    DataTable dtmyWallet = new DataTable();
                                                    string ToMemberID = Convert.ToString(dtMemberMaster.Rows[0]["MemberID"]);
                                                    objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Transfer to Member - " + ToMemberID);
                                                    objEWalletTransaction.EWalletTransaction(ToMemberID, Convert.ToDecimal(amount), "Cr", "Receive fund from Member - " + memberid);
                                                    dtmyWallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(MyFromMsrno));
                                                    string[] valueArray = new string[2];
                                                    valueArray[0] = amount;
                                                    valueArray[1] = dtmyWallet.Rows[0]["Balance"].ToString();
                                                    SMS.SendWithVar(from, 19, valueArray,1);

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
                            else if (messageArray[1].Trim().ToUpper() == "COMPL")
                            {
                                // CY LAST
                                #region Last Recharges of Particular Mobile
                                string RC_complain = messageArray[3].Trim().Replace("'", "").Replace("-", "").Replace("$", " ");
                                DataTable dt = new DataTable();
                                dt = objconnection.select_data_dt("Select Memberid + '(' + Firstname + ' ' + lastname  + ' - ' +mobile + ')' as MemID from tblmlm_membermaster where msrno='" + MsrNo + "'");
                                string DistName = dt.Rows[0]["MemID"].ToString();
                                string str = "";
                                try
                                {
                                    FlexiMail objSendMail = new FlexiMail();
                                    objSendMail.To = Convert.ToString(ConfigurationManager.AppSettings["mailTo"]);
                                    objSendMail.CC = "ezulixsoftware@gmail.com";
                                    objSendMail.BCC = str;
                                    objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                                    objSendMail.FromName = "Complain from Mobile App";
                                    objSendMail.MailBodyManualSupply = true;
                                    objSendMail.Subject = "Complain from Mobile App by " + messageArray[2].Trim().Replace("'", "").Replace("-", "").Replace("$", " ");
                                    objSendMail.MailBody = RC_complain.Replace("_", " ");
                                    objSendMail.Send();
                                }
                                catch (Exception ex)
                                { }
                                SMS.Send(from, "Dear Member, Your complain submitted successfully.", 1);
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "LAST")
                            {
                                // CY LAST
                                #region Last Recharges
                                string mobile = "";
                                if (messageArray.Length >= 2)
                                    mobile = messageArray[2].Trim().ToString();
                                string strs = "select top 5 ROW_NUMBER() OVER(ORDER BY MsrNo) AS SNo,tblRecharge_History.*,x.OperatorName,x.operatorcode from tblRecharge_History INNER JOIN tblRecharge_Operator as x ON x.operatorID=tblRecharge_History.operatorID where MsrNo=" + MsrNo + "";
                                //DataTable dt = objconnection.select_data_dt(" and Status<>'Queued' order by HistoryID desc");
                                if (mobile != "")
                                    strs = strs + " and mobileno='" + mobile + "'";
                                strs = strs + " and Status<>'Queued' order by HistoryID desc";
                                //DataTable dt = objconnection.select_data_dt("select top 5 * from tblRecharge_History where MsrNo=" + MsrNo + " and Status<>'Queued' order by HistoryID desc");
                                DataTable dt = objconnection.select_data_dt(strs);
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
                            else if (messageArray[1].Trim().ToUpper() == "EWTRAN")
                            {
                                // CY S EWTRAN 9782600666
                                #region E Wallet Transaction
                                DataTable dt = objEWalletTransaction.ManageEWalletTransaction("GetByMsrNoa", MsrNo);
                                string str = "";
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    str = str + Convert.ToString(dt.Rows[i]["Narration"]) + " - " + Convert.ToString(dt.Rows[i]["Amount"]) + " - " + Convert.ToString(dt.Rows[i]["Balance"]) + "<br>";
                                }
                                string[] valueArray = new string[1];
                                valueArray[0] = str;
                                SMS.SendWithVar(from, 28, valueArray, 1);
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
                                        int i = objconnection.update_data("update tblMLM_MemberMaster set Mobile ='" + NewNumber + "' where MsrNo=" + MsrNo + " and (select count(*) from tblmlm_membermaster where mobile='" + NewNumber + "')=0");
                                        if (i > 0)
                                        {
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
                                        SMS.Send(from, "You have entered wrong mobile-no, please check the mobile-no and try again.",1);
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
                                            intresult = objMemberMaster.AddEditMemberMaster(0, MemberID, First_Name, "",last_Name, "", DOJ, "", SixDigit.ToString(), fourdigit, Mobile, "", "", "", "", 1, Convert.ToInt32(dtMemberMaster.Rows[0]["Stateid"].ToString()), Convert.ToInt32(dtMemberMaster.Rows[0]["CityID"].ToString()), dtMemberMaster.Rows[0]["CityName"].ToString(), dtMemberMaster.Rows[0]["ZIP"].ToString(), Member_type_d, Convert.ToInt32(Member_Type), Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]), Convert.ToInt32(packageID),"");
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
                            else
                            {
                                // SKY OP_CODE MOBILE AMOUNT

                                #region Recharge
                                if (messageArray.Length >= 3)
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
                                                int ProfileID = 0;
                                                if (messageArray.Length > 4)
                                                {
                                                    ProfileID = Convert.ToInt32(messageArray[6].Trim().ToLower());
                                                }
                                                else
                                                {
                                                    ProfileID = 19;
                                                }
                                                DataTable dtOperator = objconnection.select_data_dt("select OperatorID from tblRecharge_Operator where OperatorCode='" + OperatorCode + "'");
                                                int OperatorID = Convert.ToInt32(dtOperator.Rows[0]["OperatorID"]);
                                                if (clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), MsrNo) == 1)
                                                {
                                                    string TransID = clsm.Cyrus_GetTransactionID_New();
                                                    int i = objHistory.AddEditHistory(0, MsrNo, number, "", Convert.ToDecimal(amount), OperatorID, ProfileID, TransID, "", "", "Queued");
                                                    objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Recharge to " + number + "  (Txn ID : " + TransID + ")");
                                                    objconnection.update_data("Update SMS_Recharge_History set historyid='" + i.ToString() + "' where id='" + smshistoryID + "'");
                                                    RechargeDone(i, MsrNo, memberid, PackageID, from, OperatorID, ProfileID, amount, number, Trantype, TransID);
                                                }
                                                else
                                                {
                                                    SMS.Send(from, "System Error, Please try again later.",1);
                                                }
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
            DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where Mobile='" + from + "'");
            string Recharge_Result = clsm.Cyrus_RechargeProcess(HistoryID, Convert.ToString(ProfileID), account, dtMemberMaster);
            char Splitter = Convert.ToChar(",");
            string[] split = Recharge_Result.Split(Splitter);
            if (split[0] == "Recharge Successful !!")
            {
                DataTable dtmybalance = new DataTable();
                dtmybalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", MsrNo);
                string[] valueArray = new string[4];
                valueArray[0] = number;
                valueArray[1] = amount;
                valueArray[2] = TransID;
                valueArray[3] = dtmybalance.Rows[0]["Balance"].ToString();
                SMS.SendWithVar(from, 16, valueArray,1);
            }
            else if (split[0] == "Recharge Failed !!")
            {
                string[] valueArray = new string[3];
                valueArray[0] = number;
                valueArray[1] = amount;
                valueArray[2] = TransID;
                SMS.SendWithVar(from, 3, valueArray,1);
            }
            //else if (split[0] == "Recharge Pending !!")
            //{
            //    string[] valueArray = new string[3];
            //    valueArray[0] = number;
            //    valueArray[1] = amount;
            //    valueArray[2] = TransID;
            //    SMS.SendWithVar(from, 4, valueArray);
            //}
        }
        catch (Exception ex)
        {
            string[] valueArray = new string[3];
            valueArray[0] = number;
            valueArray[1] = amount;
            valueArray[2] = TransID;
            SMS.SendWithVar(from, 4, valueArray,1);
        }
    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        catch (Exception ex)
        {
            return "0";
        }
    }
}