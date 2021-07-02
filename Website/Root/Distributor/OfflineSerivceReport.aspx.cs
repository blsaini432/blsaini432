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

public partial class Root_Distributor_OfflineSerivceReport : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    DataTable dtExport = new DataTable();
    public static string mssrno { get; set; }
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();
    clsRecharge_Dispute objDispute = new clsRecharge_Dispute();
    DataTable dtDispute = new DataTable();
    cls_connection cls = new cls_connection();
    string condition = " MsrNo > 0";
    #endregion

    #region [PageLoad]
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

    #endregion

    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillofflinereport()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        string memberid = dt.Rows[0]["MemberId"].ToString();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "L" });
        _lstparm.Add(new ParmList() { name = "@ID", value = memberid });
        _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberId = dtrow["MemberId"].ToString();
            cust.ServiceName = dtrow["ServiceName"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.Phone = dtrow["Phone"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.Address = dtrow["Address"].ToString();
            cust.CompanyName = dtrow["CompanyName"].ToString();
            cust.CompanyAddress = dtrow["CompanyAddress"].ToString();
            cust.Status = "Success";
            cust.TransID = dtrow["Ezulixtranid"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.RefrenceNumber = dtrow["RefrenceNumber"].ToString();
         //   cust.MsrNo = HttpContext.Current.Session["resellerrt"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    [WebMethod]
    public static List<Customer> fillofflinereportbydate(string fromdate, string todate)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        string memberid = dt.Rows[0]["MemberId"].ToString();
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "L" });
        _lstparm.Add(new ParmList() { name = "@ID", value = memberid });
        _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberId = dtrow["MemberId"].ToString();
            cust.ServiceName = dtrow["ServiceName"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.Phone = dtrow["Phone"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.Address = dtrow["Address"].ToString();
            cust.CompanyName = dtrow["CompanyName"].ToString();
            cust.CompanyAddress = dtrow["CompanyAddress"].ToString();
            cust.RefrenceNumber = dtrow["RefrenceNumber"].ToString();
            cust.Status = "Success";
            cust.TransID = dtrow["Ezulixtranid"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
           // cust.MsrNo = HttpContext.Current.Session["resellerrt"].ToString();
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
            string memberid = dt.Rows[0]["MemberId"].ToString();
            DataTable dtExport = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "L" });
            _lstparm.Add(new ParmList() { name = "@ID", value = memberid });
            _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "OfflineService_Report");
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
    public static List<Customer> loadofflinereceipt(string txnid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
    //    int resellerid = Convert.ToInt32(HttpContext.Current.Session["resellerrt"]);
        _lstparm.Add(new ParmList() { name = "@Action", value = "loadofflinereceipt" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
       // _lstparm.Add(new ParmList() { name = "@Id", value = resellerid });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.Id = dtrow["Id"].ToString();
            cust.ServiceName = dtrow["ServiceName"].ToString();
            cust.Status = "Success";
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.Phone = dtrow["Phone"].ToString();
            cust.TransID = dtrow["Ezulixtranid"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.Address = dtrow["Address"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.logo = string.IsNullOrEmpty(Convert.ToString(dtrow["Companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["Companylogo"]);
            custList.Add(cust);
        }
        return custList;
    }


    #region class
    public class Customer
    {
        public string MemberId { get; set; }
        public string ServiceName { get; set; }
        public string Id { get; set; }
        public string AddDate { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Status { get; set; }
        public string logo { get; set; }
        public string Amount { get; set; }
        public string TransID { get; set; }
        public string RefrenceNumber { get; set; }
        public string MsrNo { get; set; }
    }


    #endregion

}