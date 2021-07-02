using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

public partial class AppUpi_paytm_Paymentcallback : System.Web.UI.Page
{
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
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
        if (Request.Form["operationname"] != null)
        {
            cls_connection Cls = new cls_connection();
            cls_myMember Clsm = new cls_myMember();
            string OperationName = Request.Form["operationname"].ToString();
            if (OperationName == "upipaytm")
            {
                if (Request.Form["MemberID"] != null && Request.Form["MsrNo"] != null)
                {

                    string Out = String.Empty;
                    DataTable dtMember = cls.select_data_dt("select * from tblmlm_membermaster where msrno='" + Request.Form["MsrNo"] + "'");
                    string Mobile = dtMember.Rows[0]["Mobile"].ToString();
                    string amount = Request.Form["Amount"].ToString();
                    string TxnID = Request.Form["Txnid"].ToString();
                    string token = "Vq6BphVCp17sLEkXd8ezBzh3GT6Fgp";
                    string StartID = "API713068";
                    string UserID = dtMember.Rows[0]["Memberid"].ToString();
                    string Json = "{\"amount\": \"" + amount + "\",\"UserID\":\"" + UserID + "\",\"Tokenkey\":\"" + token + "\",\"StartID\":\"" + StartID + "\",\"TxnID\":\"" + TxnID + "\"}";
                    string Url = "https://payu.startrecharge.in/QRCollect/Payment";
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Accept = "application/json";
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Timeout = 100000;
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(Json);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        Out = streamReader.ReadToEnd();
                    }
                    string myresponse = Out.ToString();

                    DataSet ds = Deserialize(myresponse);
                    if (ds.Tables["root"].Rows[0]["Statuscode"].ToString() == "1")
                    {
                        if (ds.Tables["root"].Rows[0]["Status"].ToString() == "Success")
                        {
                            //  string Customer_ID = ds.Tables["root"].Rows[0]["CustomerID"].ToString();
                            string QRID = ds.Tables["root"].Rows[0]["qrcodeID"].ToString();
                            string QrString = ds.Tables["root"].Rows[0]["qrData"].ToString();
                            string image = ds.Tables["root"].Rows[0]["image"].ToString();
                            int MsrNo = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]);
                            int MemberTypeID = Convert.ToInt32(dtMember.Rows[0]["MemberTypeID"]);
                            string MemberID = dtMember.Rows[0]["MemberID"].ToString();
                            List<ParmList> _lstparm = new List<ParmList>();
                            _lstparm.Add(new ParmList() { name = "@MemberId", value = MemberID });
                            _lstparm.Add(new ParmList() { name = "@MemberTypeID", value = MemberTypeID });
                            _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
                            _lstparm.Add(new ParmList() { name = "@amount", value = amount });
                            _lstparm.Add(new ParmList() { name = "@client_vpa", value = "" });
                            _lstparm.Add(new ParmList() { name = "@Statuss", value = "Pending" });
                            _lstparm.Add(new ParmList() { name = "@mobile", value = dtMember.Rows[0]["Mobile"] });
                            _lstparm.Add(new ParmList() { name = "@email", value = dtMember.Rows[0]["Email"] });
                            _lstparm.Add(new ParmList() { name = "@mode", value = "Paytm Bank" });
                            _lstparm.Add(new ParmList() { name = "@client_name", value = dtMember.Rows[0]["Firstname"].ToString() });
                            _lstparm.Add(new ParmList() { name = "@City", value = dtMember.Rows[0]["CityName"] });
                            //_lstparm.Add(new ParmList() { name = "@pincode", value = PinCode });
                            _lstparm.Add(new ParmList() { name = "@Addresss", value = dtMember.Rows[0]["Address"] });
                            _lstparm.Add(new ParmList() { name = "@client_txn_id", value = TxnID });
                            _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                            Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparm);
                            // QRCODE.Visible = true;
                            ViewState["QrString"] = "data:image/gif;base64," + image;
                            Session["TxnID"] = TxnID.ToString();
                            string Requcest_Json = myresponse.Replace(@"\", "");
                            Response.Write(Requcest_Json);
                            //  upi.Visible = false;

                        }
                        else
                        {
                            ReturnError("Some Error Found !!", "token");
                        }
                    }
                    else
                    {
                        ReturnError("Some Error Found !!", "token");
                    }
                }
            }
            else if(OperationName == "checkstatuspaytm")
            {
                string token = "Vq6BphVCp17sLEkXd8ezBzh3GT6Fgp";
                string StartID = "API713068";
                string Out = String.Empty;
                string TxnID = Request.Form["Txnid"].ToString();
                DataTable orderid = Cls.select_data_dt("select * from tbl_UPI_paymentGateway where client_txn_id='" + TxnID + "' and statuss='Pending'");
                if (orderid.Rows.Count > 0)
                {
                    string MemberId = orderid.Rows[0]["MemberId"].ToString();
                    string MemberTypeID = orderid.Rows[0]["MemberTypeID"].ToString();
                    string Json = "{\"Tokenkey\":\"" + token + "\",\"StartID\":\"" + StartID + "\",\"TxnID\":\"" + TxnID + "\"}";
                    string Url = "https://payu.startrecharge.in/QRCollect/PaymentStatus";
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Accept = "application/json";
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Timeout = 100000;
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(Json);
                    }
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        Out = streamReader.ReadToEnd();
                    }
                    string myresponse = Out.ToString();
                    DataSet ds = Deserialize(myresponse);
                    if (ds.Tables["root"].Rows[0]["Statuscode"].ToString() == "1")
                    {
                        if (ds.Tables["root"].Rows[0]["Status"].ToString() == "Success")
                        {
                            string amount = ds.Tables["root"].Rows[0]["txnAmount"].ToString();
                            List<ParmList> _lstparms = new List<ParmList>();
                            _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["root"].Rows[0]["orderId"].ToString() });
                            _lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["root"].Rows[0]["Status"].ToString() });
                            _lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["root"].Rows[0]["Message"].ToString() });
                            _lstparms.Add(new ParmList() { name = "@client_txn_id", value = TxnID });
                            _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                            Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                            int balance = Clsm.Wallet_MakeTransaction_Ezulix(MemberId, Convert.ToDecimal(amount), "Cr", "Add Fund UPI PG Order ID : " + TxnID);
                            ReturnError("Transaction Success !!", "token");
                        }
                    }
                    else
                    {
                        ReturnError("Some Error Found 1 !!", "token");
                        
                    }
                }
                else
                {
                    ReturnError("Some Error Found 2!! ", "token");
                 
                }
            }
        }
        else
        {
            ReturnError("Some Error Found 3!! ", "token");
        }
    }
  
    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
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