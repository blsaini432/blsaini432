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
using System.Globalization;
using BLL.MLM;

public partial class EzulixApp_downlinebalance : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    clsMLM_RWalletTransaction objRWalletTransaction = new clsMLM_RWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    private static int limitamount = 5000;
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
            if (OperationName == "Downline")
            {
                #region Downline
                if (Request.Form["msrno"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    int msrno = Convert.ToInt32(Request.Form["msrno"]);
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    DataTable dtEWalletBalance = new DataTable();
                    DataTable dt = new DataTable();
                    dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetByMsrNo", msrno);
                    string output = ConvertDataTabletoString(dtEWalletBalance);
                    Response.Write("{ " + OperationName + ":" + output + "}");
                    #endregion
                }
                else
                {
                    ReturnError("Valide data !!", "Downline");
                }
            }
            if (OperationName == "Ewallettransaction")
            {
                #region  Ewallettransaction
                if (Request.Form["msrno"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    int msrno = Convert.ToInt32(Request.Form["msrno"]);
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    DataTable dtEWalletTransaction = new DataTable();
                    DataTable dt = new DataTable();
                    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@Action", value = "GetBydatewise" });
                    _lstparm.Add(new ParmList() { name = "@Id", value = msrno });
                    _lstparm.Add(new ParmList() { name = "@fromdate", value = "01-01-1990" });
                    _lstparm.Add(new ParmList() { name = "@todate", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
                    dtEWalletTransaction = cls.select_data_dtNew("ProcMLM_ManageEWalletTransaction", _lstparm);
                    if (dtEWalletTransaction.Rows.Count > 0)
                    {
                        string output = ConvertDataTabletoString(dtEWalletTransaction);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("No Transcation found", "Unknown");
                    }
                    #endregion
                }
                else
                {
                    ReturnError("Valide data !!", "Downline");
                }
            }
        }
    }
    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoString(dt);
        Response.Write("{ " + operationName + ":" + output + "}");
    }
}