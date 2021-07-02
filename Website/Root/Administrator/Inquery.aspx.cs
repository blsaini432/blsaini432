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
public partial class Root_Administrator_Inquery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillreport();

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
    public static List<Customer> fillreport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        int MsrNo = Convert.ToInt32(1);
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "admin" });     
        dtEWalletTransaction = cls.select_data_dtNew("contact", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();        
            cust.name = dtrow["name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();          
            cust.email = dtrow["email"].ToString();
            cust.message = dtrow["message"].ToString();
            cust.Date = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> fillreportbydate(string fromdate, string todate)
    {   
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("contact_admin", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.name = dtrow["name"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.message = dtrow["message"].ToString();
            cust.Date = dtrow["Createdate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    #endregion

    #region class
    public class Customer
    {
        public string message { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string Date { get; set; }
    }


    #endregion


}