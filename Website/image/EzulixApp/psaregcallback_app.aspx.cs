using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Net;
using System.IO;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Text;
using System.Xml;



public partial class psaregcallback_app : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    SqlConnection con = new SqlConnection();

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

        if (Request.QueryString["operationname"] != null)
        {
            string OperationName = Request.QueryString["operationname"].ToString();
            if (OperationName == "RejectKyc")
            {

                #region insertkyc
                if (Request.QueryString["MsrNo"] != null && Request.QueryString["operationname"] != null)
                {

                    string MsrNo = ReplaceCode(Request.QueryString["MsrNo"].ToString().Trim());
                    string reason = ReplaceCode(Request.QueryString["rejection"].ToString().Trim());
                    DataTable chkck = new DataTable();
                    chkck = cls.select_data_dt("select memberid from Tbl_Psa_Reg where MsrNo='" + Convert.ToInt32(MsrNo) + "'");
                    if (chkck.Rows.Count > 0)
                    {
                        cls.update_data("Update Tbl_Psa_Reg  set Statu='Rejected',psaloginid='',rejection='" + reason + "' where MsrNo='" + Convert.ToInt32(MsrNo) + "'");
                        ReturnError("PSA Registrantion Status is updated successfully", "RejectKyc");
                    }
                    else
                    {
                        ReturnError("Error", "RejectKyc");
                    }
     
                }
                else
                {
                    ReturnError("Invalid Details", "RejectKyc");
                }
                #endregion
            }

            else if (OperationName == "ApproveKyc")
            {

                #region insertkyc
                if (Request.QueryString["MsrNo"] != null && Request.QueryString["operationname"] != null)
                {

                    string MsrNo = ReplaceCode(Request.QueryString["MsrNo"].ToString().Trim());
                    string psaloginid = ReplaceCode(Request.QueryString["psaloginid"].ToString().Trim());
                    DataTable chkck = new DataTable();
                    chkck = cls.select_data_dt("select memberid from Tbl_Psa_Reg where MsrNo='" + Convert.ToInt32(MsrNo) + "'");
                    if (chkck.Rows.Count > 0)
                    {
                        cls.update_data("Update Tbl_Psa_Reg  set Statu='Approved',psaloginid='" + psaloginid + "'where MsrNo='" + Convert.ToInt32(MsrNo) + "'");
                        ReturnError("PSA Registrantion Status is updated successfully", "ApproveKyc");
                    }

                }
                else
                {
                    ReturnError("Invalid Details", "ApproveKyc");
                }
                #endregion
            }
        }
        else
        {
            ReturnError("Invalid Request Format", "Unknown");
        }
    }
    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }
    protected void ReturnSucess(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 1 as ResponseCode,'" + message + "' as ResponseStatus");
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

    //convert image url to base64
    public static byte[] ConvertImageURLToBase64(String url)
    {
        StringBuilder _sb = new StringBuilder();
        _sb.Append("data:image/jpg;base64,");
        Byte[] _byte = GetImage(url);
        // _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));
        return _byte;
    }

    //get image from ImageUrl using webrequest
    private static byte[] GetImage(string url)
    {
        Stream stream = null;
        byte[] buf;

        try
        {
            WebProxy myProxy = new WebProxy();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            stream = response.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                int len = (int)(response.ContentLength);
                buf = br.ReadBytes(len);
                br.Close();
            }

            stream.Close();
            response.Close();
        }
        catch (Exception exp)
        {
            buf = null;
        }

        return (buf);
    }
}



