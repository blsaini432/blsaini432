using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Common;
using BLL;
using System.Web.Services;
using System.Web.Script.Services;
public partial class Root_Administrator_GSTRegistrationReport : System.Web.UI.Page
{

    #region [Properties]
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    string condition = " RequestBymsrno > 0";
    #endregion

    //#region [PageLoad]
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        fillEmployee();
    //        GridViewSortDirection = SortDirection.Descending;
    //    }
    //}

    //#endregion

    //#region [Function]
    //private void fillEmployee()
    //{
    //    int MsrNo = Convert.ToInt32(ViewState["msnoid"]);
    //    #region Condition
    //    if (!(string.IsNullOrEmpty(txtfromdate.Text) | string.IsNullOrEmpty(txttodate.Text)))
    //    {
    //        condition = condition + " and AddDate >= '" + txtfromdate.Text + "' AND AddDate <= '" + txttodate.Text + "'";
    //    }        

    //    #endregion


    //    List<ParmList> _list = new List<ParmList>();
    //    _list.Add(new ParmList() { name = "@Action", value = "L" });
    //    dtEmployee = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _list);
    //    if (dtEmployee.Rows.Count > 0)
    //    {
    //        dtEmployee.DefaultView.RowFilter = condition;
    //        gvGST.DataSource = dtEmployee;
    //        gvGST.DataBind();
    //        if (dtEmployee.Rows.Count > 0)
    //        {
    //            litrecordcount.Text = gvGST.Rows.Count.ToString();
    //            ViewState["dtExport"] = dtEmployee.DefaultView.ToTable();
    //        }
    //    }

    //}
    //public SortDirection GridViewSortDirection
    //{
    //    get
    //    {
    //        if (ViewState["sortDirection"] == null)
    //        {
    //            ViewState["sortDirection"] = SortDirection.Ascending;
    //        }
    //        return (SortDirection)ViewState["sortDirection"];
    //    }
    //    set
    //    {
    //        ViewState["sortDirection"] = value;
    //    }
    //}
    //#endregion

    //#region [GridViewEvents]
    //protected void gvGST_RowCommand(object sender, GridViewCommandEventArgs e)
    //{

    //    if (e.CommandName == "deletesuser")
    //    {
    //        string idno = "0";
    //        idno = Convert.ToString(e.CommandArgument);
    //        cls.delete_data("delete from GSTReturn_new where GSTreturnkid='" + idno + "'");
    //        fillEmployee();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Deleted successfully !!');", true);
    //    }
    //    #region [Approve]
    //    if (e.CommandName == "Approve")
    //    {

