using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;


public partial class Root_Admin_ListEWalletBalance : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dtmembermaster = new DataTable();
                dtmembermaster = (DataTable)Session["dtRetailer"];
                fillEWalletBalance(Convert.ToInt32(dtmembermaster.Rows[0]["MsrNo"]));
                GridViewSortDirection = SortDirection.Descending;
            }
        }
    }

    #endregion

    #region [Function]
    private void fillEWalletBalance(int msrno)
    {
        dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetByMsrNo", msrno);
        gvEWalletBalance.DataSource = dtEWalletBalance;
        gvEWalletBalance.DataBind();
        if (dtEWalletBalance.Rows.Count > 0)
        {
           
            ViewState["dtExport"] = dtEWalletBalance;
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
    protected void gvEWalletBalance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetail")
        {
            Session["IdNo"] = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("ListEWalletTransaction.aspx");
        }
    }
    protected void gvEWalletBalance_Sorting(object sender, GridViewSortEventArgs e)
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
            gvEWalletBalance.DataSource = dv;
            gvEWalletBalance.DataBind();
        }
        catch (Exception ex)
        { }
    }
    
    #endregion


    #region [Export To Excel/Word/Pdf]
    protected void btnexportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = RemoveColumn();
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "EWalletBalance_Report");
            }
        }
        catch
        { }

    }
    

    protected DataTable RemoveColumn()
    {
        DataTable dt = new DataTable();
        dt = (DataTable)ViewState["dtExport"];
        if (dt.Rows.Count > 0)
        {
            dt.Columns.Add("S.No", typeof(int));
             for (int count = 0; count < dt.Rows.Count; count++)
            {
                dt.Rows[count]["S.No"] = count + 1;
            }
            dt.PrimaryKey = null;
            dt.Columns.Remove("EwalletBalanceID");
            dt.Columns.Remove("MsrNo");
            dt.Columns.Remove("AddDate");
            dt.Columns.Remove("LastUpdate");
            dt.Columns.Remove("IsDelete");
            dt.Columns.Remove("IsActive");
            dt.Columns["S.No"].SetOrdinal(0);
           
        }
       
        return dt;
    }
    #endregion
    protected void gvEWalletBalance_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}