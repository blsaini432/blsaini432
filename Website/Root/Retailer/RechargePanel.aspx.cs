using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Net;
using System.IO;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using WMPLib;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

public partial class Root_Retailer_RechargePanel : System.Web.UI.Page
{
    clsRecharge_Earning objEarning = new clsRecharge_Earning();
    DataTable dtEarning = new DataTable();
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();
    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();
    clsRecharge_Circle objCircle = new clsRecharge_Circle();
    DataTable dtCircle = new DataTable();
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();
    cls_connection objconnection = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    DataTable dtMemberMaster = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindbanner();
            DataTable dt = new DataTable();
            DataTable dtMember = (DataTable)Session["dtRetailer"];
            dt = objconnection.select_data_dt(@"EXEC Set_EzulixDmr @action='recharge', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["isrecharge"]) == true)
                {
                    DropDownList ddlElectricity = new DropDownList();
                    DropDownList ddlGas = new DropDownList();
                    clsm.FillDDL_Operator(ddlOperatorPrepaid, ddlOperatorDTH, ddlDataCard, ddlOperatorPostpaid, ddlLandlineOperator, ddlElectricity, ddlGas);
                    clsm.FillDDL_Circle(ddlCirclePrepaid, ddlCircleDatacard, ddlCirclePostpaid, ddlCircleLandline);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Recharge Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                }
            }
        }
    }
    #region bind
    public void bindbanner()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners where isactive=1 and ServiceName='Recharge' order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }

    }
    #endregion
    protected void btn_ProceedPrepaid_Click(object sender, EventArgs e)
    {
        string ss = "<table class='table table-bordered table-hover tblRequestConfirm'>";
        ss += "<tr><td>Mobile Number</td><td>" + txtNumberPrepaid.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Operator Name</td><td>" + ddlOperatorPrepaid.SelectedItem.Text.ToString() + "</td></tr>";
        ss += "<tr><td>Circle Name</td><td>" + ddlCirclePrepaid.SelectedItem.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Recharge Amount</td><td>" + txtAmountPrepaid.Text.Trim() + "</td></tr>";
        ss += "</table>";
        litRequestConfirm.Text = ss;
        Add_History(1);
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNumberPrepaid.Text.Trim() != "" && ddlOperatorPrepaid.SelectedItem.Text.ToString() != "")
            {

                if (ddlOperatorPrepaid.SelectedItem.Text.ToString() == "Vodafone")
                {
                    ViewState["id"] = "ID";
                }
                else if (ddlOperatorPrepaid.SelectedItem.Text.ToString() == "Airtel")
                {
                    ViewState["id"] = "AT";
                }
                else if (ddlOperatorPrepaid.SelectedItem.Text.ToString() == "Idea")
                {
                    ViewState["id"] = "ID";
                }
                else if (ddlOperatorPrepaid.SelectedItem.Text.ToString() == "Reliance JIO")
                {
                    ViewState["id"] = "RJ";
                }
                else if (ddlOperatorPrepaid.SelectedItem.Text.ToString() == "BSNL TopUp")
                {
                    ViewState["id"] = "CG";
                }
                if (ddlCirclePrepaid.SelectedItem.Text.ToString() == "Rajasthan")
                {
                    ViewState["circle"] = "RJ";
                }
                else if (ddlCirclePrepaid.SelectedItem.Text.ToString() == "Madhya Pradesh and Chhattisgarh")
                {
                    ViewState["circle"] = "MP";
                }
                else if (ddlCirclePrepaid.SelectedItem.Text.ToString() == "Andhra Pradesh")
                {
                    ViewState["circle"] = "AP";
                }
                else if (ddlCirclePrepaid.SelectedItem.Text.ToString() == "Bihar and Jharkhand")
                {
                    ViewState["circle"] = "BR";
                }
                else if (ddlCirclePrepaid.SelectedItem.Text.ToString() == "Himachal Pradesh")
                {
                    ViewState["circle"] = "HP";
                }
                else if (ddlCirclePrepaid.SelectedItem.Text.ToString() == "Karnataka")
                {
                    ViewState["circle"] = "KA";
                }
                else if (ddlCirclePrepaid.SelectedItem.Text.ToString() == "Maharashtra")
                {
                    ViewState["circle"] = "MH";
                }
                else if (ddlCirclePrepaid.SelectedItem.Text.ToString() == "Uttar Pradesh (East)")
                {
                    ViewState["circle"] = "UE";
                }
                else
                {
                    ViewState["circle"] = "MP";
                }
                string circle = ViewState["circle"].ToString();
                string token = "wmCJR74zQSumxcsTfSWDX06JP0kdzM1B";
                string ope = ViewState["id"].ToString();
                string parameter = "token=" + token + "&operator=" + ope + "&circle=" + circle;
                string url = "https://api.plansinfo.com/v4/mobile-plans.php" + "?" + parameter;
              // objconnection.select_data_dt("insert into ErrorLog values('" + url + "')");
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                   // clsiapplog.PrintLog("APIRequest(KYC) : " + responseFromServer + "");
                    DataSet ds = Deserialize(responseFromServer);
                    objconnection.select_data_dt("insert into ErrorLog values('" + responseFromServer + "')");
                    DataTable dtOne = new DataTable();
                    dtOne = ds.Tables[2];
                    if (dtOne.Rows.Count > 1)
                    {
                        List<ListResponse> list = new List<ListResponse>();
                        if (dtOne.Rows.Count > 0)
                        {
                            foreach (DataRow item in dtOne.Rows)
                            {
                                var cls = new ListResponse();
                                cls.rs = item["amount"].ToString();
                                cls.validity = item["validity"].ToString();
                                cls.benefit = item["benefit"].ToString();
                                list.Add(cls);

                            }
                            GridView2.DataSource = list;
                            GridView2.DataBind();
                            ViewState["id"] = "";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please try again later Or Invalid Operator');", true);
                        GridView2.DataSource = "";
                        GridView2.DataBind();
                        ViewState["id"] = "";
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('please enter mobile number and operator');", true);
            }
        }
        catch (Exception ex)
        {
            objconnection.select_data_dt("insert into ErrorLog values('" + ex.ToString() + "')");
            // clsiapplog.PrintLog("APIRequest(KYC) : " + ex.ToString() + "");
            //string error = "error found"; //ex.Message.ToString();
            //objconnection.select_data_dt("insert into ErrorLog values('" + error + "')");
            //objconnection.select_data_dt("insert into ErrorLog values('" + innerexce + "')");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found!!');", true);
        }
    }
    public class ListResponse
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string benefit { get; set; }
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
    protected void btn_ProceedPostpaid_Click(object sender, EventArgs e)
    {
        string ss = "<table class='table table-bordered table-hover tblRequestConfirm'>";
        ss += "<tr><td>Postpaid Mobile Number</td><td>" + txtNumberPostpaid.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Operator Name</td><td>" + ddlOperatorPostpaid.SelectedItem.Text.ToString() + "</td></tr>";
        ss += "<tr><td>Circle Name</td><td>" + ddlCirclePostpaid.SelectedItem.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Bill Amount</td><td>" + txtAmountPostpaid.Text.Trim() + "</td></tr>";
        ss += "</table>";
        litRequestConfirm.Text = ss;
        Add_History(4);
    }
    protected void btn_ProceedDTH_Click(object sender, EventArgs e)
    {
        string ss = "<table class='table table-bordered table-hover tblRequestConfirm'>";
        ss += "<tr><td>DTH Customer ID</td><td>" + txtCustomerID.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Operator Name</td><td>" + ddlOperatorDTH.SelectedItem.Text.ToString() + "</td></tr>";
        ss += "<tr><td>Recharge Amount</td><td>" + txtAmountDTH.Text.Trim() + "</td></tr>";
        ss += "</table>";
        litRequestConfirm.Text = ss;
        Add_History(2);
    }
    protected void btn_ProceedDatacard_Click(object sender, EventArgs e)
    {
        string ss = "<table class='table table-bordered table-hover tblRequestConfirm'>";
        ss += "<tr><td>Datacard Number</td><td>" + txtDatacardNumber.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Operator Name</td><td>" + ddlDataCard.SelectedItem.Text.ToString() + "</td></tr>";
        ss += "<tr><td>Circle Name</td><td>" + ddlCircleDatacard.SelectedItem.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Recharge Amount</td><td>" + txtDatacardAmount.Text.Trim() + "</td></tr>";
        ss += "</table>";
        litRequestConfirm.Text = ss;
        Add_History(3);
    }
    protected void btn_ProceedLandline_Click(object sender, EventArgs e)
    {
        string ss = "<table class='table table-bordered table-hover tblRequestConfirm'>";
        ss += "<tr><td>Landline Number</td><td>" + txtSTD.Text.Trim() + "-" + txtCustomerIDL.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Consumer Account Number</td><td>" + txtCANumberLandline.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Operator Name</td><td>" + ddlLandlineOperator.SelectedItem.Text.ToString() + "</td></tr>";
        ss += "<tr><td>Circle Name</td><td>" + ddlCircleLandline.SelectedItem.Text.Trim() + "</td></tr>";
        ss += "<tr><td>Bill Amount</td><td>" + txtAmountLandline.Text.Trim() + "</td></tr>";
        ss += "</table>";
        litRequestConfirm.Text = ss;
        Add_History(5);
    }
    #region methods
    protected int ChkMobileNamount(string Mobile, decimal Amount, int MsrNo, int xidno)
    {
        int result = 0;
        if (Mobile.StartsWith("0"))
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Number for Recharge or Bill Payment.. !');", true);
        else if (Convert.ToDecimal(Amount) < 5)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalid Recharge Amount.. !');", true);
        else
        {
            result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Amount, MsrNo);
            if (result == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Insufficient Balance in Wallet !');", true);
            }
        }
        return result;
    }
    private void Add_History(int id)
    {
        try
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                DataTable dts = objconnection.select_data_dt(@"EXEC Set_EzulixDmr @action='recharge', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
                if (dts.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dts.Rows[0]["isrecharge"]) == true)
                    {
                        Int32 intresult = 0; string mycirclecode = "";
                        string TransID = clsm.Cyrus_GetTransactionID_New();
                        DataTable dtMemberMaster = (DataTable)Session["dtRetailer"];
                        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                        dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]));

                        if (id == 1)
                        {
                            if (ChkMobileNamount(txtNumberPrepaid.Text, Convert.ToDecimal(txtAmountPrepaid.Text), MsrNo, id) == 1)
                            {
                                intresult = objHistory.AddEditHistory(0, MsrNo, txtNumberPrepaid.Text, "", Convert.ToDecimal(txtAmountPrepaid.Text), Convert.ToInt32(ddlOperatorPrepaid.SelectedValue), Convert.ToInt32(19), TransID, "", "", "Queued");
                                hdnTranType.Value = "rc"; mycirclecode = ddlCirclePrepaid.SelectedValue;
                            }
                            else
                            {

                                return;
                            }
                        }
                        else if (id == 2)
                        {
                            if (txtAmountDTH.Text.Trim() == "") { txtAmountDTH.Text = "0"; }
                            if (clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(txtAmountDTH.Text), MsrNo) == 1)
                            {
                                intresult = objHistory.AddEditHistory(0, MsrNo, txtCustomerID.Text, "", Convert.ToDecimal(txtAmountDTH.Text), Convert.ToInt32(ddlOperatorDTH.SelectedValue), 1, TransID, "", "", "Queued");
                                hdnTranType.Value = "rcdth"; mycirclecode = "Talktime";
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (id == 3)
                        {
                            if (ChkMobileNamount(txtDatacardNumber.Text, Convert.ToDecimal(txtDatacardAmount.Text), MsrNo, id) == 1)
                            {
                                intresult = objHistory.AddEditHistory(0, MsrNo, txtDatacardNumber.Text, "", Convert.ToDecimal(txtDatacardAmount.Text), Convert.ToInt32(ddlDataCard.SelectedValue), Convert.ToInt32(19), TransID, "", "", "Queued");
                                hdnTranType.Value = "rc"; mycirclecode = ddlCircleDatacard.SelectedValue;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (id == 4)
                        {
                            if (ChkMobileNamount(txtNumberPostpaid.Text, Convert.ToDecimal(txtAmountPostpaid.Text), MsrNo, id) == 1)
                            {
                                intresult = objHistory.AddEditHistory(0, MsrNo, txtNumberPostpaid.Text, "", Convert.ToDecimal(txtAmountPostpaid.Text), Convert.ToInt32(ddlOperatorPostpaid.SelectedValue), Convert.ToInt32(19), TransID, "", "", "Queued");
                                hdnTranType.Value = "rc";
                                mycirclecode = ddlCirclePostpaid.SelectedValue;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (id == 5)
                        {
                            if (ChkMobileNamount(txtCustomerIDL.Text, Convert.ToDecimal(txtAmountLandline.Text), MsrNo, id) == 1)
                            {
                                intresult = objHistory.AddEditHistory(0, MsrNo, (txtSTD.Text + "-" + txtCustomerIDL.Text), txtCANumberLandline.Text, Convert.ToDecimal(txtAmountLandline.Text), Convert.ToInt32(ddlLandlineOperator.SelectedValue), Convert.ToInt32(19), TransID, "", "", "Queued");
                                hdnTranType.Value = txtCANumberLandline.Text; mycirclecode = ddlCircleLandline.SelectedValue;

                            }
                            else
                            {

                                return;
                            }
                        }

                        if (intresult > 0)
                        {
                            dtHistory = objHistory.ManageHistory("Get", intresult);
                            dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]));
                            if (Convert.ToDecimal(dtHistory.Rows[0]["RechargeAmount"].ToString()) <= Convert.ToDecimal(dtEWalletBalance.Rows[0]["Balance"].ToString()))
                            {
                                clsm.Wallet_MakeTransaction(Convert.ToString(dtMemberMaster.Rows[0]["MemberID"]), Convert.ToDecimal("-" + dtHistory.Rows[0]["RechargeAmount"].ToString()), "Dr", "Recharge to " + Convert.ToString(dtHistory.Rows[0]["MobileNo"]) + " (Txn ID : " + dtHistory.Rows[0]["TransID"].ToString() + ")");
                                Session["OrderID"] = intresult;
                                string Recharge_Result = clsm.Cyrus_RechargeProcess(intresult, mycirclecode, hdnTranType.Value, dtMemberMaster);
                                char Splitter = Convert.ToChar(",");
                                string[] split = Recharge_Result.Split(Splitter);
                                if (split[0] == "Recharge Successful !!")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                                }
                                else if (split[0] == "Recharge Failed !!")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Failed !');location.replace('rechargepanel.aspx');", true);
                                }
                                else if (split[0] == "Recharge Pending !!")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Accepted !');location.replace('rechargepanel.aspx');", true);
                                }
                                clearALL();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Recharge amount can not be grater than your balance.. !');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|There is an error, Please try again !');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Recharge Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                    }
                }
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
        catch (Exception ex)
        {
            objconnection.select_data_dt("insert into test values('"+ ex.ToString()+ "')");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|There is an error, Please try again !');", true);
        }
    }
    protected void clearALL()
    {
        txtNumberPrepaid.Text = ""; ddlOperatorPrepaid.SelectedValue = "0"; ddlCirclePrepaid.SelectedValue = "0"; txtAmountPrepaid.Text = "0";
        ddlOperatorDTH.SelectedValue = "0"; txtCustomerID.Text = ""; txtAmountDTH.Text = "";
    }
    #endregion
}