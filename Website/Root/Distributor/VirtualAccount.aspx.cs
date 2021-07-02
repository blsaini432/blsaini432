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
using System.Net;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;

public partial class Root_Distributor_VirtualAccount : System.Web.UI.Page
{
    private static int limitamount = 5000;
    private Boolean IsPageRefresh = false;
    #region Access_Class
    VirtualAccount VirtualAccount = new VirtualAccount();
    cls_connection Cls = new cls_connection();
    DataTable dtmembermaster = new DataTable();
    private static string AID = "SSDT123456";
    private static string OP = "DMTNUR";
    private static string ST = "REMDOMESTIC";
    private static string request = "BENVERIFICATION";
    private static string benidelete = "BENDELETE";
    private static string customerequest = "CUSTVERIFICATION";
    private static string mm_token = "fb908720-f931-4a2c-b994-868aa349595a";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //    String Result = string.Empty;
            //    string op = "bank_account";
            //    string description = "Virtual Account created for Raftar Soft";
            //    string customer_id = "cust_CaVDm8eDRSXYME";
            //    string project = "Banking Software";
            //    DataSet ds = new DataSet();
            //    // string Result = "{\"entity\":\"collection\",\"count\":3,\"items\":[{\"id\":\"va_G3QKzoMQ9aM5oI\",\"name\":\"ezulix\",\"entity\":\"virtual_account\",\"status\":\"activ\",\"description\":null,\"amount_expected\":null,\"notes\":[],\"amount_paid\":0,\"customer_id\":null,\"receivers\":[{\"id\":\"ba_G3QKzu9hobpQOU\",\"entity\":\"bank_account\",\"ifsc\":\"RAZR0000001\",\"bank_name\":null,\"name\":\"ezulix\",\"notes\":[],\"account_number\":\"1112220005651692\"}],\"close_by\":null,\"closed_at\":null,\"created_at\":1605875559},{\"id\":\"va_G316fO2fmG0R5s\",\"name\":\"ezulix\",\"entity\":\"virtual_account\",\"status\":\"active\",\"description\":null,\"amount_expected\":null,\"notes\":[],\"amount_paid\":0,\"customer_id\":null,"receivers\":[{\"id":"ba_G316fUXkSrjQNI","entity":"bank_account","ifsc":"RAZR0000001","bank_name":null,"name":"ezulix","notes\":[],\"account_number":"1112220042325643\"}],\"close_by":null,"closed_at":null,"created_at\":1605786705},{\"id":"va_G30itokfAEskMU","name":"ezulix","entity":"virtual_account","status":"active","description":null,"amount_expected":null,"notes\":[],\"amount_paid":0,"customer_id":null,"receivers\":[{\"id":"ba_G30itvglk4sDjh","entity":"bank_account","ifsc":"RAZR0000001","bank_name":null,"name":"ezulix","notes\":[],\"account_number":"1112220007592631\"}],\"close_by":null,"closed_at":null,"created_at\":1605785355}]}";
            //   // Result = VirtualAccount.VirtualAccounts(op, description, customer_id, project);
            //    // DataSet dsx = Deserialize(Result);

            //    string Json = "{\"receivers\": {\"types\": [\"" + OP + "\"]},\"description\": \"" + description + "\",\"customer_id\": \"" + customer_id + "\",\"close_by\":  1681615838  ,\"notes\": {\"project_name \": \"" + project + "\"}}";

            //    string jsonData = Json.Replace(@"\", "");
            //    //    //string jsonDataa = Json.Replace("}", "}}");
            //    // return HTTP_POSTpay(jsonData);
            //    //return HTTP_POST(M_Uri, jsonData);

            //    string Out = String.Empty;
            //    try
            //    {


            //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            //        // WebRequest httpWebRequest = WebRequest.Create(Url);
            //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.razorpay.com/v1/virtual_accounts/");
            //        request.Method = "POST";
            //        request.ContentLength = 0;
            //        request.ContentType = "application/json";
            //        String key = "rzp_test_URWNybLYvGAURx";
            //        String secrets = "qYqGduo0XTIyv3tfJyfeaps5";
            //        string authString = string.Format("{0}:{1}", key, secrets);
            //        request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
            //        var paymentResponse = (HttpWebResponse)request.GetResponse();
            //        // public string paymentStatus;
            //        using (var streamReader = new StreamReader(paymentResponse.GetResponseStream()))
            //        {
            //            Out = streamReader.ReadToEnd();
            //        }

