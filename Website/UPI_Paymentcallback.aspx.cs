using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
public partial class UPI_Paymentcallback : System.Web.UI.Page
{
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    string key = "4dc44306-9ab4-48f2-8176-676900de52ee";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["client_txn_id"] != null && Request.QueryString["txn_id"] != null)
            {
                string client_txn_id = Request.QueryString["client_txn_id"];
                DataTable orderid = cls.select_data_dt("select * from tbl_UPI_paymentGateway where client_txn_id='" + client_txn_id + "' and statuss='Pending' and mode IS NULL");
                if (orderid.Rows.Count > 0)
                {
                    string MemberId = orderid.Rows[0]["MemberId"].ToString();
                    string MemberTypeID = orderid.Rows[0]["MemberTypeID"].ToString();
                    string date = DateTime.Now.ToString("dd-MM-yyyy");
                    string parameter = "client_key=" + key + "&client_txn_id=" + client_txn_id + "&txn_date=" + date;
                    string url = "https://upigateway.com/api/check_order_status" + "?" + parameter;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    WebRequest request = WebRequest.Create(url);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse response = request.GetResponse();
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        DataSet ds = Deserialize(responseFromServer);
                        if (ds.Tables["root"].Rows[0]["status"].ToString() == "true")
                        {
                            if (ds.Tables["data"].Rows[0]["status"].ToString() == "success")
                            {
                                string amount = ds.Tables["data"].Rows[0]["amount"].ToString();
                                List<ParmList> _lstparms = new List<ParmList>();
                                _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["data"].Rows[0]["upi_txn_id"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["data"].Rows[0]["status"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["data"].Rows[0]["remark"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@client_txn_id", value = client_txn_id });
                                _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                                cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                                int balance = clsm.Wallet_MakeTransaction_Ezulix(MemberId, Convert.ToDecimal(amount), "Cr", "Add Fund UPI PG Order ID : " + client_txn_id);
                                if (MemberTypeID == "5")
                                {
                                    Response.Redirect("~/Root/Retailer/DashBoard.aspx");
                                }
                                else if (MemberTypeID == "4")
                                {
                                    Response.Redirect("~/Root/Distributor/DashBoard.aspx");
                                }

                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Success !!');", true);
                            }
                            else
                            {
                                List<ParmList> _lstparms = new List<ParmList>();
                                _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["data"].Rows[0]["upi_txn_id"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["data"].Rows[0]["status"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["data"].Rows[0]["remark"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@client_txn_id", value = client_txn_id });
                                _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                                cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                                if (MemberTypeID == "5")
                                {
                                    Response.Redirect("~/Root/Retailer/DashBoard.aspx");
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction failure !!');", true);
                                }
                                else if (MemberTypeID == "4")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction failure !!');", true);
                                    Response.Redirect("~/Root/Distributor/DashBoard.aspx");

                                }

                            }
                        }
                        else
                        {
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found !!');", true);
                            if (MemberTypeID == "5")
                            {
                                Response.Redirect("~/Root/Retailer/DashBoard.aspx");
                            }
                            else if (MemberTypeID == "4")
                            {
                                Response.Redirect("~/Root/Distributor/DashBoard.aspx");
                            }
                        }
                    }
                }
                else
                {
                    DataTable order = cls.select_data_dt("select * from tbl_UPI_paymentGateway where client_txn_id='" + client_txn_id + "' and statuss='Pending' and mode ='app'");
                    string MemberId = order.Rows[0]["MemberId"].ToString();
                    string MemberTypeID = order.Rows[0]["MemberTypeID"].ToString();
                    string date = DateTime.Now.ToString("dd-MM-yyyy");
                    string parameter = "client_key=" + key + "&client_txn_id=" + client_txn_id + "&txn_date=" + date;
                    string url = "https://upigateway.com/api/check_order_status" + "?" + parameter;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    WebRequest request = WebRequest.Create(url);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse response = request.GetResponse();
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        DataSet ds = Deserialize(responseFromServer);
                        if (ds.Tables["root"].Rows[0]["status"].ToString() == "true")
                        {
                            if (ds.Tables["data"].Rows[0]["status"].ToString() == "success")
                            {
                                string amount = ds.Tables["data"].Rows[0]["amount"].ToString();
                                List<ParmList> _lstparms = new List<ParmList>();
                                _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["data"].Rows[0]["upi_txn_id"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["data"].Rows[0]["status"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["data"].Rows[0]["remark"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@client_txn_id", value = client_txn_id });
                                _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                                cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                                int balance = clsm.Wallet_MakeTransaction_Ezulix(MemberId, Convert.ToDecimal(amount), "Cr", "Add Fund UPI PG Order ID : " + client_txn_id);
                                ReturnError("Transaction Successfully  !!", "token");
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Success !!');", true);
                            }
                            else
                            {
                                List<ParmList> _lstparms = new List<ParmList>();
                                _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["data"].Rows[0]["upi_txn_id"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["data"].Rows[0]["status"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["data"].Rows[0]["remark"].ToString() });
                                _lstparms.Add(new ParmList() { name = "@client_txn_id", value = client_txn_id });
                                _lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                                cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                                ReturnError("Some Error Found exists in system !!", "token");
                            }
                        }
                    }
                }
                    
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Not Found !!');", true);

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

    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }

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

}