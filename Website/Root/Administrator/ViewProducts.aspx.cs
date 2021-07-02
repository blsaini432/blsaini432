using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using Common;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;


public partial class Root_Administrator_ViewProducts : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    cls_myMember clsm = new cls_myMember();
    string condition = "";
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillEmployee();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]
    private void fillEmployee()
    {
        #region Condition
        if (!(string.IsNullOrEmpty(txtfromdate.Text) | string.IsNullOrEmpty(txttodate.Text)))
        {
            condition = "AddDate >= '" + txtfromdate.Text + "' AND AddDate <= '" + txttodate.Text + "'";
        }

        #endregion
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Action", value = "E" });
        dtEmployee = cls.select_data_dtNew("Proc_Shoopingcart_details_GetSet", _list);
        if (dtEmployee.Rows.Count > 0)
        {
            dtEmployee.DefaultView.RowFilter = condition;
            gvBookedBusList.DataSource = dtEmployee;
            gvBookedBusList.DataBind();
            if (dtEmployee.Rows.Count > 0)
            {

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
        if (e.CommandName == "deletesuser")
        {
            string idno = "0";
            idno = Convert.ToString(e.CommandArgument);
            cls.delete_data("delete from [Shooping_Cart_Admin] where itemid='" + idno + "'");
            fillEmployee();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Deleted successfully !!');", true);
        }

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