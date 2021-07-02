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
using iTextSharp.text;
public partial class Root_Admin_PanRequest_new : System.Web.UI.Page
{
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    cls_myMember clsm = new cls_myMember();
    public static DataTable dtMemberMaster = new DataTable();
    string condition = " MsrNo > 0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (txttodatea.Text.Trim() == "" || txttodatea.Text.Trim() == "")
            {
                txttodatea.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                txt_fromdatea.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
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
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
            dtExport = cls.select_data_dtNew("pancard_report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "pancard_report");
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
    public static List<Customer> PanReport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@action", value = "adminftr" });
        _lstparm.Add(new ParmList() { name = "@status", value = "Pending" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("pancard_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Member_Name = dtrow["MemberName"].ToString();
            // cust.servicename = dtrow["servicename"].ToString();
            cust.Pankid = dtrow["Pankid"].ToString();
            cust.trans_amt = dtrow["Amount"].ToString();
            cust.AddProof = dtrow["AddProof"].ToString();
            cust.successdate = dtrow["Staussss"].ToString();
            cust.agent_id = dtrow["Acknowledgement_No"].ToString();
            cust.ReciptImg = dtrow["ReciptImg"].ToString();
            cust.RefNo = dtrow["RefNo"].ToString();
            cust.paydate = dtrow["RequestDate"].ToString();
            cust.statu = dtrow["Status"].ToString();
            cust.Remarks = dtrow["Remarks"].ToString();
            cust.customername = dtrow["NameOnPAN"].ToString();

            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> fillreportbydate(string fromdate, string todate,string ddl_stauts)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@action", value = "adminftr" });
        _lstparm.Add(new ParmList() { name = "@status", value = ddl_stauts});
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("pancard_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Member_Name = dtrow["MemberName"].ToString();
            cust.successdate = dtrow["Staussss"].ToString();
            cust.Pankid = dtrow["Pankid"].ToString();
            cust.trans_amt = dtrow["Amount"].ToString();
            cust.AddProof = dtrow["AddProof"].ToString();
            cust.agent_id = dtrow["Acknowledgement_No"].ToString();
            cust.ReciptImg = dtrow["ReciptImg"].ToString();
            cust.Remarks = dtrow["Remarks"].ToString();
            cust.RefNo = dtrow["RefNo"].ToString();
            cust.paydate = dtrow["RequestDate"].ToString();
            cust.statu = dtrow["Status"].ToString();
            cust.customername = dtrow["NameOnPAN"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion

    #region class
    public class Customer
    {
        public string MemberID { get; set; }
        public string Member_Name { get; set; }
        public string servicename { get; set; }
        public string account_no { get; set; }
        public string trans_amt { get; set; }
        public string ReciptImg { get; set; }
        public string agent_id { get; set; }
        public string adm_com { get; set; }
        public string AddProof { get; set; }
        public string paydate { get; set; }
        public string statu { get; set; }
        public string Pankid { get; set; }
        public string customername { get; set; }
        public string RefNo { get; set; }
        public string successdate { get; set; }
        public string  Remarks { get; set; }
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
        HttpContext.Current.Session["pankid"] = msrnoid;
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@action", value = "adminapp" });
        _lstparm.Add(new ParmList() { name = "@RequestBymsrno", value = msrno });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("pancard_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.Pankid = dtrow["Pankid"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    [WebMethod]
    public static List<Customer> clientdata(string msrno)
    {
        DataTable dt = new DataTable();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        cls_connection cls = new cls_connection();
        string msrnoid = msrno;
        HttpContext.Current.Session["pankid"] = msrnoid;
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@action", value = "adminapp" });
        _lstparm.Add(new ParmList() { name = "@RequestBymsrno", value = msrno });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("pancard_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.Pankid = dtrow["Pankid"].ToString();
            custList.Add(cust);
        }
        return custList;
    }




    [WebMethod]
    public static List<Customer> FinalPdf(string ReciptImg, string AddProof, string RefNo, string Pankid)
    {
        DataTable dt = new DataTable();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        cls_connection cls = new cls_connection();
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        string Finalpdf = ReciptImg + "|" + AddProof + "|" + RefNo;
        HttpContext.Current.Session["Finalpdf"] = Finalpdf;
       // btnSubmit_finalpdf();
        _lstparm.Add(new ParmList() { name = "@action", value = "adminapp" });
        _lstparm.Add(new ParmList() { name = "@RequestBymsrno", value = Pankid });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("pancard_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.Pankid = dtrow["Pankid"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    protected void btnSubmit_Success(object sender, EventArgs e)
    {
        try
        {
            string id = "0";
            id = HttpContext.Current.Session["pankid"].ToString();
            // GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            string  refno = txt_refno.Text;
            string Img = uploadPanImage(fupRcpt);
            if (Img != "" && refno != "")
            {
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select * from PanCardDetails where pankid='" + id + "' and RequestStatus='Pending'");
                if (dt.Rows.Count > 0)
                {
                    DataTable dd = new DataTable();
                    dd = cls.select_data_dt("select * from tblmlm_membermaster where msrno=" + Convert.ToInt32(dt.Rows[0]["RequestBymsrno"]) + "");
                    if (dd.Rows.Count > 0)
                    {
                        string memberid = dd.Rows[0]["MemberId"].ToString();
                        cls.update_data("update [PanCardDetails] set RequestStatus='Success',ReciptImg='" + Img + "',RefNo='" + refno + "',DeclineOrSuccessdate=getdate() where Pankid=" + Convert.ToInt32(id) + "");

                        List<ParmList> _lstparms = new List<ParmList>();
                        _lstparms.Add(new ParmList() { name = "@CMemberId", value = memberid });
                        _lstparms.Add(new ParmList() { name = "@TxnId", value = dt.Rows[0]["txnID"].ToString() });
                        DataTable dtcom = new DataTable();
                        dtcom = cls.select_data_dtNew("PROC_NSDL_Comission", _lstparms);
                       // fillpanreport();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Upload Receipt and Acknowledge number !!');", true);
            }

        }
        catch (Exception ex)
        {

            Function.MessageBox(ex.Message);
        }



    }

    protected void btnSubmit_Reject(object sender, EventArgs e)
    {
        try
        {
            string id = "0";
            id = HttpContext.Current.Session["pankid"].ToString();
           // GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            string status = txt_status.Text;
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from PanCardDetails p inner join ViewMLM_MemberSearch s on p.RequestBymsrno=s.msrno where pankid='" + id + "' and RequestStatus='Pending'");
            if (dt.Rows.Count > 0)
            {
                Decimal Amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                string TxnID = dt.Rows[0]["TxnID"].ToString();
                string MemberID = dt.Rows[0]["MemberID"].ToString();
                string narration = "Reverse - PanCard Request TxnID:-" + TxnID;
                cls.update_data("update [PanCardDetails] set RequestStatus='failed',RefNo='0',ReciptImg='',Remarks='" + status + "',DeclineOrSuccessdate=getdate() where Pankid=" + Convert.ToInt32(id) + "");
                cls_myMember clsm = new cls_myMember();
                clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse - PanCard Request TxnID:-" + TxnID);
              //  fillpanreport();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');", true);


            }
        }
        catch (Exception ex)
        {
            Function.MessageBox(ex.Message);
        }

    }

    protected  void btnSubmit_dataa(object sender, EventArgs e)
    {
        try
        {
            
                string idno = "0";
            idno  = HttpContext.Current.Session["pankid"].ToString();
            DataTable dt = new DataTable();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@action", value = "ClientData" });
            _lstparm.Add(new ParmList() { name = "@RequestBymsrno", value = Convert.ToInt32(idno) });
            dt = cls.select_data_dtNew("pancard_report", _lstparm);
           
            if (dt.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dt, "pancard_report");
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

    protected void btnSubmit_finalpdf(object sender, EventArgs e)
    {
        try
        {
            string path = HttpContext.Current.Session["Finalpdf"].ToString();
            string[] array = path.Split('|');
            string path1 = "https://www.sssolutionsindia.net.in/Uploads/Servicesimage/Actual/" + array[0];
            string path2 = "https://www.sssolutionsindia.net.in/Uploads/Servicesimage/Actual/" + array[1];
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
        catch (Exception ex)
        {

            Function.MessageBox(ex.Message);
        }
    }

    private string uploadPanImage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Uploads/Servicesimage/Actual/");
                string mpath = Server.MapPath("~/Uploads/Servicesimage/Medium/");
                string spath = Server.MapPath("~/Uploads/Servicesimage/Small/");

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
