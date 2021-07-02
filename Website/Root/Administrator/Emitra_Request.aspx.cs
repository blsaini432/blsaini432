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


public partial class Root_Admin_Emitra_Request : System.Web.UI.Page
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
    public static List<Customer> fillrequest()
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        dtEWalletTransaction = cls.select_data_dtNew("Set_emitra", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["memberid"].ToString();
            cust.customername = dtrow["ssoid"].ToString();
            cust.customermobile = dtrow["mobile"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.shopname = dtrow["shopname"].ToString();
            cust.shopaddress = dtrow["shopaddress"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.msrno = dtrow["msrno"].ToString();
            cust.adminstatus = dtrow["adminstatus"].ToString();
            cust.Status = dtrow["statu"].ToString();
            cust.paydate = dtrow["paydate"].ToString();
            cust.photo = dtrow["photo"].ToString();
            cust.aadhar = dtrow["aadhar"].ToString();
            cust.pancard = dtrow["pancard"].ToString();
            cust.police_verification = dtrow["police_verification"].ToString();
            cust.Bank_details = dtrow["Bank_details"].ToString();
            cust.jan_aadhar = dtrow["jan_aadhar"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    [WebMethod]
    public static List<Customer> fillrequestbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Set_emitra", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["memberid"].ToString();
            cust.customername = dtrow["ssoid"].ToString();
            cust.customermobile = dtrow["mobile"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.adminstatus = dtrow["adminstatus"].ToString();
            cust.msrno = dtrow["msrno"].ToString();
            cust.shopname = dtrow["shopname"].ToString();
            cust.shopaddress = dtrow["shopaddress"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.Status = dtrow["statu"].ToString();
            cust.paydate = dtrow["paydate"].ToString();
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
        _lstparm.Add(new ParmList() { name = "@msrno", value = fundid });
        dtEWalletTransaction = cls.select_data_dtNew("Set_emitra", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["memberid"].ToString();
            cust.photo = dtrow["photo"].ToString();
            cust.aadhar = dtrow["aadhar"].ToString();
            cust.pancard = dtrow["pancard"].ToString();
            cust.police_verification = dtrow["police_verification"].ToString();
            cust.Bank_details = dtrow["Bank_details"].ToString();
            cust.jan_aadhar = dtrow["jan_aadhar"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    #endregion

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string customername { get; set; }
        public string customermobile { get; set; }
        public string shopaddress { get; set; }
        public string email { get; set; }
        public string txnid { get; set; }
        public string shopname { get; set; }
        public string Status { get; set; }
        public string paydate { get; set; }
        public string logo { get; set; }
        public string photo { get; set; }
        public string aadhar { get; set; }
        public string police_verification { get; set; }
        public string pancard { get; set; }
        public string Bank_details { get; set; }
        public string jan_aadhar { get; set; }
        public string msrno { get; set; }
        public string adminstatus { get; set; }


    }

    #endregion

    [WebMethod]
    public static List<Customer> AppfroveRequest(string msrno)
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
        dtEWalletTransaction = cls.select_data_dtNew("Set_emitra", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();

            cust.MemberID = dtrow["memberid"].ToString();

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
        cls.select_data_dt(@"Update Tbl_emitera set statu='" + status + "',adminstatus='" + adminstatus + "'  Where  msrno='" + msrno + "'");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('Emitra_Request.aspx');", true);



    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";

        string id = fundid;
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select * from Tbl_emitera where txnid='" + id + "' and statu='Pending'");
        string status = string.Empty;
        string txnid = string.Empty;
        string totalamt = string.Empty;
        string memberid = string.Empty;
        string spkey = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
            txnid = dt.Rows[0]["txnid"].ToString();
            cls_myMember clsm = new cls_myMember();
            cls.select_data_dt(@"Update Tbl_emitera set statu='SUCCESS' Where  txnid='" + id + "'");
            //cls.select_data_dt(@"EXEC PROC_water_COM_New @txnamount=" + Convert.ToDecimal(dt.Rows[0]["trans_amt"].ToString()) + ",@CMemberId='" + memberid.ToString() + "',@TxnId='" + txnid + "',@Serviceboard='" + spkey + "'");
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string RejectRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        string txn = fundid;
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select * from Tbl_emitera where txnid='" + txn + "' and statu='Pending'");
        string status = string.Empty;
        string txnid = string.Empty;
        string totalamt = string.Empty;
        string memberid = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
            txnid = dt.Rows[0]["txnid"].ToString();
            totalamt = dt.Rows[0]["amount"].ToString();
            memberid = dt.Rows[0]["memberid"].ToString();
            cls_myMember clsm = new cls_myMember();
            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(totalamt)), "Cr", "Reverse Emitra  TxnID:-" + txnid);
            cls.select_data_dt(@"Update Tbl_emitera set statu='Fail' Where  txnid='" + txn + "'");
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }
    }

}
