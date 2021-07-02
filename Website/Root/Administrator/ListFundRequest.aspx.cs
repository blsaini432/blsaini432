using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Root_Admin_ListFundRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
            {
                DataTable dtemployee = (DataTable)Session["dtEmployee"];
                if (dtemployee.Rows.Count > 0)
                {
                    ViewState["adminmobile"] = dtemployee.Rows[0]["Mobile"].ToString();
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                }
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
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetAll" });
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
            cust.ToMember = dtrow["ToMember"].ToString();
            cust.PaymentMode = dtrow["PaymentMode"].ToString();
            cust.ChequeOrDDNumber = dtrow["ChequeOrDDNumber"].ToString();
            cust.PaymentProof = dtrow["PaymentProof"].ToString();
            cust.RequestStatus = dtrow["RequestStatus"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.Adminremark = dtrow["RCode"].ToString();
            cust.Remark = dtrow["Remark"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    [WebMethod]
    public static List<Customer> fillfundrequestbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetAll" });
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
            cust.ToMember = dtrow["ToMember"].ToString();
            cust.PaymentMode = dtrow["PaymentMode"].ToString();
            cust.ChequeOrDDNumber = dtrow["ChequeOrDDNumber"].ToString();
            cust.PaymentProof = dtrow["PaymentProof"].ToString();
            cust.RequestStatus = dtrow["RequestStatus"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.Adminremark = dtrow["RCode"].ToString();
            cust.Remark = dtrow["Remark"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
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
            _lstparm.Add(new ParmList() { name = "@Action", value = "GetAll" });
            _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("ProcMLM_ManageFundRequest", _lstparm);
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
        _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(fundid) });
        dtEWalletTransaction = cls.select_data_dtNew("ProcMLM_ManageFundRequest", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.PaymentProof = dtrow["PaymentProof"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    #endregion



    [WebMethod]
    public static List<Customer> faild(string fundid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        string msrnoid = fundid;
        HttpContext.Current.Session["msrno"] = msrnoid;
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetImage" });
        _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(fundid) });
        dtEWalletTransaction = cls.select_data_dtNew("ProcMLM_ManageFundRequest", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.Amount = dtrow["Amount"].ToString();
            HttpContext.Current.Session["amount"] = cust.Amount;
            cust.PaymentProof = dtrow["PaymentProof"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    #region class
    public class Customer
    {
        public string Adminremark { get; set; }
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
        public string Amount { get; set; }
    }

    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequest(string fundid)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "IsApprove" });
        _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(fundid) });
        dt = cls.select_data_dtNew("ProcMLM_ManageFundRequest ", _lstparm);
        actions = "success";
        return actions;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string RejectRequest(string fundid)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "IsDelete" });
        _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(fundid) });
        dt = cls.select_data_dtNew("ProcMLM_ManageFundRequest ", _lstparm);
        actions = "success";
        return actions;
    }


    protected void btnSubmit_Reject(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string txnid = HttpContext.Current.Session["msrno"].ToString();
        string adminstatus = txt_status.Text;
        if (adminstatus != "" && txnid != "")
        {
            cls.select_data_dt(@"Update tblMLM_FundRequest set RCode='" + adminstatus + "'  Where  FundRequestID='" + txnid + "'");
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "IsDelete" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(txnid) });
            dt = cls.select_data_dtNew("ProcMLM_ManageFundRequest ", _lstparm);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Update successfully!');location.replace('ListFundRequest.aspx');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload  remark!');location.replace('ListFundRequest.aspx');", true);
        }


    }

    protected void btnSubmit_Apprvo(object sender, EventArgs e)
    {
        //if (txt_fundotp.Text.Trim() == Session["chdmtOTP"].ToString())
       // {
            DataTable dtd = new DataTable();
            DataTable dt = new DataTable();
            clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
            cls_connection cls = new cls_connection();
            DataTable dtwallet = new DataTable();
            string txnid = HttpContext.Current.Session["msrno"].ToString();
            string adminstatus = HttpContext.Current.Session["adminstatus"].ToString();
            if (adminstatus != "" && txnid != "")
            {
                cls.select_data_dt(@"Update tblMLM_FundRequest set RCode='" + adminstatus + "'  Where  FundRequestID='" + txnid + "'");
                dtd = cls.select_data_dt("select msrno,Amount from tblMLM_FundRequest where FundRequestID='" + txnid + "'");
                int msrno = Convert.ToInt32(dtd.Rows[0]["msrno"]);
                int amount = Convert.ToInt32(dtd.Rows[0]["Amount"]);
                dtd = cls.select_data_dt("select * from tblMLM_membermaster where msrno='" + msrno + "'");
                clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@Action", value = "IsApprove" });
                _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(txnid) });
                dt = cls.select_data_dtNew("ProcMLM_ManageFundRequest ", _lstparm);
                dtwallet = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", msrno);
                string[] valueArray = new string[2];
                valueArray[0] = amount.ToString();
                valueArray[1] = Convert.ToString(dtwallet.Rows[0]["balance"]);
                DLTSMS.SendWithVar(dtd.Rows[0]["mobile"].ToString().ToString(), 6, valueArray, msrno);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Update successfully!');location.replace('ListFundRequest.aspx');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload  remark!');location.replace('ListFundRequest.aspx');", true);
            }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter valid OTP ');", true);
        //    fundotp.Show();

        //}


    }

    protected void btn_fundotp_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Session["dtEmployee"] != null)
        {
            string adminstatus = txt_statuss.Text;
            HttpContext.Current.Session["adminstatus"] = adminstatus;
            String Amount = HttpContext.Current.Session["amount"].ToString();
            Random random = new Random();
            int SixDigit = random.Next(1000, 9999);
            Session["chdmtOTP"] = SixDigit.ToString();
            string[] valueArray = new string[2];
            valueArray[0] = Amount;
            valueArray[1] = Session["chdmtOTP"].ToString();
            SMS.SendWithVar(ViewState["adminmobile"].ToString(), 21, valueArray, 1);
            // SendWithVarpan(ViewState["adminmobile"].ToString(), 1, valueArray);
            fundotp.Show();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some error found please try again !');", true);
        }
    }
   

}
