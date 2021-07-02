using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using Common;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Portal_Admin_Voteridrequest : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    cls_myMember clsm = new cls_myMember();
    string condition = " MsrNo > 0";
    #endregion

    





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
        dtEWalletTransaction = cls.select_data_dtNew("Proc_SHOPACT", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.passbook = dtrow["MemberName"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Membername = dtrow["MemberName"].ToString();
            cust.email = dtrow["Email"].ToString();
            cust.mobile = dtrow["Mobile"].ToString();
            cust.agentmobile = dtrow["Mobile1"].ToString();
            cust.adharnumber = dtrow["Aadharnumber"].ToString();
            cust.pannumber = dtrow["Name_of_emp"].ToString();
            cust.name = dtrow["Name_of_ect"].ToString();
            cust.txnid = dtrow["Acknowledgement_No"].ToString();
            cust.RequestBymsrno = dtrow["RequestBymsrno"].ToString();
            cust.AddDate = dtrow["addDate"].ToString();
            cust.Remark = dtrow["Remarks"].ToString();
            cust.dgid = dtrow["shopact_id"].ToString();
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
        dtEWalletTransaction = cls.select_data_dtNew("Proc_SHOPACT", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {

            Customer cust = new Customer();

            cust.passbook = dtrow["MemberName"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Membername = dtrow["MemberName"].ToString();
            cust.email = dtrow["Email"].ToString();
            cust.mobile = dtrow["Mobile"].ToString();
            cust.agentmobile = dtrow["Mobile1"].ToString();
            cust.adharnumber = dtrow["Aadharnumber"].ToString();
            cust.pannumber = dtrow["Name_of_emp"].ToString();
            cust.name = dtrow["Name_of_ect"].ToString();
            cust.txnid = dtrow["Acknowledgement_No"].ToString();
            cust.RequestBymsrno = dtrow["RequestBymsrno"].ToString();
            cust.AddDate = dtrow["addDate"].ToString();
            cust.Remark = dtrow["Remarks"].ToString();
            cust.dgid = dtrow["shopact_id"].ToString();
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

            _lstparm.Add(new ParmList() { name = "@Action", value = "date" });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Proc_SHOPACT", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "DG_Report");
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
        dtEWalletTransaction = cls.select_data_dtNew("Proc_SHOPACT", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.photo = dtrow["photo"].ToString();
            cust.aadhar = dtrow["Aadhar"].ToString();
           cust.pancard = dtrow["Oldshop"].ToString();
             cust.actphoto = dtrow["Actualphoto"].ToString();
            cust.ageProof = dtrow["file5"].ToString();
            cust.file4 = dtrow["file4"].ToString();
            cust.sign = dtrow["sign"].ToString();
            cust.file3 = dtrow["file3"].ToString();
            cust.file2 = dtrow["file2"].ToString();
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
        public string aadhar { get; set; }
        public string file3 { get; set; }
        public string RequestBymsrno { get; set; }
        public string AddDate { get; set; }
        public string Remark { get; set; }
        public string pancard { get; set; }
        public string file4 { get; set; }
        public string passbook { get; set; }
        public string photo { get; set; }
        public string ageProof { get; set; }
        public string file2 { get; set; }
        public string dgid { get; set; }
        public string txnid { get; set; }
        public string agentmobile { get; set; }
        public string actphoto { get; set; }
        public string name { get; set; }
        public string sign { get; set; }
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
        dtEWalletTransaction = cls.select_data_dtNew("Proc_SHOPACT", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.ageProof = dtrow["photo"].ToString();

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
        HttpContext.Current.Session["msrno"] = msrnoid;
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "Client" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = msrno });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_SHOPACT", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.ageProof = dtrow["photo"].ToString();

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
            cls.select_data_dt(@"Update tblSHOPACT set Remarks='" + adminstatus + "',ReciptImg='" + receipt + "',Requeststatus='Success'  Where  txnid='" + txnid + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('Shopact_Request.aspx');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload recepit and remark!');location.replace('Providentfund_request.aspx');", true);
        }


    }

    protected void btnSubmit_Reject(object sender, EventArgs e)
    {
        string adminstatus = txt_status.Text;
        if (adminstatus != "")
        {
            string txnid = HttpContext.Current.Session["msrno"].ToString();
            DataTable id = cls.select_data_dt(@"select * from  tblSHOPACT where  txnid='" + txnid + "'");
            string RequestBymsrno = id.Rows[0]["RequestBymsrno"].ToString();
            decimal Amount = Convert.ToDecimal(id.Rows[0]["Amount"].ToString());
            DataTable ids = cls.select_data_dt(@"select * from  tblmlm_membermaster where  msrno='" + RequestBymsrno + "'");
            string MemberID = ids.Rows[0]["MemberID"].ToString();
            if (MemberID != "")
            {
                cls.update_data("update tblSHOPACT set RequestStatus='failed',RefNo='0',ReciptImg='',Remarks='" + adminstatus + "' where txnid=" + txnid + "");
                cls_myMember clsm = new cls_myMember();
                clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse - shop act Request txnid:-" + txnid);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);


            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload remark !!');disablePopup();", true);
        }

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
        dt = cls.select_data_dtNew("Proc_SHOPACT", _list);
        if (dt.Rows.Count > 0)
        {
            Common.Export.ExportToExcel(dt, "shopact_Report");
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