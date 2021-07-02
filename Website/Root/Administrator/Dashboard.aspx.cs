using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Web.Services;

public partial class Root_Admin_Dashboard : System.Web.UI.Page
{
    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmployeeID"] != null)
        {
            BindLoginDetails();
            Bindmemberdata();
        }
        else
        {
            Response.Redirect("~/adminlogin.aspx");
        }
    }
    #endregion


    #region Methods
    public void BindLoginDetails()
    {
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(Session["EmployeeID"]) });
        _lstparm.Add(new ParmList() { name = "@Action", value = "Get10ByEmployeeID" });
        cls_connection cls = new cls_connection();
        DataTable dtEmployeeLoginDetail = cls.select_data_dtNew("Proc_ManageEmployeeLoginDetail", _lstparm);
        if (dtEmployeeLoginDetail.Rows.Count > 0)
        {
            //blLastLoginDate.Text = String.Format("{0:dd-MMM-yyyy hh:mm tt}", Convert.ToDateTime(dtEmployeeLoginDetail.Rows[0]["LoginDate"]));
            repEmployeeLoginDetail.DataSource = dtEmployeeLoginDetail;
            repEmployeeLoginDetail.DataBind();
        }

    }





    [WebMethod]
    public static List<Customer> Bindmemberdata()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        dtEWalletTransaction = cls.select_data_dtNew("TotalAdminDashboard", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.EBalance = dtrow["EBalance"].ToString();
            cust.RBalance = dtrow["RBalance"].ToString();
            cust.fundGiven = dtrow["fundGiven"].ToString();
            cust.TotalMember = dtrow["TotalMember"].ToString();
            cust.companylogomain = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
            cust.companylogomini = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
            cust.imgprofile = "../../Uploads/User/Profile/dummy.png";
            custList.Add(cust);
        }
        return custList;
    }


    #region class
    public class Customer
    {
        public string MemberId { get; set; }
        public string RBalance { get; set; }
        public string EBalance { get; set; }
        public string companylogomain { get; set; }
        public string companylogomini { get; set; }
        public string imgprofile { get; set; }
        public string TotalMember { get; set; }
        public string fundGiven { get; set; }
    }
    #endregion
    #endregion
}