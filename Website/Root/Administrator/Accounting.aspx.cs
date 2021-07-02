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

public partial class Root_Admin_Accounting : System.Web.UI.Page
{

    #region Properties
    cls_connection Cls = new cls_connection();
    DataTable dtEWalletTransaction = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (Txt_FromDate.Text.Trim() == "" )
            {
               
                Txt_FromDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
               
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
    public static List<Customer> filldmrreport()
    {

        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@rtype", value = "todate" });
        dtEWalletTransaction = cls.select_data_dtNew("Accounting", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {

            Customer cust = new Customer();
            cust.MemberID = dtrow["total"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> filldmrreportbydate( string service, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(todate) });
        _lstparm.Add(new ParmList() { name = "@rtype", value = service });
        dtEWalletTransaction = cls.select_data_dtNew("Accounting", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.amount = dtrow["TOTAL"].ToString();
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
        public string Bank { get; set; }
        public string SenderMobile { get; set; }
        public string Name { get; set; }
        public string RefNo { get; set; }
        public string TxnID { get; set; }
        public string BeneAC { get; set; }
        public string Ifsc { get; set; }
        public string Amount { get; set; }
        public string RRNNO { get; set; }
        public string BankTxnId { get; set; }
        public string AgentOrderId { get; set; }
        public string EzulixorderId { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
        public string TxnDate { get; set; }
        public string beneficiaryAccount { get; set; }
        public string amount { get; set; }
        public string logo { get; set; }
    }

    #endregion

}