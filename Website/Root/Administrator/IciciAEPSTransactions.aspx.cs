using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using BLL;

public partial class Root_Admin_IciciAEPSTransactions : System.Web.UI.Page
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
    public static List<Customer> fillaepstransactions()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(0) });
        _lstparm.Add(new ParmList() { name = "@rtype", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("IciciAEPS_Transaction", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.op = dtrow["op"].ToString();
            cust.memberid = dtrow["memberid"].ToString();
            cust.Member_Name = dtrow["Member_Name"].ToString();
          //  HttpContext.Current.Session["datetotalaeps"] = dtrow["total"].ToString();
            cust.Custmer_Number = dtrow["Custmer_Number"].ToString();
            cust.order_id = dtrow["order_id"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Adm_Com = dtrow["Adm_Com"].ToString();
            cust.CG = dtrow["CG"].ToString();
            cust.creted = dtrow["creted"].ToString();
            cust.txnstatus = dtrow["txnstatus"].ToString();
            cust.msg = dtrow["RESP_MSG"].ToString();
            cust.rrn = dtrow["rrn"].ToString();
            cust.aadhar = dtrow["aadhar"].ToString();
            
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
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@rtype", value = "admin" });
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("IciciAEPS_Transaction", _lstparm);
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

    [WebMethod]
    public static List<Customer> fillaepstransactionsbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@rtype", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("IciciAEPS_Transaction", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.op = dtrow["op"].ToString();
            cust.memberid = dtrow["memberid"].ToString();
            cust.Member_Name = dtrow["Member_Name"].ToString();
            cust.Custmer_Number = dtrow["Custmer_Number"].ToString();
            cust.order_id = dtrow["order_id"].ToString();
            HttpContext.Current.Session["datetotalaeps"] = dtrow["total"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Adm_Com = dtrow["Adm_Com"].ToString();
            cust.CG = dtrow["CG"].ToString();
            cust.creted = dtrow["creted"].ToString();
            cust.txnstatus = dtrow["txnstatus"].ToString();
            cust.msg = dtrow["RESP_MSG"].ToString();
            cust.rrn = dtrow["rrn"].ToString();
            cust.aadhar = dtrow["aadhar"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion

    #region class
    public class Customer
    {
        public string op { get; set; }
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
        public string rrn { get; set; }
        public string aadhar { get; set; }
    }
    #endregion

}