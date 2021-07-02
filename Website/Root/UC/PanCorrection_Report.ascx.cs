using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Common;
using Newtonsoft.Json;
public partial class Root_UC_PanCorrection_Report : System.Web.UI.UserControl
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    clsEmployee objEmployee = new clsEmployee();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    string condition = " MsrNo > 0";
    public static int msrno { get; set; }
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["msnoid"] = msrno;
            fillEmployee();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]
    private void fillEmployee()
    {
        int MsrNo = Convert.ToInt32(ViewState["msnoid"]);
        #region Condition
        if (!(string.IsNullOrEmpty(txtfromdate.Text) | string.IsNullOrEmpty(txttodate.Text)))
        {
            condition = condition + " and RequestDate >= '" + txtfromdate.Text + "' AND RequestDate <= '" + txttodate.Text + "'";
        }
        //if (txt_orderID.Text.Trim() != "")
        //{
        //    condition = condition + " and [txnID] like '%" + txt_orderID.Text.Trim() + "%'";
        //}

        if (ddl_status.SelectedValue.ToString()!="0")
        {
            condition = condition + " and [RequestStatus] ='" + ddl_status.SelectedValue.ToString() + "'";
        }

        #endregion

      
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Action", value = "member" });
        _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
        dtEmployee = cls.select_data_dtNew("pancard_report", _list);
        if (dtEmployee.Rows.Count > 0)
        {
            dtEmployee.DefaultView.RowFilter = condition;
            gvBookedBusList.DataSource = dtEmployee;
            gvBookedBusList.DataBind();
            if (dtEmployee.Rows.Count > 0)
            {
                litrecordcount.Text = gvBookedBusList.Rows.Count.ToString();
                ViewState["dtExport"] = dtEmployee.DefaultView.ToTable();
            }
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
    protected void gvBookedBusList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvBookedBusList_Sorting(object sender, GridViewSortEventArgs e)
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
            gvBookedBusList.DataSource = dv;
            gvBookedBusList.DataBind();
        }
        catch (Exception ex)
        { }
    }
    #endregion

    #region [ddlPaging]

    protected void gvBookedBusList_PageIndexChanging(object sender, EventArgs e)
    {
        fillEmployee();
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
               // dtExport.Columns.Remove("MsrNo");
              //  dtExport.Columns.Remove("IsActive");
              //  dtExport.Columns.Remove("BookingKid");
              //  dtExport.Columns.Remove("Compct");
                Common.Export.ExportToExcel(dtExport, "Report");
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
                dtExport.Columns.Remove("MsrNo");
                dtExport.Columns.Remove("IsActive");
                dtExport.Columns.Remove("BookingKid");
                dtExport.Columns.Remove("Compct");
                Common.Export.ExportToWord(dtExport, "Report");
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
                dtExport.Columns.Remove("MsrNo");
                dtExport.Columns.Remove("IsActive");
                dtExport.Columns.Remove("BookingKid");
                dtExport.Columns.Remove("Compct");
                Common.Export.ExportTopdf(dtExport, "Report");
            }
        }
        catch
        { }
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillEmployee();
    }

    protected void gvDispute_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }

  
}