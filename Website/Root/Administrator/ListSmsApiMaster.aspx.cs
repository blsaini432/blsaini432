using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
public partial class Root_Admin_ListSmsApiMaster : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    DataTable dtAPI = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillAPI();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]


    private void fillAPI()
    {
        dtAPI = cls.select_data_dt("Proc_Recharge_SMSApi 'getData'");
        gvAPI.DataSource = dtAPI;
        gvAPI.DataBind();
        ViewState["dtExport"] = dtAPI;
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
    protected void gvAPI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [IsDelete]

        if (e.CommandName == "IsDelete")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                cls.update_data("Proc_Recharge_SMSApi 'IsDelete'," + idno + "");
                fillAPI();

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
                cls.update_data("Proc_Recharge_SMSApi 'IsActive',"+idno+"");
                fillAPI();
            }
            catch (Exception ex)
            {
                Function.MessageBox(ex.Message);
            }
        }
        #endregion

        if (e.CommandName == "activate")
        {
            cls.update_data("Update tblmlm_membermaster set SMSapi='" + e.CommandArgument.ToString() + "'");
            fillAPI();
        }
      
        
    }
    #endregion
    protected void gvAPI_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvAPI.PageIndex = e.NewPageIndex;
        fillAPI();
    }
    protected void gvAPI_Sorting(object sender, GridViewSortEventArgs e)
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
            gvAPI.DataSource = dv;
            gvAPI.DataBind();
        }
        catch (Exception ex)
        { }
    }

   
    #region [ddlPaging]

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillAPI();
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
                Common.Export.ExportToExcel(dtExport, "API_Report");
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
                Common.Export.ExportToWord(dtExport, "API_Report");
            }
        }
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
                Common.Export.ExportTopdf(dtExport, "API_Report");
            }
        }
        catch
        { }
    }

    #endregion


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillAPI();
    }
    protected void gvAPI_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void gvAPI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string apiidno = cls.select_data_scalar_string("Select SMSAPI from tblmlm_membermaster where msrno=1");
            HiddenField litAPIID = (HiddenField)e.Row.FindControl("hdnAPIid");


            Button btn = (Button)e.Row.FindControl("btnSwitch");
            if (litAPIID.Value == apiidno)
            {
                btn.Visible = false;
            }
            else
            {
                btn.Visible = true;
            }
        }
    }
}