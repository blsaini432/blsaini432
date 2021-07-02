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
public partial class Root_Distributor_DmrNew : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    EzulixDmrPayOut EDmrPayOut = new EzulixDmrPayOut();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["dtDistributor"]!=null)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataTable dtMember = (DataTable)Session["dtDistributor"];
                    dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='xpressdmr', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["isxpressdmr"]) == false)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                        }
                        else
                        {
                            ViewState["MemberId"] = null;
                            ViewState["MsrNo"] = null;
                            ViewState["dmtmobile"] = null;
                            ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                            ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                            ViewState["dmtmobile"] = dt.Rows[0]["Mobile"].ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                    }
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }
      
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtMember = (DataTable)Session["dtDistributor"];
        dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='xpressdmr', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["isxpressdmr"]) == true)
            {
                double NetAmount = TotupAmount(Convert.ToDouble(txt_Amount.Text), ViewState["MemberId"].ToString());
                if (NetAmount > Convert.ToDouble(txt_Amount.Text))
                {
                    int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), Convert.ToInt32(ViewState["MsrNo"]));
                    if (res == 1)
                    {

                        Random random = new Random();
                        int SixDigit = random.Next(1000, 9999);
                        Session["chdmtOTP"] = SixDigit.ToString();
                        string[] valueArray = new string[2];
                        valueArray[0] = txt_Amount.Text;
                        valueArray[1] = Session["chdmtOTP"].ToString();
                        SendWithVarpan(ViewState["dmtmobile"].ToString(), 1, valueArray);
                        mpe_dmrotp.Show();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('OTP Sent Successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insuffient Wallet Balance');", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Surcharge Not Define');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
        }
    }
    protected void btn_dmrotp_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable dtMember = (DataTable)Session["dtDistributor"];
            dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='xpressdmr', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["isxpressdmr"]) == true)
                {

                    if (txt_dmrotp.Text.Trim() == Session["chdmtOTP"].ToString())
                    {
                        string timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
                        timestamp = timestamp.Replace("-", "");
                        timestamp = timestamp.Replace(":", "");
                        timestamp = timestamp.Replace(".", "");
                        timestamp = timestamp.Replace(" ", "");
                        string orderId = timestamp;
                        string amount = txt_Amount.Text.Trim();
                        string beneficiaryAccount = txt_Acno.Text.Trim();
                        string beneficiaryIFSC = txt_Ifsccode.Text.Trim();
                        string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                        string Result = string.Empty;
                        double NetAmount = TotupAmount(Convert.ToDouble(amount), ViewState["MemberId"].ToString());
                        if (NetAmount > Convert.ToDouble(amount))
                        {
                            int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), Convert.ToInt32(ViewState["MsrNo"]));
                            if (res == 1)
                            {
                                int tra = Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal("-" + NetAmount.ToString().Trim()), "Dr", "DMR Topup Txn:- " + orderId + "");
                                if (tra > 0)
                                {
                                    Result = EDmrPayOut.WithDrawal_Ammount(orderId, amount, beneficiaryAccount, beneficiaryIFSC, date);
                                    DataSet ds = Deserialize(Result);
                                    List<ParmList> _lstparm = new List<ParmList>();
                                    _lstparm.Add(new ParmList() { name = "@MemberId", value = ViewState["MemberId"].ToString() });
                                    _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(ViewState["MsrNo"]) });
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
                                        Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + orderId + "");
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('OTP is not valid for this transaction');", true);
                        mpe_dmrotp.Show();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
            }
        }
        catch (Exception EX)
        {
            ResetCtrl();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
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
            DataTable dtMemberMaster = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='chk', @msrno=" + Convert.ToInt32(ViewState["MsrNo"]) + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='sur',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
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
      "Dear Customer, you need an OTP  to access Payout Transaction for Rs.@v0@ and OTP is @v1@. Never Share it with anyone.Bank Never calls to verify it."//1

    };
}