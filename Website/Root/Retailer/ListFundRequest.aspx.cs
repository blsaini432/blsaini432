using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Root_Retailer_ListFundRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {

                if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
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
    public static List<Customer> fillfundrequest()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetByMsrNo" });
        _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        dtEWalletTransaction = cls.select_data_dtNew("ProcMLM_ManageFundRequest", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.FundRequestID = dtrow["FundRequestID"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Membername = dtrow["Membername"].ToString();
            cust.FromBank = dtrow["FromBank"].ToString();
            cust.ToBank = dtrow["ToBank"].ToString();
            cust.PaymentMode = dtrow["PaymentMode"].ToString();
            cust.ChequeOrDDNumber = dtrow["ChequeOrDDNumber"].ToString();
            cust.PaymentProof = dtrow["PaymentProof"].ToString();
            cust.RequestStatus = dtrow["RequestStatus"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.adminremark = dtrow["RCode"].ToString();
            cust.Remark = dtrow["Remark"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    [WebMethod]
    public static List<Customer> fillfundrequestbydate(string fromdate, string todate)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetByMsrNo" });
        _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("ProcMLM_ManageFundRequest", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.FundRequestID = dtrow["FundRequestID"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Membername = dtrow["Membername"].ToString();
            cust.FromBank = dtrow["FromBank"].ToString();
            cust.ToBank = dtrow["ToBank"].ToString();
             cust.PaymentMode = dtrow["PaymentMode"].ToString();
            cust.ChequeOrDDNumber = dtrow["ChequeOrDDNumber"].ToString();
            cust.PaymentProof = dtrow["PaymentProof"].ToString();
            cust.RequestStatus = dtrow["RequestStatus"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.adminremark = dtrow["RCode"].ToString();
            cust.Remark = dtrow["Remark"].ToString();
            
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> ShowFundImage(string fundid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetImage" });
        _lstparm.Add(new ParmList() { name = "@ID", value =  Convert.ToInt32(fundid) });
        dtEWalletTransaction = cls.select_data_dtNew("ProcMLM_ManageFundRequest", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.PaymentProof = dtrow["PaymentProof"].ToString();
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
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "GetByMsrNo" });
            _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("ProcMLM_ManageFundRequest", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "FundRequestds_Report");
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
        public string FundRequestID { get; set; }
        public string MemberID { get; set; }
        public string Membername { get; set; }
        public string FromBank { get; set; }
        public string ToBank { get; set; }
        public string ToMember { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeOrDDNumber { get; set; }
        public string PaymentProof { get; set; }
        public string RequestStatus { get; set; }
        public string AddDate { get; set; }
        public string Remark { get; set; }
        public string adminremark { get; set; }
    }

    #endregion


}
