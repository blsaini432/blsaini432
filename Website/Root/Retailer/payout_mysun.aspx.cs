using BLL;
using Newtonsoft.Json;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
public partial class Root_Retailer_payout_mysun : System.Web.UI.Page
{
    #region Access_Class
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    payout_mysun PayOuts = new payout_mysun();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtRetailer"] != null)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                var HostIp = Dns.GetHostAddresses("www.mysunshinenet.com").FirstOrDefault();
                int msrno = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]);
                dt = Cls.select_data_dt(@"exec Set_EzulixDmr @action='instpayout', @msrno=" + msrno + "");
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["instantpayout"]) == true)
                    {
                        ViewState["MemberId"] = null;
                        ViewState["MsrNo"] = null;
                        Session["IpAddress"] = HostIp;
                        ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                        Session["TransactionPassword"] = dtMember.Rows[0]["TransactionPassword"];
                        Session["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                        ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                        ViewState["dmtmobile"] = dtMember.Rows[0]["Mobile"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('payout service Not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('payout service Not active');window.location ='DashBoard.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }
    }
    
    protected void btn_dmrotp_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtMember = (DataTable)Session["dtRetailer"];
        if (ViewState["MsrNo"] != null && ViewState["MemberId"] != null)
        {
            Random random = new Random();
            int SixDigit = random.Next(1000, 9999);
            Session["chdmtOTP"] = SixDigit.ToString();
            string[] valueArray = new string[2];
            valueArray[0] = txt_Amount.Text;
            valueArray[1] = Session["chdmtOTP"].ToString();
            SendWithVarpan(ViewState["dmtmobile"].ToString(), 1, valueArray);
            mpe_dmrotp.Show();           
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('AEPS Wallet balance is insufficent for this transcation !');", true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dt = new DataTable();
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                string memberid = dtMember.Rows[0]["MemberId"].ToString();
                dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instpayout', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
                if (dt.Rows.Count > 0)
                {
                    if (txt_dmrotp.Text.Trim() == Session["chdmtOTP"].ToString())
                    {
                        if (Convert.ToDecimal(txt_Amount.Text.Trim()) > 0)
                        {
                            int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(txt_Amount.Text.Trim()), Convert.ToInt32(dtMember.Rows[0]["MsrNo"]));
                            if (res == 1)
                            {
                                string ipaddress = Session["IpAddress"].ToString();
                                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(2020, 1, 1))).TotalSeconds;
                                int timestamp = unixTimestamp;
                                string sp_key = Transactionlist.SelectedValue;
                                string amount = txt_Amount.Text.Trim();
                                string credit_amount = amount + ".00";
                                string bene_name = txt_name.Text.Trim();
                                string latitude = "26.912434";
                                string longitude = "75.787270";
                                string endpoint_ip = Request.UserHostAddress;
                                string remarks = "Payout";
                                int otp_auth = 0;
                                Random random = new Random();
                                int SixDigit = random.Next(10000, 99999);
                                cls_myMember clsm = new cls_myMember();
                                string external_ref = clsm.Cyrus_GetTransactionID_New();
                                string debit_account = "107305006606";
                                string credit_account = txt_Acno.Text.Trim();
                                string ifs_code = txt_Ifsccode.Text.Trim();
                                string Result = string.Empty;
                               double NetAmount = TotupAmount(Convert.ToDouble(amount), dtMember.Rows[0]["MemberId"].ToString()); ;
                                if (NetAmount > Convert.ToDouble(amount))
                                {
                                    int tra = Clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal("-" + NetAmount), "Dr", "Inst Payout Topup Txn:- " + external_ref + "");
                                    if (tra > 0)
                                    {
                                        //insertion start
                                        List<ParmList> _lstparm = new List<ParmList>();
                                        _lstparm.Add(new ParmList() { name = "@MemberId", value = ViewState["MemberId"].ToString() });
                                        _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(ViewState["MsrNo"]) });
                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(amount) });
                                        _lstparm.Add(new ParmList() { name = "@beneficiaryAccount", value = credit_account });
                                        _lstparm.Add(new ParmList() { name = "@beneficiaryIFSC", value = ifs_code });
                                        _lstparm.Add(new ParmList() { name = "@txnstatus", value = "Pending" });
                                        _lstparm.Add(new ParmList() { name = "@transaction_types_id", value = ipaddress });
                                        _lstparm.Add(new ParmList() { name = "@transaction_status_id", value = "" });
                                        _lstparm.Add(new ParmList() { name = "@open_transaction_ref_id", value = sp_key });
                                        _lstparm.Add(new ParmList() { name = "@purpose", value = remarks });
                                        _lstparm.Add(new ParmList() { name = "@recepient_name", value = bene_name });
                                        _lstparm.Add(new ParmList() { name = "@merchant_ref_id", value = external_ref });
                                        _lstparm.Add(new ParmList() { name = "@debit_account_number", value = debit_account });
                                        _lstparm.Add(new ParmList() { name = "@NetAmount", value = Convert.ToDecimal(NetAmount) });
                                        _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                                        Cls.select_data_dtNew("SET_Ezulix_PayOut_MoneyTransfer_new", _lstparm);

                                        if (ipaddress =="147.139.34.181")
                                        {
                                            Result = PayOuts.InitiatePayouts(sp_key, debit_account, external_ref, credit_account, ifs_code, amount, latitude, longitude, endpoint_ip, bene_name, remarks, otp_auth);
                                        }
                                        else if(ipaddress =="147.139.5.70")
                                        {
                                            Result = PayOuts.InitiatePayoutsip(sp_key, debit_account, external_ref, credit_account, ifs_code, amount, latitude, longitude, endpoint_ip, bene_name, remarks, otp_auth);
                                        }
                                        else
                                        {
                                            Clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Inst Payout Fail Txn:-" + external_ref + "");
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Server Side! Please Try Again');window.location ='payout_mysun.aspx';", true);
                                        }
                                        //insertion end
                                        if (Result != string.Empty)
                                        {
                                            DataSet ds = Deserialize(Result);
                                            if (ds.Tables.Contains("root") && ds.Tables["root"].Columns.Contains("statuscode"))
                                            {
                                                if (ds.Tables["root"].Rows[0]["statuscode"].ToString() == "TXN")
                                                {
                                                    List<ParmList> _lstparms = new List<ParmList>();
                                                    _lstparms.Add(new ParmList() { name = "@txnstatus", value = ds.Tables["root"].Rows[0]["status"].ToString() });
                                                    _lstparms.Add(new ParmList() { name = "@status", value = ds.Tables["root"].Rows[0]["statuscode"].ToString() });
                                                  //  _lstparms.Add(new ParmList() { name = "@transaction_status_id", value = ds.Tables["root"].Rows[0]["statuscode"].ToString() });
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
                                                    ResetCtrl();
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Success ');window.location ='payout_mysun.aspx';", true);
                                                }
                                                else if (ds.Tables["root"].Rows[0]["statuscode"].ToString() == "ERR")
                                                {
                                                    string error = ds.Tables["root"].Rows[0]["status"].ToString();
                                                    List<ParmList> _lstparmss = new List<ParmList>();
                                                    _lstparmss.Add(new ParmList() { name = "@merchant_ref_id", value = external_ref });
                                                    _lstparmss.Add(new ParmList() { name = "@txnstatus", value = ds.Tables["root"].Rows[0]["status"].ToString() });
                                                    _lstparmss.Add(new ParmList() { name = "@status", value = ds.Tables["root"].Rows[0]["statuscode"].ToString() });
                                                    _lstparmss.Add(new ParmList() { name = "@Action", value = "err" });
                                                    Cls.select_data_dtNew("SET_Ezulix_PayOut_MoneyTransfer_new", _lstparmss);
                                                    Clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(NetAmount), "Cr", "Inst Payout Fail Txn:-" + external_ref + "");
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Fill the write details than again try');window.location ='payout_mysun.aspx';", true);
                                                }
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Please Try again ');window.location ='payout_mysun.aspx';", true);
                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Time Out!');window.location ='DashBoard.aspx';", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');window.location ='DashBoard.aspx';", true);
                                    }


                                }
                                else
                                {
                                    ResetCtrl();
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Surcharge not define.');window.location ='DashBoard.aspx';", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');window.location ='DashBoard.aspx';", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter the Aount greater than zero');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter valid OTP ');", true);
                        mpe_dmrotp.Show();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('payout service Not active');window.location ='DashBoard.aspx';", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Time Out ! Please logout and login again');window.location ='DashBoard.aspx';", true);
            }


        }
        catch (Exception EX)
        {
            ResetCtrl();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('ERRER');", true);
        }
    }

    protected void btn_Closedmr_Click(object sender, EventArgs e)
    {
        mpe_dmrotp.Hide();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetCtrl();
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

    protected void UpdateTime(object sender, EventArgs e)
    {
        string time = DateTime.Now.ToString("hh:mm:ss tt");
        string script = "window.onload = function() { UpdateTime('" + time + "'); };";
        ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);
    }

    private void ResetCtrl()
    {
        try
        {
            txt_Acno.Text = string.Empty;
            txt_Ifsccode.Text = string.Empty;
            txt_Amount.Text = string.Empty;
            txt_Ifsccode.Text = string.Empty;
            Session["chdmtOTP"] = string.Empty;
            txt_name.Text = string.Empty;
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
        }
    }
    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objMemberMaster.ManageMemberMaster("Get", id);

        if (dt.Rows.Count > 0)
        {
            //txt_name.Text = Convert.ToString(dt.Rows[0]["FirstName"]);
            //   txt_email.Text = Convert.ToString(dt.Rows[0]["Email"]);
            // txt_mobile.Text = Convert.ToString(dt.Rows[0]["mobile"]);
            txt_Ifsccode.Text = Convert.ToString(dt.Rows[0]["bankifsc"]);
            txt_Acno.Text = Convert.ToString(dt.Rows[0]["bankac"]);
        }
    }
    public double TotupAmount(double amount, string memberid)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            DataTable dtMemberMaster = Cls.select_data_dt(@"EXEC Set_EzulixPayOut_instnew @action='chk', @msrno=" + Convert.ToInt32(ViewState["MsrNo"]) + "");
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

    public static string get_SMSBaseURL(string Mobile, string smsMessage, int ApiID, string Route)
    {
        cls_connection cls = new cls_connection();
        DataTable dtAPI = new DataTable();

        dtAPI = cls.select_data_dt("Proc_Recharge_SMSApi 'getDataById'," + ApiID + "");
        string str = "";
        str = dtAPI.Rows[0]["URL"].ToString() + dtAPI.Rows[0]["prm1"].ToString() + "=" + dtAPI.Rows[0]["prm1val"].ToString() + "&";
        if (dtAPI.Rows[0]["prm2"].ToString() != "" && dtAPI.Rows[0]["prm2val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm2"].ToString() + "=" + dtAPI.Rows[0]["prm2val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm3"].ToString() != "" && dtAPI.Rows[0]["prm3val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm3"].ToString() + "=" + dtAPI.Rows[0]["prm3val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm4"].ToString() != "" && dtAPI.Rows[0]["prm4val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm4"].ToString() + "=" + dtAPI.Rows[0]["prm4val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm5"].ToString() != "" && dtAPI.Rows[0]["prm5val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm5"].ToString() + "=" + dtAPI.Rows[0]["prm5val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm6"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm6"].ToString() + "=" + Mobile + "&";
        }
        if (dtAPI.Rows[0]["prm7"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm7"].ToString() + "=" + smsMessage + "";
        }

        //str = str + "route=" + Route;

        return str;
    }

    public static void SendWithVar(string Mobile, int Template, string[] ValueArray, int MsrNo)
    {
        try
        {
            if (Mobile.Length == 10)
            {
                Mobile = "91" + Mobile;
            }
            cls_connection cls = new cls_connection();
            int ApiID = cls.select_data_scalar_int("select SMSAPI from tblMLM_MemberMaster where MsrNo=" + MsrNo);
            cls.insert_data("exec ProcSMS_updateCount '" + Mobile + "'");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string smsMessage = GetString(Template, ValueArray);
            string baseurl = get_SMSBaseURL(Mobile, smsMessage, ApiID, "T");
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




}
