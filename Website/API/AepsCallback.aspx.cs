using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

public partial class API_AepsCallback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string memberid = Request.Headers["Memberid"].ToString();
            string admincomm = Request.Headers["commission"].ToString();
            cls_connection Cls = new cls_connection();
            DataTable dt = Cls.select_data_dt(@"SELECT * FROM tblmlm_membermaster WHERE MemberID='" + memberid + "'");
            if (dt.Rows.Count > 0)
            {
                string input = string.Empty;
                using (var reader = new StreamReader(Request.InputStream))
                {
                    input = reader.ReadToEnd();
                }
                if (input != string.Empty)
                {
                    DataSet ds = Deserialize(input);
                    string json = JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
                    //Cls.select_data_dt("insert into sptest values('" + json + "')");
                    ViewState["Txn"] = null;
                    ViewState["Txn"] = ds.Tables[0].Rows[0]["ORDER_ID"].ToString();
                    DataTable dtckhtra = Cls.select_data_dt(@"SELECT * FROM tbl_ezulix_aeps_wd WHERE order_id LIKE '%" + ds.Tables[0].Rows[0]["ORDER_ID"].ToString() + "%'");
                    if (dtckhtra.Rows.Count > 0)
                    {
                        Cls.select_data_dt(@"SELECT * FROM tbl_ezulix_aeps_wd WHERE order_id LIKE '%" + ds.Tables[0].Rows[0]["ORDER_ID"].ToString() + "%'");
                    }
                    else
                    {
                        Cls.select_data_dt(@"EXEC set_ezulix_aeps_wd @op='" + ds.Tables[0].Rows[0]["op"].ToString() + "',@st='" + ds.Tables[0].Rows[0]["st"].ToString() + "',@txn_amount='" + Convert.ToDecimal(ds.Tables[0].Rows[0]["txn_amount"].ToString()) + "',@aid='" + ds.Tables[0].Rows[0]["aid"].ToString() + "',@cn='" + ds.Tables[0].Rows[0]["cn"].ToString() + "',@txnstatus='" + ds.Tables[0].Rows[0]["txnstatus"].ToString() + "',@rrn='" + ds.Tables[0].Rows[0]["rrn"].ToString() + "',@ResCode='" + ds.Tables[0].Rows[0]["ResCode"].ToString() + "',@AadharNumber='" + ds.Tables[0].Rows[0]["AadharNumber"].ToString() + "',@BANK_NAME='" + ds.Tables[0].Rows[0]["BANK_NAME"].ToString() + "',@RESP_MSG='" + ds.Tables[0].Rows[0]["RESP_MSG"].ToString() + "',@order_id='" + ds.Tables[0].Rows[0]["order_id"].ToString() + "',@order_id_txn='" + ds.Tables[0].Rows[0]["order_id_txn"].ToString() + "',@txn_amount_tra='" + Convert.ToDecimal(ds.Tables[0].Rows[0]["TXN_AMOUNT"].ToString()) + "',@atm_charge='" + Convert.ToDecimal(ds.Tables[0].Rows[0]["atm_charge"].ToString()) + "',@agent_charge='" + Convert.ToDecimal(ds.Tables[0].Rows[0]["agent_charge"].ToString()) + "',@custmer_chagrge='" + Convert.ToDecimal(ds.Tables[0].Rows[0]["custmer_chagrge"].ToString()) + "',@memberid='" + dt.Rows[0]["MemberID"].ToString() + "',@msrno='" + dt.Rows[0]["MsrNo"].ToString() + "',@admcom='" + Convert.ToDecimal(admincomm) + "'");
                    }
                    if (Convert.ToString(ds.Tables[0].Rows[0]["txnstatus"]) == "SUCCESS")
                    {
                        cls_myMember Clsm = new cls_myMember();
                        DataTable dtwallet = Cls.select_data_dt(@"SELECT * FROM tblMLM_RWalletTransaction WHERE Narration LIKE '%" + ds.Tables[0].Rows[0]["ORDER_ID"].ToString() + "%'");
                        if (dtwallet.Rows.Count > 0)
                            Cls.select_data_dt(@"SELECT * FROM tblMLM_RWalletTransaction WHERE Narration LIKE '%" + ds.Tables[0].Rows[0]["ORDER_ID"].ToString() + "%'");
                        else
                            Clsm.AEPSWallet_MakeTransaction_Ezulix(dt.Rows[0]["MemberID"].ToString(), Convert.ToDecimal(ds.Tables[0].Rows[0]["TXN_AMOUNT"].ToString()), "Cr", "AEPS Topup Txn: " + ds.Tables[0].Rows[0]["ORDER_ID"].ToString() + "");
                            Cls.select_data_dt(@"EXEC SET_AEPS_Commission @memberid='" + dt.Rows[0]["MemberID"].ToString() + "',@txnamount= '" + Convert.ToDecimal(ds.Tables[0].Rows[0]["TXN_AMOUNT"].ToString()) + "',@txnid='" + ds.Tables[0].Rows[0]["ORDER_ID"].ToString() + "'");
                    }
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.AddHeader("content-type", "application/json");
                    Response.Write("{\"CODE\": \"Success\",\"Txn\":\"" + ds.Tables[0].Rows[0]["ORDER_ID"].ToString() + "\"}");
                }
            }
        }
        catch (Exception ex)
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