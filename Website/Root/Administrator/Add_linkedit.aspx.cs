using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
public partial class Root_Administrator_Add_linkedit : System.Web.UI.Page
{
    #region [Properties]

    DataTable dtExport = new DataTable();
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillVideo();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]
    private void fillVideo()
    {
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Action", value = "link" });
        _list.Add(new ParmList() { name = "@ID", value = 0 });
        string TxnID = clsm.Cyrus_GetTransactionID_New();
        DataTable dt = new DataTable();
        dt = cls.select_data_dtNew("Proc_ManageVedio", _list);
        gvImage.DataSource = dt;
        gvImage.DataBind();
        if (dt.Rows.Count > 0)
        {
            //   litrecordcount.Text = dtBanner.Rows.Count.ToString();            
            ViewState["dtExport"] = dt;
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
    protected void gvImage_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [IsDelete]

        if (e.CommandName == "IsDelete")
        {
            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@Id", value = idno });
                _list.Add(new ParmList() { name = "@Action", value = "Delete" });
                string TxnID = clsm.Cyrus_GetTransactionID_New();
                DataTable dt = new DataTable();
                dt = cls.select_data_dtNew("Proc_ManageVedio", _list);

                fillVideo();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('delete Successfull!');", true);
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
                idno = Convert.ToInt32(e.CommandArgument);
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@Id", value = 0 });
                _list.Add(new ParmList() { name = "@Action", value = "IsActive" });
                string TxnID = clsm.Cyrus_GetTransactionID_New();
                DataTable dt = new DataTable();
                dt = cls.select_data_dtNew("Proc_ManageImage", _list);
                fillVideo();

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion


    }
    protected void gvImage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvImage.PageIndex = e.NewPageIndex;
        fillVideo();
    }
    protected void gvImage_Sorting(object sender, GridViewSortEventArgs e)
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
            gvImage.DataSource = dv;
            gvImage.DataBind();
        }
        catch (Exception ex)
        { }
    }

    #endregion

    #region [ddlPaging]

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillVideo();
    }

    #endregion
    protected void gvImage_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}