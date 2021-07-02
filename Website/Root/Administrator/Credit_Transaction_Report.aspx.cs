using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Root_Credit_Transaction_Report : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    DataTable dtExport = new DataTable();
    cls_connection cls = new cls_connection();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }


    }

    #endregion

    #region [Function]
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillcredittransaction()
    {
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@fromdate", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        _lstparm.Add(new ParmList() { name = "@todate", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        dtEWalletTransaction = cls.select_data_dtNew("Recharge_CreditTransactions_List", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Debit = dtrow["Debit"].ToString();
            cust.Credit = dtrow["Credit"].ToString();
            cust.Balance = dtrow["Balance"].ToString();
            cust.narration = dtrow["Narration"].ToString();
            cust.date = dtrow["createdate"].ToString();
            cust.Factor = dtrow["Factor"].ToString();
            cust.PayStatus = dtrow["PayStatus"].ToString();
            cust.ewallettransactionid = dtrow["ewallettransactionid"].ToString();
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
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@fromdate", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@todate", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Recharge_CreditTransactions_List", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "FundRequest_Report");
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
    public static List<Customer> fillcredittransactionbydate(string fromdate, string todate)
    {
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@fromdate", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@todate", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Recharge_CreditTransactions_List", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Debit = dtrow["Debit"].ToString();
            cust.Credit = dtrow["Credit"].ToString();
            cust.Balance = dtrow["Balance"].ToString();
            cust.narration = dtrow["Narration"].ToString();
            cust.date = dtrow["createdate"].ToString();
            cust.Factor = dtrow["Factor"].ToString();
            cust.PayStatus = dtrow["PayStatus"].ToString();
            cust.ewallettransactionid = dtrow["ewallettransactionid"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string MarkStatus(string EwalletTransactionID)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@EwalletTransId", value = Convert.ToInt32(EwalletTransactionID) });
        cls.select_data_dtNew("Recharge_CreditTransactions_Pay ", _lstparm);
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
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string Balance { get; set; }
        public string narration { get; set; }
        public string date { get; set; }
        public string Factor { get; set; }
        public string PayStatus { get; set; }
        public string ewallettransactionid { get; set; }
    }

    #endregion



    //protected void gvEWalletTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "pay")
    //    {
    //        cls.select_data_dt("Exec dbo.Recharge_CreditTransactions_Pay '" + e.CommandArgument.ToString() + "'");
    //        fillEWalletTransaction();
    //    }
    //}
    //protected void gvEWalletTransaction_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        e.Row.TableSection = TableRowSection.TableHeader;
    //    }
    //}
}