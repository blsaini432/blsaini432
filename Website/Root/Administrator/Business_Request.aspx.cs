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
using System.Web.Script.Services;
public partial class Root_Admin_Business_Request : System.Web.UI.Page
{
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    string condition = " ewallettransactionid > 0";
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
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(1);
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("sp_Business_trans", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "Business_Report");
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
    public static List<Customer> Business_Request()
    {
        DataTable dtEWalletTransaction = new DataTable();
        int MsrNo = Convert.ToInt32(1);
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
       
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("sp_Business_trans", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["NAME"].ToString();
            cust.Business_Partner = dtrow["Business_Partner"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Present_Business = dtrow["Present_Business"].ToString();
            cust.Experinces = dtrow["Experinces"].ToString();
           // cust.DOB = dtrow["DOB"].ToString();
            cust.Present_income = dtrow["Present_income"].ToString();
            cust.invert_rent = dtrow["invert_rent"].ToString();
            cust.Business = dtrow["Business"].ToString();
            cust.full_address = dtrow["full_address"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();

            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> Business_Requestbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(1);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("sp_Business_trans", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["NAME"].ToString();
            cust.Business_Partner = dtrow["Business_Partner"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Present_Business = dtrow["Present_Business"].ToString();
            cust.Experinces = dtrow["Experinces"].ToString();
          //  cust.DOB = dtrow["DOB"].ToString();
            cust.Present_income = dtrow["Present_income"].ToString();
            cust.invert_rent = dtrow["invert_rent"].ToString();
            cust.Business = dtrow["Business"].ToString();
            cust.full_address = dtrow["full_address"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
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
        public string MemberName { get; set; }
        public string invert_rent { get; set; }
        public string policy_paylastdate { get; set; }
        public string Present_income { get; set; }
        public string Name { get; set; }
        public string Business { get; set; }
        public string DOB { get; set; }
        public string full_address { get; set; }
        public string mobile { get; set; }
        public string Experinces { get; set; }
        public string Createdate { get; set; }
        public string Present_Business { get; set; }
        public string Business_Partner { get; set; }
    }

    #endregion
}