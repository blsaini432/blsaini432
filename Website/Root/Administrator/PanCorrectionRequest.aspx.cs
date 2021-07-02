using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
public partial class Root_Admin_pancorrereport : System.Web.UI.Page
{
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillpanreport();
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {

        DataTable dtExport = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(txt_fdate.Text) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(txttdate.Text) });
        dtExport = cls.select_data_dtNew("pancardcorr_report", _lstparm);
        if (dtExport.Rows.Count > 0)
        {
            Common.Export.ExportToExcel(dtExport, "pancardcorr_report");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);
        }

    }
    #region [Function]
    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static string Clientdata(string fundid)
    {
        return "../../Download.aspx?kid=" + fundid;
    }

    public void fillpanreport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        int MsrNo = Convert.ToInt32(0);
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("pancardcorr_report", _lstparm);
        if (dtEWalletTransaction.Rows.Count > 0)
        {
            gvBookedBusList.DataSource = dtEWalletTransaction;
            gvBookedBusList.DataBind();
        }
    }

    public void fillpanreportbydate(string fromdate, string todate, string txnid)
    {
        int MsrNo = Convert.ToInt32(1);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        if (TxnID.Text != "")
        {
            _lstparm.Add(new ParmList() { name = "@action", value = "admintxnid" });
            _lstparm.Add(new ParmList() { name = "@status", value = ddl_stauts.SelectedItem.ToString() });
            _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
            dtEWalletTransaction = cls.select_data_dtNew("pancardcorr_report", _lstparm);
            if (dtEWalletTransaction.Rows.Count > 0)
            {
                gvBookedBusList.DataSource = dtEWalletTransaction;
                gvBookedBusList.DataBind();
            }
            else
            {
                gvBookedBusList.DataBind();
            }
        }
        else if(txttdate.Text !="" && txt_fdate.Text !="")
        {
            _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
            _lstparm.Add(new ParmList() { name = "@status", value = ddl_stauts.SelectedItem.ToString() });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
           
            dtEWalletTransaction = cls.select_data_dtNew("pancardcorr_report", _lstparm);
            if (dtEWalletTransaction.Rows.Count > 0)
            {
                gvBookedBusList.DataSource = dtEWalletTransaction;
                gvBookedBusList.DataBind();
            }
            else
            {
                gvBookedBusList.DataBind();
            }

        }      
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select Date!');", true);
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequest(string fundid, string bankid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        if (bankid != "")
        {
            string id = fundid;
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from PanCardDetails where pankid='" + id + "' and RequestStatus='Pending'");
            if (dt.Rows.Count > 0)
            {
                cls.update_data("update [PanCardDetails] set RequestStatus='Success',RefNo='" + bankid + "',ReciptImg='',DeclineOrSuccessdate=getdate() where Pankid=" + Convert.ToInt32(id) + "");
                actions = "success";
                return actions;
            }
            else
            {
                return actions;
            }
        }
        else
        {
            return actions;
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string RejectRequest(string fundid, string bankid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        if (bankid != "")
        {
            string id = fundid;
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from PanCardDetails p inner join ViewMLM_MemberSearch s on p.RequestBymsrno=s.msrno where pankid='" + id + "' and RequestStatus='Pending'");
            if (dt.Rows.Count > 0)
            {
                Decimal Amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                string TxnID = dt.Rows[0]["TxnID"].ToString();
                string MemberID = dt.Rows[0]["MemberID"].ToString();
                string narration = "Reverse - PanCard Request TxnID:-" + TxnID;
                cls.update_data("update [PanCardDetails] set RequestStatus='failed',RefNo='0',ReciptImg='',Remarks='" + bankid + "',DeclineOrSuccessdate=getdate() where Pankid=" + Convert.ToInt32(id) + "");
                cls_myMember clsm = new cls_myMember();
                clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse - PanCard Request TxnID:-" + TxnID);
                actions = "success";
                return actions;
            }
            else
            {
                return actions;
            }
        }
        else
        {
            return actions;
        }

    }


    #region [GridViewEvents]
    protected void gvBookedBusList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [Approve]
        if (e.CommandName == "Approve")
        {
            try
            {
                string id = "0";
                id = Convert.ToString(e.CommandArgument);
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                TextBox txt = (TextBox)row.FindControl("txt_refno");
                FileUpload fupRcpt = (FileUpload)row.FindControl("fupRcpt");
                string Img = uploadPanImage(fupRcpt);
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select * from PanCardDetails where pankid='" + id + "' and RequestStatus='Pending'");
                if (dt.Rows.Count > 0)
                {
                    DataTable dd = new DataTable();
                    dd = cls.select_data_dt("select * from tblmlm_membermaster where msrno=" + Convert.ToInt32(dt.Rows[0]["RequestBymsrno"]) + "");
                    if (dd.Rows.Count > 0)
                    {
                        string memberid = dd.Rows[0]["MemberId"].ToString();
                        cls.update_data("update [PanCardDetails] set RequestStatus='Success',ReciptImg='" + Img + "',RefNo='" + txt.Text + "',DeclineOrSuccessdate=getdate() where Pankid=" + Convert.ToInt32(id) + "");

                        List<ParmList> _lstparms = new List<ParmList>();
                        _lstparms.Add(new ParmList() { name = "@CMemberId", value = memberid });
                        _lstparms.Add(new ParmList() { name = "@TxnId", value = dt.Rows[0]["txnID"].ToString() });
                        DataTable dtcom = new DataTable();
                        dtcom = cls.select_data_dtNew("PROC_NSDL_Comission", _lstparms);
                        fillpanreport();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');", true);
                    }
                }

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion
        #region [Reject]
        if (e.CommandName == "Reject")
        {
            try
            {
                string id = "0";
                id = Convert.ToString(e.CommandArgument);
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                TextBox txt = (TextBox)row.FindControl("txt_remarks");
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select * from PanCardDetails p inner join ViewMLM_MemberSearch s on p.RequestBymsrno=s.msrno where pankid='" + id + "' and RequestStatus='Pending'");
                if (dt.Rows.Count > 0)
                {
                    Decimal Amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                    string TxnID = dt.Rows[0]["TxnID"].ToString();
                    string MemberID = dt.Rows[0]["MemberID"].ToString();
                    string narration = "Reverse - PanCard Request TxnID:-" + TxnID;
                    cls.update_data("update [PanCardDetails] set RequestStatus='failed',RefNo='0',ReciptImg='',Remarks='" + txt.Text + "',DeclineOrSuccessdate=getdate() where Pankid=" + Convert.ToInt32(id) + "");
                    cls_myMember clsm = new cls_myMember();
                    clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse -Correction PanCard Request TxnID:-" + TxnID);
                    fillpanreport();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');", true);


                }
            }
            catch (Exception ex)
            {
                Function.MessageBox(ex.Message);
            }
        }

        if (e.CommandName == "TempReject")
        {
            try
            {
                string id = "0";
                id = Convert.ToString(e.CommandArgument);
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                TextBox txt = (TextBox)row.FindControl("txt_remarks");
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select * from PanCardDetails p inner join ViewMLM_MemberSearch s on p.RequestBymsrno=s.msrno where pankid='" + id + "' and RequestStatus='Pending'");
                if (dt.Rows.Count > 0)
                {
                    Decimal Amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                    string TxnID = dt.Rows[0]["TxnID"].ToString();
                    string MemberID = dt.Rows[0]["MemberID"].ToString();
                    cls.update_data("update [PanCardDetails] set RequestStatus='Temp Rejected',RefNo='0',ReciptImg='',Remarks='" + txt.Text + "',DeclineOrSuccessdate=getdate() where Pankid=" + Convert.ToInt32(id) + "");
                    fillpanreport();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');", true);


                }
            }
            catch (Exception ex)
            {
                Function.MessageBox(ex.Message);
            }
        }
        #endregion
        #region[Client Data in Word]
        if (e.CommandName == "downloadmerge")
        {
            string path = e.CommandArgument.ToString();
            string[] array = path.Split('|');
            string path1 = "http://onebreeze.in/Uploads/Servicesimage/Actual/" + array[0];
            string path2 = "http://onebreeze.in/Uploads/Servicesimage/Actual/" + array[1];
            string refno = array[2];
            PdfReader pdfReader1 = new PdfReader(path1);
            PdfReader pdfReader2 = new PdfReader(path2);
            List<PdfReader> readerList = new List<PdfReader>();
            readerList.Add(pdfReader1);
            readerList.Add(pdfReader2);
            //Define a new output document and its size, type
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            //Get instance response output stream to write output file.
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();

            foreach (PdfReader reader in readerList)
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    document.Add(iTextSharp.text.Image.GetInstance(page));
                }
            }
            document.Close();
            string ffname = refno + ".pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + ffname + "");
            Response.ContentType = "application/pdf";
            Response.End();

        }
        if (e.CommandName == "WordDownload")
        {
            try
            {
                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                DataTable dt = new DataTable();
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@action", value = "ClientData" });
                _lstparm.Add(new ParmList() { name = "@RequestBymsrno", value = Convert.ToInt32(idno) });
                dt = cls.select_data_dtNew("pancardcorr_report", _lstparm);
                if (dt.Rows.Count > 0)
                {
                    Common.Export.ExportToExcel(dt, "pancardcorr_report");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);
                }

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion
    }


    #endregion

    #endregion

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string Status { get; set; }
        public string RequestBy { get; set; }
        public string Name { get; set; }
        public string PanCardType { get; set; }
        public string TxnID { get; set; }
        public string Amount { get; set; }
        public string RequestDate { get; set; }
        public string Remarks { get; set; }
        public string RefNo { get; set; }
        public string PankID { get; set; }
        public string AddProof { get; set; }



    }
    protected void gvBookedBusList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBookedBusList.PageIndex = e.NewPageIndex;
        if (txttdate.Text == "" || txt_fdate.Text == "")
        {
            fillpanreport();
        }
        else
        {
            fillpanreportbydate(txt_fdate.Text, txttdate.Text,TxnID.Text);
        }
    }

    #endregion

    private static DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }

    protected void btnsearch_Click1(object sender, EventArgs e)
    {
        fillpanreportbydate(txt_fdate.Text, txttdate.Text, TxnID.Text);
    }



    private string uploadPanImage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Uploads/Servicesimage/Actual/");
                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
                string Extension = System.IO.Path.GetExtension(_fup.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf" || Extension == ".doc" || Extension == ".docx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);

                    return FileName;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select JPG/JPEG/PNG/GIF/PDF/Word/Text File Only!');", true);

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