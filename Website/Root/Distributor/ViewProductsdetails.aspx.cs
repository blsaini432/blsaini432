using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Root_Distributor_ViewProductsdetails : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    DataTable dtEWalletTransaction = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                DataTable dtMemberMaster = new DataTable();
                dtMemberMaster = (DataTable)Session["dtDistributor"];
                string Memberid = dtMemberMaster.Rows[0]["MemberId"].ToString();
                int Msrno = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                ViewState["Msrno"] = Msrno;
                fillEWalletTransaction(Msrno);
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }

        }
    }

    #region Method
    private void fillEWalletTransaction(int MsrNo)
    {
        string str = "select * from Shooping_Cart_MemberDetails where RequestBy in (select MemberId from tblmlm_membermaster where msrno=" + MsrNo + ")";
        if (txtfromdate.Text.Trim() != "" && txttodate.Text.Trim() != "")
            str = str + "and AddDate Between '" + changedatetommddyy(txtfromdate.Text.Trim()) + "'AND'" + changedatetommddyy(txttodate.Text.Trim()) + "'";

        dtEWalletTransaction = Cls.select_data_dt(str);
        litrecordcount.Text = dtEWalletTransaction.Rows.Count.ToString();
        ViewState["dtExport"] = dtEWalletTransaction;
        gv_Transaction.DataSource = dtEWalletTransaction;
        gv_Transaction.DataBind();

    }

    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    #endregion

    #region Events
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        dtMemberMaster = (DataTable)Session["MemberMaster"];
        fillEWalletTransaction(Convert.ToInt32(ViewState["Msrno"].ToString()));
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
        catch { }

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
                Common.Export.ExportTopdf(dtExport, "AEPSTransaction_Report");
            }
        }
        catch
        { }
    }

    protected DataTable RemoveColumn()
    {
        DataTable dt = new DataTable();
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
    #endregion
    protected void gv_Transaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_Transaction.PageIndex = e.NewPageIndex;
        fillEWalletTransaction(Convert.ToInt32(Session["DistributorMsrNo"].ToString()));
    }
}