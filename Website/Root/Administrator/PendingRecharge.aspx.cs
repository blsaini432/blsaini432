using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Net;
using System.IO;

public partial class Root_Admin_PendingRequest : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    DataTable dtExport = new DataTable();

    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();

    clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();

    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();
    cls_myMember clsm = new cls_myMember();
    string condition = " SerialNo > 0";
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillHistory();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]

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

    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        fillHistory();
    }
    protected void gvHistory_Sorting(object sender, GridViewSortEventArgs e)
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
            gvHistory.DataSource = dv;
            gvHistory.DataBind();
        }
        catch (Exception ex)
        { }
    }

    #endregion

    #region [ddlPaging]

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillHistory();
    }

    #endregion

    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillHistory();
    }

    protected void btnCheckStatus_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow grow in gvHistory.Rows)
            {
                CheckBox chkRow = (CheckBox)grow.FindControl("chkRow");
                if (chkRow.Checked)
                {
                    clsm.Cyrus_RechargePendingProcess(Convert.ToInt32(gvHistory.DataKeys[grow.RowIndex].Value));
                }
            }
        }
        catch (Exception ex)
        {

        }
        fillHistory();
    }


    private void fillHistory()
    {
        dtHistory = objHistory.ManageHistory("GetPending", 0);
      //  dtHistory.DefaultView.RowFilter = condition;
        gvHistory.DataSource = dtHistory;
        gvHistory.DataBind();
        ViewState["dtExport"] = dtHistory;
        dtHistory.Columns.Remove("MsrNo");
        dtHistory.Columns.Remove("OperatorID");
        dtHistory.Columns.Remove("CircleID");
        dtHistory.Columns.Remove("Response");
        dtHistory.Columns.Remove("APIMessage");
        dtHistory.Columns.Remove("LastUpdate");
    }



    protected void gvHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Success")
        {
            clsm.Cyrus_Recharge_ForceSuccess(Convert.ToInt32(e.CommandArgument.ToString()));
        }
        else if (e.CommandName == "Fail")
        {
            clsm.Cyrus_Recharge_ForceFailed(Convert.ToInt32(e.CommandArgument.ToString()));
        }
        fillHistory();
    }
    protected void gvHistory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}