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
using System.Collections.Generic;

public partial class cms_ListManageService : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    protected void dgNews_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                cls_connection cls = new cls_connection();
                cls.select_data_dt("delete from tbl_Service where id="+ idno + "");
                BindGrid();
            }
            catch
            {
                Page.RegisterStartupScript("Msg1", "<script>alert('Please News related Data delete first,after that you can delete this !');</script>");
            }
        }
       

    }
    protected void dgNews_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgNews.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void dgNews_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }


    private void BindGrid()
    {
        List<ParmList> _lstparm = new List<ParmList>();
        cls_connection cls = new cls_connection();
        _lstparm.Add(new ParmList() { name = "@action", value = "AI" });
        DataTable dt = cls.select_data_dtNew("Sp_ServiceFeeSettings", _lstparm);
        if (dt.Rows.Count > 0)
        {
            dgNews.DataSource = dt;
            dgNews.DataBind();
        }
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
    protected void dgNews_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            e.Item.TableSection = TableRowSection.TableHeader;
        }
    }
}