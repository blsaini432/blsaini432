using System;
using System.Data;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

public partial class API_Aeps : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Headers["Memberid"] != null && Request.Headers["commission"] != null)
            {
                string memberid = Request.Headers["Memberid"].ToString();
                string admincomm = Request.Headers["commission"].ToString();
                cls_connection Cls = new cls_connection();
                Cls.select_data_dt("INSERT INTO TestAeps VALUES ('" + memberid + "')");
                DataTable dt = Cls.select_data_dt(@"SELECT * FROM tblmlm_membermaster WHERE MemberID='" + memberid + "'");
                if (dt.Rows.Count > 0)
                {
                    string input = string.Empty;
                    using (var reader = new StreamReader(Request.InputStream))
                    {
                       
                        input = reader.ReadToEnd();
                        Cls.select_data_dt("INSERT INTO TestAeps VALUES ('" + input + "')");

                    }
                    if (input != string.Empty)
                    {
                        DataSet ds = Deserialize(input);
                        string dg = ds.Tables["HEADER"].Rows[0]["OP"].ToString();
                        if (dg == "MICROATM")
                        {
                            ViewState["Txn"] = null;
                            ViewState["Txn"] = ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString();
                            Cls.select_data_dt(@"EXEC set_ezulix_aeps_wd @op='" + ds.Tables["HEADER"].Rows[0]["OP"].ToString() + "',@st='" + ds.Tables["HEADER"].Rows[0]["ST"].ToString() + "',@txn_amount='" + Convert.ToDecimal(ds.Tables["HEADER"].Rows[0]["TXN_AMOUNT"].ToString()) + "',@aid='" + ds.Tables["HEADER"].Rows[0]["AID"].ToString() + "',@cn='" + ds.Tables["DATA"].Rows[0]["CN"].ToString() + "',@order_id='" + ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString() + "',@order_id_txn='" + ds.Tables["TXNDETAILS"].Rows[0]["ORDER_ID"].ToString() + "',@txn_amount_tra='" + Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["TXN_AMOUNT"].ToString()) + "',@atm_charge='" + Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["APM_CHARGE"].ToString()) + "',@agent_charge='" + Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["AGENT_CHARGE"].ToString()) + "',@memberid='" + dt.Rows[0]["MemberID"].ToString() + "',@msrno='" + dt.Rows[0]["MsrNo"].ToString() + "',@admcom='" + Convert.ToDecimal(admincomm) + "',@txnstatus='" + ds.Tables["TXNDETAILS"].Rows[0]["txnStatus"].ToString() + "',@rrn='" + ds.Tables["TXNDETAILS"].Rows[0]["RRN"].ToString() + "',@ResCode='" + ds.Tables["TXNDETAILS"].Rows[0]["RES_CODE"].ToString() + "',@AadharNumber='" + ds.Tables["TXNDETAILS"].Rows[0]["CARD_NUMBER"].ToString() + "',@BANK_NAME='" + ds.Tables["TXNDETAILS"].Rows[0]["BANK_NAME"].ToString() + "',@RESP_MSG='" + ds.Tables["TXNDETAILS"].Rows[0]["RESP_MSG"].ToString() + "'");
                            cls_myMember Clsm = new cls_myMember();
                            if (ds.Tables["TXNDETAILS"].Rows[0]["txnStatus"].ToString() == "SUCCESS")
                            {
                                Clsm.AEPSWallet_MakeTransaction_Ezulix(dt.Rows[0]["MemberID"].ToString(), Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["TXN_AMOUNT"].ToString()), "Cr", "MicroATM Txn: " + ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString() + "");
                                Cls.select_data_dt(@"EXEC SET_AEPS_Commission @memberid='" + dt.Rows[0]["MemberID"].ToString() + "',@txnamount= '" + Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["TXN_AMOUNT"].ToString()) + "',@txnid='" + ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString() + "'");
                            }
                            Response.Clear();
                            Response.ClearHeaders();
                            Response.AddHeader("content-type", "application/json");
                            Response.Write("{\"CODE\": \"Success\",\"Txn\":\"" + ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString() + "\"}");
                        }
                        else
                        {
                            ViewState["Txn"] = null;
                            ViewState["Txn"] = ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString();
                            Cls.select_data_dt(@"EXEC set_ezulix_aeps_wd @op='" + ds.Tables["HEADER"].Rows[0]["OP"].ToString() + "',@st='" + ds.Tables["HEADER"].Rows[0]["ST"].ToString() + "',@txn_amount='" + Convert.ToDecimal(ds.Tables["HEADER"].Rows[0]["TXN_AMOUNT"].ToString()) + "',@aid='" + ds.Tables["HEADER"].Rows[0]["AID"].ToString() + "',@cn='" + ds.Tables["DATA"].Rows[0]["CN"].ToString() + "',@order_id='" + ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString() + "',@order_id_txn='" + ds.Tables["TXNDETAILS"].Rows[0]["ORDER_ID"].ToString() + "',@txn_amount_tra='" + Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["TXN_AMOUNT"].ToString()) + "',@atm_charge='" + Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["APM_CHARGE"].ToString()) + "',@agent_charge='" + Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["AGENT_CHARGE"].ToString()) + "',@memberid='" + dt.Rows[0]["MemberID"].ToString() + "',@msrno='" + dt.Rows[0]["MsrNo"].ToString() + "',@admcom='" + Convert.ToDecimal(admincomm) + "',@txnstatus='" + ds.Tables["TXNDETAILS"].Rows[0]["txnStatus"].ToString() + "',@rrn='" + ds.Tables["TXNDETAILS"].Rows[0]["RRN"].ToString() + "',@ResCode='" + ds.Tables["TXNDETAILS"].Rows[0]["RES_CODE"].ToString() + "',@AadharNumber='" + ds.Tables["TXNDETAILS"].Rows[0]["AadharNumber"].ToString() + "',@BANK_NAME='" + ds.Tables["TXNDETAILS"].Rows[0]["BANK_NAME"].ToString() + "',@RESP_MSG='" + ds.Tables["TXNDETAILS"].Rows[0]["RESP_MSG"].ToString() + "'");
                            cls_myMember Clsm = new cls_myMember();
                            if (ds.Tables["TXNDETAILS"].Rows[0]["txnStatus"].ToString() == "SUCCESS")
                            {
                                Clsm.AEPSWallet_MakeTransaction_Ezulix(dt.Rows[0]["MemberID"].ToString(), Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["TXN_AMOUNT"].ToString()), "Cr", "AEPS Topup Txn: " + ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString() + "");
                                Cls.select_data_dt(@"EXEC SET_AEPS_Commission @memberid='" + dt.Rows[0]["MemberID"].ToString() + "',@txnamount= '" + Convert.ToDecimal(ds.Tables["TXNDETAILS"].Rows[0]["TXN_AMOUNT"].ToString()) + "',@txnid='" + ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString() + "'");
                            }
                            Response.Clear();
                            Response.ClearHeaders();
                            Response.AddHeader("content-type", "application/json");
                            Response.Write("{\"CODE\": \"Success\",\"Txn\":\"" + ds.Tables["DATA"].Rows[0]["ORDER_ID"].ToString() + "\"}");
                        }
                                
                      
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("content-type", "application/json");
            Response.Write("{\"CODE\": \"Fail\",\"Txn\":\"" + ViewState["Txn"].ToString() + "\"}");
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
}