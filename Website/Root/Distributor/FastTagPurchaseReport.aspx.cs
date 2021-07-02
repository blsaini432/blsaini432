using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Common;

public partial class Root_Distributor_FastTagPurchaseReport : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
            fillEmployee(MsrNo);
            GridViewSortDirection = SortDirection.Descending;
            
        }
    }

    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    #region [Function]
    private void fillEmployee(int MsrNo)
    {
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@action", value = "mem" });
        _list.Add(new ParmList() { name = "@RequestByMsrNo", value = Convert.ToInt32(MsrNo) });
        _list.Add(new ParmList() { name = "@fromdate", value = Convert.ToDateTime(System.DateTime.Now.ToString("MM-dd-yyyy")) });
        _list.Add(new ParmList() { name = "@todate", value = Convert.ToDateTime(System.DateTime.Now.ToString("MM-dd-yyyy")) });
        dtEmployee = cls.select_data_dtNew("sp_FasttagPurchase", _list);
        if (dtEmployee.Rows.Count > 0)
        {
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
        DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
        fillEmployee(MsrNo);
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
   
                Common.Export.ExportTopdf(dtExport, "Report");
            }
        }
        catch
        { }
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@action", value = "mem" });
        _list.Add(new ParmList() { name = "@RequestByMsrNo", value = Convert.ToInt32(MsrNo) });
        _list.Add(new ParmList() { name = "@fromdate", value = changedatetommddyy(txtfromdate.Text) });
        _list.Add(new ParmList() { name = "@todate", value = changedatetommddyy(txttodate.Text) });
        dtEmployee = cls.select_data_dtNew("sp_FasttagPurchase", _list);
        if (dtEmployee.Rows.Count > 0)
        {
            gvBookedBusList.DataSource = dtEmployee;
            gvBookedBusList.DataBind();
            if (dtEmployee.Rows.Count > 0)
            {
                litrecordcount.Text = gvBookedBusList.Rows.Count.ToString();
                ViewState["dtExport"] = dtEmployee.DefaultView.ToTable();
            }
        }
        else
        {
            gvBookedBusList.DataSource = null;
            gvBookedBusList.DataBind();
        }

    }
    protected void gvDispute_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }


  
  
}