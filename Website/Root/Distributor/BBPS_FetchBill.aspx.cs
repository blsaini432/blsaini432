using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
public partial class Root_Distributor_BBPS_FetchBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["amount"] != null && Session["BillDate"] != null && Session["duedate"] != null && Session["amountpayable"] != null && Session["maxamount"] != null && Session["minamount"] != null && Session["OfficeCode"] != null && Session["billerid"] != null && Session["customername"] != null && Session["billername"]!=null)
        {
            if (Session["ref_id"] != null && Session["field_namelbl"] != null && Session["dtDistributor"] != null && Session["field_namevalue"] != null && Session["jwt_token"] != null)
            {
                lblcustomername.Text = Session["customername"].ToString();
                lblbillername.Text = Session["billername"].ToString();
                lblbillerid.Text = Session["billerid"].ToString();
                lblduedate.Text = Session["duedate"].ToString();
                lbltotalamount.Text = Session["amount"].ToString();
                lblbilamount.Text= Session["amount"].ToString();
                lblbilperiod.Text = "NA";
                lblfixedcharges.Text = "NA";
                lblcustomerfee.Text = "NA";
                lbladditionalcharges.Text = "NA";
                lbllatefees.Text = "NA";
                
            }
        }
        else
        {
            Response.Redirect("BBPS_Electricity.aspx");
        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (Session["ref_id"] != null && Session["field_namelbl"] != null && Session["dtDistributor"] != null && Session["field_namevalue"] != null && Session["amount"] != null && Session["billerid"] != null && Session["jwt_token"] != null && Session["customername"]!=null && Session["duedate"]!=null)
        {
            DataTable dtmember = (DataTable)Session["dtDistributor"];
            if (dtmember.Rows.Count > 0)
            {
                string biller_id = Session["billerid"].ToString();
                cls_connection Cls = new cls_connection();
                string billername = string.Empty;
                string biller_category = string.Empty;
                DataTable dt = new DataTable();
                dt =  Cls.select_data_dt(@"select biller_id,biller_name,biller_category from tbl_billerinfo where  biller_id='" + biller_id + "'");
                if(dt.Rows.Count > 0)
                {
                    billername = dt.Rows[0]["biller_name"].ToString();
                    biller_category = dt.Rows[0]["biller_category"].ToString();
                }
                string ref_id = Session["ref_id"].ToString();
                string duedate = Session["duedate"].ToString();
                string field_namelbl = Session["field_namelbl"].ToString();
                string field_namevalue = Session["field_namevalue"].ToString();
                string amount = Session["amount"].ToString();

                string billunit = string.Empty;
                if(Session["subdivname"]!=null)
                {
                    billunit = Session["subdivname"].ToString();
                }
                string initiating_channel = "AGT";
                string customer_mobile = Session["customer_mobile"].ToString();
                string CustomerName = Session["customername"].ToString();
                string geocode = "24.1568,78.5263";
                int msrno = Convert.ToInt32(dtmember.Rows[0]["MsrNo"].ToString());
                DataTable detail = Cls.select_data_dt("select * from tblmlm_membermaster where MsrNo='" + msrno + "'");
                string postal_code = detail.Rows[0]["ZIP"].ToString();
                string mobile = detail.Rows[0]["Mobile"].ToString();
                int terminal_id = msrno;
                string payment_mode = "Cash";
                string payment_bank = "Cash";
                string bbpsAgentId = "PT01PT25INTU00000001";
                int cou_cust_conv_fee = 0;
                string Remarks = "bbpsbill";
                EzulixPaytmBBPS eBbps = new EzulixPaytmBBPS();
                string jwt_token = eBbps.GenerateAgenttoken(dtmember.Rows[0]["MemberId"].ToString());
                cls_myMember clsm = new cls_myMember();
                string Result = string.Empty;
               // int chkbalance = 1;
                int chkbalance = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), Convert.ToInt32(dtmember.Rows[0]["Msrno"]));
                if (chkbalance == 1)
                {
                  
                    int a = clsm.Wallet_MakeTransaction_Ezulix(dtmember.Rows[0]["Memberid"].ToString(), Convert.ToDecimal("-" + amount), "Dr", "BBPSBill:" + ref_id + "");
                    if (a == 1)
                    {
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = "" });
                        _lstparm.Add(new ParmList() { name = "@agent_id", value = ref_id });
                        _lstparm.Add(new ParmList() { name = "@opr_id", value = "" });
                        _lstparm.Add(new ParmList() { name = "@account_no", value = Session["field_namevalue"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@sp_key", value = biller_id });
                        _lstparm.Add(new ParmList() { name = "@trans_amt", value = Convert.ToDecimal(amount) });
                        _lstparm.Add(new ParmList() { name = "@statu", value = "Pending" });
                        _lstparm.Add(new ParmList() { name = "@res_code", value = "00" });
                        _lstparm.Add(new ParmList() { name = "@res_msg", value = "Transaction Pending" });
                        _lstparm.Add(new ParmList() { name = "@customername", value = CustomerName });
                        _lstparm.Add(new ParmList() { name = "@customermobile", value = customer_mobile });
                        _lstparm.Add(new ParmList() { name = "@billerId", value = biller_id });
                        if(biller_id == "MAHA00000MAH01")
                        {
                            _lstparm.Add(new ParmList() { name = "@billunit", value = billunit });
                        }
                        _lstparm.Add(new ParmList() { name = "@duedate", value = duedate });
                        _lstparm.Add(new ParmList() { name = "@biloperator", value = billername });
                         _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
                        _lstparm.Add(new ParmList() { name = "@memberid", value = dtmember.Rows[0]["MemberId"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@action", value = "I" });
                        Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparm);
                        if (biller_id == "MAHA00000MAH01")
                        {
                            Result = eBbps.bill_paymahashtra(jwt_token, ref_id, field_namelbl, field_namevalue, biller_id, initiating_channel, terminal_id, mobile, geocode, postal_code, customer_mobile, payment_mode, payment_bank, Remarks, cou_cust_conv_fee.ToString(), CustomerName, duedate, billername, amount,billunit);
                        }
                        else
                        {
                            Result = eBbps.bill_pay(jwt_token, ref_id, field_namelbl, field_namevalue, biller_id, initiating_channel, terminal_id, mobile, geocode, postal_code, customer_mobile, payment_mode, payment_bank, Remarks, cou_cust_conv_fee.ToString(), CustomerName, duedate, billername, amount);
                        }
                          
                        if (Result != string.Empty)
                        {
                            DataSet ds = new DataSet();
                            ds = Deserialize(Result);
                            string Status = ds.Tables[0].Rows[0]["Status"].ToString();
                            string Message = ds.Tables[0].Rows[0]["message"].ToString();
                            string StatusCode = string.Empty;
                            if (ds.Tables[0].Columns.Contains("statusCode"))
                            {
                                StatusCode = ds.Tables[0].Rows[0]["statusCode"].ToString();
                            }
                            string bbpsrefid = string.Empty;
                            if(StatusCode =="600")
                            {
                                List<ParmList> _lstparmnew = new List<ParmList>();
                                _lstparmnew.Add(new ParmList() { name = "@agent_id", value = ref_id });
                                _lstparmnew.Add(new ParmList() { name = "@opr_id", value = bbpsrefid });
                                _lstparmnew.Add(new ParmList() { name = "@statu", value = "Failed" });
                                _lstparmnew.Add(new ParmList() { name = "@res_msg", value = Message });
                                _lstparmnew.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
                                _lstparmnew.Add(new ParmList() { name = "@action", value = "U" });
                                Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparmnew);
                                clsm.Wallet_MakeTransaction_Ezulix(dtmember.Rows[0]["Memberid"].ToString(), Convert.ToDecimal(amount), "Cr", "Fail BBPSBill:" + ref_id + "");
                            }
                            else
                            {
                                if (Status == "Pending" || Status == "PENDING")
                                {
                                    string pendingresult = eBbps.CheckStatus(jwt_token, ref_id);
                                    if (pendingresult != string.Empty)
                                    {
                                        ds = Deserialize(pendingresult);
                                        Status = ds.Tables[0].Rows[0]["Status"].ToString();
                                        Message = ds.Tables[0].Rows[0]["message"].ToString();
                                        if (ds.Tables[0].Columns.Contains("bbpsRefId"))
                                        {
                                            bbpsrefid = ds.Tables[0].Rows[0]["bbpsRefId"].ToString();
                                        }

                                        List<ParmList> _lstparmnew = new List<ParmList>();
                                        _lstparmnew.Add(new ParmList() { name = "@agent_id", value = ref_id });
                                        _lstparmnew.Add(new ParmList() { name = "@opr_id", value = bbpsrefid });
                                        _lstparmnew.Add(new ParmList() { name = "@statu", value = Status });
                                        _lstparmnew.Add(new ParmList() { name = "@res_msg", value = Message });
                                        _lstparmnew.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
                                        _lstparmnew.Add(new ParmList() { name = "@action", value = "U" });
                                        Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparmnew);
                                        if (Status == "FAILURE" || Status == "false")
                                        {
                                            clsm.Wallet_MakeTransaction_Ezulix(dtmember.Rows[0]["Memberid"].ToString(), Convert.ToDecimal(amount), "Cr", "Fail BBPSBill:" + ref_id + "");
                                        }
                                        else if (Status == "SUCCESS")
                                        {
                                            DataTable dtchk = new DataTable();
                                            dtchk = Cls.select_data_dt("select ServiceId from Tbl_paytmservice inner join tbl_billerinfo on tbl_billerinfo.biller_category=Tbl_paytmservice.ServiceName where biller_id='" + biller_id + "'");
                                            if (dtchk.Rows.Count > 0)
                                            {
                                                List<ParmList> _lstparmn = new List<ParmList>();
                                                _lstparmn.Add(new ParmList() { name = "@memberid", value = dtmember.Rows[0]["Memberid"].ToString() });
                                                _lstparmn.Add(new ParmList() { name = "@serviceid", value = Convert.ToInt32(dtchk.Rows[0]["ServiceId"]) });
                                                _lstparmn.Add(new ParmList() { name = "@txnamount", value = Convert.ToDecimal(amount) });
                                                _lstparmn.Add(new ParmList() { name = "@txnid", value = ref_id });
                                                Cls.select_data_dtNew("SET_BBPS_Commission", _lstparmn);
                                            }
                                        }
                                    }
                                }
                                else if(Status == "FAILURE" || Status == "false")
                                {
                                    List<ParmList> _lstparmnew = new List<ParmList>();
                                    _lstparmnew.Add(new ParmList() { name = "@agent_id", value = ref_id });
                                    _lstparmnew.Add(new ParmList() { name = "@opr_id", value = bbpsrefid });
                                    _lstparmnew.Add(new ParmList() { name = "@statu", value = Status });
                                    _lstparmnew.Add(new ParmList() { name = "@res_msg", value = Message });
                                    _lstparmnew.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
                                    _lstparmnew.Add(new ParmList() { name = "@action", value = "U" });
                                    Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparmnew);
                                    clsm.Wallet_MakeTransaction_Ezulix(dtmember.Rows[0]["Memberid"].ToString(), Convert.ToDecimal(amount), "Cr", "Fail BBPSBill:" + ref_id + "");
                                }
                                else
                                {
                                    List<ParmList> _lstparmnew = new List<ParmList>();
                                    _lstparmnew.Add(new ParmList() { name = "@agent_id", value = ref_id });
                                    _lstparmnew.Add(new ParmList() { name = "@opr_id", value = bbpsrefid });
                                    _lstparmnew.Add(new ParmList() { name = "@statu", value = Status });
                                    _lstparmnew.Add(new ParmList() { name = "@res_msg", value = Message });
                                    _lstparmnew.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
                                    _lstparmnew.Add(new ParmList() { name = "@action", value = "U" });
                                    Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparmnew);
                                     if (Status == "SUCCESS")
                                    {
                                        
                                        DataTable dtchk = new DataTable();
                                        dtchk = Cls.select_data_dt("select ServiceId from Tbl_paytmservice inner join tbl_billerinfo on tbl_billerinfo.biller_category=Tbl_paytmservice.ServiceName where biller_id='" + biller_id + "'");
                                        if (dtchk.Rows.Count>0)
                                        {
                                            List<ParmList> _lstparmn = new List<ParmList>();
                                            _lstparmn.Add(new ParmList() { name = "@memberid", value = dtmember.Rows[0]["Memberid"].ToString() });
                                            _lstparmn.Add(new ParmList() { name = "@serviceid", value = Convert.ToInt32(dtchk.Rows[0]["ServiceId"]) });
                                            _lstparmn.Add(new ParmList() { name = "@txnamount", value = Convert.ToDecimal(amount) });
                                            _lstparmn.Add(new ParmList() { name = "@txnid", value = ref_id });
                                            Cls.select_data_dtNew("SET_BBPS_Commission", _lstparmn);
                                        }


                                       
                                    }
                                }
                            }
                        }
                        Response.Redirect("Bill_PreReceipt.aspx?billref=" + ref_id, true);
                     }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficient Wallet Balance');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficient Wallet Balance');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Session Expried Please Login Again');", true);
            }
        }
    }


    #region methods
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

}