            //        //JObject paymentStatusData = ParseResponse(paymentResponse);
            //        // paymentStatus = paymentStatusData["status"].ToString();


            //    }
            //    catch (WebException ex)
            //    {
            //        var httpResponse = (HttpWebResponse)ex.Response;
            //        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //        {
            //            Out = streamReader.ReadToEnd();
            //        }
            //    }
            //    string myresponse = Out.ToString();
            //    DataSet dss = Deserialize(myresponse);
            //}
        }

    }
    public static string HTTP_POSTpay(string Data)
    {
        string Out = String.Empty;
        try
        {


            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            // WebRequest httpWebRequest = WebRequest.Create(Url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.razorpay.com/v1/virtual_accounts/" + "va_G4YpEUCsFVPMBG");
            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "application/json";
            String key = "rzp_test_URWNybLYvGAURx";
            String secrets = "qYqGduo0XTIyv3tfJyfeaps5";
            string authString = string.Format("{0}:{1}", key, secrets);
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));
            var paymentResponse = (HttpWebResponse)request.GetResponse();
            // public string paymentStatus;
            using (var streamReader = new StreamReader(paymentResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }

            //JObject paymentStatusData = ParseResponse(paymentResponse);
            // paymentStatus = paymentStatusData["status"].ToString();


        }
        catch (WebException ex)
        {
            var httpResponse = (HttpWebResponse)ex.Response;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
        }
        string myresponse = Out.ToString();
        return myresponse;
        // DataSet ds = Deserialize(myresponse);
    }


    #region Search Customer 
    protected void btn_login_Click1(object sender, EventArgs e)
    {
        try
        {
            String Result = string.Empty;
            string op = "bank_account";
            string description = "Virtual Account created for Raftar Soft";
            string customer_id = "cust_CaVDm8eDRSXYME";
            string project = "Banking Software";
            Result = VirtualAccount.VirtualAccounts(op, description, customer_id, project);
            //  ResetBeni()
            Result = "";
            if (Result != string.Empty)
            {


            }
        }
        catch (Exception ex)
        {
            //   lblerror.Text = ex.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something is wrong, Contact to your admin');window.location ='DashBoard.aspx';", true);
        }

    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        String Result = string.Empty;
        string op = "bank_account";
        string description = "Virtual Account created for Raftar Soft";
        string customer_id = "cust_CaVDm8eDRSXYME";
        string project = "Banking Software";
        DataSet ds = new DataSet();
        // string Result = "{\"entity\":\"collection\",\"count\":3,\"items\":[{\"id\":\"va_G3QKzoMQ9aM5oI\",\"name\":\"ezulix\",\"entity\":\"virtual_account\",\"status\":\"activ\",\"description\":null,\"amount_expected\":null,\"notes\":[],\"amount_paid\":0,\"customer_id\":null,\"receivers\":[{\"id\":\"ba_G3QKzu9hobpQOU\",\"entity\":\"bank_account\",\"ifsc\":\"RAZR0000001\",\"bank_name\":null,\"name\":\"ezulix\",\"notes\":[],\"account_number\":\"1112220005651692\"}],\"close_by\":null,\"closed_at\":null,\"created_at\":1605875559},{\"id\":\"va_G316fO2fmG0R5s\",\"name\":\"ezulix\",\"entity\":\"virtual_account\",\"status\":\"active\",\"description\":null,\"amount_expected\":null,\"notes\":[],\"amount_paid\":0,\"customer_id\":null,"receivers\":[{\"id":"ba_G316fUXkSrjQNI","entity":"bank_account","ifsc":"RAZR0000001","bank_name":null,"name":"ezulix","notes\":[],\"account_number":"1112220042325643\"}],\"close_by":null,"closed_at":null,"created_at\":1605786705},{\"id":"va_G30itokfAEskMU","name":"ezulix","entity":"virtual_account","status":"active","description":null,"amount_expected":null,"notes\":[],\"amount_paid":0,"customer_id":null,"receivers\":[{\"id":"ba_G30itvglk4sDjh","entity":"bank_account","ifsc":"RAZR0000001","bank_name":null,"name":"ezulix","notes\":[],\"account_number":"1112220007592631\"}],\"close_by":null,"closed_at":null,"created_at\":1605785355}]}";
        Result = VirtualAccount.VirtualAccounts(op, description, customer_id, project);
        DataSet dsx = Deserialize(Result);
        string Reseult = string.Empty;

    }

    private static DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }

}