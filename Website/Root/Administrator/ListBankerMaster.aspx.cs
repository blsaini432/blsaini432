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

public partial class Root_Admin_ListBankerMaster : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_BankerMaster objBankerMaster = new clsMLM_BankerMaster();
    DataTable dtBankerMaster = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillBankerMaster();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]
    private void fillBankerMaster()
    {
        dtBankerMaster = objBankerMaster.ManageBankerMaster("GetAll", 0);
        gvBankerMaster.DataSource = dtBankerMaster;
        gvBankerMaster.DataBind();
        if (dtBankerMaster.Rows.Count > 0)
        {            
            //litrecordcount.Text = dtBankerMaster.Rows.Count.ToString();            
            ViewState["dtExport"] = dtBankerMaster;
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
    protected void gvBankerMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [IsDelete]

        if (e.CommandName == "IsDelete")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objBankerMaster.ManageBankerMaster("IsDelete", idno);
                fillBankerMaster();

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
                objBankerMaster.ManageBankerMaster("IsActive", idno);
                fillBankerMaster();

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion

        #region [IsAdmin]
        if (e.CommandName == "IsAdmin")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objBankerMaster.ManageBankerMaster("IsAdmin", idno);
                fillBankerMaster();

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion

      
    }
    protected void gvBankerMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvBankerMaster.PageIndex = e.NewPageIndex;
        fillBankerMaster();
    }
    protected void gvBankerMaster_Sorting(object sender, GridViewSortEventArgs e)
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
            gvBankerMaster.DataSource = dv;
            gvBankerMaster.DataBind();
        }
        catch (Exception ex)
        { }
    }
    
    #endregion


    protected void gvBankerMaster_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}