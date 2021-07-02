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
using System.Xml.Serialization;
using System.Text;
using System.Xml.Linq;

public partial class API_dodmr : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
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
        XDocument stud;
        try
        {
            if (!IsPostBack)
            {

                if (Request.Form["memberid"] != null)
                {
                    string memberid = Request.Form["memberid"].ToString().Replace("'", "").Replace("-", "");
                    DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where MemberID='" + memberid + "'");
                    if (dtMemberMaster.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtMemberMaster.Rows[0]["IsActive"]) == true && Convert.ToBoolean(dtMemberMaster.Rows[0]["IsEmailVerify"]) == true)
                        {
                            if (Request.Form["pin"] != null)
                            {
                                if (Convert.ToString(dtMemberMaster.Rows[0]["transactionpassword"]) == Request.Form["pin"].ToString().Replace("'", "").Replace("-", ""))
                                {
                                    #region BankList
                                    if (Request.Form["trantype"].ToString().ToUpper() == "LISTBANK")
                                    {
                                        DataSet dt = new DataSet();
                                        dt = cls.select_data_ds("Select bankcode,bankname from Fiti_Bank_List order by BankName");
                                        dt.DataSetName = "DMR_BANK";
                                        dt.Tables[0].TableName = "BANK_LIST";
                                        string ss = dt.GetXml();
                                        Response.ClearHeaders();
                                        Response.AddHeader("content-type", "text/xml");
                                        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + ss);
                                    }
                                    #endregion
                                    #region PROCESSTRF
                                    else if (Request.Form["trantype"].ToString().ToUpper() == "PROCESSTRF")
                                    {
                                        if (Request.Form["amount"] != null || Request.Form["BankCode"] != null || Request.Form["sender_mobile"] != null || Request.Form["txnid"] != null || Request.Form["Accountno"] != null || Request.Form["ReceiverName"] != null)
                                        {
                                            string amount = Request.Form["amount"].ToString().Replace("'", "").Replace("-", "");
                                            string BankCode = Request.Form["BankCode"].ToString().Replace("'", "").Replace("-", "");
                                            string sender_mobile = Request.Form["sender_mobile"].ToString().Replace("'", "").Replace("-", "");
                                            string txnid = Request.Form["txnid"].ToString().Replace("'", "").Replace("-", "");
                                            string Accountno = Request.Form["Accountno"].ToString().Replace("'", "").Replace("-", "");
                                            string ReceiverName = Request.Form["ReceiverName"].ToString().Replace("'", "").Replace("-", "");
                                            if (cls.select_data_scalar_int("Select count(*) from mm_fundtransfer where Txnid='" + txnid.Trim() + "'") == 0 && txnid.Trim().Length == 10)
                                            {
                                                if (cls.select_data_scalar_int("Select count(*) from Fiti_Bank_List where bankcode='" + BankCode + "'") > 0)
                                                {
                                                    try
                                                    {
                                                        if (sender_mobile.Length != 10 || (Convert.ToInt64(sender_mobile) * 1) == Convert.ToInt64(sender_mobile))
                                                        {
                                                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                            new XElement("PROCESSTRF",
                                                                new XElement("statuscode", "0"),
                                                                new XElement("status", "FAILURE"),
                                                                new XElement("error_code", "Invalid Sender Mobile"),
                                                                new XElement("message", "Invalid Sender Mobile"),
                                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                                ));
                                                            string _encodedXML = stud.ToString();
                                                            Response.ClearHeaders();
                                                            Response.AddHeader("content-type", "text/xml");
                                                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                            return;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                        new XElement("PROCESSTRF",
                                                            new XElement("statuscode", "0"),
                                                            new XElement("status", "FAILURE"),
                                                            new XElement("error_code", "Invalid Sender Mobile"),
                                                            new XElement("message", "Invalid Sender Mobile"),
                                                            new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                            ));
                                                        string _encodedXML = stud.ToString();
                                                        Response.ClearHeaders();
                                                        Response.AddHeader("content-type", "text/xml");
                                                        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                        return;
                                                    }
                                                    DataTable dtbalance = cls.select_data_dt("Select MsrNo,Balance from tblMLM_EWalletBalance where MsrNo=(select msrno from tblmlm_membermaster where msrno='" + dtMemberMaster.Rows[0]["msrno"].ToString() + "' and memberid='" + memberid + "')");
                                                    if (dtbalance.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToDecimal(TotupAmount(Convert.ToDouble(amount), dtMemberMaster)) <= Convert.ToDecimal(dtbalance.Rows[0]["Balance"].ToString()) && Convert.ToDecimal(TotupAmount(Convert.ToDouble(amount), dtMemberMaster)) > 0)
                                                        {
                                                            MoneyTransfer money = new MoneyTransfer();
                                                            string postdata2 = money.SetPostData_PROCESSMONEYTRANSFER_new(sender_mobile, BankCode, amount, txnid, Accountno, ReceiverName, "imps");
                                                            DataSet dds = money.MakeTransaction_new(postdata2);
                                                            DataTable ds = dds.Tables[0].DefaultView.ToTable();
                                                            if (ds.Rows[0]["statuscode"].ToString() == "1")
                                                            {
                                                                //history update here
                                                                cls.update_data("Exec MM_UpdateFundTransfer 0," + dtMemberMaster.Rows[0]["msrno"].ToString() + ",'" + ReceiverName + "'," + Convert.ToDecimal(amount) + ",'IMPS','" + ds.Rows[0]["txstatus_desc"].ToString() + "','" + Accountno + "','NON-IFSC','" + ds.Rows[0]["message"].ToString() + "','" + ds.Rows[0]["txstatus_desc"].ToString() + "','" + ds.Rows[0]["bank_ref_num"].ToString() + "','" + ds.Rows[0]["transactionid"].ToString() + "','" + txnid + "','" + dtbalance.Rows[0]["msrno"].ToString() + "'");
                                                                cls.insert_data("Exec MM_ActualTransaction_add 0,'" + ds.Rows[0]["message"].ToString() + "','0','" + ds.Rows[0]["amount"].ToString() + "','" + dtMemberMaster.Rows[0]["msrno"].ToString() + "','" + ds.Rows[0]["service_tax"].ToString() + "','" + ds.Rows[0]["bank_ref_num"].ToString() + "','" + ds.Rows[0]["customer_id"].ToString() + "','" + ds.Rows[0]["currency"].ToString() + "','" + ds.Rows[0]["transactionid"].ToString() + "','" + ds.Rows[0]["recipient_id"].ToString() + "','" + txnid + "'");
                                                                //string[] valueArray = new string[4];
                                                                //valueArray[0] = amount;
                                                                //valueArray[1] = Accountno;
                                                                //valueArray[2] = txnid;
                                                                //valueArray[3] = ds.Rows[0]["bank_ref_num"].ToString();
                                                                //SMS.SendWithVar(sender_mobile, 1, valueArray, 1);
                                                                stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                                new XElement("PROCESSTRF",
                                                                    new XElement("statuscode", "1"),
                                                                    new XElement("status", "Success"),
                                                                    new XElement("bankref", ds.Rows[0]["bank_ref_num"].ToString()),
                                                                    new XElement("banktxnid", ds.Rows[0]["transactionid"].ToString()),
                                                                    new XElement("error_code", ""),
                                                                    new XElement("message", ds.Rows[0]["message"].ToString().Replace("'", "")),
                                                                    new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                                    ));
                                                                string _encodedXML = stud.ToString();
                                                                Response.ClearHeaders();
                                                                Response.AddHeader("content-type", "text/xml");
                                                                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success | Fund transfer Successfully !');location.replace('SendMoney.aspx');", true);
                                                            }
                                                            else
                                                            {
                                                                stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                                new XElement("PROCESSTRF",
                                                                    new XElement("statuscode", "0"),
                                                                    new XElement("status", "FAILURE"),
                                                                    new XElement("error_code", "Bank Error"),
                                                                    new XElement("message", ds.Rows[0]["message"].ToString().Replace("'", "")),
                                                                    new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                                    ));
                                                                string _encodedXML = stud.ToString();
                                                                Response.ClearHeaders();
                                                                Response.AddHeader("content-type", "text/xml");
                                                                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error | " + ds.Rows[0]["message"].ToString().Replace("'", "") + " !');", true);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                            new XElement("PROCESSTRF",
                                                                new XElement("statuscode", "0"),
                                                                new XElement("status", "FAILURE"),
                                                                new XElement("error_code", "Insufficient Balance"),
                                                                new XElement("message", "Insufficient Balance or Surcharge config error"),
                                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                                ));
                                                            string _encodedXML = stud.ToString();
                                                            Response.ClearHeaders();
                                                            Response.AddHeader("content-type", "text/xml");
                                                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                        new XElement("PROCESSTRF",
                                                            new XElement("statuscode", "0"),
                                                            new XElement("status", "FAILURE"),
                                                            new XElement("error_code", "Configuration Error"),
                                                            new XElement("message", "Please contact to admin"),
                                                            new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                            ));
                                                        string _encodedXML = stud.ToString();
                                                        Response.ClearHeaders();
                                                        Response.AddHeader("content-type", "text/xml");
                                                        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                    new XElement("PROCESSTRF",
                                                        new XElement("statuscode", "0"),
                                                        new XElement("status", "FAILURE"),
                                                        new XElement("error_code", "Invalid Bank Code"),
                                                        new XElement("message", "Invalid Bank Code"),
                                                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                        ));
                                                    string _encodedXML = stud.ToString();
                                                    Response.ClearHeaders();
                                                    Response.AddHeader("content-type", "text/xml");
                                                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                }
                                            }
                                            else
                                            {
                                                stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                new XElement("PROCESSTRF",
                                                    new XElement("statuscode", "0"),
                                                    new XElement("status", "FAILURE"),
                                                    new XElement("error_code", "Duplicate Transcation ID"),
                                                    new XElement("message", "Duplicate Transcation ID"),
                                                    new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                    ));
                                                string _encodedXML = stud.ToString();
                                                Response.ClearHeaders();
                                                Response.AddHeader("content-type", "text/xml");
                                                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                            }
                                        }
                                        else
                                        {
                                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("PROCESSTRF",
                                                new XElement("statuscode", "0"),
                                                new XElement("status", "FAILURE"),
                                                new XElement("error_code", "Insufficient or Invalid Parameters"),
                                                new XElement("message", "Please check API doc"),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                ));
                                            string _encodedXML = stud.ToString();
                                            Response.ClearHeaders();
                                            Response.AddHeader("content-type", "text/xml");
                                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                        }
                                    }
                                    #endregion
                                    #region generateOTP
                                    else if (Request.Form["trantype"].ToString().ToUpper() == "GENOTP")
                                    {
                                        if (Request.Form["banktxnid"] != null)
                                        {
                                            string banktxnid = Request.Form["banktxnid"].ToString().Replace("'", "").Replace("-", "");
                                            MoneyTransfer mm = new MoneyTransfer();
                                            DataTable dt = new DataTable();
                                            dt = cls.select_data_dt("Exec RP_DMR_transaction_refund 0,'" + banktxnid + "'");
                                            if (dt.Rows.Count > 0)
                                            {
                                                string trnasctionid = dt.Rows[0]["BankTXNID"].ToString();
                                                string referenceid = dt.Rows[0]["TxnID"].ToString();
                                                string type = "resend";
                                                string depositor = dt.Rows[0]["custMobile"].ToString();
                                                string postData = mm.SetPostData_GenerateOTPRefund(trnasctionid, referenceid, type);
                                                DataSet ds = new DataSet();
                                                ds = mm.MakeTransaction_new(postData);
                                                if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "1")
                                                {
                                                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                    new XElement("GENOTP",
                                                        new XElement("statuscode", "1"),
                                                        new XElement("status", "SUCCESS"),
                                                        new XElement("error_code", ""),
                                                        new XElement("message", "OTP sent successfully"),
                                                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                        ));
                                                    string _encodedXML = stud.ToString();
                                                    Response.ClearHeaders();
                                                    Response.AddHeader("content-type", "text/xml");
                                                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                }
                                                else
                                                {
                                                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                    new XElement("GENOTP",
                                                        new XElement("statuscode", "0"),
                                                        new XElement("status", "FAILURE"),
                                                        new XElement("error_code", ds.Tables[0].Rows[0]["message"].ToString()),
                                                        new XElement("message", "Error in process"),
                                                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                        ));
                                                    string _encodedXML = stud.ToString();
                                                    Response.ClearHeaders();
                                                    Response.AddHeader("content-type", "text/xml");
                                                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                }
                                            }
                                            else
                                            {
                                                stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                new XElement("GENOTP",
                                                    new XElement("statuscode", "0"),
                                                    new XElement("status", "FAILURE"),
                                                    new XElement("error_code", "Transaction not found"),
                                                    new XElement("message", "Transaction not found in refundable list"),
                                                    new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                    ));
                                                string _encodedXML = stud.ToString();
                                                Response.ClearHeaders();
                                                Response.AddHeader("content-type", "text/xml");
                                                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                            }
                                        }
                                        else
                                        {
                                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("GENOTP",
                                                new XElement("statuscode", "0"),
                                                new XElement("status", "FAILURE"),
                                                new XElement("error_code", "Insufficient or Invalid Parameters"),
                                                new XElement("message", "Please check API doc"),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                ));
                                            string _encodedXML = stud.ToString();
                                            Response.ClearHeaders();
                                            Response.AddHeader("content-type", "text/xml");
                                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                        }
                                    }
                                    #endregion
                                    #region generateREFUND
                                    else if (Request.Form["trantype"].ToString().ToUpper() == "DMRREFUND")
                                    {
                                        if (Request.Form["banktxnid"] != null && Request.Form["otp"] != null)
                                        {
                                            string banktxnid = Request.Form["banktxnid"].ToString().Replace("'", "").Replace("-", "");
                                            string otp = Request.Form["otp"].ToString().Replace("'", "").Replace("-", "");
                                            MoneyTransfer mm = new MoneyTransfer();
                                            DataTable dt = cls.select_data_dt("Exec RP_DMR_transaction_refund 0,'" + banktxnid + "'");
                                            if (dt.Rows.Count > 0)
                                            {
                                                if (dt.Rows[0]["status"].ToString().ToLower() == "refundable" && otp.Length == 6)
                                                {
                                                    cls.select_data_dt("Update MM_fundtransfer set statuscode='refundinprocess' where transactionstatus='" + banktxnid + "'");
                                                    string trnasctionid = dt.Rows[0]["BankTXNID"].ToString();
                                                    string referenceid = dt.Rows[0]["TxnID"].ToString();
                                                    string type = "imps";
                                                    string depositor = dt.Rows[0]["custMobile"].ToString();
                                                    string postData = mm.SetPostData_GenerateUdmrRefund(trnasctionid, referenceid, type, otp, depositor);
                                                    DataSet ds = new DataSet();
                                                    ds = mm.MakeTransaction_new(postData);
                                                    if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "1")
                                                    {
                                                        cls.select_data_dt("Exec DMR_GenerateRefund '" + banktxnid + "','Faield Bcz NPCI/Issuing bank is not connected or down Benename:'");
                                                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                        new XElement("GENOTP",
                                                            new XElement("statuscode", "1"),
                                                            new XElement("status", "SUCCESS"),
                                                            new XElement("error_code", ""),
                                                            new XElement("message", "Transaction refunded success."),
                                                            new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                            ));
                                                        string _encodedXML = stud.ToString();
                                                        Response.ClearHeaders();
                                                        Response.AddHeader("content-type", "text/xml");
                                                        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                    }
                                                    else
                                                    {
                                                        cls.select_data_dt("Update MM_fundtransfer set statuscode='refundable' where transactionstatus='" + banktxnid + "'");
                                                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                        new XElement("GENOTP",
                                                            new XElement("statuscode", "0"),
                                                            new XElement("status", "FAILED"),
                                                            new XElement("error_code", ds.Tables[0].Rows[0]["message"].ToString()),
                                                            new XElement("message", "Transaction failed"),
                                                            new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                            ));
                                                        string _encodedXML = stud.ToString();
                                                        Response.ClearHeaders();
                                                        Response.AddHeader("content-type", "text/xml");
                                                        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("DMRREFUND",
                                                new XElement("statuscode", "0"),
                                                new XElement("status", "FAILURE"),
                                                new XElement("error_code", "Insufficient or Invalid Parameters"),
                                                new XElement("message", "Please check API doc"),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                ));
                                            string _encodedXML = stud.ToString();
                                            Response.ClearHeaders();
                                            Response.AddHeader("content-type", "text/xml");
                                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                        }

                                    }
                                    #endregion

                                    #region Remitter Registration
                                    else if (Request.Form["trantype"].ToString().ToUpper() == "REMREG")
                                    {
                                        if (Request.Form["banktxnid"] != null)
                                        {
                                            string banktxnid = Request.Form["banktxnid"].ToString().Replace("'", "").Replace("-", "");
                                            MoneyTransfer mm = new MoneyTransfer();
                                            DataTable dt = new DataTable();
                                            dt = cls.select_data_dt("Exec RP_DMR_transaction_refund 0,'" + banktxnid + "'");
                                            if (dt.Rows.Count > 0)
                                            {
                                                string trnasctionid = dt.Rows[0]["BankTXNID"].ToString();
                                                string referenceid = dt.Rows[0]["TxnID"].ToString();
                                                string type = "resend";
                                                string depositor = dt.Rows[0]["custMobile"].ToString();
                                                string postData = mm.SetPostData_GenerateOTPRefund(trnasctionid, referenceid, type);
                                                DataSet ds = new DataSet();
                                                ds = mm.MakeTransaction_new(postData);
                                                if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "1")
                                                {
                                                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                    new XElement("GENOTP",
                                                        new XElement("statuscode", "1"),
                                                        new XElement("status", "SUCCESS"),
                                                        new XElement("error_code", ""),
                                                        new XElement("message", "OTP sent successfully"),
                                                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                        ));
                                                    string _encodedXML = stud.ToString();
                                                    Response.ClearHeaders();
                                                    Response.AddHeader("content-type", "text/xml");
                                                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                }
                                                else
                                                {
                                                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                    new XElement("GENOTP",
                                                        new XElement("statuscode", "0"),
                                                        new XElement("status", "FAILURE"),
                                                        new XElement("error_code", ds.Tables[0].Rows[0]["message"].ToString()),
                                                        new XElement("message", "Error in process"),
                                                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                        ));
                                                    string _encodedXML = stud.ToString();
                                                    Response.ClearHeaders();
                                                    Response.AddHeader("content-type", "text/xml");
                                                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                }
                                            }
                                            else
                                            {
                                                stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                new XElement("GENOTP",
                                                    new XElement("statuscode", "0"),
                                                    new XElement("status", "FAILURE"),
                                                    new XElement("error_code", "Transaction not found"),
                                                    new XElement("message", "Transaction not found in refundable list"),
                                                    new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                    ));
                                                string _encodedXML = stud.ToString();
                                                Response.ClearHeaders();
                                                Response.AddHeader("content-type", "text/xml");
                                                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                            }
                                        }
                                        else
                                        {
                                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("GENOTP",
                                                new XElement("statuscode", "0"),
                                                new XElement("status", "FAILURE"),
                                                new XElement("error_code", "Insufficient or Invalid Parameters"),
                                                new XElement("message", "Please check API doc"),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now.Date))
                                                ));
                                            string _encodedXML = stud.ToString();
                                            Response.ClearHeaders();
                                            Response.AddHeader("content-type", "text/xml");
                                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("RechargeResponse",
                                                new XElement("txid", "0"),
                                                new XElement("status", "Failure"),
                                                new XElement("error_code", "IVP"),
                                                new XElement("operator_ref", ""),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                                ));
                                    string _encodedXML = stud.ToString();
                                    Response.ClearHeaders();
                                    Response.AddHeader("content-type", "text/xml");
                                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                    //Response.Write("0,FAILURE,IVP, ," + DateTime.Now);
                                }
                            }
                            else
                            {
                                stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("RechargeResponse",
                                                new XElement("txid", "0"),
                                                new XElement("status", "Failure"),
                                                new XElement("error_code", "IVP"),
                                                new XElement("operator_ref", ""),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                                ));
                                string _encodedXML = stud.ToString();
                                Response.ClearHeaders();
                                Response.AddHeader("content-type", "text/xml");
                                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                //Response.Write("0,FAILURE,IVP, ," + DateTime.Now);
                            }
                        }
                        else
                        {
                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("RechargeResponse",
                                                new XElement("txid", "0"),
                                                new XElement("status", "Failure"),
                                                new XElement("error_code", "IVP"),
                                                new XElement("operator_ref", ""),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                                ));
                            string _encodedXML = stud.ToString();
                            Response.ClearHeaders();
                            Response.AddHeader("content-type", "text/xml");
                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                            //Response.Write("0,FAILURE,IAU, ," + DateTime.Now);
                        }
                    }
                    else
                    {
                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("RechargeResponse",
                                                new XElement("txid", "0"),
                                                new XElement("status", "Failure"),
                                                new XElement("error_code", "IVU"),
                                                new XElement("operator_ref", ""),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                                ));
                        string _encodedXML = stud.ToString();
                        Response.ClearHeaders();
                        Response.AddHeader("content-type", "text/xml");
                        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                        //Response.Write("0,FAILURE,IVU, ," + DateTime.Now);
                    }
                }
                else
                {
                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                    new XElement("DMR_RESPONSE",
                        new XElement("txid", "0"),
                        new XElement("status", "Failure"),
                        new XElement("error_code", "IVU"),
                        new XElement("operator_ref", ""),
                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                        ));
                    string _encodedXML = stud.ToString();
                    Response.ClearHeaders();
                    Response.AddHeader("content-type", "text/xml");
                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());

                }
            }
        }
        catch (Exception ex)
        {
            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
            new XElement("RechargeResponse",
                new XElement("txid", "0"),
                new XElement("status", "Unknown"),
                new XElement("error_code", "Invalid Request !! Request has been forwarded to Admin."),
                new XElement("operator_ref", ex.Message),
                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                ));
            string _encodedXML = stud.ToString();
            Response.ClearHeaders();
            Response.AddHeader("content-type", "text/xml");
            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
        }
    }
    public double TotupAmount(double amount, DataTable dtMemberMaster)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            //cls_connection cls = new cls_connection();
            //DataTable dtMemberMaster = cls.select_data_dt("Select * from tblmlm_membermaster where msrno='" + msrno + "'");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt("Select top 1 * from tblMM_Surcharge where startval<=" + amount.ToString() + " and endval>=" + amount.ToString() + " and packageid='" + PackageID + "' order by id");
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
    public string Noble_GetTransactionID_New()
    {
        Random rnd = new Random();
        Int64 month = rnd.Next(10000, 99999);
        month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(10000, 99999)));
        return month.ToString();
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
        catch
        {
            return "1";
        }
    }

    public string HTTPPOST(string Url, string Data)
    {
        string Out = String.Empty;
        HttpWebRequest reqq = WebRequest.Create(Url) as HttpWebRequest;
        reqq.Accept = "application/json";
        reqq.ContentType = "application/json";
        reqq.Method = "POST";
        reqq.Timeout = 100000;
        byte[] sentData = Encoding.UTF8.GetBytes(Data);
        reqq.ContentLength = sentData.Length;
        using (System.IO.Stream sendStream = reqq.GetRequestStream())
        {
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
        }
        System.Net.WebResponse res = reqq.GetResponse();
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
        return Out;
    }

}