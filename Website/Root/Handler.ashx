<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using System.Text;
using System.Web.Services;

public class Handler : IHttpHandler
{

    EzulixBus ibus = new EzulixBus();

    public void ProcessRequest(HttpContext context)
    {
        string callback = context.Request.QueryString["callback"];
        string customerId = string.Empty;

        string json = this.GetCustomersJSON(context.Request["customerId"]);
        if (!string.IsNullOrEmpty(callback))
        {
            json = string.Format("{0}({1});", callback, json);
        }
        context.Response.ContentType = "text/xml";
        context.Response.Write(json);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    public string GetCustomersJSON(string customerId)
    {
        DataSet myObjectDT = new DataSet();
        if (customerId != " ")
        {
            DataSet ds = new DataSet();
            string result = ibus.Seat_Layout(customerId);
            dynamic myObject = JsonConvert.DeserializeObject(result);
            //Using DataTable with JsonConvert.DeserializeObject, here you need to import using System.Data;  
            myObjectDT = Deserialize(result);

        }
        return myObjectDT.GetXml();
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
    //Call Method getuser()
}
