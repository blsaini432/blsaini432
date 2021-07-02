using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Root_Retailer_dmr : System.Web.UI.Page
{

    private static int limitamount = 5000;
    private Boolean IsPageRefresh = false;
    #region Access_Class
    EzulixDmr eDmr = new EzulixDmr();
    cls_connection Cls = new cls_connection();
    DataTable dtmembermaster = new DataTable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dt = new DataTable();
                dtmembermaster = (DataTable)Session["dtRetailer"];
                if (dtmembermaster.Rows.Count > 0)
                {
                    int msrno = Convert.ToInt32(dtmembermaster.Rows[0]["MsrNo"]);
                    dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + msrno + "");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["isemailverify"]) == true)
                        {
                            ViewState["MemberId"] = null;
                            ViewState["MsrNo"] = null;
                            ViewState["MsrNo"] = msrno;
                            ViewState["MemberId"] = dt.Rows[0]["MemberId"].ToString();
                            ViewState["dmtmobile"] = dt.Rows[0]["Mobile"].ToString();
                            bindbankdetails();
                            bindbanner();
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }

    }
    #region Events

    protected void ddl_choosebankchanged(object sender, EventArgs e)
    {
        txt_Ifsccode.Text = ddl_choosebank.SelectedValue;
    }

    public void bindbanner()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners where isactive=1 and ServiceName='DMR' order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }

    }
    #endregion


    #region methods
    public void bindbankdetails()
    {
        DataTable dtbankname = new DataTable();
        dtbankname = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='ifsc'");
        ddl_choosebank.DataSource = dtbankname;
        ddl_choosebank.DataTextField = "bankname";
        ddl_choosebank.DataValueField = "masterifsc";
        ddl_choosebank.DataBind();
        ddl_choosebank.Items.Insert(0, new ListItem("Select Bank", "0"));

    }
    protected void btn_Reg_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Cls.select_data_dt(@"select * from tbl_Instant_Remiter_Reg where mobile='" + txt_Mobile.Text.Trim() + "' and isactive=1");
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Remitter is already registred');", true);
                
            }
            else
            {
                String Result = string.Empty;
                Result = eDmr.Remitter_Registration(txt_Mobile.Text.Trim(), txt_Name.Text.Trim(), txt_SurName.Text.Trim(), txt_Pin.Text.Trim());
                if (Result != string.Empty)
                {
                    DataSet ds = new DataSet();
                    ds = Deserialize(Result);
                    if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                    {
                        if(ds.Tables["remitter"].Rows[0]["is_verified"].ToString() =="0")
                        {
                            Session["remitteridv"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            divregotp.Visible = true;
                            divremitter.Visible = true;
                            btn_remiitervalidate.Visible = true;
                            btn_Reg.Visible = false;
                            home2.Visible = true;
                            profile2.Visible = false;
                        }
                        else
                        {
                            Session["remitteridv"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            divregotp.Visible = false;
                            btn_remiitervalidate.Visible = false;
                            btn_Reg.Visible = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);

                        }
                    }
                    else if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "EZX")
                    {
                        divregotp.Visible = false;
                        btn_remiitervalidate.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');window.location ='DashBoard.aspx';", true);
                    }
                    else
                    {
                        divregotp.Visible = false;
                        btn_remiitervalidate.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ResetControl();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');window.location ='DashBoard.aspx';", true);
        }
    }
    protected void gv_Benificerydetails_RowCommand(object sender, RepeaterCommandEventArgs e)
    {
        try
        {
            if (ViewState["Log_Mobile"] != null && ViewState["MsrNo"] != null && ViewState["MemberId"] != null)
            {
                DataTable dtbenidetail = (DataTable)Session["benidetail"];
                if (e.CommandName == "del")
                {
                    string Result = string.Empty;
                    string Index = Convert.ToString(e.CommandArgument);
                    Result = eDmr.Beneficiary_Delete(Index, ViewState["remitId"].ToString());
                    if (Result != string.Empty)
                    {
                        DataSet ds = Deserialize(Result);
                        if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                        {

                            ViewState["DelBeniId"] = null;
                            ViewState["DelBeniId"] = Index;
                            Panel_Del.Attributes.Add("style", "display:block;");
                            mp_DelOtp.Show();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                        }
                    }
                }
                else if (e.CommandName == "imps")
                {
                    string Index = Convert.ToString(e.CommandArgument);
                    DataTable tblFiltered = dtbenidetail.AsEnumerable().Where(row => row.Field<String>("id") == Index).CopyToDataTable();
                    ViewState["trabeniid"] = null;
                    ViewState["trabeniid"] = Index;
                    txt_Acno.Text = tblFiltered.Rows[0]["account"].ToString();
                    txt_Acno.Enabled = false;
                    txt_Ifsccode.Text = tblFiltered.Rows[0]["ifsc"].ToString();
                    txt_Ifsccode.Enabled = false;
                    txt_Beniname.Text = tblFiltered.Rows[0]["name"].ToString();
                    txt_Beniname.Enabled = false;
                    Button2.Visible = false;
                    chkselectifsc.Visible = false;
                  //  ddl_choosebank.SelectedValue = tblFiltered.Rows[0]["ifsc"].ToString();
                    ddl_choosebank.Enabled = false;
                    ddl_action.Enabled = false;
                    btn_SendAmount.Visible = true;
                    btn_Addbeni.Visible = false;
                    divtransferamount.Visible = true;
                    ResetTransfer();
                }

            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your seesion is time out, please login agarin!');window.location ='Dmr.aspx';", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');window.location ='DashBoard.aspx';", true);
        }
    }
    private static DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }

    private void FillBeni()
    {
        try
        {
            string Result = string.Empty;
            Result = eDmr.Remitter_Details(ViewState["Log_Mobile"].ToString());
            if (Result != string.Empty)
            {
                DataSet ds = Deserialize(Result);
                if (ds.Tables.Contains("beneficiary"))
                {
                    if (ds.Tables["beneficiary"].Rows.Count > 0)
                    {
                        rpt_benificiary.DataSource = ds.Tables["beneficiary"];
                        rpt_benificiary.DataBind();
                        Session.Add("benidetail", ds.Tables["beneficiary"]);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please add a beneficiary first');", true);

                }
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');window.location ='DashBoard.aspx';", true);
        }
    }


    protected void chkselectifscchanged(object sender, EventArgs e)
    {
        if (chkselectifsc.Checked == true)
        {
            txt_Ifsccode.Enabled = true;
            ddl_choosebank.Enabled = false;
        }
        else
        {
            ddl_choosebank.Enabled = true;
            txt_Ifsccode.Enabled = false;
        }
    }
    #endregion

    protected void btn_login_Click1(object sender, EventArgs e)
    {
        try
        {
            String Result = string.Empty;
            Result = eDmr.Remitter_Details(txt_loginID.Text.Trim());
          //  ResetBeni();
            if (Result != string.Empty)
            {
                DataSet ds = new DataSet();
                ds = Deserialize(Result);
                if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "RNF")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mobile number not register!, Please Register');", true);
                    divremitter.Visible = true;
                    divremitterreg.Visible = true;
                    divhome.Visible = true;
                    divbody.Visible = false;

                }
                else
                {
                    if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                    {
                        if (ds.Tables["remitter"].Rows[0]["is_verified"].ToString() == "0")
                        {
                            //txt_Mobile.Text = string.Empty;
                            //Session["remitteridv"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            //ModalPopupExtender1.Show();
                            divbody.Visible = false;

                        }
                        else
                        {
                            ViewState["Log_Mobile"] = null;
                            ViewState["Log_Mobile"] = txt_loginID.Text.Trim();
                            ViewState["remitId"] = null;
                            ViewState["remitId"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            Session["remitId"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            lbl_Remname.Text = ds.Tables["remitter"].Rows[0]["name"].ToString();
                            lbl_Remmno.Text = ds.Tables["remitter"].Rows[0]["mobile"].ToString();
                            lbl_Consumed.Text = ds.Tables["remitter"].Rows[0]["consumedlimit"].ToString();
                            lbl_reming.Text = ds.Tables["remitter"].Rows[0]["remaininglimit"].ToString();
                            rpt_benificiary.DataSource = ds.Tables["beneficiary"];
                            rpt_benificiary.DataBind();
                            Session.Add("benidetail", ds.Tables["beneficiary"]);
                            divremitter.Visible = false;
                            divremitterreg.Visible = false;
                            divhome.Visible = false;
                            divbody.Visible = true;

                        }
                    }
                    else if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "EZX")
                    {
                        divbody.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');window.location ='DashBoard.aspx';", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
         //   lblerror.Text = ex.ToString();
           ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');window.location ='DashBoard.aspx';", true);
        }

    }


    protected void btn_loginnew_Click1(object sender, EventArgs e)
    {
        try
        {
            String Result = string.Empty;
            Result = eDmr.Remitter_Details(Text1.Text.Trim());
            ResetBeni();
            if (Result != string.Empty)
            {
                DataSet ds = new DataSet();
                ds = Deserialize(Result);
                if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "RNF")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mobile number not register!, Please Register');", true);
                    divremitter.Visible = true;
                    divremitterreg.Visible = true;
                    divhome.Visible = true;
                    divbody.Visible = false;
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                    {
                        ResetBeni();
                        if (ds.Tables["remitter"].Rows[0]["is_verified"].ToString() == "0")
                        {
                            //txt_Mobile.Text = string.Empty;
                            //Session["remitteridv"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            //ModalPopupExtender1.Show();
                            divbody.Visible = false;

                        }
                        else
                        {
                            ViewState["Log_Mobile"] = null;
                            ViewState["Log_Mobile"] = Text1.Text.Trim();
                            ViewState["remitId"] = null;
                            ViewState["remitId"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            Session["remitId"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            lbl_Remname.Text = ds.Tables["remitter"].Rows[0]["name"].ToString();
                            lbl_Remmno.Text = ds.Tables["remitter"].Rows[0]["mobile"].ToString();
                            lbl_Consumed.Text = ds.Tables["remitter"].Rows[0]["consumedlimit"].ToString();
                            lbl_reming.Text = ds.Tables["remitter"].Rows[0]["remaininglimit"].ToString();
                            rpt_benificiary.DataSource = ds.Tables["beneficiary"];
                            rpt_benificiary.DataBind();
                            Session.Add("benidetail", ds.Tables["beneficiary"]);
                            divremitter.Visible = false;
                            divremitterreg.Visible = false;
                            divhome.Visible = false;
                            divbody.Visible = true;

                        }
                    }
                    else if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "EZX")
                    {
                        divbody.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');window.location ='DashBoard.aspx';", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');window.location ='DashBoard.aspx';", true);
        }

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string Result = string.Empty;
        cls_myMember Clsm = new cls_myMember();
        dtmembermaster = (DataTable)Session["dtRetailer"];
        int msrno = Convert.ToInt32(dtmembermaster.Rows[0]["MsrNo"]);
        string memberid = dtmembermaster.Rows[0]["MemberId"].ToString();
        if (msrno > 0 && ViewState["MsrNo"] != null)
        {
            int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(3), Convert.ToInt32(msrno));
            if (res == 1)
            {
                string ETranId = string.Empty;
                ETranId = Clsm.Cyrus_GetTransactionID_New();
                DataTable dtMemberId = Cls.select_data_dt(@"select MemberID from tblmlm_membermaster where msrno=" + msrno + "");
                int tra = Clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal("-" + 3), "Dr", "DMR AC Verify Txn:- " + ETranId + "");
                if (tra > 0)
                {
                    Result = eDmr.Beneficiary_Account_Verification(ViewState["Log_Mobile"].ToString(), txt_Acno.Text, txt_Ifsccode.Text, ViewState["remitId"].ToString());
                    if (Result != string.Empty)
                    {
                        DataSet ds = Deserialize(Result);
                        if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                        {
                            txt_Beniname.Text = ds.Tables[1].Rows[0]["benename"].ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Benificery verify successfully');", true);

                        }
                        else if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "SPD" || ds.Tables[0].Rows[0]["statuscode"].ToString() == "IAN" || ds.Tables[0].Rows[0]["statuscode"].ToString() == "EZX")
                        {
                            Clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(3), "Cr", "FailDMR AC Verify Txn:- " + ETranId + "");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                        }
                        else
                        {
                            Cls.select_data_dt(@"insert into Instant_bug values ('AC VERIFY','" + Result + ",ETranId:" + ETranId + "," + DateTime.Now.ToString() + "')");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');window.location ='DashBoard.aspx';", true);
                }
            }
        }
        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Session is time out !');window.location ='DashBoard.aspx';", true);
        }
    }

    protected void btn_SendAmount_Click(object sender, EventArgs e)
    {
        if (ViewState["Log_Mobile"] != null && ViewState["MsrNo"] != null && ViewState["MemberId"] != null)
        {
            if (Convert.ToInt32(txt_Amount.Text.Trim()) <= 25000)
            {
                Random random = new Random();
                int SixDigit = random.Next(1000, 9999);
                Session["chdmtOTP"] = SixDigit.ToString();
                Session["txtamount"] = txt_Amount.Text.Trim();
                string[] valueArray = new string[2];
                valueArray[0] = txt_Amount.Text;
                valueArray[1] = Session["chdmtOTP"].ToString();
                SendWithVarpan(ViewState["dmtmobile"].ToString(), 1, valueArray);
                btn_dmrotp.Visible = true;
                btn_SendAmount.Visible = false;
                btn_Addbeni.Visible = false;
                diventerotp.Visible = true;
            }
            else
            {
                // ResetControl();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Amount could not be grater than 25000!')", true);
            }
        }
        else
        {
            // ResetControl();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your seesion is time out, please login agarin!');window.location ='Dmr.aspx';", true);
        }
    }

    protected void btn_dmrotp_Click(object sender, EventArgs e)
    {
        string Result = string.Empty;
        try
        {
            if (txt_dmrotp.Text.Trim() == Session["chdmtOTP"].ToString())
            {
                if (txt_Amount.Text.Trim() == Session["txtamount"].ToString())
                {
                    DataSet ds = new DataSet();
                    cls_myMember Clsm = new cls_myMember();
                    if (ViewState["Log_Mobile"] != null && ViewState["MsrNo"] != null && ViewState["MemberId"] != null)
                    {
                        if (Convert.ToInt32(txt_Amount.Text.Trim()) <= 25000)
                        {
                            DataTable dtbenidetail = (DataTable)Session["benidetail"];
                            DataTable tblFiltered = dtbenidetail.AsEnumerable().Where(row => row.Field<String>("id") == ViewState["trabeniid"].ToString()).CopyToDataTable();
                            string bundleid = Clsm.Cyrus_GetTransactionID_New();
                            int totaltra;
                            decimal TraAmt = Convert.ToDecimal(txt_Amount.Text.Trim());
                            decimal totalamt = Convert.ToDecimal(txt_Amount.Text.Trim());
                            decimal RemainAmt = 0;
                            if (Convert.ToInt32(txt_Amount.Text.Trim()) % limitamount == 0)
                                totaltra = Convert.ToInt32(txt_Amount.Text.Trim()) / limitamount;
                            else
                                totaltra = (Convert.ToInt32(txt_Amount.Text.Trim()) / limitamount) + 1;
                            for (int i = 1; i <= totaltra; i++)
                            {
                                if (totalamt <= limitamount && TraAmt > 0)
                                    if (i > 1)
                                        TraAmt = RemainAmt;
                                    else
                                        TraAmt = Convert.ToDecimal(txt_Amount.Text.Trim());
                                else
                                    TraAmt = Convert.ToDecimal(limitamount);
                                double NetAmount = TotupAmount(Convert.ToDouble(TraAmt), ViewState["MemberId"].ToString());
                                if (NetAmount > Convert.ToDouble(TraAmt))
                                {
                                    int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), Convert.ToInt32(ViewState["MsrNo"]));
                                    if (res == 1)
                                    {
                                        string ETranId = string.Empty;
                                        ETranId = Clsm.Cyrus_GetTransactionID_New();
                                        int tra = Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal("-" + NetAmount.ToString().Trim()), "Dr", "DMR Topup Txn:- " + ETranId + "");
                                        if (tra > 0)
                                        {
                                            Result = eDmr.Fund_Transfer(ViewState["Log_Mobile"].ToString(), ViewState["trabeniid"].ToString(), ETranId, Convert.ToDecimal(TraAmt), "IMPS", ViewState["MemberId"].ToString(), ETranId, tblFiltered.Rows[0]["name"].ToString(), tblFiltered.Rows[0]["bank"].ToString(), tblFiltered.Rows[0]["account"].ToString(), bundleid);
                                            if (Result != string.Empty)
                                            {
                                                ds = Deserialize(Result);
                                                if (ds.Tables.Contains("root") && ds.Tables["root"].Columns.Contains("statuscode"))
                                                {
                                                    if (ds.Tables["root"].Rows[0]["statuscode"].ToString() == "EZX")
                                                    {
                                                        Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "TXN")
                                                    {
                                                        Session["chdmtOTP"] = null;
                                                        Session["txtamount"] = null;
                                                        List<ParmList> _lstparm = new List<ParmList>();
                                                        _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                        _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["data"].Rows[0]["ipay_id"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["data"].Rows[0]["ref_no"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["data"].Rows[0]["opr_id"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["charged_amt"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["locked_amt"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["response"].Rows[0]["statuscode"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["response"].Rows[0]["status"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["name"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["account"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["ifsc"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                        _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["bank"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                        _lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                        Cls.select_data_dtNew("Set_Instant_Fund_Tra", _lstparm);
                                                        Cls.select_data_dt(@"EXEC SET_DIST_Commission '" + ViewState["MemberId"].ToString() + "','" + TraAmt + "','" + ETranId + "','dmr'");
                                                        RemainAmt = totalamt - TraAmt;
                                                        TraAmt = RemainAmt;
                                                        totalamt = RemainAmt;
                                                    }
                                                    else if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "ETO")
                                                    {
                                                        List<ParmList> _lstparm = new List<ParmList>();
                                                        Session["chdmtOTP"] = null;
                                                        Session["txtamount"] = null;
                                                        _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ViewState["remitId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                        _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = "" });
                                                        _lstparm.Add(new ParmList() { name = "@ref_no", value = "" });
                                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = "" });
                                                        _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal("0.00") });
                                                        _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal("0.00") });
                                                        _lstparm.Add(new ParmList() { name = "@statuscode", value = "" });
                                                        _lstparm.Add(new ParmList() { name = "@status_", value = "" });
                                                        _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["name"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["account"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                        _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["ifsc"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["bank"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                        Cls.select_data_dtNew("Set_Instant_Fund_Tra", _lstparm);
                                                        RemainAmt = TraAmt - limitamount;
                                                        TraAmt = RemainAmt;
                                                        totalamt = RemainAmt;
                                                    }
                                                    else if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "TUP")
                                                    {
                                                        List<ParmList> _lstparm = new List<ParmList>();
                                                        Session["chdmtOTP"] = null;
                                                        Session["txtamount"] = null;
                                                        _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ViewState["remitId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                        _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["data"].Rows[0]["ipay_id"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["data"].Rows[0]["ref_no"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["data"].Rows[0]["opr_id"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["charged_amt"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["locked_amt"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["response"].Rows[0]["statuscode"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["response"].Rows[0]["status"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["name"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["account"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                        _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["ifsc"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["bank"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                        _lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                        Cls.select_data_dtNew("Set_Instant_Fund_Tra", _lstparm);
                                                        break;
                                                    }
                                                    else if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "IAN" || ds.Tables["response"].Rows[0]["statuscode"].ToString() == "SPE")
                                                    {
                                                        List<ParmList> _lstparm = new List<ParmList>();
                                                        Session["chdmtOTP"] = null;
                                                        Session["txtamount"] = null;
                                                        _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ViewState["remitId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                        _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["data"].Rows[0]["ipay_id"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["data"].Rows[0]["ref_no"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["data"].Rows[0]["opr_id"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["charged_amt"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["data"].Rows[0]["locked_amt"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["response"].Rows[0]["statuscode"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["response"].Rows[0]["status"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["name"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["account"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                        _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["ifsc"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["bank"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                        _lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                        Cls.select_data_dtNew("Set_Instant_Fund_Tra", _lstparm);
                                                        Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                        break;
                                                    }
                                                    else if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "ERR" || ds.Tables["response"].Rows[0]["statuscode"].ToString() == "DTX" || ds.Tables["response"].Rows[0]["statuscode"].ToString() == "IAB")
                                                    {
                                                        if (ds.Tables["response"].Rows[0]["status"].ToString() == "Beneficiary Verifiaction Pending")
                                                        {
                                                            Session["chdmtOTP"] = null;
                                                            Session["txtamount"] = null;
                                                            Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["response"].Rows[0]["status"].ToString() + "');", true);
                                                            ResetBeni();
                                                            txt_Amount.Text = string.Empty;
                                                            //lbl_TrabeniName.Text = string.Empty;
                                                            //lbl_TraACNo.Text = string.Empty;
                                                            //lbl_TraIfsc.Text = string.Empty;
                                                            //"IMPS" = string.Empty;
                                                            //TabContainer1.ActiveTabIndex = 2;
                                                            //dv_Newbeni.Visible = false;
                                                            //dv_beniOtp.Visible = true;
                                                            ViewState["beneficiary_id"] = ViewState["trabeniid"].ToString();
                                                            txt_Beniname.Text = tblFiltered.Rows[0]["name"].ToString();
                                                            // txt_Benimobile.Text = tblFiltered.Rows[0]["mobile"].ToString();
                                                            txt_Acno.Text = tblFiltered.Rows[0]["account"].ToString();
                                                            txt_Ifsccode.Text = tblFiltered.Rows[0]["ifsc"].ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "IAB")
                                                            {
                                                                Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Dmr Service is down, Try After Some time !');", true);
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["response"].Rows[0]["status"].ToString() + "');", true);
                                                                ResetBeni();
                                                                txt_Amount.Text = string.Empty;
                                                                //lbl_TrabeniName.Text = string.Empty;
                                                                //lbl_TraACNo.Text = string.Empty;
                                                                //lbl_TraIfsc.Text = string.Empty;
                                                                //"IMPS" = string.Empty;
                                                                //TabContainer1.ActiveTabIndex = 3;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    else if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "ISE")
                                                    {
                                                        Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Cls.select_data_dt(@"insert into Instant_bug values ('Transcation','" + Result + ",ETranId:" + ETranId + "," + DateTime.Now.ToString() + "')");
                                                        ResetBeni();
                                                        txt_Amount.Text = string.Empty;
                                                        //lbl_TrabeniName.Text = string.Empty;
                                                        //lbl_TraACNo.Text = string.Empty;
                                                        //lbl_TraIfsc.Text = string.Empty;
                                                        //"IMPS" = string.Empty;
                                                        //TabContainer1.ActiveTabIndex = 5;
                                                        //FillTraDetail();
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["response"].Rows[0]["status"].ToString() + "');", true);
                                                        break;
                                                    }
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["response"].Rows[0]["status"].ToString() + "');", true);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ResetControl();
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');window.location ='DashBoard.aspx';", true);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        ResetControl();
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet balance is insufficent for this transcation !');window.location ='DashBoard.aspx';", true);
                                        break;
                                    }
                                }
                                else
                                {
                                    ResetControl();
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Surcharge is not define, Contact to your admin !');window.location ='DashBoard.aspx';", true);
                                    break;
                                }
                            }
                            ResetBeni();
                            //if (ds.Tables["response"].Rows[0]["statuscode"].ToString() == "TXN")
                            //{
                            //    pnl_Recipt.Attributes.Add("style", "display:block;");
                            //    mpe_Recipt.Show();
                            //    DataTable dtCompany = Cls.select_data_dt(@"select * from tblcompany where companyid=2");
                            //    imgCompanyLogo.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtCompany.Rows[0]["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtCompany.Rows[0]["companylogo"]);
                            //    lbl_Company_Name.Text = dtCompany.Rows[0]["CompanyName"].ToString();
                            //    lbl_Remitter_Name.Text = lbl_Remname.Text;
                            //    lbl_Beni_Name.Text = tblFiltered.Rows[0]["name"].ToString();
                            //    lbl_Bani_ac_ifsc.Text = tblFiltered.Rows[0]["account"].ToString() + "[" + tblFiltered.Rows[0]["ifsc"].ToString() + "]";
                            //    DataTable dtTxn = Cls.select_data_dt(@"select mode,EzulixTranid,amount,status_,ref_no,created,remit_mobile from tbl_instant_fund_tra where budleid='" + bundleid + "'");
                            //    lbl_Remitter_Mobile.Text = dtTxn.Rows[0]["remit_mobile"].ToString();
                            //    grid_Recipt.DataSource = dtTxn;
                            //    grid_Recipt.DataBind();
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["response"].Rows[0]["status"].ToString() + "');", true);
                            //}
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["response"].Rows[0]["status"].ToString() + "');", true);
                        }
                        else
                        {
                            ResetControl();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Amount could not be grater than 25000!')", true);
                        }
                    }
                    else
                    {
                        ResetControl();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your seesion is time out, please login agarin!');window.location ='Dmr.aspx';", true);
                    }
                }
                else
                {
                    ResetControl();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You have tried to change amount,otp is not valid for this transaction amount');window.location ='Dmr.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('OTP is not valid for this transaction');", true);
            }
        }
        catch (Exception ex)
        {
            Cls.select_data_dt(@"insert into Instant_bug values ('Transcation','" + Result + "," + DateTime.Now.ToString() + "')");
            ResetControl();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');", true);
        }


    }

    private void ResetBeni()
    {
        txt_Ifsccode.Enabled = true;
        txt_Ifsccode.Text = "";
        txt_Beniname.Text = "";
        txt_Acno.Text = "";
        ddl_choosebank.SelectedIndex = 0;
        txt_Beniname.Enabled = true;
        txt_Acno.Enabled = true;
        btn_SendAmount.Visible = false;
        btn_dmrotp.Visible = false;
        divtransferamount.Visible = false;
        ddl_choosebank.Enabled = true;
        chkselectifsc.Enabled = true;
        chkselectifsc.Visible = true;
        Button2.Visible = true;
        diventerotp.Visible = false;
        btn_Addbeni.Visible = true;
        txt_DelOtp.Text = string.Empty;
        txt_dmrotp.Text = string.Empty;
        txt_remitterotp.Text = string.Empty;
    }
    private void ResetTransfer()
    {
        txt_Amount.Text = "";
        txt_Acno.Enabled = false;
        txt_Ifsccode.Enabled = false;
        txt_Beniname.Enabled = false;
        Button2.Visible = false;
        chkselectifsc.Visible = false;
        ddl_choosebank.Enabled = false;
        ddl_action.Enabled = false;
        btn_SendAmount.Visible = true;
        btn_Addbeni.Visible = false;
        divtransferamount.Visible = true;
        diventerotp.Visible = false;
        btn_dmrotp.Visible = false;
        txt_DelOtp.Text = string.Empty;
        txt_dmrotp.Text = string.Empty;
        txt_remitterotp.Text = string.Empty;
    }

    private void ResetControl()
    {
        txt_DelOtp.Text = string.Empty;
        txt_dmrotp.Text = string.Empty;
        txt_remitterotp.Text = string.Empty;
        txt_Ifsccode.Enabled = true;
        txt_Ifsccode.Text = "";
        txt_Beniname.Text = "";
        txt_Acno.Text = "";
        ddl_choosebank.SelectedIndex = 0;
        txt_Beniname.Enabled = true;
        txt_Acno.Enabled = true;
        btn_SendAmount.Visible = false;
        btn_dmrotp.Visible = false;
        divtransferamount.Visible = false;
        ddl_choosebank.Enabled = true;
        chkselectifsc.Enabled = true;
        chkselectifsc.Visible = true;
        Button2.Visible = true;
        diventerotp.Visible = false;
        btn_Addbeni.Visible = true;
    }
    protected void btn_Recipt_PrintClose_Click(object sender, EventArgs e)
    {
        mpe_Recipt.Hide();
    }
    protected void btn_Addbeni_Click(object sender, EventArgs e)
    {
        try
        {
            string Result = string.Empty;
            DataTable dt = Cls.select_data_dt(@"select * from tbl_Instant_Beni_Reg where  beni_ac='" + txt_Acno.Text.Trim() + "' and beni_ifsc='" + txt_Ifsccode.Text.Trim() + "' and remitId='" + ViewState["remitId"].ToString() + "' and isactive=1");
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Beneficiary is already registred');", true);
                FillBeni();
                ResetBeni();
            }
            else
            {
                Result = eDmr.Beneficiary_Registration(ViewState["remitId"].ToString(), txt_Beniname.Text.Trim(), "", txt_Ifsccode.Text.Trim(), txt_Acno.Text.Trim());
                if (Result != string.Empty)
                {
                    DataSet ds = Deserialize(Result);
                    if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                    {
                        ViewState["beneficiary_id"] = null;
                        ViewState["beneficiary_id"] = ds.Tables["beneficiary"].Rows[0]["id"].ToString();
                        FillBeni();
                        ResetBeni();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');", true);
        }
    }


    protected void btn_BeniDelOtpSub_Click(object sender, EventArgs e)
    {
        try
        {
            mp_DelOtp.Hide();
            string Result = string.Empty;
            Result = eDmr.Beneficiary_Delete_Validate(ViewState["DelBeniId"].ToString(), ViewState["remitId"].ToString(), txt_DelOtp.Text.Trim());
            if (Result != string.Empty)
            {
                DataSet ds = Deserialize(Result);
                if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                    Panel_Del.Attributes.Add("style", "display:none;");
                    mp_DelOtp.Hide();
                    FillBeni();
                    ResetBeni();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                    Panel_Del.Attributes.Add("style", "display:block;");
                    mp_DelOtp.Show();
                    ResetBeni();
                }
            }
        }
        catch (Exception ex)
        {
            ResetBeni();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');window.location ='DashBoard.aspx';", true);
        }
    }
    #region  
    [WebMethod]
    public string BindBenificary(string mobile)
    {
        string st = string.Empty;
        try
        {
            EzulixDmr eDmr = new EzulixDmr();
            string Result = string.Empty;
            Result = eDmr.Remitter_Details(mobile);
            if (Result != string.Empty)
            {
                DataSet ds = Deserialize(Result);
                if (ds.Tables.Contains("beneficiary"))
                {
                    if (ds.Tables["beneficiary"].Rows.Count > 0)
                    {
                        DataTable dtt = ds.Tables["beneficiary"];
                        st = ConvertDataTabletoString(dtt);
                    }
                    else
                    {
                        st = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {

            st = "";
        }
        return st;
    }
    #endregion


    #region  
    [WebMethod]
    public static List<RootObject> RemitLogin(string mobile)
    {
        string Result = string.Empty;
        List<RootObject> custlist = new List<RootObject>();
        RootObject cust = new RootObject();
        try
        {
            EzulixDmr eDmr = new EzulixDmr();
            Page page = HttpContext.Current.Handler as Page;
            var currentScriptManager = System.Web.UI.ScriptManager.GetCurrent(page);
            Result = eDmr.Remitter_Details(mobile);
            if (Result != string.Empty)
            {
                RootObject Auths = JsonConvert.DeserializeObject<RootObject>(Result);
                DataSet ds = new DataSet();
                ds = Deserialize(Result);
                if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "RNF")
                {
                    cust.status = ds.Tables[0].Rows[0]["Status"].ToString();
                    cust.statuscode = ds.Tables[0].Rows[0]["StatusCode"].ToString();
                    custlist.Add(cust);
                    return custlist;
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                    {
                        if (ds.Tables["remitter"].Rows[0]["is_verified"].ToString() == "0")
                        {
                            HttpContext.Current.Session["remitteridv"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            cust.data.remitter.name = ds.Tables["remitter"].Rows[0]["name"].ToString();
                            cust.data.remitter.remaininglimit = ds.Tables["remitter"].Rows[0]["remaininglimit"].ToString();
                            cust.data.remitter.consumedlimit = ds.Tables["remitter"].Rows[0]["consumedlimit"].ToString();
                            custlist.Add(cust);
                            return custlist;
                        }
                        else
                        {
                            cust.data.remitter.name = ds.Tables["remitter"].Rows[0]["name"].ToString();
                            cust.data.remitter.remaininglimit = ds.Tables["remitter"].Rows[0]["remaininglimit"].ToString();
                            cust.data.remitter.consumedlimit = ds.Tables["remitter"].Rows[0]["consumedlimit"].ToString();
                            custlist.Add(cust);
                            return custlist;
                        }
                    }
                    else if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "EZX")
                    {
                        return custlist;
                    }
                }
            }
        }
        catch (Exception ex)
        {

            return custlist;
        }
        return custlist;
    }

    #endregion
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

    #region dmrclassess
    public class Remitter
    {
        public string id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string kycstatus { get; set; }
        public string consumedlimit { get; set; }
        public string remaininglimit { get; set; }
        public string kycdocs { get; set; }
        public int is_verified { get; set; }
        public int perm_txn_limit { get; set; }
    }

    public class Beneficiary
    {
        public string id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string account { get; set; }
        public string bank { get; set; }
        public string status { get; set; }
        public string last_success_date { get; set; }
        public string last_success_name { get; set; }
        public string last_success_imps { get; set; }
        public string ifsc { get; set; }
        public string imps { get; set; }
    }

    public class Mode
    {
        public int imps { get; set; }
        public int neft { get; set; }
    }

    public class Limit
    {
        public int total { get; set; }
        public int consumed { get; set; }
        public int remaining { get; set; }
    }

    public class RemitterLimit
    {
        public int code { get; set; }
        public int status { get; set; }
        public Mode mode { get; set; }
        public Limit limit { get; set; }
    }

    public class Data
    {
        public Remitter remitter { get; set; }
        public List<Beneficiary> beneficiary { get; set; }
        public List<RemitterLimit> remitter_limit { get; set; }
    }

    public class RootObject
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public Data data { get; set; }
    }
    public static void SendWithVarpan(string Mobile, int Template, string[] ValueArray)
    {
        try
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string smsMessage = GetString(Template, ValueArray);
            string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=198296AFda5tMRgn5a854e41&route=4&sender=EZXDMT&mobiles=" + Mobile + "& message=" + smsMessage + "";
            // string baseurl = "http://www.panaceasms.in/new/api/api_http.php?username=ezulix&password=sky12345&senderid=SKYTLE&to=" + Mobile + "&text=" + smsMessage + "&route=Transaction&type=text";
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
    public double TotupAmount(double amount, string memberid)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            DataTable dtmembers = new DataTable();
            dtmembers = (DataTable)Session["dtRetailer"];
            DataTable dtMemberMaster = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='chk', @msrno=" + Convert.ToInt32(dtmembers.Rows[0]["MsrNo"]) + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantsur',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
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
    #endregion


    protected void btnaddbenecheck_Click(object sender, EventArgs e)
    {
        ResetBeni();
    }

    protected void btnclose_Click(object sender, EventArgs e)
    {
        mp_DelOtp.Hide();
    }
   
    protected void btn_remiitervalidate_Click(object sender, EventArgs e)
    {
        String Result = string.Empty;
        string remitterid = Session["remitteridv"].ToString();
        string mobile = txt_Mobile.Text.Trim();
        if (txt_Mobile.Text.Trim() == string.Empty)
        {
            mobile = txt_loginID.Text = string.Empty;
        }
        string otp = txt_remitterotp.Text.Trim();
        Result = eDmr.Remitter_Validate(remitterid, mobile, otp);
        if (Result != string.Empty)
        {
            DataSet ds = new DataSet();
            ds = Deserialize(Result);
            if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
            {
                Cls.select_data_dt("Exec SET_Instant_Remiter_Reg '" + ViewState["MemberId"].ToString() + "','" + txt_Mobile.Text.Trim() + "','" + txt_Name.Text.Trim() + "','" + txt_Pin.Text.Trim() + "','" + Session["remitteridv"].ToString() + "'");
                txt_Name.Text = string.Empty;
                txt_Mobile.Text = string.Empty;
                txt_Pin.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Remitter is registered successfully');", true);
            }
            else if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "EZX")
            {
                divregotp.Visible = false;
                btn_remiitervalidate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');window.location ='DashBoard.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                divregotp.Visible = true;
                btn_remiitervalidate.Visible = true;
                btn_Reg.Visible = false;
               
            }
        }
    }

    protected void link1_Click(object sender, EventArgs e)
    {
        home2.Visible = true;
        profile2.Visible = false;
        home2.Attributes.Add("class", "tab-pane fade active show");

    }

    protected void link2_Click(object sender, EventArgs e)
    {
        home2.Visible = false;
        profile2.Visible = true;
    }
}