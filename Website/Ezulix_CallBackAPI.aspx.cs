using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
public partial class Ezulix_CallBackAPI : System.Web.UI.Page
{
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();

    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();

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
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack && Request.QueryString["status"] != null)
        //{
        //    if (Request.QueryString["status"].ToString().ToLower() == "dmr")
        //    {
        //        if (Request.QueryString["status"] != null && Request.QueryString["txid"] != null && Request.QueryString["mark"] != null)
        //        {
        //            cls.select_data_dt("Update MM_FundTransfer set statuscode='refundable' where transactionstatus='" + Request.QueryString["txid"].ToString() + "'");
        //        }
        //    }
        //    else if (Request.QueryString["status"].ToString().ToLower() == "dmr_refund")
        //    {
        //        if (Request.QueryString["status"] != null && Request.QueryString["txid"] != null && Request.QueryString["mark"] != null)
        //        {
        //            cls.select_data_dt("Update MM_FundTransfer set statuscode='refundable' where TXNID='" + Request.QueryString["txid"].ToString() + "'");
        //            cls.select_data_dt("Exec DMR_QuickRefund '" + Request.QueryString["txid"].ToString() + "','" + Request.QueryString["mark"].ToString() + "'");
        //        }
        //    }
        //    else
        //    {
                if (Request.QueryString["mytxid"] != null && Request.QueryString["txid"] != null && Request.QueryString["optxid"] != null && Request.QueryString["mobileno"] != null && Request.QueryString["status"] != null)
                {
                    DataTable dtHistory = new DataTable();
                    dtHistory = cls.select_data_dt("Exec Ravi_Bonrix_update '" + Request.QueryString["txid"].ToString() + "','" + Request.QueryString["mobileno"] + "'");
                    if (dtHistory.Rows.Count > 0)
                    {
                        //dtHistory = objHistory.ManageHistory("GetAllbyCTranIDx", Convert.ToInt32(Request.QueryString["txid"].ToString()));
                        string mymobile = cls.select_data_scalar_string("Select mobile from tblmlm_membermaster where msrno='" + dtHistory.Rows[0]["msrno"].ToString() + "'");
                        if (Request.QueryString["status"].ToString().Trim().ToLower() == "success")
                        {
                            DataTable i = objHistory.UpdateHistory("UpdateStatus", Convert.ToInt32(dtHistory.Rows[0]["historyid"].ToString()), Convert.ToInt32(dtHistory.Rows[0]["msrno"].ToString()), 0, 0, 0, 0, "", "", "Success", Request.QueryString["mytxid"].ToString(), "", Request.QueryString["optxid"].ToString());
                            //Session["OrderID"] = Request.QueryString["mytxid"].ToString();
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Successful !');location.replace('Invoice.aspx');", true);
                            DataTable dtmybalance = new DataTable();

                            dtmybalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(dtHistory.Rows[0]["msrno"].ToString()));
                            string[] valueArray = new string[4];
                            valueArray[0] = dtHistory.Rows[0]["mobileno"].ToString();
                            valueArray[1] = dtHistory.Rows[0]["rechargeamount"].ToString();
                            valueArray[2] = dtHistory.Rows[0]["TransID"].ToString();
                            valueArray[3] = dtmybalance.Rows[0]["Balance"].ToString();
                            SMS.SendWithVar(mymobile, 16, valueArray, 1);
                            Response.Write("Recharge status has been set as Success");
                        }
                        else if (Request.QueryString["status"].ToString().Trim().ToLower() == "failed")
                        {
                            objEWalletTransaction.EWalletTransaction(Convert.ToString(dtHistory.Rows[0]["Memberid"].ToString()), Convert.ToDecimal(dtHistory.Rows[0]["rechargeamount"].ToString()), "Cr", "Refund amount due to recharge failure - " + dtHistory.Rows[0]["mobileno"].ToString() + " (Txn ID : " + Request.QueryString["txid"].ToString() + ")");
                            DataTable i = objHistory.UpdateHistory("UpdateStatus", Convert.ToInt32(dtHistory.Rows[0]["historyid"].ToString()), 0, 0, 0, 0, 0, "", "", "Failed", Convert.ToString(Request.QueryString["mytxid"].ToString()), "Recharge Failed from operator", Request.QueryString["optxid"].ToString());
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Failed !');location.replace('Recharge_ListHistory.aspx');", true);
                            string[] valueArray = new string[4];
                            valueArray[0] = dtHistory.Rows[0]["rechargeamount"].ToString();
                            valueArray[1] = dtHistory.Rows[0]["mobileno"].ToString();
                            valueArray[2] = dtHistory.Rows[0]["TransID"].ToString();
                            SMS.SendWithVar(mymobile, 3, valueArray, 1);
                            Response.Write("Recharge status has been set as Failed");
                        }
                    }
                    else
                    { Response.Write("Recharge is not in pending status now"); }
                }
            }
        }
  