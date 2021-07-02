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
        if (Session["dtDistributor"] != null)
        {
            DataTable dtmember = (DataTable)Session["dtDistributor"];
            if (dtmember.Rows.Count > 0)
            {
                int msrno = Convert.ToInt32(dtmember.Rows[0]["MsrNo"]);
               // BindLoginDetails(msrno);
                Binnews();
                BindServices();
                bindbanner();
                bindmodel();
                mpe_dmrotp.Show();

            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }

    }
    #endregion

    #region Methods
    //public void BindLoginDetails(int msrno)
    //{
    //    List<ParmList> _lstparm = new List<ParmList>();
    //    _lstparm.Add(new ParmList() { name = "@ID", value = msrno });
    //    _lstparm.Add(new ParmList() { name = "@Action", value = "GetByMsrNo" });
    //    cls_connection cls = new cls_connection();
    //    DataTable dtMemberLoginDetail = cls.select_data_dtNew("ProcMLM_ManageMemberMasterLoginDetail", _lstparm);
    //    if (dtMemberLoginDetail.Rows.Count > 0)
    //    {
    //        repEmployeeLoginDetail.DataSource = dtMemberLoginDetail;
    //        repEmployeeLoginDetail.DataBind();
    //    }

    //}
    public void Binnews()
    {
        cls_connection cls = new cls_connection();
        DataTable dtnews = cls.select_data_dt("Newssection");
        if(dtnews.Rows.Count > 0)
        {
            rptnews.DataSource = dtnews;
            rptnews.DataBind();
        }
    }

    public void bindbanner()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 3 BannerImage from tblbanner where isactive=1 and isdelete=0 order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }

    }

    public void BindServices()
    {
        if (HttpContext.Current.Session["dtDistributor"] != null)
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
            string memberid = dt.Rows[0]["MemberId"].ToString();
            DataTable dtEWalletTransaction = new DataTable();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@memberid", value = memberid });
            dtEWalletTransaction = cls.select_data_dtNew("Sp_getservices", _lstparm);
            rep.DataSource = dtEWalletTransaction;
            rep.DataBind();
        }
    }

    [WebMethod]
    public static List<Customer> Bindmemberdata()
    {
        List<Customer> custList = new List<Customer>();
        if (HttpContext.Current.Session["dtDistributor"] != null)
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
            DataTable dtEWalletTransaction = new DataTable();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
            dtEWalletTransaction = cls.select_data_dtNew("TotalMemberDashboard", _lstparm);
            foreach (DataRow dtrow in dtEWalletTransaction.Rows)
            {
                Customer cust = new Customer();
                cust.RBalance = dtrow["RBalance"].ToString();
                cust.EBalance = dtrow["EBalance"].ToString();
                cust.rechargeamount = dtrow["rechargeamount"].ToString();
                cust.totaldmr = dtrow["totaldmr"].ToString();
                cust.MemberId = dtrow["MemberId"].ToString();
                cust.MemberType = dtrow["MemberType"].ToString();
                cust.FirstName = dtrow["FirstName"].ToString();
                cust.companylogomain = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
                cust.companylogomini = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
                cust.imgprofile = string.IsNullOrEmpty(Convert.ToString(dtrow["userprofile"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/User/Profile/" + Convert.ToString(dtrow["userprofile"]);
                custList.Add(cust);
            }
        }
        return custList;
    }
    #endregion

    public void bindmodel()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners  order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater2.DataSource = dt;
            repeater2.DataBind();
        }

    }

    protected void btn_Closedmr_Click(object sender, EventArgs e)
    {
        mpe_dmrotp.Hide();
    }

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberId { get; set; }
        public string RBalance { get; set; }
        public string EBalance { get; set; }
        public string rechargeamount { get; set; }
        public string totaldmr { get; set; }
        public string companylogomain { get; set; }
        public string companylogomini { get; set; }
        public string imgprofile { get; set; }
        public string MemberType { get; set; }
        public string FirstName { get; set; }
    }
    #endregion
}