using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

public partial class API_PayOutCallback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cls_connection Cls = new cls_connection();
            cls_myMember clsm = new cls_myMember();
            string input = string.Empty;
            using (var reader = new StreamReader(Request.InputStream))
            {
                input = reader.ReadToEnd();
            }
            if (input != string.Empty)
            {
                DataSet ds = Deserialize(input);
                if (ds.Tables["Result"].Rows[0]["MemberId"].ToString() == "EZ230611")
                {
                    DataTable dt = Cls.select_data_dt(@"SELECT * FROM t_Ezulix_PayOut_MoneyTransfer WHERE AgentOrderId='" + ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString() + "'");
                    if (dt.Rows.Count == 1 && dt.Rows[0]["status"].ToString() != "SUCCESS")
                    {
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@status", value = ds.Tables["Response"].Rows[0]["status"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@statusCode", value = ds.Tables["Response"].Rows[0]["statusCode"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@statusMessage", value = ds.Tables["Response"].Rows[0]["statusMessage"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@EzulixorderId", value = ds.Tables["Result"].Rows[0]["EzulixorderId"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@BankOrderId", value = ds.Tables["Result"].Rows[0]["BankOrderId"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@NetAmountEzulix", value = Convert.ToDecimal(ds.Tables["Result"].Rows[0]["NetAmount"].ToString()) });
                        _lstparm.Add(new ParmList() { name = "@rrn", value = ds.Tables["Result"].Rows[0]["rrn"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@AgentOrderId", value = ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@Action", value = "U" });
                        Cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer ", _lstparm);
                        if (ds.Tables["Response"].Rows[0]["status"].ToString() == "FAILURE")
                        {
                            string agentoid = ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString();
                            if (agentoid.Contains("PA"))
                            {
                               
                                DataTable dd = new DataTable();
                                string a = "Fail Xpress AEPS Payoyut Topup Txn: -" + ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString();
                                dd = Cls.select_data_dt("select * from tblmlm_rwallettransaction where narration='" + a + "' and Factor='Cr'");
                                if (dd.Rows.Count == 0)
                                {
                                    clsm.AEPSWallet_MakeTransaction_Ezulix(dt.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString()), "Cr", "Fail Xpress AEPS Payoyut Topup Txn:-" + ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString() + "");
                                }
                            }
                            else
                            {
                                clsm.Wallet_MakeTransaction(dt.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString()), "Cr", "Fail DMR Txn Topup Txn:-" + ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString() + "");
                            }
                        }
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.AddHeader("content-type", "application/json");
                        Response.Write("{\"CallBackResponseCode\": \"200\"}");
                    }
                }
                else
                {
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.AddHeader("content-type", "application/json");
                    Response.Write("{\"CallBackResponseCode\": \"400\"}");
                }
            }
            else
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("content-type", "application/json");
                Response.Write("{\"CallBackResponseCode\": \"Invalid Reuqest\"}");
            }
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("content-type", "application/json");
            Response.Write("{\"CallBackResponseCode\": \"Invalid Reuqest\"}");
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