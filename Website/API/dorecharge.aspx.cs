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
public partial class api_dorecharge : System.Web.UI.Page
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

                if (Request.QueryString["memberid"] != null)
                {
                    string memberid = Request.QueryString["memberid"].ToString().Replace("'", "").Replace("-", "");
                    DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where MemberID='" + memberid + "'");
                    if (dtMemberMaster.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtMemberMaster.Rows[0]["IsActive"]) == true)
                        {
                            if (Request.QueryString["pin"] != null)
                            {
                                if (Convert.ToString(dtMemberMaster.Rows[0]["transactionpassword"]) == Request.QueryString["pin"].ToString().Replace("'", "").Replace("-", ""))
                                {
                                    if (Request.QueryString["amount"] != null)
                                    {
                                        if (Request.QueryString["number"] != null)
                                        {
                                            if (Request.QueryString["operator"] != null && Request.QueryString["circle"] != null)
                                            {
                                                //string password = Request.QueryString["password"];
                                                string amount = Request.QueryString["amount"].ToString().Replace("'", "").Replace("-", "");
                                                //string number = Request.QueryString["number"].ToString().Replace("'", "").Replace("-", "");
                                                string number = Request.QueryString["number"].ToString().Replace("'", "");

                                                string account = "";
                                                if (Request.QueryString["account"] != null)
                                                    account = Request.QueryString["account"].ToString().Replace("'", "").Replace("-", "");


                                                DataTable dtOperatorCode = objconnection.select_data_dt("select OperatorID from tblRecharge_Operator where OperatorCode='" + Request.QueryString["operator"].ToString().Replace("'", "").Replace("-", "") + "'");
                                                DataTable dtCircleCode = objconnection.select_data_dt("select CircleID from tblRecharge_Circle where CircleCode='" + Request.QueryString["circle"].ToString().Replace("'", "").Replace("-", "") + "'");

                                                int OperatorID = Convert.ToInt32(dtOperatorCode.Rows[0]["OperatorID"]);
                                                int CircleID = 0;
                                                if (dtCircleCode.Rows.Count > 0)
                                                    CircleID = Convert.ToInt32(dtCircleCode.Rows[0]["CircleID"]);
                                                else
                                                    CircleID = 19;
                                                int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                                                int PackageID = Convert.ToInt32(dtMemberMaster.Rows[0]["PackageID"]);

                                                dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", MsrNo);
                                                if (Convert.ToDecimal(amount) > Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"]))
                                                {
                                                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                        new XElement("RechargeResponse",
                                                        new XElement("txid", "0"),
                                                        new XElement("status", "Failure"),
                                                        new XElement("error_code", "Low Balance"),
                                                        new XElement("operator_ref", ""),
                                                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                                    ));
                                                    string _encodedXML = stud.ToString();
                                                    Response.ClearHeaders();
                                                    Response.AddHeader("content-type", "text/xml");
                                                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                    //Response.Write("0,FAILURE,LWB, ," + DateTime.Now);
                                                }
                                                else
                                                {
                                                    string TransID = "";
                                                    if (Request.QueryString["usertx"] != null)
                                                        TransID = Request.QueryString["usertx"].ToString().Replace("'", "").Replace("-", "");
                                                    else
                                                        TransID = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();

                                                    int ccnt = cls.select_data_scalar_int("Select count(*) from tblrecharge_history where transid='" + TransID + "'");
                                                    if (ccnt == 0)
                                                    {
                                                        int i = objHistory.AddEditHistory(0, MsrNo, number, account, Convert.ToDecimal(amount), OperatorID, CircleID, TransID, "", "", "Queued");
                                                        if (i > 0)
                                                        {
                                                            objEWalletTransaction.EWalletTransaction(memberid, -Convert.ToDecimal(amount), "Dr", "Recharge to " + number + " (Txn ID : " + TransID + ")");
                                                            RechargeDone(i, MsrNo, memberid, PackageID, OperatorID, CircleID, amount, number, account, TransID);
                                                        }
                                                        else
                                                        {
                                                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                                new XElement("RechargeResponse",
                                                                new XElement("txid", "0"),
                                                                new XElement("status", "Failure"),
                                                                new XElement("error_code", "Duplicate Transaction"),
                                                                new XElement("operator_ref", ""),
                                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                                            ));
                                                            string _encodedXML = stud.ToString();
                                                            Response.ClearHeaders();
                                                            Response.AddHeader("content-type", "text/xml");
                                                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                                            //Response.Write("0,FAILURE,LWB, ," + DateTime.Now);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                                           new XElement("RechargeResponse",
                                                           new XElement("txid", "0"),
                                                           new XElement("status", "Failure"),
                                                           new XElement("error_code", "Duplicate Transaction ID"),
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
                                        }
                                        else
                                        {
                                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("RechargeResponse",
                                                new XElement("txid", "0"),
                                                new XElement("status", "Failure"),
                                                new XElement("error_code", "IVN"),
                                                new XElement("operator_ref", ""),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                                ));

                                            string _encodedXML = stud.ToString();
                                            Response.ClearHeaders();
                                            Response.AddHeader("content-type", "text/xml");
                                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                            //Response.Write("0,FAILURE,IVN, ," + DateTime.Now);
                                        }
                                    }
                                    else
                                    {
                                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                            new XElement("RechargeResponse",
                                                new XElement("txid", "0"),
                                                new XElement("status", "Failure"),
                                                new XElement("error_code", "IVA"),
                                                new XElement("operator_ref", ""),
                                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                                ));
                                        string _encodedXML = stud.ToString();
                                        Response.ClearHeaders();
                                        Response.AddHeader("content-type", "text/xml");
                                        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                                        //Response.Write("0,FAILURE,IVA, ," + DateTime.Now);
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
    public string Noble_GetTransactionID_New()
    {
        Random rnd = new Random();
        Int64 month = rnd.Next(10000, 99999);
        month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(10000, 99999)));
        return month.ToString();
    }
    protected int ValidateMemberRequest(int msrno)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Exec ValidateMember '" + msrno.ToString() + "'");
        int ss = Convert.ToInt32(dt.Rows[0][0]);
        return ss;
    }
    private void RechargeDone(int HistoryID, int MsrNo, string memberid, int PackageID, int OperatorID, int ProfileID, string amount, string number, string account, string TransID)
    {
        XDocument stud;
        #region RechargeDONE
        try
        {

            cls_myMember clsm = new cls_myMember();
            if (ValidateMemberRequest(MsrNo) == 0)
            {
                DataTable dtMemberMaster = cls.select_data_dt("select msrno,memberid,firstname + ' ' + lastname as Membername from tblMLM_MemberMaster where msrno='" + MsrNo.ToString() + "'");
                string Recharge_Result = clsm.Cyrus_RechargeProcess(HistoryID, Convert.ToString(ProfileID), account, dtMemberMaster);
                char Splitter = Convert.ToChar(",");
                string[] split = Recharge_Result.Split(Splitter);
                if (split[0] == "Recharge Successful !!")
                {
                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                        new XElement("RechargeResponse",
                        new XElement("txid", TransID),
                        new XElement("status", "Success"),
                        new XElement("error_code", split[2].ToString()),
                        new XElement("operator_ref", split[3].ToString()),
                       new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                    ));
                    string _encodedXML = stud.ToString();
                    Response.ClearHeaders();
                    Response.AddHeader("content-type", "text/xml");
                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                }
                else if (split[0] == "Recharge Failed !!")
                {
                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                         new XElement("RechargeResponse",
                         new XElement("txid", TransID),
                         new XElement("status", "Failure"),
                         new XElement("error_code", split[2].ToString()),
                         new XElement("operator_ref", split[3].ToString()),
                         new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                     ));
                    string _encodedXML = stud.ToString();
                    Response.ClearHeaders();
                    Response.AddHeader("content-type", "text/xml");
                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                }
                else if (split[0] == "Recharge Pending !!")
                {
                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                        new XElement("RechargeResponse",
                        new XElement("txid", TransID),
                        new XElement("status", "Pending"),
                        new XElement("error_code", "PEN"),
                        new XElement("operator_ref", split[3].ToString()),
                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
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
                        new XElement("RechargeResponse",
                        new XElement("txid", TransID),
                        new XElement("status", "Failure"),
                        new XElement("error_code", "Account blocked"),
                        new XElement("operator_ref", ""),
                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                    ));
                string _encodedXML = stud.ToString();
                Response.ClearHeaders();
                Response.AddHeader("content-type", "text/xml");
                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
            }
        }
        catch (Exception ex)
        {
            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                        new XElement("RechargeResponse",
                        new XElement("txid", TransID),
                        new XElement("status", "Requested"),
                        new XElement("error_code", "Operator Issue Found"),
                        new XElement("operator_ref", ""),
                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                    ));
            string _encodedXML = stud.ToString();
            Response.ClearHeaders();
            Response.AddHeader("content-type", "text/xml");
            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
        }
        #endregion
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
    protected void ProcessPendingStatus(int historyid, string flag)
    {
        string OperatorRef = "";
        DataTable dtHistory = new DataTable();
        dtHistory = objHistory.ManageHistory("GetAll", Convert.ToInt32(historyid));
        dtAPI = objAPI.ManageAPI("Get", Convert.ToInt32(dtHistory.Rows[0]["APIID"]));
        int StatusPosition = Convert.ToInt32(dtAPI.Rows[0]["S_StatusPosition"]);
        string Success = Convert.ToString(dtAPI.Rows[0]["Success"]);
        string Failed = Convert.ToString(dtAPI.Rows[0]["Failed"]);
        var status = "";
        if (dtAPI.Rows[0]["StatusURL"].ToString() != "")
        {
            string str = "";
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
                //str = str + dtAPI.Rows[0]["S_prm3"].ToString() + "=" + Convert.ToString(dtHistory.Rows[0]["APITransID"]);
                if (Convert.ToInt32(dtHistory.Rows[0]["APIID"]) == 2)
                    str = str + dtAPI.Rows[0]["S_prm3"].ToString() + "=" + Convert.ToString(dtHistory.Rows[0]["TransID"]);
                else
                    str = str + dtAPI.Rows[0]["S_prm3"].ToString() + "=" + Convert.ToString(dtHistory.Rows[0]["aPITransID"]);
            }
            //if (dtAPI.Rows[0]["S_prm4"].ToString() != "")
            //{
            //    str = str + dtAPI.Rows[0]["S_prm4"].ToString() + "=XXXXX&";
            //}
            if (str.EndsWith("&"))
                str = str.Substring(0, str.Length - 1);
            string result = apicall(str);
            string[] split = result.Split(',');
            if (split[0].ToString() == "EMPTY TRANSACTION ID")
                status = "reversed";
            else
                status = split[StatusPosition];


            if (split.Length == 6 && Convert.ToInt32(dtHistory.Rows[0]["APIID"]) == 2)
            {
                OperatorRef = split[5].ToString();
            }
        }
        if (flag == "P")
        {
            if (status.ToLower() == Success.ToLower())
            {
                DataTable i = objHistory.UpdateHistory("UpdateStatus", historyid, Convert.ToInt32(dtHistory.Rows[0]["MsrNo"]), 0, 0, 0, 0, "", "", "Success", "", "", OperatorRef);
            }
            else if (status.ToLower() == Failed.ToLower() || status.ToLower() == "reversed")
            {
                objEWalletTransaction.EWalletTransaction(Convert.ToString(dtHistory.Rows[0]["MemberID"]), Convert.ToDecimal(dtHistory.Rows[0]["RechargeAmount"]), "Cr", "Return amount (Txn ID : " + dtHistory.Rows[0]["TransID"].ToString() + ")");
                DataTable i = objHistory.UpdateHistory("UpdateStatus", historyid, 0, 0, 0, 0, 0, "", "", "Failed", "", "", OperatorRef);
            }
        }
        else
        {
            cls.update_data("Update tblRecharge_history set APImessage='" + OperatorRef + "' where historyid='" + historyid + "'");
        }
    }
}