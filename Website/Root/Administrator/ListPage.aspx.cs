using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;

public partial class cms_ListPage : System.Web.UI.Page
{
    clsPage objPage = new clsPage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid(0);
        }
    }
    protected void dgPage_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objPage.ManagePage("IsDelete", idno);
                BindGrid(0);
            }
            catch
            {
                Page.RegisterStartupScript("Msg1", "<script>alert('Please Page related Data delete first,after that you can delete this !');</script>");
            }
        }
        if (e.CommandName == "IsActive")
        {
            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objPage.ManagePage("IsActive", idno);
                BindGrid(0);
            }
            catch
            {
               
            }
        }

    }
    protected void dgPage_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgPage.CurrentPageIndex = e.NewPageIndex;
        BindGrid(0);
    }
    protected void dgPage_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }
    private void BindGrid(int id)
    {
        DataTable dt = new DataTable();
        dt = objPage.ManagePage("GetAll", id);
        dgPage.DataSource = dt;
        dgPage.DataBind();
    }
    

    protected void dgPage_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        try
        {
            DataTable dt = objPage.ManagePage("GetAll", 0);
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
            dgPage.DataSource = dv;
            dgPage.DataBind();
        }
        catch (Exception ex)
        { }
    }
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;
            return (SortDirection)ViewState["sortDirection"];
        }
        set
        {
            ViewState["sortDirection"] = value;
        }
    }
    protected void dgPage_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            e.Item.TableSection = TableRowSection.TableHeader;
        }
    }
}