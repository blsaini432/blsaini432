using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;


public partial class Root_Distributor_Vehicleowner_report : System.Web.UI.Page
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
            if (Session["dtDistributor"] != null)
            {
                if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");

                }
                DataTable dtmembermaster = new DataTable();
                dtmembermaster = (DataTable)Session["dtDistributor"];
               
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
            DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
            DataTable dtExport = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "rt" });
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("pro_VEHICLEOWNERSHIP", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "Drvingliciense_report");
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
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(MsrNo) });
        _lstparm.Add(new ParmList() { name = "@Action", value = "rt" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("pro_VEHICLEOWNERSHIP", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["fran_code"].ToString();
            cust.Custmer_Number = dtrow["name"].ToString();
            cust.father_name = dtrow["father_name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.state = dtrow["state"].ToString();
            cust.city = dtrow["city"].ToString();
            cust.type = dtrow["type"].ToString();
            cust.HP = dtrow["HP"].ToString();
            cust.Date = dtrow["Date"].ToString();
          
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
        dtEWalletTransaction = cls.select_data_dtNew("pro_VEHICLEOWNERSHIP", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["fran_code"].ToString();
            cust.Custmer_Number = dtrow["name"].ToString();
            cust.father_name = dtrow["father_name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.state = dtrow["state"].ToString();
            cust.city = dtrow["city"].ToString();
            cust.type = dtrow["type"].ToString();
            cust.HP = dtrow["HP"].ToString();
            cust.Date = dtrow["Date"].ToString();
            // cust.logo = string.IsNullOrEmpty(Convert.ToString(dtrow["Companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["Companylogo"]);
            custList.Add(cust);
        }
        return custList;
    }





    [WebMethod]
    public static List<Customer> fillaepstransactionsbydate(string fromdate, string todate)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "rt" });
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("pro_VEHICLEOWNERSHIP", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["fran_code"].ToString();
            cust.Custmer_Number = dtrow["name"].ToString();
            cust.father_name = dtrow["father_name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.state = dtrow["state"].ToString();
            cust.city = dtrow["city"].ToString();
            cust.type = dtrow["type"].ToString();
            cust.HP = dtrow["HP"].ToString();
            cust.Date = dtrow["Date"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion

    #region class
    public class Customer
    {
        public string memberid { get; set; }
        public string father_name { get; set; }
        public string Custmer_Number { get; set; }
        public string mobile { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string type { get; set; }
        public string HP { get; set; }
        public string Date { get; set; }
        public string msg { get; set; }
        public string BANK_NAME { get; set; }
        public string AadharNumber { get; set; }

    }
    #endregion

}