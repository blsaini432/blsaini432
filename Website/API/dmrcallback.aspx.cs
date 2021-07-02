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

public partial class API_dmrcallback : System.Web.UI.Page
{
    cls_connection Cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        string input = string.Empty;
        using (var reader = new StreamReader(Request.InputStream))
        {
            input = reader.ReadToEnd();
        }
        if (input != string.Empty)
        {
            DataSet ds = Deserialize(input);
            try
            {
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@ref_no", value = ds.Tables[0].Rows[0]["ref_no"].ToString() });
                _lstparm.Add(new ParmList() { name = "@statuscode", value = ds.Tables[0].Rows[0]["statuscode"].ToString() });
                _lstparm.Add(new ParmList() { name = "@status_", value = ds.Tables[0].Rows[0]["status_"].ToString() });
                _lstparm.Add(new ParmList() { name = "@Memberid", value = ds.Tables[0].Rows[0]["agent_member_id"].ToString() });
                _lstparm.Add(new ParmList() { name = "@ipay_id", value = ds.Tables[0].Rows[0]["ipay_id"].ToString() });
                //Cls.select_data_dtNew("PROC_DMR_CALLBACK ", _lstparm);
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("content-type", "application/json");
                Response.Write("{\"RESP_CODE\": 300,\"RESPONSE\": \"SUCCESS\", \"RESP_MSG\": \"Transaction Success\"}");
            }
            catch (Exception ex)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.AddHeader("content-type", "application/json");
                Response.Write("{\"RESP_CODE\": 302, \"RESPONSE\": \"FAILED\", \"RESP_MSG\": \"Transaction Failed\"}");
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
}