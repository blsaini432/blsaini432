using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Common;
using Newtonsoft.Json;
public partial class Root_UC_Pansionyojanareport : System.Web.UI.UserControl
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
            //DataTable dt = new DataTable();
            //dt = cls.select_data_dt("Exec totRecords '" + msrno + "'");
            //#region [Pan Status]
            //lblTotPan.Text = dt.Rows[0]["TotPanReq"].ToString();
            //lblSuccPan.Text = dt.Rows[0]["SuccPanReq"].ToString();
            //lblFailPan.Text = dt.Rows[0]["failedPanReq"].ToString();
            //lblTempPan.Text = dt.Rows[0]["TempPanReq"].ToString();
            //lblPendPan.Text = dt.Rows[0]["PendingPanReq"].ToString();
            //#endregion
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
            condition = condition + " and Adddate >= '" + txtfromdate.Text + "' AND Adddate <= '" + txttodate.Text + "'";
        }
        //if (txt_orderID.Text.Trim() != "")
        //{
        //    condition = condition + " and [Acknowledgement_No] like '%" + txt_orderID.Text.Trim() + "%'";
        //}

        //if (ddl_status.SelectedValue.ToString() != "0")
        //{
        //    condition = condition + " and [RequestStatus] ='" + ddl_status.SelectedValue.ToString() + "'";
        //}

        #endregion


        List<ParmList> _list = new List<ParmList>();
        if (Session["dtDistributor"] != null)
            _list.Add(new ParmList() { name = "@Action", value = "L" });
        else
            _list.Add(new ParmList() { name = "@Action", value = "DTL" });
        _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
        dtEmployee = cls.select_data_dtNew("Proc_tblpansionyojana", _list);
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
                //dtExport.Columns.Remove("MemberID");
                //dtExport.Columns.Remove("ReciptImg");
                //if (Session["dtRetailer"] != null)
                //{
                //    dtExport.Columns.Remove("Correctionform");
                //    dtExport.Columns.Remove("PanNo");
                //    dtExport.Columns.Remove("PANType");
                //}
                Common.Export.ExportToExcel(dtExport, "PancardReport");
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
                //dtExport.Columns.Remove("MemberID");
                //dtExport.Columns.Remove("ReciptImg");
                //if (Session["dtRetailer"] != null)
                //{
                //    dtExport.Columns.Remove("Correctionform");
                //    dtExport.Columns.Remove("PanNo");
                //    dtExport.Columns.Remove("PANType");
                //}
                Common.Export.ExportToWord(dtExport, "PancardReport");
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
                //dtExport.Columns.Remove("MemberID");
                //dtExport.Columns.Remove("ReciptImg");
                //if (Session["dtRetailer"] != null)
                //{
                //    dtExport.Columns.Remove("Correctionform");
                //    dtExport.Columns.Remove("PanNo");
                //    dtExport.Columns.Remove("PANType");
                //}
                Common.Export.ExportTopdf(dtExport, "PancardReport");
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