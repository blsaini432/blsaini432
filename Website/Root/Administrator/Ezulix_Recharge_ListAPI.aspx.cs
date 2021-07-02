using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Root_Admin_ListAPI : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            fillAPI();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]


    private void fillAPI()
    {
        dtAPI = objAPI.ManageAPI("GetAll", 0);
        gvAPI.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        gvAPI.DataSource = dtAPI;
        gvAPI.DataBind();
        litrecordcount.Text = dtAPI.Rows.Count.ToString();
        ViewState["dtExport"] = dtAPI;
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
    protected void gvAPI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [IsDelete]

        if (e.CommandName == "IsDelete")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                objAPI.ManageAPI("IsDelete", idno);
                fillAPI();

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
                objAPI.ManageAPI("IsActive", idno);
                fillAPI();
            }
            catch (Exception ex)
            {
                Function.MessageBox(ex.Message);
            }
        }
        #endregion

        #region [View]
        if (e.CommandName == "View")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                //objAPI.ManageAPI("IsActive", idno);
                //fillAPI();
                dtAPI = objAPI.ManageAPI("Get", idno);
                string str = "";
                str = dtAPI.Rows[0]["URL"].ToString() + dtAPI.Rows[0]["prm1"].ToString() + "=" + dtAPI.Rows[0]["prm1val"].ToString() + "&";
                if (dtAPI.Rows[0]["prm2"].ToString() != "" && dtAPI.Rows[0]["prm2val"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm2"].ToString() + "=" + dtAPI.Rows[0]["prm2val"].ToString() + "&";
                }
                if (dtAPI.Rows[0]["prm3"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm3"].ToString() + "=XXXXXXXXXX&";
                }
                if (dtAPI.Rows[0]["prm4"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm4"].ToString() + "=XX&";
                }
                if (dtAPI.Rows[0]["prm5"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm5"].ToString() + "=XX&";
                }
                if (dtAPI.Rows[0]["prm6"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm6"].ToString() + "=XX&";
                }
                if (dtAPI.Rows[0]["prm7"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm7"].ToString() + "=XX&";
                }
                if (dtAPI.Rows[0]["prm8"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm8"].ToString() + "=XX&";
                }
                if (dtAPI.Rows[0]["prm9"].ToString() != "" && dtAPI.Rows[0]["prm9val"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm9"].ToString() + "=" + dtAPI.Rows[0]["prm9val"].ToString() + "&";
                }
                if (dtAPI.Rows[0]["prm10"].ToString() != "" && dtAPI.Rows[0]["prm10val"].ToString() != "")
                {
                    str = str + dtAPI.Rows[0]["prm10"].ToString() + "=" + dtAPI.Rows[0]["prm10val"].ToString() + "&";
                }
                if (str.EndsWith("&"))
                    str = str.Substring(0, str.Length - 1);
                litAPI.Text = str;
                litAPIName.Text = dtAPI.Rows[0]["APIName"].ToString();

                
                if (dtAPI.Rows[0]["BalanceURL"].ToString() != "")
                {
                    string strBalanceAPI = "";
                    if (dtAPI.Rows[0]["B_prm1"].ToString() != "" && dtAPI.Rows[0]["B_prm1val"].ToString() != "")
                    {
                        strBalanceAPI = dtAPI.Rows[0]["BalanceURL"].ToString() + dtAPI.Rows[0]["B_prm1"].ToString() + "=" + dtAPI.Rows[0]["B_prm1val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm2"].ToString() != "" && dtAPI.Rows[0]["B_prm2val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm2"].ToString() + "=" + dtAPI.Rows[0]["B_prm2val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm3"].ToString() != "" && dtAPI.Rows[0]["B_prm3val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm3"].ToString() + "=" + dtAPI.Rows[0]["B_prm3val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm4"].ToString() != "" && dtAPI.Rows[0]["B_prm4val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm4"].ToString() + "=" + dtAPI.Rows[0]["B_prm4val"].ToString();
                    }
                    if (strBalanceAPI.EndsWith("&"))
                        strBalanceAPI = strBalanceAPI.Substring(0, strBalanceAPI.Length - 1);
                    litBalanceAPI.Text = strBalanceAPI;
                }


                if (dtAPI.Rows[0]["StatusURL"].ToString() != "")
                {
                    string strBalanceAPI = "";
                    if (dtAPI.Rows[0]["S_prm1"].ToString() != "" && dtAPI.Rows[0]["S_prm1val"].ToString() != "")
                    {
                        strBalanceAPI = dtAPI.Rows[0]["StatusURL"].ToString() + dtAPI.Rows[0]["S_prm1"].ToString() + "=" + dtAPI.Rows[0]["S_prm1val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["S_prm2"].ToString() != "" && dtAPI.Rows[0]["S_prm2val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["S_prm2"].ToString() + "=" + dtAPI.Rows[0]["S_prm2val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["S_prm3"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["S_prm3"].ToString() + "=XXXXX&";
                    }
                    if (dtAPI.Rows[0]["S_prm4"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["S_prm4"].ToString() + "=XXXXX&";
                    }
                    if (strBalanceAPI.EndsWith("&"))
                        strBalanceAPI = strBalanceAPI.Substring(0, strBalanceAPI.Length - 1);
                    litStatusAPI.Text = strBalanceAPI;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);
            }
            catch (Exception ex)
            {
                Function.MessageBox(ex.Message);
            }
        }
        #endregion
    }
    protected void gvAPI_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvAPI.PageIndex = e.NewPageIndex;
        fillAPI();
    }
    protected void gvAPI_Sorting(object sender, GridViewSortEventArgs e)
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
            gvAPI.DataSource = dv;
            gvAPI.DataBind();
        }
        catch (Exception ex)
        { }
    }
    
    #endregion

    #region [ddlPaging]

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillAPI();
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
                dtExport.Columns.Remove("IsDelete");
                dtExport.Columns.Remove("IsActive");
                Common.Export.ExportToExcel(dtExport, "API_Report");
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
                dtExport.Columns.Remove("IsDelete");
                dtExport.Columns.Remove("IsActive");
                Common.Export.ExportToWord(dtExport, "API_Report");
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
                dtExport.Columns.Remove("IsDelete");
                dtExport.Columns.Remove("IsActive");
                Common.Export.ExportTopdf(dtExport, "API_Report");
            }
        }
        catch
        { }
    }

    #endregion


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillAPI();
    }
}