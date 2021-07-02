using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

public partial class AppUpi_Paymentcallback : System.Web.UI.Page
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
            string OperationName = Request.Form["operationname"].ToString();
            if (OperationName == "upigatewayadd")
                if (Request.Form["MemberID"] != null && Request.Form["MsrNo"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    cls_myMember clsm = new cls_myMember();
                    DataTable dtMember = cls.select_data_dt("select * from tblmlm_membermaster where msrno='" + Request.Form["MsrNo"] + "'");
                    string Mobile = dtMember.Rows[0]["Mobile"].ToString();
                    string Name = Request.Form["Name"].ToString();
                    string CustomerID = clsm.Cyrus_GetTransactionID_New();
                    string Email = dtMember.Rows[0]["Email"].ToString();
                    string City = dtMember.Rows[0]["CityName"].ToString();
                    string PinCode = Request.Form["Pincode"].ToString();
                    string Address = dtMember.Rows[0]["Address"].ToString();
                    int MsrNo = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]);
                    int MemberTypeID = Convert.ToInt32(dtMember.Rows[0]["MemberTypeID"]);
                    string MemberID = dtMember.Rows[0]["MemberID"].ToString();
                    string token = "Vq6BphVCp17sLEkXd8ezBzh3GT6Fgp";
                    string parameter = "token=" + token + "&CustomerID=" + CustomerID + "&Name=" + Name + "&Mobile=" + Mobile + "&Email=" + Email + "&City=" + City + "&PinCode=" + PinCode + "&Address=" + Address;
                    string url = "http://payu.startrecharge.in/QRCode/AccountGenerate" + "?" + parameter;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    WebRequest request = WebRequest.Create(url);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    WebResponse response = request.GetResponse();
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        //  clsiapplog.PrintLog("APIRequest(KYC) : " + responseFromServer + "");
                        DataSet ds = Deserialize(responseFromServer);
                        if (ds.Tables["root"].Rows[0]["Error"].ToString() == "200")
                        {
                            if (ds.Tables["root"].Rows[0]["Status"].ToString() == "Success")
                            {
                                string Customer_ID = ds.Tables["data"].Rows[0]["CustomerID"].ToString();
                                string QRID = ds.Tables["data"].Rows[0]["QRID"].ToString();
                                string QrString = ds.Tables["data"].Rows[0]["QrString"].ToString();
                                string MerchantVPA = ds.Tables["data"].Rows[0]["MerchantVPA"].ToString();
                                List<ParmList> _lstparm = new List<ParmList>();
                                _lstparm.Add(new ParmList() { name = "@MemberId", value = MemberID });
                                _lstparm.Add(new ParmList() { name = "@MemberTypeID", value = MemberTypeID });
                                _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
                                _lstparm.Add(new ParmList() { name = "@amount", value = 0 });
                                _lstparm.Add(new ParmList() { name = "@client_vpa", value = "" });
                                _lstparm.Add(new ParmList() { name = "@Statuss", value = "Pending" });
                                _lstparm.Add(new ParmList() { name = "@mobile", value = dtMember.Rows[0]["Mobile"] });
                                _lstparm.Add(new ParmList() { name = "@email", value = dtMember.Rows[0]["Email"] });
                                _lstparm.Add(new ParmList() { name = "@client_name", value = Name });
                                _lstparm.Add(new ParmList() { name = "@mode", value = "HDFC Bank" });
                                _lstparm.Add(new ParmList() { name = "@City", value = dtMember.Rows[0]["CityName"] });
                                _lstparm.Add(new ParmList() { name = "@pincode", value = PinCode });
                                _lstparm.Add(new ParmList() { name = "@Addresss", value = dtMember.Rows[0]["Address"] });
                                _lstparm.Add(new ParmList() { name = "@client_txn_id", value = Customer_ID });
                                _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                                Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparm);
                                string Requcest_Json = responseFromServer.Replace(@"\", "");
                                Response.Write(Requcest_Json);
                            }
                            else
                            {
                                ReturnError("wrong Data !!", "token");
                            }
                        }
                        else
                        {
                            ReturnError(" Missing Data !!", "token");
                        }
                    }
                }
                else
                {
                    ReturnError("Some Error Found exists in system !!", "token");
                }
        }
        else
        {
            ReturnError("Some Error Found !!", "token");
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