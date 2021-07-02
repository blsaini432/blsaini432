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


public partial class Root_Retailer_PaymentGateway_report : System.Web.UI.Page
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
            if (Session["dtRetailer"] != null)
            {
                if (Txt_FromDate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    Txt_FromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                }
                DataTable dtmembermaster = new DataTable();
                dtmembermaster = (DataTable)Session["dtRetailer"];            
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
    public static List<Customer> filldmrreport()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("paymentgateway_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {

            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["contact"].ToString();
            cust.vpa = dtrow["vpa"].ToString();
            cust.orderid = dtrow["POrderid"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.payid = dtrow["payid"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.method = dtrow["Method"].ToString();
            cust.cardid = dtrow["cardid"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.totalamount = dtrow["totalamount"].ToString();
            cust.feerate = dtrow["feerate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> filldmrreportbydate(string fromdate, string todate)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("paymentgateway_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["contact"].ToString();
            cust.vpa = dtrow["vpa"].ToString();
            cust.orderid = dtrow["POrderid"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.payid = dtrow["payid"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.method = dtrow["method"].ToString();
            cust.cardid = dtrow["cardid"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.totalamount = dtrow["totalamount"].ToString();
            cust.feerate = dtrow["feerate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> loaddmrreceipt(string txnid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "paymentgateway" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["contact"].ToString();
            cust.vpa = dtrow["vpa"].ToString();
            cust.orderid = dtrow["POrderid"].ToString();
            cust.Amount = dtrow["amount"].ToString();
            cust.payid = dtrow["payid"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.Status = dtrow["Statuss"].ToString();
            cust.method = dtrow["Method"].ToString();
            cust.cardid = dtrow["cardid"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.totalamount = dtrow["totalamount"].ToString();
            cust.feerate = dtrow["feerate"].ToString();
            cust.logo = string.IsNullOrEmpty(Convert.ToString(dtrow["Companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["Companylogo"]);
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
            DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
            int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
            DataTable dtEWalletTransaction = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("paymentgateway_report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "paymentgateway_report");
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
    #endregion

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string Bank { get; set; }
        public string SenderMobile { get; set; }
        public string Name { get; set; }
        public string RefNo { get; set; }
        public string TxnID { get; set; }
        public string cardid { get; set; }
        public string method { get; set; }
        public string Amount { get; set; }
        public string feerate { get; set; }
        public string payid { get; set; }
        public string vpa { get; set; }
        public string orderid { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
        public string TxnDate { get; set; }
        public string logo { get; set; }
        public string mode { get; set; }
        public string amount { get; set; }
        public string totalamount { get; set; }
        public string createdate { get; set; }
        public string email { get; set; }
    }

    #endregion

}