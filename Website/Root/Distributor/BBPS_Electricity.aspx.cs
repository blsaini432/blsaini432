using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text;
using System.ComponentModel;

public partial class Root_Distributor_BBPS_Electricity : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    public static string jwt_token;
    cls_myMember clsm = new cls_myMember();
    EzulixPaytmBBPS eBbps = new EzulixPaytmBBPS();
    DataTable dt = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                //string Result = string.Empty;
                //Result = eBbps.GenerateAgenttoken();
                //if (Result != null)
                //{
                //    DataSet ds = Deserialize(Result.ToString());
                //    jwt_token = ds.Tables["root"].Rows[0]["token"].ToString();
                //    Session["jwt_token"] = jwt_token;
                //    Cls.insert_data("insert into tblBBPS_token(token) Values('" + jwt_token + "')");
                //}
                //DataTable dtMemberMaster = new DataTable();
                //dtMemberMaster = (DataTable)Session["dtDistributor"];
                //string Memberid = dtMemberMaster.Rows[0]["MemberId"].ToString();
                //int Msrno = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                //ViewState["Msrno"] = Msrno;
                //ViewState["Memberid"] = Memberid;


                FilleBoard();

            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }

    void FilleBoard()
    {
        DataTable dta = new DataTable();
        dta = Cls.select_data_dt(@"select distinct biller_category from tbl_billerinfo");
        ddl_Catgory.DataSource = dta;
        ddl_Catgory.DataTextField = "biller_category";
        ddl_Catgory.DataValueField = "biller_category";
        ddl_Catgory.DataBind();
        ddl_Catgory.Items.Insert(0, new ListItem("Select Category", "0"));
    }

    void FillSubdivison()
    {
        dt = Cls.select_data_dt(@"select SubgroupName from tbl_electricity_subgroup where OperaterId='MAHA00000MAH01'");
        ddl_subdivison.DataSource = dt;
        ddl_subdivison.DataTextField = "SubgroupName";
        ddl_subdivison.DataValueField = "SubgroupName";
        ddl_subdivison.DataBind();
        ddl_subdivison.Items.Insert(0, new ListItem("Select Subgroup", "0"));
    }
    #region Methods
    public DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        try
        {
            ds.Clear();
            XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
            StringReader theReader = new StringReader(doc.InnerXml.ToString());
            ds.ReadXml(theReader);

            return ds;
        }
        catch (Exception err)
        {
            return ds;
        }
    }

    #endregion


    protected void ddl_Catgory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Catgory.SelectedIndex > 0)
        {

            Session["Eboard"] = null;
            dt = Cls.select_data_dt(@"select biller_id,biller_name from tbl_billerinfo where biller_category ='" + ddl_Catgory.SelectedValue + "'");
            Session["Eboard"] = dt;
            ddl_Eboard.DataSource = dt;
            ddl_Eboard.DataTextField = "biller_name";
            ddl_Eboard.DataValueField = "biller_id";
            ddl_Eboard.DataBind();
            ddl_Eboard.Items.Insert(0, new ListItem("Select Board", "0"));
        }


    }

    protected void ddl_Eboard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Eboard.SelectedIndex > 0)
        {
            if (ddl_Eboard.SelectedValue == "MAHA00000MAH01")
            {

                divdivsion.Attributes.Add("style", "display:flex");
                FillSubdivison();
                DataTable dt = Cls.select_data_dt(@"select * from tbl_billerinfo where biller_id ='" + ddl_Eboard.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    string name = dt.Rows[0]["bill_details_input_parameters0field_name"].ToString();
                    lblconsumerno.Text = name;
                    Button1.Visible = false;
                    btn_fetchfill.Visible = true;
                }
            }
            else
            {
                divdivsion.Attributes.Add("style", "display:none");
                DataTable dt = Cls.select_data_dt(@"select * from tbl_billerinfo where biller_id ='" + ddl_Eboard.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {

                    string name = dt.Rows[0]["bill_details_input_parameters0field_name"].ToString();
                    lblconsumerno.Text = name;

                    if (Convert.ToBoolean(dt.Rows[0]["isbillfetch"]) == true)
                    {
                        Button1.Visible = false;
                        btn_fetchfill.Visible = true;
                        divbillamount.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        divbillamount.Attributes.Add("style", "display:flex");
                        Button1.Visible = true;
                        btn_fetchfill.Visible = false;
                    }

                }
            }

        }

    }
    //protected void btn_fetchfill_Click(object sender, EventArgs e)
    //{
    //    DataTable dtmember = (DataTable)Session["dtDistributor"];
    //    if (dtmember.Rows.Count > 0)
    //    {
    //        string jwt_token = eBbps.GenerateAgenttoken(Convert.ToString(dtmember.Rows[0]["MemberId"]));
    //        string ref_id = Generateref_id();

    //        string field_namelbl = lblconsumerno.Text;
    //        // string field_namelbl = "K Number";

    //        string field_namevalue = txt_servicenum.Text;
    //        // string field_namevalue = "123456789013";

    //        string biller_id = ddl_Eboard.SelectedValue;
    //        //   string biller_id = "CESC00000KOL01";

    //        string initiating_channel = "AGT";
    //        string customer_mobile = txt_customermobile.Text;
    //        // string agent_id = "PT03";
    //        string agent_id = "PT25";
    //        // string customer_mobile = "7878656534";
    //        string si_txn = "Yes";
    //        string geocode = "24.1568,78.5263";
    //        string mobile = dtmember.Rows[0]["Mobile"].ToString();
    //        // string bbpsAgentId = "PT01PT0MOB7118732216";
    //        string bbpsAgentId = "PT01PT25INTU00000001";
    //        string postal_code = "302019";
    //        int terminal_id = Convert.ToInt32(dtmember.Rows[0]["MsrNo"].ToString());
    //        string Result = string.Empty;
    //        if (biller_id == "MAHA00000MAH01")
    //        {
    //            Result = eBbps.bill_fetchmaharashtra(jwt_token, ref_id, field_namelbl, field_namevalue, biller_id, initiating_channel, terminal_id, mobile, geocode, postal_code, customer_mobile, ddl_subdivison.SelectedValue);
    //        }
    //        else
    //        {
    //            Result = eBbps.bill_fetch(jwt_token, ref_id, field_namelbl, field_namevalue, biller_id, initiating_channel, terminal_id, mobile, geocode, postal_code, customer_mobile);
    //        }

    //        if (Result != string.Empty)
    //        {
    //            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(Result);
    //            string Status = myDeserializedClass.status;
    //            string message = myDeserializedClass.message;
    //            if (Status == "SUCCESS")
    //            {
    //                string amount = myDeserializedClass.data.bill_details.amount;

    //                if (amount == null || amount == "")
    //                {
    //                    Session["amount"] = "0.00";
    //                }
    //                else
    //                {
    //                    Session["amount"] = amount;
    //                }
    //                string BillDate = myDeserializedClass.data.bill_details.BillDate;
    //                if (BillDate == null || BillDate == "")
    //                {
    //                    Session["BillDate"] = "NA";
    //                }
    //                else
    //                {
    //                    Session["BillDate"] = BillDate;
    //                }
    //                string duedate = myDeserializedClass.data.bill_details.DueDate;
    //                if (duedate == null || duedate == "")
    //                {
    //                    Session["duedate"] = "NA";
    //                }
    //                else
    //                {
    //                    Session["duedate"] = duedate;
    //                }
    //                string amountpayable = myDeserializedClass.data.bill_details.AmountPayable;
    //                if (amountpayable == null || amountpayable == "")
    //                {
    //                    Session["amountpayable"] = "NA";
    //                }
    //                else
    //                {
    //                    Session["amountpayable"] = amountpayable;
    //                }
    //                string maxamount = myDeserializedClass.data.bill_details.maxAmount;
    //                if (maxamount == null || maxamount == "")
    //                {
    //                    Session["maxamount"] = "0.00";
    //                }
    //                else
    //                {
    //                    Session["maxamount"] = maxamount;
    //                }
    //                string minamount = myDeserializedClass.data.bill_details.minAmount;
    //                if (minamount == null || minamount == "")
    //                {
    //                    Session["minamount"] = "0.00";
    //                }
    //                else
    //                {
    //                    Session["minamount"] = minamount;
    //                }
    //                string OfficeCode = myDeserializedClass.data.bill_details.OfficeCode;
    //                if (OfficeCode == null || OfficeCode == "")
    //                {
    //                    Session["OfficeCode"] = "0.00";
    //                }
    //                else
    //                {
    //                    Session["OfficeCode"] = OfficeCode;
    //                }
    //                string billerid = myDeserializedClass.data.biller_details.biller_id;
    //                if (billerid == null || billerid == "")
    //                {
    //                    Session["billerid"] = "0.00";
    //                }
    //                else
    //                {
    //                    Session["billerid"] = billerid;
    //                }
    //                string customername = myDeserializedClass.data.bill_details.CustomerName;
    //                if (customername == null || customername == "")
    //                {
    //                    Session["customername"] = "NA";
    //                }
    //                else
    //                {
    //                    Session["customername"] = customername;
    //                }
    //                Session["ref_id"] = ref_id;
    //                Session["field_namelbl"] = field_namelbl;
    //                Session["jwt_token"] = jwt_token;
    //                Session["field_namevalue"] = field_namevalue;
    //                Session["customer_mobile"] = customer_mobile;
    //                Session["billername"] = ddl_Eboard.SelectedItem.ToString();
    //                if (biller_id == "MAHA00000MAH01")
    //                {
    //                    Session["subdivname"] = ddl_subdivison.SelectedValue;
    //                }
    //                Response.Redirect("BBPS_FetchBill.aspx");

    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + message + "');", true);
    //            }
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Empty Result!');", true);
    //        }
    //    }
    //    else
    //    {

    //    }


    //}

    #region Methods
    public string Generateref_id()
    {
        Random rnd = new Random();
        Int64 month = rnd.Next(10000, 99999);
        month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(10000, 99999)));
        return month.ToString();
    }

    #endregion


    #region Class 

    public class BillerFetchResponse
    {
    }

    public class BillerDetails
    {
        public string biller_id { get; set; }
    }

    public class BillDetails
    {
        public List<object> more_info { get; set; }
        public string editable { get; set; }
        public string minAmount { get; set; }
        public string maxAmount { get; set; }
        public string amount { get; set; }
        [JsonProperty("Name")]   //this one changed
        public string CustomerName { get; set; }
        [JsonProperty("Office Code")]   //this one changed
        public string OfficeCode { get; set; }
        [JsonProperty("Binder Number")]   //this one changed
        public string BinderNumber { get; set; }
        [JsonProperty("Invoice Id")]   //this one changed
        public string InvoiceId { get; set; }
        [JsonProperty("Bill Date")]   //this one changed
        public string BillDate { get; set; }
        [JsonProperty("Due Date")]   //this one changed
        public string DueDate { get; set; }
        [JsonProperty("Amount Payable")]   //this one changed
        public string AmountPayable { get; set; }
    }

    public class Data
    {
        public BillerFetchResponse biller_fetch_response { get; set; }
        public BillerDetails biller_details { get; set; }
        public BillDetails bill_details { get; set; }
    }

    public class Root
    {
        public string ref_id { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }




    #endregion

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    if (Convert.ToDecimal(txt_amount.Text) > 0)
    //    {
    //        DataTable dtmember = (DataTable)Session["dtDistributor"];
    //        if (dtmember.Rows.Count > 0)
    //        {
    //            string ref_id = Generateref_id();
    //            string biller_id = ddl_Eboard.SelectedValue;
    //            cls_connection Cls = new cls_connection();
    //            string billername = string.Empty;
    //            DataTable dt = new DataTable();
    //            dt = Cls.select_data_dt(@"select biller_id,biller_name from tbl_billerinfo where biller_category ='FASTag Recharge' and biller_id='" + biller_id + "'");
    //            if (dt.Rows.Count > 0)
    //            {
    //                billername = dt.Rows[0]["biller_name"].ToString();
    //            }

    //            string duedate = "";
    //            string field_namelbl = lblconsumerno.Text;
    //            string field_namevalue = txt_servicenum.Text;
    //            string amount = txt_amount.Text.Trim();


    //            string initiating_channel = "AGT";
    //            string customer_mobile = txt_customermobile.Text;
    //            string CustomerName = "";
    //            string geocode = "24.1568,78.5263";
    //            int msrno = Convert.ToInt32(dtmember.Rows[0]["MsrNo"].ToString());
    //            DataTable detail = Cls.select_data_dt("select * from tblmlm_membermaster where MsrNo='" + msrno + "'");
    //            string postal_code = detail.Rows[0]["ZIP"].ToString();
    //            string mobile = detail.Rows[0]["Mobile"].ToString();
    //            int terminal_id = msrno;
    //            string payment_mode = "Cash";
    //            string payment_bank = "Cash";
    //            string bbpsAgentId = "PT01PT25INTU00000001";
    //            int cou_cust_conv_fee = 0;
    //            string Remarks = "bbpsbill";
    //           // EzulixPaytmBBPS eBbps = new EzulixPaytmBBPS();
    //            string jwt_token = eBbps.GenerateAgenttoken(dtmember.Rows[0]["MemberId"].ToString());
    //            cls_myMember clsm = new cls_myMember();
    //            string Result = string.Empty;
    //            // int chkbalance = 1;
    //            int chkbalance = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), Convert.ToInt32(dtmember.Rows[0]["Msrno"]));
    //            if (chkbalance == 1)
    //            {

    //                int a = clsm.Wallet_MakeTransaction_Ezulix(dtmember.Rows[0]["Memberid"].ToString(), Convert.ToDecimal("-" + amount), "Dr", "PayEleBill:" + ref_id + "");
    //                if (a == 1)
    //                {
    //                    List<ParmList> _lstparm = new List<ParmList>();
    //                    _lstparm.Add(new ParmList() { name = "@ipay_id", value = "" });
    //                    _lstparm.Add(new ParmList() { name = "@agent_id", value = ref_id });
    //                    _lstparm.Add(new ParmList() { name = "@opr_id", value = "" });
    //                    _lstparm.Add(new ParmList() { name = "@account_no", value = field_namevalue });
    //                    _lstparm.Add(new ParmList() { name = "@sp_key", value = biller_id });
    //                    _lstparm.Add(new ParmList() { name = "@trans_amt", value = Convert.ToDecimal(amount) });
    //                    _lstparm.Add(new ParmList() { name = "@statu", value = "Pending" });
    //                    _lstparm.Add(new ParmList() { name = "@res_code", value = "00" });
    //                    _lstparm.Add(new ParmList() { name = "@res_msg", value = "Transaction Pending" });
    //                    _lstparm.Add(new ParmList() { name = "@customername", value = CustomerName });
    //                    _lstparm.Add(new ParmList() { name = "@customermobile", value = customer_mobile });
    //                    _lstparm.Add(new ParmList() { name = "@billerId", value = biller_id });

    //                    _lstparm.Add(new ParmList() { name = "@duedate", value = duedate });
    //                    _lstparm.Add(new ParmList() { name = "@biloperator", value = billername });
    //                    _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
    //                    _lstparm.Add(new ParmList() { name = "@memberid", value = dtmember.Rows[0]["MemberId"].ToString() });
    //                    _lstparm.Add(new ParmList() { name = "@action", value = "I" });
    //                    Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparm);

    //                    Result = eBbps.bill_pay(jwt_token, ref_id, field_namelbl, field_namevalue, biller_id, initiating_channel, terminal_id, mobile, geocode, postal_code, customer_mobile, payment_mode, payment_bank, Remarks, cou_cust_conv_fee.ToString(), CustomerName, duedate, billername, amount);


    //                    if (Result != string.Empty)
    //                    {
    //                        DataSet ds = new DataSet();
    //                        ds = Deserialize(Result);
    //                        string Status = ds.Tables[0].Rows[0]["Status"].ToString();
    //                        string Message = ds.Tables[0].Rows[0]["message"].ToString();
    //                        string StatusCode = string.Empty;
    //                        if (ds.Tables[0].Columns.Contains("statusCode"))
    //                        {
    //                            StatusCode = ds.Tables[0].Rows[0]["statusCode"].ToString();
    //                        }
    //                        string bbpsrefid = string.Empty;
    //                        if (StatusCode == "600")
    //                        {
    //                            List<ParmList> _lstparmnew = new List<ParmList>();
    //                            _lstparmnew.Add(new ParmList() { name = "@agent_id", value = ref_id });
    //                            _lstparmnew.Add(new ParmList() { name = "@opr_id", value = bbpsrefid });
    //                            _lstparmnew.Add(new ParmList() { name = "@statu", value = "Failed" });
    //                            _lstparmnew.Add(new ParmList() { name = "@res_msg", value = Message });
    //                            _lstparmnew.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
    //                            _lstparmnew.Add(new ParmList() { name = "@action", value = "U" });
    //                            Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparmnew);
    //                            clsm.Wallet_MakeTransaction_Ezulix(dtmember.Rows[0]["Memberid"].ToString(), Convert.ToDecimal(amount), "Cr", "Fail PayEleBill:" + ref_id + "");
    //                        }
    //                        else
    //                        {
    //                            if (Status == "Pending" || Status == "PENDING")
    //                            {
    //                                string pendingresult = eBbps.CheckStatus(jwt_token, ref_id);
    //                                if (pendingresult != string.Empty)
    //                                {
    //                                    ds = Deserialize(pendingresult);
    //                                    Status = ds.Tables[0].Rows[0]["Status"].ToString();
    //                                    Message = ds.Tables[0].Rows[0]["message"].ToString();
    //                                    if (ds.Tables[0].Columns.Contains("bbpsRefId"))
    //                                    {
    //                                        bbpsrefid = ds.Tables[0].Rows[0]["bbpsRefId"].ToString();
    //                                    }

    //                                    List<ParmList> _lstparmnew = new List<ParmList>();
    //                                    _lstparmnew.Add(new ParmList() { name = "@agent_id", value = ref_id });
    //                                    _lstparmnew.Add(new ParmList() { name = "@opr_id", value = bbpsrefid });
    //                                    _lstparmnew.Add(new ParmList() { name = "@statu", value = Status });
    //                                    _lstparmnew.Add(new ParmList() { name = "@res_msg", value = Message });
    //                                    _lstparmnew.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
    //                                    _lstparmnew.Add(new ParmList() { name = "@action", value = "U" });
    //                                    Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparmnew);
    //                                    if (Status == "FAILURE" || Status == "false")
    //                                    {
    //                                        clsm.Wallet_MakeTransaction_Ezulix(dtmember.Rows[0]["Memberid"].ToString(), Convert.ToDecimal(amount), "Cr", "Fail PayEleBill:" + ref_id + "");
    //                                    }
    //                                    else if (Status == "SUCCESS")
    //                                    {
    //                                        DataTable dtchk = new DataTable();
    //                                        dtchk = Cls.select_data_dt("select ServiceId from Tbl_paytmservice inner join tbl_billerinfo on tbl_billerinfo.biller_category=Tbl_paytmservice.ServiceName where biller_id='" + biller_id + "'");
    //                                        if (dtchk.Rows.Count > 0)
    //                                        {
    //                                            List<ParmList> _lstparmn = new List<ParmList>();
    //                                            _lstparmn.Add(new ParmList() { name = "@memberid", value = dtmember.Rows[0]["Memberid"].ToString() });
    //                                            _lstparmn.Add(new ParmList() { name = "@serviceid", value = Convert.ToInt32(dtchk.Rows[0]["ServiceId"]) });
    //                                            _lstparmn.Add(new ParmList() { name = "@txnamount", value = Convert.ToDecimal(amount) });
    //                                            _lstparmn.Add(new ParmList() { name = "@txnid", value = ref_id });
    //                                            Cls.select_data_dtNew("SET_BBPS_Commission", _lstparmn);
    //                                        }

    //                                    }
    //                                }
    //                            }
    //                            else if (Status == "FAILURE" || Status == "false")
    //                            {
    //                                List<ParmList> _lstparmnew = new List<ParmList>();
    //                                _lstparmnew.Add(new ParmList() { name = "@agent_id", value = ref_id });
    //                                _lstparmnew.Add(new ParmList() { name = "@opr_id", value = bbpsrefid });
    //                                _lstparmnew.Add(new ParmList() { name = "@statu", value = Status });
    //                                _lstparmnew.Add(new ParmList() { name = "@res_msg", value = Message });
    //                                _lstparmnew.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
    //                                _lstparmnew.Add(new ParmList() { name = "@action", value = "U" });
    //                                Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparmnew);
    //                                clsm.Wallet_MakeTransaction_Ezulix(dtmember.Rows[0]["Memberid"].ToString(), Convert.ToDecimal(amount), "Cr", "Fail PayEleBill:" + ref_id + "");
    //                            }
    //                            else
    //                            {
    //                                List<ParmList> _lstparmnew = new List<ParmList>();
    //                                _lstparmnew.Add(new ParmList() { name = "@agent_id", value = ref_id });
    //                                _lstparmnew.Add(new ParmList() { name = "@opr_id", value = bbpsrefid });
    //                                _lstparmnew.Add(new ParmList() { name = "@statu", value = Status });
    //                                _lstparmnew.Add(new ParmList() { name = "@res_msg", value = Message });
    //                                _lstparmnew.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
    //                                _lstparmnew.Add(new ParmList() { name = "@action", value = "U" });
    //                                Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparmnew);
    //                                if (Status == "SUCCESS")
    //                                {
    //                                    DataTable dtchk = new DataTable();
    //                                    dtchk = Cls.select_data_dt("select ServiceId from Tbl_paytmservice inner join tbl_billerinfo on tbl_billerinfo.biller_category=Tbl_paytmservice.ServiceName where biller_id='" + biller_id + "'");
    //                                    if (dtchk.Rows.Count > 0)
    //                                    {
    //                                        List<ParmList> _lstparmn = new List<ParmList>();
    //                                        _lstparmn.Add(new ParmList() { name = "@memberid", value = dtmember.Rows[0]["Memberid"].ToString() });
    //                                        _lstparmn.Add(new ParmList() { name = "@serviceid", value = Convert.ToInt32(dtchk.Rows[0]["ServiceId"]) });
    //                                        _lstparmn.Add(new ParmList() { name = "@txnamount", value = Convert.ToDecimal(amount) });
    //                                        _lstparmn.Add(new ParmList() { name = "@txnid", value = ref_id });
    //                                        Cls.select_data_dtNew("SET_BBPS_Commission", _lstparmn);
    //                                    }

    //                                }
    //                            }
    //                        }
    //                    }
    //                    Response.Redirect("Bill_PreReceipt.aspx?billref=" + ref_id, true);
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficient Wallet Balance');", true);
    //                }
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficient Wallet Balance');", true);
    //            }

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Session Expried Please Login Again');", true);
    //        }
    //    }

    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter the amount');", true);
    //    }


    //}
}