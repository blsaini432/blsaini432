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
public partial class Root_Distributor_CasteCertificate_Report : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    DataTable dtEWalletTransaction = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    public static string mssrno { get; set; }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                }
                DataTable dtmembermaster = new DataTable();
                dtmembermaster = (DataTable)Session["dtDistributor"];
                mssrno = dtmembermaster.Rows[0]["MsrNo"].ToString();
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
    public static List<Customer> panreport()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "rtdt" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("Set_CasteCertificate_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["memberid"].ToString();
            cust.customername = dtrow["customername"].ToString();
            cust.customermobile = dtrow["customermobile"].ToString();
            cust.fathername = dtrow["fathername"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.Status = dtrow["statu"].ToString();
            cust.paydate = dtrow["paydate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> panreportbydate(string fromdate, string todate)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "rtdt" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Set_CasteCertificate_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["memberid"].ToString();
            cust.customername = dtrow["customername"].ToString();
            cust.customermobile = dtrow["customermobile"].ToString();
            cust.fathername = dtrow["fathername"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.Status = dtrow["statu"].ToString();
            cust.paydate = dtrow["paydate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> loadreceipt(string txnid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "loadreceipt" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
        dtEWalletTransaction = cls.select_data_dtNew("Set_CasteCertificate_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["memberid"].ToString();
            cust.customername = dtrow["customername"].ToString();
            cust.customermobile = dtrow["customermobile"].ToString();
            cust.fathername = dtrow["fathername"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.Status = dtrow["statu"].ToString();
            cust.paydate = dtrow["paydate"].ToString();
            custList.Add(cust);
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
            DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
            DataTable dtExport = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "rtdt" });
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Set_CasteCertificate_report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "income_Report");
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
        public string customername { get; set; }
        public string customermobile { get; set; }
        public string fathername { get; set; }
        public string email { get; set; }
        public string txnid { get; set; }
        public string commission { get; set; }
        public string Status { get; set; }
        public string paydate { get; set; }
        public string logo { get; set; }
        
    }
    #endregion


    


}