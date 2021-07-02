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
public partial class Root_Distributor_PrepaidCardPanel : System.Web.UI.Page
{
    #region globaldecalarations
    public static int msrno { get; set; }
    #endregion

    #region Access_Class
    InstantPrepaidCard ePrepaidcardnew = new InstantPrepaidCard();
    cls_connection Cls = new cls_connection();
    #endregion

    #region Pageload
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                DataTable dt = new DataTable();
                DataTable dtMember = (DataTable)Session["dtDistributor"];
                dt = Cls.select_data_dt("select * from tblmlm_membermaster where msrno=" + Convert.ToInt32(dtMember.Rows[0]["MsrNo"]) + "");
                if (dt.Rows.Count > 0)
                {

                    Session["MsrNo"] = msrno;
                    Session["MemberId"] = dt.Rows[0]["MemberId"].ToString();

                }
            }

        }
    }
    #endregion

    #region methods

    public class UploadDoc
    {
        public string Doctype { get; set; }
        public string image { get; set; }
        public string filename { get; set; }
    }

    static string base64String = null;

    public void ImageToBase64(string UploadImg)
    {
        string path = UploadImg;
        byte[] imageBytes = File.ReadAllBytes(Server.MapPath(path));
        base64String = Convert.ToBase64String(imageBytes);
        UploadDoc doc = new UploadDoc();
        ViewState["image"] = null;
        ViewState["image"] = base64String;

    }

    public System.Drawing.Image Base64ToImage()
    {
        byte[] imageBytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        ms.Write(imageBytes, 0, imageBytes.Length);
        System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        return image;
    }
    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    public string ConvertDataTabletoString(DataTable dt)
    {
        string Json = JsonConvert.SerializeObject(dt);
        return Json;
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
    #endregion



    #region customerlogin
    protected void btn_CustomerLogin_Click(object sender, EventArgs e)
    {
        string mobile = txt_uMobile.Text.Trim();
        string email = txt_email.Text.Trim();
        Session["cardmobile"] = txt_uMobile.Text.Trim();
        Session["cardemail"] = txt_email.Text.Trim();
        Session["cardpan"] = txt_PAN.Text.Trim();

        Session["cardscn"] = txt_cardlastdigit.Text.Trim();
        Session["cardkit"] = txt_cardkitnumber.Text.Trim();
        string result = ePrepaidcardnew.genrateotp(mobile, email);
        DataSet dsresult = new DataSet();
        dsresult = Deserialize(result);
        if (dsresult.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
        {
           ModalPopupExtender1.Show();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('OTP Sent Successfully');", true);
        }
        else
        {
            ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsresult.Tables[0].Rows[0]["status"].ToString() + "');", true);
        }
    }
    #endregion

    #region uploadkyc
    protected void btn_uploadkyc_Click(object sender, EventArgs e)
    {
        string mobile = txt_kycmobile.Text.Trim();
        string pan = txt_kycpannumber.Text.Trim();
        if (mobile != "" && pan != "")
        {
            if (fup_aadhar.HasFile)
            {
                try
                {
                    if (fup_aadhar.PostedFile.ContentType == "application/pdf")
                    {
                        if (fup_aadhar.PostedFile.ContentLength <= 2072000)
                        {
                            string filename = DateTime.Now.Ticks + Path.GetFileName(fup_aadhar.FileName);
                            fup_aadhar.SaveAs(Server.MapPath("../../Uploads/PrepaidCard/") + filename);
                            string DomainName = HttpContext.Current.Request.Url.Host;
                            string filepath = "../../Uploads/PrepaidCard/" + filename;
                            string link = "http://" + DomainName + "/Uploads/PrepaidCard/" + filename;
                            string result = ePrepaidcardnew.uploadkyc(mobile, pan, link, filename);

                            DataSet dsresult = new DataSet();
                            dsresult = Deserialize(result);
                            if (dsresult.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                            {
                                List<ParmList> _lstparm = new List<ParmList>();
                                _lstparm.Add(new ParmList() { name = "@name", value = "" });
                                _lstparm.Add(new ParmList() { name = "@mobile", value = mobile });
                                _lstparm.Add(new ParmList() { name = "@email", value = "" });
                                _lstparm.Add(new ParmList() { name = "@pan", value = pan });
                                _lstparm.Add(new ParmList() { name = "@filename", value = filename });
                                _lstparm.Add(new ParmList() { name = "@MemberId", value = Session["MemberId"].ToString() });
                                _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(Session["Msrno"]) });
                                _lstparm.Add(new ParmList() { name = "@cardscn", value = "" });
                                _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                                _lstparm.Add(new ParmList() { name = "@filepath", value = link });
                                Cls.select_data_dtNew("Proc_Instant_Paypreaidcardkyc", _lstparm);
                               
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsresult.Tables[0].Rows[0]["status"].ToString() + "');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsresult.Tables[0].Rows[0]["status"].ToString() + "');", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('The pdf has to be less than or equal 2 MB!')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only Upload pdf file');", true);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                    //  Cls.select_data_dt("insert into pptest values('" + ex.ToString() + "')");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(" + ex.ToString() + ");", true);
                }
            }

        }

    }
    #endregion

    #region Activatecard
    protected void btn_activatecard_Click(object sender, EventArgs e)
    {
        string mobile = txt_acocuntmobile.Text.Trim();
        string pan = txt_accountpan.Text.Trim();
        if (mobile != "" && pan != "")
        {
            string result = ePrepaidcardnew.accountstatus(mobile, pan);
            DataSet dsresult = new DataSet();
            dsresult = Deserialize(result);
            if (dsresult.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
            {
                card_status.Visible = true;
                if (dsresult.Tables.Contains("data"))
                {
                    string name = dsresult.Tables[1].Rows[0]["name"].ToString();
                    string mobiles = dsresult.Tables[1].Rows[0]["mobile"].ToString();
                    string email = dsresult.Tables[1].Rows[0]["email"].ToString();
                    lblcardholder_name.Text = name;
                    lblchk_accountmobile.Text = mobiles;
                    lblaccount_email.Text = email;
                }
                if (dsresult.Tables.Contains("cards"))
                {
                    string cardtype = dsresult.Tables["cards"].Rows[0]["card_type"].ToString();
                    string card_network = dsresult.Tables["cards"].Rows[0]["card_network"].ToString();
                    string card_statuss = dsresult.Tables["cards"].Rows[0]["card_status"].ToString();
                    string card_bin = dsresult.Tables["cards"].Rows[0]["card_bin"].ToString();
                    string atm_status = dsresult.Tables["cards"].Rows[0]["atm_status"].ToString();
                    string kit_number = dsresult.Tables["cards"].Rows[0]["kit_number"].ToString();
                    string scn = dsresult.Tables["cards"].Rows[0]["scn"].ToString();
                    string expiry_date = dsresult.Tables["cards"].Rows[0]["expiry_date"].ToString();
                    string user_kyc_mode = dsresult.Tables["cards"].Rows[0]["user_kyc_mode"].ToString();
                    lbl_cardtype.Text = cardtype;
                    lbl_cardnetwork.Text = card_network;
                    lbl_cardbin.Text = card_bin;
                    lbl_atm_status.Text = atm_status;
                    lbl_kitnumber.Text = kit_number;
                    lbl_accountcardnumber.Text = "XXXX-XXXX-XXXX-" + scn;
                    lbl_expiraydate.Text = expiry_date;
                    lbl_userkycmode.Text = user_kyc_mode;
                    lbl_cardstatus.Text = card_statuss;
                }
            }
            else
            {
                card_status.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsresult.Tables[0].Rows[0]["status"].ToString() + "');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter valid fileds');", true);
        }
    }
    #endregion

    #region checkcardbalance
    protected void btn_checkbalance_Click(object sender, EventArgs e)
    {
        string mobile = txt_mobilechkbal.Text.Trim();
        string pan = txt_carddigitchkbal.Text.Trim();
        if (mobile != "" && pan != "")
        {
            DataSet dsresult = new DataSet();
            string result = ePrepaidcardnew.accountbalance(mobile, pan);
            dsresult = Deserialize(result);
            if (result.Contains("TXN"))
            {

                RootObjectm root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObjectm>(result);
                showbalance.Visible = true;
                lblchk_cardbalance.Text = root.data.balance;
                lblchk_cardmobile.Text = mobile;
            }
            else
            {

               showbalance.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsresult.Tables[0].Rows[0]["status"].ToString() + "');", true);
            }
        }
    }
    #endregion

    #region topupcard
    protected void btn_refill_Click(object sender, EventArgs e)
    {
        DataTable dtMember = (DataTable)Session["dtDistributor"];
        string memberid = dtMember.Rows[0]["MemberId"].ToString();
        string msrno= dtMember.Rows[0]["MsrNo"].ToString();
        cls_myMember clsm = new cls_myMember();
        string mobile = txt_Mobile.Text.Trim();
        string scn = txt_lastdigitcard.Text.Trim();
        string amount = txt_tranamount.Text.Trim();
        string pan = txt_customerpan.Text.Trim();
        string agentid = clsm.Cyrus_GetTransactionID_New();
        if (mobile != "" && scn != "" && amount != "" && Convert.ToDecimal(amount) > 0)
        {
            int cHKBALANSE = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), Convert.ToInt32(msrno));
            if (cHKBALANSE == 1)
            {
                int a = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal("-" + amount), "Dr", "Topup PrepaidCard:" + agentid + "");
                if (a == 1)
                {
                    string result = ePrepaidcardnew.accounttopup(mobile, pan, agentid, amount, scn);
                    DataSet dsresult = new DataSet();
                    dsresult = Deserialize(result);
                    if (dsresult.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
                    {
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = dsresult.Tables[1].Rows[0]["ipay_id"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@agent_id", value = agentid });
                        _lstparm.Add(new ParmList() { name = "@opr_id", value = dsresult.Tables[1].Rows[0]["opr_id"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@card_no", value = scn });
                        _lstparm.Add(new ParmList() { name = "@trans_amt", value = Convert.ToDecimal(dsresult.Tables[1].Rows[0]["amount"].ToString()) });
                        _lstparm.Add(new ParmList() { name = "@statu", value = dsresult.Tables[0].Rows[0]["status"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@statuscode", value = dsresult.Tables[0].Rows[0]["statuscode"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@msg", value = dsresult.Tables[0].Rows[0]["status"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
                        _lstparm.Add(new ParmList() { name = "@mobile", value = mobile });
                        _lstparm.Add(new ParmList() { name = "@memberid", value = memberid });
                        Cls.select_data_dtNew("Set_Ezulix_PrepaidCard", _lstparm);
                        Cls.select_data_dt(@"EXEC SET_InstantPayPrepaidCardCommission @memberid='" + memberid + "',@txnamount='" + Convert.ToDecimal(dsresult.Tables[1].Rows[0]["amount"].ToString()) + "',@txnid='" + agentid + "'");
                        txt_Mobile.Text = "";
                        txt_lastdigitcard.Text = "";
                        txt_tranamount.Text = "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Top UP Success!!!');", true);
                    }
                    else
                    {
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = "" });
                        _lstparm.Add(new ParmList() { name = "@agent_id", value = agentid });
                        _lstparm.Add(new ParmList() { name = "@opr_id", value = "00" });
                        _lstparm.Add(new ParmList() { name = "@card_no", value = scn });
                        _lstparm.Add(new ParmList() { name = "@trans_amt", value = Convert.ToDecimal(amount) });
                        _lstparm.Add(new ParmList() { name = "@statu", value = "Pending" });
                        _lstparm.Add(new ParmList() { name = "@statuscode", value = "PEN" });
                        _lstparm.Add(new ParmList() { name = "@msg", value = "Transaction Pending" });
                        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
                        _lstparm.Add(new ParmList() { name = "@mobile", value = mobile });
                        _lstparm.Add(new ParmList() { name = "@memberid", value = memberid });
                        Cls.select_data_dtNew("Set_Ezulix_PrepaidCard", _lstparm);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsresult.Tables[0].Rows[0]["status"].ToString() + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficent Wallet Balance to do topup');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficent Wallet Balance to do topup');", true);

            }
        }
    }
    #endregion

    #region classforuploadkyc

    public class Data2
    {
        public string AuthData { get; set; }
        public string LicKey { get; set; }
        public string Iv { get; set; }
        public string TransId { get; set; }
        public string aesKey { get; set; }
    }

    public class Ekyc
    {
        public string action_url { get; set; }
        public Data2 data { get; set; }
    }

    public class Data
    {
        public string token { get; set; }
        public string customer_id { get; set; }
        public int ekyc_status { get; set; }
        public string mobile { get; set; }
        public Ekyc ekyc { get; set; }
    }

    public class RootObject
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public Data data { get; set; }
    }
    #endregion

    #region classforcheckbalance
    public class Balancem
    {
        public string entityId { get; set; }
        public string productId { get; set; }
        public object yseId { get; set; }
        public string balance { get; set; }
    }

    public class Datam
    {
        public string balance { get; set; }
    }

    public class RootObjectm
    {
        public string statuscode { get; set; }
        public string status { get; set; }
        public Datam data { get; set; }
    }

    #endregion

    protected void btn_remiitervalidate_Click(object sender, EventArgs e)
    {
        string mobile = Session["cardmobile"].ToString();
        string email = Session["cardemail"].ToString();
        string pan = Session["cardpan"].ToString();
        string mobileotp = txt_mobileotp.Text.Trim();
        string emailotp = txt_emailotp.Text.Trim();
        string scn = Session["cardscn"].ToString();
        string kitnumber = Session["cardkit"].ToString();

        if (mobile != "" && mobileotp != "" && emailotp != "")
        {
            string result = ePrepaidcardnew.accountcreate(mobile, email, mobileotp, emailotp, pan, "PHYSICAL", scn, kitnumber, "26.8866", "75.7450");
            DataSet dsresult = new DataSet();
            dsresult = Deserialize(result);
            if (dsresult.Tables[0].Rows[0]["statuscode"].ToString() == "TXN")
            {
                ModalPopupExtender1.Hide();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsresult.Tables[0].Rows[0]["status"].ToString() + "');", true);
            }
            else
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + dsresult.Tables[0].Rows[0]["status"].ToString() + "');", true);
            }
        }
    }

    protected void btn_closermei_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
}