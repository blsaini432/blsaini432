using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Common;

public partial class Root_Admin_FastTagActivationReport : System.Web.UI.Page
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
            fillEmployee();
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
    private void fillEmployee()
    {
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@action", value = "L" });
        _list.Add(new ParmList() { name = "@fromdate", value = Convert.ToDateTime(System.DateTime.Now.ToString("MM-dd-yyyy")) });
        _list.Add(new ParmList() { name = "@todate", value = Convert.ToDateTime(System.DateTime.Now.ToString("MM-dd-yyyy")) });
        dtEmployee = cls.select_data_dtNew("sp_FasttagActivation", _list);
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
    protected void gvBookedBusList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [Approve]
        if (e.CommandName == "Approve")
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument);
                DataTable Dm = cls.select_data_dt("select * from FasttagActivation where Id=" + id + " and Status='Pending'");
                if (Dm.Rows.Count > 0)
                {
                    cls.select_data_dt("update FasttagActivation set Status='Success' where Id=" + id + "");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Approve Successfully!');", true);
                    fillEmployee();
                }

            }
            catch (Exception ex)
            {   
                Function.MessageBox(ex.Message);
            }
        }
        #endregion
        #region [Reject]
        if (e.CommandName == "Reject")
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument);
                DataTable Dm = cls.select_data_dt("select * from FasttagActivation where Id=" + id + " and Status='Pending'");
                if (Dm.Rows.Count > 0)
                {
                    cls.select_data_dt("update FasttagActivation set Status='Failed' where Id=" + id + "");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Reject Successfullyt!');", true);
                    fillEmployee();
                }
            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion
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
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@action", value = "L" });
        _list.Add(new ParmList() { name = "@fromdate", value = changedatetommddyy(txtfromdate.Text) });
        _list.Add(new ParmList() { name = "@todate", value = changedatetommddyy(txttodate.Text) });
        dtEmployee = cls.select_data_dtNew("sp_FasttagActivation", _list);
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