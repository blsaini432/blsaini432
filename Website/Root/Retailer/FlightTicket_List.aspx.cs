using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FlightTicket_List : System.Web.UI.Page
{
    #region Properties
    cls_myMember clsm = new cls_myMember();
    private string Resultt = string.Empty;
    cls_connection Cls = new cls_connection();
    private EzulixAir eAir = new EzulixAir();
    private string Result = string.Empty;
    DataTable dtTicketList = new DataTable();
    private DataSet ds = new DataSet();
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
                if (Session["MsrNoLog"] != null)
                {
                    fillTicketTransaction(Convert.ToInt32(Session["MsrNoLog"].ToString()));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopupnew()", true);
                }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    #region Method
    private void fillTicketTransaction(int MsrNo)
    {
        try
        {
            string str = "Exec Ticket_List @msrno=" + MsrNo + "";
            if (txtfromdate.Text.Trim() != "" && txttodate.Text.Trim() != "")
                str = str + ",@datefrom='" + changedatetommddyy(txtfromdate.Text.Trim()) + "',@dateto='" + changedatetommddyy(txttodate.Text.Trim()) + "'";
            else
                str = str + ",@datefrom='01-01-2018',@dateto='" + System.DateTime.Now.ToString("MM-dd-yyyy") + "'";
            dtTicketList = Cls.select_data_dt(str);
            litrecordcount.Text = dtTicketList.Rows.Count.ToString();
            ViewState["dtExport"] = dtTicketList;
            gv_Transaction.DataSource = dtTicketList;
            gv_Transaction.DataBind();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    public class BookingDetails
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string PNR { get; set; }
        public string BookingId { get; set; }
        public string TraceId { get; set; }
        public string Source { get; set; }
    }

    protected void gvTicketTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string transactionid = Convert.ToString(e.CommandArgument);
            int id = Convert.ToInt32(transactionid);
            if (e.CommandName == "Receipt")
            {
                string str = "Exec Ticket_Detail @id=" + transactionid + "";
                dtTicketData = Cls.select_data_dt(str);
                string EndUserIp = dtTicketData.Rows[0]["EndUserIp"].ToString();
                string TokenIdd = dtTicketData.Rows[0]["Air_TokenId"].ToString();
                Session["ReceiptTokenId"] = TokenIdd;
                string PNR = dtTicketData.Rows[0]["PNR"].ToString();
                string BookingId = dtTicketData.Rows[0]["BookingId"].ToString();
                List<BookingDetails> BObj = new List<BookingDetails>
                {
                    new BookingDetails
                    {
                        EndUserIp=EndUserIp,
                        TokenId=TokenIdd,
                        PNR=PNR,
                        BookingId=BookingId
                    }
                };
                string Json = JsonConvert.SerializeObject(BObj);
                Resultt = eAir.GetBookingDetails(Json.Substring(1, (Json.Length) - 1));
                if (Resultt != null)
                {
                    ds = eAir.Deserialize(Resultt);
                    Session["ReceiptDetail"] = ds;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Result');location.replace('Flight_Receipt.aspx');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Empty Result');location.replace('FlightTicket_List.aspx');", true);
                }

            }
            else if (e.CommandName == "Cancel")
            {


                string str = "Exec Ticket_Detail @id=" + transactionid + "";
                dtTicketData = Cls.select_data_dt(str);

                string EndUserIp = "172.107.166.241";
                string TokenIdd = dtTicketData.Rows[0]["Air_TokenId"].ToString();
                string BookingId = dtTicketData.Rows[0]["BookingId"].ToString();
                string CancellationType = "2";
                string Origin = "";
                string RequestType = "1";
                string Destination = "";
                string TicketId = dtTicketData.Rows[0]["TicketId"].ToString();
                string Remarks = "Cancel Ticket";
                string agentid = dtTicketData.Rows[0]["Air_agentid"].ToString();
                string PublishFee = dtTicketData.Rows[0]["Air_PublishFee"].ToString();
                string NetPayable = dtTicketData.Rows[0]["Air_agencynetpay"].ToString();

                Result = eAir.TicketCancel(EndUserIp, TokenIdd, BookingId, CancellationType, Origin, RequestType, Destination, TicketId, Remarks, PublishFee, agentid, NetPayable);
                DataSet dss = eAir.Deserialize(Result);

                if (Result != string.Empty)
                {
                    if (dss.Tables.Contains("TicketCRInfo"))
                    {
                        DataTable dt = Cls.select_data_dt(@"Select FlightStatus from tbl_Flight_Api_Response where BookingId = '" + BookingId + "'");
                        if (dt.Rows[0]["FlightStatus"].ToString() != "Cancelled")
                        {
                            if (dss.Tables["TicketCRInfo"].Rows[0]["Status"].ToString() == "1" && dss.Tables["TicketCRInfo"].Rows[0]["Remarks"].ToString() == "Successful")
                            {
                                Cls.update_data("update tbl_Flight_Api_Response set FlightStatus='" + "Cancelled" + "' where BookingId='" + BookingId + "'");
                               
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Cancelled Successfully');location.replace('FlightTicket_List.aspx');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Ticket Already Cancelled');location.replace('FlightTicket_List.aspx');", true);
                         
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + dss.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightTicket_List.aspx');", true);
                    }
                }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightTicket_List.aspx');", true);
        }
    }
    public static void AjaxMessageBox(Control page, string msg)
    {
        try
        {
            string script = "alert('" + msg + "')";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "UserSecurity", script, true);
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        try
        {

            mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
            return mmddyy;
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
            return mmddyy;
        }
    }
    #endregion

    #region Events
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            dtMemberMaster = (DataTable)Session["MemberMaster"];
            fillTicketTransaction(Convert.ToInt32(Session["MsrNoLog"].ToString()));
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }

    protected DataTable RemoveColumn()
    {
        DataTable dt = new DataTable();
        try
        {
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
            return dt;
        }
    }
    #endregion
    protected void gv_Transaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gv_Transaction.PageIndex = e.NewPageIndex;
            fillTicketTransaction(Convert.ToInt32(Session["MsrNoLog"].ToString()));
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
}