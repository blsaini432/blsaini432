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
using System.Text.RegularExpressions;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Xml;
using System.Globalization;
using BLL.MLM;


public partial class EzulixApp_InstantPayout : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    clsMLM_RWalletTransaction objRWalletTransaction = new clsMLM_RWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    private static int limitamount = 5000;
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
    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }
    protected string ReplaceCode(string str)
    {
        return str.Replace("'", "").Replace("-", "").Replace(";", "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["operationname"] != null)
        {
            string OperationName = Request.Form["operationname"].ToString();

            #region Instant Payout
            if (OperationName == "otpsendinstpayout")
            {
                if (Request.Form["Amount"] != null && Request.Form["Mobile"] != null && Request.Form["MemberId"] != null)
                {
                    string amount = ReplaceCode(Request.Form["Amount"].ToString().Trim());
                    string mobile = ReplaceCode(Request.Form["Mobile"].ToString().Trim());
                    string Memberid = ReplaceCode(Request.Form["MemberId"].ToString().Trim());
                    cls_connection Cls = new cls_connection();
                    DataTable dt = new DataTable();
                    DataTable dtmember = cls.select_data_dt("select * from tblmlm_membermaster where MemberId='" + Memberid + "'");
                    if (dtmember.Rows.Count > 0)
                    {
                        int msrno = Convert.ToInt32(dtmember.Rows[0]["MsrNo"]);
                        dt = Cls.select_data_dt(@"exec Set_EzulixDmr @action='instpayout', @msrno=" + msrno + "");
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(dt.Rows[0]["instantpayout"]) == false)
                            {
                                ReturnError("Payout DMR Is Inactive Contact your admin!", "Unknown");
                            }
                            else
                            {
                                cls_myMember Clsm = new cls_myMember();
                                double NetAmount = PayoutTotupAmount(Convert.ToDouble(amount), dtmember.Rows[0]["MemberId"].ToString());
                                if (NetAmount > Convert.ToDouble(amount))
                                {
                                    int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), Convert.ToInt32(msrno));
                                    if (res == 1)
                                    {
                                        Random random = new Random();
                                        int SixDigit = random.Next(1000, 9999);
                                        string[] valueArray = new string[2];
                                        valueArray[0] = amount;
                                        valueArray[1] = SixDigit.ToString();
                                        SendWithVarpan(mobile, 1, valueArray);
                                        Response.Write("{ " + OperationName + ":" + SixDigit.ToString() + "}");
                                    }
                                    else
                                    {
                                        ReturnError("Wallet balance is insufficent for this transcation !", "Unknown");
                                    }
                                }
                                else
                                {

                                    ReturnError("Surcharge not define.", "Unknown");
                                }
                            }
                        }
                        else
                        {
                            ReturnError("Payout Service not Active.", "Unknown");
                        }
                    }
                    else
                    {
                        ReturnError("No Member Found", "Unknown");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
            }
            else if (OperationName == "instpayoutreport")
            {
                #region MemberReport
                if (Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string memberid = string.Empty;
                    memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    int msrNo = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + memberid + "'");
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@msrno", value = msrNo });
                    _lstparm.Add(new ParmList() { name = "@datefrom", value = "01-01-1990" });
                    _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
                    DataTable dttra = Cls.select_data_dtNew(@"Ezulix_PayOut_Instant_Report", _lstparm);
                    if (dttra.Rows.Count > 0)
                    {
                        string output = ConvertDataTabletoString(dttra);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("No Transcation found", "Unknown");
                    }

                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }
                #endregion
            }
            else if (OperationName == "trainstpayout")
            {
                if (Request.Form["account"] != null && Request.Form["ifsc"] != null && Request.Form["amount"] != null)
                {
                    payout_mysun PayOuts = new payout_mysun();
                    cls_connection Cls = new cls_connection();
                    cls_myMember Clsm = new cls_myMember();
                    DataTable dt = new DataTable();
                    string msrno = ReplaceCode(Request.Form["msrno"].ToString().Trim());
                    string memberid = ReplaceCode(Request.Form["Memberid"].ToString().Trim());
                    string amount = ReplaceCode(Request.Form["amount"].ToString().Trim());
                    string account = ReplaceCode(Request.Form["account"].ToString().Trim());
                    string ifsc = ReplaceCode(Request.Form["ifsc"].ToString().Trim());
                    string type = ReplaceCode(Request.Form["type"].ToString().Trim());
                    string name = ReplaceCode(Request.Form["name"].ToString().Trim());
                    dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instpayout', @msrno=" + msrno + "");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["instantpayout"]) == false)
                        {
                            ReturnError("Payout DMR Is Inactive Contact your admin!", "Unknown");
                        }
                        else
                        {
                            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(2020, 1, 1))).TotalSeconds;
                            int timestamp = unixTimestamp;
                            string sp_key = type;
                            string bene_name = name;
                            string latitude = "26.912434";
                            string longitude = "75.787270";
                            string endpoint_ip = Request.UserHostAddress;
                          //  string endpoint_ip = "106.0.56.71";
                            string remarks = "Payout";
                            int otp_auth = 0;
                            Random random = new Random();
                            int SixDigit = random.Next(10000, 99999);
                            cls_myMember clsm = new cls_myMember();
                            string external_ref = clsm.Cyrus_GetTransactionID_New();
                            string debit_account = "107305006606";
                            string credit_account = account;
                            string ifs_code = ifsc;
                            string Result = string.Empty;
                            double NetAmount = PayoutTotupAmount(Convert.ToDouble(amount), memberid);
                            if (NetAmount > Convert.ToDouble(amount))
                            {
                                int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), Convert.ToInt32(msrno));
                                if (res == 1)
                                {
                                    int tra = Clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal("-" + NetAmount), "Dr", "APP Inst Payout Topup Txn:- " + external_ref + "");
                                    if (tra > 0)
                                    {
                                        List<ParmList> _lstparm = new List<ParmList>();
                                        _lstparm.Add(new ParmList() { name = "@MemberId", value = memberid });
                                        _lstparm.Add(new ParmList() { name = "@MsrNo", value = msrno });
                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(amount) });
                                        _lstparm.Add(new ParmList() { name = "@beneficiaryAccount", value = credit_account });
                                        _lstparm.Add(new ParmList() { name = "@beneficiaryIFSC", value = ifs_code });
                                        _lstparm.Add(new ParmList() { name = "@txnstatus", value = "Pending" });
                                        _lstparm.Add(new ParmList() { name = "@transaction_types_id", value = "" });
                                        _lstparm.Add(new ParmList() { name = "@transaction_status_id", value = "" });
                                        _lstparm.Add(new ParmList() { name = "@open_transaction_ref_id", value = sp_key });
                                        _lstparm.Add(new ParmList() { name = "@purpose", value = remarks });
                                        _lstparm.Add(new ParmList() { name = "@recepient_name", value = bene_name });
                                        _lstparm.Add(new ParmList() { name = "@merchant_ref_id", value = external_ref });
                                        _lstparm.Add(new ParmList() { name = "@debit_account_number", value = debit_account });
                                        _lstparm.Add(new ParmList() { name = "@NetAmount", value = Convert.ToDecimal(NetAmount) });
                                        _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                                        Cls.select_data_dtNew("SET_Ezulix_PayOut_MoneyTransfer_new", _lstparm);

                                        Result = PayOuts.InitiatePayouts(sp_key, debit_account, external_ref, credit_account, ifs_code, amount, latitude, longitude, endpoint_ip, bene_name, remarks, otp_auth);
                                        DataSet ds = Deserialize(Result);
                                        if (ds.Tables.Contains("root") && ds.Tables["root"].Columns.Contains("statuscode"))
                                        {
                                            if (ds.Tables["root"].Rows[0]["statuscode"].ToString() == "TXN")
                                            {
                                                List<ParmList> _lstparms = new List<ParmList>();
                                                _lstparms.Add(new ParmList() { name = "@txnstatus", value = ds.Tables["root"].Rows[0]["status"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@status", value = ds.Tables["root"].Rows[0]["statuscode"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@transaction_status_id", value = ds.Tables["root"].Rows[0]["statuscode"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@charged_amt", value = ds.Tables["data"].Rows[0]["charged_amt"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@commercial_value", value = ds.Tables["data"].Rows[0]["commercial_value"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["data"].Rows[0]["ipay_id"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@ipay_uuid", value = ds.Tables["root"].Rows[0]["ipay_uuid"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@orderid", value = ds.Tables["root"].Rows[0]["orderid"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@debit_account_number", value = ds.Tables["data"].Rows[0]["debit_account"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@credit_refid", value = ds.Tables["payout"].Rows[0]["credit_refid"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@merchant_ref_id", value = ds.Tables["data"].Rows[0]["external_ref"].ToString() });
                                                _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                                                Cls.select_data_dtNew("SET_Ezulix_PayOut_MoneyTransfer_new", _lstparms);
                                                Response.Write("{ " + OperationName + ":" + Result + "}");
                                            }
                                            else if (ds.Tables["root"].Rows[0]["statuscode"].ToString() == "ERR")
                                            {
                                                List<ParmList> _lstparmss = new List<ParmList>();
                                                _lstparmss.Add(new ParmList() { name = "@merchant_ref_id", value = external_ref });
                                                _lstparmss.Add(new ParmList() { name = "@txnstatus", value = ds.Tables["root"].Rows[0]["status"].ToString() });
                                                _lstparmss.Add(new ParmList() { name = "@status", value = ds.Tables["root"].Rows[0]["statuscode"].ToString() });
                                                _lstparmss.Add(new ParmList() { name = "@Action", value = "err" });
                                                Cls.select_data_dtNew("SET_Ezulix_PayOut_MoneyTransfer_new", _lstparmss);
                                                Clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "APP Inst Payout Fail Txn:-" + external_ref + "");
                                                Response.Write("{ " + OperationName + ":" + Result + "}");
                                            }
                                          
                                        }
                                        else
                                        {
                                            ReturnError("Error |Some Error Found Please Try again", OperationName);
                                        }

                                    }
                                    else
                                    {
                                        ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                    }
                                }
                                else
                                {
                                    ReturnError("Error | You do not have sufficient Wallet Balance", OperationName);
                                }
                            }
                            else
                            {
                                ReturnError("Error | Surcharge not define", OperationName);
                            }

                        }
                    }
                    else
                    {
                        ReturnError("Payout Is Inactive Contact your admin!", "Unknown");
                    }

                }
                else
                {
                    ReturnError("Invalid Request Format", "Unknown");
                }

            }

            #endregion
        }
        else
        {
            ReturnError("Invalid Request Format", "Unknown");
        }
    }
    private DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
    private void setControl()
    {
        Session["PayOrderId"] = null;
        Session["tx"] = null;
        Session["txtAmount"] = null;
        Session["Returnurl"] = null;
    }
    public double PayoutTotupAmount(double amount, string memberid)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            int msrNo = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + memberid + "'");
            DataTable dtMemberMaster = cls.select_data_dt(@"EXEC Set_EzulixPayOut_instnew @action='chk', @msrno=" + Convert.ToInt32(msrNo) + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixPayOut_instnew @action='payoutsur',@amount='" + Convert.ToDecimal(amount) + "',@packageid='" + PackageID + "'");
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
    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }

    public static void SendWithVarpan(string Mobile, int Template, string[] ValueArray)
    {
        try
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string smsMessage = GetString(Template, ValueArray);
            string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=198296AFda5tMRgn5a854e41&route=4&sender=EZXDMT&DLT_TE_ID=1207160975824663033&mobiles=" + Mobile + "& message=" + smsMessage + "";

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
            return fileData;
        }
        else
        {
            for (int i = ValueArray.GetLowerBound(0); i <= ValueArray.GetUpperBound(0); i++)
            {
                fileData = fileData.Replace("@v" + i.ToString() + "@", (string)ValueArray[i]);
            }
            return fileData;
        }
    }

    public static string[] arrTemplate = new string[]
   {
        "Zero",
       "@v1@ is your OTP to  access DMT Transaction for Rs. @v0@  and Never Share it with anyone. Bank Never calls to verify it."//1
   };
}

