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

public partial class EzulixApp_Purchageservice : System.Web.UI.Page
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
            #region Purchageservice
            if (OperationName == "Purchageservice")
            {
                if (Request.Form["MemberId"] != null)
                {
                    int msrno = Convert.ToInt32(Request.Form["msrno"].ToString());
                    string service = ReplaceCode(Request.Form["servicename"].ToString().Trim());
                    cls_connection Cls = new cls_connection();
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select Packageid from tblmlm_membermaster  where msrno='" + msrno + "'");
                    if (dt.Rows.Count > 0)
                    {
                        int packageid = Convert.ToInt32(dt.Rows[0]["Packageid"]);
                        cls_connection objconnection = new cls_connection();
                        string Result = string.Empty;
                        dt = objconnection.select_data_dt("select Amount  from [tblmlm_manageservice] where PackageId='" + Convert.ToInt32(packageid) + "' and Servicename ='" + service + "'");
                        if (dt.Rows.Count > 0)
                        {
                            string AMOUNT = dt.Rows[0]["Amount"].ToString();                     
                            string output = ConvertDataTabletoString(dt);
                            Response.Write("{ " + OperationName + ":" + output + "}");
                        }
                        else
                        {
                            ReturnError("Service  Disable!", "Unknown");
                        }
                    }
                    else
                    {
                        ReturnError("Some Error Found", "Unknown");
                    }
                }
            }

            else if (OperationName == "purchagreport")
            {
                #region MemberReport
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string memberid = string.Empty;
                    memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    int msrNo = cls.select_data_scalar_int(@"select MsrNo from tblmlm_membermaster where MemberID='" + memberid + "'");
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@msrno", value = msrNo });
                    _lstparm.Add(new ParmList() { name = "@datefrom", value = "01-01-1990" });
                    _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
                    DataTable dttra = Cls.select_data_dtNew(@"paymentgateway_report", _lstparm);
                    if (dttra.Rows.Count > 0)
                    {
                        string output = ConvertDataTabletoString(dttra);
                        Response.Write("{ " + OperationName + ":" + output + "}");
                    }
                    else
                    {
                        ReturnError("No Transcation found", "Unknown");
                    }
                }

                #endregion
            }

            else if (OperationName == "pgcallbackdata")
            {
                #region PGSavedata
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["memberid"] != null)
                {
                    cls_connection Cls = new cls_connection();
                    string memberid = string.Empty;
                    DataTable dt = new DataTable();                 
                    memberid = ReplaceCode(Request.Form["memberid"].ToString().Trim());
                    string orderId = ReplaceCode(Request.Form["orderId"].ToString().Trim());
                    string amount = ReplaceCode(Request.Form["amount"].ToString().Trim());
                    string txnid = ReplaceCode(Request.Form["txnid"].ToString().Trim());
                    string payid = ReplaceCode(Request.Form["payid"].ToString().Trim());
                    string servicename = ReplaceCode(Request.Form["servicename"].ToString().Trim());
                    dt = cls.select_data_dt("select * from tblmlm_membermaster  where memberid='" + memberid + "'");
                    if (dt.Rows.Count > 0)
                    {
                        cls.insert_data("Exec dbo.insertpaymentGateway null,'" + dt.Rows[0]["MemberID"].ToString() + "', '" + dt.Rows[0]["MsrNo"].ToString() + "' , '" + orderId + "','" + payid + "', '" + null + "' , '" + "captured" + "','" + amount + "','" + "null" + "', '" + "null" + "' , '" + "null" + "' , '" + "AddWallet" + "', '" + dt.Rows[0]["Mobile"].ToString() + "','" + dt.Rows[0]["Email"].ToString() + "','" + txnid + "','" + servicename + "'");
                        Response.Write("{" + OperationName + ":[{\"RESPONSE\": \"SUCCESS\", \"RESP_MSG\": \"Success|Record inserted successfully\"}]}");
                       
                    }
                    else
                    {
                        ReturnError("No Transcation found", "Unknown");
                    }


                }

                #endregion

            }
            #endregion

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

}