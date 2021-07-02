using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;

public partial class EzulixApp_bbpsapp : System.Web.UI.Page
{
    #region Properites
    cls_connection cls = new cls_connection();
    EzulixBBPSAPI eBbps = new EzulixBBPSAPI();
    cls_myMember clsm = new cls_myMember();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["OperationName"] != null)
        {
            string OperationName = Request.Form["OperationName"].ToString();
            if (OperationName == "elebiller")
            {
                #region BillerList
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec bbps_app_getdetails @action='listbiller',@mcode='" + mcode + "',@deviceid='" + deviceid + "',@loginip='" + loginip + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        dt.Columns.Remove("ResponseCode");
                        dt.Columns.Remove("ResponseStatus");
                        string json = ConvertDataTabletoJson(dt);
                        Response.Write("{ " + OperationName + ":" + json + "}");
                    }
                    else
                    {
                        string json = ConvertDataTabletoJson(dt);
                        Response.Write("{ " + OperationName + ":" + json + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", OperationName);
                }
                #endregion
            }
            else if (OperationName == "chkbiller")
            {
                #region CheckBiller
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["spkey"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string spkey = ReplaceCode(Request.Form["spkey"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec bbps_app_getdetails @action='chkbiller',@mcode='" + mcode + "',@deviceid='" + deviceid + "',@loginip='" + loginip + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        string Result = eBbps.bbps_biller(spkey);
                        Response.Write("{ " + OperationName + ":" + Result + "}");
                    }
                    else
                    {
                        string json = ConvertDataTabletoJson(dt);
                        Response.Write("{ " + OperationName + ":" + json + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", OperationName);
                }
                #endregion
            }
            else if (OperationName == "fetchbill")
            {
                #region FetchBill
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["spkey"] != null && Request.Form["account"] != null && Request.Form["custmermobile"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    string spkey = ReplaceCode(Request.Form["spkey"].ToString().Trim());
                    string account = ReplaceCode(Request.Form["account"].ToString().Trim());
                    string custmermobile = ReplaceCode(Request.Form["custmermobile"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec bbps_app_getdetails @action='chkbiller',@mcode='" + mcode + "',@deviceid='" + deviceid + "',@loginip='" + loginip + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        string agentid = clsm.Cyrus_GetTransactionID_New();
                        if (spkey == "DGE" || spkey == "MGE" || spkey == "PGE" || spkey == "UGE")
                        {
                            ReturnError("Bill Fetch facility is not avaliable for this operator", OperationName);
                        }
                        else if (spkey == "MDE")
                        {
                            if (Request.Form["billingunit"] != null)
                            {
                                string billingunit = ReplaceCode(Request.Form["billingunit"].ToString().Trim());
                                string Result = eBbps.bill_fetch_Maharshtra(spkey, agentid, custmermobile, "AGT", loginip, deviceid, "Cash", "bill", "", "", "24.1568,78.5263", "1878", account, billingunit);
                                Response.Write("{ " + OperationName + ":" + Result + "}");
                            }
                            else
                            {
                                ReturnError("Invalid Request Format", OperationName);
                            }
                        }
                        else if (spkey == "JBE")
                        {
                            if (Request.Form["jhasubcode"] != null)
                            {
                                string jhasubcode = ReplaceCode(Request.Form["jhasubcode"].ToString().Trim());
                                string Result = eBbps.bill_fetch_Jha(spkey, agentid, custmermobile, "AGT", loginip, deviceid, "Cash", "bill", "", "", "24.1568,78.5263", "1878", account, jhasubcode);
                                Response.Write("{ " + OperationName + ":" + Result + "}");
                            }
                            else
                            {
                                ReturnError("Invalid Request Format", OperationName);
                            }
                        }
                        else if (spkey == "AEE" || spkey == "ASE")
                        {
                            string Result = eBbps.bill_fetch(spkey, agentid, custmermobile, "AGT", loginip, deviceid, "Wallet", "Wallet|" + custmermobile + "", "", "", "24.1568,78.5263", "1878", account);
                            Response.Write("{ " + OperationName + ":" + Result + "}");
                        }
                        else
                        {
                            string Result = eBbps.bill_fetch(spkey, agentid, custmermobile, "AGT", loginip, deviceid, "Cash", "bill", "", "", "24.1568,78.5263", "1878", account);
                            Response.Write("{ " + OperationName + ":" + Result + "}");
                        }
                    }
                    else
                    {
                        string json = ConvertDataTabletoJson(dt);
                        Response.Write("{ " + OperationName + ":" + json + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", OperationName);
                }
                #endregion
            }
            else if (OperationName == "paybill")
            {
                #region PaymentBill
                if (Request.Form["mcode"] != null && Request.Form["deviceid"] != null && Request.Form["loginip"] != null && Request.Form["spkey"] != null && Request.Form["account"] != null && Request.Form["amount"] != null && Request.Form["custmermobile"] != null && Request.Form["refrenceid"] != null)
                {
                    string mcode = ReplaceCode(Request.Form["mcode"].ToString().Trim());
                    string deviceid = ReplaceCode(Request.Form["deviceid"].ToString().Trim());
                    string loginip = ReplaceCode(Request.Form["loginip"].ToString().Trim());
                    DataTable dt = cls.select_data_dt("Exec bbps_app_getdetails @action='paybill',@mcode='" + mcode + "',@deviceid='" + deviceid + "',@loginip='" + loginip + "'");
                    if (dt.Rows[0]["ResponseCode"].ToString() == "1")
                    {
                        string agentid = clsm.Cyrus_GetTransactionID_New();
                        string spkey = ReplaceCode(Request.Form["spkey"].ToString().Trim());
                        object account = ReplaceCode(Request.Form["account"].ToString().Trim());
                        decimal amount = Convert.ToDecimal(ReplaceCode(Request.Form["amount"].ToString().Trim()));
                        string custmermobile = ReplaceCode(Request.Form["custmermobile"].ToString().Trim());
                        string refrenceid = ReplaceCode(Request.Form["refrenceid"].ToString().Trim());
                        DataTable dtchk = cls.select_data_dt(@"select * from tbl_ezulix_ele where account_no='" + account + "' and sp_key='" + spkey + "' and trans_amt='" + amount + "' and statu in('SUCCESS','Pending')");
                        if (dtchk.Rows.Count > 0)
                        {
                            DateTime billpaydate = Convert.ToDateTime(dtchk.Rows[0]["paydate"].ToString());
                            string currentdate = DateTime.Now.ToString("yyyyMMdd");
                            string currenttime = DateTime.Now.ToString("HH:mm");
                            string paydate1 = billpaydate.ToString("yyyyMMdd");
                            string paytime = billpaydate.ToString("HH:mm");
                            if (paydate1 == currentdate)
                            {
                                string difference = DateTime.Parse(currenttime).Subtract(DateTime.Parse(paytime)).ToString("t");
                                DateTime dtkk = Convert.ToDateTime(difference);
                                int total = (dtkk.Hour * 60) + dtkk.Minute;
                                if (Convert.ToInt32(total) > 5)
                                {
                                    int chkbal = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString()));
                                    if (chkbal == 1)
                                    {
                                        int a = clsm.Wallet_MakeTransaction(dt.Rows[0]["MemberID"].ToString(), Convert.ToDecimal("-" + amount), "Dr", "PayEleBill:" + agentid + "");
                                        if (a == 1)
                                        {
                                            List<ParmList> _lstparm = new List<ParmList>();
                                            _lstparm.Add(new ParmList() { name = "@ipay_id", value = "" });
                                            _lstparm.Add(new ParmList() { name = "@agent_id", value = agentid });
                                            _lstparm.Add(new ParmList() { name = "@opr_id", value = "00" });
                                            _lstparm.Add(new ParmList() { name = "@account_no", value = account });
                                            _lstparm.Add(new ParmList() { name = "@sp_key", value = spkey });
                                            _lstparm.Add(new ParmList() { name = "@trans_amt", value = Convert.ToDecimal(0) });
                                            _lstparm.Add(new ParmList() { name = "@statu", value = "Pending" });
                                            _lstparm.Add(new ParmList() { name = "@res_code", value = "" });
                                            _lstparm.Add(new ParmList() { name = "@res_msg", value = "" });
                                            _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString()) });
                                            _lstparm.Add(new ParmList() { name = "@memberid", value = dt.Rows[0]["MemberID"].ToString() });
                                            cls.select_data_dtNew("Set_Ezulix_Ele", _lstparm);
                                            ReturnError("Transaction Success", OperationName);

                                        }
                                        else
                                        {
                                            ReturnError("Insufficient Wallet Balance!", OperationName);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ReturnError("Insufficient Wallet Balance!", OperationName);
                                        return;
                                    }
                                }
                                else
                                {
                                    ReturnError("same transaction can not be repated within 5 minutes", OperationName);
                                    return;
                                }
                            }
                            else
                            {
                                int chkbal = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString()));
                                if (chkbal == 1)
                                {
                                    int a = clsm.Wallet_MakeTransaction(dt.Rows[0]["MemberID"].ToString(), Convert.ToDecimal("-" + amount), "Dr", "PayEleBill:" + agentid + "");
                                    if (a == 1)
                                    {
                                        List<ParmList> _lstparm = new List<ParmList>();
                                        _lstparm.Add(new ParmList() { name = "@ipay_id", value = "" });
                                        _lstparm.Add(new ParmList() { name = "@agent_id", value = agentid });
                                        _lstparm.Add(new ParmList() { name = "@opr_id", value = "00" });
                                        _lstparm.Add(new ParmList() { name = "@account_no", value = account });
                                        _lstparm.Add(new ParmList() { name = "@sp_key", value = spkey });
                                        _lstparm.Add(new ParmList() { name = "@trans_amt", value = Convert.ToDecimal(0) });
                                        _lstparm.Add(new ParmList() { name = "@statu", value = "Pending" });
                                        _lstparm.Add(new ParmList() { name = "@res_code", value = "" });
                                        _lstparm.Add(new ParmList() { name = "@res_msg", value = "" });
                                        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString()) });
                                        _lstparm.Add(new ParmList() { name = "@memberid", value = dt.Rows[0]["MemberID"].ToString() });
                                        cls.select_data_dtNew("Set_Ezulix_Ele", _lstparm);
                                        ReturnError("Transaction Success", OperationName);
                                    }
                                    else
                                    {
                                        ReturnError("Insufficient Wallet Balance!", OperationName);
                                        return;
                                    }
                                }
                                else
                                {
                                    ReturnError("Insufficient Wallet Balance!", OperationName);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            int chkbal = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(amount), Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString()));
                            if (chkbal == 1)
                            {
                                int a = clsm.Wallet_MakeTransaction(dt.Rows[0]["MemberID"].ToString(), Convert.ToDecimal("-" + amount), "Dr", "PayEleBill:" + agentid + "");
                                if (a == 1)
                                {
                                    List<ParmList> _lstparm = new List<ParmList>();
                                    _lstparm.Add(new ParmList() { name = "@ipay_id", value = "" });
                                    _lstparm.Add(new ParmList() { name = "@agent_id", value = agentid });
                                    _lstparm.Add(new ParmList() { name = "@opr_id", value = "00" });
                                    _lstparm.Add(new ParmList() { name = "@account_no", value = account });
                                    _lstparm.Add(new ParmList() { name = "@sp_key", value = spkey });
                                    _lstparm.Add(new ParmList() { name = "@trans_amt", value = Convert.ToDecimal(0) });
                                    _lstparm.Add(new ParmList() { name = "@", value = "" });
                                    _lstparm.Add(new ParmList() { name = "@resstatu", value = "Pending" });
                                    _lstparm.Add(new ParmList() { name = "@res_code_msg", value = "" });
                                    _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString()) });
                                    _lstparm.Add(new ParmList() { name = "@memberid", value = dt.Rows[0]["MemberID"].ToString() });
                                    cls.select_data_dtNew("Set_Ezulix_Ele", _lstparm);
                                    ReturnError("Transaction Success", OperationName);
                                }
                                else
                                {
                                    ReturnError("Insufficient Wallet Balance!", OperationName);
                                    return;
                                }
                            }
                            else
                            {
                                ReturnError("Insufficient Wallet Balance!", OperationName);
                                return;
                            }
                        }
                    }
                    else
                    {
                        string json = ConvertDataTabletoJson(dt);
                        Response.Write("{ " + OperationName + ":" + json + "}");
                    }
                }
                else
                {
                    ReturnError("Invalid Request Format", OperationName);
                }
                #endregion
            }

            else
            {
                ReturnError("Invalid Request Format", "Unknown");
            }
        }
        else
        {
            ReturnError("Invalid Request Format", "Unknown");
        }
    }

    private string ReplaceCode(string str)
    {
        return str.Replace("'", "").Replace("-", "").Replace(";", "");
    }
    public string ConvertDataTabletoJson(DataTable dt)
    {
        return JsonConvert.SerializeObject(dt);
    }

    protected void ReturnError(string message, string operationName)
    {
        DataTable dt = cls.select_data_dt("Select 0 as ResponseCode,'" + message + "' as ResponseStatus");
        string output = ConvertDataTabletoJson(dt);
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

    protected string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
        try
        {
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.UTF8.GetBytes(Data);
            req.ContentLength = sentData.Length;
            using (System.IO.Stream sendStream = req.GetRequestStream())
            {
                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();
            }
            System.Net.WebResponse res = req.GetResponse();
            System.IO.Stream ReceiveStream = res.GetResponseStream();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
            {
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);

                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    Out += str;
                    count = sr.Read(read, 0, 256);
                }
            }
        }
        catch (ArgumentException ex)
        {
            Out = string.Format("HTTP_ERROR :: The second HttpWebRequest object has raised an Argument Exception as 'Connection' Property is set to 'Close' :: {0}", ex.Message);
        }
        catch (WebException ex)
        {
            Out = string.Format("HTTP_ERROR :: WebException raised! :: {0}", ex.Message);
        }
        catch (Exception ex)
        {
            Out = string.Format("HTTP_ERROR :: Exception raised! :: {0}", ex.Message);
        }
        string myresponse = Out.ToString();

        return myresponse;
    }
}