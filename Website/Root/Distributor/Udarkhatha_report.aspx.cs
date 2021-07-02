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
public partial class Root_Distributor_Udarkhatha_report : System.Web.UI.Page
{

    #region Properties
    cls_connection Cls = new cls_connection();
    DataTable dtEWalletTransaction = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                cls_connection cls = new cls_connection();
                List<ParmList> _lstparm = new List<ParmList>();
              DataTable dt = (DataTable)Session["dtDistributor"];
                int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
                _lstparm.Add(new ParmList() { name = "@Action", value = "A" });
                _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
                dtEWalletTransaction = cls.select_data_dtNew("Proc_khatabook", _lstparm);
                foreach (DataRow dtrow in dtEWalletTransaction.Rows)
                {
                    Customer cust = new Customer();
                    cust.Amount = dtrow["amount"].ToString();
                    Session["Totalamount"] = cust.Amount;
                    lblamount.Text = Session["Totalamount"].ToString();
                }
                DataTable dtmembermaster = new DataTable();
                dtmembermaster = (DataTable)Session["dtDistributor"];
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
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
    public static List<Customer> fillreport()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        dtEWalletTransaction = cls.select_data_dtNew("sp_khatabook_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {

            Customer cust = new Customer();
            cust.Mobile = dtrow["mobile"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.address = dtrow["address"].ToString();
            cust.Amount = dtrow["totalamount"].ToString();
            cust.credit = dtrow["credit"].ToString();
            cust.debit = dtrow["debit"].ToString();
            cust.TxnID = dtrow["txn"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> fillreportbydate(string fromdate, string todate)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
      
        dtEWalletTransaction = cls.select_data_dtNew("sp_khatabook_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.Mobile = dtrow["mobile"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.address = dtrow["address"].ToString();
            cust.Amount = dtrow["totalamount"].ToString();
            cust.TxnID = dtrow["txn"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.credit = dtrow["credit"].ToString();
            cust.debit = dtrow["debit"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Customer> loaddmrreceipt(string txnid)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        string txn = txnid;
        HttpContext.Current.Session["txn"] = txn;
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "u" });
        _lstparm.Add(new ParmList() { name = "@txn", value = txnid });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_khatabook", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.Mobile = dtrow["mobile"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.address = dtrow["address"].ToString();
            cust.Amount = dtrow["totalamount"].ToString();
            cust.TxnID = dtrow["txn"].ToString();
            cust.credit = dtrow["credit"].ToString();
            cust.debit = dtrow["debit"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        
        return custList;
    }


    protected void btn_export_Click(object sender, EventArgs e)
    {
            DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
            DataTable dtEWalletTransaction = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            dtExport = cls.select_data_dtNew("sp_khatabook_report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "Udarkhata_Report");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);
            }
       
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Customer> deletedata(string txnid)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> success = new List<Customer>();
        cls_connection cls = new cls_connection();
        string txn = txnid;
        if (txn != "")
        {

            DataTable dts = new DataTable();
            dt = cls.select_data_dt("select * from tbl_khatabooks where txn='" + txnid + "'");
            String Result = string.Empty;
            if (dt.Rows.Count > 0)
            {
                cls_myMember clsm = new cls_myMember();
                cls.select_data_dt(@"delete from tbl_khatabooks  Where  txn='" + txnid + "'");
                return success;

            }
            else
            {

            }
        }
        else
        {

        }
        return success;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        cls_connection cls = new cls_connection();
        string  credit = txt_credit.Text;
        string  debit = txt_debit.Text;
        string txnid = HttpContext.Current.Session["txn"].ToString();
        if (txnid != "" && credit !="" && debit =="" )
        {
          
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from tbl_khatabooks where txn='" + txnid + "'");
            String Result = string.Empty;
            if (dt.Rows.Count > 0)
            {
                int newamount = Convert.ToInt32(credit);
                int amount = Convert.ToInt32(dt.Rows[0]["credit"]);
                int totalamount = amount + newamount;
                cls_myMember clsm = new cls_myMember();
                cls.select_data_dt(@"Update tbl_khatabooks set credit='" + totalamount + "' Where  txn='" + txnid + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Detail Update  Successfull!');location.replace('Udarkhatha_report.aspx');", true);

            }
            else
            {
               
            }
        }
        else if (txnid != "" && credit == "" && debit != "")
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from tbl_khatabooks where txn='" + txnid + "'");
            String Result = string.Empty;
            if (dt.Rows.Count > 0)
            {
                int newamount = Convert.ToInt32(debit);
                int amount = Convert.ToInt32(dt.Rows[0]["debit"]);
                int totalamount = amount + newamount;
                cls_myMember clsm = new cls_myMember();
                cls.select_data_dt(@"Update tbl_khatabooks set debit='" + totalamount + "' Where  txn='" + txnid + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Detail Update  Successfull!');location.replace('Udarkhatha_report.aspx');", true);

            }
            else
            {

            }

        }
        else if(txnid != "" && credit != "" && debit != "")
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from tbl_khatabooks where txn='" + txnid + "'");
            String Result = string.Empty;
            if (dt.Rows.Count > 0)
            {
                int newamount = Convert.ToInt32(debit);
                int amount = Convert.ToInt32(dt.Rows[0]["debit"]);
                int totalamount = amount + newamount;
                int crtedamount = Convert.ToInt32(credit);
                int crerdtamount = Convert.ToInt32(dt.Rows[0]["credit"]);
                int totalcreditamount = amount + newamount;
                cls_myMember clsm = new cls_myMember();
                cls.select_data_dt(@"Update tbl_khatabooks set debit='" + totalamount + "' credit='"+ totalcreditamount + "' Where  txn='" + txnid + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Detail Update  Successfull!');location.replace('Udarkhatha_report.aspx');", true);

            }
            else
            {

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error found !');location.replace('Udarkhatha_report.aspx');", true);
        }

    }
    #endregion

    #region class
    public class Customer
    {
       
        public string address { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
      public string debit { get; set; }
        public string credit { get; set; }
        public string TxnID { get; set; }
       
        public string Amount { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
    }

    #endregion

}