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

public partial class Root_Distributor_AepsNewTranscation : System.Web.UI.Page
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
    public static List<Customer> fillaepsreport()
    {
        int MsrNo = Convert.ToInt32(mssrno);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@rtype", value = "rt" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("AEPS_Transaction_New", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();

            cust.MemberID = dtrow["MemberID"].ToString();
            cust.servicename = dtrow["CustmerMobile"].ToString();
            cust.TxnAmount = dtrow["TxnAmount"].ToString();
            cust.commission = dtrow["commission"].ToString();
            cust.RetailerTxnId = dtrow["RetailerTxnId"].ToString();
            cust.TxnDate = dtrow["TxnDate"].ToString();

            custList.Add(cust);
        }
        return custList;
    }



    [WebMethod]
    public static List<Customer> fillaepsreportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(mssrno);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@rtype", value = "rt" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("AEPS_Transaction_New", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.servicename = dtrow["CustmerMobile"].ToString();
            cust.TxnAmount = dtrow["TxnAmount"].ToString();
            cust.commission = dtrow["commission"].ToString();
            cust.RetailerTxnId = dtrow["RetailerTxnId"].ToString();
            cust.TxnDate = dtrow["TxnDate"].ToString();
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
        public string servicename { get; set; }
        public string TxnAmount { get; set; }
        public string commission { get; set; }
        public string RetailerTxnId { get; set; }
        public string TxnDate { get; set; }
    
    }

    #endregion


}