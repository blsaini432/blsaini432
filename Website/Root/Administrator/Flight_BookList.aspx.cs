using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Root_Administrator_Flight_BookList : System.Web.UI.Page
{
    #region Properties
    cls_myMember clsm = new cls_myMember();
    cls_connection Cls = new cls_connection();
    private EzulixAir eAir = new EzulixAir();
    private string Result = string.Empty;
    DataTable dtBookList = new DataTable();
    DataTable dtTicketData = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                fillBook();
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
        }
    }

    #region Method
    private void fillBook()
    {
        try { 
        string str = "Exec sp_Book_ListAdmin ";
        if (txtfromdate.Text.Trim() != "" && txttodate.Text.Trim() != "")
            str = str + "@datefrom='" + changedatetommddyy(txtfromdate.Text.Trim()) + "',@dateto='" + changedatetommddyy(txttodate.Text.Trim()) + "'";
        else
            str = str + "@datefrom='01-01-2018',@dateto='" + System.DateTime.Now.ToString("MM-dd-yyyy") + "'";
        dtBookList = Cls.select_data_dt(str);
        litrecordcount.Text = dtBookList.Rows.Count.ToString();
        ViewState["dtExport"] = dtBookList;
        gv_Transaction.DataSource = dtBookList;
        gv_Transaction.DataBind();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
        }
    }


   public static void AjaxMessageBox(Control page, string msg)
    {try { 
        string script = "alert('" + msg + "')";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "UserSecurity", script, true);
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
        }
    }
    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        try { 
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
            return mmddyy;
        }
    }
    #endregion

    #region Events
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try { 
        dtMemberMaster = (DataTable)Session["MemberMaster"];
        fillBook();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
        }
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
                Common.Export.ExportToExcel(dtExport, "AEPSTransaction_Report");
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
        }

    }
    protected void btnexportWord_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = (DataTable)ViewState["dtExport"];
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToWord(dtExport, "AEPSTransaction_Report");
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
        }

    }
    protected void btnexportPdf_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = (DataTable)ViewState["dtExport"];
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportTopdf(dtExport, "AEPSTransaction_Report");
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
        }
    }

    protected DataTable RemoveColumn()
    {
        DataTable dt = new DataTable();
        try { 
        dt = (DataTable)ViewState["dtExport"];
        if (dt.Rows.Count > 0)
        {
            dt.Columns.Add("S.No", typeof(int));
            for (int count = 0; count < dt.Rows.Count; count++)
            {
                dt.Rows[count]["S.No"] = count + 1;
            }
            dt.PrimaryKey = null;
            dt.Columns.Remove("EWalletTransactionID");
            dt.Columns.Remove("MsrNo");
            dt.Columns.Remove("AddDate");
            dt.Columns.Remove("LastUpdate");
            dt.Columns.Remove("IsDelete");
            dt.Columns.Remove("IsActive");
            dt.Columns.Remove("Narration");
            dt.Columns["S.No"].SetOrdinal(0);
        }
        return dt;
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
            return dt;
        }
    }
    #endregion
    public class BookingDetails
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string PNR { get; set; }
        public string BookingId { get; set; }
        public string TraceId { get; set; }
        public string Source { get; set; }
    }
    protected void gv_Transaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        { 
        gv_Transaction.PageIndex = e.NewPageIndex;
        fillBook();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('DashBoard.aspx');", true);
        }
    }
}