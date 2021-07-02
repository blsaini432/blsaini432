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
public partial class Root_Distributor_BBPS_FetchBill_New : System.Web.UI.Page
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
            Response.Redirect("BBPS_Electricity_New.aspx");
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
                  
                    int a = clsm.Wallet_MakeTransaction_Ezulix(dtmember.Rows[0]["Memberid"].ToString(), Convert.ToDecimal("-" + amount), "Dr", "BBPSBillPay:" + ref_id + "");
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
                        _lstparm.Add(new ParmList() { name = "@duedate", value = duedate });
                        _lstparm.Add(new ParmList() { name = "@biloperator", value = billername });
                         _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
                        _lstparm.Add(new ParmList() { name = "@memberid", value = dtmember.Rows[0]["MemberId"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@action", value = "I" });
                        Cls.select_data_dtNew("Set_Ezulix_Ele_Paytm", _lstparm);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Bill Pay Succeess');window.location ='BBPS_Transactions_New.aspx';", true);
                    }
                       // Response.Redirect("Bill_PreReceipt.aspx?billref=" + ref_id, true);
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