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

public partial class Root_Admin_Accountopen_list : System.Web.UI.Page
{
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
    public static List<Customer> fillposrequest()
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "u" });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_Accountopen", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.BankName = dtrow["BankName"].ToString();
            cust.BannerImage = dtrow["BannerImage"].ToString();
            cust.NavigateURL = dtrow["NavigateURL"].ToString();
            cust.MemberID = dtrow["ID"].ToString();
            cust.payout = dtrow["payout"].ToString();
            cust.instructions = dtrow["instructions"].ToString();
            cust.reportlink = dtrow["reportlink"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    [WebMethod]
    public static List<Customer> fillposrequestbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
       
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_Accountopen", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.BankName = dtrow["BankName"].ToString();
            cust.BannerImage = dtrow["BannerImage"].ToString();
            cust.NavigateURL = dtrow["NavigateURL"].ToString();
            cust.MemberID = dtrow["ID"].ToString();
            cust.payout = dtrow["payout"].ToString();
            cust.instructions = dtrow["instructions"].ToString();
            cust.reportlink = dtrow["reportlink"].ToString();
            cust.AddDate = dtrow["adddate"].ToString();
            cust.Remark = dtrow["adminstatus"].ToString();
            cust.photo = dtrow["photo"].ToString();
            cust.noc = dtrow["noc"].ToString();
            cust.marksheet = dtrow["marksheet"].ToString();
            cust.status = dtrow["status"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    protected void btn_export_Click(object sender, EventArgs e)
    {
        
        
            int MsrNo = Convert.ToInt32(0);
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
          
            _lstparm.Add(new ParmList() { name = "@Action", value = "u" });
           
            dtExport = cls.select_data_dtNew("Proc_Accountopen", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "accountopen");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);
            }
       
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
        _lstparm.Add(new ParmList() { name = "@msrno", value =  Convert.ToInt32(fundid) });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_Accountopen", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
             cust.BankName = dtrow["BankName"].ToString();
            cust.BannerImage = dtrow["BannerImage"].ToString();
            cust.NavigateURL = dtrow["NavigateURL"].ToString();
            cust.MemberID = dtrow["ID"].ToString();
            cust.payout = dtrow["payout"].ToString();
            cust.instructions = dtrow["instructions"].ToString();
            cust.reportlink = dtrow["reportlink"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    #endregion

    #region class
    public class Customer
    {
        public string status { get; set; }
        public string MemberID { get; set; }
        public string Membername { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string adharnumber { get; set; }
        public string payout { get; set; }
        public string instructions { get; set; }
        public string reportlink { get; set; }
        public string Msrno { get; set; }
        public string AddDate { get; set; }
        public string Remark { get; set; }
        public string pancard { get; set; }
        public string marksheet { get; set; }
        public string passbook { get; set; }
        public string photo { get; set; }
        public string noc { get; set; }
        public string NavigateURL { get; set; }
        public string BankName { get; set; }
        public string BannerImage { get; set; }
    }

    #endregion

    [WebMethod]
    public static List<Customer> ApproveRequest(string fundid)
    {
        DataTable dt = new DataTable();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        cls_connection cls = new cls_connection();
        string ID = fundid;
        HttpContext.Current.Session["msrno"] = ID;
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "G" });
        _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(fundid) });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_Accountopen", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.BankName = dtrow["BankName"].ToString();
            cust.BannerImage = dtrow["BannerImage"].ToString();
            cust.NavigateURL = dtrow["NavigateURL"].ToString();
            cust.MemberID = dtrow["ID"].ToString();
            cust.payout = dtrow["payout"].ToString();
            cust.instructions = dtrow["instructions"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    [WebMethod]
    public static List<Customer> RejectRequest(string fundid)
    {
        DataTable dt = new DataTable();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        cls_connection cls = new cls_connection();
        string ID = fundid;
        List<Customer> custList = new List<Customer>();
        cls.select_data_dt(@"delete from tblaccountopens  Where  ID='" + ID + "'");

        return custList;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string status = "";
        string URL = TXT_URL.Text;
      
        string msrno = HttpContext.Current.Session["msrno"].ToString();
        cls.select_data_dt(@"Update tblaccountopens set NavigateURL='" + URL + "'  Where  ID='" + msrno + "'");
       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('Accountopen_list.aspx');", true);



    }
}
