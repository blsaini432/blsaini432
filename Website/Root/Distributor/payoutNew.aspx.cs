using BLL;
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

public partial class Root_Distributor_payoutNew : System.Web.UI.Page
{
    #region Access_Class
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    PayOut PayOuts = new PayOut();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtDistributor"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataTable dtMember = (DataTable)Session["dtDistributor"];
                    dtMemberMaster = objMemberMaster.ManageMemberMaster("Get", Convert.ToInt32(Session["DistributorMsrNo"]));
                    dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='payout', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["isaepspayout"]) == true)
                        {
                            //cls.fill_MemberType(ddlMemberType, dtMemberMaster.Rows[0]["membertype"].ToString());
                            ViewState["MemberId"] = null;
                            ViewState["MsrNo"] = null;
                            ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                            Session["TransactionPassword"] = dtMember.Rows[0]["TransactionPassword"];
                            Session["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                            ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                            ViewState["dmtmobile"] = dtMember.Rows[0]["Mobile"].ToString();

                            FillData(Convert.ToInt32(Session["MsrNo"]));
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
        DataTable dtMember = (DataTable)Session["dtDistributor"];
        dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='payout', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
        if (dt.Rows.Count > 0)
        {
            DataTable dst = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(dtMember.Rows[0]["MsrNo"]) + ",@action='sum'");
            if (dst.Rows.Count > 0)
            {

                string usbal = dst.Rows[0]["usebal"].ToString();
                double NetAmount = TotupAmount(Convert.ToDouble(txt_Amount.Text), ViewState["MemberId"].ToString());
                if (NetAmount > Convert.ToDouble(txt_Amount.Text))
                {
                    if (Convert.ToDecimal(usbal) >= Convert.ToDecimal(NetAmount))
                    {

                        //  Random random = new Random();
                        //  int SixDigit = random.Next(1000, 9999);
                        //  Session["chdmtOTP"] = SixDigit.ToString();
                        //  string[] valueArray = new string[2];
                        //  valueArray[0] = txt_Amount.Text;
                        //  valueArray[1] = Session["chdmtOTP"].ToString();
                        // SMS.SendWithVar(ViewState["dmtmobile"].ToString(), 1, valueArray, 0);
                        // SendWithVarpan(ViewState["dmtmobile"].ToString(), 1, valueArray);
                        mpe_dmrotp.Show();
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('O');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('AEPS Wallet balance is insufficent for this transcation !');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Surcharge Not Define');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insuffient Wallet Balance');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Payout service Not Active');", true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {


            if (Session["dtDistributor"] != null)
            {
                DataTable dt = new DataTable();
                DataTable dtMember = (DataTable)Session["dtDistributor"];
                dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='payout', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
                if (dt.Rows.Count > 0)
                {
                    if (txt_dmrotp.Text.Trim() == Session["TransactionPassword"].ToString())
                    {
                        if (Transactionlist.SelectedIndex > 0)
                        {
                            if (Convert.ToDecimal(txt_Amount.Text.Trim()) > 0)
                            {
                                DataTable dst = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(dtMember.Rows[0]["MsrNo"]) + ",@action='sum'");
                                if (dst.Rows.Count > 0)
                                {
                                    string usbal = dst.Rows[0]["usebal"].ToString();
                                    if (Convert.ToDecimal(usbal) >= Convert.ToDecimal(txt_Amount.Text.Trim()))
                                    {
                                        Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(2020, 1, 1))).TotalSeconds;
                                        int timestamp = unixTimestamp;
                                        string selectedText = Transactionlist.SelectedValue;
                                        string amount = txt_Amount.Text.Trim();
                                        string finalamount = amount + ".00";
                                        string name = txt_name.Text.Trim();
                                        string email = txt_email.Text.Trim();
                                        string mobile = txt_mobile.Text.Trim();
                                        Random random = new Random();
                                        int SixDigit = random.Next(10000, 99999);
                                        cls_myMember clsm = new cls_myMember();
                                        string merchant = clsm.Cyrus_GetTransactionID_New();
                                        string dummyaccount = "031905008166";
                                        string purpose = "testing";
                                        string beneficiaryAccount = txt_Acno.Text.Trim();
                                        string beneficiaryIFSC = txt_Ifsccode.Text.Trim();
                                        string date = System.DateTime.Now.ToString("yyyy-MM-dd");
                                        string Result = string.Empty;
                                        double NetAmount = TotupAmount(Convert.ToDouble(amount), dtMember.Rows[0]["MemberId"].ToString()); ;
                                        if (NetAmount > Convert.ToDouble(amount))
                                        {
                                            int tra = Clsm.AEPSWallet_MakeTransaction_Ezulix(dtMember.Rows[0]["MemberId"].ToString(), Convert.ToDecimal("-" + NetAmount.ToString().Trim()), "Dr", " Payout Topup Txn:-" + merchant + "");
                                            if (tra > 0)
                                            {
                                                //insertion start
                                                List<ParmList> _lstparm = new List<ParmList>();
                                                _lstparm.Add(new ParmList() { name = "@MemberId", value = ViewState["MemberId"].ToString() });
                                                _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(ViewState["MsrNo"]) });
                                                _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(amount) });
                                                _lstparm.Add(new ParmList() { name = "@beneficiaryAccount", value = beneficiaryAccount });
                                                _lstparm.Add(new ParmList() { name = "@beneficiaryIFSC", value = beneficiaryIFSC });
                                                _lstparm.Add(new ParmList() { name = "@txnstatus", value = "Pending" });
                                                _lstparm.Add(new ParmList() { name = "@transaction_types_id", value = "" });
                                                _lstparm.Add(new ParmList() { name = "@transaction_status_id", value = "" });
                                                _lstparm.Add(new ParmList() { name = "@open_transaction_ref_id", value = "" });
                                                _lstparm.Add(new ParmList() { name = "@purpose", value = "" });
                                                _lstparm.Add(new ParmList() { name = "@recepient_name", value = "" });
                                                _lstparm.Add(new ParmList() { name = "@email_id", value = "" });
                                                _lstparm.Add(new ParmList() { name = "@mobile_number", value = "" });
                                                _lstparm.Add(new ParmList() { name = "@merchant_ref_id", value = merchant });
                                                _lstparm.Add(new ParmList() { name = "@debit_account_number", value = "" });
                                                _lstparm.Add(new ParmList() { name = "@NetAmount", value = Convert.ToDecimal(NetAmount) });
                                                _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                                                Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer_new", _lstparm);

                                                //insertion end
                                                Result = PayOuts.InitiatePayouts(selectedText, finalamount, beneficiaryAccount, beneficiaryIFSC, mobile, merchant, email, name, dummyaccount, purpose, timestamp);
                                                if (Result != string.Empty)
                                                {

                                                    DataSet ds = Deserialize(Result);
                                                    if (ds.Tables[0].Rows[0]["status"].ToString() == "200")
                                                    {
                                                        string txnid = ds.Tables[1].Rows[0]["transaction_status_id"].ToString();
                                                        string mode = ds.Tables[1].Rows[0]["transaction_types_id"].ToString();
                                                        string code = ds.Tables[0].Rows[0]["status"].ToString();
                                                        List<ParmList> _lstparms = new List<ParmList>();
                                                        _lstparms.Add(new ParmList() { name = "@status", value = ds.Tables[0].Rows[0]["status"].ToString() });
                                                        _lstparms.Add(new ParmList() { name = "@transaction_status_id", value = ds.Tables[1].Rows[0]["transaction_status_id"].ToString() });
                                                        _lstparms.Add(new ParmList() { name = "@open_transaction_ref_id", value = ds.Tables[1].Rows[0]["open_transaction_ref_id"].ToString() });
                                                        _lstparms.Add(new ParmList() { name = "@purpose", value = ds.Tables[1].Rows[0]["purpose"].ToString() });
                                                        _lstparms.Add(new ParmList() { name = "@recepient_name", value = ds.Tables[1].Rows[0]["recepient_name"].ToString() });
                                                        _lstparms.Add(new ParmList() { name = "@email_id", value = ds.Tables[1].Rows[0]["email_id"].ToString() });
                                                        _lstparms.Add(new ParmList() { name = "@mobile_number", value = ds.Tables[1].Rows[0]["mobile_number"].ToString() });
                                                        _lstparms.Add(new ParmList() { name = "@merchant_ref_id", value = merchant });
                                                        _lstparms.Add(new ParmList() { name = "@debit_account_number", value = ds.Tables[1].Rows[0]["debit_account_number"].ToString() });

                                                        if (txnid == "15" && code == "200" && mode == "2")
                                                        {
                                                            _lstparms.Add(new ParmList() { name = "@bank_response", value = "Success" });
                                                            _lstparms.Add(new ParmList() { name = "@transaction_types_id", value = "NEFT" });
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Success" });
                                                        }
                                                        else if (txnid == "17" && code == "200")
                                                        {
                                                            Clsm.AEPSWallet_MakeTransaction_Ezulix(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "Payout Fail Txn:- " + merchant + "");
                                                            _lstparms.Add(new ParmList() { name = "@bank_response", value = "Failed" });
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Failed" });
                                                        }
                                                        else if ((txnid == "16" || txnid == "20") && code == "200")
                                                        {
                                                            _lstparms.Add(new ParmList() { name = "@bank_response", value = "Pending/processing" });
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Pending" });
                                                        }
                                                        else if (txnid == "21" && code == "200")
                                                        {
                                                            _lstparms.Add(new ParmList() { name = "@bank_response", value = "If this is a NEFT transaction, please wait for the NEFT cycle to process or if you have a maker-checker configured for your ICICI current account, please make sure that this transaction is approved by all the checkers. This transaction can change to Success or Failure depending on the beneficiary bank's response" });
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Pending" });
                                                        }
                                                        else if (txnid == "42" && code == "200")
                                                        {
                                                            Clsm.AEPSWallet_MakeTransaction_Ezulix(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "Payout Fail Txn:- " + merchant + "");
                                                            _lstparms.Add(new ParmList() { name = "@bank_response", value = "You donot have sufficient balance to perform this transaction." });                                                        
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Failed" });
                                                        }
                                                        else if (txnid == "40" && code == "200")
                                                        {
                                                            Clsm.AEPSWallet_MakeTransaction_Ezulix(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "Payout Fail Txn:- " + merchant + "");
                                                            _lstparms.Add(new ParmList() { name = "@bank_response", value = "Your transaction has failed due to invalid account beneficiary account number." });                                                        
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Failed" });
                                                        }
                                                        else if (txnid == "48" && code == "200")
                                                        {
                                                            Clsm.AEPSWallet_MakeTransaction_Ezulix(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "Payout Fail Txn:- " + merchant + "");
                                                            _lstparms.Add(new ParmList() { name = "@bank_response", value = "The transaction has been submitted to beneficiary bank. Please check with beneficiary before reinitiating the transaction." });                                               
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Pending" });
                                                        }
                                                        else if (txnid == "15" && code == "200" && mode == "4")
                                                        {
                                                            _lstparms.Add(new ParmList() { name = "@bank_response", value = "Success" });
                                                            _lstparms.Add(new ParmList() { name = "@transaction_types_id", value = "IMPS" });
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Success" });
                                                        }
                                                        else if (txnid == "1" && code == "200")
                                                        {
                                                            Clsm.AEPSWallet_MakeTransaction_Ezulix(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "Payout Fail Txn:- " + merchant + "");
                                                            _lstparms.Add(new ParmList() { name = "@bank_response", value = "Failed" });
                                                            _lstparms.Add(new ParmList() { name = "@transaction_types_id", value = mode });
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Failed" });
                                                        }
                                                        else
                                                        {
                                                            _lstparms.Add(new ParmList() { name = "@txnstatus", value = "Failed" });
                                                        }
                                                        _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                                                        Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer_new", _lstparms);

                                                        if (ds.Tables[1].Rows[0]["transaction_status_id"].ToString() == "15")
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction successfully ');window.location ='payoutnew.aspx';", true);
                                                        }
                                                        else if (ds.Tables[1].Rows[0]["transaction_status_id"].ToString() == "16")
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Pending  ');window.location ='payoutnew.aspx';", true);
                                                        }
                                                        else if (ds.Tables[1].Rows[0]["transaction_status_id"].ToString() == "17")
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Transaction Failed ');window.location ='payoutnew.aspx';", true);
                                                        }
                                                        else if (ds.Tables[1].Rows[0]["transaction_status_id"].ToString() == "20")
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Processing  ');window.location ='payoutnew.aspx';", true);
                                                        }
                                                        else if (ds.Tables[1].Rows[0]["transaction_status_id"].ToString() == "42")
                                                        {

                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Failed');window.location ='payoutnew.aspx';", true);
                                                        }
                                                        else if (ds.Tables[1].Rows[0]["transaction_status_id"].ToString() == "48")
                                                        {

                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Pending');window.location ='payoutnew.aspx';", true);
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' transaction Status pending ');window.location ='payoutnew.aspx';", true);
                                                        }
                                                    }
                                                    else if (ds.Tables[0].Rows[0]["status"].ToString() == "422")
                                                    {
                                                        List<ParmList> _lstparmss = new List<ParmList>();
                                                        _lstparmss.Add(new ParmList() { name = "@status", value = ds.Tables[1].Rows[0]["ifsc_code"].ToString() });
                                                        _lstparmss.Add(new ParmList() { name = "@merchant_ref_id", value = merchant });
                                                        _lstparmss.Add(new ParmList() { name = "@txnstatus", value = "Failed" });
                                                        _lstparmss.Add(new ParmList() { name = "@Action", value = "U" });
                                                        Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer_new", _lstparmss);
                                                        Clsm.AEPSWallet_MakeTransaction_Ezulix(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "Payout Fail Txn:- " + merchant + "");
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Unprocessable Entity ');window.location ='payoutnew.aspx';", true);
                                                    }
                                                    else if (ds.Tables[0].Rows[0]["status"].ToString() == "502")
                                                    {
                                                        List<ParmList> _lstparmss = new List<ParmList>();
                                                        _lstparmss.Add(new ParmList() { name = "@status", value = ds.Tables[0].Rows[0]["message"].ToString() });
                                                        _lstparmss.Add(new ParmList() { name = "@merchant_ref_id", value = merchant });
                                                        _lstparmss.Add(new ParmList() { name = "@txnstatus", value = "Failed" });
                                                        _lstparmss.Add(new ParmList() { name = "@Action", value = "U" });
                                                        Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer_new", _lstparmss);
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Bad Gateway ');window.location ='payoutnew.aspx';", true);
                                                    }
                                                    else if (ds.Tables[0].Rows[0]["status"].ToString() == "500")
                                                    {
                                                        List<ParmList> _lstparmss = new List<ParmList>();
                                                        _lstparmss.Add(new ParmList() { name = "@status", value = ds.Tables[0].Rows[0]["message"].ToString() });
                                                        _lstparmss.Add(new ParmList() { name = "@merchant_ref_id", value = merchant });
                                                        _lstparmss.Add(new ParmList() { name = "@txnstatus", value = "Failed" });
                                                        _lstparmss.Add(new ParmList() { name = "@Action", value = "U" });
                                                        Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer_new", _lstparmss);
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' Internal server error ');window.location ='payoutnew.aspx';", true);
                                                    }
                                                    else
                                                    {
                                                        List<ParmList> _lstparmss = new List<ParmList>();
                                                        _lstparmss.Add(new ParmList() { name = "@status", value = ds.Tables[0].Rows[0]["message"].ToString() });
                                                        _lstparmss.Add(new ParmList() { name = "@merchant_ref_id", value = merchant });
                                                        _lstparmss.Add(new ParmList() { name = "@txnstatus", value = "Failed" });
                                                        _lstparmss.Add(new ParmList() { name = "@Action", value = "U" });
                                                        Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer_new", _lstparmss);
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' some error found server ');window.location ='payoutnew.aspx';", true);
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Transaction Type');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter valid PIN ');", true);
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
            txt_email.Text = string.Empty;
            //txt_mer.Text = string.Empty;
            txt_mobile.Text = string.Empty;
            txt_name.Text = string.Empty;
            // txt_otp.Text = string.Empty;
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
            txt_email.Text = Convert.ToString(dt.Rows[0]["Email"]);
            txt_mobile.Text = Convert.ToString(dt.Rows[0]["mobile"]);
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
            string modetype = Transactionlist.SelectedValue;
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            DataTable dtMemberMaster = Cls.select_data_dt(@"EXEC Set_EzulixPayOut_new @action='chk', @msrno=" + Convert.ToInt32(ViewState["MsrNo"]) + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixPayOut_new @action='payoutsur',@amount='" + Convert.ToDecimal(amount) + "',@packageid='" + PackageID + "',@modetype=" + modetype + "");
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

            string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=283978AXbKsVmeubkY5d1f3aec&route=4&sender=Payout&mobiles=" + Mobile + "& message=" + smsMessage + "";
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
        "Dear Customer, you need an OTP  to access DMT Transaction for Rs.@v0@ and OTP is @v1@. Never Share it with anyone.Bank Never calls to verify it."//1
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
