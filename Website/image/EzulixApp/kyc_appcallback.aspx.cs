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


public partial class EzulixApp_kyc_appcallback : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();


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
         string authmembertype = "3,4,5,6";
         if (Request.QueryString["operationname"] != null)
         {
             string OperationName = Request.QueryString["operationname"].ToString();
             //string msginfo = Request.QueryString["msginfo"].ToString().Replace("-", "").Replace("'", "");
             if (OperationName == "updatestatus")
             {
                 #region updatestatus
                 if (Request.QueryString["MsrNo"] != null && Request.QueryString["Statu"] != null && Request.QueryString["name"] != null && Request.QueryString["mobile"] != null)
                 {
                     int msrno = Convert.ToInt32(Request.QueryString["MsrNo"]);
                     string status = Request.QueryString["Statu"].ToString();
                     string rejection = Request.QueryString["rejection"].ToString();
                     string name = Request.QueryString["name"].ToString();
                     string mobile = Request.QueryString["mobile"].ToString();
                     DataTable dr = new DataTable();
                     dr = cls.select_data_dt("select * from Tbl_Aeps_Reg where MsrNo='" + msrno + "'");
                     if (dr.Rows.Count > 0)
                     {

                         if (status == "Rejected")
                         {
                             cls.select_data_dt(@"Update Tbl_Aeps_Reg set rejection='" + rejection + "',Statu='" + status + "',F_Name='" + name + "',Contact_Number='" + mobile + "'  Where MsrNo='" + msrno + "'");
                             ReturnError("Data Updated Successfully", "updatestatus");
                         }
                         else
                         {
                             cls.select_data_dt(@"Update Tbl_Aeps_Reg set Statu='" + status + "' Where MsrNo='" + msrno + "'");
                             ReturnError("Data Updated Successfully", "updatestatus");
                         }
                     }
                     else
                     {
                         ReturnError("Invalid Details", "updatestatus");
                     }
                 }
                 else
                 {
                     ReturnError("Invalid Details", "updatestatus");
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
}