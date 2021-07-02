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

public partial class api_Login : System.Web.UI.Page
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
    clsMLM_FundRequest objFundRequest = new clsMLM_FundRequest();
    DataTable dtFundRequest = new DataTable();
    cls_myMember clsm = new cls_myMember();
    cls_Universal objUniversal = new cls_Universal();
    clsRecharge_Dispute objDispute = new clsRecharge_Dispute();
    clsRecharge_TariffPlan objTariffPlan = new clsRecharge_TariffPlan();
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
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
                    string REQUEST_MOB_NUMBER = Request.QueryString["from"].Trim().Replace("-", "").Replace("'", "");
                    string from = Request.QueryString["from"].Trim().Replace("-", "").Replace("'", "");
                    string membertypechk = "0";
                    if (from.Length == 12)
                    {
                        from = from.Substring(2, 10);
                    }
                    string message = Request.QueryString["message"].Trim().Replace("-", "").Replace("'", "");
                    string[] messageArray = message.Split(' ');
                    if (messageArray[0].Trim() == "3" || messageArray[0].Trim() == "4" || messageArray[0].Trim() == "5" || messageArray[0].Trim() == "6")
                    {
                        membertypechk = "3,4,5,6";
                    }
                    //else if (messageArray[0].Trim() == "6")
                    //{
                    //    membertypechk = "6";
                    //}
                    //DataTable dtMemberMaster = new DataTable();
                    //if (messageArray[1].Trim().ToUpper() == "LOGIN")
                    //    dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[2].Trim() + "'AND password='" + messageArray[3].Trim() + "' and membertypeID in (4,5)");
                    //else if (messageArray[1].Trim().ToUpper() == "BAL" || messageArray[1].Trim().ToUpper() == "BALT" || messageArray[1].Trim().ToUpper() == "LAST" || messageArray[1].Trim().ToUpper() == "OPTRS" || messageArray[1].Trim().ToUpper() == "CIRCLE" || messageArray[1].Trim().ToUpper() == "CHANGE" || messageArray[1].Trim().ToUpper() == "STATUS" || messageArray[1].Trim().ToUpper() == "MADD" || messageArray[1].Trim().ToUpper() == "FBANK" || messageArray[1].Trim().ToUpper() == "TBANK" || messageArray[1].Trim().ToUpper() == "ADDFUND" || messageArray[1].Trim().ToUpper() == "WLD")
                    //    dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[2].Trim() + "' and membertypeID in (4,5)");
                    //else
                    //    dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[1].Trim() + "' and membertypeID in (4,5)");
                    DataTable dtMemberMaster = new DataTable();
                    if (messageArray[1].Trim().ToUpper() == "LOGIN")
                        dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[2].Trim() + "'AND Password='" + messageArray[3].Trim() + "' and membertypeID in(" + membertypechk + ") and isactive=1 and isdelete=0");
                    else if (messageArray[1].Trim().ToUpper() == "BAL" || messageArray[1].Trim().ToUpper() == "BALT" || messageArray[1].Trim().ToUpper() == "LAST" || messageArray[1].Trim().ToUpper() == "OPTRS" || messageArray[1].Trim().ToUpper() == "CIRCLE" || messageArray[1].Trim().ToUpper() == "CHANGE" || messageArray[1].Trim().ToUpper() == "STATUS" || messageArray[1].Trim().ToUpper() == "MADD" || messageArray[1].Trim().ToUpper() == "FBANK" || messageArray[1].Trim().ToUpper() == "TBANK" || messageArray[1].Trim().ToUpper() == "ADDFUND" || messageArray[1].Trim().ToUpper() == "EWTRAN" || messageArray[1].Trim().ToUpper() == "CPASS" || messageArray[1].Trim().ToUpper() == "RDISLIST" || messageArray[1].Trim().ToUpper() == "DISPUTERPT" || messageArray[1].Trim().ToUpper() == "BALTD" || messageArray[1].Trim().ToUpper() == "FREQ" || messageArray[1].Trim().ToUpper() == "FORPASS" || messageArray[1].Trim().ToUpper() == "RDISPUTE" || messageArray[1].Trim().ToUpper() == "BPLAN" || messageArray[1].Trim().ToUpper() == "FORPIN" || messageArray[1].Trim().ToUpper() == "CPIN" || messageArray[1].Trim().ToUpper() == "BALD" || messageArray[1].Trim().ToUpper() == "NEWS" || messageArray[1].Trim().ToUpper() == "COMPL" || messageArray[1].Trim().ToUpper() == "CPBAL" || messageArray[1].Trim().ToUpper() == "REGUSR" || messageArray[1].Trim().ToUpper() == "DPKGS")
                        dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[2].Trim() + "' and membertypeID in(" + membertypechk + ") and isactive=1 and isdelete=0");
                    else if (messageArray[1].Trim().ToUpper() == "RCREQ" || messageArray[1].Trim().ToUpper() == "RCSUCC" || messageArray[1].Trim().ToUpper() == "RCFAIL" || messageArray[1].Trim().ToUpper() == "RSTATUS" || messageArray[1].Trim().ToUpper() == "WAREQ" || messageArray[1].Trim().ToUpper() == "WSTATUS" || messageArray[1].Trim().ToUpper() == "MPROMO" || messageArray[1].Trim().ToUpper() == "COUPONA" || messageArray[1].Trim().ToUpper() == "COUPONB" || messageArray[1].Trim().ToUpper() == "LOADCOUPON" || messageArray[1].Trim().ToUpper() == "CBWTRAN" || messageArray[1].Trim().ToUpper() == "MYCOUPONCODE" || messageArray[1].Trim().ToUpper() == "COMPL" || messageArray[1].Trim().ToUpper() == "CHKPAY" || messageArray[1].Trim().ToUpper() == "REPEAT")
                        dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[2].Trim() + "' and membertypeID in (6) and isactive=1 and isdelete=0");
                    else if (messageArray[1].Trim().ToUpper() == "REG")
                        registration(messageArray[2].Trim(), messageArray[3].Trim(), messageArray[4].Trim());
                    else if (messageArray[1].Trim().ToUpper() == "REGV")
                        registrationV(messageArray[2].Trim(), messageArray[3].Trim());
                    else if (messageArray[1].Trim().ToUpper() == "RSOTP")
                        RSOTPSend(messageArray[2].Trim(), messageArray[3].Trim());
                    else
                        dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster WHERE Mobile='" + messageArray[1].Trim() + "' and membertypeID in(" + membertypechk + ") and isactive=1 and isdelete=0");


                    if (dtMemberMaster.Rows.Count > 0)
                    {
                        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                        int PackageID = Convert.ToInt32(dtMemberMaster.Rows[0]["PackageID"]);
                        string memberid = Convert.ToString(dtMemberMaster.Rows[0]["MemberID"]);
                        string loginusertypeid = Convert.ToString(dtMemberMaster.Rows[0]["MembertypeID"]);
                        from = Convert.ToString(dtMemberMaster.Rows[0]["Mobile"]);
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
                                string Authenticate = "1," + dtMemberMaster.Rows[0]["MsrNo"].ToString() + "," + dtMemberMaster.Rows[0]["Mobile"].ToString() + "," + Convert.ToString(dtEWalletBalance.Rows[0]["Balance"]) + "," + dtMemberMaster.Rows[0]["TransactionPassword"].ToString() + "," + dtMemberMaster.Rows[0]["MemberID"].ToString() + "," + dtMemberMaster.Rows[0]["Email"].ToString() + "," + dtMemberMaster.Rows[0]["firstname"].ToString() + "_" + dtMemberMaster.Rows[0]["lastname"].ToString() + "," + (dtMemberMaster.Rows[0]["Memberimage"].ToString() == "" ? "http://www.payhalt.com/images/profile/actual/Member.jpg" : "http://www.payhalt.com/images/profile/small/" + dtMemberMaster.Rows[0]["Memberimage"].ToString()) + ",";
                                Response.Write(Authenticate);
                                #endregion
                            }

                            else if (messageArray[1].Trim().ToUpper() == "FORPIN")
                            {
                                // CY S FORPASS 9782600666
                                #region Forget Password
                                DataTable dt = new DataTable();
                                dt = objconnection.select_data_dt("Select * from  tblmlm_membermaster where mobile='" + messageArray[2].Trim().ToString() + "'");
                                string[] valueArray = new string[3];
                                valueArray[0] = dt.Rows[0]["Firstname"].ToString();
                                valueArray[1] = dt.Rows[0]["mobile"].ToString();
                                valueArray[2] = dt.Rows[0]["TransactionPassword"].ToString();
                                SMS.SendWithVar(messageArray[2].Trim().ToString(), 12, valueArray, MsrNo);
                                Response.Write("1,Sent Successfully,");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "NEWS")
                            {
                                // CY LAST
                                #region News List
                                // DataTable dt = objOperator.ManageOperator("GetByServiceTypeID", Convert.ToInt32(messageArray[3].Trim().ToString()));
                                DataTable dt = objconnection.select_data_dt("Select replace(Newsdesc,',','$') as NewsDesc from tblnews where isdelete=0 and isactive=1"); //objOperator.ManageOperator("GetByServiceTypeID", Convert.ToInt32(messageArray[3].Trim().ToString()));
                                string str = "";
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    str = str + Convert.ToString(dt.Rows[i]["NewsDesc"]) + " | ";
                                }
                                //string[] valueArray = new string[1];
                                //valueArray[0] = str;
                                ////SMS.SendWithVar(from, 8, valueArray);
                                Response.Write(str.ToString() + ",");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "CPIN")
                            {
                                // CY S CPASS 9782600666 Old NewPass
                                #region Change Password
                                try
                                {

                                    int intresult = 0;
                                    DataTable dt = new DataTable();
                                    dt = objconnection.select_data_dt("Exec Proc_UpdatePin '" + MsrNo.ToString() + "','" + messageArray[4].Trim() + "','" + messageArray[3].Trim() + "'");
                                    intresult = Convert.ToInt32(dt.Rows[0][0]);
                                    //intresult = objUniversal.UpdatePassword("UpdateMemberPassword", Convert.ToInt32(MsrNo), messageArray[3].Trim(), messageArray[4].Trim());
                                    if (intresult > 0)
                                    {

                                        string[] valueArray = new string[1];
                                        valueArray[0] = "1,SUCCESS";
                                        Response.Write(valueArray[0].ToString() + ",");
                                    }
                                    else
                                    {
                                        string[] valueArray = new string[1];
                                        valueArray[0] = "0,Invalid Input.";
                                        Response.Write(valueArray[0].ToString() + ",");

                                    }
                                }
                                catch
                                {
                                    string[] valueArray = new string[1];
                                    valueArray[0] = "0,Invalid Input.";
                                    Response.Write(valueArray[0].ToString() + ",");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "BALT")
                            {
                                // CY BALT 9983554400 100   anil with downline
                                #region Balance Transfer
                                if (messageArray.Length >= 4)
                                {
                                    string amount = messageArray[4].Trim();
                                    if (amount != null && amount != "")
                                    {
                                        if (clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), MsrNo) == 0)
                                        {
                                            Response.Write("0,Insufficient Fund,");
                                        }
                                        else
                                        {
                                            string ToNumber = messageArray[3].Trim();
                                            try
                                            {
                                                //int x = clsm.Cyrus_TransferFund(from, ToNumber, 1, MsrNo, Convert.ToDecimal(amount));
                                                //if (x == 0)
                                                //{
                                                //    Response.Write("0,Invalid Mobile Number.,");
                                                //}
                                                //else
                                                //{
                                                //    Response.Write("1,SUCCESS");
                                                //}
                                                Response.Write("0,Invalid Mobile Number.,");
                                            }
                                            catch
                                            {
                                                Response.Write("0,Invalid Mobile Number.,");
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
                                    Response.Write("0,Invalid request.,");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "BALTC")
                            {
                                // CY BALT 9983554400 100   anil with downline
                                #region Balance Transfer
                                if (messageArray.Length >= 4)
                                {
                                    string amount = messageArray[4].Trim();
                                    if (amount != null && amount != "")
                                    {
                                        if (clsm.Cyrus_ChkEwalletBalance_BeforeTransactionCC(Convert.ToDecimal(amount), MsrNo) == 0)
                                        {
                                            Response.Write("0,Insufficient Fund or limit exceeded,");
                                        }
                                        else
                                        {
                                            string ToNumber = messageArray[2].Trim();
                                            try
                                            {
                                                //int x = clsm.Cyrus_TransferFund_C(from, ToNumber, 1, MsrNo, Convert.ToDecimal(amount));
                                                //if (x == 0)
                                                //{
                                                //    Response.Write("0,Invalid Mobile Number.,");
                                                //}
                                                //else
                                                //{
                                                //    Response.Write("1,SUCCESS");
                                                //}

                                                Response.Write("0,Invalid Mobile Number.,");
                                            }
                                            catch
                                            {
                                                Response.Write("0,Invalid Mobile Number.,");
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
                                    Response.Write("0,Invalid request.,");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "BALD")
                            {
                                // CY BALT 9983554400 100   anil with downline
                                #region Balance Deduct
                                if (messageArray.Length >= 4)
                                {
                                    string amount = messageArray[4].Trim();
                                    string ToNumber = messageArray[3].Trim();
                                    int tomsrn = objconnection.select_data_scalar_int("select msrno from tblmlm_membermaster where mobile='" + ToNumber + "' and isactive=1 and isdelete=0");
                                    if (tomsrn != null && tomsrn > 0)
                                    {
                                        if (amount != null && amount != "")
                                        {
                                            if (clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), tomsrn) == 0)
                                            {
                                                Response.Write("0,Insufficient Fund,");
                                            }
                                            else
                                            {

                                                try
                                                {
                                                    int x = clsm.Cyrus_TransferFund(ToNumber, from, 1, MsrNo, Convert.ToDecimal(amount));
                                                    if (x == 0)
                                                    {
                                                        Response.Write("0,Invalid Mobile Number.,");
                                                    }
                                                    else
                                                    {
                                                        Response.Write("1,SUCCESS");
                                                    }
                                                }
                                                catch
                                                {
                                                    Response.Write("0,Invalid Mobile Number.,");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Response.Write("0,Invalid Amount.,");
                                        }
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Invalid request.,");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "CPBAL")
                            {
                                // CY BALT 9983554400 100   anil with downline
                                #region Balance Deduct
                                if (messageArray.Length >= 3)
                                {
                                    string ToNumber = messageArray[3].Trim();
                                    int tomsrn = objconnection.select_data_scalar_int("select msrno from tblmlm_membermaster where mobile='" + ToNumber + "' and isactive=1 and isdelete=0");
                                    int parentmsrno = objconnection.select_data_scalar_int("select parentmsrno from tblmlm_membermaster where msrno='" + tomsrn + "' and isactive=1 and isdelete=0");
                                    if (parentmsrno == MsrNo)
                                    {
                                        DataTable MemberdtEWalletBalance = new DataTable();
                                        MemberdtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", tomsrn);
                                        string[] valueArray = new string[1];
                                        valueArray[0] = Convert.ToString(MemberdtEWalletBalance.Rows[0]["Balance"]);
                                        Response.Write("1," + valueArray[0].ToString() + ",");
                                    }
                                    else
                                    {
                                        Response.Write("0,Invalid Member Mobile no.,");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Invalid request.,");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "LAST")
                            {
                                #region Last Recharges
                                string mobile = "";
                                if (messageArray.Length > 3)
                                    mobile = messageArray[3].Trim().ToString();
                                string strs = "select top 200 ROW_NUMBER() OVER(ORDER BY MsrNo) AS SNo,tblRecharge_History.*,x.OperatorName,x.operatorcode from tblRecharge_History INNER JOIN tblRecharge_Operator as x ON x.operatorID=tblRecharge_History.operatorID where MsrNo=" + MsrNo + "";
                                //DataTable dt = objconnection.select_data_dt(" and Status<>'Queued' order by HistoryID desc");
                                if (mobile != "")
                                    strs = strs + " and mobileno='" + mobile + "'";
                                strs = strs + " and Status<>'Queued' order by HistoryID desc";
                                DataTable dt = objconnection.select_data_dt(strs);
                                string str = "";
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    //str = str + Convert.ToString(dt.Rows[i]["SNo"]) + ". " + Convert.ToString(dt.Rows[i]["MobileNo"]) + " - " + Convert.ToString(dt.Rows[i]["RechargeAmount"]) + " - " + Convert.ToString(dt.Rows[i]["Status"]) + " - " + Convert.ToString(dt.Rows[i]["TransID"]) + " - " + Convert.ToString(dt.Rows[i]["AddDate"]) + " - " + Convert.ToString(dt.Rows[i]["APITransID"]) + " - " + Convert.ToString(dt.Rows[i]["OperatorName"]) + " - " + Convert.ToString(dt.Rows[i]["APIMessage"]) + ",\n";
                                    //str = str + Convert.ToString(dt.Rows[i]["SNo"]) + ". " + Convert.ToString(dt.Rows[i]["MobileNo"]) + " - " + Convert.ToString(dt.Rows[i]["OperatorName"]) + " - " + Convert.ToString(dt.Rows[i]["TransID"]) + " - " + Convert.ToString(dt.Rows[i]["APITransID"]) + " - " + Convert.ToString(dt.Rows[i]["APIMessage"]) + " - " + Convert.ToString(dt.Rows[i]["AddDate"]) + " - " + Convert.ToString(dt.Rows[i]["RechargeAmount"]) + " - " + Convert.ToString(dt.Rows[i]["Status"]) + ",\n";
                                    str = str + Convert.ToString(dt.Rows[i]["SNo"]) + ". " + Convert.ToString(dt.Rows[i]["MobileNo"]) + " - " + Convert.ToString(dt.Rows[i]["OperatorName"]) + " - " + Convert.ToString(dt.Rows[i]["TransID"]) + " - " + Convert.ToString(dt.Rows[i]["APITransID"]) + " - " + Convert.ToString(dt.Rows[i]["APIMessage"]) + " - " + Convert.ToString(dt.Rows[i]["AddDate"]) + " - " + Convert.ToString(dt.Rows[i]["RechargeAmount"]) + " - " + Convert.ToString(dt.Rows[i]["Status"]) + " - " + Convert.ToString(dt.Rows[i]["Operatorcode"]) + " - " + Convert.ToString(dt.Rows[i]["historyid"]) + ",\n";
                                }
                                string[] valueArray = new string[1];
                                valueArray[0] = str;
                                //SMS.SendWithVar(from, 8, valueArray);
                                Response.Write(valueArray[0].ToString() + ",");
                                #endregion
                                //// CY LAST
                                //#region Last Recharges
                                //DataTable dt = objconnection.select_data_dt("select top 200 ROW_NUMBER() OVER(ORDER BY MsrNo) AS SNo,tblRecharge_History.*,x.OperatorName,x.operatorcode from tblRecharge_History INNER JOIN tblRecharge_Operator as x ON x.operatorID=tblRecharge_History.operatorID where MsrNo=" + MsrNo + " and Status<>'Queued' order by HistoryID desc");
                                //string str = "";
                                //for (int i = 0; i < dt.Rows.Count; i++)
                                //{
                                //    //str = str + Convert.ToString(dt.Rows[i]["SNo"]) + ". " + Convert.ToString(dt.Rows[i]["MobileNo"]) + " - " + Convert.ToString(dt.Rows[i]["RechargeAmount"]) + " - " + Convert.ToString(dt.Rows[i]["Status"]) + " - " + Convert.ToString(dt.Rows[i]["TransID"]) + " - " + Convert.ToString(dt.Rows[i]["AddDate"]) + " - " + Convert.ToString(dt.Rows[i]["APITransID"]) + " - " + Convert.ToString(dt.Rows[i]["OperatorName"]) + " - " + Convert.ToString(dt.Rows[i]["APIMessage"]) + ",\n";
                                //    str = str + Convert.ToString(dt.Rows[i]["SNo"]) + ". " + Convert.ToString(dt.Rows[i]["MobileNo"]) + " - " + Convert.ToString(dt.Rows[i]["OperatorName"]) + " - " + Convert.ToString(dt.Rows[i]["TransID"]) + " - " + Convert.ToString(dt.Rows[i]["APITransID"]) + " - " + Convert.ToString(dt.Rows[i]["APIMessage"]) + " - " + Convert.ToString(dt.Rows[i]["AddDate"]) + " - " + Convert.ToString(dt.Rows[i]["RechargeAmount"]) + " - " + Convert.ToString(dt.Rows[i]["Status"]) + " - " + Convert.ToString(dt.Rows[i]["Operatorcode"]) + " - " + Convert.ToString(dt.Rows[i]["historyid"]) + ",\n";
                                //}
                                //string[] valueArray = new string[1];
                                //valueArray[0] = str;
                                ////SMS.SendWithVar(from, 8, valueArray);
                                //Response.Write(valueArray[0].ToString() + ",");
                                //#endregion
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
                                // DataTable dt = objconnection.select_data_dt("select cc.CircleID ,c.CircleName from tblRecharge_CircleCode as cc INNER JOIN tblRecharge_Circle AS c ON c.CircleID=cc.CircleID where cc.IsDelete=0 and cc.IsActive=1");//objCircle.ManageCircle("Get", 0);
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
                            else if (messageArray[1].Trim().ToUpper() == "FBANK")
                            {
                                // CY LAST
                                #region From  Bank  List
                                DataTable dt = objconnection.select_data_dt("Select BankerMasterID,BankerMasterName from tblMLM_BankerMaster    where IsActive='true' and  IsDelete='false' order by BankerMasterName   ");//objCircle.ManageCircle("Get", 0);
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
                                Response.Write("{ FBANK:" + output + "},");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "ADDFUND")
                            {
                                // CY LAST 
                                #region Add Fund Request
                                try
                                {

                                    int intresult = 0;
                                    DateTime ChequeDate;
                                    ChequeDate = DateTime.Now;
                                    string Remark = (messageArray[7].Trim()).Replace("_", " ");
                                    intresult = objFundRequest.AddEditFundRequest(0, "E Wallet", Convert.ToInt32(MsrNo), memberid, Convert.ToDecimal(messageArray[3].Trim()), messageArray[4].Trim(), messageArray[5].Trim(), "", messageArray[6].Trim(), ChequeDate, "Pending", Remark, "", messageArray[4].Trim(), messageArray[8].Trim());
                                    if (intresult > 0)
                                    {

                                        string[] valueArray = new string[1];
                                        valueArray[0] = "1,SUCCESS";
                                        Response.Write(valueArray[0].ToString() + ",");
                                    }
                                    else
                                    {

                                        // SMS.SendWithVar(from, 8, valueArray);
                                        string[] valueArray = new string[1];
                                        valueArray[0] = "0,Invalid Input.";
                                        Response.Write(valueArray[0].ToString() + ",");
                                        //string output = ConvertDataTabletoString(dt);
                                        //Response.Write("{ FBANK:" + output + "},");
                                    }
                                }
                                catch
                                {
                                    string[] valueArray = new string[1];
                                    valueArray[0] = "0,Invalid Input.";
                                    Response.Write(valueArray[0].ToString() + ",");
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
                                Response.Write("Complain Sent Successfully !!,");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "TBANK")
                            {
                                // CY LAST
                                #region TO  Bank  List
                                DataTable dt = objconnection.select_data_dt("exec ProcMLM_ManageMemberBanker 'GetByParentMsrNo'," + MsrNo);//objCircle.ManageCircle("Get", 0);
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
                                Response.Write("{ TBANK:" + output + "},");
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
                                        int x = clsm.Cyrus_ChangeMobile(NewNumber, MsrNo);
                                        if (x == 0)
                                        {
                                            Response.Write("0,Invalid Input !!,");
                                        }
                                        Response.Write("1," + NewNumber + ",");
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
                                if (messageArray.Length >= 3)
                                {
                                    string TransID = messageArray[3].Trim();
                                    if (TransID != null && TransID != "")
                                    {
                                        DataTable dt = objconnection.select_data_dt("select top 1 Status,Mobileno,Rechargeamount,b.operatorname as operatorname from tblRecharge_History as a,tblrecharge_operator as b where a.TransID='" + TransID + "' and a.operatorid=b.operatorid");
                                        if (dt.Rows.Count > 0)
                                        {
                                            string[] valueArray = new string[6];
                                            valueArray[0] = TransID;
                                            valueArray[1] = Convert.ToString(dt.Rows[0]["Status"]);
                                            valueArray[2] = Convert.ToString(dt.Rows[0]["Mobileno"]);
                                            valueArray[3] = Convert.ToString(dt.Rows[0]["RechargeAmount"]);
                                            valueArray[4] = Convert.ToString(dt.Rows[0]["operatorname"]);
                                            Response.Write("1," + valueArray[0].ToString() + "," + valueArray[1].ToString() + "," + valueArray[2].ToString() + "," + valueArray[3].ToString() + "," + valueArray[4].ToString() + ",");
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
                                DataTable dt = objconnection.select_data_dt("select * from tblMAdd  WHERE IsActive='true' and  IsDelete='false' and IsDist=1");
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
                            else if (messageArray[1].Trim().ToUpper() == "MYCOUPONCODE")
                            {
                                DataTable dt = new DataTable();
                                dt = objconnection.select_data_dt("Select * from tblmlm_MemberCouponCode where msrno=" + MsrNo.ToString());
                                if (dt.Rows.Count == 0)
                                    //lblCouponcode.Text = "NO COUPON !!";
                                    Response.Write("0,No Coupon !!,");
                                else
                                    //lblCouponcode.Text = dt.Rows[0]["Couponcode"].ToString();
                                    Response.Write("1," + dt.Rows[0]["Couponcode"].ToString() + ",");
                            }
                            else if (messageArray[1].Trim().ToUpper() == "EWTRAN")
                            {
                                // CY S EWTRAN 9782600666
                                #region E Wallet Transaction
                                DataTable dt = objEWalletTransaction.ManageEWalletTransaction("GetByMsrNo", MsrNo);
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ EWList:" + output + "},");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "CBWTRAN")
                            {
                                // CY S EWTRAN 9782600666
                                #region CW Wallet Transaction
                                //DataTable dt = objEWalletTransaction.ManageEWalletTransaction("GetByMsrNo", MsrNo);
                                DataTable dt = objconnection.select_data_dt("Exec ProcMLM_ManageSWalletTransaction 'GetByMsrNo'," + MsrNo + "");
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ CBWList:" + output + "},");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "CPASS")
                            {
                                // CY S CPASS 9782600666 Old NewPass
                                #region Change Password
                                try
                                {

                                    int intresult = 0;
                                    intresult = objUniversal.UpdatePassword("UpdateMemberPassword", Convert.ToInt32(MsrNo), messageArray[3].Trim(), messageArray[4].Trim());
                                    if (intresult > 0)
                                    {

                                        string[] valueArray = new string[1];
                                        valueArray[0] = "1,SUCCESS";
                                        Response.Write(valueArray[0].ToString() + ",");
                                    }
                                    else
                                    {
                                        string[] valueArray = new string[1];
                                        valueArray[0] = "0,Invalid Input.";
                                        Response.Write(valueArray[0].ToString() + ",");

                                    }
                                }
                                catch
                                {
                                    string[] valueArray = new string[1];
                                    valueArray[0] = "0,Invalid Input.";
                                    Response.Write(valueArray[0].ToString() + ",");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "DPKGS")
                            {
                                // CY S EWTRAN 9782600666
                                #region CW Wallet Transaction
                                DataTable dt = objconnection.select_data_dt("select packageid,packagename from tblmlm_package where msrno=" + MsrNo + " and isactive=1 and isdelete=0");
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ DPKGS:" + output + "},");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "REGUSR")
                            {
                                // CY S CPASS 9782600666 Old NewPass
                                try
                                {
                                    if (messageArray.Length >= 5)
                                    {
                                        string Mobilenumber = messageArray[3].Trim();
                                        string myusername = messageArray[4].Trim().Replace("$", " ");
                                        string mypackage = messageArray[5].Trim();

                                        if (Mobilenumber.Length != 10)
                                        {
                                            Response.Write("0,Invalid mobile number,");
                                            return;
                                        }

                                        int intresult = 0;
                                        //intresult = objUniversal.UpdatePassword("UpdateMemberPassword", Convert.ToInt32(MsrNo), messageArray[3].Trim(), messageArray[4].Trim());
                                        Random random = new Random();
                                        int SixDigit = random.Next(100000, 999999);
                                        string MemberID = "";
                                        if (loginusertypeid == "4")
                                        {
                                            MemberID = "RT" + SixDigit;
                                        }
                                        else if (loginusertypeid == "3")
                                        {
                                            MemberID = "DT" + SixDigit;
                                        }
                                        else if (loginusertypeid == "5")
                                        {
                                            Response.Write("0,Retailer can not register new member.,");
                                            return;
                                        }
                                        SixDigit = random.Next(100000, 999999);
                                        int sixdigit1 = random.Next(1000, 9999);
                                        DateTime DOJ = DateTime.Now;
                                        try
                                        {
                                            intresult = objMemberMaster.AddEditMemberMaster(0, MemberID, myusername, "", "", DOJ, "", SixDigit.ToString(), sixdigit1.ToString().Substring(0,4), Mobilenumber, "", "", "", "", 1, 22, 450, "", "", "Retailer", 5, Convert.ToInt32(MsrNo), Convert.ToInt32(mypackage), "");
                                            if (intresult > 0)
                                            {
                                                //RegisterMail.SendRegistrationMail(MemberID + " - " + txtFirstName.Text + " " + txtLastName.Text, txtEmail.Text, txtMobile.Text, txtPassword.Text, txtTransactionPassword.Text);
                                                string[] valueArray = new string[4];
                                                valueArray[0] = myusername;
                                                valueArray[1] = MemberID;
                                                valueArray[2] = SixDigit.ToString();
                                                valueArray[3] = sixdigit1.ToString().Substring(0, 4);
                                                //SMS.SendWithVar(txtMobile.Text, 14, valueArray,1);
                                                SMS.SendWithVar(Mobilenumber, 26, valueArray, 1);
                                                Response.Write("1,Member Registered successfully !!,");
                                            }
                                            else
                                            {
                                                Response.Write("1,Member already registered !!,");
                                            }
                                        }
                                        catch
                                        {
                                            string[] valueArray = new string[1];
                                            valueArray[0] = "0,System Error.";
                                            Response.Write(valueArray[0].ToString() + ",");
                                        }
                                    }
                                }
                                catch
                                {
                                    string[] valueArray = new string[1];
                                    valueArray[0] = "0,Invalid Input.";
                                    Response.Write(valueArray[0].ToString() + ",");
                                }
                            }

                            else if (messageArray[1].Trim().ToUpper() == "RDISLIST")
                            {
                                // CY S RDISLIST 9782600666
                                #region Raise Dispute
                                DataTable dt = objHistory.ManageHistory("GetByMsrNo", Convert.ToInt32(MsrNo));
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ RDISLIST:" + output + "},");
                                #endregion
                            }

                            else if (messageArray[1].Trim().ToUpper() == "RDISPUTE")
                            {
                                // CY S RDISPUTE 9782600666 62995 msg
                                #region Raise Dispute Click
                                try
                                {

                                    DataTable dtintresult = new DataTable();
                                    dtintresult = objHistory.ManageHistory("Get", Convert.ToInt32(messageArray[3].Trim()));
                                    string msg = messageArray[4].Trim().Replace("_", " ");
                                    Int32 intresult2 = 0;
                                    if (dtintresult.Rows.Count > 0)
                                    {
                                        intresult2 = objDispute.AddEditDispute(0, Convert.ToInt32(messageArray[3].Trim()), msg, false);
                                        if (intresult2 > 0)
                                        {
                                            string[] valueArray = new string[1];
                                            valueArray[0] = "1,SUCCESS";
                                            Response.Write(valueArray[0].ToString() + ",");
                                        }
                                        else
                                        {
                                            string[] valueArray = new string[1];
                                            valueArray[0] = "0,Invalid Input.";
                                            Response.Write(valueArray[0].ToString() + ",");

                                        }
                                    }
                                    else
                                    {
                                        string[] valueArray = new string[1];
                                        valueArray[0] = "0,Invalid Input.";
                                        Response.Write(valueArray[0].ToString() + ",");

                                    }
                                }
                                catch
                                {
                                    string[] valueArray = new string[1];
                                    valueArray[0] = "0,Invalid Input.";
                                    Response.Write(valueArray[0].ToString() + ",");
                                }
                                #endregion
                            }

                            else if (messageArray[1].Trim().ToUpper() == "DISPUTERPT")
                            {
                                // CY S DISPUTERPT 9782600666
                                #region Raise Dispute
                                DataTable dt = objDispute.ManageDispute("GetByHistoryID", Convert.ToInt32(MsrNo));
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ DISPUTERPT:" + output + "},");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "BALTD")
                            {
                                // CY BALTD 9983554400 100  
                                #region Balance Transfer Downline
                                if (messageArray.Length >= 4)
                                {
                                    string amount = messageArray[4].Trim();
                                    if (amount != null && amount != "")
                                    {
                                        if (Convert.ToDecimal(amount) > Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                        {
                                            SMS.Send(from, "You have insufficient fund, please credit fund from MasterDistributor or company.", MsrNo);
                                        }
                                        else
                                        {
                                            string ToNumber = messageArray[3].Trim();
                                            dtMemberMaster.Clear();
                                            dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where Mobile='" + ToNumber + "' and MsrNo in (Select msrno from tblmlm_membertree where parentstr like '%,' + Convert(varchar,'" + MsrNo + "') + ',%')");
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

                            else if (messageArray[1].Trim().ToUpper() == "FREQ")
                            {
                                // CY S FREQ 9782600666
                                #region Fund req report
                                DataTable dt = objFundRequest.ManageFundRequest("GetByMsrNo", Convert.ToInt32(MsrNo));
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ FUNDReQ:" + output + "},");
                                #endregion
                            }

                            else if (messageArray[1].Trim().ToUpper() == "FORPASS")
                            {
                                // CY S FORPASS 9782600666
                                #region Forget Password
                                DataTable dt = new DataTable();
                                dt = objconnection.select_data_dt("Select * from  tblmlm_membermaster where mobile='" + messageArray[2].Trim().ToString() + "'");
                                string[] valueArray = new string[3];
                                valueArray[0] = dt.Rows[0]["Firstname"].ToString();
                                valueArray[1] = dt.Rows[0]["mobile"].ToString();
                                valueArray[2] = dt.Rows[0]["Password"].ToString();
                                SMS.SendWithVar(messageArray[2].Trim().ToString(), 12, valueArray, MsrNo);
                                Response.Write("1,Sent Successfully,");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "MYCOMM")
                            {
                                // CY S MYCOMM 9782600666
                                #region My Commission
                                DataTable dt = new DataTable();
                                dt = objconnection.select_data_dt("select *,case when issurcharge=1 then 'Surcharge : ' + CONVERT(varchar,isnull(commission,0)) else CONVERT(varchar,commission) end  as MyCommission,(select ServiceTypeName from tblRecharge_ServiceType where tblRecharge_Operator.ServiceTypeID=tblRecharge_ServiceType.ServiceTypeID) as ServiceType from tblRecharge_Operator left outer join (select OperatorID, Commission, PackageID,issurcharge from tblRecharge_Commission where PackageID=" + Convert.ToInt32(PackageID) + ") as xx  on tblRecharge_Operator.OperatorID=xx.OperatorID where tblRecharge_Operator.IsDelete=0");
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ MYCOMM:" + output + "},");
                                #endregion
                            }

                            else if (messageArray[1].Trim().ToUpper() == "BPLAN")
                            {
                                // CY S BPLAN 
                                #region Browse Plan
                                DataTable dt = new DataTable();
                                //dt = objTariffPlan.ManageTariffPlan("GetAll", 0);
                                dt = objconnection.select_data_dt("EXEC GetTariffPlan_app '" + messageArray[3].Trim().ToString().ToUpper().Replace(";", "").Replace("-", "") + "','" + messageArray[4].Trim().ToString().ToUpper().Replace(";", "").Replace("-", "") + "'");
                                string output = ConvertDataTabletoString(dt);
                                Response.Write("{ BPLAN:" + output + "},");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "MPROMO")
                            {
                                #region PromoChk
                                if (messageArray.Length == 6)
                                {
                                    string Usr_Mobile = messageArray[2].ToString();
                                    string Usr_ServiceType = messageArray[3].ToString();
                                    string Usr_RCAMT = messageArray[4].ToString();
                                    string Usr_PROMOCODE = messageArray[5].ToString();

                                    string Return_TP = "";
                                    string Return_RCAMT = "0";
                                    string Return_DAMT = "0";
                                    string Return_DESC = "";
                                    //Promo Code check starts here
                                    DataTable dtpromo = new DataTable();
                                    double discount, TotalAmount, NetAmount;
                                    TotalAmount = Convert.ToDouble(Usr_RCAMT);
                                    DataTable dtx = new DataTable();
                                    dtx = objconnection.select_data_dt("Exec ApplyPromocode_Recharge_app '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + Usr_PROMOCODE + "',0,'" + Usr_RCAMT + "'");
                                    if (Convert.ToInt32(dtx.Rows[0]["idno"]) > 0)
                                    {
                                        Return_TP = "DC";
                                        discount = Convert.ToDouble(dtx.Rows[0]["amount"]);
                                        TotalAmount = Convert.ToDouble(Usr_RCAMT);
                                        NetAmount = TotalAmount - discount;
                                        Return_DAMT = discount.ToString();
                                        //litAmount.Text = Convert.ToString(NetAmount);
                                        //hdnamount.Value = Convert.ToString(NetAmount);
                                        Return_DESC = "Discount of Rs. 10 will be adjusted from your coupon wallet";
                                        Response.Write("1,DC," + Return_DAMT + "," + NetAmount + "," + Return_DESC + ",");
                                    }
                                    else if (Convert.ToInt32(dtx.Rows[0]["idno"]) == 0)
                                    {
                                        if (objconnection.select_data_scalar_int("SELECT COUNT(*) FROM Coupon_PurchaseHistory WHERE C_couponcode='" + Usr_PROMOCODE + "' and c_msrno='" + dtMemberMaster.Rows[0]["msrno"].ToString() + "' and Convert(varchar,validtodate,112)>=Convert(varchar,getdate(),112)") > 0)
                                        {
                                            dtx = objconnection.select_data_dt("SELECT * FROM Coupon_PurchaseHistory WHERE C_couponcode='" + Usr_PROMOCODE + "' and c_msrno='" + dtMemberMaster.Rows[0]["msrno"].ToString() + "' and Convert(varchar,validtodate,112)>=Convert(varchar,getdate(),112) and amountfrom<='" + TotalAmount.ToString() + "' and amountto>='" + TotalAmount.ToString() + "' and Convert(int,C_transactioid)<couponcount and '" + Usr_ServiceType + "' in (SELECT serviceid FROM Coupon_New_Setting_Services WHERE categoryid=Coupon_PurchaseHistory.C_CouponCategoryid)");
                                            if (dtx.Rows.Count > 0)
                                            {
                                                if (Convert.ToInt32(dtx.Rows[0]["coupontype"]) == 1) //Discount
                                                {
                                                    TotalAmount = Convert.ToDouble(Usr_RCAMT);
                                                    if (Convert.ToInt32(dtx.Rows[0]["isflatDiscount"]) == 1)
                                                        discount = Convert.ToDouble(dtx.Rows[0]["discount"]);
                                                    else
                                                        discount = (TotalAmount * (Convert.ToDouble(dtx.Rows[0]["discount"]) / 100));
                                                    NetAmount = TotalAmount - discount;
                                                    Return_DAMT = discount.ToString();
                                                    //litAmount.Text = Convert.ToString(NetAmount);
                                                    //hdnamount.Value = Convert.ToString(NetAmount);
                                                    Return_DESC = dtx.Rows[0]["discount"].ToString() + "% Discount = Rs." + discount.ToString() + " on this recharge";
                                                    Response.Write("1,D," + Return_DAMT + "," + NetAmount + "," + Return_DESC + ",");
                                                }
                                                else if (Convert.ToInt32(dtx.Rows[0]["coupontype"]) == 2) //Cashback
                                                {
                                                    TotalAmount = Convert.ToDouble(Usr_RCAMT);
                                                    if (Convert.ToInt32(dtx.Rows[0]["isflatDiscount"]) == 1)
                                                        discount = Convert.ToDouble(dtx.Rows[0]["discount"]);
                                                    else
                                                        discount = (TotalAmount * (Convert.ToDouble(dtx.Rows[0]["discount"]) / 100));
                                                    NetAmount = TotalAmount;
                                                    Return_DAMT = discount.ToString();
                                                    //litAmount.Text = Convert.ToString(NetAmount);
                                                    //hdnamount.Value = Convert.ToString(NetAmount);
                                                    Return_DESC = "Cashback of Rs. " + discount.ToString() + " will be added in your Recharge Wallet";
                                                    //lblCouponDescription.Text = "Cashback of Rs. " + discount.ToString() + " will be added in your Recharge Wallet after completion of this Recharge Transaction.";
                                                    Response.Write("1,C," + Return_DAMT + "," + NetAmount + "," + Return_DESC + ",");
                                                    //Session.Add("Cashback", discount.ToString());
                                                    //CouponApply.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Coupon code does not match with Terms of Use. Please check Terms and condition of this coupon!!')", true);
                                                Response.Write("0,0,0,0,Coupon code does not match with Terms of Use,");
                                            }
                                        }
                                        else if (objconnection.select_data_scalar_int("SELECT COUNT(*) FROM Offer_Coupon_Code as a,Coupon_New_Setting_Master as b WHERE a.couponid=b.id and a.couponcode='" + Usr_PROMOCODE + "' and b.couponpurchasevalue=0 and Convert(varchar,datevalidto,112)>=Convert(varchar,getdate(),112)") > 0)
                                        {
                                            //dtx = cls.select_data_dt("SELECT * FROM Coupon_PurchaseHistory WHERE C_couponcode='" + txtpromocode.Text.Trim() + "' and c_msrno='" + dtMemeber.Rows[0]["msrno"].ToString() + "' and Convert(varchar,validtodate,112)>=Convert(varchar,getdate(),112) and amountfrom<='" + TotalAmount.ToString() + "' and amountto>='" + TotalAmount.ToString() + "' and Convert(int,C_transactioid)<couponcount and '" + hdnServiceTypeIDno.Value + "' in (SELECT serviceid FROM Coupon_New_Setting_Services WHERE categoryid=Coupon_PurchaseHistory.C_CouponCategoryid)");
                                            dtx = objconnection.select_data_dt("SELECT * FROM Offer_Coupon_Code as a,Coupon_New_Setting_Master as b WHERE  a.couponid=b.id and a.couponcode='" + Usr_PROMOCODE + "' and Convert(varchar,datevalidfrom,112)<=Convert(varchar,getdate(),112) and Convert(varchar,datevalidto,112)>=Convert(varchar,getdate(),112) and amountvalidfrom<='" + TotalAmount.ToString() + "' and amountvalidto>='" + TotalAmount.ToString() + "'and '" + Usr_ServiceType + "' in (SELECT serviceid FROM Coupon_New_Setting_Services WHERE categoryid=a.couponid)");
                                            if (dtx.Rows.Count > 0)
                                            {
                                                if (Convert.ToInt32(dtx.Rows[0]["coupontype"]) == 1) //Discount
                                                {
                                                    TotalAmount = Convert.ToDouble(Usr_RCAMT);
                                                    if (Convert.ToInt32(dtx.Rows[0]["isDiscountflat"]) == 1)
                                                        discount = Convert.ToDouble(dtx.Rows[0]["discountoncoupon"]);
                                                    else
                                                        discount = (TotalAmount * (Convert.ToDouble(dtx.Rows[0]["discountoncoupon"]) / 100));
                                                    NetAmount = TotalAmount - discount;
                                                    Return_DAMT = discount.ToString();
                                                    //litAmount.Text = Convert.ToString(NetAmount);
                                                    //hdnamount.Value = Convert.ToString(NetAmount);
                                                    Return_DESC = dtx.Rows[0]["discountoncoupon"].ToString() + "% Discount = Rs." + discount.ToString() + " on this recharge.";
                                                    Response.Write("1,D," + Return_DAMT + "," + NetAmount + "," + Return_DESC + ",");
                                                }
                                                else if (Convert.ToInt32(dtx.Rows[0]["coupontype"]) == 2) //Cashback
                                                {
                                                    TotalAmount = Convert.ToDouble(Usr_RCAMT);
                                                    if (Convert.ToInt32(dtx.Rows[0]["isDiscountflat"]) == 1)
                                                        discount = Convert.ToDouble(dtx.Rows[0]["discountoncoupon"]);
                                                    else
                                                        discount = (TotalAmount * (Convert.ToDouble(dtx.Rows[0]["discountoncoupon"]) / 100));
                                                    NetAmount = TotalAmount;
                                                    Return_DAMT = discount.ToString();
                                                    //litAmount.Text = Convert.ToString(NetAmount);
                                                    //hdnamount.Value = Convert.ToString(NetAmount);
                                                    Return_DESC = "Cashback of Rs. " + discount.ToString() + " will be added in your Recharge Wallet";
                                                    //lblCouponDescription.Text = "Cashback of Rs. " + discount.ToString() + " will be added in your Recharge Wallet after completion of this Recharge Transaction.";
                                                    Response.Write("1,C," + Return_DAMT + "," + NetAmount + "," + Return_DESC + ",");
                                                }
                                            }
                                            else
                                            {
                                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This Coupon code does not match with Terms of Use. Please check Terms and condition of this coupon!!')", true);
                                                Response.Write("0,0,0,0,Coupon code does not match with Terms of Use,");
                                            }
                                        }
                                        else
                                        {
                                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This promo code is not valid.')", true);
                                            Response.Write("0,0,0,0,Promo code is not valid,");
                                        }
                                    }
                                    else if (Convert.ToInt32(dtx.Rows[0]["idno"]) == -1)
                                    {
                                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('This promo code is already used in this week.')", true);
                                        Response.Write("0,0,0,0,Promo code is already used in this week,");
                                    }
                                    else if (Convert.ToInt32(dtx.Rows[0]["idno"]) == -2)
                                    {
                                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Please Check Activation date and Minimum Recharge Amount.')", true);
                                        Response.Write("0,0,0,0,Recheck Activation date and Minimum Recharge Amount,");
                                    }
                                    //Promocode ends here
                                }
                                else
                                {
                                    Response.Write("0,0,0,Invalid Request,");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "COUPONA")
                            {
                                string COUPONCODE = messageArray[3].Trim().ToUpper().Replace(";", "").Replace("-", "");
                                DataTable dt = new DataTable();
                                if (objconnection.select_data_scalar_int("SELECT count(*) FROM tblmlm_MemberCouponCode WHERE msrno='" + dtMemberMaster.Rows[0]["msrno"].ToString() + "'") == 0)
                                {
                                    dt = objconnection.select_data_dt("Exec Offer_validatePromoCoupon_app '" + COUPONCODE + "'");
                                    if (dt.Rows.Count > 0)
                                    {
                                        Response.Write("1," + dt.Rows[0]["id"].ToString() + "," + dt.Rows[0]["couponcode"].ToString() + "," + dt.Rows[0]["categoryid"].ToString() + "," + dt.Rows[0]["categoryname"].ToString() + "," + dt.Rows[0]["imgsrc"].ToString() + "," + dt.Rows[0]["details"].ToString() + "," + dt.Rows[0]["TNC"].ToString() + "," + dt.Rows[0]["couponvalue"].ToString() + ",,,");
                                    }
                                    else
                                    {
                                        Response.Write("0,This Coupon Code is not valid,");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,You have already redeem your coupon,");
                                }
                            }
                            else if (messageArray[1].Trim().ToUpper() == "COUPONB")
                            {
                                string COUPONID = messageArray[3].Trim().ToUpper().Replace(";", "").Replace("-", "");
                                DataTable dt = new DataTable();
                                dt = objconnection.select_data_dt("Exec Offer_UpdatePromoCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + COUPONID + "'");
                                if (Convert.ToInt32(dt.Rows[0]["idno"]) > 0)
                                {
                                    DataTable dtm = new DataTable();
                                    dtm = objconnection.select_data_dt("SELECT top 1 * FROM tblmlm_MemberCouponCode where msrno='" + dtMemberMaster.Rows[0]["msrno"].ToString() + "'");
                                    if (dtm.Rows.Count > 0)
                                    {
                                        string[] valueArray = new string[3];
                                        valueArray[0] = dtMemberMaster.Rows[0]["firstname"].ToString();
                                        valueArray[1] = dtm.Rows[0]["couponcode"].ToString();
                                        valueArray[2] = "JETPAID";
                                        SMS.SendWithVar(dtMemberMaster.Rows[0]["mobile"].ToString(), 27, valueArray, 1);
                                    }
                                    Response.Write("1,Coupon Activated !!," + dtm.Rows[0]["couponcode"].ToString() + ",,");
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Coupon Activated !! Please check your mail.');", true);
                                }
                                else
                                {
                                    Response.Write("1,coupon has been used by someone recently !!,,,");
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('This coupon has been used by someone recently !!');", true);
                                }
                            }
                            else if (messageArray[1].Trim().ToUpper() == "LOADCOUPON")
                            {
                                DataTable dt = new DataTable();
                                dt = objconnection.select_data_dt("SELECT * FROM tblmlm_MemberCouponCode where msrno='" + dtMemberMaster.Rows[0]["msrno"].ToString() + "'");
                                if (dt.Rows.Count > 0)
                                {
                                    Response.Write("1," + dt.Rows[0]["CouponCode"].ToString() + ",coupon has been used by someone recently !!,,,");
                                    //lblMyPromoCode.Text = dt.Rows[0]["CouponCode"].ToString();
                                    //noPromo.Visible = false;
                                }
                                else
                                {
                                    Response.Write("0,,No active Promocode,,,");
                                }
                            }
                            else if (messageArray[1].Trim().ToUpper() == "CLEARPROMO")
                            {
                                string PromoTP = "0";
                                string PromoCode = "0";
                                string DISCOUNT = "0";
                                if (messageArray.Length == 5)
                                {
                                    PromoTP = messageArray[3].ToString().Replace("-", "").Replace(";", ""); ;
                                    if (PromoTP.Trim() != "0")
                                    {
                                        PromoCode = messageArray[4].ToString().Replace("-", "").Replace(";", "");
                                        if (PromoTP.ToUpper() == "DC")
                                        {
                                            objconnection.insert_data("Exec dbo.PromoCode_ReleaseUnused '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode + "'");
                                        }
                                        else if (PromoTP.ToUpper() == "C")
                                        {
                                            objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',-1");
                                        }
                                        else if (PromoTP.ToUpper() == "D")
                                        {
                                            objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',-1");
                                        }
                                    }
                                }
                            }
                            else if (messageArray[1].Trim().ToUpper() == "RSOTP")
                            {
                                //CY Add 
                                #region RESEND_OTP
                                DataTable dt = new DataTable();
                                dt = objconnection.select_data_dt("Select * from  tblmlm_membermaster where mobile='" + messageArray[2].Trim().ToString() + "'");
                                string[] valueArray = new string[2];
                                valueArray[0] = dt.Rows[0]["Firstname"].ToString();
                                valueArray[1] = messageArray[3].Trim().ToString();
                                SMS.SendWithVar(messageArray[2].Trim().ToString(), 21, valueArray, MsrNo);
                                Response.Write("1,Sent Successfully,");
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "CHKPAY")
                            {
                                //CHKPAY historyid
                                string hsid = messageArray[3].Trim();
                                string ss = Convert.ToString(objconnection.select_data_scalar_int("Select count(*) from tbl_paymentGateway_an where hid='" + hsid + "'"));
                                Response.Write(ss + ",SUCCESS,");
                            }
                            else if (messageArray[1].Trim().ToUpper() == "RCREQ")
                            {
                                //CY Add 
                                #region Recharge_request
                                //Response.Write("0,Temperory Server Down..try later,");
                                if (messageArray.Length >= 5)
                                {
                                    string PromoTP = "0";
                                    string PromoCode = "0";
                                    string DISCOUNT = "0";
                                    string amount = messageArray[5].Trim();
                                    string walletAmount = amount;
                                    if (amount != null && amount != "")
                                    {
                                        string number = messageArray[4].Trim();
                                        if (number != null && number != "")
                                        {
                                            string OperatorCode = messageArray[3].Trim();
                                            if (OperatorCode != null && OperatorCode != "")
                                            {
                                                string Trantype = ""; string mycirclecode = "";
                                                DataTable dtOperator = objconnection.select_data_dt("select OperatorID from tblRecharge_Operator where OperatorCode='" + OperatorCode + "'");
                                                int OperatorID = Convert.ToInt32(dtOperator.Rows[0]["OperatorID"]);
                                                int ProfileID = 0;
                                                if (messageArray.Length >= 6)
                                                {
                                                    ProfileID = Convert.ToInt32(messageArray[6].Trim().ToLower());
                                                }
                                                else
                                                {
                                                    ProfileID = 19;
                                                }
                                                string TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
                                                int i = objHistory.AddEditHistory(0, MsrNo, number, "", Convert.ToDecimal(amount), OperatorID, ProfileID, TransID, "", "", "Queued");
                                                if (messageArray.Length >= 7)
                                                {
                                                    PromoTP = messageArray[7].ToString().Replace("-", "").Replace(";", ""); ;
                                                    if (PromoTP.Trim() != "0")
                                                    {
                                                        DISCOUNT = messageArray[9].ToString().Replace("-", "").Replace(";", "");
                                                        PromoCode = messageArray[8].ToString().Replace("-", "").Replace(";", "");
                                                        if (PromoTP.ToUpper() == "DC")
                                                        {
                                                            objconnection.update_data("Exec ApplyPromocode_Recharge_app_chk '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode + "','" + i.ToString() + "'");
                                                            walletAmount = (Convert.ToDecimal(amount) - Convert.ToDecimal(DISCOUNT)).ToString();
                                                        }
                                                        else if (PromoTP.ToUpper() == "C")
                                                        {
                                                            objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',1");
                                                        }
                                                        else if (PromoTP.ToUpper() == "D")
                                                        {
                                                            objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',1");
                                                            walletAmount = (Convert.ToDecimal(amount) - Convert.ToDecimal(DISCOUNT)).ToString();
                                                        }
                                                    }
                                                }
                                                Response.Write("1," + i.ToString() + "," + ProfileID.ToString() + "," + walletAmount + "," + amount + "");
                                            }
                                            else
                                            {
                                                Response.Write("0,Wrong OperatorCode,");
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
                            else if (messageArray[1].Trim().ToUpper() == "WAREQ")
                            {
                                //CY Add 
                                #region Wallet_Add_Request
                                if (messageArray.Length == 4)
                                {
                                    string amount = messageArray[3].Trim();
                                    if (amount != null && amount != "")
                                    {
                                        string number = messageArray[2].Trim();
                                        if (number != null && number != "")
                                        {
                                            DataTable dt = new DataTable();
                                            dt = objconnection.select_data_dt("Select * from tblmlm_membermaster where mobile='" + number + "' and isactive=1 and isdelete=0");
                                            if (dt.Rows.Count > 0)
                                            {
                                                Random random = new Random();
                                                int SixUniqCode = random.Next(100000, 999999);
                                                string TransID = SixUniqCode.ToString();
                                                int i = objconnection.insert_data("insert into tbl_APP_WalletADD values ('" + dt.Rows[0]["msrno"].ToString() + "','" + number + "','" + amount + "','" + TransID + "',0)");
                                                Response.Write("1," + TransID + "," + amount + ",");
                                            }
                                            else
                                            {
                                                Response.Write("0,Wrong Number,");
                                                //SMS.Send(from, "You have enterd wrong number, please check the number and try again.");
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
                            else if (messageArray[1].Trim().ToUpper() == "WSTATUS")
                            {
                                //By Shyam 
                                #region Rstatus
                                #region Recharge_Success
                                if (messageArray.Length >= 3)
                                {
                                    DataTable dt = objconnection.select_data_dt("select * from tbl_paymentGateway_an  WHERE Hid='" + messageArray[3].Trim() + "' and pstatusid=1 and rstatusid=0 ");
                                    if (dt.Rows.Count > 0)
                                    {
                                        string mhistoryID = messageArray[3].Trim();
                                        if (mhistoryID != null && mhistoryID != "")
                                        {
                                            string ProfileID = "0";
                                            //if (ProfileID != null && ProfileID != "")
                                            //{
                                            DataTable dtHistory = new DataTable();
                                            dtHistory = objHistory.ManageHistory("Get", Convert.ToInt32(mhistoryID));
                                            DataTable dtx = new DataTable();
                                            dtx = objconnection.select_data_dt("Select * from tbl_APP_WalletADD where widno='" + messageArray[3].Trim() + "'");
                                            if (dtHistory.Rows.Count > 0)
                                            {
                                                int a = objconnection.insert_data("update  tbl_paymentGateway_an set RstatusId=1 where hid=" + messageArray[3].Trim());
                                                RechargeDone(Convert.ToInt32(mhistoryID), Convert.ToInt32(dtHistory.Rows[0]["MsrNo"]), memberid, PackageID, from, Convert.ToInt32(dtHistory.Rows[0]["OperatorID"]), Convert.ToInt32(ProfileID), Convert.ToString(dtHistory.Rows[0]["RechargeAmount"]), Convert.ToString(dtHistory.Rows[0]["MobileNo"]), "", Convert.ToString(dtHistory.Rows[0]["TransID"]), "0", "0", "0");
                                            }
                                            else if (dtx.Rows.Count > 0)
                                            {
                                                int a = objconnection.insert_data("update  tbl_paymentGateway_an set RstatusId=1 where hid=" + messageArray[3].Trim());
                                                objEWalletTransaction.EWalletTransaction(dtMemberMaster.Rows[0]["MemberID"].ToString(), Convert.ToDecimal(dtx.Rows[0]["Amount"]), "Cr", "Cash Added in wallet by App " + messageArray[2].Trim());
                                                Response.Write("1,Fund Added Successfully,");
                                            }
                                            //}
                                            //else
                                            //{
                                            //    Response.Write("0,Invalid Request,");
                                            //}
                                        }
                                        else
                                        {
                                            Response.Write("0,Invalid Request,");
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("0,payment not done,");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Invalid Request,");
                                }
                                #endregion



                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "RSTATUS")
                            {
                                //By Shyam 
                                #region Rstatus
                                #region Recharge_Success
                                if (messageArray.Length >= 3)
                                {
                                    string PromoTP = "0";
                                    string PromoCode = "0";
                                    string DISCOUNT = "0";
                                    DataTable dt = objconnection.select_data_dt("select * from tbl_paymentGateway_an  WHERE Hid='" + messageArray[3].Trim() + "' and pstatusid=1 and rstatusid=0 ");
                                    if (dt.Rows.Count > 0)
                                    {
                                        string mhistoryID = messageArray[3].Trim();
                                        if (mhistoryID != null && mhistoryID != "")
                                        {
                                            string ProfileID = "0";
                                            //if (ProfileID != null && ProfileID != "")
                                            //{
                                            DataTable dtHistory = new DataTable();
                                            dtHistory = objHistory.ManageHistory("Get", Convert.ToInt32(mhistoryID));
                                            int a = objconnection.insert_data("update  tbl_paymentGateway_an set RstatusId=1 where hid=" + messageArray[3].Trim());
                                            if (messageArray.Length >= 6)
                                            {
                                                PromoTP = messageArray[5].ToString().Replace("-", "").Replace(";", ""); ;
                                                if (PromoTP.Trim() != "0")
                                                {
                                                    DISCOUNT = messageArray[7].ToString().Replace("-", "").Replace(";", "");
                                                    PromoCode = messageArray[6].ToString().Replace("-", "").Replace(";", "");
                                                    //if (PromoTP.ToUpper() == "DC")
                                                    //{
                                                    //    objconnection.update_data("Exec ApplyPromocode_Recharge_app_chk '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode + "','" + i.ToString() + "'");
                                                    //    walletAmount = (Convert.ToDecimal(amount) - Convert.ToDecimal(DISCOUNT)).ToString();
                                                    //}
                                                    //else if (PromoTP.ToUpper() == "C")
                                                    //{
                                                    //    objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',1");
                                                    //}
                                                    //else if (PromoTP.ToUpper() == "D")
                                                    //{
                                                    //    objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',1");
                                                    //    walletAmount = (Convert.ToDecimal(amount) - Convert.ToDecimal(DISCOUNT)).ToString();
                                                    //}
                                                }
                                            }
                                            RechargeDone(Convert.ToInt32(mhistoryID), Convert.ToInt32(dtHistory.Rows[0]["MsrNo"]), memberid, PackageID, from, Convert.ToInt32(dtHistory.Rows[0]["OperatorID"]), Convert.ToInt32(ProfileID), Convert.ToString(dtHistory.Rows[0]["RechargeAmount"]), Convert.ToString(dtHistory.Rows[0]["MobileNo"]), "", Convert.ToString(dtHistory.Rows[0]["TransID"]), PromoTP, DISCOUNT, PromoCode);

                                            //}
                                            //else
                                            //{
                                            //    Response.Write("0,Invalid Request,");
                                            //}
                                        }
                                        else
                                        {
                                            Response.Write("0,Invalid Request,");
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("0,payment not done,");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Invalid Request,");
                                }
                                #endregion



                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "RCSUCC")
                            {
                                //CY Add 
                                #region Recharge_Success
                                if (messageArray.Length >= 3)
                                {
                                    string mhistoryID = messageArray[2].Trim();
                                    if (mhistoryID != null && mhistoryID != "")
                                    {
                                        string ProfileID = messageArray[3].Trim();
                                        //if (ProfileID != null && ProfileID != "")
                                        //{
                                        DataTable dtHistory = new DataTable();
                                        dtHistory = objHistory.ManageHistory("Get", Convert.ToInt32(mhistoryID));
                                        //RechargeDone(Convert.ToInt32(mhistoryID), Convert.ToInt32(dtHistory.Rows[0]["MsrNo"]), memberid, PackageID, from, Convert.ToInt32(dtHistory.Rows[0]["OperatorID"]), Convert.ToInt32(ProfileID), Convert.ToString(dtHistory.Rows[0]["RechargeAmount"]), Convert.ToString(dtHistory.Rows[0]["MobileNo"]), "", Convert.ToString(dtHistory.Rows[0]["TransID"]));
                                        //}
                                        //else
                                        //{
                                        //    Response.Write("0,Invalid Request,");
                                        //}
                                    }
                                    else
                                    {
                                        Response.Write("0,Invalid Request,");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Invalid Request,");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "RCFAIL")
                            {
                                //CY Add 
                                #region Recharge_FAIL
                                if (messageArray.Length >= 6)
                                {
                                    string mhistoryID = messageArray[2].Trim();
                                    if (mhistoryID != null && mhistoryID != "")
                                    {
                                        string ProfileID = messageArray[3].Trim();
                                        //if (ProfileID != null && ProfileID != "")
                                        //{
                                        string TxID, ErrorCode, OperatorRef;
                                        TxID = messageArray[4].Trim();
                                        ErrorCode = messageArray[5].Trim();
                                        OperatorRef = messageArray[6].Trim();
                                        DataTable dtHistory = new DataTable();
                                        dtHistory = objHistory.ManageHistory("Get", Convert.ToInt32(mhistoryID));
                                        //objEWalletTransaction.EWalletTransaction(memberid, Convert.ToDecimal(dtHistory.Rows[0]["RechargeAmount"]), "Cr", "Return amount - " + dtHistory.Rows[0]["TransID"].ToString());
                                        DataTable i = objHistory.UpdateHistory("UpdateStatus", Convert.ToInt32(mhistoryID), 0, 0, 0, 0, 0, "", "", "Failed", TxID, ErrorCode, OperatorRef);
                                        Response.Write("0," + dtHistory.Rows[0]["TransID"].ToString() + ",Failure,RQF," + OperatorRef + "," + DateTime.Now + ",");
                                        //}
                                        //else
                                        //{
                                        //    Response.Write("0,Invalid Request,");
                                        //}
                                    }
                                    else
                                    {
                                        Response.Write("0,Invalid Request,");
                                    }
                                }
                                else
                                {
                                    Response.Write("0,Invalid Request,");
                                }
                                #endregion
                            }
                            else if (messageArray[1].Trim().ToUpper() == "REPEAT")
                            {
                                if (messageArray.Length == 5)
                                {
                                    DataTable dth = new DataTable();
                                    dth = objconnection.select_data_dt("Select * from tblrecharge_history where historyid='" + messageArray[3].Trim().Replace("-", "").Replace(";", "") + "' and msrno='" + MsrNo + "'");
                                    if (dth.Rows.Count > 0)
                                    {
                                        string PromoTP = "0";
                                        string PromoCode = "0";
                                        string DISCOUNT = "0";
                                        string amount = dth.Rows[0]["RechargeAmount"].ToString();
                                        string walletAmount = amount;
                                        if (amount != null && amount != "")
                                        {
                                            string number = dth.Rows[0]["Mobileno"].ToString();
                                            if (number != null && number != "")
                                            {
                                                string OperatorCode = messageArray[4].Trim().Replace("-", "").Replace(";", "");
                                                if (OperatorCode != null && OperatorCode != "")
                                                {
                                                    string Trantype = ""; string mycirclecode = "";

                                                    DataTable dtOperator = objconnection.select_data_dt("select OperatorID from tblRecharge_Operator where OperatorCode='" + OperatorCode + "' and isactive=1 and isdelete=0");
                                                    if (dtOperator.Rows.Count > 0)
                                                    {
                                                        int OperatorID = Convert.ToInt32(dtOperator.Rows[0]["OperatorID"]);
                                                        int ProfileID = 0;
                                                        if (messageArray.Length >= 6)
                                                        {
                                                            ProfileID = Convert.ToInt32(messageArray[5].Trim().ToLower());
                                                        }
                                                        else
                                                        {
                                                            ProfileID = 19;
                                                        }
                                                        if (clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), MsrNo) == 1)
                                                        {
                                                            string TransID = clsm.Cyrus_GetTransactionID_New();
                                                            int i = objHistory.AddEditHistory(0, MsrNo, number, "", Convert.ToDecimal(amount), OperatorID, ProfileID, TransID, "", "", "Queued");
                                                            if (messageArray.Length > 6)
                                                            {
                                                                PromoTP = messageArray[6].ToString().Replace("-", "").Replace(";", ""); ;
                                                                if (PromoTP.Trim() != "0")
                                                                {
                                                                    DISCOUNT = messageArray[8].ToString().Replace("-", "").Replace(";", "");
                                                                    PromoCode = messageArray[7].ToString().Replace("-", "").Replace(";", "");
                                                                    if (PromoTP.ToUpper() == "DC")
                                                                    {
                                                                        objconnection.update_data("Exec ApplyPromocode_Recharge_app_chk '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode + "','" + i.ToString() + "'");
                                                                        walletAmount = (Convert.ToDecimal(amount) - Convert.ToDecimal(DISCOUNT)).ToString();
                                                                    }
                                                                    else if (PromoTP.ToUpper() == "C")
                                                                    {
                                                                        objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',1");
                                                                    }
                                                                    else if (PromoTP.ToUpper() == "D")
                                                                    {
                                                                        objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',1");
                                                                        walletAmount = (Convert.ToDecimal(amount) - Convert.ToDecimal(DISCOUNT)).ToString();
                                                                    }
                                                                }

                                                            }
                                                            objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(walletAmount), "Dr", "Recharge to " + Convert.ToString(number) + " TxnID : " + TransID + "");
                                                            RechargeDone(i, MsrNo, memberid, PackageID, from, OperatorID, ProfileID, amount, number, Trantype, TransID, PromoTP.ToUpper(), DISCOUNT, PromoCode.ToUpper());
                                                        }
                                                        else
                                                        {
                                                            Response.Write("0,Insufficient Fund,");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        SMS.Send(from, "You have entered wrong OperatorCode, please check the OperatorCode and try again.", MsrNo);
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
                                        Response.Write("0,Wrong Message,");
                                }
                                else
                                {
                                    Response.Write("0,Wrong Message,");
                                    //SMS.Send(from, "You have entered wrong message, please check the message and try again.");
                                }
                            }
                            else
                            {
                                #region Recharge
                                #region recharge latest
                                // CY OPERATORCODE ProfileCODE NUMBER AMOUNT RCTP TT
                                //S DT699101 AT 7737251804 10 19
                                if (messageArray.Length >= 5)
                                {
                                    string PromoTP = "0";
                                    string PromoCode = "0";
                                    string DISCOUNT = "0";
                                    string amount = messageArray[4].Trim();
                                    string walletAmount = amount;
                                    if (amount != null && amount != "")
                                    {
                                        string number = messageArray[3].Trim();
                                        if (number != null && number != "")
                                        {
                                            string OperatorCode = messageArray[2].Trim();
                                            if (OperatorCode != null && OperatorCode != "")
                                            {
                                                string Trantype = ""; string mycirclecode = "";

                                                DataTable dtOperator = objconnection.select_data_dt("select OperatorID from tblRecharge_Operator where OperatorCode='" + OperatorCode + "' and isactive=1 and isdelete=0");
                                                if (dtOperator.Rows.Count > 0)
                                                {
                                                    int OperatorID = Convert.ToInt32(dtOperator.Rows[0]["OperatorID"]);
                                                    int ProfileID = 0;
                                                    if (messageArray.Length >= 6)
                                                    {
                                                        ProfileID = Convert.ToInt32(messageArray[5].Trim().ToLower());
                                                    }
                                                    else
                                                    {
                                                        ProfileID = 19;
                                                    }
                                                    if (clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), MsrNo) == 1)
                                                    {
                                                        string TransID = clsm.Cyrus_GetTransactionID_New();
                                                        int i = objHistory.AddEditHistory(0, MsrNo, number, "", Convert.ToDecimal(amount), OperatorID, ProfileID, TransID, "", "", "Queued");
                                                        if (messageArray.Length > 6)
                                                        {
                                                            PromoTP = messageArray[6].ToString().Replace("-", "").Replace(";", ""); ;
                                                            if (PromoTP.Trim() != "0")
                                                            {
                                                                DISCOUNT = messageArray[8].ToString().Replace("-", "").Replace(";", "");
                                                                PromoCode = messageArray[7].ToString().Replace("-", "").Replace(";", "");
                                                                if (PromoTP.ToUpper() == "DC")
                                                                {
                                                                    objconnection.update_data("Exec ApplyPromocode_Recharge_app_chk '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode + "','" + i.ToString() + "'");
                                                                    walletAmount = (Convert.ToDecimal(amount) - Convert.ToDecimal(DISCOUNT)).ToString();
                                                                }
                                                                else if (PromoTP.ToUpper() == "C")
                                                                {
                                                                    objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',1");
                                                                }
                                                                else if (PromoTP.ToUpper() == "D")
                                                                {
                                                                    objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',1");
                                                                    walletAmount = (Convert.ToDecimal(amount) - Convert.ToDecimal(DISCOUNT)).ToString();
                                                                }
                                                            }

                                                        }
                                                        objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(walletAmount), "Dr", "Recharge to " + Convert.ToString(number) + " TxnID : " + TransID + "");
                                                        RechargeDone(i, MsrNo, memberid, PackageID, from, OperatorID, ProfileID, amount, number, Trantype, TransID, PromoTP.ToUpper(), DISCOUNT, PromoCode.ToUpper());
                                                    }
                                                    else
                                                    {
                                                        Response.Write("0,Insufficient Fund,");
                                                    }
                                                }
                                                else
                                                {
                                                    SMS.Send(from, "You have entered wrong OperatorCode, please check the OperatorCode and try again.", MsrNo);
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
    private void RechargeDone(int HistoryID, int MsrNo, string memberid, int PackageID, string from, int OperatorID, int ProfileID, string amount, string number, string account, string TransID, string PromoTP, string discount, string PromoCode)
    {
        try
        {

             DataTable dtt = new DataTable();
            dtt = objconnection.select_data_dt("Exec ChkRechargePayment '" + HistoryID.ToString() + "','" + TransID.ToString() + "'");
            if (Convert.ToInt32(dtt.Rows[0][0]) > 0)
            {



                objconnection.insert_data("insert into tblrecharge_app values ('" + HistoryID.ToString() + "')");
                DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where Mobile='" + from + "'");
                string Recharge_Result = clsm.Cyrus_RechargeProcess(HistoryID, Convert.ToString(ProfileID), account, dtMemberMaster);
                char Splitter = Convert.ToChar(",");
                string[] split = Recharge_Result.Split(Splitter);
                if (split[0] == "Recharge Successful !!")
                {
                    if (PromoTP.ToUpper() == "C")
                    {
                        decimal CashbackAmount = Convert.ToDecimal(discount);
                        objEWalletTransaction.EWalletTransaction(dtMemberMaster.Rows[0]["MemberId"].ToString(), CashbackAmount, "Cr", "Coupon used on this Recharge on " + number + " Txn:" + TransID);
                    }
                    Response.Write("1," + TransID + ",Success," + split[2].ToString() + "," + split[3].ToString() + "," + DateTime.Now + ",");
                }
                else if (split[0] == "Recharge Failed !!")
                {
                    if (PromoTP.ToUpper() == "C")
                    {
                        objconnection.update_data("Exec Update_usedCoupon '" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + PromoCode.ToUpper() + "',-1");
                    }
                    Response.Write("0," + TransID + ",Failure," + split[2].ToString() + "," + split[3].ToString() + "," + DateTime.Now + ",");
                }
                else if (split[0] == "Recharge Pending !!")
                {
                    Response.Write("0," + TransID + ",Requested," + split[2].ToString() + "," + split[3].ToString() + "," + DateTime.Now + ",");
                }


            }
            else
                Response.Write("0,Go and Fuck your self,FUCK,PEN,," + DateTime.Now + ",");

        }
        catch (Exception ex)
        {
            Response.Write("0," + TransID + ",Requested,PEN,," + DateTime.Now + ",");
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
    #region registration
    protected void registration(string mob, string email, string pass)
    {
        // CY LAST
        #region Registration
        // DataTable dt = objOperator.ManageOperator("GetByServiceTypeID", Convert.ToInt32(messageArray[3].Trim().ToString()));
        //  DataTable dt = objconnection.select_data_dt("Select OperatorCode,OperatorName from tblRecharge_Operator where ServiceTypeID='" + Convert.ToInt32(messageArray[3].Trim().ToString()) + "' and  IsActive='true' and  IsDelete='false' order by OperatorName asc"); //objOperator.ManageOperator("GetByServiceTypeID", Convert.ToInt32(messageArray[3].Trim().ToString()));

        // string output = ConvertDataTabletoString(dt);
        //Response.Write("{ Country:" + output + "},");

        Int32 intresult = 0;
        DateTime date = DateTime.Now;
        Random random = new Random();
        int SixDigit = random.Next(100000, 999999);
        int SixDigitOTP = random.Next(100000, 999999);
        string MemberID = "CT" + SixDigit;
        string[] name = email.Split('@');
        string FName = name[0];
        intresult = objMemberMaster.AddEditMemberMaster(0, MemberID, FName, "", email, date, "", pass, SixDigit.ToString().Substring(0, 4), mob, "", "", "", "", 1, 0, 0, "", "", "Customer", 6, 1, 1, "");

        if (intresult > 0)
        {
            objconnection.update_data("Update tblmlm_membermaster set isactive=0 where msrno='" + intresult.ToString() + "'");
            //RegisterMail.Customer(FName, email, mob, pass, SixDigit.ToString().Substring(0, 4));
            string[] valueArray = new string[2];
            valueArray[0] = FName;
            valueArray[1] = SixDigitOTP.ToString();
            SMS.SendWithVar(mob, 21, valueArray, intresult);
            //dtUniversal = objUniversal.UniversalLogin("LoginCustomer", txtRegisterEmail.Text.Trim().Replace("'", "").ToString(), txtRegisterPassword.Text.Trim().Replace("'", "").ToString());
            // if (dtUniversal.Rows.Count > 0)
            //{
            //  Session.Add("dtMember", dtUniversal);
            // fillCustomerDetail();
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "disablePopup();alert('Welcome, All details has been sent at your email !');", true);
            // }
            Response.Write("1,Success," + MemberID + "," + pass + "," + SixDigitOTP.ToString() + ",");
            return;
        }
        else
        {
            Response.Write("0,Failed," + MemberID + "," + pass + "," + SixDigitOTP.ToString() + ",");
            return;
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('This email or mobile no already register !');", true);
        }
        #endregion

    }
    protected void registrationV(string mob, string otp)
    {
        // CY LAST
        #region Registration
        // DataTable dt = objOperator.ManageOperator("GetByServiceTypeID", Convert.ToInt32(messageArray[3].Trim().ToString()));
        //  DataTable dt = objconnection.select_data_dt("Select OperatorCode,OperatorName from tblRecharge_Operator where ServiceTypeID='" + Convert.ToInt32(messageArray[3].Trim().ToString()) + "' and  IsActive='true' and  IsDelete='false' order by OperatorName asc"); //objOperator.ManageOperator("GetByServiceTypeID", Convert.ToInt32(messageArray[3].Trim().ToString()));

        // string output = ConvertDataTabletoString(dt);
        //Response.Write("{ Country:" + output + "},");

        DataTable dt = new DataTable();
        Int32 intresult = 0;
        dt = objconnection.select_data_dt("select * from tblmlm_membermaster where memberid='" + mob + "' or mobile='" + mob + "'");
        intresult = objconnection.update_data("Update tblmlm_membermaster set isactive=1 where msrno='" + dt.Rows[0]["msrno"].ToString() + "'");
        //if (intresult > 0)
        //{
        RegisterMail.Customer(dt.Rows[0]["firstname"].ToString(), dt.Rows[0]["email"].ToString(), dt.Rows[0]["mobile"].ToString(), dt.Rows[0]["password"].ToString(), dt.Rows[0]["transactionpassword"].ToString(), dt.Rows[0]["memberid"].ToString());
        string[] valueArray = new string[1];
        valueArray[0] = dt.Rows[0]["firstname"].ToString();
        SMS.SendWithVar(dt.Rows[0]["mobile"].ToString(), 9, valueArray, Convert.ToInt32(dt.Rows[0]["Msrno"].ToString()));
        //dtUniversal = objUniversal.UniversalLogin("LoginCustomer", txtRegisterEmail.Text.Trim().Replace("'", "").ToString(), txtRegisterPassword.Text.Trim().Replace("'", "").ToString());
        // if (dtUniversal.Rows.Count > 0)
        //{
        //  Session.Add("dtMember", dtUniversal);
        // fillCustomerDetail();
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "disablePopup();alert('Welcome, All details has been sent at your email !');", true);
        // }
        Response.Write("1,Success," + dt.Rows[0]["memberid"].ToString() + "," + dt.Rows[0]["password"].ToString() + ",");
        return;
        //}
        //else
        //{
        //    Response.Write("0,Failed," + dt.Rows[0]["memberid"].ToString() + "," + dt.Rows[0]["password"].ToString());
        //    return;
        //    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('This email or mobile no already register !');", true);
        //}
        #endregion

    }

    protected void RSOTPSend(string Mobile, string otp)
    {
        #region RESEND_OTP
        DataTable dt = new DataTable();
        dt = objconnection.select_data_dt("Select * from  tblmlm_membermaster where (mobile='" + Mobile + "' OR MemberID='" + Mobile + "')");
        if (dt.Rows.Count > 0)
        {
            string[] valueArray = new string[2];
            valueArray[0] = dt.Rows[0]["Firstname"].ToString();
            valueArray[1] = otp;
            SMS.SendWithVar(dt.Rows[0]["mobile"].ToString(), 21, valueArray, Convert.ToInt32(dt.Rows[0]["Msrno"]));
            Response.Write("1,Sent Successfully,");
            return;
        }
        else
        {
            Response.Write("0,Failed,");
            return;
        }
        #endregion

    }
    #endregion
}