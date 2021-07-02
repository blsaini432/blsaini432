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

public partial class Root_Distributor_myDMRTransactions : System.Web.UI.Page
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

                if (Txt_FromDate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    Txt_FromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
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
    public static List<Customer> fillDMTreport()
    {
        int MsrNo = Convert.ToInt32(mssrno);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("RP_DMR_transaction", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["SenderMobile"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.BeneAC = dtrow["BeneAC"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.RefNO = dtrow["RefNO"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.TxnID = dtrow["TxnID"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    [WebMethod]
    public static List<Customer> fillDMTreportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(mssrno);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("RP_DMR_transaction", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.SenderMobile = dtrow["SenderMobile"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.BeneAC = dtrow["BeneAC"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.RefNO = dtrow["RefNO"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.TxnID = dtrow["TxnID"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
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
        dtEWalletTransaction = cls.select_data_dtNew("RP_DMR_transaction", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            //Customer cust = new Customer();
            //cust.MemberID = dtrow["MemberID"].ToString();
            //cust.servicename = dtrow["servicename"].ToString();
            //cust.agent_id = dtrow["agent_id"].ToString();
            //cust.opr_id = dtrow["opr_id"].ToString();
            //cust.account_no = dtrow["account_no"].ToString();
            //cust.trans_amt = dtrow["trans_amt"].ToString();
            //cust.Status = dtrow["Status"].ToString();
            //cust.paydate = dtrow["paydate"].ToString();
            //cust.logo = string.IsNullOrEmpty(Convert.ToString(dtrow["Companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["Companylogo"]);
            //custList.Add(cust);
        }
        return custList;
    }




    #endregion

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string SenderMobile { get; set; }
        public string Name { get; set; }
        public string BeneAC { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
        public string RefNO { get; set; }
        public string TxnID { get; set; }
        public string Createdate { get; set; }
        public string logo { get; set; }
    }

    #endregion

}