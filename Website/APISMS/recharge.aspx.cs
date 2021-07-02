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

public partial class Recharge : System.Web.UI.Page
{
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();

    clsRecharge_History objHistory = new clsRecharge_History();

    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();

    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();

    clsRecharge_Circle objCircle = new clsRecharge_Circle();
    DataTable dtCircle = new DataTable();

    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();

    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();

    cls_connection objconnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["memberid"] != null)
            {
                string memberid = Request.QueryString["memberid"];
                DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where MemberID='" + memberid + "'");
                if (dtMemberMaster.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtMemberMaster.Rows[0]["IsActive"]) == true)
                    {
                        if (Request.QueryString["password"] != null)
                        {
                            if (Convert.ToString(dtMemberMaster.Rows[0]["Password"]) == Request.QueryString["password"])
                            {
                                if (Request.QueryString["amount"] != null)
                                {
                                    if (Request.QueryString["number"] != null)
                                    {
                                        if (Request.QueryString["OperatorID"] != null && Request.QueryString["CircleID"] != null)
                                        {
                                            string password = Request.QueryString["password"];
                                            string amount = Request.QueryString["amount"];
                                            string number = Request.QueryString["number"];

                                            string account = "";
                                            if (Request.QueryString["account"] != null)
                                                account = Request.QueryString["account"];

                                            int OperatorID = Convert.ToInt32(Request.QueryString["OperatorID"]);
                                            int CircleID = Convert.ToInt32(Request.QueryString["CircleID"]);

                                            int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                                            int PackageID = Convert.ToInt32(dtMemberMaster.Rows[0]["PackageID"]);

                                            dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", MsrNo);
                                            if (Convert.ToDecimal(amount) > Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                            {
                                                Response.Write("0,FAILURE,LWB, ," + DateTime.Now);
                                            }
                                            else
                                            {
                                                Random rnd = new Random();
                                                Int64 month = rnd.Next(1000, 9999);
                                                //month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(1000, 9999)));
                                                //month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(1000, 9999)));
                                                //month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(1000, 9999)));
                                                string TransID = Convert.ToString(month.ToString() + Convert.ToString(rnd.Next(1000, 9999)));
                                                //string TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20).ToUpper();
                                                //string TransID = Convert.ToString(month);
                                                int i = objHistory.AddEditHistory(0, MsrNo, number, account, Convert.ToDecimal(amount), OperatorID, CircleID, TransID, "", "", "Queued");
                                                objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Recharge to " + number);
                                                RechargeDone(i, MsrNo, memberid, PackageID, OperatorID, CircleID, amount, number, account, TransID);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("0,FAILURE,IVN, ," + DateTime.Now);
                                    }
                                }
                                else
                                {
                                    Response.Write("0,FAILURE,IVA, ," + DateTime.Now);
                                }
                            }
                            else
                            {
                                Response.Write("0,FAILURE,IVP, ," + DateTime.Now);
                            }
                        }
                        else 
                        {
                            Response.Write("0,FAILURE,IVP, ," + DateTime.Now);
                        }
                    }
                    else                     
                    {
                        Response.Write("0,FAILURE,IAU, ," + DateTime.Now);
                    }
                }
                else
                {
                    Response.Write("0,FAILURE,IVU, ," + DateTime.Now);
                }
            }
            else
            {
                Response.Write("0,FAILURE,IVU, ," + DateTime.Now);
            }
        }
    }

    private void RechargeDone(int HistoryID, int MsrNo, string memberid, int PackageID, int OperatorID, int CircleID, string amount, string number, string account, string TransID)
    {
        try
        {
            DataTable dttAPI = objconnection.select_data_dt("select ActiveAPI from tblRecharge_Commission where OperatorID=" + OperatorID + " and PackageID=" + PackageID);
            int APIID = Convert.ToInt32(dttAPI.Rows[0]["ActiveAPI"]);
            DataTable dtOperatorCode = objconnection.select_data_dt("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + OperatorID + "and APIID=" + APIID);
            DataTable dtCircleCode = objconnection.select_data_dt("select CircleCode from tblRecharge_CircleCode where CircleID=" + CircleID + "and APIID=" + APIID);

            string OperatorCode = Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"]);
            string CircleCode = Convert.ToString(dtCircleCode.Rows[0]["CircleCode"]);

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
                    str = str + dtAPI.Rows[0]["prm5"].ToString() + "=" + CircleCode + "&";
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
                    Response.Write(TransID + ",Success," + ErrorCode + "," + OperatorRef + "," + DateTime.Now);
                }
                else if (Status.ToLower() == Failed.ToLower() || Status.ToLower() == "103" || Status.IndexOf("last 3 Hour") > 0)
                {
                    objEWalletTransaction.EWalletTransaction(memberid, Convert.ToDecimal(amount), "Cr", "Return amount");
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Failed", TxID, ErrorCode, OperatorRef);
                    Response.Write(TransID + ",Failure,RQF," + OperatorRef + "," + DateTime.Now);
                }
                else if (Status.ToLower() == Pending.ToLower())
                {
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", HistoryID, 0, 0, 0, 0, 0, "", "", "Pending", TxID, "", OperatorRef);
                    Response.Write(TransID + ",Pending,PEN," + OperatorRef + "," + DateTime.Now);
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
        catch
        {
            return "0";
        }
    }
}