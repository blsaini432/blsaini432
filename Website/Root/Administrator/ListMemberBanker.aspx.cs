using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ListMemberBanker : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_MemberBanker objMemberBanker = new clsMLM_MemberBanker();
    DataTable dtMemberBanker = new DataTable();
    DataTable dtExport = new DataTable();
    string condition = " MsrNo > 0";
    cls_connection cls = new cls_connection();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillMemberBanker();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]
    private void fillMemberBanker()
    { 

        dtMemberBanker = objMemberBanker.ManageMemberBanker("GetAll", 0);
        gvMemberBanker.DataSource = dtMemberBanker;
        gvMemberBanker.DataBind();
        if (dtMemberBanker.Rows.Count > 0)
        {
            ViewState["dtExport"] = dtMemberBanker;
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
    protected void gvMemberBanker_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [IsDelete]

        if (e.CommandName == "IsDelete")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objMemberBanker.ManageMemberBanker("IsDelete", idno);
                fillMemberBanker();

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
                objMemberBanker.ManageMemberBanker("IsActive", idno);
                fillMemberBanker();

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion

        #region [IsApprove]
        if (e.CommandName == "IsApprove")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objMemberBanker.ManageMemberBanker("IsApprove", idno);
                fillMemberBanker();

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion

      
    }
    protected void gvMemberBanker_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvMemberBanker.PageIndex = e.NewPageIndex;
        fillMemberBanker();
    }
    protected void gvMemberBanker_Sorting(object sender, GridViewSortEventArgs e)
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
            gvMemberBanker.DataSource = dv;
            gvMemberBanker.DataBind();
        }
        catch (Exception ex)
        { }
    }
    
    #endregion

    #region [ddlPaging]

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMemberBanker();
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
                Common.Export.ExportToExcel(dtExport, "MemberBanker_Report");
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
                Common.Export.ExportToWord(dtExport, "MemberBanker_Report");
            }
        }
        catch { }

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
                Common.Export.ExportTopdf(dtExport, "MemberBanker_Report");
            }
        }
        catch { }
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillMemberBanker();
    }
    protected void gvMemberBanker_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}