using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Xml;
using Newtonsoft.Json;

public partial class API_mpostxn : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    cls_myMember cLSM = new cls_myMember();
    DataTable dtsell = new DataTable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        string input = string.Empty;
        using (var reader = new StreamReader(Request.InputStream))
        {
            input = reader.ReadToEnd();
        }
        if (input != string.Empty && Request.Headers.ToString().Contains("Memberid") && Request.Headers.ToString().Contains("Type") && Request.Headers.ToString().Contains("Commission"))
        {
            string memberid = Request.Headers["Memberid"].ToString();
            string Txntype = Request.Headers["Type"].ToString();
            string Commission = Request.Headers["Commission"].ToString();
            DataTable dtMember = Cls.select_data_dt(@"SELECT * FROM tblmlm_membermaster WHERE Memberid='" + memberid + "'");
            if (dtMember.Rows.Count > 0)
            {
                DataSet ds = Deserialize(input);
                DataTable dtchktxn = Cls.select_data_dt(@"SELECT * FROM MposTxn WHERE TransactionId='" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "'");
                if (dtchktxn.Rows.Count == 0)
                {
                    List<ParmList> _list = new List<ParmList>();
                    _list.Add(new ParmList() { name = "@MerchantId", value = ds.Tables[0].Rows[0]["merchantId"].ToString() });
                    if (ds.Tables[0].Rows[0].Table.Columns.Contains("status"))
                        _list.Add(new ParmList() { name = "@Statu", value = ds.Tables[0].Rows[0]["status"].ToString() });
                    else
                        _list.Add(new ParmList() { name = "@Statu", value = ds.Tables[0].Rows[0]["statu"].ToString() });
                    _list.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"].ToString()) });
                    _list.Add(new ParmList() { name = "@AuthCode", value = ds.Tables[0].Rows[0]["authCode"].ToString() });
                    _list.Add(new ParmList() { name = "@BankMerchantId", value = ds.Tables[0].Rows[0]["bankMerchantId"].ToString() });
                    _list.Add(new ParmList() { name = "@BankTerminalId", value = ds.Tables[0].Rows[0]["bankTerminalId"].ToString() });
                    _list.Add(new ParmList() { name = "@BatchNo", value = ds.Tables[0].Rows[0]["batchNo"].ToString() });
                    _list.Add(new ParmList() { name = "@CardBrand", value = ds.Tables[0].Rows[0]["cardBrand"].ToString() });
                    _list.Add(new ParmList() { name = "@CardHolderName", value = ds.Tables[0].Rows[0]["cardHolderName"].ToString() });
                    _list.Add(new ParmList() { name = "@CardNumber", value = ds.Tables[0].Rows[0]["cardNumber"].ToString() });
                    _list.Add(new ParmList() { name = "@CardType", value = ds.Tables[0].Rows[0]["cardType"].ToString() });
                    _list.Add(new ParmList() { name = "@InvoiceNo", value = ds.Tables[0].Rows[0]["invoiceNo"].ToString() });
                    if (ds.Tables[0].Rows[0].Table.Columns.Contains("MerchantName"))
                    {
                        _list.Add(new ParmList() { name = "@MerchantName", value = ds.Tables[0].Rows[0]["MerchantName"].ToString() });
                    }
                    else
                    {
                        _list.Add(new ParmList() { name = "@MerchantName", value = ds.Tables[1].Rows[0][0].ToString() });
                    }
                    if (ds.Tables[0].Rows[0].Table.Columns.Contains("MerchantAddress"))
                    {
                        _list.Add(new ParmList() { name = "@MerchantAddress", value = ds.Tables[0].Rows[0]["MerchantAddress"].ToString() });
                    }
                    else
                    {
                        _list.Add(new ParmList() { name = "@MerchantAddress", value = ds.Tables[1].Rows[1][0].ToString() });
                    }
                    _list.Add(new ParmList() { name = "@PanLength", value = ds.Tables[0].Rows[0]["panLength"].ToString() });
                    _list.Add(new ParmList() { name = "@PaymentId", value = ds.Tables[0].Rows[0]["paymentId"].ToString() });
                    _list.Add(new ParmList() { name = "@PinVerifiedFlag", value = ds.Tables[0].Rows[0]["pinVerifiedFlag"].ToString() });
                    _list.Add(new ParmList() { name = "@ResponseCode", value = ds.Tables[0].Rows[0]["responseCode"].ToString() });
                    _list.Add(new ParmList() { name = "@TransactionId", value = ds.Tables[0].Rows[0]["transactionId"].ToString() });
                    if (ds.Tables[0].Rows[0].Table.Columns.Contains("aid"))
                        _list.Add(new ParmList() { name = "@Aid", value = ds.Tables[0].Rows[0]["aid"].ToString() });
                    if (ds.Tables[0].Rows[0].Table.Columns.Contains("aidname"))
                        _list.Add(new ParmList() { name = "@Aidname", value = ds.Tables[0].Rows[0]["aidname"].ToString() });
                    if (ds.Tables[0].Rows[0].Table.Columns.Contains("rrn"))
                        _list.Add(new ParmList() { name = "@Rrn", value = ds.Tables[0].Rows[0]["rrn"].ToString() });
                    if (ds.Tables[0].Rows[0].Table.Columns.Contains("tc"))
                        _list.Add(new ParmList() { name = "@Tc", value = ds.Tables[0].Rows[0]["tc"].ToString() });
                    if (ds.Tables[0].Rows[0].Table.Columns.Contains("tsi"))
                        _list.Add(new ParmList() { name = "@Tsi", value = ds.Tables[0].Rows[0]["tsi"].ToString() });
                    if (ds.Tables[0].Rows[0].Table.Columns.Contains("tvr"))
                        _list.Add(new ParmList() { name = "@Tvr", value = ds.Tables[0].Rows[0]["tvr"].ToString() });
                    _list.Add(new ParmList() { name = "@MemberId", value = memberid });
                    _list.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(dtMember.Rows[0]["MsrNo"].ToString()) });
                    _list.Add(new ParmList() { name = "@TxnType", value = Txntype.ToString() });
                    _list.Add(new ParmList() { name = "@Asur", value = Commission });
                    //Cls.select_data_dtNew("SET_MposTxn", _list);
                }
                DataTable dtchkwallet = Cls.select_data_dt(@"SELECT * FROM tblMLM_RWalletTransaction WHERE Narration='Mpos TXN:" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "'");
                if (dtchkwallet.Rows.Count == 0)
                {
                    if (Txntype == "sell")
                    {
                        dtsell = Cls.select_data_dt(@"EXEC SET_Mpos_Sell_Com @Amount='" + Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"].ToString()) + "',@Packageid=" + dtMember.Rows[0]["PackageID"].ToString() + ",@Action='sell',@TransactionId='" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "'");
                        if (dtsell.Rows.Count > 0)
                            cLSM.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(dtsell.Rows[0]["NetAmount"].ToString()), "Cr", "Mpos TXN:" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "");
                        else
                            cLSM.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"].ToString()), "Cr", "Mpos TXN:" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "");
                    }
                    else if (Txntype == "emi")
                    {
                        dtsell = Cls.select_data_dt(@"EXEC SET_Mpos_Sell_Com @Amount='" + Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"].ToString()) + "',@Packageid=" + dtMember.Rows[0]["PackageID"].ToString() + ",@Action='emi',@TransactionId='" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "'");
                        if (dtsell.Rows.Count > 0)
                            cLSM.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(dtsell.Rows[0]["NetAmount"].ToString()), "Cr", "Mpos TXN:" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "");
                        else
                            cLSM.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"].ToString()), "Cr", "Mpos TXN:" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "");
                    }
                    else if (Txntype == "cash")
                    {
                        dtsell = Cls.select_data_dt(@"EXEC SET_MPoS_Commission @memberid='" + memberid + "',@txnamount='" + Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"].ToString()) + "',@txnid='" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "'");
                        cLSM.AEPSWallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"].ToString()), "Cr", "Mpos TXN:" + ds.Tables[0].Rows[0]["transactionId"].ToString() + "");
                    }
                }
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("content-type", "application/json");
                Response.Write("{\"RESP_CODE\": 300,\"RESPONSE\": \"SUCCESS\", \"RESP_MSG\": \"Transaction Success\"}");
            }
            else
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("content-type", "application/json");
                Response.Write("{\"RESP_CODE\": 302, \"RESPONSE\": \"FAILED\", \"RESP_MSG\": \"Transaction Failed\"}");
            }
        }
        else
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("content-type", "application/json");
            Response.Write("{\"RESP_CODE\": 302, \"RESPONSE\": \"FAILED\", \"RESP_MSG\": \"Transaction Failed\"}");
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