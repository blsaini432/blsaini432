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
public partial class api_LoginB2C : System.Web.UI.Page
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

    clsRecharge_Circle objCircle = new clsRecharge_Circle();

    cls_connection objconnection = new cls_connection();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if ((Request.QueryString["from"] != null && Request.QueryString["from"] != "") && (Request.QueryString["message"] != null && Request.QueryString["message"] != ""))
                {
                    string REQUEST_MOB_NUMBER = Request.QueryString["from"].Trim();
                    string from = Request.QueryString["from"].Trim();
                    if (from.Length == 12)
                    {
                        from = from.Substring(2, 10);
                    }
                    string message = Request.QueryString["message"].Trim();
                    string[] messageArray = message.Split(' ');

                    DataTable dtMemberMaster = new DataTable();
                    if (messageArray[1].Trim().ToUpper() == "LOGIN")
                        dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[2].Trim() + "'AND password='" + messageArray[3].Trim() + "' and membertypeID in (6)");
                    else if (messageArray[1].Trim().ToUpper() == "BAL" || messageArray[1].Trim().ToUpper() == "BALT" || messageArray[1].Trim().ToUpper() == "LAST" || messageArray[1].Trim().ToUpper() == "OPTRS" || messageArray[1].Trim().ToUpper() == "CIRCLE" || messageArray[1].Trim().ToUpper() == "CHANGE" || messageArray[1].Trim().ToUpper() == "STATUS" || messageArray[1].Trim().ToUpper() == "MADD")
                        dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[2].Trim() + "' and membertypeID in (6)");
                    else
                        dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[1].Trim() + "' and membertypeID in (6)");
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
                                Response.Write(valueArray[0].ToString() + ",");
                                //SMS.SendWithVar(from, 1, valueArray);
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "LOGIN")
                            {
                                // S LOGIN usr pwd
                                #region Login
                                string Authenticate = "1," + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "," + dtMemberMaster.Rows[0]["Mobile"].ToString() + "," + Convert.ToString(dtEWalletBalance.Rows[0]["Balance"]) + "," + dtMemberMaster.Rows[0]["TransactionPassword"].ToString() + ",";
                                Response.Write(Authenticate);
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "BALT")
                            {
                                // CY BALT 9983554400 100
                                #region Balance Transfer
                                if (messageArray.Length >= 4)
                                {
                                    string amount = messageArray[4].Trim();
                                    if (amount != null && amount != "")
                                    {
                                        if (Convert.ToDecimal(amount) > Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                        {
                                            SMS.Send(from, "You have insufficient fund, please credit fund from MasterDistributor or company.",0);
                                        }
                                        else
                                        {
                                            string ToNumber = messageArray[3].Trim();
                                            dtMemberMaster.Clear();
                                            dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where Mobile='" + ToNumber + "'");
                                            if (dtMemberMaster.Rows.Count > 0)
                                            {
                                                string ToMemberID = Convert.ToString(dtMemberMaster.Rows[0]["MemberID"]);
                                                objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Transfer to Member - " + ToMemberID);
                                                objEWalletTransaction.EWalletTransaction(ToMemberID, Convert.ToDecimal(amount), "Cr", "Receive fund from Member - " + memberid);
                                                string[] valueArray = new string[2];
                                                valueArray[0] = ToNumber;
                                                valueArray[1] = amount;
                                                Response.Write("1,SUCCESS");
                                            }
                                            else
                                            {
                                                Response.Write("0,Invalid Input.,");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("0,Invalid Amount.,");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Invalid Input.,");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "LAST")
                            {
                                // CY LAST
                                #region Last Recharges
                                DataTable dt = objconnection.select_data_dt("select top 15 ROW_NUMBER() OVER(ORDER BY MsrNo) AS SNo,* from tblRecharge_History where MsrNo=" + MsrNo + " and Status<>'Queued' order by HistoryID desc");
                                string str = "";
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    str = str + Convert.ToString(dt.Rows[i]["SNo"]) + ". " + Convert.ToString(dt.Rows[i]["MobileNo"]) + " - " + Convert.ToString(dt.Rows[i]["RechargeAmount"]) + " - " + Convert.ToString(dt.Rows[i]["Status"]) + " - " + Convert.ToString(dt.Rows[i]["TransID"]) + " - " + Convert.ToString(dt.Rows[i]["AddDate"]) + ",\n";
                                }
                                string[] valueArray = new string[1];
                                valueArray[0] = str;
                                //SMS.SendWithVar(from, 8, valueArray);
                                Response.Write(valueArray[0].ToString() + ",");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "OPTRS")
                            {
                                // CY LAST
                                #region Operator List
                                // DataTable dt = objOperator.ManageOperator("GetByServiceTypeID", Convert.ToInt32(messageArray[3].Trim().ToString()));
                                DataTable dt = objconnection.select_data_dt("Select OperatorCode,OperatorName from tblRecharge_Operator where ServiceTypeID='" + Convert.ToInt32(messageArray[3].Trim().ToString()) + "' and  IsActive='true' and  IsDelete='false' order by OperatorName asc"); //objOperator.ManageOperator("GetByServiceTypeID", Convert.ToInt32(messageArray[3].Trim().ToString()));
                                //string str = "";
                                //for (int i = 0; i < dt.Rows.Count; i++)
                                //{
                                //    str = str + Convert.ToString(dt.Rows[i]["OperatorID"]) + "-" + Convert.ToString(dt.Rows[i]["OperatorName"]) + "\n";
                                //}
                                //string[] valueArray = new string[1];
                                //valueArray[0] = str;
                                ////SMS.SendWithVar(from, 8, valueArray);
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ Country:" + output + "},");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "CIRCLE")
                            {
                                // CY LAST
                                #region Circle List
                                //DataTable dt = objconnection.select_data_dt("select cc.CircleID ,c.CircleName from tblRecharge_CircleCode as cc INNER JOIN tblRecharge_Circle AS c ON c.CircleID=cc.CircleID where cc.IsDelete=0 and cc.IsActive=1");//objCircle.ManageCircle("Get", 0);
                                DataTable dt = objCircle.ManageCircle("Get", 0);
                                //string str = "";
                                //for (int i = 0; i < dt.Rows.Count; i++)
                                //{
                                //    str = str + Convert.ToString(dt.Rows[i]["CircleID"]) + " - " + Convert.ToString(dt.Rows[i]["CircleName"]) + "\n";
                                //}
                                //string[] valueArray = new string[1];
                                //valueArray[0] = str;
                                //SMS.SendWithVar(from, 8, valueArray);
                                // Response.Write(valueArray[0].ToString() + ",");
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ Circle:" + output + "},");
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
                                        Response.Write("1," + valueArray[0].ToString() + ",");
                                    }
                                    else
                                    {
                                        Response.Write("0,Invalid Input !!,");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Invalid Request !!,");
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
                                            Response.Write("1," + valueArray[0].ToString() + "," + valueArray[1].ToString() + ",");
                                        }
                                        else
                                        {
                                            Response.Write("0,Invalid Transaction ID !!,Invalid Transaction ID !!,");
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("0,Invalid Transaction ID !!,Invalid Transaction ID !!,");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Invalid Input !!,Invalid Input !!,");
                                }
                                #endregion
                            }

                            else if (messageArray[1].Trim().ToUpper() == "MADD")
                            {
                                //CY Add 
                                #region Addvetisement
                                DataTable dt = objconnection.select_data_dt("select * from tblMAdd  WHERE IsActive='true' and  IsDelete='false' and IsDist=2");
                                if (dt.Rows.Count > 0)
                                {
                                    string[] valueArray = new string[2];
                                    valueArray[0] = Convert.ToString(dt.Rows[0]["MAddID"]);
                                    valueArray[1] = Convert.ToString(dt.Rows[0]["MAddImage"]);
                                    Response.Write("1," + valueArray[0].ToString() + "," + valueArray[1].ToString() + ",");
                                }
                                else
                                {
                                    Response.Write("0,Invalid Add ID !!");
                                }

                                #endregion
                            }
                            else
                            {
                                // CY OPERATORCODE ProfileCODE NUMBER AMOUNT
                                //S DT699101 REL tt 98xxxxxx90 10
                                #region Recharge
                                //if (messageArray.Length == 5)
                                //{
                                //    string amount = messageArray[3].Trim();
                                //    if (amount != null && amount != "")
                                //    {
                                //        string number = messageArray[2].Trim();
                                //        if (number != null && number != "")
                                //        {
                                //            string OperatorCode = messageArray[1].Trim();
                                //            if (OperatorCode != null && OperatorCode != "")
                                //            {
                                //                string Trantype = ""; string mycirclecode = "";
                                //                if (messageArray[4].Trim().ToLower() == "dth")
                                //                { Trantype = "rcdth"; mycirclecode = "Talktime"; }
                                //                else
                                //                {
                                //                    Trantype = "rc";
                                //                    if (messageArray.Length == 6)
                                //                    {
                                //                        if (messageArray[5].Trim().ToLower() == "sr")
                                //                            mycirclecode = "SpecialRecharge";
                                //                        else if (messageArray[5].Trim().ToLower() == "vl")
                                //                            mycirclecode = "Validity";
                                //                        else if (messageArray[5].Trim().ToLower() == "tt")
                                //                            mycirclecode = "TalkTime";
                                //                    }
                                //                    else
                                //                    {
                                //                        mycirclecode = "Talktime";
                                //                    }
                                //                }
                                //                DataTable dtOperator = objconnection.select_data_dt("select OperatorID from tblRecharge_Operator where OperatorCode='" + OperatorCode + "'");
                                //                int OperatorID = Convert.ToInt32(dtOperator.Rows[0]["OperatorID"]);
                                //                //string ProfileCode = messageArray[2].Trim();
                                //                //if (ProfileCode != null && ProfileCode != "")
                                //                //{
                                //                //DataTable dtProfile = objconnection.select_data_dt("select ProfileID from tblRecharge_Profile where ProfileCode='" + ProfileCode + "'");
                                //                int ProfileID = 0;
                                //                //if (dtProfile.Rows.Count > 0)
                                //                //ProfileID = Convert.ToInt32(dtProfile.Rows[0]["ProfileID"]);
                                //                if (Convert.ToDecimal(amount) > Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                //                {
                                //                    SMS.Send(from, "You have insufficient fund, please credit fund from MasterDistributor or company.");
                                //                }
                                //                else
                                //                {
                                //                    string TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
                                //                    int i = objHistory.AddEditHistory(0, MsrNo, number, "", Convert.ToDecimal(amount), OperatorID, ProfileID, TransID, "", "", "Queued");
                                //                    objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Recharge to " + number);

                                //                    RechargeDone(i, MsrNo, memberid, PackageID, from, OperatorID, ProfileID, amount, number, Trantype, TransID);
                                //                }
                                //            }
                                //            else
                                //            {
                                //                SMS.Send(from, "You have enterd wrong OperatorCode, please check the OperatorCode and try again.");
                                //            }
                                //        }
                                //        else
                                //        {
                                //            SMS.Send(from, "You have enterd wrong number, please check the number and try again.");
                                //        }
                                //    }
                                //    else
                                //    {
                                //        SMS.Send(from, "You have enterd wrong amount, please check the amount and try again.");
                                //    }
                                //}
                                //else
                                //{
                                //    SMS.Send(from, "You have entered wrong message, please check the message and try again.");
                                //}
                                #endregion
                                #region Recharge New
                                //if (messageArray.Length == 6)
                                //{
                                //    string amount = messageArray[5].Trim();
                                //    if (amount != null && amount != "")
                                //    {
                                //        string number = messageArray[4].Trim();
                                //        if (number != null && number != "")
                                //        {
                                //            string OperatorCode = messageArray[3].Trim();
                                //            if (OperatorCode != null && OperatorCode != "")
                                //            {
                                //                DataTable dtOperator = objconnection.select_data_dt("select OperatorID from tblRecharge_Operator where OperatorCode='" + OperatorCode + "'");
                                //                int OperatorID = Convert.ToInt32(dtOperator.Rows[0]["OperatorID"]);
                                //                string ProfileCode = messageArray[1].Trim();
                                //                if (ProfileCode != null && ProfileCode != "")
                                //                {
                                //                    DataTable dtProfile = objconnection.select_data_dt("select ProfileID from tblRecharge_Profile where ProfileName='" + ProfileCode + "'");
                                //                    int ProfileID = 0;
                                //                    if (dtProfile.Rows.Count > 0)
                                //                        ProfileID = Convert.ToInt32(dtProfile.Rows[0]["ProfileID"]);
                                //                    if (Convert.ToDecimal(amount) > Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                //                    {
                                //                        Response.Write("0,insufficient fund,");
                                //                        //SMS.Send(REQUEST_MOB_NUMBER, "You have insufficient fund, please credit fund from MasterDistributor or company.");
                                //                    }
                                //                    else
                                //                    {
                                //                        string TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
                                //                        int i = objHistory.AddEditHistory(0, MsrNo, number, "", Convert.ToDecimal(amount), OperatorID, ProfileID, TransID, "", "", "Queued");
                                //                        objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Recharge to " + number);
                                //                        RechargeDone(i, MsrNo, memberid, PackageID, REQUEST_MOB_NUMBER, OperatorID, ProfileID, amount, number, "", TransID);
                                //                        // Response.Write("1,Recharge Done,");
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    Response.Write("0,wrong ProfileCode,");
                                //                    //SMS.Send(REQUEST_MOB_NUMBER, "You have enterd wrong ProfileCode, please check the ProfileCode and try again.");
                                //                }
                                //            }
                                //            else
                                //            {
                                //                Response.Write("0,Wrong OperatorCode,");
                                //                //SMS.Send(REQUEST_MOB_NUMBER, "You have enterd wrong OperatorCode, please check the OperatorCode and try again.");
                                //            }
                                //        }
                                //        else
                                //        {
                                //            Response.Write("0,Wrong Number,");
                                //            // SMS.Send(REQUEST_MOB_NUMBER, "You have enterd wrong number, please check the number and try again.");
                                //        }
                                //    }
                                //    else
                                //    {
                                //        Response.Write("0,Wrong Amount,");
                                //        // SMS.Send(REQUEST_MOB_NUMBER, "You have enterd wrong amount, please check the amount and try again.");
                                //    }
                                //}
                                //else
                                //{
                                //    Response.Write("0,Wrong Message,");
                                //    // SMS.Send(REQUEST_MOB_NUMBER, "You have entered wrong message, please check the message and try again.");
                                //}
                                #endregion
                                #region recharge latest
                                // CY OPERATORCODE ProfileCODE NUMBER AMOUNT RCTP TT
                                //S DT699101 AT 7737251804 10 19
                                if (messageArray.Length >= 5)
                                {
                                    string amount = messageArray[4].Trim();
                                    if (amount != null && amount != "")
                                    {
                                        string number = messageArray[3].Trim();
                                        if (number != null && number != "")
                                        {
                                            string OperatorCode = messageArray[2].Trim();
                                            if (OperatorCode != null && OperatorCode != "")
                                            {
                                                string Trantype = ""; string mycirclecode = "";
                                                //if (messageArray[4].Trim().ToLower() == "dth")
                                                //{ Trantype = "rcdth"; mycirclecode = "Talktime"; }
                                                //else
                                                //{
                                                //    Trantype = "rc";
                                                //if (messageArray.Length == 6)
                                                //{
                                                //    if (messageArray[5].Trim().ToLower() == "sr")
                                                //        mycirclecode = "SpecialRecharge";
                                                //    else if (messageArray[5].Trim().ToLower() == "vl")
                                                //        mycirclecode = "Validity";
                                                //    else if (messageArray[5].Trim().ToLower() == "tt")
                                                //        mycirclecode = "TalkTime";
                                                //}
                                                //else
                                                //{
                                                //    mycirclecode = "Talktime";
                                                //}
                                                //}
                                                DataTable dtOperator = objconnection.select_data_dt("select OperatorID from tblRecharge_Operator where OperatorCode='" + OperatorCode + "'");
                                                int OperatorID = Convert.ToInt32(dtOperator.Rows[0]["OperatorID"]);
                                                //string ProfileCode = messageArray[2].Trim();
                                                //if (ProfileCode != null && ProfileCode != "")
                                                //{
                                                //DataTable dtProfile = objconnection.select_data_dt("select ProfileID from tblRecharge_Profile where ProfileCode='" + ProfileCode + "'");
                                                int ProfileID = 0;
                                                if (messageArray.Length >= 6)
                                                {
                                                    ProfileID = Convert.ToInt32(messageArray[5].Trim().ToLower());
                                                }
                                                else
                                                {
                                                    ProfileID = 19;
                                                }

                                                //if (dtProfile.Rows.Count > 0)
                                                //ProfileID = Convert.ToInt32(dtProfile.Rows[0]["ProfileID"]);
                                                if (Convert.ToDecimal(amount) > Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                                {
                                                    Response.Write("0,Insufficient Fund,");
                                                    //SMS.Send(from, "You have insufficient fund, please credit fund from MasterDistributor or company.");
                                                }
                                                else
                                                {
                                                    string TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
                                                    int i = objHistory.AddEditHistory(0, MsrNo, number, "", Convert.ToDecimal(amount), OperatorID, ProfileID, TransID, "", "", "Queued");
                                                    objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Recharge to " + number);
                                                    RechargeDone(i, MsrNo, memberid, PackageID, from, OperatorID, ProfileID, amount, number, Trantype, TransID);
                                                }
                                            }
                                            else
                                            {
                                                Response.Write("0,Wrong OperatorCode,");
                                                //SMS.Send(from, "You have enterd wrong OperatorCode, please check the OperatorCode and try again.");
                                            }
                                        }
                                        else
                                        {
                                            Response.Write("0,Wrong Number,");
                                            //SMS.Send(from, "You have enterd wrong number, please check the number and try again.");
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("0,Wrong Amount,");
                                        // SMS.Send(from, "You have enterd wrong amount, please check the amount and try again.");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Wrong Message,");
                                    //SMS.Send(from, "You have entered wrong message, please check the message and try again.");
                                }
                                #endregion


                            }
                        }
                        else
                        {
                            Response.Write("0,account suspended,");
                            //SMS.Send(from, "Your account has been suspended, please contact to your MasterDistributor or company.");
                        }
                    }
                    else
                    {
                        Response.Write("0,Invalid User,");
                        // SMS.Send(from, "You are not a valid user.");
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
            // mom clsmom = new mom();
            DataTable dtHistory = new DataTable();
            dtHistory = objHistory.ManageHistory("Get", HistoryID);
            //DataTable dttAPI = objconnection.select_data_dt("select ActiveAPI from tblRecharge_Commission where OperatorID=" + OperatorID + "");//" and PackageID=" + PackageID);
            int APIID = Convert.ToInt32(dtHistory.Rows[0]["APIID"].ToString());//Convert.ToInt32(dttAPI.Rows[0]["ActiveAPI"]);
            DataTable dtOperatorCode = objconnection.select_data_dt("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + OperatorID + " and APIID=" + APIID);
            //DataTable dtProfileCode = objconnection.select_data_dt("select ProfileCode from tblRecharge_ProfileCode where ProfileID=" + ProfileID + "and APIID=" + APIID);
            string OperatorCode = Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"]);
            ////if (Convert.ToInt32(APIID) == 10)
            ///    OperatorCode = clsmom.MOM_operatorCode(Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"])).Trim();
            //else
            //    OperatorCode = Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"]).Trim();

            string ProfileCode = "";
            //if (dtProfileCode.Rows.Count > 0)
            ProfileCode = Convert.ToString(ProfileID);
            if (APIID == 0)
            {
                DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Success", "", "", "");
                Response.Write("1,success,");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Bill Payment Successful !');location.replace('Recharge_ListHistory.aspx');", true);
            }
            else
            {
                //string TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
                dtAPI = objAPI.ManageAPI("Get", APIID);
                string str = "";

                //Append Code by ravi 13-08-2014
                if (dtAPI.Rows[0]["URL"].ToString() != "http://rechargeapi.click4mlm.com/api/login.aspx?")
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
                    Response.Write("1," + TransID + ",Success," + ErrorCode + "," + OperatorRef + "," + DateTime.Now + ",");
                    DataTable dtmybalance = new DataTable();
                    dtmybalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", MsrNo);
                    string[] valueArray = new string[4];
                    valueArray[0] = number;
                    valueArray[1] = amount;
                    valueArray[2] = TransID;
                    valueArray[3] = dtmybalance.Rows[0]["Balance"].ToString();
                    // SMS.SendWithVar(from, 2, valueArray);
                }
                else if (Status.ToLower() == Failed.ToLower() || Status.ToLower() == "103")
                {
                    objEWalletTransaction.EWalletTransaction(memberid, Convert.ToDecimal(amount), "Cr", "Return amount");
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Failed", TxID, ErrorCode, OperatorRef);
                    Response.Write("0," + TransID + ",Failure,RQF," + OperatorRef + "," + DateTime.Now + ",");
                    string[] valueArray = new string[3];
                    valueArray[0] = number;
                    valueArray[1] = amount;
                    valueArray[2] = TransID;
                    //  SMS.SendWithVar(from, 3, valueArray);
                }
                else if (Status.ToLower() == Pending.ToLower())
                {
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Pending", TxID, "", OperatorRef);
                    Response.Write("0," + TransID + ",Pending,PEN," + OperatorRef + "," + DateTime.Now + ",");
                    string[] valueArray = new string[3];
                    valueArray[0] = number;
                    valueArray[1] = amount;
                    valueArray[2] = TransID;
                    //SMS.SendWithVar(from, 4, valueArray);
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
    //private void RechargeDone(int HistoryID, int MsrNo, string memberid, int PackageID, string from, int OperatorID, int ProfileID, string amount, string number, string account, string TransID)
    //{
    //    try
    //    {
    //        DataTable dttAPI = objconnection.select_data_dt("select ActiveAPI from tblRecharge_Commission where OperatorID=" + OperatorID + " and PackageID=" + PackageID);
    //        int APIID = Convert.ToInt32(dttAPI.Rows[0]["ActiveAPI"]);
    //        DataTable dtOperatorCode = objconnection.select_data_dt("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + OperatorID + "and APIID=" + APIID);
    //        DataTable dtProfileCode = objconnection.select_data_dt("select ProfileCode from tblRecharge_ProfileCode where ProfileID=" + ProfileID + "and APIID=" + APIID);


    //        string OperatorCode = Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"]);
    //        string ProfileCode = "";

    //        if (dtProfileCode.Rows.Count > 0)

    //            ProfileCode = Convert.ToString(dtProfileCode.Rows[0]["ProfileCode"]);

    //        if (APIID == 0)
    //        {
    //            DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Success", "", "", "");
    //            Response.Write("1,Bill Payment Successful !,");
    //            // ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Bill Payment Successful !');location.replace('Recharge_ListHistory.aspx');", true);
    //        }
    //        else
    //        {
    //            //string TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
    //            dtAPI = objAPI.ManageAPI("Get", APIID);
    //            string str = "";
    //            str = dtAPI.Rows[0]["URL"].ToString() + dtAPI.Rows[0]["prm1"].ToString() + "=" + dtAPI.Rows[0]["prm1val"].ToString() + "&";
    //            if (dtAPI.Rows[0]["prm2"].ToString() != "" && dtAPI.Rows[0]["prm2val"].ToString() != "")
    //            {
    //                str = str + dtAPI.Rows[0]["prm2"].ToString() + "=" + dtAPI.Rows[0]["prm2val"].ToString() + "&";
    //            }
    //            if (dtAPI.Rows[0]["prm3"].ToString() != "")
    //            {
    //                str = str + dtAPI.Rows[0]["prm3"].ToString() + "=" + number + "&";
    //            }
    //            if (dtAPI.Rows[0]["prm4"].ToString() != "")
    //            {
    //                str = str + dtAPI.Rows[0]["prm4"].ToString() + "=" + OperatorCode + "&";
    //            }
    //            if (dtAPI.Rows[0]["prm5"].ToString() != "")
    //            {
    //                str = str + dtAPI.Rows[0]["prm5"].ToString() + "=" + ProfileCode + "&";
    //            }
    //            if (dtAPI.Rows[0]["prm6"].ToString() != "")
    //            {
    //                str = str + dtAPI.Rows[0]["prm6"].ToString() + "=" + amount + "&";
    //            }
    //            if (dtAPI.Rows[0]["prm7"].ToString() != "")
    //            {
    //                str = str + dtAPI.Rows[0]["prm7"].ToString() + "=" + account + "&";
    //            }
    //            if (dtAPI.Rows[0]["prm8"].ToString() != "")
    //            {
    //                str = str + dtAPI.Rows[0]["prm8"].ToString() + "=" + TransID + "&";
    //            }
    //            if (dtAPI.Rows[0]["prm9"].ToString() != "" && dtAPI.Rows[0]["prm9val"].ToString() != "")
    //            {
    //                str = str + dtAPI.Rows[0]["prm9"].ToString() + "=" + dtAPI.Rows[0]["prm9val"].ToString() + "&";
    //            }
    //            if (dtAPI.Rows[0]["prm10"].ToString() != "" && dtAPI.Rows[0]["prm10val"].ToString() != "")
    //            {
    //                str = str + dtAPI.Rows[0]["prm10"].ToString() + "=" + dtAPI.Rows[0]["prm10val"].ToString() + "&";
    //            }
    //            if (str.EndsWith("&"))
    //                str = str.Substring(0, str.Length - 1);

    //            string Success = Convert.ToString(dtAPI.Rows[0]["Success"]);
    //            string Failed = Convert.ToString(dtAPI.Rows[0]["Failed"]);
    //            string Pending = Convert.ToString(dtAPI.Rows[0]["Pending"]);

    //            var TxID = "";
    //            var Status = "";
    //            var OperatorRef = "";
    //            var ErrorCode = "";

    //            string result = apicall(str);

    //            char Splitter = Convert.ToChar(dtAPI.Rows[0]["Splitter"]);
    //            string[] split = result.Split(Splitter);
    //            if (Convert.ToString(dtAPI.Rows[0]["TxIDPosition"]) != "")
    //            {
    //                try
    //                {
    //                    int TxIDPosition = Convert.ToInt32(dtAPI.Rows[0]["TxIDPosition"]);
    //                    TxID = split[TxIDPosition];
    //                }
    //                catch { }
    //            }
    //            if (Convert.ToString(dtAPI.Rows[0]["StatusPosition"]) != "")
    //            {
    //                try
    //                {
    //                    int StatusPosition = Convert.ToInt32(dtAPI.Rows[0]["StatusPosition"]);
    //                    Status = split[StatusPosition];
    //                }
    //                catch { }
    //            }
    //            if (Convert.ToString(dtAPI.Rows[0]["OperatorRefPosition"]) != "")
    //            {
    //                try
    //                {
    //                    int OperatorRefPosition = Convert.ToInt32(dtAPI.Rows[0]["OperatorRefPosition"]);
    //                    OperatorRef = split[OperatorRefPosition];
    //                }
    //                catch { }
    //            }
    //            if (Convert.ToString(dtAPI.Rows[0]["ErrorCodePosition"]) != "")
    //            {
    //                try
    //                {
    //                    int ErrorCodePosition = Convert.ToInt32(dtAPI.Rows[0]["ErrorCodePosition"]);
    //                    ErrorCode = split[ErrorCodePosition];
    //                }
    //                catch { }
    //            }


    //            if (Status.ToLower() == Success.ToLower())
    //            {
    //                DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, MsrNo, 0, 0, 0, 0, "", "", "Success", TxID, "", OperatorRef);
    //                Response.Write("0," + TransID + ",Success," + ErrorCode + "," + OperatorRef + "," + DateTime.Now + ",");
    //                string[] valueArray = new string[3];
    //                valueArray[0] = number;
    //                valueArray[1] = amount;
    //                valueArray[2] = TransID;
    //                // SMS.SendWithVar(REQUEST_MOB_NUMBER, 2, valueArray);
    //            }
    //            else if (Status.ToLower() == Failed.ToLower() || Status.ToLower() == "103")
    //            {
    //                objEWalletTransaction.EWalletTransaction(memberid, Convert.ToDecimal(amount), "Cr", "Return amount");
    //                DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Failed", TxID, ErrorCode, OperatorRef);
    //                Response.Write("0," + TransID + ",Failure,RQF," + OperatorRef + "," + DateTime.Now + ",");
    //                string[] valueArray = new string[3];
    //                valueArray[0] = number;
    //                valueArray[1] = amount;
    //                valueArray[2] = TransID;
    //                //SMS.SendWithVar(REQUEST_MOB_NUMBER, 3, valueArray);
    //            }
    //            else if (Status.ToLower() == Pending.ToLower())
    //            {
    //                DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Pending", TxID, "", OperatorRef);
    //                Response.Write("0," + TransID + ",Pending,PEN," + OperatorRef + "," + DateTime.Now + ",");
    //                string[] valueArray = new string[3];
    //                valueArray[0] = number;
    //                valueArray[1] = amount;
    //                valueArray[2] = TransID;
    //                // SMS.SendWithVar(REQUEST_MOB_NUMBER, 4, valueArray);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    //public string apicall(string url)
    //{
    //    HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
    //    try
    //    {
    //        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
    //        StreamReader sr = new StreamReader(httpres.GetResponseStream());
    //        string results = sr.ReadToEnd();
    //        sr.Close();
    //        return results;
    //    }
    //    catch (Exception ex)
    //    {
    //        return "0";
    //    }
    //}
}