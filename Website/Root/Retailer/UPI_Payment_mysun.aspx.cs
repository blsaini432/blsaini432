using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web.UI;
using System.Xml;

public partial class Root_Retailer_UPI_Payment_mysun : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtRetailer"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataTable dtMember = (DataTable)Session["dtRetailer"];
                    dtMemberMaster = objMemberMaster.ManageMemberMaster("Get", Convert.ToInt32(Session["RetailerMsrNo"]));
                    int msrno = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]);
                    dt = Cls.select_data_dt(@"exec Set_EzulixDmr @action='utigateway', @msrno=" + msrno + "");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["utigateway"]) == true)
                        {
                            ViewState["MemberId"] = null;
                            ViewState["MsrNo"] = null;
                            ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                            Session["TransactionPassword"] = dtMember.Rows[0]["TransactionPassword"];
                            Session["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                            ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                            ViewState["dmtmobile"] = dtMember.Rows[0]["Mobile"].ToString();

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('UTI Gateway service Not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('UTI Gateway service Not active');window.location ='DashBoard.aspx';", true);
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
        string Out = String.Empty;
        cls_myMember clsm = new cls_myMember();
        DataTable dtMember = (DataTable)Session["dtRetailer"];
        string Mobile = dtMember.Rows[0]["Mobile"].ToString();
        string amount = txt_Amount.Text;
        string TxnID = clsm.Cyrus_GetTransactionID_New();
        string token = "Vq6BphVCp17sLEkXd8ezBzh3GT6Fgp";
        string StartID = "API713068";
        string UserID = dtMember.Rows[0]["Memberid"].ToString();
        string Json = "{\"amount\": \"" + amount + "\",\"UserID\":\"" + UserID + "\",\"Tokenkey\":\"" + token + "\",\"StartID\":\"" + StartID + "\",\"TxnID\":\"" + TxnID + "\"}";
        string Url = "https://payu.startrecharge.in/QRCollect/Payment";
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
        httpWebRequest.Method = "POST";
        httpWebRequest.Accept = "application/json";
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Timeout = 100000;
        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(Json);
        }
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            Out = streamReader.ReadToEnd();
        }
        string myresponse = Out.ToString();

        DataSet ds = Deserialize(myresponse);
        if (ds.Tables["root"].Rows[0]["Statuscode"].ToString() == "1")
        {
            if (ds.Tables["root"].Rows[0]["Status"].ToString() == "Success")
            {
                //  string Customer_ID = ds.Tables["root"].Rows[0]["CustomerID"].ToString();
                string QRID = ds.Tables["root"].Rows[0]["qrcodeID"].ToString();
                string QrString = ds.Tables["root"].Rows[0]["qrData"].ToString();
                string image = ds.Tables["root"].Rows[0]["image"].ToString();
                int MsrNo = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]);
                int MemberTypeID = Convert.ToInt32(dtMember.Rows[0]["MemberTypeID"]);
                string MemberID = dtMember.Rows[0]["MemberID"].ToString();
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@MemberId", value = MemberID });
                _lstparm.Add(new ParmList() { name = "@MemberTypeID", value = MemberTypeID });
                _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
                _lstparm.Add(new ParmList() { name = "@amount", value = amount });
                _lstparm.Add(new ParmList() { name = "@client_vpa", value = "" });
                _lstparm.Add(new ParmList() { name = "@Statuss", value = "Pending" });
                _lstparm.Add(new ParmList() { name = "@mobile", value = dtMember.Rows[0]["Mobile"] });
                _lstparm.Add(new ParmList() { name = "@email", value = dtMember.Rows[0]["Email"] });
                _lstparm.Add(new ParmList() { name = "@mode", value = "Paytm Bank" });
                _lstparm.Add(new ParmList() { name = "@client_name", value = dtMember.Rows[0]["Firstname"].ToString() });
                _lstparm.Add(new ParmList() { name = "@City", value = dtMember.Rows[0]["CityName"] });
                //  _lstparm.Add(new ParmList() { name = "@pincode", value = "" });
                _lstparm.Add(new ParmList() { name = "@Addresss", value = dtMember.Rows[0]["Address"] });
                _lstparm.Add(new ParmList() { name = "@client_txn_id", value = TxnID });
                _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparm);
                QRCODE.Visible = true;
                ViewState["QrString"] = "data:image/gif;base64," + image;
                Session["TxnID"] = TxnID.ToString();
                upi.Visible = false;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('try Again !!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found !!');", true);
        }
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        string token = "Vq6BphVCp17sLEkXd8ezBzh3GT6Fgp";
        string StartID = "API713068";
        string Out = String.Empty;
        string TxnID = Session["TxnID"].ToString();
        DataTable orderid = Cls.select_data_dt("select * from tbl_UPI_paymentGateway where client_txn_id='" + TxnID + "' and statuss='Pending'");
        if (orderid.Rows.Count > 0)
        {
            string MemberId = orderid.Rows[0]["MemberId"].ToString();
            string MemberTypeID = orderid.Rows[0]["MemberTypeID"].ToString();
            string Json = "{\"Tokenkey\":\"" + token + "\",\"StartID\":\"" + StartID + "\",\"TxnID\":\"" + TxnID + "\"}";
            string Url = "https://payu.startrecharge.in/QRCollect/PaymentStatus";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Json);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
            string myresponse = Out.ToString();
            DataSet ds = Deserialize(myresponse);
            if (ds.Tables["root"].Rows[0]["Statuscode"].ToString() == "1")
            {
                if (ds.Tables["root"].Rows[0]["Status"].ToString() == "Success")
                {
                    string amount = ds.Tables["root"].Rows[0]["txnAmount"].ToString();
                    List<ParmList> _lstparms = new List<ParmList>();
                    _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["root"].Rows[0]["orderId"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["root"].Rows[0]["Status"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["root"].Rows[0]["Message"].ToString() });
                    _lstparms.Add(new ParmList() { name = "@client_txn_id", value = TxnID });
                    _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                    Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                    int balance = Clsm.Wallet_MakeTransaction_Ezulix(MemberId, Convert.ToDecimal(amount), "Cr", "Add Fund UPI PG Order ID : " + TxnID);
                    Session["TxnID"] = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Success');window.location ='UPI_Payment_Report.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment Status Pending. try Again');window.location ='DashBoard.aspx';", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found !!');", true);
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

}