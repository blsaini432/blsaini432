using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
public partial class Root_Admin_AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( Session["EmployeeID"]!=null || Session["dtEmployee"]!=null)
        {
            int id = Convert.ToInt32(Session["EmployeeID"]);
            DynamicMenu(id);
            Bindadmindata();
        }
        else
        {
            Response.Redirect("~/adminlogin.aspx");
        }
    }
    protected void linkbtn_logout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("~/adminlogin.aspx");
    }

    public void Bindadmindata()
    {
        DataTable dtEWalletTransaction = new DataTable();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        dtEWalletTransaction = cls.select_data_dtNew("TotalAdminDashboard", _lstparm);
        if(dtEWalletTransaction.Rows.Count >0)
        {
            foreach (DataRow dtrow in dtEWalletTransaction.Rows)
            {

                imgcompany.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
                imgcompanymini.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtrow["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["companylogo"]);
                imgprofile.ImageUrl = "../../Uploads/User/Profile/dummy.png";

            }
        }

    }
  
    #region BindMenu
    StringBuilder strBud = new StringBuilder();
    //private void DynamicMenu()
    //{
    //    StringBuilder strbuild = new StringBuilder();
    //    DataTable dtParent = new DataTable();
    //    cls_connection cls = new cls_connection();
    //    dtParent = cls.select_data_dt(@"select * from tblmlm_menuadmin where ParentID=0 and IsActive=1 and isdelete=0 order by position");
    //    int a = dtParent.Rows.Count;
    //    strbuild.Append("<ul id='main-menu-navigation' class='nav'>");
    //    for (int i = 0; i < a; i++)
    //    {
    //        DataTable dtChild = new DataTable();
    //        dtChild = cls.select_data_dt(@"select * from tblmlm_menuadmin where ParentID='" + dtParent.Rows[i]["MenuID"].ToString() + "' and IsActive=1 and isdelete=0");
    //        int b = dtChild.Rows.Count;
    //        if (b > 0)
    //        {
    //            strbuild.Append("<li class='nav-item'><a href='#" + dtParent.Rows[i]["MenuId"].ToString() + "' class='nav-link'  data-toggle='collapse'  aria-expanded='false'><i class='" + dtParent.Rows[i]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtParent.Rows[i]["MenuName"].ToString() + "</span><i class='menu-arrow'></i></a>");
    //            strbuild.Append("<div class='collapse' style='' id='" + dtParent.Rows[i]["MenuId"].ToString() + "'>");
    //            strbuild.Append("<ul class='nav flex-column sub-menu'>");
    //            for (int j = 0; j < b; j++)
    //            {
    //                DataTable dtSubchild = new DataTable();
    //                dtSubchild = cls.select_data_dt(@"select * from tblmlm_menuadmin where ParentID='" + dtChild.Rows[j]["MenuID"].ToString() + "' and IsActive=1 and isdelete=0");
    //                int c = dtSubchild.Rows.Count;
    //                if (c > 0)
    //                {
    //                    strbuild.Append("<li class='nav-item'><a href='#" + dtChild.Rows[j]["MenuId"].ToString() + "' class='nav-link'> <i class='" + dtChild.Rows[j]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtChild.Rows[j]["MenuName"].ToString() + "</span><i class='menu-arrow'></i></a>");
    //                    strbuild.Append("<div class='collapse' style='' id='" + dtChild.Rows[j]["MenuId"].ToString() + "'>");
    //                    strbuild.Append("<ul class='nav flex-column sub-menu'>");
    //                    for (int k = 0; k < c; k++)
    //                    {
    //                        strbuild.Append("<li><a href=" + dtSubchild.Rows[k]["MenuLink"].ToString() + " class='nav-link'  data-toggle='collapse'  aria-expanded='false'>" + dtSubchild.Rows[k]["MenuName"].ToString() + "</a></li>");
    //                    }
    //                    strbuild.Append("</ul>");
    //                    strbuild.Append("</div>");
    //                    strbuild.Append("</li>");
    //                }
    //                else
    //                {
    //                    strbuild.Append("<li class='nav-item'><a href='" + dtChild.Rows[j]["MenuLink"].ToString() + "' class='nav-link'><i class='" + dtChild.Rows[j]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtChild.Rows[j]["MenuName"].ToString() + "</a></li>");
    //                }
    //            }
    //            strbuild.Append("</ul>");
    //            strbuild.Append("</div>");
    //            strbuild.Append("</li>");
    //        }
    //        else
    //        {
    //            strbuild.Append("<li class='nav-item'><a href='" + dtParent.Rows[i]["MenuLink"].ToString() + "' class='nav-link'><i class='" + dtParent.Rows[i]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtParent.Rows[i]["MenuName"].ToString() + "</span></a></li>");
    //        }
    //    }
    //    strbuild.Append("</ul>");
    //    ltrMenu.Text = strbuild.ToString();

    //}


    private void DynamicMenu(int menuid)
    {
        StringBuilder strbuild = new StringBuilder();
        DataTable dtParents = new DataTable();
        cls_connection cls = new cls_connection();
        DataTable dtmenu = new DataTable();
        dtmenu = cls.select_data_dt("exec ProcMLM_ManageMenuAdmin1 'GetByEmployee'," + menuid);
        var SelectedValues = dtmenu.AsEnumerable().Select(s => s.Field<Int32>("MenuID")).ToArray();
        string commaSeperatedValues = string.Join(",", SelectedValues);
        DataRow[] dtParent;
        dtParent = dtmenu.Select("ParentID=0 and IsActive=1 and isdelete=0");
        int a = dtParent.Length;
        strbuild.Append("<ul id='main-menu-navigation' class='nav'>");
        for (int i = 0; i < a; i++)
        {
            DataTable dtChild = new DataTable();
            dtChild = cls.select_data_dt(@"select * from tblmlm_menuadmin where ParentID='" + dtParent[i]["MenuID"].ToString() + "' and menuid in(" + commaSeperatedValues + ") and IsActive=1 and isdelete=0");
            int b = dtChild.Rows.Count;
            if (b > 0)
            {
                strbuild.Append("<li class='nav-item'><a href='#" + dtParent[i]["MenuId"].ToString() + "' class='nav-link'  data-toggle='collapse'  aria-expanded='false'><i class='" + dtParent[i]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtParent[i]["MenuName"].ToString() + "</span><i class='menu-arrow'></i></a>");
                strbuild.Append("<div class='collapse' style='' id='" + dtParent[i]["MenuId"].ToString() + "'>");
                strbuild.Append("<ul class='nav flex-column sub-menu'>");
                for (int j = 0; j < b; j++)
                {
                    DataTable dtSubchild = new DataTable();
                    dtSubchild = cls.select_data_dt(@"select * from tblmlm_menuadmin where ParentID='" + dtChild.Rows[j]["MenuID"].ToString() + "' and IsActive=1 and isdelete=0");
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
                strbuild.Append("<li class='nav-item'><a href='" + dtParent[i]["MenuLink"].ToString() + "' class='nav-link'><i class='" + dtParent[i]["cssclass"].ToString() + "'></i><span class='menu-title'>" + dtParent[i]["MenuName"].ToString() + "</span></a></li>");
            }
        }
        strbuild.Append("</ul>");
        ltrMenu.Text = strbuild.ToString();

    }
    #endregion
}
