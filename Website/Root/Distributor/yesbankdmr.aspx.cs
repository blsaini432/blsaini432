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
using System.Text;

public partial class Root_Distributor_yesbankdmr : System.Web.UI.Page
{

    private static int limitamount = 5000;
    private Boolean IsPageRefresh = false;
    #region Access_Class
    YesBankdmr yesdmr = new YesBankdmr();
    cls_connection Cls = new cls_connection();
    DataTable dtmembermaster = new DataTable();
    private static string AID = "SSDT123456";
    private static string OP = "DMTNUR";
    private static string ST = "REMDOMESTIC";
    private static string request = "BENVERIFICATION";
    private static string benidelete = "BENDELETE";
    private static string customerequest = "CUSTVERIFICATION";
    private static string mm_token = "83af3ace-f1cc-4ab7-b64e-969ebd0f95a9";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                DataTable dt = new DataTable();
                dtmembermaster = (DataTable)Session["dtDistributor"];
                if (dtmembermaster.Rows.Count > 0)
                {
                    //int msrno = Convert.ToInt32(dtmembermaster.Rows[0]["MsrNo"]);
                    //dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantdmr', @msrno=" + msrno + "");
                    //if (dt.Rows.Count > 0)
                    //{
                    //    if (Convert.ToBoolean(dt.Rows[0]["isemailverify"]) == true)
                    //    {
                            ViewState["MemberId"] = null;
                            ViewState["MsrNo"] = null;
                            ViewState["MsrNo"] = dtmembermaster.Rows[0]["Msrno"].ToString(); 
                            ViewState["MemberId"] = dtmembermaster.Rows[0]["MemberId"].ToString();
                            ViewState["dmtmobile"] = dtmembermaster.Rows[0]["Mobile"].ToString();
                            bindbankdetails();
                    //    }
                    //    else
                    //    {

                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                    //    }
                    //}
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
    #endregion

    #region Search Customer 
    protected void btn_login_Click1(object sender, EventArgs e)
    {
        try
        {
            String Result = string.Empty;
            Result = yesdmr.SearchCustomer(txt_loginID.Text.Trim(), AID.ToString(), OP.ToString(), ST.ToString());
            //  ResetBeni();
            if (Result != string.Empty)
            {
                DataSet ds = new DataSet();
                ds = Deserialize(Result);
                if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "700")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                    divremitter.Visible = true;
                    divremitterreg.Visible = true;
                    divhome.Visible = true;
                    divbody.Visible = false;
                    txt_loginID.Visible = true;

                }
                else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "500")
                {
                    divremitter.Visible = true;
                    divbody.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
                    {

                        ViewState["Log_Mobile"] = null;
                        ViewState["Log_Mobile"] = txt_loginID.Text.Trim();
                        ViewState["remitId"] = null;
                        ViewState["remitId"] = ds.Tables["BENEFICIARY_DATA"].Rows[0]["BENE_ID"].ToString();
                        Session["remitId"] = ds.Tables["BENEFICIARY_DATA"].Rows[0]["BENE_ID"].ToString();
                        lbl_Remname.Text = ds.Tables["DATA"].Rows[0]["SEDNER_FNAME"].ToString();
                        lbl_Remmno.Text = ds.Tables["DATA"].Rows[0]["SENDER_MOBILENO"].ToString();
                        lbl_Consumed.Text = ds.Tables["DATA"].Rows[0]["SENDER_AVAILBAL"].ToString();
                        lbl_reming.Text = ds.Tables["DATA"].Rows[0]["SENDER_MONTHLYBAL"].ToString();
                        rpt_benificiary.DataSource = ds.Tables["BENEFICIARY_DATA"];
                        rpt_benificiary.DataBind();
                        Session.Add("benidetail", ds.Tables["BENEFICIARY_DATA"]);
                        divremitter.Visible = false;
                        divremitterreg.Visible = false;
                        divhome.Visible = false;
                        divbody.Visible = true;

                    }
                    else
                    {
                        divbody.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');window.location ='DashBoard.aspx';", true);
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

    private static DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
    #endregion

    #region Search Customer verify
    protected void btn_customer_verify(object sender, EventArgs e)
    {
        try
        {
            String Result = string.Empty;

            Result = yesdmr.SearchCustomer(txt_verifycus.Text.Trim(), AID.ToString(), OP.ToString(), ST.ToString());
            if (Result != string.Empty)
            {
                DataSet dsa = new DataSet();
                dsa = Deserialize(Result);
                if (dsa.Tables[0].Rows[0]["RESPONSE"].ToString() == "EPMONEY_CUST_NOT_VERIFIED")
                {
                    Session["remitteridv"] = dsa.Tables["BENEFICIARY_DATA"].Rows[0]["BENE_ID"].ToString();
                    Result = yesdmr.Customerverify(txt_verifycus.Text.Trim(), AID.ToString(), OP.ToString(), ST.ToString(), request);
                    if (Result != string.Empty)
                    {
                        DataSet ds = new DataSet();
                        ds = Deserialize(Result);
                        if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "700")
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);

                            divremitter.Visible = true;
                            divremitterreg.Visible = true;
                            divhome.Visible = true;
                            divbody.Visible = false;
                            txt_loginID.Visible = true;

                        }
                        else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "500")
                        {
                            divremitter.Visible = true;
                            divbody.Visible = false;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                        }
                        else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
                        {
                            // Session["remitteridv"] = ds.Tables[0].Rows[0]["BENE_ID"].ToString();
                            Session["Responsecode"] = ds.Tables[0].Rows[0]["RESPONSE_CODE"].ToString();
                            divremitter.Visible = false;
                            divremitterreg.Visible = false;
                            Panel_gDel.Attributes.Add("style", "display:block;");
                            btncusverify.Show();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsa.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                }

            }
        }
        catch (Exception ex)
        {
            //   lblerror.Text = ex.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');window.location ='DashBoard.aspx';", true);
        }

    }

    #endregion

    #region Customer Registration
    protected void btn_Reg_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Cls.select_data_dt(@"select * from tbl_Yesmoney_Remiter_Reg where mobile='" + txt_Mobile.Text.Trim() + "' and isactive=1");
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Remitter is already registred');", true);

            }
            else
            {
                String Result = "";
                string CUST_LNAME = "";
                string STATE = "";
                string CUST_ADDRESS = "";
                string PINCODE = "";
                string CUST_TITLE = "";
                string CITY = "";
                string CUST_EMAIL = "";
                string CUST_ALTMOBILENO = "";
                string BENE_MOBILENO = "";
                string CUST_DOB = "";
                Result = yesdmr.Customer_Registration(txt_Mobile.Text.Trim(), txt_Name.Text.Trim(), txt_SurName.Text.Trim(), txt_account.Text.Trim(), txt_ifsc.Text.Trim(), AID.ToString(), OP.ToString(), ST.ToString(), CUST_LNAME, STATE, CUST_ADDRESS, PINCODE, CUST_TITLE, CITY, CUST_EMAIL, CUST_ALTMOBILENO, BENE_MOBILENO, CUST_DOB);
                if (Result != string.Empty)
                {
                    DataSet ds = new DataSet();
                    ds = Deserialize(Result);
                    if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
                    {
                        if (ds.Tables[0].Rows[0]["RESPONSE"].ToString() == "EPMONEY_ADD_CUSTOMER_BENEFICIARY_SUCCESS")
                        {
                            Session["remitteridv"] = ds.Tables[0].Rows[0]["BENE_ID"].ToString();
                            Session["Responsecode"] = ds.Tables[0].Rows[0]["RESPONSE_CODE"].ToString();
                            divregotp.Visible = true;
                            btn_remiitervalidate.Visible = true;
                            btn_Reg.Visible = false;
                        }
                        else
                        {
                            Session["remitteridv"] = ds.Tables["remitter"].Rows[0]["id"].ToString();
                            divregotp.Visible = false;
                            btn_remiitervalidate.Visible = false;
                            btn_Reg.Visible = false;
                            divremitter.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);

                        }
                    }
                    else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "700")
                    {
                        btn_Reg.Visible = true;
                        divregotp.Visible = false;
                        btn_remiitervalidate.Visible = false;
                        divremitter.Visible = false;
                        divbody.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                    }
                    else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "991")
                    {
                        btn_Reg.Visible = true;
                        divregotp.Visible = false;
                        btn_remiitervalidate.Visible = false;
                        divremitter.Visible = false;
                        divbody.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                    }
                    else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "420")
                    {
                        btn_Reg.Visible = true;
                        divregotp.Visible = false;
                        btn_remiitervalidate.Visible = false;
                        divremitter.Visible = false;
                        divbody.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                    }
                    else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "500")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                        divregotp.Visible = false;
                        btn_remiitervalidate.Visible = false;
                        divremitter.Visible = true;
                        divbody.Visible = false;
                        btn_Reg.Visible = true;
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
    #endregion

    #region ResetControl
    private void ResetControl()
    {
        txt_DelOtp.Text = string.Empty;
        txt_dmrotp.Text = string.Empty;
        txt_remitterotp.Text = string.Empty;
        txt_Ifsccode.Enabled = true;
        txt_Ifsccode.Text = "";
        txt_Beniname.Text = "";
        txt_Acno.Text = "";
        //  ddl_choosebank.SelectedIndex = 0;
        txt_Beniname.Enabled = true;
        txt_Acno.Enabled = true;
        //  btn_SendAmount.Visible = false;
        //  btn_dmrotp.Visible = false;
        //  divtransferamount.Visible = false;
        // ddl_choosebank.Enabled = true;
        // chkselectifsc.Enabled = true;
        // chkselectifsc.Visible = true;
        // Button2.Visible = true;
        diventerotp.Visible = false;
        // btn_Addbeni.Visible = true;
    }
    private void ResetTransfer()
    {
        txt_Amount.Text = "";
        txt_Acno.Enabled = false;
        txt_Ifsccode.Enabled = false;
        txt_Beniname.Enabled = false;
        //  Button2.Visible = false;
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
    #endregion

    #region beni reg otp Verify 
    protected void btn_remiitervalidate_Click(object sender, EventArgs e)
    {
        String Result = string.Empty;
        string remitterid = Session["remitteridv"].ToString();
        string Responsecode = Session["Responsecode"].ToString();
        string mobile = txt_Mobile.Text.Trim();
        if (txt_Mobile.Text.Trim() == string.Empty)
        {
            mobile = txt_loginID.Text = string.Empty;
        }
        string otp = txt_remitterotp.Text.Trim();
        Result = yesdmr.cusotpverify(AID.ToString(), OP.ToString(), ST.ToString(), remitterid, mobile, otp, Responsecode, customerequest);
        if (Result != string.Empty)
        {
            DataSet ds = new DataSet();
            ds = Deserialize(Result);
            if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
            {
                Cls.select_data_dt("Exec SET_yesmoney_Remiter_Reg '" + ViewState["MemberId"].ToString() + "','" + txt_Mobile.Text.Trim() + "','" + txt_Name.Text.Trim() + "','" + txt_account.Text.Trim() + "','" + Session["remitteridv"].ToString() + "'");
                txt_Name.Text = string.Empty;
                txt_Mobile.Text = string.Empty;
                txt_account.Text = string.Empty;
                divremitter.Visible = true;
                divremitterreg.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Customer is registered successfully');", true);
            }
            else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "700")
            {
                divregotp.Visible = false;
                btn_remiitervalidate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
            }
            else
            {
                divremitter.Visible = true;
                divremitterreg.Visible = true;
                divregotp.Visible = false;
                btn_remiitervalidate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
            }
        }
    }
    #endregion

    #region btn_customer_verifyotp 
    protected void btn_customer_verifyotp(object sender, EventArgs e)
    {
        String Result = string.Empty;
        string remitterid = Session["remitteridv"].ToString();
        string Responsecode = Session["Responsecode"].ToString();
        string mobile = txt_verifycus.Text.Trim();
        if (txt_verifycus.Text.Trim() == string.Empty)
        {
            mobile = txt_verifycus.Text = string.Empty;
        }
        string otp = txt_cusotp.Text.Trim();
        Result = yesdmr.cusotpverify(AID.ToString(), OP.ToString(), ST.ToString(), remitterid, mobile, otp, Responsecode, customerequest);
        if (Result != string.Empty)
        {
            DataSet ds = new DataSet();
            ds = Deserialize(Result);
            if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
            {
                Cls.select_data_dt("Exec SET_yesmoney_Remiter_Reg '" + ViewState["MemberId"].ToString() + "','" + txt_Mobile.Text.Trim() + "','" + txt_Name.Text.Trim() + "','" + txt_account.Text.Trim() + "','" + Session["remitteridv"].ToString() + "'");
                txt_Name.Text = string.Empty;
                txt_Mobile.Text = string.Empty;
                txt_account.Text = string.Empty;
                divremitter.Visible = true;
                divremitterreg.Visible = true;
                Panel_Del.Attributes.Add("style", "display:none;");
                mp_DelOtp.Hide();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Customer Verify successfully');", true);
            }
            else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "700")
            {
                divregotp.Visible = false;
                btn_remiitervalidate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
            }
            else
            {
                divremitter.Visible = true;
                btn_Reg.Visible = true;
                divremitterreg.Visible = true;
                divregotp.Visible = false;
                btn_remiitervalidate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
            }
        }
    }
    #endregion

    #region Beneficiary otp Verify 
    protected void btn_remterotp_Click(object sender, EventArgs e)
    {
        String Result = string.Empty;
        string remitterid = Session["BENE_ID"].ToString();
        string Responsecode = Session["RESPONSE_CODE"].ToString();
        string mobile = ViewState["Log_Mobile"].ToString();
        string otp = txt_remotp.Text.Trim();
        Result = yesdmr.cusotpverify(AID.ToString(), OP.ToString(), ST.ToString(), remitterid, mobile, otp, Responsecode, request);
        if (Result != string.Empty)
        {
            DataSet ds = new DataSet();
            ds = Deserialize(Result);
            if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
            {
                FillBeni();
                ResetBeni();
                btn_remterotp.Visible = false;
                divremotp.Visible = false;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);

            }
            else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "700")
            {
                divregotp.Visible = false;
                btn_remiitervalidate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');window.location ='DashBoard.aspx';", true);
            }
            else
            {
                divregotp.Visible = false;
                btn_remiitervalidate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
            }
        }
    }
    #endregion

    #region  Otp Generater 
    protected void btn_customerverify_Click(object sender, EventArgs e)
    {
        String Result = string.Empty;
        string remitterid = Session["remitteridv"].ToString();
        string mobile = txt_Mobile.Text.Trim();
        if (txt_Mobile.Text.Trim() == string.Empty)
        {
            mobile = txt_loginID.Text = string.Empty;
        }
        string otp = txt_remitterotp.Text.Trim();
        Result = yesdmr.CustomerVerifyotp(remitterid, mobile, otp, AID.ToString(), OP.ToString(), ST.ToString(), request.ToString());
        if (Result != string.Empty)
        {
            DataSet ds = new DataSet();
            ds = Deserialize(Result);
            if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
            {
                Cls.select_data_dt("Exec SET_yesmoney_Remiter_Reg '" + ViewState["MemberId"].ToString() + "','" + txt_Mobile.Text.Trim() + "','" + txt_Name.Text.Trim() + "','" + txt_account.Text.Trim() + "','" + Session["remitteridv"].ToString() + "'");
                txt_Name.Text = string.Empty;
                txt_Mobile.Text = string.Empty;
                txt_account.Text = string.Empty;
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
                divregotp.Visible = false;
                btn_remiitervalidate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
            }
        }
    }
    #endregion

    #region  gv_Benificerydetails_RowCommand
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
                    string benid = Convert.ToString(e.CommandArgument);
                    Result = yesdmr.deletebenigenerateotp(OP.ToString(), ST.ToString(), AID.ToString(), ViewState["Log_Mobile"].ToString(), benidelete);
                    if (Result != string.Empty)
                    {
                        DataSet ds = Deserialize(Result);
                        if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
                        {

                            ViewState["responsecode"] = null;
                            ViewState["responsecode"] = ds.Tables[0].Rows[0]["RESPONSE_CODE"].ToString();
                            ViewState["DelBeniId"] = null;
                            ViewState["DelBeniId"] = benid;
                            Panel_Del.Attributes.Add("style", "display:block;");
                            mp_DelOtp.Show();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                        }
                    }
                }
                else if (e.CommandName == "imps")
                {
                    string Index = Convert.ToString(e.CommandArgument);
                    DataTable tblFiltered = dtbenidetail.AsEnumerable().Where(row => row.Field<String>("BENE_ID") == Index).CopyToDataTable();
                    ViewState["trabeniid"] = null;
                    ViewState["trabeniid"] = Index;
                    txt_Acno.Text = tblFiltered.Rows[0]["BANK_ACCOUNTNO"].ToString();
                    txt_Acno.Enabled = false;
                    txt_Ifsccode.Text = tblFiltered.Rows[0]["BANKIFSC_CODE"].ToString();
                    txt_Ifsccode.Enabled = false;
                    txt_Beniname.Text = tblFiltered.Rows[0]["BENE_NAME"].ToString();
                    txt_Beniname.Enabled = false;
                    // Button2.Visible = false;
                    chkselectifsc.Visible = false;
                    ddl_choosebank.SelectedValue = tblFiltered.Rows[0]["BANKIFSC_CODE"].ToString();
                    ddl_choosebank.Enabled = false;
                    ddl_action.Enabled = false;
                    btn_SendAmount.Visible = true;
                    btn_Addbeni.Visible = false;
                    btn_dmrotp.Visible = false;
                    divtransferamount.Visible = true;
                    diventerotp.Visible = false;
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
    #endregion

    protected void btnaddbenecheck_Click(object sender, EventArgs e)
    {
        ResetBeni();
    }
    #region selectifscchanged
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

    #region add benificial
    protected void btn_Addbeni_Click(object sender, EventArgs e)
    {
        try
        {
            string Result = string.Empty;
            DataTable dt = Cls.select_data_dt(@"select * from tbl_Yesmoney_Beni_Reg where  beni_ac='" + txt_Acno.Text.Trim() + "' and beni_ifsc='" + txt_Ifsccode.Text.Trim() + "' and remitId='" + ViewState["remitId"].ToString() + "' and isactive=1");
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Beneficiary is already registred');", true);
                FillBeni();
                ResetBeni();
            }
            else
            {
                Result = yesdmr.Beneficiary_Registration(AID.ToString(), OP.ToString(), ST.ToString(), txt_Beniname.Text.Trim(), ViewState["Log_Mobile"].ToString(), txt_Ifsccode.Text.Trim(), txt_Acno.Text.Trim());
                if (Result != string.Empty)
                {
                    DataSet ds = Deserialize(Result);
                    if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
                    {
                        ViewState["remitId"] = null;
                        ViewState["BENE_ID"] = ds.Tables[0].Rows[0]["BENE_ID"].ToString();
                        Session["BENE_ID"] = ds.Tables[0].Rows[0]["BENE_ID"].ToString();
                        Session["RESPONSE_CODE"] = ds.Tables[0].Rows[0]["RESPONSE_CODE"].ToString();
                        divremotp.Visible = true;
                        btn_remterotp.Visible = true;
                        btn_Addbeni.Visible = false;
                        // ResetBeni();
                        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    }
                    else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "420")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
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
    #endregion

    #region list benifical
    private void FillBeni()
    {
        try
        {
            string Result = string.Empty;
            Result = yesdmr.listbenifical(ViewState["Log_Mobile"].ToString(), AID.ToString(), OP.ToString(), ST.ToString());
            if (Result != string.Empty)
            {
                DataSet ds = Deserialize(Result);
                if (ds.Tables.Contains("BENEFICIARY_DATA"))
                {
                    if (ds.Tables["BENEFICIARY_DATA"].Rows.Count > 0)
                    {
                        rpt_benificiary.DataSource = ds.Tables["BENEFICIARY_DATA"];
                        rpt_benificiary.DataBind();
                        Session.Add("benidetail", ds.Tables["BENEFICIARY_DATA"]);
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
        //Button2.Visible = true;
        diventerotp.Visible = false;
        btn_Addbeni.Visible = true;
        txt_DelOtp.Text = string.Empty;
        txt_dmrotp.Text = string.Empty;
        txt_remitterotp.Text = string.Empty;
    }
    #endregion

    #region delete beni otp
    protected void btn_BeniDelOtpSub_Click(object sender, EventArgs e)
    {
        try
        {
            mp_DelOtp.Hide();
            string Result = string.Empty;
            Result = yesdmr.Beneficiarydelete(OP.ToString(), ST.ToString(), AID.ToString(), ViewState["Log_Mobile"].ToString(), ViewState["DelBeniId"].ToString(), ViewState["responsecode"].ToString(), txt_DelOtp.Text.Trim());
            if (Result != string.Empty)
            {
                DataSet ds = Deserialize(Result);
                if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                    Panel_Del.Attributes.Add("style", "display:none;");
                    mp_DelOtp.Hide();
                    FillBeni();
                    ResetBeni();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                    Panel_Del.Attributes.Add("style", "display:block;");
                    mp_DelOtp.Hide();
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
    #endregion

    #region search new customer
    protected void btn_loginnew_customer(object sender, EventArgs e)
    {
        try
        {
            String Result = string.Empty;
            Result = yesdmr.SearchCustomer(txt_changerem.Text.Trim(), AID.ToString(), OP.ToString(), ST.ToString());
            //  ResetBeni();
            if (Result != string.Empty)
            {
                DataSet ds = new DataSet();
                ds = Deserialize(Result);
                if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "700")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                    divremitter.Visible = true;
                    divremitterreg.Visible = true;
                    divhome.Visible = true;
                    divbody.Visible = false;

                }
                else if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "500")
                {
                    divremitter.Visible = true;
                    divbody.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');", true);
                }
                else
                {
                    if (ds.Tables[0].Rows[0]["RESP_CODE"].ToString() == "200")
                    {

                        ViewState["Log_Mobile"] = null;
                        ViewState["Log_Mobile"] = txt_loginID.Text.Trim();
                        ViewState["remitId"] = null;
                        ViewState["remitId"] = ds.Tables["BENEFICIARY_DATA"].Rows[0]["BENE_ID"].ToString();
                        Session["remitId"] = ds.Tables["BENEFICIARY_DATA"].Rows[0]["BENE_ID"].ToString();
                        lbl_Remname.Text = ds.Tables["DATA"].Rows[0]["SEDNER_FNAME"].ToString();
                        lbl_Remmno.Text = ds.Tables["DATA"].Rows[0]["SENDER_MOBILENO"].ToString();
                        lbl_Consumed.Text = ds.Tables["DATA"].Rows[0]["SENDER_AVAILBAL"].ToString();
                        lbl_reming.Text = ds.Tables["DATA"].Rows[0]["SENDER_MONTHLYBAL"].ToString();
                        rpt_benificiary.DataSource = ds.Tables["BENEFICIARY_DATA"];
                        rpt_benificiary.DataBind();
                        Session.Add("benidetail", ds.Tables["BENEFICIARY_DATA"]);
                        divremitter.Visible = false;
                        divremitterreg.Visible = false;
                        divhome.Visible = false;
                        divbody.Visible = true;

                    }
                    else if (ds.Tables[0].Rows[0]["statuscode"].ToString() == "EZX")
                    {
                        divbody.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "');window.location ='DashBoard.aspx';", true);
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
    #endregion

    #region  fund transfer
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
                        if (Convert.ToInt32(txt_Amount.Text.Trim()) <= 30000)
                        {
                            DataTable dtbenidetail = (DataTable)Session["benidetail"];
                            DataTable tblFiltered = dtbenidetail.AsEnumerable().Where(row => row.Field<String>("BENE_ID") == ViewState["trabeniid"].ToString()).CopyToDataTable();
                            string bundleid = Clsm.Cyrus_GetTransactionID_New();
                            int totaltra;
                            string kyctype = "KYC";
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
                                    TraAmt = Convert.ToDecimal(TraAmt);
                                double NetAmount = Convert.ToDouble(30000);
                                if (NetAmount > Convert.ToDouble(TraAmt))
                                {
                                   // int res = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(NetAmount), Convert.ToInt32(ViewState["MsrNo"]));
                                    int res = 1;
                                    if (res == 1)
                                    {
                                        string ETranId = string.Empty;
                                        ETranId = Clsm.Cyrus_GetTransactionID_New();
                                        // int tra = Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal("-" + NetAmount.ToString().Trim()), "Dr", "DMR Topup Txn:- " + ETranId + "");
                                        int tra = 1;
                                        if (tra > 0)
                                        {
                                            Result = yesdmr.Fund_Transfer(OP.ToString(), ST.ToString(), AID.ToString(), ViewState["Log_Mobile"].ToString(), ViewState["Log_Mobile"].ToString(), ViewState["trabeniid"].ToString(), ETranId, Convert.ToDecimal(TraAmt), kyctype, "IMPS", "100", "", tblFiltered.Rows[0]["BENE_NAME"].ToString(), tblFiltered.Rows[0]["BANKIFSC_CODE"].ToString(), tblFiltered.Rows[0]["BENE_BANKNAME"].ToString(), tblFiltered.Rows[0]["BANK_ACCOUNTNO"].ToString());
                                            if (Result != string.Empty)
                                            {
                                                ds = Deserialize(Result);

                                                if (ds.Tables.Contains("root") && ds.Tables["root"].Columns.Contains("RESP_sCODE"))
                                                {
                                                    if (ds.Tables["root"].Rows[0]["RESP_CODE"].ToString() == "EZX")
                                                    {
                                                       // Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables[0].Rows[0]["status"].ToString() + "');", true);
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    if (ds.Tables["root"].Rows[0]["RESP_CODE"].ToString() == "300")
                                                    {
                                                        Session["chdmtOTP"] = null;
                                                        Session["txtamount"] = null;
                                                        List<ParmList> _lstparm = new List<ParmList>();
                                                        _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                        _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["REQUEST_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["REQUEST_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["PAID_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["TRANSFER_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESP_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESPONSE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["BENE_NAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["BANK_ACCOUNTNO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["BANKIFSC_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                        _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["BENE_BANKNAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                        // _lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                     //   Cls.select_data_dtNew("Set_YesMoney_Fund_Tra", _lstparm);
                                                        // Cls.select_data_dt(@"EXEC SET_DIST_Commission '" + ViewState["MemberId"].ToString() + "','" + TraAmt + "','" + ETranId + "','dmr'");
                                                        RemainAmt = totalamt - TraAmt;
                                                        TraAmt = RemainAmt;
                                                        totalamt = RemainAmt;
                                                    }
                                                    else if (ds.Tables["root"].Rows[0]["RESP_CODE"].ToString() == "350")
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
                                                       // Cls.select_data_dtNew("Set_YesMoney_Fund_Tra", _lstparm);
                                                        //Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                        break;
                                                    }
                                                    else if (ds.Tables["root"].Rows[0]["RESP_CODE"].ToString() == "302")
                                                    {
                                                        List<ParmList> _lstparm = new List<ParmList>();
                                                        Session["chdmtOTP"] = null;
                                                        Session["txtamount"] = null;
                                                        _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ViewState["remitId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                        _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["REQUEST_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["PAID_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["TRANSFER_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESP_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESPONSE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["BENE_NAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["BANK_ACCOUNTNO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                        _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["BANKIFSC_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["BENE_BANKNAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                        //_lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                       // Cls.select_data_dtNew("Set_YesMoney_Fund_Tra", _lstparm);
                                                        break;
                                                    }
                                                    else if (ds.Tables["root"].Rows[0]["RESP_CODE"].ToString() == "420")
                                                    {
                                                        if (ds.Tables["root"].Rows[0]["RESPONSE"].ToString() == "PENDING")
                                                        {
                                                            List<ParmList> _lstparm = new List<ParmList>();
                                                            Session["chdmtOTP"] = null;
                                                            Session["txtamount"] = null;
                                                            _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@agent_id", value = ViewState["remitId"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                            _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                            _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["REQUEST_REFERENCE_NO"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["PAID_AMOUNT"].ToString()) });
                                                            _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["TRANSFER_AMOUNT"].ToString()) });
                                                            _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESP_CODE"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESPONSE"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                            _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["BENE_NAME"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["BANK_ACCOUNTNO"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                            _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["BANKIFSC_CODE"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["BENE_BANKNAME"].ToString() });
                                                            _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                            //_lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                           // Cls.select_data_dtNew("Set_YesMoney_Fund_Tra", _lstparm);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                          //  Clsm.Wallet_MakeTransaction(ViewState["MemberId"].ToString(), Convert.ToDecimal(NetAmount), "Cr", "DMR Fail Txn:- " + ETranId + "");
                                                            break;
                                                        }
                                                    }
                                                    else if (ds.Tables["root"].Rows[0]["RESP_CODE"].ToString() == "656")
                                                    {
                                                        List<ParmList> _lstparm = new List<ParmList>();
                                                        Session["chdmtOTP"] = null;
                                                        Session["txtamount"] = null;
                                                        _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ViewState["remitId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                        _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["REQUEST_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["PAID_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["TRANSFER_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESP_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESPONSE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["BENE_NAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["BANK_ACCOUNTNO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                        _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["BANKIFSC_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["BENE_BANKNAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                        //_lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                       // Cls.select_data_dtNew("Set_YesMoney_Fund_Tra", _lstparm);
                                                        break;
                                                    }
                                                    else if (ds.Tables["root"].Rows[0]["RESP_CODE"].ToString() == "420")
                                                    {
                                                        List<ParmList> _lstparm = new List<ParmList>();
                                                        Session["chdmtOTP"] = null;
                                                        Session["txtamount"] = null;
                                                        _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ViewState["remitId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                        _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["REQUEST_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["PAID_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["TRANSFER_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESP_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESPONSE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["BENE_NAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["BANK_ACCOUNTNO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                        _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["BANKIFSC_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["BENE_BANKNAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                        //_lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                      //  Cls.select_data_dtNew("Set_YesMoney_Fund_Tra", _lstparm);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        List<ParmList> _lstparm = new List<ParmList>();
                                                        Session["chdmtOTP"] = null;
                                                        Session["txtamount"] = null;
                                                        _lstparm.Add(new ParmList() { name = "@remit_mobile", value = ViewState["Log_Mobile"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_id", value = ViewState["trabeniid"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ViewState["remitId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(TraAmt) });
                                                        _lstparm.Add(new ParmList() { name = "@mode", value = "IMPS" });
                                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["EP_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["REQUEST_REFERENCE_NO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@charged_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["PAID_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@locked_amt", value = Convert.ToDecimal(ds.Tables["TRANSACTION_DETAILS"].Rows[0]["TRANSFER_AMOUNT"].ToString()) });
                                                        _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESP_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables["TRANSACTION_DETAILS"].Rows[0]["RESPONSE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = ETranId });
                                                        _lstparm.Add(new ParmList() { name = "@Memberid", value = ViewState["MemberId"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_name", value = tblFiltered.Rows[0]["BENE_NAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@beni_account", value = tblFiltered.Rows[0]["BANK_ACCOUNTNO"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@netamount", value = Convert.ToDecimal(NetAmount) });
                                                        _lstparm.Add(new ParmList() { name = "@beni_ifsc", value = tblFiltered.Rows[0]["BANKIFSC_CODE"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@bank", value = tblFiltered.Rows[0]["BENE_BANKNAME"].ToString() });
                                                        _lstparm.Add(new ParmList() { name = "@budleid", value = bundleid });
                                                        //_lstparm.Add(new ParmList() { name = "@adminsurcharge", value = Convert.ToDecimal(ds.Tables["root"].Rows[0]["adminsurcharge"].ToString()) });
                                                      //  Cls.select_data_dtNew("Set_YesMoney_Fund_Tra", _lstparm);
                                                        break;
                                                    }
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
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["root"].Rows[0]["RESP_MSG"].ToString() + "');", true);
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your seesion is time out, please login agarin!');window.location ='yesbankdmr.aspx';", true);
                    }
                }
                else
                {
                    ResetControl();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You have tried to change amount,otp is not valid for this transaction amount');window.location ='yesbankdmr.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('OTP is not valid for this transaction');", true);
            }
        }
        catch (Exception ex)
        {
            Cls.select_data_dt(@"insert into Yesmoney_error values ('Transcation','" + Result + "," + DateTime.Now.ToString() + "')");
            ResetControl();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');", true);
        }


    }
    #endregion

    #region  send mail and sms
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
        "Dear Customer, you need an OTP  to Yes Bank DMT Transaction for Rs.@v0@ and OTP is @v1@. Never Share it with anyone.Bank Never calls to verify it."//1
    };
    #endregion

    #region commision
    public double TotupAmount(double amount, string memberid)
    {
        double NetAmount = 0;
        //double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        //if (amount > 0)
        //{
        //    DataTable dtsr = new DataTable();
        //    cls_connection cls = new cls_connection();
        //    DataTable dtmembers = new DataTable();
        //    dtmembers = (DataTable)Session["dtDistributor"];
        //    DataTable dtMemberMaster = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='chk', @msrno=" + Convert.ToInt32(dtmembers.Rows[0]["MsrNo"]) + "");
        //    string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
        //    dtsr = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='instantsur',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
        //    if (dtsr.Rows.Count > 0)
        //    {
        //        surcharge_rate = Convert.ToDouble(dtsr.Rows[0]["surcharge"].ToString());
        //        isFlat = Convert.ToInt32(dtsr.Rows[0]["isflat"].ToString());
        //        if (surcharge_rate > 0)
        //        {
        //            if (isFlat == 0)
        //                surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
        //            else
        //                surcharge_amt = surcharge_rate;
        //        }
        //        NetAmount = amount + surcharge_amt;
        //    }
        //    else
        //    {
        //        NetAmount = 0;
        //    }
        //}
        //else
        //{
        //    NetAmount = 0;
        //}
        return NetAmount;
    }
    #endregion
}