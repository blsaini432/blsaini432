using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ListBanner : System.Web.UI.Page
{
    #region [Properties]
    clsBanner objBanner = new clsBanner();
    DataTable dtBanner = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillBanner();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]
    private void fillBanner()
    {
        dtBanner = objBanner.ManageBanner("GetAll", 0);
        gvBanner.DataSource = dtBanner;
        gvBanner.DataBind();
        if (dtBanner.Rows.Count > 0)
        {
         //   litrecordcount.Text = dtBanner.Rows.Count.ToString();            
            ViewState["dtExport"] = dtBanner;
        }
    }

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
            {
                ViewState["sortDirection"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["sortDirection"];
        }
        set
        {
            ViewState["sortDirection"] = value;
        }
    }
    #endregion

    #region [GridViewEvents]
    protected void gvBanner_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [IsDelete]

        if (e.CommandName == "IsDelete")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objBanner.ManageBanner("IsDelete", idno);
                fillBanner();

            }
            catch
            {
                Function.MessageBox("Sorry.You cannot delete this Item.If you want to delete this fist delete child Items related this Item.!");
            }
        }

        #endregion

        #region [IsActive]
        if (e.CommandName == "IsActive")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objBanner.ManageBanner("IsActive", idno);
                fillBanner();

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion

      
    }
    protected void gvBanner_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvBanner.PageIndex = e.NewPageIndex;
        fillBanner();
    }
    protected void gvBanner_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dtExport"];
            DataView dv = new DataView(dt);
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                dv.Sort = e.SortExpression + " DESC";
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                dv.Sort = e.SortExpression + " ASC";
            }
            gvBanner.DataSource = dv;
            gvBanner.DataBind();
        }
        catch (Exception ex)
        { }
    }
    
    #endregion

    #region [ddlPaging]

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillBanner();
    }

    #endregion

    
    protected void gvBanner_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}