    //        try
    //        {
    //            ressf.Visible = true;
    //            reqrefno.Enabled = true;
    //            //recp.Visible = true;
    //            //reqrecipt.Enabled = true;
    //            btnFail.Enabled = false;
    //            btnFail.Visible = false;
    //            btnSuccess.Visible = true;
    //            btnSuccess.Enabled = true;
    //            string idno = "0";
    //            idno = Convert.ToString(e.CommandArgument);
    //            hdnid.Value = idno;
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);
    //        }
    //        catch (Exception ex)
    //        {

    //            Function.MessageBox(ex.Message);
    //        }
    //    }
    //    #endregion
    //    #region [Reject]
    //    if (e.CommandName == "Reject")
    //    {

    //        try
    //        {
    //            ressf.Visible = false;
    //            reqrefno.Enabled = false;
    //            //recp.Visible = false;
    //            //reqrecipt.Enabled = false;
    //            btnFail.Enabled = true;
    //            btnFail.Visible = true;
    //            btnSuccess.Visible = false;
    //            btnSuccess.Enabled = false;

    //            string idno = "0";
    //            idno = Convert.ToString(e.CommandArgument);
    //            hdnid.Value = idno;
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);

    //        }
    //        catch (Exception ex)
    //        {

    //            Function.MessageBox(ex.Message);
    //        }
    //    }
    //    #endregion
    //    #region[Client Data in Word]
    //    if (e.CommandName == "WordDownload")
    //    {
    //        try
    //        {

    //            string idno = "0";
    //            idno = Convert.ToString(e.CommandArgument);
    //            DataTable dt = new DataTable();
    //            List<ParmList> _list = new List<ParmList>();
    //            _list.Add(new ParmList() { name = "@Action", value = "ClientData" });
    //            _list.Add(new ParmList() { name = "@GSTkid", value = Convert.ToInt32(idno) });
    //            dt = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _list);
    //            if (dt.Rows.Count > 0)
    //            {
    //                GridView GridView1 = new GridView();
    //                HttpContext.Current.Response.Clear();

    //                HttpContext.Current.Response.Buffer = true;

    //                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=User " + dt.Rows[0]["NameOnPan"] + ".doc");

    //                HttpContext.Current.Response.Charset = "";

    //                HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";

    //                StringWriter sw = new StringWriter();

    //                HtmlTextWriter hw = new HtmlTextWriter(sw);
    //                GridView1.DataSource = dt;
    //                GridView1.AllowPaging = false;

    //                GridView1.DataBind();

    //                GridView1.RenderControl(hw);

    //                HttpContext.Current.Response.Output.Write(sw.ToString());

    //                HttpContext.Current.Response.Flush();

    //                HttpContext.Current.Response.End();
    //            }

    //        }
    //        catch (Exception ex)
    //        {

    //            Function.MessageBox(ex.Message);
    //        }
    //    }
    //    #endregion
    //}

    //protected void gvGST_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    try
    //    {
    //        DataTable dt = (DataTable)ViewState["dtExport"];
    //        DataView dv = new DataView(dt);
    //        if (GridViewSortDirection == SortDirection.Ascending)
    //        {
    //            GridViewSortDirection = SortDirection.Descending;
    //            dv.Sort = e.SortExpression + " DESC";
    //        }
    //        else
    //        {
    //            GridViewSortDirection = SortDirection.Ascending;
    //            dv.Sort = e.SortExpression + " ASC";
    //        }
    //        gvGST.DataSource = dv;
    //        gvGST.DataBind();
    //    }
    //    catch (Exception ex)
    //    { }
    //}
    //#endregion

    //#region [ddlPaging]

    //protected void gvGST_PageIndexChanging(object sender, EventArgs e)
    //{
    //    fillEmployee();
    //}

    //#endregion

    //#region [Export To Excel/Word/Pdf]
    //protected void btnexportExcel_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        dtExport = (DataTable)ViewState["dtExport"];
    //        if (dtExport.Rows.Count > 0)
    //        {
    //            dtExport.Columns.Remove("RequestBymsrno");                
    //            Common.Export.ExportToExcel(dtExport, "Report");
    //        }
    //    }
    //    catch
    //    { }

    //}
    //protected void btnexportWord_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        dtExport = (DataTable)ViewState["dtExport"];
    //        if (dtExport.Rows.Count > 0)
    //        {
    //            dtExport.Columns.Remove("RequestBymsrno");               
    //            Common.Export.ExportToWord(dtExport, "Report");
    //        }
    //    }
    //    catch
    //    { }

    //}
    //protected void btnexportPdf_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        dtExport = (DataTable)ViewState["dtExport"];
    //        if (dtExport.Rows.Count > 0)
    //        {
    //            dtExport.Columns.Remove("RequestBymsrno");                
    //            Common.Export.ExportTopdf(dtExport, "Report");
    //        }
    //    }
    //    catch
    //    { }
    //}

    //#endregion

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    fillEmployee();
    //}

    //protected void gvGST_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        e.Row.TableSection = TableRowSection.TableHeader;
    //    }
    //}

    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    int comissionamount = 0;
    //    int cnt = 0;
    //    DataTable dtgetmsrno = cls.select_data_dt("select * from  GSTDetails where GSTkid='" + Convert.ToInt32(hdnid.Value) + "'");
    //    int msrnoretailer = Convert.ToInt32(dtgetmsrno.Rows[0]["RequestBymsrno"].ToString());
    //    decimal Amoutpancardretailer = Convert.ToDecimal(dtgetmsrno.Rows[0]["Amount"].ToString());
    //    DataTable dtgetmemeberidretailer = cls.select_data_dt("select * from tblmlm_membermaster where MsrNo='" + msrnoretailer + "'");
    //    DataTable dtgetdistrimsrno = cls.select_data_dt("select * from ViewMLM_MemberSearch where MemberID='" + dtgetmemeberidretailer.Rows[0]["MemberID"] + "'");
    //    if (dtgetdistrimsrno.Rows.Count > 0)
    //    {
    //        DataTable dtgetdistrfees = cls.select_data_dt("SELECT MemberID,MsrNo,STDCode FROM tblmlm_membermaster WHERE MsrNO='" + Convert.ToInt32(dtgetdistrimsrno.Rows[0]["ParentMsrNo"]) + "'");
    //        string memberidss = dtgetdistrfees.Rows[0]["MemberID"].ToString();
    //        if (memberidss == "100000")
    //        {

    //           // string Img = uploadPanImage(FileUploadadressImage);
    //            cls.update_data("update [GSTDetails] set RequestStatus='Success',RefNo='" + txt_refno.Text + "',Remarks='" + txtadminRemark.Text.Trim() + "',ModifiedDate=getdate() where GSTkid=" + Convert.ToInt32(hdnid.Value) + "");
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);
    //            fillEmployee();
    //        }
    //        else
    //        {
    //            if (dtgetdistrfees.Rows.Count > 0)
    //            {

    //                decimal fees = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=3 and actiontype='Gst Return Fee'"));
    //                //   int fees = Convert.ToInt32(dtgetdistrfees.Rows[0]["STDCode"].ToString());
    //                string memberid = dtgetdistrfees.Rows[0]["MemberID"].ToString();
    //                comissionamount = Convert.ToInt32(Amoutpancardretailer) - Convert.ToInt32(fees);
    //                cls_myMember clsm = new cls_myMember();
    //                cnt = clsm.Wallet_Addfund(Convert.ToInt32(dtgetdistrimsrno.Rows[0]["ParentMsrNo"]), memberid, Convert.ToDecimal(comissionamount), "GST Return Comission from member id " + dtgetmemeberidretailer.Rows[0]["MemberID"] + "and Transactionid is:" + dtgetmsrno.Rows[0]["txnID"].ToString(), "0");
    //                if (cnt > 0)
    //                {
    //                   // string Img = uploadPanImage(FileUploadadressImage);
    //                    cls.update_data("update [GSTDetails] set RequestStatus='Success',RefNo='" + txt_refno.Text + "',Remarks='" + txtadminRemark.Text.Trim() + "',ModifiedDate=getdate() where GSTkid=" + Convert.ToInt32(hdnid.Value) + "");
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);
    //                    fillEmployee();
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some error Occured Please Try Again!!');disablePopup();", true);

    //    }


    //}

    //protected void btnFail_Click(object sender, EventArgs e)
    //{

    //    DataTable dt = new DataTable();
    //    dt = cls.select_data_dt("select MemberId ,* from GSTDetails inner join  tblmlm_membermaster  on tblmlm_membermaster.msrno=GSTDetails.RequestByMsrno where GSTkid='" + hdnid.Value + "'");
    //    if (dt.Rows.Count > 0)
    //    {
    //        Decimal Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
    //        string MemberID = Convert.ToString(dt.Rows[0]["MemberId"].ToString());
    //        string TxnID = dt.Rows[0]["txnid"].ToString();
    //        if (MemberID != "" && MemberID != null)
    //        {
    //            cls.update_data("update [GSTDetails] set RequestStatus='failed',RefNo='0',Remarks='" + txtadminRemark.Text.Trim() + "',ModifiedDate=getdate() where GSTkid=" + Convert.ToInt32(hdnid.Value) + "");
    //            cls_myMember clsm = new cls_myMember();
    //            clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse - GST Request TxnID:-" + TxnID);
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);
    //            fillEmployee();
    //        }
    //    }
    //}

    //private string uploadPanImage(FileUpload _fup)
    //{
    //    clsImageResize objImageResize = new clsImageResize();
    //    if (_fup.HasFile == true)
    //    {
    //        if (_fup.PostedFile.FileName != "")
    //        {
    //            string opath = Server.MapPath("~/Uploads/Servicesimage/Actual/");
    //            string mpath = Server.MapPath("~/Uploads/Servicesimage/Medium/");
    //            string spath = Server.MapPath("~/Uploads/Servicesimage/Small/");
    //            if (!Directory.Exists(opath))
    //            {
    //                Directory.CreateDirectory(opath);
    //            }
    //            if (!Directory.Exists(mpath))
    //            {
    //                Directory.CreateDirectory(mpath);
    //            }
    //            if (!Directory.Exists(spath))
    //            {
    //                Directory.CreateDirectory(spath);
    //            }

    //            //Check file extension (must be JPG)
    //            string Extension = System.IO.Path.GetExtension(_fup.FileName).ToLower();
    //            if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf" || Extension == ".doc" || Extension == ".docx")
    //            {
    //                string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
    //                _fup.PostedFile.SaveAs(opath + FileName);
    //                objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
    //                objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);

    //                return FileName;
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select JPG/JPEG/PNG/GIF/PDF/Word/Text File Only!');", true);

    //            }
    //        }
    //    }
    //    else
    //    {
    //        return "";
    //    }

    //    return "";
    //}





    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
            {
                txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
    }


    #region [Function]
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillposrequest()
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "L" });
        //  _lstparm.Add(new ParmList() { name = "@datefrom", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        //  _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
           
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Membername = dtrow["MemberName"].ToString();
            cust.email = dtrow["Email"].ToString();
            cust.mobile = dtrow["MobileNo"].ToString();
            cust.agentmobile = dtrow["Mobile"].ToString();
           // cust.adharnumber = dtrow["Aadhar_no"].ToString();
            cust.pannumber = dtrow["BusinessPanCard"].ToString();
            cust.aadharfont = dtrow["NameOnPan"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.RequestBymsrno = dtrow["RequestBymsrno"].ToString();
            cust.AddDate = dtrow["addDate"].ToString();
            cust.Remark = dtrow["Remarks"].ToString();
            cust.RequestBymsrno = dtrow["GSTkid"].ToString();
            // cust.noc = dtrow["noc"].ToString();
            //cust.marksheet = dtrow["marksheet"].ToString();
            cust.status = dtrow["RequestStatus"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    [WebMethod]
    public static List<Customer> fillposrequestbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();

        _lstparm.Add(new ParmList() { name = "@Action", value = "date" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {

            Customer cust = new Customer();

            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Membername = dtrow["MemberName"].ToString();
            cust.email = dtrow["Email"].ToString();
            cust.mobile = dtrow["MobileNo"].ToString();
            // cust.agentmobile = dtrow["Mobile1"].ToString();
            // cust.adharnumber = dtrow["Aadhar_no"].ToString();
            cust.pannumber = dtrow["BusinessPanCard"].ToString();
            cust.aadharfont = dtrow["NameOnPan"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.RequestBymsrno = dtrow["RequestBymsrno"].ToString();
            cust.AddDate = dtrow["addDate"].ToString();
            cust.Remark = dtrow["Remarks"].ToString();
            cust.RequestBymsrno = dtrow["GSTkid"].ToString();
            // cust.noc = dtrow["noc"].ToString();
            //cust.marksheet = dtrow["marksheet"].ToString();
            cust.status = dtrow["RequestStatus"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(0);
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();

            _lstparm.Add(new ParmList() { name = "@Action", value = "L" });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "Udayog_Report");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select date range to genrate excel');", true);
        }
    }

    [WebMethod]
    public static List<Customer> ShowFundImage(string fundid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "Client" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = fundid });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.passbook = dtrow["PhotoImage"].ToString();
            //cust.aadharfont = dtrow["PANcard"].ToString();
            cust.adharback = dtrow["AadharImage"].ToString();
            cust.pancard = dtrow["ChequeImage"].ToString();
            cust.file = dtrow["OtherDocImage"].ToString();
            cust.file2 = dtrow["PanCardImage"].ToString();
            //cust.file3 = dtrow["file3"].ToString();
           // cust.file4 = dtrow["file4"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    #endregion

    #region class
    public class Customer
    {
        public string status { get; set; }
        public string MemberID { get; set; }
        public string Membername { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string adharnumber { get; set; }
        public string pannumber { get; set; }
        public string aadharfont { get; set; }
        public string adharback { get; set; }
        public string RequestBymsrno { get; set; }
        public string AddDate { get; set; }
        public string Remark { get; set; }
        public string pancard { get; set; }
        public string file4 { get; set; }
        public string passbook { get; set; }
        public string photo { get; set; }
        public string file { get; set; }
        public string file2 { get; set; }
        public string file3 { get; set; }
        public string txnid { get; set; }
        public string agentmobile { get; set; }
    }

    #endregion

    [WebMethod]
    public static List<Customer> ApproveRequest(string msrno)
    {
        DataTable dt = new DataTable();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        cls_connection cls = new cls_connection();
        string msrnoid = msrno;
        HttpContext.Current.Session["msrno"] = msrnoid;
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "Client" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = msrno });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MobileNo"].ToString();

            custList.Add(cust);
        }
        return custList;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string adminstatus = txt_status.Text;
        string receipt = uploadPanImage(RECEPT);
        if (receipt != "" && adminstatus != "")
        {
            string txnid = HttpContext.Current.Session["msrno"].ToString();
            cls.select_data_dt(@"Update GSTDetails set Remarks='" + adminstatus + "',AuthLetterImage='" + receipt + "',Requeststatus='Success'  Where  txnid='" + txnid + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('GSTRegistrationReport.aspx');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload recepit and remark!');location.replace('GSTRegistrationReport.aspx');", true);
        }


    }


    protected void btnSubmit_Reject(object sender, EventArgs e)
    {
        string adminstatus = txt_status.Text;
        if (adminstatus != "")
        {
            string txnid = HttpContext.Current.Session["msrno"].ToString();
            DataTable id = cls.select_data_dt(@"select * from  GSTDetails where  txnid='" + txnid + "'");
            string GSTkid = id.Rows[0]["GSTkid"].ToString();
            string RequestBymsrno = id.Rows[0]["RequestBymsrno"].ToString();
            decimal Amount = Convert.ToDecimal(id.Rows[0]["Amount"].ToString());
            DataTable ids = cls.select_data_dt(@"select * from  tblmlm_membermaster where  msrno='" + RequestBymsrno + "'");
            string MemberID = ids.Rows[0]["MemberID"].ToString();
            if (MemberID != "")
            {
                cls.update_data("update GSTDetails set RequestStatus='failed',Remarks='" + adminstatus + "' where GSTkid=" + GSTkid + "");
                cls_myMember clsm = new cls_myMember();
                clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse - GST Request Request txnid:-" + txnid);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('GSTRegistrationReport.aspx');", true);


            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload remark!');location.replace('GSTRegistrationReport.aspx');", true);
        }

    }
    [WebMethod]
    public static List<Customer> clientdata(string msrno)
    {
        DataTable dt = new DataTable();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        cls_connection cls = new cls_connection();
        string msrnoid = msrno;
        HttpContext.Current.Session["msrno"] = msrnoid;
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "Client" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = msrno });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["mobileNo"].ToString();

            custList.Add(cust);
        }
        return custList;
    }

    protected void btn_export_Clickdata(object sender, EventArgs e)
    {

        cls_connection cls = new cls_connection();
        string txnid = HttpContext.Current.Session["msrno"].ToString();
        List<Customer> custList = new List<Customer>();
        DataTable dt = new DataTable();
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Action", value = "Client" });
        _list.Add(new ParmList() { name = "@txnid", value = txnid });
        dt = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _list);
        if (dt.Rows.Count > 0)
        {
            Common.Export.ExportToExcel(dt, "gstreg_Report");
        }
        else
        {

        }

    }

    private string uploadPanImage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Root/Upload/PanCardRequest/Actual/");
                string mpath = Server.MapPath("~/Root/Upload/PanCardRequest/Medium/");
                string spath = Server.MapPath("~/Root/Upload/PanCardRequest/Small/");

                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
                if (!Directory.Exists(mpath))
                {
                    Directory.CreateDirectory(mpath);
                }
                if (!Directory.Exists(spath))
                {
                    Directory.CreateDirectory(spath);
                }

                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(_fup.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".xls" || Extension == ".pdf" || Extension == ".xlsx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                    // objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
                    //   objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);

                    return FileName;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select JPG/JPEG/PNG/GIF Images Only!');", true);

                }
            }
        }
        else
        {
            return "";
        }

        return "";
    }
}