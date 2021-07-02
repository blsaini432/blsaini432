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
using System.IO;

public partial class Root_Admin_MM_Transactions : System.Web.UI.Page
{
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    string condition = " ewallettransactionid > 0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
    
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
    public static List<Customer> filldmrinreport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(0) });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("MM_transactionREport", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.CustMobile = dtrow["CustMobile"].ToString();
            cust.name = dtrow["name"].ToString();
            cust.BeneAC = dtrow["BeneAC"].ToString();
            cust.BankName = dtrow["BankName"].ToString();
            cust.RefNO = dtrow["RefNO"].ToString();
            cust.TxnID = dtrow["TxnID"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.SurchargeTaken = dtrow["SurchargeTaken"].ToString();
            cust.CommissionGiven = dtrow["CommissionGiven"].ToString();
            cust.AdminCost = dtrow["AdminCost"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.createdate = dtrow["createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> filldmrinreportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("MM_transactionREport", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.CustMobile = dtrow["CustMobile"].ToString();
            cust.name = dtrow["name"].ToString();
            cust.BeneAC = dtrow["BeneAC"].ToString();
            cust.BankName = dtrow["BankName"].ToString();
            cust.RefNO = dtrow["RefNO"].ToString();
            cust.TxnID = dtrow["TxnID"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.SurchargeTaken = dtrow["SurchargeTaken"].ToString();
            cust.CommissionGiven = dtrow["CommissionGiven"].ToString();
            cust.AdminCost = dtrow["AdminCost"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.createdate = dtrow["createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ExportDMR(string fromdate, string todate)
    {
        cls_connection cls = new cls_connection();
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (fromdate == "" || todate =="")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtEWalletTransaction = cls.select_data_dtNew("MM_transactionREport", _lstparm);
            Common.Export.ExportToExcel(dtEWalletTransaction, "DMRTransaction_Report");
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtEWalletTransaction = cls.select_data_dtNew("MM_transactionREport", _lstparm);

            string filename = "TesT";
            GridView GridView1 = new GridView();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.DataSource = dtEWalletTransaction;
            GridView1.AllowPaging = false;
            GridView1.DataBind();

            GridView1.CssClass = "ExportGridViewStyle";
            GridView1.HeaderStyle.CssClass = "ExportHeaderStyle";

            GridView1.RowStyle.CssClass = "ExportRowStyle";
            GridView1.AlternatingRowStyle.CssClass = "ExRoottRowStyle";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];
                row.Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
            //Common.Export.ExportToExcel(dtEWalletTransaction, "DMRTransaction_Report");
            actions = "";
        }
        return actions;
    }

    #endregion

    #region class
    public class Customer
    {
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string CustMobile { get; set; }
        public string name { get; set; }
        public string BeneAC { get; set; }
        public string BankName { get; set; }
        public string RefNO { get; set; }
        public string TxnID { get; set; }
        public string Amount { get; set; }
        public string SurchargeTaken { get; set; }
        public string CommissionGiven { get; set; }
        public string AdminCost { get; set; }
        public string Status { get; set; }
        public string createdate { get; set; }

    }
    #endregion

    protected void btnexport_Click(object sender, EventArgs e)
    {
        if(txttodate.Text == "" || txt_fromdate.Text== "" )
        {
            DataTable dtEWalletTransaction = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(0) });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
            dtEWalletTransaction = cls.select_data_dtNew("MM_transactionREport", _lstparm);
            if (dtEWalletTransaction.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtEWalletTransaction, "DMRTransaction_Report");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('No Data Avaible to Export');", true);
            }
        }
        else
        {
            DataTable dtEWalletTransaction = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(0) });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(txt_fromdate.Text) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(txttodate.Text) });
            dtEWalletTransaction = cls.select_data_dtNew("MM_transactionREport", _lstparm);
            if (dtEWalletTransaction.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtEWalletTransaction, "DMRTransaction_Report");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('No Data Avaible to Export');", true);
            }
        }
       

    }
}