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
using System.Text.RegularExpressions;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Xml;
using System.Globalization;
using BLL.MLM;
using Paytm;

public partial class EzulixApp_Paytmcallback : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    clsMLM_RWalletTransaction objRWalletTransaction = new clsMLM_RWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    private static int limitamount = 5000;
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
    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }
    protected string ReplaceCode(string str)
    {
        return str.Replace("'", "").Replace("-", "").Replace(";", "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["ORDERID"] != null)
        {
            string ORDERID = Request.Form["ORDERID"];
            string STATUS = Request.Form["STATUS"];
            string log_data = new StreamReader(Request.InputStream).ReadToEnd();
            DataTable orderid = cls.select_data_dt("select * from tbl_paytm_paymentGateway where TransactionId='" + ORDERID + "' and statuss='Pending'");
            cls.update_data("update tbl_paytm_paymentGateway set log_data='" + log_data + "',callback_status='" + STATUS + "' where TransactionId='" + ORDERID + "'");
            if (orderid.Rows.Count > 0)
            {
                string userid = orderid.Rows[0]["Memberid"].ToString();
                string xml_data = new StreamReader(Request.InputStream).ReadToEnd();
                string MID = Request.Form["MID"];
                string TXNID = Request.Form["TXNID"];
                string TXNAMOUNT = Request.Form["TXNAMOUNT"];
                string PAYMENTMODE = Request.Form["PAYMENTMODE"];
                string STATUSS = Request.Form["STATUS"];
                string RESPMSG = Request.Form["RESPMSG"];
                string GATEWAYNAME = Request.Form["GATEWAYNAME"];
                string BANKTXNID = Request.Form["BANKTXNID"];
                string BANKNAME = Request.Form["BANKNAME"];
                string CHECKSUMHASH = Request.Form["CHECKSUMHASH"];
                string TXNDATE = Request.Form["TXNDATE"];
                String paytmChecksum = "";

                /* Create a Dictionary from the parameters received in POST */
                //Dictionary<String, String> paytmParams = new Dictionary<String, String>();
                //foreach (string key in Request.Form.Keys)
                //{
                //    if (key.Equals("CHECKSUMHASH"))
                //    {
                //        paytmChecksum = Request.Form[key];
                //    else
                //    {
                //        paytmParams.Add(key.Trim(), Request.Form[key].Trim());
                //    }
                //}   }
                // 
                //bool isVerifySignature = Paytm.Checksum.verifySignature(paytmParams, "QL4M#TrfBmd&8VM8", paytmChecksum);
                //if (isVerifySignature == true)
                // {
                try
                {

                    if (STATUS == "TXN_SUCCESS")
                    {
                        Dictionary<string, string> body = new Dictionary<string, string>();
                        Dictionary<string, string> head = new Dictionary<string, string>();
                        Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();
                        body.Add("mid", "Cybdee15771167977955");
                        body.Add("orderId", ORDERID);
                        string paytmchecksums = Checksum.generateSignature(JsonConvert.SerializeObject(body), "4tJ4XRfjoH&Ac5dP");
                        head.Add("signature", paytmchecksums);
                        requestBody.Add("body", body);
                        requestBody.Add("head", head);
                        string post_data = JsonConvert.SerializeObject(requestBody);
                        //For  Staging
                        // string url = "https://securegw-stage.paytm.in/v3/order/status";
                        //For  Production 
                        string url = "https://securegw.paytm.in/v3/order/status";
                        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                        webRequest.Method = "POST";
                        webRequest.ContentType = "application/json";
                        webRequest.ContentLength = post_data.Length;
                        using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                        {
                            requestWriter.Write(post_data);
                        }
                        string responseData = string.Empty;
                        using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                        {
                            responseData = responseReader.ReadToEnd();

                            if (responseData != string.Empty)
                            {
                                DataSet ds = Deserialize(responseData);
                                if (ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["signature"] != null)
                                    {
                                        if (ds.Tables[2].Rows[0]["resultStatus"].ToString() == "TXN_SUCCESS")
                                        {
                                            decimal amount = Convert.ToDecimal(ds.Tables[1].Rows[0]["TXNAMOUNT"]);
                                            if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "DC" && amount <= 2000)
                                            {

                                                DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                                if (wallet.Rows.Count > 0)
                                                {
                                                    ReturnError("Transaction Status Arleady Updated. !!", "callback");

                                                }
                                                else
                                                {
                                                    double rate = 0.7;
                                                    double GSTrate = 18;
                                                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                                                    double GSTamount = TotupAmountGST(Convert.ToDouble(feeamount), GSTrate);
                                                    double totalamount = GSTamount + feeamount;
                                                    double NetAmount = Convert.ToDouble(amount) - totalamount;
                                                    string memberid = userid;
                                                    DataTable dtresult = new DataTable();
                                                    cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + feeamount + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                                    int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                                    if (balance > 0)
                                                    {
                                                        ReturnError("Transaction sucessfully.!", "callback");

                                                    }
                                                    else
                                                    {
                                                        ReturnError("Some Issue occured to add payment in wallet please try after some time", "callback");

                                                    }
                                                }
                                            }
                                            else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "DC" && amount > 2000)
                                            {
                                                DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                                if (wallet.Rows.Count > 0)
                                                {
                                                    ReturnError("Transaction Status Arleady Updated. !!", "callback");
                                                    
                                                }
                                                else
                                                {

                                                    double rate = 1.75;
                                                    double GSTrate = 18;
                                                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                                                    double GSTamount = TotupAmountGST(Convert.ToDouble(feeamount), GSTrate);
                                                    double totalamount = GSTamount + feeamount;
                                                    double NetAmount = Convert.ToDouble(amount) - totalamount;
                                                    string memberid = userid;
                                                    DataTable dtresult = new DataTable();
                                                    cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + feeamount + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                                    int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                                    if (balance > 0)
                                                    {
                                                        ReturnError("Transaction sucessfully.!", "callback");

                                                    }
                                                    else
                                                    {
                                                        ReturnError("Some Issue occured to add payment in wallet please try after some time", "callback");

                                                    }
                                                }
                                            }
                                            else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "CC")
                                            {
                                                DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                                if (wallet.Rows.Count > 0)
                                                {
                                                    ReturnError("Transaction Status Arleady Updated. !!", "callback");

                                                }
                                                else
                                                {
                                                    double rate = 1.95;
                                                    double GSTrate = 18;
                                                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                                                    double GSTamount = TotupAmountGST(Convert.ToDouble(feeamount), GSTrate);
                                                    double totalamount = GSTamount + feeamount;
                                                    double NetAmount = Convert.ToDouble(amount) - totalamount;
                                                    string memberid = userid;
                                                    DataTable dtresult = new DataTable();
                                                    cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + feeamount + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                                    int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                                    if (balance > 0)
                                                    {
                                                        ReturnError("Transaction sucessfully.!", "callback");

                                                    }
                                                    else
                                                    {
                                                        ReturnError("Some Issue occured to add payment in wallet please try after some time", "callback");

                                                    }
                                                }
                                            }
                                            else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "PPI")
                                            {
                                                DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                                if (wallet.Rows.Count > 0)
                                                {
                                                    ReturnError("Transaction Status Arleady Updated. !!", "callback");

                                                }
                                                else
                                                {
                                                    double rate = 1.70;
                                                    double GSTrate = 18;
                                                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                                                    double GSTamount = TotupAmountGST(Convert.ToDouble(feeamount), GSTrate);
                                                    double totalamount = GSTamount + feeamount;
                                                    double NetAmount = Convert.ToDouble(amount) - totalamount;
                                                    string memberid = userid;
                                                    DataTable dtresult = new DataTable();
                                                    cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + feeamount + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                                    int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                                    if (balance > 0)
                                                    {
                                                        ReturnError("Transaction sucessfully.!", "callback");

                                                    }
                                                    else
                                                    {
                                                        ReturnError("Some Issue occured to add payment in wallet please try after some time", "callback");

                                                    }
                                                }

                                            }
                                            else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "NB")
                                            {
                                                DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                                if (wallet.Rows.Count > 0)
                                                {
                                                    ReturnError("Transaction sucessfully.!", "callback");

                                                }
                                                else
                                                {
                                                    double rate = 2;
                                                    double GSTrate = 18;
                                                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                                                    double GSTamount = TotupAmountGST(Convert.ToDouble(feeamount), GSTrate);
                                                    double totalamount = GSTamount + feeamount;
                                                    double NetAmount = Convert.ToDouble(amount) - totalamount;
                                                    string memberid = userid;
                                                    DataTable dtresult = new DataTable();
                                                    cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + feeamount + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                                    int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Fund PG Order ID : " + ORDERID);
                                                    if (balance > 0)
                                                    {
                                                        ReturnError("Transaction sucessfully.!", "callback");

                                                    }
                                                    else
                                                    {
                                                        ReturnError("Some Issue occured to add payment in wallet please try after some time", "callback");

                                                    }
                                                }

                                            }
                                            else if (ds.Tables[1].Rows[0]["PAYMENTMODE"].ToString() == "UPI")
                                            {
                                                DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                                if (wallet.Rows.Count > 0)
                                                {
                                                    ReturnError("Transaction Status Arleady Updated. !!", "callback");

                                                }
                                                else
                                                {
                                                    double rate = 0;
                                                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                                                    double NetAmount = Convert.ToDouble(amount) - feeamount;
                                                    string memberid = userid;
                                                    DataTable dtresult = new DataTable();
                                                    cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + feeamount + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                                    int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "APP Add Fund PG Order ID : " + ORDERID);

                                                    if (balance > 0)
                                                    {
                                                        ReturnError("Transaction sucessfully.!", "callback");

                                                    }
                                                    else
                                                    {
                                                        ReturnError("Some Issue occured to add payment in wallet please try after some time", "callback");

                                                    }
                                                }

                                            }
                                            else
                                            {
                                                DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                                if (wallet.Rows.Count > 0)
                                                {
                                                    ReturnError("Transaction Status Arleady Updated. !!", "callback");

                                                }
                                                else
                                                {
                                                    double rate = 0;
                                                    double feeamount = TotupAmount(Convert.ToDouble(amount), rate);
                                                    double NetAmount = Convert.ToDouble(amount) - feeamount;
                                                    string memberid = userid;
                                                    DataTable dtresult = new DataTable();
                                                    cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + feeamount + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                                    int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "APP Add Fund PG Order ID : " + ORDERID);

                                                    if (balance > 0)
                                                    {
                                                        ReturnError("Transaction sucessfully.!", "callback");

                                                    }
                                                    else
                                                    {
                                                        ReturnError("Some Issue occured to add payment in wallet please try after some time", "callback");

                                                    }
                                                }

                                            }
                                        }
                                        else
                                        {
                                            cls.select_data_dt("insert into ErrorLog values('" + responseData.ToString() + "')");
                                            string memberid = Session["userid"].ToString();
                                            cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',Statuss='Failed',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");

                                        }
                                    }
                                    else
                                    {
                                        cls.select_data_dt("insert into ErrorLog values('" + responseData.ToString() + "')");
                                        ReturnError("Transaction Status Arleady Updated. !!", "callback");

                                    }
                                }
                                else
                                {
                                    cls.select_data_dt("insert into ErrorLog values('" + responseData.ToString() + "')");
                                    ReturnError("No Data Found Please Check After Some Time!!", "callback");

                                }


                            }
                            else
                            {
                                cls.select_data_dt("insert into ErrorLog values('" + responseData.ToString() + "')");
                                ReturnError("Some Error Found Please Try After Some Time!", "callback");

                            }
                        }
                    }
                    else
                    {
                        ReturnError("Some Error Found Please Try After Some Time!!!", "callback");

                    }
                }
                catch (Exception ex)
                {
                    cls.select_data_dt("insert into ErrorLog values('" + ex.ToString() + "')");
                    ReturnError("Some Error Found Please Try After Some Time!", "callback");

                }

                //}
                // else
                // {
                // cls.update_data("update tbl_paytm_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',Statuss='Failed',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                // cls.update_data("update tbl_paytm_paymentGateway set log_data='" + log_data + "', payid='" + STATUS + "'  where TransactionId='" + ORDERID + "'");
                // ReturnError("Some Error Found Please Try After Some Time!!", "callback");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Again Payment process');location.replace('paytmgateway.aspx');", true);
                //return;
                // }

                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Again Payment process');window.location ='paytmgateway.aspx';", true);
                ////}
                ////else
                ////{
                ////    cls.select_data_dt("insert into ErrorLog values('" + log_data + "')");
                ////    Retu/rnError("Some Error Found Please Try After Some Time!!!", "callback");
                ////    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Again Payment process!! ');window.location ='paytmgateway.aspx';", true);
                ///}    //}

            }
            else
            {
                ReturnError("Order ID null Please Again Try!!", "callback");
            }
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
    private void setControl()
    {
        Session["PayOrderId"] = null;
        Session["tx"] = null;
        Session["txtAmount"] = null;
        Session["Returnurl"] = null;
    }
    public double TotupAmount(double amount, double rate)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0;
        if (amount > 0)
        {
            surcharge_rate = rate;
            if (surcharge_rate > 0)
            {
                surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
            }
            NetAmount = surcharge_amt;
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }
    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }
    public double TotupAmountGST(double amount, double rate)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0;
        if (amount > 0)
        {
            surcharge_rate = rate;
            if (surcharge_rate > 0)
            {
                surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
            }
            NetAmount = surcharge_amt;
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }
}

