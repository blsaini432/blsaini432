using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
//using Ionic.Zip;

public partial class Root_Admin_Listaeps_reg : System.Web.UI.Page
{
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    string condition = " ewallettransactionid > 0";
    private static string mm_token = "1567CC1ACE";
    private static string mm_api_memberid = "EZ198646";
    public string M_Uri = "http://api.ezulix.in/";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                //dtMemberMaster = (DataTable)Session["MemberMaster"];
                FillAEPS_Reg_List(1);
            }

        }
    }

    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    private void FillAEPS_Reg_List(int MsrNo)
    {
        string str = "Exec Set_AEPS_Reg_List 0";
        if (txtfromdate.Text.Trim() != "" && txttodate.Text.Trim() != "")
            str = str + ",'" + changedatetommddyy(txtfromdate.Text.Trim()) + "','" + changedatetommddyy(txttodate.Text.Trim()) + "'";
        else
            str = str + ",'" + System.DateTime.Now.ToString("MM-dd-yyyy") + "','" + System.DateTime.Now.ToString("MM-dd-yyyy") + "'";
        dtEWalletTransaction = cls.select_data_dt(str);
        litrecordcount.Text = dtEWalletTransaction.Rows.Count.ToString();
        ViewState["dtExport"] = dtEWalletTransaction;
        gvEWalletTransaction.DataSource = dtEWalletTransaction;
        gvEWalletTransaction.DataBind();
    }
    #region [GridViewEvents]
    protected void gvEWalletTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "approve")
        {
            int MsrNo = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            DataTable Dt = cls.select_data_dt(@"SELECT * FROM Tbl_Aeps_Reg WHERE MsrNo=" + MsrNo + "");
            JavaScriptSerializer Js = new JavaScriptSerializer();
            RequestKyc Obj = new RequestKyc()
            {
                FirstName = Dt.Rows[0]["F_Name"].ToString(),
                LastName = Dt.Rows[0]["L_Name"].ToString(),
                ShopName = Dt.Rows[0]["Shop_Name"].ToString(),
                PanNumber = Dt.Rows[0]["Pan_Number"].ToString(),
                MobileNumber = Dt.Rows[0]["Contact_Number"].ToString(),
                ParmamentState = Dt.Rows[0]["P_State"].ToString(),
                ParmamentCity = Dt.Rows[0]["P_City"].ToString(),
                ParmamentAddress = Dt.Rows[0]["P_Address"].ToString(),
                ParmamentPin = Dt.Rows[0]["P_Pin"].ToString(),
                AddProofUrl = Dt.Rows[0]["Addr_Proof_Filename"].ToString(),
                AddProofNumber = Dt.Rows[0]["Addr_Proof_Num"].ToString(),
                SelfDeclNumber = Dt.Rows[0]["Self_Decl_Num"].ToString(),
                SelfDeclUrl = Dt.Rows[0]["Self_Decl_Filename"].ToString(),
                MsrNo = Convert.ToInt32(Dt.Rows[0]["MsrNo"]),
                MemberId = Dt.Rows[0]["MemberID"].ToString(),
                AdminMemberId = mm_api_memberid,
            };
            string Result = HTTP_POST(M_Uri + "aeps/kyc?MemberId=" + mm_api_memberid + "&ApiKey=" + mm_token + "", Js.Serialize(Obj));
            if (Result != string.Empty)
            {
                if (Result == "Error")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Result + "');", true);
                }
                else
                {
                    cls.select_data_dt(@"Update Tbl_Aeps_Reg SET Statu='Approved' WHERE MemberID='" + Dt.Rows[0]["MemberID"].ToString() + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Kyc submit successfully.');", true);
                    FillAEPS_Reg_List(1);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('!Error');", true);
            }
        }
        else if (e.CommandName == "reject")
        {
            int MsrNo = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            TextBox txt = row.FindControl("txt_Rejection") as TextBox;
            if (txt.Text != string.Empty)
            {
                cls.select_data_dt(@"Update Tbl_Aeps_Reg SET Statu='Rejected',rejection='" + txt.Text.Trim() + "' WHERE MsrNo='" + MsrNo + "'");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Enter rejection reason.');", true);
            }
        }
        else if (e.CommandName == "force")
        {
            string Memberid = (e.CommandArgument).ToString(); ;
            JavaScriptSerializer Js = new JavaScriptSerializer();
            MemberDetail Obj = new MemberDetail()
            {
                MemberId = Memberid
            };
            string Result = HTTP_POST(M_Uri + "aeps/forsestart?MemberId=" + mm_api_memberid + "&ApiKey=" + mm_token + "", Js.Serialize(Obj));
            if (Result != string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Result + "');", true);
            }
        }
    }
    protected void gvEWalletTransaction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEWalletTransaction.PageIndex = e.NewPageIndex;
        FillAEPS_Reg_List(1);
    }
    protected void gvEWalletTransaction_Sorting(object sender, GridViewSortEventArgs e)
    {
        //try
        //{
        //    DataTable dt = (DataTable)ViewState["dtExport"];
        //    DataView dv = new DataView(dt);
        //    if (GridViewSortDirection == SortDirection.Ascending)
        //    {
        //        GridViewSortDirection = SortDirection.Descending;
        //        dv.Sort = e.SortExpression + " DESC";
        //    }
        //    else
        //    {
        //        GridViewSortDirection = SortDirection.Ascending;
        //        dv.Sort = e.SortExpression + " ASC";
        //    }
        //    gvEWalletTransaction.DataSource = dv;
        //    gvEWalletTransaction.DataBind();
        //}
        //catch (Exception ex)
        //{ }
    }

    #endregion
    #region [Export To Excel/Word/Pdf]
    protected void btnexportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = (DataTable)ViewState["dtExport"];
            //dtExport = RemoveColumn();
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "EWalletTransaction_Report");
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
                Common.Export.ExportToWord(dtExport, "EWalletTransaction_Report");
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
                Common.Export.ExportTopdf(dtExport, "EWalletTransaction_Report");
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        dtMemberMaster = (DataTable)Session["MemberMaster"];
        FillAEPS_Reg_List(1);
    }
    protected void gvEWalletTransaction_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }

    #region PropertiesClass
    public class RequestKyc
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ShopName { get; set; }
        public string PanNumber { get; set; }
        public string MobileNumber { get; set; }
        public string ParmamentState { get; set; }
        public string ParmamentCity { get; set; }
        public string ParmamentAddress { get; set; }
        public string ParmamentPin { get; set; }
        public string AddProofUrl { get; set; }
        public string AddProofNumber { get; set; }
        public string SelfDeclNumber { get; set; }
        public string SelfDeclUrl { get; set; }
        public int MsrNo { get; set; }
        public string MemberId { get; set; }
        public string AdminMemberId { get; set; }
    }

    public class MemberDetail
    {
        public string MemberId { get; set; }
    }
    #endregion

    #region HTTP_POST
    public static string HTTP_POST(string Url, string Data)
    {
        string Out = String.Empty;
        try
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 100000;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
        }
        catch (WebException e)
        {
            Out = "Error";
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    #endregion


    #region Method
    private DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
    #endregion
}