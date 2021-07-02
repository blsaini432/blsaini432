using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ListPackage : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_Package objPackage = new clsMLM_Package();
    DataTable dtPackage = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillPackage();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]
    private void fillPackage()
    {
        dtPackage = objPackage.ManagePackage("GetByMsrNo", 1);
        //gvPackage.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        gvPackage.DataSource = dtPackage;
        gvPackage.DataBind();
        ViewState["dtExport"] = dtPackage;
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
    protected void gvPackage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [IsDelete]

        if (e.CommandName == "IsDelete")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objPackage.ManagePackage("IsDelete", idno);
                fillPackage();

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
                objPackage.ManagePackage("IsActive", idno);
                fillPackage();

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion

      
    }
    protected void gvPackage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvPackage.PageIndex = e.NewPageIndex;
        fillPackage();
    }
    protected void gvPackage_Sorting(object sender, GridViewSortEventArgs e)
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
            gvPackage.DataSource = dv;
            gvPackage.DataBind();
        }
        catch (Exception ex)
        { }
    }
    
    #endregion

    #region [ddlPaging]

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillPackage();
    }

    #endregion

    #region [Export To Excel/Word/Pdf]
    protected void btnexportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = (DataTable)ViewState["dtExport"];
            if (dtExport.Rows.Count > 0)
            {
                dtExport.Columns.Remove("IsDelete");
                dtExport.Columns.Remove("IsActive");
                Common.Export.ExportToExcel(dtExport, "Package_Report");
            }
        }
        catch
        { }

    }
    protected void btnexportWord_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        dtExport = (DataTable)ViewState["dtExport"];
        if (dtExport.Rows.Count > 0)
        {
            dtExport.Columns.Remove("IsDelete");
            dtExport.Columns.Remove("IsActive");
            Common.Export.ExportToWord(dtExport, "Package_Report");
        } }
        catch
        { }

    }
    protected void btnexportPdf_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        dtExport = (DataTable)ViewState["dtExport"];
        if (dtExport.Rows.Count > 0)
        {
            dtExport.Columns.Remove("IsDelete");
            dtExport.Columns.Remove("IsActive");
            Common.Export.ExportTopdf(dtExport, "Package_Report");
        }
        }
        catch
        { }
    }

    #endregion
    protected void gvPackage_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}