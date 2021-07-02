using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.Services;
public partial class Root_Retailer_MemberMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {

                DataTable dtRetailer = (DataTable)Session["dtRetailer"];
                int msrno = Convert.ToInt32(dtRetailer.Rows[0]["MsrNo"]);
                Session["RetailerMsrNo"] = msrno;
                Session["MsrNoLog"] = msrno;
                Session["MemberIdLog"] = dtRetailer.Rows[0]["MemberId"].ToString();
                DynamicMenu();
                loaddashboard();
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }
    protected void linkbtn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("~/userlogin.aspx");
    }

    #region BindMenu
    StringBuilder strBud = new StringBuilder();
    private void DynamicMenu()
    {
        StringBuilder strbuild = new StringBuilder();
        DataTable dtParent = new DataTable();
        cls_connection cls = new cls_connection();
        dtParent = cls.select_data_dt(@"select * from tblMLM_MenuMember where ParentID=0 and IsActive=1 order by position");
        int a = dtParent.Rows.Count;
        strbuild.Append("<ul id='main-menu-navigation' class='nav'>");
        for (int i = 0; i < a; i++)
        {
            DataTable dtChild = new DataTable();
            dtChild = cls.select_data_dt(@"select * from tblMLM_MenuMember where ParentID='" + dtParent.Rows[i]["MenuID"].ToString() + "' and IsActive=1");
            int b = dtChild.Rows.Count;
            if (b > 0)
            {
                strbuild.Append("<li class='nav-item'><a href='#" + dtParent.Rows[i]["MenuId"].ToString() + "' class='nav-link'  data-toggle='collapse'  aria-expanded='false'><i class='" + dtParent.Rows[i]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtParent.Rows[i]["MenuName"].ToString() + "</span><i class='menu-arrow'></i></a>");
                strbuild.Append("<div class='collapse' style='' id='" + dtParent.Rows[i]["MenuId"].ToString() + "'>");
                strbuild.Append("<ul class='nav flex-column sub-menu'>");
                for (int j = 0; j < b; j++)
                {
                    DataTable dtSubchild = new DataTable();
                    dtSubchild = cls.select_data_dt(@"select * from tblMLM_MenuMember where ParentID='" + dtChild.Rows[j]["MenuID"].ToString() + "' and IsActive=1");
                    int c = dtSubchild.Rows.Count;
                    if (c > 0)
                    {
                        strbuild.Append("<li class='nav-item'><a href='#" + dtChild.Rows[j]["MenuId"].ToString() + "' class='nav-link'> <i class='" + dtChild.Rows[j]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtChild.Rows[j]["MenuName"].ToString() + "</span><i class='menu-arrow'></i></a>");
                        strbuild.Append("<div class='collapse' style='' id='" + dtChild.Rows[j]["MenuId"].ToString() + "'>");
                        strbuild.Append("<ul class='nav flex-column sub-menu'>");
                        for (int k = 0; k < c; k++)
                        {
                            strbuild.Append("<li><a href=" + dtSubchild.Rows[k]["MenuLink"].ToString() + " class='nav-link'  data-toggle='collapse'  aria-expanded='false'>" + dtSubchild.Rows[k]["MenuName"].ToString() + "</a></li>");
                        }
                        strbuild.Append("</ul>");
                        strbuild.Append("</div>");
                        strbuild.Append("</li>");
                    }
                    else
                    {
                        strbuild.Append("<li class='nav-item'><a href='" + dtChild.Rows[j]["MenuLink"].ToString() + "' class='nav-link'><i class='" + dtChild.Rows[j]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtChild.Rows[j]["MenuName"].ToString() + "</a></li>");
                    }
                }
                strbuild.Append("</ul>");
                strbuild.Append("</div>");
                strbuild.Append("</li>");
            }
            else
            {
                strbuild.Append("<li class='nav-item'><a href='" + dtParent.Rows[i]["MenuLink"].ToString() + "' class='nav-link'><i class='" + dtParent.Rows[i]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtParent.Rows[i]["MenuName"].ToString() + "</span></a></li>");
            }
        }
        strbuild.Append("</ul>");
        ltrMenu.Text = strbuild.ToString();

    }
    #endregion

    public void loaddashboard()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        dtEWalletTransaction = cls.select_data_dtNew("TotalMemberDashboard", _lstparm);
        if (dtEWalletTransaction.Rows.Count > 0)
        {
            foreach (DataRow dtrow in dtEWalletTransaction.Rows)
            {
                lblebalance.Text = dtrow["EBalance"].ToString();
                lblrbalance.Text = dtrow["RBalance"].ToString();
                lblmemberid.Text = dtrow["MemberId"].ToString();
                lblname.Text = dtrow["FirstName"].ToString();
                lblmembertype.Text = dtrow["MemberType"].ToString();
                imgcompany.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
                imgcompanymini.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
               // imgprofile.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtrow["userprofile"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/User/Profile/" + Convert.ToString(dtrow["userprofile"]);
                imgUser.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtrow["userprofile"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/User/Profile/" + Convert.ToString(dtrow["userprofile"]);
            }
        }
    }

    [WebMethod]
    public static List<Customer> Bindmemberdata()
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        dtEWalletTransaction = cls.select_data_dtNew("TotalMemberDashboard", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.EBalance = dtrow["EBalance"].ToString();
            cust.RBalance = dtrow["RBalance"].ToString();
            cust.MemberId = dtrow["MemberId"].ToString();
            cust.companylogomain = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
            cust.companylogomini = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
            cust.imgprofile = string.IsNullOrEmpty(Convert.ToString(dtrow["userprofile"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/User/Profile/" + Convert.ToString(dtrow["userprofile"]);
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
    }
    #endregion
}
