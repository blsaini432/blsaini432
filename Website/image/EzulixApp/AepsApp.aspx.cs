using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public partial class EzulixApp_AepsApp : System.Web.UI.Page
{

    #region Properites
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    EzulixAepsV1 EAeps = new EzulixAepsV1();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["OperationName"] != null)
        {
            string OperationName = Request.Form["OperationName"].ToString();
            if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
            {
                string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                if (OperationName == "search")
                {
                    #region SearchCustmer
                    if (Request.Form["Agentd_id"] != null && Request.Form["CUSTOMER_MOBILE"] != null)
                    {
                        DataTable dt = cls.select_data_dt("Exec aeps_app_getdetails @action='search',@mcode='" + mcode + "',@deviceid='" + deviceid + "',@loginip='" + loginip + "'");
                        if (dt.Rows[0]["RESP_CODE"].ToString() == "1")
                        {
                            string Agentd_id = ReplaceCode(Request.Form["Agentd_id"].ToString().Trim());
                            string CUSTOMER_MOBILE = ReplaceCode(Request.Form["CUSTOMER_MOBILE"].ToString().Trim());
                            String Result = EAeps.searchcustmer(Agentd_id, CUSTOMER_MOBILE);
                            Response.Write("{ " + OperationName + ":" + Result + "}");
                        }
                        else
                        {
                            string json = ConvertDataTabletoJson(dt);
                            Response.Write("{ " + OperationName + ":" + json + "}");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", OperationName);
                    }
                    #endregion
                }
                else if (OperationName == "custmerreg")
                {
                    #region CustmerRegistration
                    if (Request.Form["Agentd_id"] != null && Request.Form["CUSTOMER_MOBILE"] != null && Request.Form["CustmerName"] != null)
                    {
                        DataTable dt = cls.select_data_dt("Exec aeps_app_getdetails @action='custmerreg',@mcode='" + mcode + "',@deviceid='" + deviceid + "',@loginip='" + loginip + "'");
                        if (dt.Rows[0]["RESP_CODE"].ToString() == "1")
                        {
                            string Agentd_id = ReplaceCode(Request.Form["Agentd_id"].ToString().Trim());
                            string CUSTOMER_MOBILE = ReplaceCode(Request.Form["CUSTOMER_MOBILE"].ToString().Trim());
                            string CustmerName = ReplaceCode(Request.Form["CustmerName"].ToString().Trim());
                            String Result = EAeps.custmerregistration(Agentd_id, CUSTOMER_MOBILE, CustmerName);
                            Response.Write("{ " + OperationName + ":" + Result + "}");
                        }
                        else
                        {
                            string json = ConvertDataTabletoJson(dt);
                            Response.Write("{ " + OperationName + ":" + json + "}");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", OperationName);
                    }
                    #endregion
                }
                else if (OperationName == "getrd")
                {
                    #region GetRdData
                    if (Request.Form["Agentd_id"] != null && Request.Form["CUSTOMER_MOBILE"] != null && Request.Form["Device"] != null)
                    {
                        DataTable dt = cls.select_data_dt("Exec aeps_app_getdetails @action='getrd',@mcode='" + mcode + "',@deviceid='" + deviceid + "',@loginip='" + loginip + "'");
                        if (dt.Rows[0]["RESP_CODE"].ToString() == "1")
                        {
                            string Agentd_id = ReplaceCode(Request.Form["Agentd_id"].ToString().Trim());
                            string CUSTOMER_MOBILE = ReplaceCode(Request.Form["CUSTOMER_MOBILE"].ToString().Trim());
                            string Device = ReplaceCode(Request.Form["Device"].ToString().Trim());
                            String Result = EAeps.rddata(Agentd_id, CUSTOMER_MOBILE, Device);
                            Response.Write("{ " + OperationName + ":" + Result + "}");
                        }
                        else
                        {
                            string json = ConvertDataTabletoJson(dt);
                            Response.Write("{ " + OperationName + ":" + json + "}");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", OperationName);
                    }
                    #endregion
                }
                else if (OperationName == "balinfo")
                {
                    #region BalanseInformation
                    if (Request.Form["Agentd_id"] != null && Request.Form["IIN"] != null && Request.Form["AadharNumber"] != null && Request.Form["mobileNumber"] != null && Request.Form["BiometricData"] != null)
                    {
                        DataTable dt = cls.select_data_dt("Exec aeps_app_getdetails @action='balinfo',@mcode='" + mcode + "',@deviceid='" + deviceid + "',@loginip='" + loginip + "'");
                        if (dt.Rows[0]["RESP_CODE"].ToString() == "1")
                        {
                            string Agentd_id = ReplaceCode(Request.Form["Agentd_id"].ToString().Trim());
                            string IIN = ReplaceCode(Request.Form["IIN"].ToString().Trim());
                            string AadharNumber = ReplaceCode(Request.Form["AadharNumber"].ToString().Trim());
                            string mobileNumber = ReplaceCode(Request.Form["mobileNumber"].ToString().Trim());
                            string BiometricData = Request.Form["BiometricData"].ToString();
                            String Result = EAeps.balanseinfo(Agentd_id, IIN, AadharNumber, "true", mobileNumber, BiometricData, "0");
                            Response.Write("{ " + OperationName + ":" + Result + "}");
                        }
                        else
                        {
                            string json = ConvertDataTabletoJson(dt);
                            Response.Write("{ " + OperationName + ":" + json + "}");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", OperationName);
                    }
                    #endregion
                }
                else if (OperationName == "balwd")
                {
                    #region AccountWithdrawal
                    if (Request.Form["Agentd_id"] != null && Request.Form["IIN"] != null && Request.Form["AadharNumber"] != null && Request.Form["mobileNumber"] != null && Request.Form["BiometricData"] != null && Request.Form["Amount"] != null)
                    {
                        DataTable dt = cls.select_data_dt("Exec aeps_app_getdetails @action='balinfo',@mcode='" + mcode + "',@deviceid='" + deviceid + "',@loginip='" + loginip + "'");
                        if (dt.Rows[0]["RESP_CODE"].ToString() == "1")
                        {
                            string AadharNumber = ReplaceCode(Request.Form["AadharNumber"].ToString().Trim());
                            byte[] CAadhar = HmacSha512(AadharNumber, AadharNumber);
                            DataTable dtChk = cls.select_data_dt(@"SELECT COUNT(*) FROM t_Ezulix_Aeps_Txn_History WHERE CONVERT (VARCHAR, TxnDate,112)= CONVERT(VARCHAR, GETDATE(), 112) AND CustmerAadhar = '" + CAadhar + "'");
                            if (Convert.ToInt32(dtChk.Rows[0][0]) >= 5)
                            {
                                ReturnError("Your aadhaar transaction limit exceeded.", OperationName);
                            }
                            else
                            {
                                string Agentd_id = ReplaceCode(Request.Form["Agentd_id"].ToString().Trim());
                                string IIN = ReplaceCode(Request.Form["IIN"].ToString().Trim());
                                string mobileNumber = ReplaceCode(Request.Form["mobileNumber"].ToString().Trim());
                                string BiometricData = Request.Form["BiometricData"].ToString();
                                string Amount = ReplaceCode(Request.Form["Amount"].ToString().Trim());
                                String Result = string.Empty;
                                try
                                {
                                    Result = EAeps.accountwithdrawal(Agentd_id, IIN, AadharNumber, "true", mobileNumber, BiometricData, Amount);
                                    ResponceBalanceInfo Obj = JsonConvert.DeserializeObject<ResponceBalanceInfo>(Result);
                                    if (Obj.RESP_CODE == "302")
                                    {
                                        Obj.DATA.BalanceAmountActual = "0";
                                    }
                                    decimal TxnCharge = 0;
                                    if (Obj.DATA != null)
                                    {
                                        if (Obj.DATA.txnCharge == null)
                                        {
                                            TxnCharge = 0;
                                        }
                                        else
                                        {
                                            TxnCharge = Convert.ToDecimal(Obj.DATA.txnCharge);
                                        }
                                        List<ParmList> _listparam = new List<ParmList>();
                                        _listparam.Add(new ParmList() { name = "@RESP_CODE", value = Obj.RESP_CODE });
                                        _listparam.Add(new ParmList() { name = "@RESPONSE", value = Obj.RESPONSE });
                                        _listparam.Add(new ParmList() { name = "@RESP_MSG", value = Obj.RESP_MSG });
                                        _listparam.Add(new ParmList() { name = "@STAN", value = Obj.DATA.STAN });
                                        _listparam.Add(new ParmList() { name = "@RRN", value = Obj.DATA.RRN });
                                        _listparam.Add(new ParmList() { name = "@Aadhar", value = Obj.DATA.Aadhar });
                                        _listparam.Add(new ParmList() { name = "@IIN", value = Obj.DATA.IIN });
                                        _listparam.Add(new ParmList() { name = "@TxnAmount", value = Convert.ToDecimal(Obj.DATA.TxnAmount) });
                                        _listparam.Add(new ParmList() { name = "@ResponseCode", value = Obj.DATA.ResponseCode });
                                        _listparam.Add(new ParmList() { name = "@AccountType", value = Obj.DATA.AccountType });
                                        _listparam.Add(new ParmList() { name = "@BalanceType", value = Obj.DATA.BalanceType });
                                        _listparam.Add(new ParmList() { name = "@CurrancyCode", value = Obj.DATA.CurrancyCode });
                                        _listparam.Add(new ParmList() { name = "@BalanceIndicator", value = Obj.DATA.BalanceIndicator });
                                        _listparam.Add(new ParmList() { name = "@BalanceAmount", value = Obj.DATA.BalanceAmount });
                                        _listparam.Add(new ParmList() { name = "@AccountTypeLedger", value = Obj.DATA.AccountTypeLedger });
                                        _listparam.Add(new ParmList() { name = "@BalanceTypeLedger", value = Obj.DATA.BalanceTypeLedger });
                                        _listparam.Add(new ParmList() { name = "@CurrancyCodeLedger", value = Obj.DATA.CurrancyCodeLedger });
                                        _listparam.Add(new ParmList() { name = "@BalanceIndicatorLedger", value = Obj.DATA.BalanceIndicatorLedger });
                                        _listparam.Add(new ParmList() { name = "@BalanceAmountLedger", value = Obj.DATA.BalanceAmountLedger });
                                        _listparam.Add(new ParmList() { name = "@AccountTypeActual", value = Obj.DATA.AccountTypeActual });
                                        _listparam.Add(new ParmList() { name = "@BalanceTypeActual", value = Obj.DATA.BalanceTypeActual });
                                        _listparam.Add(new ParmList() { name = "@CurrancyCodeActual", value = Obj.DATA.CurrancyCodeActual });
                                        _listparam.Add(new ParmList() { name = "@BalanceIndicatorActual", value = Obj.DATA.BalanceIndicatorActual });
                                        _listparam.Add(new ParmList() { name = "@BalanceAmountActual", value = Convert.ToDecimal(Obj.DATA.BalanceAmountActual) });
                                        _listparam.Add(new ParmList() { name = "@Status", value = Convert.ToInt32(Obj.DATA.Status) });
                                        _listparam.Add(new ParmList() { name = "@UIDAIAuthenticationCode", value = Obj.DATA.UIDAIAuthenticationCode });
                                        _listparam.Add(new ParmList() { name = "@RetailerTxnId", value = Obj.DATA.RetailerTxnId });
                                        _listparam.Add(new ParmList() { name = "@TerminalId", value = Obj.DATA.TerminalId });
                                        _listparam.Add(new ParmList() { name = "@TxnCharge", value = TxnCharge });
                                        _listparam.Add(new ParmList() { name = "@PaidAmount", value = Convert.ToDecimal(Obj.DATA.paidAmount) });
                                        _listparam.Add(new ParmList() { name = "@AID", value = Obj.DATA.AID });
                                        _listparam.Add(new ParmList() { name = "@MemberId", value = Obj.DATA.Agentd_id });
                                        _listparam.Add(new ParmList() { name = "@AdminCommission", value = Convert.ToDecimal(Obj.DATA.Commission) });
                                        _listparam.Add(new ParmList() { name = "@CustmerAadhar", value = CAadhar });
                                        _listparam.Add(new ParmList() { name = "@CustmerMobile", value = mobileNumber });
                                        _listparam.Add(new ParmList() { name = "@Chanel", value = "A" });
                                        cls.select_data_dtNew(@"SET_t_Ezulix_Aeps_Txn_History", _listparam);
                                        if (Obj.RESP_CODE == "300")
                                        {
                                            cls_myMember Clsm = new cls_myMember();
                                            Clsm.AEPSWallet_MakeTransaction_Ezulix(Agentd_id, Convert.ToDecimal(Obj.DATA.TxnAmount), "Cr", "AEPS Topup Txn: " + Obj.DATA.RetailerTxnId + "");
                                            cls.select_data_dt(@"EXEC SET_AEPS_Commission @memberid='" + Agentd_id + "',@txnamount= '" + Convert.ToDecimal(Obj.DATA.TxnAmount) + "',@txnid='" + Obj.DATA.RetailerTxnId + "'");
                                        }
                                    }
                                    else
                                    {
                                        List<ParmList> _listparam = new List<ParmList>();
                                        _listparam.Add(new ParmList() { name = "@RESP_CODE", value = Obj.RESP_CODE });
                                        _listparam.Add(new ParmList() { name = "@RESPONSE", value = Obj.RESPONSE });
                                        _listparam.Add(new ParmList() { name = "@RESP_MSG", value = Obj.RESP_MSG });
                                        _listparam.Add(new ParmList() { name = "@MemberId", value = Agentd_id });
                                        _listparam.Add(new ParmList() { name = "@CustmerAadhar", value = CAadhar });
                                        _listparam.Add(new ParmList() { name = "@CustmerMobile", value = mobileNumber });
                                        cls.select_data_dtNew(@"SET_t_Ezulix_Aeps_Txn_History", _listparam);
                                    }
                                    Response.Write("{ " + OperationName + ":" + Result + "}");
                                }
                                catch (Exception ex)
                                {
                                    cls_connection Cls = new cls_connection();
                                    Cls.select_data_dt(@"EXEC SET_t_Ezulix_Aeps_Txn_History_Err_Log @ServiceType='balwd',@Agentd_id='" + Agentd_id + "',@IIN='" + IIN + "',@mobileNumber='" + mobileNumber + "',@Amount='" + Convert.ToDecimal(Amount) + "',@Response='" + Result + "',@Exception='" + ex.ToString() + "'");
                                    ReturnError("Error", OperationName);
                                }
                            }
                        }
                        else
                        {
                            string json = ConvertDataTabletoJson(dt);
                            Response.Write("{ " + OperationName + ":" + json + "}");
                        }
                    }
                    else
                    {
                        ReturnError("Invalid Request Format", OperationName);
                    }
                    #endregion
                }
            }
            else
            {
                ReturnError("Invalid Request Format", OperationName);
            }
        }
        else
        {
            ReturnError("Invalid Request Format", "Unknown");
        }
    }

    #region Method
    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 404 as RESP_CODE,'" + message + "' as status");
        string output = ConvertDataTabletoJson(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }

    private string ConvertDataTabletoJson(DataTable dt)
    {
        return JsonConvert.SerializeObject(dt);
    }

    private string ReplaceCode(string str)
    {
        return str.Replace("'", "").Replace("-", "").Replace(";", "");
    }

    public static byte[] HmacSha512(string text, string key)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        var hmac = new Org.BouncyCastle.Crypto.Macs.HMac(new Org.BouncyCastle.Crypto.Digests.Sha512Digest());
        hmac.Init(new Org.BouncyCastle.Crypto.Parameters.KeyParameter(System.Text.Encoding.UTF8.GetBytes(key)));
        byte[] result = new byte[hmac.GetMacSize()];
        hmac.BlockUpdate(bytes, 0, bytes.Length);
        hmac.DoFinal(result, 0);
        return result;
    }

    public static string cleanForJSON(string s)
    {
        if (s == null || s.Length == 0)
        {
            return "";
        }

        char c = '\0';
        int i;
        int len = s.Length;
        StringBuilder sb = new StringBuilder(len + 4);
        String t;

        for (i = 0; i < len; i += 1)
        {
            c = s[i];
            switch (c)
            {
                case '\\':
                case '"':
                    sb.Append('\\');
                    sb.Append(c);
                    break;
                case '/':
                    sb.Append('\\');
                    sb.Append(c);
                    break;
                case '\b':
                    sb.Append("\\b");
                    break;
                case '\t':
                    sb.Append("\\t");
                    break;
                case '\n':
                    sb.Append("\\n");
                    break;
                case '\f':
                    sb.Append("\\f");
                    break;
                case '\r':
                    sb.Append("\\r");
                    break;
                default:
                    if (c < ' ')
                    {
                        t = "000" + String.Format("X", c);
                        sb.Append("\\u" + t.Substring(t.Length - 4));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                    break;
            }
        }
        return sb.ToString();
    }
    #endregion

    #region PropertiesClass
    public class BalanceInfo
    {
        public string STAN { get; set; }
        public string RRN { get; set; }
        public string Aadhar { get; set; }
        public string IIN { get; set; }
        public string TxnAmount { get; set; }
        public string ResponseCode { get; set; }
        public string AccountType { get; set; }
        public string BalanceType { get; set; }
        public string CurrancyCode { get; set; }
        public string BalanceIndicator { get; set; }
        public string BalanceAmount { get; set; }
        public string AccountTypeLedger { get; set; }
        public string BalanceTypeLedger { get; set; }
        public string CurrancyCodeLedger { get; set; }
        public string BalanceIndicatorLedger { get; set; }
        public string BalanceAmountLedger { get; set; }
        public string AccountTypeActual { get; set; }
        public string BalanceTypeActual { get; set; }
        public string CurrancyCodeActual { get; set; }
        public string BalanceIndicatorActual { get; set; }
        public string BalanceAmountActual { get; set; }
        public string Status { get; set; }
        public string UIDAIAuthenticationCode { get; set; }
        public string RetailerTxnId { get; set; }
        public string TerminalId { get; set; }
        public string txnDate { get; set; }
        public string txnCharge { get; set; }
        public string paidAmount { get; set; }
        public string Commission { get; set; }
        public string AID { get; set; }
        public string Agentd_id { get; set; }
    }

    public class ResponceBalanceInfo
    {
        public string RESP_CODE { get; set; }
        public string RESPONSE { get; set; }
        public string RESP_MSG { get; set; }
        public BalanceInfo DATA { get; set; }
    }

    public class GETRDHASHDATA
    {
        public string wadh { get; set; }
        public string pidOpt { get; set; }
        public string reqUrl { get; set; }
    }

    public class ResponceGetRd
    {
        public int RESP_CODE { get; set; }
        public string RESPONSE { get; set; }
        public string RESP_MSG { get; set; }
        public GETRDHASHDATA DATA { get; set; }
    }
    #endregion
}