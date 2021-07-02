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

public partial class Root_Distributor_UPI_Payment_mysun : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();

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
        cls_myMember clsm = new cls_myMember();
        DataTable dtMember = (DataTable)Session["dtDistributor"];
        string Mobile = dtMember.Rows[0]["Mobile"].ToString();
        string Name = txt_name.Text;
        string CustomerID = clsm.Cyrus_GetTransactionID_New();
        string Email = dtMember.Rows[0]["Email"].ToString();
        string City = dtMember.Rows[0]["CityName"].ToString();
        string PinCode = txt_upi.Text;
        string Address = dtMember.Rows[0]["Address"].ToString();
        string token = "Vq6BphVCp17sLEkXd8ezBzh3GT6Fgp";
        string parameter = "token=" + token + "&CustomerID=" + CustomerID + "&Name=" + Name + "&Mobile=" + Mobile + "&Email=" + Email + "&City=" + City + "&PinCode=" + PinCode + "&Address=" + Address;
        string url = "http://payu.startrecharge.in/QRCode/AccountGenerate" + "?" + parameter;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        WebRequest request = WebRequest.Create(url);
        request.Credentials = CredentialCache.DefaultCredentials;
        WebResponse response = request.GetResponse();
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);
        using (Stream dataStream = response.GetResponseStream())
        {
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            //  clsiapplog.PrintLog("APIRequest(KYC) : " + responseFromServer + "");
            DataSet ds = Deserialize(responseFromServer);
            if (ds.Tables["root"].Rows[0]["Error"].ToString() == "200")
            {
                if (ds.Tables["root"].Rows[0]["Status"].ToString() == "Success")
                {
                    string Customer_ID = ds.Tables["data"].Rows[0]["CustomerID"].ToString();
                    string QRID = ds.Tables["data"].Rows[0]["QRID"].ToString();
                    string QrString = ds.Tables["data"].Rows[0]["QrString"].ToString();
                    string MerchantVPA = ds.Tables["data"].Rows[0]["MerchantVPA"].ToString();
                    int MsrNo = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]);
                    int MemberTypeID = Convert.ToInt32(dtMember.Rows[0]["MemberTypeID"]);
                    string MemberID = dtMember.Rows[0]["MemberID"].ToString();
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@MemberId", value = MemberID });
                    _lstparm.Add(new ParmList() { name = "@MemberTypeID", value = MemberTypeID });
                    _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
                    _lstparm.Add(new ParmList() { name = "@amount", value = 0});
                    _lstparm.Add(new ParmList() { name = "@client_vpa", value = "" });
                    _lstparm.Add(new ParmList() { name = "@Statuss", value = "Pending" });
                    _lstparm.Add(new ParmList() { name = "@mobile", value = dtMember.Rows[0]["Mobile"] });
                    _lstparm.Add(new ParmList() { name = "@email", value = dtMember.Rows[0]["Email"] });
                    _lstparm.Add(new ParmList() { name = "@client_name", value = Name });
                    _lstparm.Add(new ParmList() { name = "@mode", value = "HDFC Bank" });
                    _lstparm.Add(new ParmList() { name = "@City", value = dtMember.Rows[0]["CityName"] });
                    _lstparm.Add(new ParmList() { name = "@pincode", value = PinCode });
                    _lstparm.Add(new ParmList() { name = "@Addresss", value = dtMember.Rows[0]["Address"] });
                    _lstparm.Add(new ParmList() { name = "@client_txn_id", value = Customer_ID });
                    _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                    Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparm);
                    QRCODE.Visible = true;
                    ViewState["QrString"] = QrString;
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
       
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
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