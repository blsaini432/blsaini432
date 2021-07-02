using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Root_Admin_Aepsbank : System.Web.UI.Page
{
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public DataTable dtMemberMaster = new DataTable();
    cls_myMember Clsm = new cls_myMember();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
            {
                txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            }

        }
    }

    #region [Function]
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillaepsbankreport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(0) });
        _lstparm.Add(new ParmList() { name = "@action", value = "s" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("SetMLM_RwalletWithdraw_Request", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.bank = dtrow["bank"].ToString();
            cust.ac = dtrow["ac"].ToString();
            cust.ifsc = dtrow["ifsc"].ToString();
            cust.amount = dtrow["amount"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.wtxnid = dtrow["wtxnid"].ToString();
            cust.status = dtrow["status"].ToString();
            cust.atxnid = dtrow["atxnid"].ToString();
            cust.reqdate = dtrow["reqdate"].ToString();
            cust.adate = dtrow["adate"].ToString();
            cust.id = dtrow["id"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(0);
            DataTable dtExport = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@action", value = "s" });
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("SetMLM_RwalletWithdraw_Request", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "AepsBankRequest_Report");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select date range to genrate excel');", true);
        }
    }


    [WebMethod]
    public static List<Customer> fillaepsbankreportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@action", value = "s" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("SetMLM_RwalletWithdraw_Request", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.bank = dtrow["bank"].ToString();
            cust.ac = dtrow["ac"].ToString();
            cust.ifsc = dtrow["ifsc"].ToString();
            cust.amount = dtrow["amount"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.wtxnid = dtrow["wtxnid"].ToString();
            cust.status = dtrow["status"].ToString();
            cust.atxnid = dtrow["atxnid"].ToString();
            cust.reqdate = dtrow["reqdate"].ToString();
            cust.adate = dtrow["adate"].ToString();
            cust.id = dtrow["id"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveBankRequest(string fundid, string bankid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        if (bankid != "")
        {
            int id = Convert.ToInt32(fundid);
            cls_myMember Clsm = new cls_myMember();
            string Txn = Clsm.Cyrus_GetTransactionID_New();
            DataTable dt = cls.select_data_dt(@"EXEC SetMLM_RwalletWithdraw_Request @action='get',@id=" + id + "");
            if (dt.Rows.Count > 0)
            {
                int resaeps = Clsm.AEPSWallet_MakeTransaction_Ezulix(dt.Rows[0]["memberid"].ToString(), Convert.ToDecimal("-" + dt.Rows[0]["amount"].ToString()), "Dr", "Bank Topup TXN:" + Txn + "");
                cls.select_data_dt(@"EXEC SetMLM_RwalletWithdraw_Request @action='ap',@wtxnid='" + Txn + "',@id=" + id + ",@atxnid='" + bankid + "'");
                actions = "success";
                return actions;
            }
            else
            {
                return actions;
            }
        }
        else
        {
            return actions;
        }
       
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string RejectBankRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        int id = Convert.ToInt32(fundid);
        cls.select_data_dt(@"EXEC SetMLM_RwalletWithdraw_Request @action='rej',@id=" + id + "");
        actions = "success";
        return actions;
    }

    #endregion




    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string bank { get; set; }
        public string ac { get; set; }
        public string ifsc { get; set; }
        public string amount { get; set; }
        public string txnid { get; set; }
        public string wtxnid { get; set; }
        public string status { get; set; }
        public string atxnid { get; set; }
        public string reqdate { get; set; }
        public string adate { get; set; }
        public string id { get; set; }
    }
    #endregion


    //protected void gvEWalletTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "ae")
    //    {
    //        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
    //        TextBox txt = (TextBox)row.FindControl("txt_banktxn");
    //        if (txt.Text != string.Empty)
    //        {
    //            int id = Convert.ToInt32(e.CommandArgument.ToString());
    //            string Txn = Clsm.Cyrus_GetTransactionID_New();
    //            DataTable dt = cls.select_data_dt(@"EXEC SetMLM_RwalletWithdraw_Request @action='get',@id=" + id + "");
    //            int resaeps = Clsm.AEPSWallet_MakeTransaction_Ezulix(dt.Rows[0]["memberid"].ToString(), Convert.ToDecimal("-" + dt.Rows[0]["amount"].ToString()), "Dr", "Bank Topup TXN:" + Txn + "");
    //            cls.select_data_dt(@"EXEC SetMLM_RwalletWithdraw_Request @action='ap',@wtxnid='" + Txn + "',@id=" + id + ",@atxnid='" + txt.Text.Trim() + "'");
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Bank request approved successfully !!');", true);

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please input Bank Txn Id !!');", true);
    //        }
    //    }
    //    else if (e.CommandName == "re")
    //    {
    //        int id = Convert.ToInt32(e.CommandArgument.ToString());
    //        cls.select_data_dt(@"EXEC SetMLM_RwalletWithdraw_Request @action='rej',@id=" + id + "");
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Bank request reject successfully !!');", true);

    //    }
    //}

}