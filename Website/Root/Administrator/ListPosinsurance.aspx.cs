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


public partial class Root_Admin_ListPosinsurance : System.Web.UI.Page
{
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
    public static List<Customer> fillposrequest()
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        dtEWalletTransaction = cls.select_data_dtNew("posinsurance_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.passbook = dtrow["passbook"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Msrno = dtrow["Msrno"].ToString();
            cust.Membername = dtrow["NAME"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.adharnumber = dtrow["adharnumber"].ToString();
            cust.pannumber = dtrow["pannumber"].ToString();
            cust.aadharfont = dtrow["aadharfont"].ToString();
            cust.adharback = dtrow["adharback"].ToString();
            cust.pancard = dtrow["pancard"].ToString();
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
        dtEWalletTransaction = cls.select_data_dtNew("posinsurance_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.passbook = dtrow["passbook"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Membername = dtrow["NAME"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.adharnumber = dtrow["adharnumber"].ToString();
            cust.pannumber = dtrow["pannumber"].ToString();
            cust.aadharfont = dtrow["aadharfont"].ToString();
            cust.adharback = dtrow["adharback"].ToString();
            cust.pancard = dtrow["pancard"].ToString();

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
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(0);
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
          
            _lstparm.Add(new ParmList() { name = "@Msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("posinsurance_report", _lstparm);
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
        dtEWalletTransaction = cls.select_data_dtNew("posinsurance_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.passbook = dtrow["passbook"].ToString();
            cust.aadharfont = dtrow["aadharfont"].ToString();
            cust.adharback = dtrow["adharback"].ToString();
            cust.pancard = dtrow["pancard"].ToString();
            cust.photo = dtrow["photo"].ToString();
            cust.noc = dtrow["noc"].ToString();
            cust.marksheet = dtrow["marksheet"].ToString();
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
        public string pannumber { get; set; }
        public string aadharfont { get; set; }
        public string adharback { get; set; }
        public string Msrno { get; set; }
        public string AddDate { get; set; }
        public string Remark { get; set; }
        public string pancard { get; set; }
        public string marksheet { get; set; }
        public string passbook { get; set; }
        public string photo { get; set; }
        public string noc { get; set; }
    }

    #endregion

    [WebMethod]
    public static List<Customer> ApproveRequest(string msrno)
    {
        DataTable dt = new DataTable();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        cls_connection cls = new cls_connection();
        string msrnoid = msrno;
        HttpContext.Current.Session["msrno"] = msrnoid;
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetImage" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
        dtEWalletTransaction = cls.select_data_dtNew("posinsurance_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.passbook = dtrow["passbook"].ToString();
           
            custList.Add(cust);
        }
        return custList;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string status = droplist.SelectedItem.ToString();
        string adminstatus = txt_status.Text;
        string msrno = HttpContext.Current.Session["msrno"].ToString();
        cls.select_data_dt(@"Update tbl_posinsurance set status='" + status + "',adminstatus='"+adminstatus+"'  Where  msrno='" + msrno + "'");
       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('ListPosinsurance.aspx');", true);



    }
}
