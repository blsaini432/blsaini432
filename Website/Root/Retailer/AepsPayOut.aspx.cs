using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Root_Retailer_AepsPayOut : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    EzulixAepsPayOut EPayout = new EzulixAepsPayOut();
    cls_myMember Clsm = new cls_myMember();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='payout', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["isaepspayout"]) == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('AEPS Payout is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                    }
                    else
                    {
                        if (Convert.ToString(dt.Rows[0]["bankname"]) != "" && Convert.ToString(dt.Rows[0]["bankifsc"]) != "" && Convert.ToString(dt.Rows[0]["bankac"]) != "")
                        {
                            txt_Acno.Text = dtMember.Rows[0]["bankac"].ToString();
                            txt_Ifsccode.Text = dtMember.Rows[0]["bankifsc"].ToString();
                            ViewState["MemberId"] = null;
                            ViewState["MsrNo"] = null;
                            ViewState["aepsmobile"] = null;
                            ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                            ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                            ViewState["aepsmobile"] = dt.Rows[0]["Mobile"].ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Bank Details Not Updated, Contact to your admin');window.location ='DashBoard.aspx';", true);
                        }

                    }
                }
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
        }
    }
    #region Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["dtRetailer"] != null)
        {
            DataTable dst = new DataTable();
            DataTable dtMember = (DataTable)Session["dtRetailer"];
            dst = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='payout', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
            if (dst.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dst.Rows[0]["isaepspayout"]) == true)
                {
                    string bankac = dtMember.Rows[0]["bankac"].ToString();
                    string bankifsc = dtMember.Rows[0]["bankifsc"].ToString();
                    if (Convert.ToString(txt_Acno.Text.Trim()) == bankac && Convert.ToString(txt_Ifsccode.Text.Trim()) == bankifsc)
                    {
                        DataTable dt = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(dtMember.Rows[0]["MsrNo"]) + ",@action='sum'");
                        if (dt.Rows.Count > 0)
                        {
                            string usbal = dt.Rows[0]["usebal"].ToString();
                            double NetAmount = TotupAmount(Convert.ToDouble(txt_Amount.Text.Trim()), dtMember.Rows[0]["MemberId"].ToString());
                            if (NetAmount > Convert.ToDouble(txt_Amount.Text.Trim()))
                            {
                                if (Convert.ToDecimal(usbal) >= Convert.ToDecimal(NetAmount))
                                {
                                    Random random = new Random();
                                    int SixDigit = random.Next(1000, 9999);
                                    Session["chdmtOTP"] = SixDigit.ToString();
                                    string[] valueArray = new string[2];
                                    valueArray[0] = txt_Amount.Text;
                                    valueArray[1] = Session["chdmtOTP"].ToString();
                                    SendWithVarpan(ViewState["aepsmobile"].ToString(), 1, valueArray);
                                    mpe_dmrotp.Show();
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('OTP Sent Successfully');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Surcharge not define.');", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Bank Details doesnot match Try later!');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('AEPS Payout is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('AEPS Payout is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Session time Out Please Login again!');window.location ='../../UserLogin.aspx';", true);
        }

    }
    protected void btn_dmrotp_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dst = new DataTable();
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                dst = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='payout', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
                if (dst.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dst.Rows[0]["isaepspayout"]) == true)
                    {
                        if (txt_dmrotp.Text.Trim() == Session["chdmtOTP"].ToString())
                        {
                            DataTable dt = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(dtMember.Rows[0]["MsrNo"]) + ",@action='sum'");
                            if (dt.Rows.Count > 0)
                            {
                                string usbal = dt.Rows[0]["usebal"].ToString();
                                if (Convert.ToDecimal(usbal) >= Convert.ToDecimal(txt_Amount.Text.Trim()))
                                {
                                    string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                                    timestamp = timestamp.Replace("-", "");
                                    timestamp = timestamp.Replace(":", "");
                                    timestamp = timestamp.Replace(".", "");
                                    timestamp = timestamp.Replace(" ", "");
                                    string orderId = "PA" + timestamp;
                                    string amount = txt_Amount.Text.Trim();
                                    string beneficiaryAccount = txt_Acno.Text.Trim();
                                    string beneficiaryIFSC = txt_Ifsccode.Text.Trim();
                                    string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                                    string Result = string.Empty;
                                    double NetAmount = TotupAmount(Convert.ToDouble(amount), dtMember.Rows[0]["MemberId"].ToString());
                                    if (NetAmount > Convert.ToDouble(amount))
                                    {

                                        int tra = Clsm.AEPSWallet_MakeTransaction_Ezulix(dtMember.Rows[0]["MemberId"].ToString(), Convert.ToDecimal("-" + NetAmount.ToString().Trim()), "Dr", "Xpress AEPS Payoyut Topup Txn:-" + orderId + "");
                                        if (tra > 0)
                                        {
                                            Result = EPayout.WithDrawal_Ammount(orderId, amount, beneficiaryAccount, beneficiaryIFSC, date);
                                            DataSet ds = Deserialize(Result);
                                            List<ParmList> _lstparm = new List<ParmList>();
                                            _lstparm.Add(new ParmList() { name = "@MemberId", value = dtMember.Rows[0]["MemberId"].ToString() });
                                            _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]) });
                                            _lstparm.Add(new ParmList() { name = "@AgentOrderId", value = orderId });
                                            _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(amount) });
                                            _lstparm.Add(new ParmList() { name = "@beneficiaryAccount", value = beneficiaryAccount });
                                            _lstparm.Add(new ParmList() { name = "@beneficiaryIFSC", value = beneficiaryIFSC });
                                            _lstparm.Add(new ParmList() { name = "@status", value = ds.Tables[0].Rows[0]["status"].ToString() });
                                            _lstparm.Add(new ParmList() { name = "@statusCode", value = ds.Tables[0].Rows[0]["statusCode"].ToString() });
                                            _lstparm.Add(new ParmList() { name = "@statusMessage", value = ds.Tables[0].Rows[0]["statusMessage"].ToString() });
                                            _lstparm.Add(new ParmList() { name = "@NetAmount", value = Convert.ToDecimal(NetAmount) });
                                            _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                                            Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer", _lstparm);
                                            if (ds.Tables[0].Rows[0]["status"].ToString() == "FAILURE")
                                            {
                                                Clsm.AEPSWallet_MakeTransaction_Ezulix(dtMember.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "Fail Xpress AEPS Payoyut Topup Txn:-" + orderId + "");
                                            }
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["statusMessage"].ToString() + "');window.location ='DmrNewReport.aspx';", true);
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
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');window.location ='DashBoard.aspx';", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('OTP is not valid for this transaction');", true);
                            mpe_dmrotp.Show();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('AEPS Payout is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('AEPS Payout is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Session time Out Please Login again!');window.location ='../../UserLogin.aspx';", true);
            }
        }
        catch (Exception EX)
        {
            ResetCtrl();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
        }

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
    }

    protected void btn_Closedmr_Click(object sender, EventArgs e)
    {
        mpe_dmrotp.Hide();
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
    private void ResetCtrl()
    {
        try
        {
            txt_Acno.Text = string.Empty;
            txt_Ifsccode.Text = string.Empty;
            txt_Amount.Text = string.Empty;
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
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
            DataTable dtMember = (DataTable)Session["dtRetailer"];
            DataTable dtMemberMaster = Cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='chk', @msrno=" + Convert.ToInt32(dtMember.Rows[0]["MsrNo"]) + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixPayOutDmr @action='aepspayoutsur',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
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
            string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=198296AFda5tMRgn5a854e41&route=4&sender=EZXDMT&mobiles=" + Mobile + "& message=" + smsMessage + "";
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
        "Dear Customer, you need an OTP  to access Payout Transaction for Rs.@v0@ and OTP is @v1@. Never Share it with anyone.Bank Never calls to verify it."//1
    };

    //protected void btn_Register_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["dtRetailer"] != null)
    //        {
    //            DataTable dtMember = (DataTable)Session["dtRetailer"];
    //            string Pin = Cls.select_data_scalar_string(@"SELECT P_Pin FROM tbl_aeps_reg WHERE MsrNo=" + dtMember.Rows[0]["MsrNo"] + "");
    //            string Result = "{\"statuscode\":\"TXN\",\"status\":\"Transaction Successful\",\"data\":{\"remitter\":{\"is_verified\":1,\"id\":\"191783\"}";
    //            //string Result = EDmr.Remitter_Registration(dtMember.Rows[0]["Mobile"].ToString(), dtMember.Rows[0]["FirstName"].ToString(), dtMember.Rows[0]["LastName"].ToString(), Pin);
    //            DataSet ds = Deserialize(Result);
    //            if (Convert.ToBoolean(ds.Tables["remitter"].Rows[0]["is_verified"]))
    //            {
    //                Result = string.Empty;
    //                string TxnId = Clsm.Cyrus_GetTransactionID_New();
    //                int tra = Clsm.Wallet_MakeTransaction(dtMember.Rows[0]["MemberId"].ToString(), Convert.ToDecimal("-" + 3), "Dr", "AepsPayOut AC Verify Txn:- " + TxnId + "");
    //                if (tra > 0)
    //                {
    //                    Result = EDmr.Beneficiary_Account_Verification(dtMember.Rows[0]["Mobile"].ToString(), Txt_AccountNo.Text.Trim(), Txt_Ifsc.Text.Trim(), TxnId);
    //                    DataSet dsRsult = Deserialize(Result);
    //                    if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
    //                    {

    //                    }
    //                    else
    //                    {
    //                        Clsm.Wallet_MakeTransaction(dtMember.Rows[0]["Mobile"].ToString(), Convert.ToDecimal(3), "Cr", "Fail AepsPayOut AC Verify Txn:- " + TxnId + "");
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "window.location ='DashBoard.aspx';", true);
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    #endregion
}