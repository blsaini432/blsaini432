using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BLL;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Configuration;
using BLL.MLM;
/// <summary>
/// Summary description for cls_myMember
/// </summary>
public class cls_myMember
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

    cls_connection cls = new cls_connection();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    public cls_myMember()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    public string Get_Member_RecoverPass(int msrno)
    {
        string returndiv = "";
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select * from tblmlm_membermaster where msrno='" + msrno.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup2tbl'>";
            returndiv += "<tr><td>Member ID :</td><td>" + dt.Rows[0]["MemberID"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Member Name :</td><td>" + dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["Lastname"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Email :</td><td>" + dt.Rows[0]["Email"] + "</td></tr>";
            returndiv += "<tr><td>Password :</td><td>" + dt.Rows[0]["password"] + "</td></tr>";
            returndiv += "<tr><td>Transaction Password :</td><td>" + dt.Rows[0]["transactionPassword"] + "</td></tr>";
            returndiv += "</table></div>";
        }
        else
        {
            returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><h2 style='text-align:center'>No result found</h2></div>";
        }
        return returndiv;
    }
    public string Get_Member_WalletInfo(int msrno)
    {
        string returndiv = "";
        DataTable dt = new DataTable();
        DataTable dtwallet = new DataTable();
        dtwallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", msrno);
        dt = cls.select_data_dt("Select * from tblmlm_membermaster where msrno='" + msrno.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup2tbl'>";
            returndiv += "<tr><td>Member ID :</td><td>" + dt.Rows[0]["MemberID"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Member Name :</td><td>" + dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["Lastname"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Wallet Balance :</td><td>" + Convert.ToString(dtwallet.Rows[0]["Balance"]) + "</td></tr>";
            //returndiv += "<tr><td>Transaction Password :</td><td>" + dt.Rows[0]["transactionPassword"] + "</td></tr>";
            returndiv += "</table></div>";
        }
        else
        {
            returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><h2 style='text-align:center'>No result found</h2></div>";
        }
        return returndiv;
    }
    public string Get_Member_ResellerInfo(int msrno)
    {
        string returndiv = "";
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select * from tblmlm_membermaster where msrno='" + msrno.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup2tbl'>";
            returndiv += "<tr><td>Member ID :</td><td>" + dt.Rows[0]["MemberID"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Member Name :</td><td>" + dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["Lastname"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Email :</td><td>" + dt.Rows[0]["Email"] + "</td></tr>";
            returndiv += "<tr><td>Password :</td><td>" + dt.Rows[0]["password"] + "</td></tr>";
            returndiv += "<tr><td>Website :</td><td>" + dt.Rows[0]["s_address"] + "</td></tr>";
            returndiv += "</table></div>";
        }
        else
        {
            returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><h2 style='text-align:center'>No result found</h2></div>";
        }
        return returndiv;
    }
    public int Wallet_Addfund(int msrno, string memberid, decimal amount, string narr, string mobile)
    {
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
        int cnt = cls.select_data_scalar_int("select 1 from tblmlm_membermaster where memberid='" + memberid + "' and isactive=1 and isdelete=0");
        if (cnt > 0)
        {

            objEWalletTransaction.EWalletTransaction(memberid, amount, "Cr", narr);
            DataTable dtwallet = new DataTable();
            dtwallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", msrno);
            string[] valueArray = new string[2];
            valueArray[0] = amount.ToString();
            valueArray[1] = Convert.ToString(dtwallet.Rows[0]["balance"]);
            DLTSMS.SendWithVar(mobile, 4, valueArray, msrno);
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public int Wallet_AddfundInRWallet(int msrno, string memberid, decimal amount, string narr, string mobile)
    {
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
        int cnt = cls.select_data_scalar_int("select 1 from tblmlm_membermaster where memberid='" + memberid + "' and isactive=1 and isdelete=0");
        if (cnt > 0)
        {
            cls.select_data_dt("Exec ProcMLM__RWalletTransaction '" + memberid + "', " + amount + ",'Cr', '" + narr + "'");
            DataTable dtwallet = new DataTable();
            dtwallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNoInRwallet", msrno);
            string[] valueArray = new string[2];
            valueArray[0] = amount.ToString();
            valueArray[1] = Convert.ToString(dtwallet.Rows[0]["balance"]);
            DLTSMS.SendWithVar(mobile, 4, valueArray, msrno);
            return 1;


        }
        else
        {
            return 0;
        }
    }

    public int Wallet_Deductfund(int msrno, string memberid, decimal amount, string narr, string mobile)
    {
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
        objEWalletTransaction.EWalletTransaction(memberid, Convert.ToDecimal("-" + amount.ToString()), "Dr", narr);
        DataTable dtwallet = new DataTable();
        dtwallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", msrno);
        string[] valueArray = new string[2];
        valueArray[0] = amount.ToString();
        valueArray[1] = Convert.ToString(dtwallet.Rows[0]["balance"]);
        DLTSMS.SendWithVar(mobile, 3, valueArray, msrno);
        return 1;
    }
    public int Wallet_MakeTransaction(string memberid, decimal amount, string Factor, string narr)
    {
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        DataTable dt = objEWalletTransaction.EWalletTransaction_Ezulix(memberid, amount, Factor, narr);
        if (dt.Rows[0]["idno"].ToString() == "1")
            return 1;
        else
            return 0;
    }
    public int Wallet_MakeTransaction_Ezulix(string memberid, decimal amount, string Factor, string narr)
    {
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        DataTable dt = objEWalletTransaction.EWalletTransaction_Ezulix(memberid, amount, Factor, narr);
        if (dt.Rows[0]["idno"].ToString() == "1")
            return 1;
        else
            return 0;
    }

    public int AEPSWallet_MakeTransaction_Ezulix(string memberid, decimal amount, string Factor, string narr)
    {
        clsMLM_RWalletTransaction objEWalletTransaction = new clsMLM_RWalletTransaction();
        DataTable dt = objEWalletTransaction.RWalletTransaction(memberid, amount, Factor, narr);
        if (dt.Rows[0]["idno"].ToString() == "1")
            return 1;
        else
            return 0;
    }

    public int New_Wallet_MakeTransaction(string memberid, decimal amount, string Factor, string narr)
    {
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        DataTable dt = objEWalletTransaction.EWalletTransaction_Ezulix(memberid, amount, Factor, narr);
        if (dt.Rows[0]["idno"].ToString() == "1")
            return 1;
        else
            return 0;
    }

    public int Cyrus_ChangeMobile(string mobile, int Msrno)
    {
        int x = cls.select_data_scalar_int("Select count(*) from tblmlm_membermaster where mobile='" + mobile + "' and msrno<>'" + Msrno.ToString() + "'");
        if (x == 0)
        {
            int i = cls.update_data("update tblMLM_MemberMaster set Mobile ='" + mobile + "' where MsrNo=" + Msrno.ToString());
            string[] valueArray = new string[1];
            valueArray[0] = mobile;
            SMS.SendWithVar(mobile, 6, valueArray, Msrno);
            return 1;
        }
        else
        { return 0; }
    }
    public DataTable Cyrus_GetMyPackageList(int msrno)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select PackageName,PackageID from tblmlm_package where MsrNo='" + msrno.ToString() + "' and IsActive=1 and IsDelete=0");
        return dt;
    }
    public DataTable Cyrus_GetMyLastTenTransactions(int msrno)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select top 10 * from tblRecharge_History where MsrNo=" + msrno.ToString() + " and Status<>'Queued' order by HistoryID desc");
        return dt;
    }
    public int Cyrus_ChkEwalletBalance_BeforeTransaction(Decimal Amount, int msrno)
    {
        int mybal = 0;
        dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("RGetBalanceByMsrNo", msrno);
        if (Amount <= Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
        {
            mybal = 1;
        }
        return mybal;
    }


    //Cyrus_ChkEwalletBalance_BeforeTransactionCC
    public int Cyrus_ChkEwalletBalance_BeforeTransactionCC(Decimal Amount, int msrno)
    {
        int mybal = 0;
        int cnt = cls.select_data_scalar_int("Select count(*) from tblmlm_ewallettransaction where narration like '%Transferd to Member%' and Convert(varchar,adddate,112)=Convert(varchar,getdate(),112) and msrno='" + msrno + "'");
        if (cnt <= 2)
        {
            dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("RGetBalanceByMsrNo", msrno);
            if (Amount <= Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
            {
                mybal = 1;
            }
        }
        return mybal;
    }



    public int Cyrus_TransferFund(string fromMemberId, string TomemberID, int ismobile, int FromMsrno, Decimal amount)
    {
        string fromMobile = "", ToMobile = ""; int Tomsrno = 0; int ReturnVal = 0; string membertypeid = "";

        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select Mobile,Memberid,membertypeid from tblmlm_membermaster where msrno='" + FromMsrno.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            fromMobile = dt.Rows[0]["Mobile"].ToString();
            fromMemberId = dt.Rows[0]["MemberID"].ToString();
            membertypeid = dt.Rows[0]["membertypeid"].ToString();
        }
        else { return 0; }
        if (ismobile == 1)
        {
            dt = cls.select_data_dt("select * from tblMLM_MemberMaster where (Mobile='" + TomemberID + "' or memberid='" + TomemberID + "') and parentmsrno='" + FromMsrno + "'");
            if (dt.Rows.Count > 0)
            {
                Tomsrno = Convert.ToInt32(dt.Rows[0]["msrno"]);
                ToMobile = dt.Rows[0]["Mobile"].ToString();
                TomemberID = dt.Rows[0]["MemberID"].ToString();
            }
            else { return 0; }
        }
        else
        {
            dt = cls.select_data_dt("select * from tblMLM_MemberMaster where memberid='" + TomemberID + "' and parentmsrno='" + FromMsrno + "'");
            if (dt.Rows.Count > 0)
            {
                Tomsrno = Convert.ToInt32(dt.Rows[0]["msrno"]);
                ToMobile = dt.Rows[0]["Mobile"].ToString();
                TomemberID = dt.Rows[0]["MemberID"].ToString();
            }
            else { return 0; }
        }
        if (membertypeid != "7")
            Wallet_MakeTransaction(fromMemberId, -Convert.ToDecimal(amount), "Dr", "Transferd to Member - " + TomemberID);
        Wallet_MakeTransaction(TomemberID, Convert.ToDecimal(amount), "Cr", "Receive fund from Member - " + fromMemberId);
        string[] valueArray = new string[2];
        valueArray[0] = ToMobile;
        valueArray[1] = amount.ToString();
        SMS.SendWithVar(fromMobile, 5, valueArray, FromMsrno);
        DataTable dtmyWallet = new DataTable();
        dtmyWallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Tomsrno);
        string[] valueArray1 = new string[2];
        valueArray1[0] = dtmyWallet.Rows[0]["Balance"].ToString();
        valueArray1[1] = amount.ToString();
        SMS.SendWithVar(ToMobile, 15, valueArray1, Tomsrno);
        return 1;
    }
    public int Cyrus_TransferFund_C(string fromMemberId, string TomemberID, int ismobile, int FromMsrno, Decimal amount)
    {
        string fromMobile = "", ToMobile = ""; int Tomsrno = 0; int ReturnVal = 0; string membertypeid = "";

        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select Mobile,Memberid,membertypeid from tblmlm_membermaster where msrno='" + FromMsrno.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            fromMobile = dt.Rows[0]["Mobile"].ToString();
            fromMemberId = dt.Rows[0]["MemberID"].ToString();
            membertypeid = dt.Rows[0]["membertypeid"].ToString();
        }
        else { return 0; }
        if (ismobile == 1)
        {
            dt = cls.select_data_dt("select * from tblMLM_MemberMaster where (Mobile='" + TomemberID + "' or memberid='" + TomemberID + "')");
            if (dt.Rows.Count > 0)
            {
                Tomsrno = Convert.ToInt32(dt.Rows[0]["msrno"]);
                ToMobile = dt.Rows[0]["Mobile"].ToString();
                TomemberID = dt.Rows[0]["MemberID"].ToString();
            }
            else { return 0; }
        }
        else
        {
            dt = cls.select_data_dt("select * from tblMLM_MemberMaster where memberid='" + TomemberID + "'");
            if (dt.Rows.Count > 0)
            {
                Tomsrno = Convert.ToInt32(dt.Rows[0]["msrno"]);
                ToMobile = dt.Rows[0]["Mobile"].ToString();
                TomemberID = dt.Rows[0]["MemberID"].ToString();
            }
            else { return 0; }
        }
        if (membertypeid != "7")
            Wallet_MakeTransaction(fromMemberId, -Convert.ToDecimal(amount), "Dr", "Transferd to Member - " + TomemberID);
        Wallet_MakeTransaction(TomemberID, Convert.ToDecimal(amount), "Cr", "Receive fund from Member - " + fromMemberId);
        string[] valueArray = new string[2];
        valueArray[0] = ToMobile;
        valueArray[1] = amount.ToString();
        //SMS.SendWithVar(fromMobile, 5, valueArray, FromMsrno);
        DataTable dtmyWallet = new DataTable();
        dtmyWallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Tomsrno);
        string[] valueArray1 = new string[2];
        valueArray1[0] = dtmyWallet.Rows[0]["Balance"].ToString();
        valueArray1[1] = amount.ToString();
        SMS.SendWithVar(ToMobile, 15, valueArray1, Tomsrno);
        return 1;
    }
    public string Cyrus_GetTransactionID_New()
    {
        Random rnd = new Random();
        Int64 month = rnd.Next(10000, 99999);
        month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(10000, 99999)));
        return month.ToString();
    }
    public void FillDDL_Operator(DropDownList PrepaidDDL, DropDownList DTHDDL, DropDownList DataCardddl, DropDownList PostpaidDDL, DropDownList LandlineDDL, DropDownList ddlElectricity, DropDownList ddlGAS)
    {
        //Prepaid
        dtOperator = objOperator.ManageOperator("GetByServiceTypeIDRV", 1);
        PrepaidDDL.DataSource = dtOperator;
        PrepaidDDL.DataValueField = "OperatorID";
        PrepaidDDL.DataTextField = "OperatorName";
        PrepaidDDL.DataBind();
        PrepaidDDL.Items.Insert(0, new ListItem("Select Your Operay", "0"));
        //DTH
        dtOperator = objOperator.ManageOperator("GetByServiceTypeIDRV", 5);
        DTHDDL.DataSource = dtOperator;
        DTHDDL.DataValueField = "OperatorID";
        DTHDDL.DataTextField = "OperatorName";
        DTHDDL.DataBind();
        DTHDDL.Items.Insert(0, new ListItem("Select Your DTH Provider", "0"));
        //Datacard
        dtOperator = objOperator.ManageOperator("GetByServiceTypeIDRV", 3);
        DataCardddl.DataSource = dtOperator;
        DataCardddl.DataValueField = "OperatorID";
        DataCardddl.DataTextField = "OperatorName";
        DataCardddl.DataBind();
        DataCardddl.Items.Insert(0, new ListItem("Select Your Operator", "0"));
        //Postpaid
        dtOperator = objOperator.ManageOperator("GetByServiceTypeIDRV", 2);
        PostpaidDDL.DataSource = dtOperator;
        PostpaidDDL.DataValueField = "OperatorID";
        PostpaidDDL.DataTextField = "OperatorName";
        PostpaidDDL.DataBind();
        PostpaidDDL.Items.Insert(0, new ListItem("Select Your Operator", "0"));
        //Landline
        dtOperator = objOperator.ManageOperator("GetByServiceTypeIDRV", 6);
        LandlineDDL.DataSource = dtOperator;
        LandlineDDL.DataValueField = "OperatorID";
        LandlineDDL.DataTextField = "OperatorName";
        LandlineDDL.DataBind();
        LandlineDDL.Items.Insert(0, new ListItem("Select Your Operator", "0"));
        //Electricity
        dtOperator = objOperator.ManageOperator("GetByServiceTypeIDRV", 7);
        ddlElectricity.DataSource = dtOperator;
        ddlElectricity.DataValueField = "OperatorID";
        ddlElectricity.DataTextField = "OperatorName";
        ddlElectricity.DataBind();
        ddlElectricity.Items.Insert(0, new ListItem("Select Your Electricity Provider", "0"));
        //GAS
        dtOperator = objOperator.ManageOperator("GetByServiceTypeIDRV", 8);
        ddlGAS.DataSource = dtOperator;
        ddlGAS.DataValueField = "OperatorID";
        ddlGAS.DataTextField = "OperatorName";
        ddlGAS.DataBind();
        ddlGAS.Items.Insert(0, new ListItem("Select Your Gas Provider", "0"));
    }
    public void FillDDL_Circle(DropDownList ddlCirclePrepaid, DropDownList ddlCircleDatacard, DropDownList ddlCirclePostpaid, DropDownList ddlCircleLandline)
    {
        //Prepaid
        dtCircle = objCircle.ManageCircle("Get", 0);
        ddlCirclePrepaid.DataSource = dtCircle;
        ddlCirclePrepaid.DataValueField = "CircleID";
        ddlCirclePrepaid.DataTextField = "CircleName";
        ddlCirclePrepaid.DataBind();
        ddlCirclePrepaid.Items.Insert(0, new ListItem("Select Your Circle", "0"));
        //DataCard
        ddlCircleDatacard.DataSource = dtCircle;
        ddlCircleDatacard.DataValueField = "CircleID";
        ddlCircleDatacard.DataTextField = "CircleName";
        ddlCircleDatacard.DataBind();
        ddlCircleDatacard.Items.Insert(0, new ListItem("Select Your Circle", "0"));
        //Postpaid
        ddlCirclePostpaid.DataSource = dtCircle;
        ddlCirclePostpaid.DataValueField = "CircleID";
        ddlCirclePostpaid.DataTextField = "CircleName";
        ddlCirclePostpaid.DataBind();
        ddlCirclePostpaid.Items.Insert(0, new ListItem("Select Your Circle", "0"));
        //Landline
        ddlCircleLandline.DataSource = dtCircle;
        ddlCircleLandline.DataValueField = "CircleID";
        ddlCircleLandline.DataTextField = "CircleName";
        ddlCircleLandline.DataBind();
        ddlCircleLandline.Items.Insert(0, new ListItem("Select Your Circle", "0"));
    }
    public void FillDDL_Operator_B2C(DropDownList PrepaidDDL, DropDownList DTHDDL, DropDownList DataCardddl, DropDownList PostpaidDDL, DropDownList LandlineDDL, DropDownList ddlElectricity, DropDownList ddlGAS, DropDownList ddlInsurance)
    {
        //
        //Prepaid
        dtOperator = objOperator.ManageOperator("GetByServiceTypeID", 1);
        PrepaidDDL.DataSource = dtOperator;
        PrepaidDDL.DataValueField = "OperatorID";
        PrepaidDDL.DataTextField = "OperatorName";
        PrepaidDDL.DataBind();
        PrepaidDDL.Items.Insert(0, new ListItem("Prepaid Operator", "0"));
        //DTH
        dtOperator = objOperator.ManageOperator("GetByServiceTypeID", 5);
        DTHDDL.DataSource = dtOperator;
        DTHDDL.DataValueField = "OperatorID";
        DTHDDL.DataTextField = "OperatorName";
        DTHDDL.DataBind();
        DTHDDL.Items.Insert(0, new ListItem("DTH Provider", "0"));
        //Datacard
        dtOperator = objOperator.ManageOperator("GetByServiceTypeID", 3);
        DataCardddl.DataSource = dtOperator;
        DataCardddl.DataValueField = "OperatorID";
        DataCardddl.DataTextField = "OperatorName";
        DataCardddl.DataBind();
        DataCardddl.Items.Insert(0, new ListItem("Datacard Operator", "0"));
        //Postpaid
        dtOperator = objOperator.ManageOperator("GetByServiceTypeID", 2);
        PostpaidDDL.DataSource = dtOperator;
        PostpaidDDL.DataValueField = "OperatorID";
        PostpaidDDL.DataTextField = "OperatorName";
        PostpaidDDL.DataBind();
        PostpaidDDL.Items.Insert(0, new ListItem("Postpaid Operator", "0"));
        //Landline
        dtOperator = objOperator.ManageOperator("GetByServiceTypeID", 6);
        LandlineDDL.DataSource = dtOperator;
        LandlineDDL.DataValueField = "OperatorID";
        LandlineDDL.DataTextField = "OperatorName";
        LandlineDDL.DataBind();
        LandlineDDL.Items.Insert(0, new ListItem("Landline Operator", "0"));
        //Electricity
        dtOperator = objOperator.ManageOperator("GetByServiceTypeID", 7);
        ddlElectricity.DataSource = dtOperator;
        ddlElectricity.DataValueField = "OperatorID";
        ddlElectricity.DataTextField = "OperatorName";
        ddlElectricity.DataBind();
        ddlElectricity.Items.Insert(0, new ListItem("Electricity Provider", "0"));
        //GAS
        dtOperator = objOperator.ManageOperator("GetByServiceTypeID", 8);
        ddlGAS.DataSource = dtOperator;
        ddlGAS.DataValueField = "OperatorID";
        ddlGAS.DataTextField = "OperatorName";
        ddlGAS.DataBind();
        ddlGAS.Items.Insert(0, new ListItem("Gas Provider", "0"));
        //GAS
        dtOperator = objOperator.ManageOperator("GetByServiceTypeID", 9);
        ddlInsurance.DataSource = dtOperator;
        ddlInsurance.DataValueField = "OperatorID";
        ddlInsurance.DataTextField = "OperatorName";
        ddlInsurance.DataBind();
        ddlInsurance.Items.Insert(0, new ListItem("Insurance Provider", "0"));
    }
    public void FillDDL_Circle_B2C(DropDownList ddlCirclePrepaid, DropDownList ddlCircleDatacard, DropDownList ddlCirclePostpaid)
    {
        //Prepaid
        dtCircle = objCircle.ManageCircle("Get", 0);
        ddlCirclePrepaid.DataSource = dtCircle;
        ddlCirclePrepaid.DataValueField = "CircleID";
        ddlCirclePrepaid.DataTextField = "CircleName";
        ddlCirclePrepaid.DataBind();
        ddlCirclePrepaid.Items.Insert(0, new ListItem("Select Your Circle", "0"));
        //DataCard
        ddlCircleDatacard.DataSource = dtCircle;
        ddlCircleDatacard.DataValueField = "CircleID";
        ddlCircleDatacard.DataTextField = "CircleName";
        ddlCircleDatacard.DataBind();
        ddlCircleDatacard.Items.Insert(0, new ListItem("Select Your Circle", "0"));
        //Postpaid
        ddlCirclePostpaid.DataSource = dtCircle;
        ddlCirclePostpaid.DataValueField = "CircleID";
        ddlCirclePostpaid.DataTextField = "CircleName";
        ddlCirclePostpaid.DataBind();
        ddlCirclePostpaid.Items.Insert(0, new ListItem("Select Your Circle", "0"));
        ////Landline
        //ddlCircleLandline.DataSource = dtCircle;
        //ddlCircleLandline.DataValueField = "CircleID";
        //ddlCircleLandline.DataTextField = "CircleName";
        //ddlCircleLandline.DataBind();
        //ddlCircleLandline.Items.Insert(0, new ListItem("Select Your Circle", "0"));
    }
    public string Cyrus_apicall(string url)
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
        catch
        {
            return "0";
        }
    }
    public double Cyrus_getbalanceofAPI(int apiidno)
    {
        double returnvalue = 0;
        if (apiidno != 10)
        {
            DataTable dtAPI = new DataTable();
            dtAPI = objAPI.ManageAPI("GetAll", apiidno);
            if (dtAPI.Rows[0]["BalanceURL"].ToString() != "")
            {
                try
                {
                    string strBalanceAPI = "";
                    if (dtAPI.Rows[0]["B_prm1"].ToString() != "" && dtAPI.Rows[0]["B_prm1val"].ToString() != "")
                    {
                        strBalanceAPI = dtAPI.Rows[0]["BalanceURL"].ToString() + dtAPI.Rows[0]["B_prm1"].ToString() + "=" + dtAPI.Rows[0]["B_prm1val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm2"].ToString() != "" && dtAPI.Rows[0]["B_prm2val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm2"].ToString() + "=" + dtAPI.Rows[0]["B_prm2val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm3"].ToString() != "" && dtAPI.Rows[0]["B_prm3val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm3"].ToString() + "=" + dtAPI.Rows[0]["B_prm3val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm4"].ToString() != "" && dtAPI.Rows[0]["B_prm4val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm4"].ToString() + "=" + dtAPI.Rows[0]["B_prm4val"].ToString();
                    }
                    if (strBalanceAPI.EndsWith("&"))
                        strBalanceAPI = strBalanceAPI.Substring(0, strBalanceAPI.Length - 1);

                    string result = Cyrus_apicall(strBalanceAPI);
                    string[] split = result.Split(',');
                    returnvalue = Convert.ToDouble(split[Convert.ToInt32(dtAPI.Rows[0]["B_BalancePosition"].ToString())]);
                }
                catch
                {
                    returnvalue = 0;
                }
            }
        }
        else
        {
            //mom clsmom = new mom();
            //returnvalue = Convert.ToDouble(clsmom.MOM_GetBalance());
            returnvalue = Convert.ToDouble(0);
        }
        return returnvalue;
    }
    public string GetRechargeURL_YAHAYA(string number, string amount, string opcode)
    {
        string retval = "http://rome2in.com/services/api/api/recharge.php?";
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select * from YAHAYA_OP where opcode='" + opcode + "'");
        if (dt.Rows.Count > 0)
        {
            retval = retval + "agent_id=100018&key=YAHYAtyevfr1fg7&";
            retval = retval + "mnumber=" + number;
            retval = retval + "&amount=" + amount;
            retval = retval + "&mobiletype=TopUp&service_type=" + dt.Rows[0]["servicetype"].ToString() + "";
            retval = retval + "&service_provider=" + dt.Rows[0]["yopname"].ToString() + "";
            retval = retval + "&service_provider_id=" + dt.Rows[0]["servicetypeid"].ToString() + "";
            retval = retval + "&recharge_type=" + dt.Rows[0]["rechargeType"].ToString() + "";
        }
        return retval;
    }
    public string GetRechargeURL_YAHAYA_Status(string mobilenumber, string APItransid)
    {
        string retval = "http://rome2in.com/services/api/recharge_status.php?";
        retval = retval + "agent_id=100018&key=YAHYAtyevfr1fg7";
        retval = retval + "&mnumber=" + mobilenumber;
        retval = retval + "&Transid=" + APItransid;
        return retval;
    }
    public string Cyrus_RechargeProcess(int id, string mycirclecode, string trantype, DataTable dtMemberMaster)
    {
        string returnStr = "";
        try
        {
            // mom clsmom = new mom();
            dtHistory = objHistory.ManageHistory("Get", id);
            string APIIDNO = dtHistory.Rows[0]["APIID"].ToString();
            DataTable dtOperatorCode = cls.select_data_dt("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + Convert.ToInt32(dtHistory.Rows[0]["OperatorID"]) + "and APIID=" + Convert.ToInt32(APIIDNO));
            DataTable dtCircleCode = cls.select_data_dt("select CircleCode from tblRecharge_CircleCode where CircleID=" + Convert.ToInt32(dtHistory.Rows[0]["CircleID"]) + "and APIID=" + Convert.ToInt32(APIIDNO));
            string amount = Convert.ToString(Convert.ToInt32(dtHistory.Rows[0]["RechargeAmount"]));
            string account = trantype;//hdnTranType.Value;//Convert.ToString(dtHistory.Rows[0]["caNumber"]);
            string number = Convert.ToString(dtHistory.Rows[0]["MobileNo"]);
            string TransID = Convert.ToString(dtHistory.Rows[0]["TransID"]);
            if (Convert.ToInt32(dtHistory.Rows[0]["APIID"]) == 0)
            {
                DataTable i = objHistory.UpdateHistory("UpdateStatus", id, 0, 0, 0, 0, 0, "", "", "Offline", "", "", "");
                returnStr = "Bill Payment Successful !!," + id.ToString();
            }
            else
            {
                string OperatorCode = "";
                if (Convert.ToInt32(APIIDNO) == 10)
                    //OperatorCode = clsmom.MOM_operatorCode(Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"])).Trim();
                    OperatorCode = "";
                else
                    OperatorCode = Convert.ToString(dtOperatorCode.Rows[0]["OperatorCode"]).Trim();

                string CircleCode;
                if (dtCircleCode.Rows.Count > 0)
                    CircleCode = dtCircleCode.Rows[0][0].ToString();
                else
                    CircleCode = mycirclecode;

                dtAPI = objAPI.ManageAPI("Get", Convert.ToInt32(APIIDNO));
                string str = "";
                if (APIIDNO == "13")
                {
                    str = GetRechargeURL_YAHAYA(number, amount, OperatorCode);
                }
                else
                {
                    //Append Code by ravi 13-08-2014
                    if (dtAPI.Rows[0]["URL"].ToString().ToLower() != "http://api.ezulix/api/recharge.aspx?")
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
                        str = str + dtAPI.Rows[0]["prm5"].ToString() + "=" + CircleCode + "&";
                    }
                    if (dtAPI.Rows[0]["prm6"].ToString() != "")
                    {
                        str = str + dtAPI.Rows[0]["prm6"].ToString() + "=" + amount + "&";
                    }
                    if (dtAPI.Rows[0]["prm7"].ToString() != "")
                    {
                        //if  (OperatorCode=="" || OperatorCode=="")
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

                }
                //str = str + "&trantype=" + hdnTranType.Value;
                string Success = Convert.ToString(dtAPI.Rows[0]["Success"]);
                string Failed = Convert.ToString(dtAPI.Rows[0]["Failed"]);
                string Pending = Convert.ToString(dtAPI.Rows[0]["Pending"]);

                var TxID = "";
                var Status = "";
                var OperatorRef = "";
                var ErrorCode = "";

                string result = Cyrus_apicall(str);
                char Splitter = Convert.ToChar(dtAPI.Rows[0]["Splitter"]);
                string[] split = result.Split(Splitter);
                if (Convert.ToString(dtAPI.Rows[0]["TxIDPosition"]) != "")
                {
                    try
                    {
                        int TxIDPosition = Convert.ToInt32(dtAPI.Rows[0]["TxIDPosition"]);
                        if (TxIDPosition <= split.Length)
                            TxID = split[TxIDPosition];
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (Convert.ToString(dtAPI.Rows[0]["StatusPosition"]) != "")
                {
                    try
                    {
                        int StatusPosition = Convert.ToInt32(dtAPI.Rows[0]["StatusPosition"]);
                        if (StatusPosition <= split.Length)
                            Status = split[StatusPosition];
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (Convert.ToString(dtAPI.Rows[0]["OperatorRefPosition"]) != "")
                {
                    try
                    {
                        int OperatorRefPosition = Convert.ToInt32(dtAPI.Rows[0]["OperatorRefPosition"]);
                        if (OperatorRefPosition <= split.Length)
                            OperatorRef = split[OperatorRefPosition];
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (Convert.ToString(dtAPI.Rows[0]["ErrorCodePosition"]) != "")
                {
                    try
                    {
                        int ErrorCodePosition = Convert.ToInt32(dtAPI.Rows[0]["ErrorCodePosition"]);
                        if (ErrorCodePosition <= split.Length)
                            ErrorCode = split[ErrorCodePosition];
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (Status.ToLower() == Success.ToLower())
                {
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", id, Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]), 0, 0, 0, 0, "", "", "Success", TxID, "", OperatorRef);
                    returnStr = "Recharge Successful !!," + id.ToString() + "," + ErrorCode + "," + OperatorRef + ",";
                }
                else if (Status.ToLower() == Failed.ToLower() || Status.ToLower() == "103" || Status.IndexOf("last 3 Hour") > 0)
                {
                    DataTable dtdeduction = new DataTable();
                    //dtdeduction = cls.select_data_dt("exec ResellerDownlineRecharge '" + dtHistory.Rows[0]["MsrNo"].ToString() + "'");
                    //for (int ix = 0; ix < dtdeduction.Rows.Count; ix++)
                    //{
                    if (cls.select_data_scalar_int("Select count(*) from tblmlm_MemberCouponCode_use where historyid='" + id + "'") > 0)
                        amount = (Convert.ToDecimal(amount) - 10).ToString();
                    Wallet_MakeTransaction(Convert.ToString(dtMemberMaster.Rows[0]["MemberID"]), Convert.ToDecimal(amount), "Cr", "Refund amount due to recharge failure - (Txn ID : " + dtHistory.Rows[0]["TransID"].ToString() + ")");
                    //}
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", id, 0, 0, 0, 0, 0, "", "", "Failed", TxID, ErrorCode, OperatorRef);
                    returnStr = "Recharge Failed !!," + id.ToString() + "," + ErrorCode + "," + OperatorRef + ",";
                }
                else if (Status.ToLower() == Pending.ToLower())
                {
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", id, 0, 0, 0, 0, 0, "", "", "Pending", TxID, "", OperatorRef);
                    returnStr = "Recharge Pending !!," + id.ToString() + "," + ErrorCode + "," + OperatorRef + ",";
                }
                else
                {
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", id, 0, 0, 0, 0, 0, "", "", "Pending", TxID, "", OperatorRef);
                    returnStr = "Recharge Pending !!," + id.ToString() + "," + ErrorCode + "," + OperatorRef + ",";
                }
            }
        }
        catch (Exception ex)
        {
            dtHistory = objHistory.ManageHistory("Get", id);
            if (dtHistory.Rows.Count > 0)
            {
                cls.insert_data("insert into tbl_OrderMsg values (1, '" + ex.Message + "','" + dtHistory.Rows[0]["Msrno"].ToString() + "','" + dtHistory.Rows[0]["RechargeAmount"].ToString() + "',1,1,'" + id.ToString() + "','','','','',0,0,1,getdate(),getdate())");
                if (dtHistory.Rows[0]["status"].ToString().ToLower() == "queued")
                {
                    DataTable i = objHistory.UpdateHistory("UpdateStatus", id, 0, 0, 0, 0, 0, "", "", "Pending", "", "", "");
                    returnStr = "Recharge Pending !!," + id.ToString() + ",,,";
                }
            }
        }
        return returnStr;
    }
    public void Cyrus_RechargePendingProcess(int historyId)
    {
        dtHistory = objHistory.ManageHistory("GetPQ", historyId);
        dtAPI = objAPI.ManageAPI("Get", Convert.ToInt32(dtHistory.Rows[0]["APIID"]));
        int StatusPosition = Convert.ToInt32(dtAPI.Rows[0]["S_StatusPosition"]);
        string Success = Convert.ToString(dtAPI.Rows[0]["Success"]);
        string Failed = Convert.ToString(dtAPI.Rows[0]["Failed"]);
        var status = ""; string error_msg = ""; string APItxnid = "";
        string str = "";

        if (dtAPI.Rows[0]["StatusURL"].ToString() != "")
        {
            if (Convert.ToInt32(dtHistory.Rows[0]["APIID"]) == 13)
            {
                str = GetRechargeURL_YAHAYA_Status(dtHistory.Rows[0]["mobileno"].ToString(), dtHistory.Rows[0]["apitransid"].ToString());
            }
            else
            {
                if (dtAPI.Rows[0]["S_prm1"].ToString() != "" && dtAPI.Rows[0]["S_prm1val"].ToString() != "")
                {
                    str = dtAPI.Rows[0]["StatusURL"].ToString() + dtAPI.Rows[0]["S_prm1"].ToString() + "=" + dtAPI.Rows[0]["S_prm1val"].ToString() + "&";
                }
                if (dtAPI.Rows[0]["S_prm2"].ToString() != "" && dtAPI.Rows[0]["S_prm2val"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["S_prm2"].ToString() + "=" + dtAPI.Rows[0]["S_prm2val"].ToString() + "&";
                }
                if (dtAPI.Rows[0]["S_prm3"].ToString() != "")
                {
                    if (dtHistory.Rows[0]["APIID"].ToString() == "10" || dtHistory.Rows[0]["APIID"].ToString() == "11")
                    {
                        str = str + dtAPI.Rows[0]["S_prm3"].ToString() + "=" + Convert.ToString(dtHistory.Rows[0]["TransID"]);
                    }
                    else
                    {
                        str = str + dtAPI.Rows[0]["S_prm3"].ToString() + "=" + Convert.ToString(dtHistory.Rows[0]["TransID"]);
                    }
                }
                if (str.EndsWith("&"))
                    str = str.Substring(0, str.Length - 1);
            }
            string result = Cyrus_apicall(str);
            string[] split = result.Split(',');
            if (Convert.ToInt32(dtHistory.Rows[0]["APIID"].ToString()) == 10)
            {
                StatusPosition = split.Length - 2;
            }
            status = split[StatusPosition];
            if (status.ToLower() == Failed.ToLower() || status.ToLower() == "reversed")
            {
                if (Convert.ToInt32(dtHistory.Rows[0]["APIID"].ToString()) == 10)
                {
                    error_msg = split[4];
                    APItxnid = split[1];
                }
                else
                {
                    error_msg = "FAILURE";
                    APItxnid = Convert.ToString(dtHistory.Rows[0]["APITransID"]);
                }
            }
        }

        if (status.ToLower() == Success.ToLower())
        {
            DataTable i = objHistory.UpdateHistory("UpdateStatus", historyId, Convert.ToInt32(dtHistory.Rows[0]["MsrNo"]), 0, 0, 0, 0, "", "", "Success", "", "", "");
        }
        else if (status.ToLower() == Failed.ToLower() || status.ToLower() == "reversed" || status.ToLower() == "103" || status.ToLower() == "invalid" || status.IndexOf("last 3 Hour") > 0)
        {
            int membertypeID = cls.select_data_scalar_int("Select membertypeid from tblmlm_membermaster where msrno='" + dtHistory.Rows[0]["msrno"].ToString() + "'");
            if (membertypeID == 6 && status.ToLower() == "invalid")
            {
                DataTable i = objHistory.UpdateHistory("UpdateStatus", historyId, 0, 0, 0, 0, 0, "", "", "Failed", APItxnid, error_msg, "");
            }
            else
            {
                DataTable dtdeduction = new DataTable();
                dtdeduction = cls.select_data_dt("exec ResellerDownlineRecharge '" + dtHistory.Rows[0]["MsrNo"].ToString() + "'");
                for (int ix = 0; ix < dtdeduction.Rows.Count; ix++)
                {
                    Wallet_MakeTransaction(Convert.ToString(dtdeduction.Rows[ix]["MemberID"]), Convert.ToDecimal(dtHistory.Rows[0]["RechargeAmount"]), "Cr", "Refund amount due to recharge failure - (Txn ID : " + dtHistory.Rows[0]["TransID"].ToString() + ")");
                }
                DataTable i = objHistory.UpdateHistory("UpdateStatus", historyId, 0, 0, 0, 0, 0, "", "", "Failed", APItxnid, error_msg, "");
            }
        }
    }
    public void Cyrus_Recharge_ForceFailed(int historyId)
    {
        dtHistory = objHistory.ManageHistory("GetAll", historyId);
        DataTable dtdeduction = new DataTable();
        dtdeduction = cls.select_data_dt("select * from tblmlm_membermaster where msrno=" + Convert.ToInt32(dtHistory.Rows[0]["MsrNo"].ToString()) + "");

        Wallet_MakeTransaction(Convert.ToString(dtdeduction.Rows[0]["MemberID"]), Convert.ToDecimal(dtHistory.Rows[0]["RechargeAmount"]), "Cr", "Refund amount due to recharge failure - (Txn ID : " + dtHistory.Rows[0]["TransID"].ToString() + ")");
        DataTable i = objHistory.UpdateHistory("UpdateStatus", historyId, 0, 0, 0, 0, 0, "", "", "Failed", "", "Foced Failed", "");
    }
    public void Cyrus_Recharge_ForceSuccess(int historyId)
    {
        dtHistory = objHistory.ManageHistory("GetAll", historyId);
        DataTable i = objHistory.UpdateHistory("UpdateStatus", historyId, Convert.ToInt32(dtHistory.Rows[0]["MsrNo"]), 0, 0, 0, 0, "", "", "Success", "", "", "Force Success");
    }

    public string Atom_Requery_status(string merchantID, string merchanttxnid, string amt)
    {
        try
        {
            string URL = "https://payment.atomtech.in/paynetz/vfts?merchantid=" + merchantID + "&merchanttxnid=" + merchanttxnid + "&amt=" + amt + "&tdate=" + String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date);
            string result = Cyrus_apicall(URL);
            DataSet ds = new DataSet();
            StringReader theReader = new StringReader(result);
            ds.ReadXml(theReader);
            return ds.Tables[0].Rows[0]["verified"].ToString().ToUpper();
        }
        catch (Exception ex)
        {
            return "FAILED";
        }
    }
    public void Noble_Offline_Recharge(string memberid, string amount, string operatorname, string operatorid, int msrno, string prm1, string prm1val, string prm2, string prm2val, string prm3, string prm3val, string prm4, string prm4val, string prm5, string prm5val, string prm6, string prm6val, string prm7, string prm7val, string prm8, string prm8val, string prm9, string prm9val, string prm10, string prm10val, string prm11, string prm11val, string prm12, string prm12val, string remark)
    {
        string TransID = Cyrus_GetTransactionID_New();
        Wallet_MakeTransaction(Convert.ToString(memberid), Convert.ToDecimal("-" + amount), "Dr", "Recharge of  " + operatorname + " (Txn ID : " + TransID + ") OTHER");
        int myidno = 0;
        myidno = cls.insert_data("Exec Recharge_AddofflineRecharge 0,'" + operatorid + "','" + operatorname + "',10,'" + msrno.ToString() + "','" + TransID + "','" + prm1 + "','" + prm1val + "','" + prm2 + "','" + prm2val + "','" + prm3 + "','" + prm3val + "','" + prm4 + "','" + prm4val + "','" + prm5 + "','" + prm5val + "','" + prm6 + "','" + prm6val + "','" + prm7 + "','" + prm7val + "','" + prm8 + "','" + prm8val + "','" + prm9 + "','" + prm9val + "','" + prm10 + "','" + prm10val + "','" + prm11 + "','" + prm11val + "','" + prm12 + "','" + prm12val + "','" + remark + "','','Pending','" + amount + "'");
        if (myidno > 0)
        {
            DataTable dtx = new DataTable();
            dtx = cls.select_data_dt("Select Operatorname,email from tblRecharge_offlineOperatorSetting where id='" + operatorid + "' and isactive=1 and isdelete=0");
            if (dtx.Rows.Count > 0)
            {
                string email = dtx.Rows[0]["email"].ToString();
                if (email != "")
                {
                    dtx = cls.select_data_dt("Exec Recharge_ListofflineRecharge '01-01-1990','01-01-1990',0,0,'" + myidno + "',''");
                    if (dtx.Rows.Count > 0)
                    {
                        FlexiMail objSendMail = new FlexiMail();
                        objSendMail.To = email;
                        objSendMail.CC = "ezulixsoftware@gmail.com";
                        objSendMail.BCC = "";
                        objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                        objSendMail.FromName = "";
                        objSendMail.MailBodyManualSupply = true;
                        objSendMail.MailBody = dtx.Rows[0]["TranInfo"].ToString();
                        objSendMail.Subject = "Offline Operator Request";
                        objSendMail.Send();
                    }
                }
            }
        }
    }




}