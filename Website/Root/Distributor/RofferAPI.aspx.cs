using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Razorpay.Api;
using System.Net;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using Paytm;
using System.IO;
using System.Xml;
public partial class Root_Distributor_RofferAPI : System.Web.UI.Page
{
    #region 
    cls_connection cls = new cls_connection();
    public string orderId;
    public decimal amount;
    public string name;
    public string txnToken;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtDistributor"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataTable dtMember = (DataTable)Session["dtDistributor"];
                    ViewState["MemberId"] = null;
                    ViewState["MsrNo"] = null;
                    ViewState["dmtmobile"] = null;
                    ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                    ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        String number = "9166396947";
        string username = "501859";
        string token = "8338ab1fafa3e46b3b4549477e352805";
        string opcode = "V";
        // string post_data = JsonConvert.SerializeObject(requestBody);
        string parameter = "username=" + username + "&token=" + token + "&number=" + number + "&opcode=" + opcode;
        string url = "https://myrc.in/plan_api/r_offer" + "?" + parameter;

        //For  Production 
        //string  url  =  "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=YOUR_MID_HERE&orderId=ORDERID_98765";
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        WebRequest request = WebRequest.Create(url);
        // If required by the server, set the credentials.
        request.Credentials = CredentialCache.DefaultCredentials;

        // Get the response.
        WebResponse response = request.GetResponse();
        // Display the status.
        Console.WriteLine(((HttpWebResponse)response).StatusDescription);

        // Get the stream containing content r1eturned by the server.
        // The using block ensures the stream is automatically closed.
        using (Stream dataStream = response.GetResponseStream())
        {
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            // Console.WriteLine(responseFromServer);
            // string Request_Jsonold = responseFromServer.Replace(@"[", "{");
            //string requesst = Request_Jsonold.Replace(@"]", "}");

            DataSet ds = Deserialize(responseFromServer);
            DataTable dtOne = new DataTable();
            DataTable dtTwo = new DataTable();
            //foreach (DataRow dtrow in ds.Rows[0])
            //{
            //}
            dtOne = ds.Tables[0];
            List<ListResponse> list = new List<ListResponse>();
            if (dtOne.Rows.Count > 0)
            {
                foreach (DataRow item in dtOne.Rows)
                {
                    var cls = new ListResponse();
                    cls.rs = item["rs"].ToString();
                    cls.desc = item["desc"].ToString();
                    list.Add(cls);
                }
            }
           
            //string[] lines = ds;
            // DataSet dsAirLine = (DataSet)Session["FlightList"];
            // DataSet lines = ds ;

            //}
        }
    }
    public class ListResponse
    {
        public string rs { get; set; }
        public string desc { get; set; }
    }

    private DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = (System.Xml.XmlDocument)Newtonsoft.Json.JsonConvert.DeserializeXmlNode("{\"root \":" + result + "}", "root");
        //  XmlDocument doc = JsonConvert.DeserializeXmlNode("{\"Row\":" + result + "}", "root").ToXmlString();
        //   JsonConvert.DeserializeXmlNode(result, "root");
        // var doc = JsonExtensions.DeserializeXmlNode(result, "root", "array");
        // XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
}

