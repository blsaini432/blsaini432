using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;


public partial class Root_Retailer_AEPSTransactionss : System.Web.UI.Page
{
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public DataTable dtMemberMaster = new DataTable();
    cls_myMember Clsm = new cls_myMember();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {
                if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");

                }
                DataTable dtmembermaster = new DataTable();
                dtmembermaster = (DataTable)Session["dtRetailer"];
               
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }


            

        }
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
            int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
            DataTable dtExport = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@rtype", value = "rt" });
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("AEPS_Transaction", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "AEPSTransaction_Report");
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

    #region [Function]
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillaepstransactions()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(MsrNo) });
        _lstparm.Add(new ParmList() { name = "@rtype", value = "rt" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("AEPS_Transaction", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["memberid"].ToString();
            cust.Custmer_Number = dtrow["Custmer_Number"].ToString();
            cust.order_id = dtrow["order_id"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.creted = dtrow["creted"].ToString();
            cust.txnstatus = dtrow["txnstatus"].ToString();
            cust.msg = dtrow["RESP_MSG"].ToString();
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
        _lstparm.Add(new ParmList() { name = "@rtype", value = "recpt" });
        _lstparm.Add(new ParmList() { name = "@order_id", value = txnid });
        dtEWalletTransaction = cls.select_data_dtNew("AEPS_Transaction", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["memberid"].ToString();
            // cust.Custmer_Number = dtrow["Custmer_Number"].ToString();
            cust.order_id = dtrow["order_id"].ToString();
            cust.Amount = dtrow["txn_amount_tra"].ToString();
            cust.creted = dtrow["creted"].ToString();
            cust.txnstatus = dtrow["txnstatus"].ToString();
            cust.AadharNumber = dtrow["AadharNumber"].ToString();
            cust.BANK_NAME = dtrow["BANK_NAME"].ToString();
            // cust.logo = string.IsNullOrEmpty(Convert.ToString(dtrow["Companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["Companylogo"]);
            custList.Add(cust);
        }
        return custList;
    }





    [WebMethod]
    public static List<Customer> fillaepstransactionsbydate(string fromdate, string todate)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@rtype", value = "rt" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("AEPS_Transaction", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["memberid"].ToString();
            cust.Custmer_Number = dtrow["Custmer_Number"].ToString();
            cust.order_id = dtrow["order_id"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.creted = dtrow["creted"].ToString();
            cust.txnstatus = dtrow["txnstatus"].ToString();
            cust.msg = dtrow["RESP_MSG"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion

    #region class
    public class Customer
    {
        public string memberid { get; set; }
        public string Member_Name { get; set; }
        public string Custmer_Number { get; set; }
        public string order_id { get; set; }
        public string Amount { get; set; }
        public string Adm_Com { get; set; }
        public string CG { get; set; }
        public string creted { get; set; }
        public string txnstatus { get; set; }
        public string msg { get; set; }
        public string BANK_NAME { get; set; }
        public string AadharNumber { get; set; }

    }
    #endregion

}