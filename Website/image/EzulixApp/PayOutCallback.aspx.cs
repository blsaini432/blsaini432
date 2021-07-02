using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Configuration;
using BLL;

public partial class API_PayOutCallback : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            string input = string.Empty;
            using (var reader = new StreamReader(Request.InputStream))
            {
                input = reader.ReadToEnd();
            }
            if (input != string.Empty)
            {
                DataSet ds = Deserialize(input);
                if (ds.Tables["Result"].Rows[0]["MemberId"].ToString() == "EZ113916")
                {
                    string id = ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString();
                    DataTable dt=cls.select_data_dt("select * from t_Ezulix_PayOut_MoneyTransfer where AgentOrderId='" + id + "'");
                       
                    if (dt.Rows.Count == 1 && dt.Rows[0]["status"].ToString() != "SUCCESS")
                    {
                       
                        List <ParmList> _lstparm = new List<ParmList>();
                  
                        _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(0) });
                        _lstparm.Add(new ParmList() { name = "@AgentOrderId", value = ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(0) });
                        _lstparm.Add(new ParmList() { name = "@beneficiaryAccount", value = "" });
                        _lstparm.Add(new ParmList() { name = "@beneficiaryIFSC", value = "" });
                        _lstparm.Add(new ParmList() { name = "@EzulixorderId", value = ds.Tables["Result"].Rows[0]["EzulixorderId"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@BankOrderId", value = ds.Tables["Result"].Rows[0]["BankOrderId"].ToString() });

                        
                        _lstparm.Add(new ParmList() { name = "@status", value = ds.Tables["Response"].Rows[0]["status"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@statusCode", value = ds.Tables["Response"].Rows[0]["statusCode"].ToString(), });
                        _lstparm.Add(new ParmList() { name = "@statusMessage", value = ds.Tables["Response"].Rows[0]["statusMessage"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@NetAmountEzulix", value = Convert.ToDecimal(ds.Tables["Result"].Rows[0]["NetAmount"].ToString()) });
                        _lstparm.Add(new ParmList() { name = "@Action", value = "U" });
                        _lstparm.Add(new ParmList() { name = "@rrn", value = ds.Tables["Result"].Rows[0]["rrn"].ToString()});
                        cls.select_data_dtNew("SET_t_Ezulix_PayOut_MoneyTransfer", _lstparm);

                       
                        if (ds.Tables[0].Rows[0]["status"].ToString() == "FAILURE")
                        {
                            cls_myMember Clsm = new cls_myMember();
                            Clsm.Wallet_MakeTransaction(dt.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString()), "Cr", "Xpress DMR Fail Txn:- " + ds.Tables["Result"].Rows[0]["AgentOrderId"].ToString() + "");
                            
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