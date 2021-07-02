using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Root_Admin_Passportservices_Request : System.Web.UI.Page
{
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public DataTable dtMemberMaster = new DataTable();
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
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(0);
            DataTable dtEWalletTransaction = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
            dtExport = cls.select_data_dtNew("Set_Passportservices_report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "pancard_Report");
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
    public static List<Customer> pancardreport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(0) });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Passportservices_report", _lstparm);
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
            cust.photo = dtrow["photo"].ToString();
            cust.aadhar = dtrow["aadhar"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> pancardreporttbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Passportservices_report", _lstparm);
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
            cust.photo = dtrow["photo"].ToString();
            cust.aadhar = dtrow["aadhar"].ToString();
            cust.paydate = dtrow["paydate"].ToString();
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
        public string fathername { get; set; }
        public string email { get; set; }
        public string txnid { get; set; }
        public string commission { get; set; }
        public string Status { get; set; }
        public string paydate { get; set; }
        public string logo { get; set; }
        public string photo { get; set; }
        public string aadhar { get; set; }
    }
    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
      
            string id = fundid;
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from Tbl_Passportservices where txnid='" + id + "' and statu='Pending'");        
            string txnid = string.Empty;        
            String Result = string.Empty;
            if (dt.Rows.Count > 0)
            {
                txnid = dt.Rows[0]["txnid"].ToString();
                cls_myMember clsm = new cls_myMember();
                cls.select_data_dt(@"Update Tbl_Passportservices set statu='SUCCESS' Where  txnid='" + id + "'");
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
        dt = cls.select_data_dt("select * from Tbl_Passportservices where txnid='" + txn + "' and statu='Pending'");
        string status = string.Empty;
        string txnid = string.Empty;
        string totalamt = string.Empty;
        string memberid = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
            txnid = dt.Rows[0]["txnid"].ToString();
            totalamt = dt.Rows[0]["amount"].ToString();
            cls_myMember clsm = new cls_myMember();
            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(totalamt)), "Cr", "Reverse Passport Service TxnID:-" + txnid);
            cls.select_data_dt(@"Update Tbl_Passportservices set statu='Fail' Where  txnid='" + txn + "'");
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }
    }







}