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


public partial class Portal_Admin_Account_Report : System.Web.UI.Page
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
        _lstparm.Add(new ParmList() { name = "@Action", value = "ADMIN" });
        //  _lstparm.Add(new ParmList() { name = "@datefrom", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        //  _lstparm.Add(new ParmList() { name = "@dateto", value = System.DateTime.Now.ToString("MM-dd-yyyy") });
        dtEWalletTransaction = cls.select_data_dtNew("Proc_accountopenss", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.bank = dtrow["bank"].ToString();
            cust.branchname = dtrow["branchname"].ToString();
            cust.Customeetype = dtrow["Customeetype"].ToString();
            cust.email = dtrow["Email"].ToString();
            cust.mobile = dtrow["Mobile"].ToString();
            cust.work = dtrow["work"].ToString();
            cust.state = dtrow["STATE"].ToString();
            cust.lname = dtrow["lname"].ToString();
           // cust.DOB = dtrow["DOB"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.SEX = dtrow["SEX"].ToString();
            cust.AddDate = dtrow["addDate"].ToString();
            cust.Remark = dtrow["Remarks"].ToString();
            cust.id = dtrow["id"].ToString();
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
        dtEWalletTransaction = cls.select_data_dtNew("Proc_accountopenss", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {

            Customer cust = new Customer();
            cust.bank = dtrow["bank"].ToString();
            cust.branchname = dtrow["branchname"].ToString();
            cust.Customeetype = dtrow["Customeetype"].ToString();
            cust.email = dtrow["Email"].ToString();
            cust.mobile = dtrow["Mobile"].ToString();
            cust.work = dtrow["work"].ToString();
            cust.state = dtrow["STATE"].ToString();
            cust.lname = dtrow["lname"].ToString();
           // cust.DOB = dtrow["DOB"].ToString();
            cust.txnid = dtrow["txnid"].ToString();
            cust.SEX = dtrow["SEX"].ToString();
            cust.AddDate = dtrow["addDate"].ToString();
            cust.Remark = dtrow["Remarks"].ToString();
            cust.id = dtrow["id"].ToString();
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

            _lstparm.Add(new ParmList() { name = "@Action", value = "ADMIN" });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Proc_accountopenss", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "Accountopen_Report");
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
        dtEWalletTransaction = cls.select_data_dtNew("Proc_accountopenss", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.aadhar_image = dtrow["aadhar_image"].ToString();
            cust.pan_image = dtrow["pan_image"].ToString();
            cust.photo = dtrow["photo"].ToString();
           
           
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
        public string branchname { get; set; }
        public string sex { get; set; }
        public string work { get; set; }
        public string Customeetype { get; set; }
        public string RequestBymsrno { get; set; }
        public string AddDate { get; set; }
        public string Remark { get; set; }
        public string bank { get; set; }
        public string lname { get; set; }
        public string SEX { get; set; }
        public string state { get; set; }
        public string DOB { get; set; }
        public string aadhar_image { get; set; }
        public string photo { get; set; }
        public string txnid { get; set; }
        public string pan_image { get; set; }
        public string id { get; set; }
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
        dtEWalletTransaction = cls.select_data_dtNew("Proc_accountopenss", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.bank = dtrow["bank"].ToString();

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
        dtEWalletTransaction = cls.select_data_dtNew("Proc_accountopenss", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.bank = dtrow["bank"].ToString();

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
        //string receipt = uploadPanImage(RECEPT);
        if ( adminstatus != "")
        {
            string txnid = HttpContext.Current.Session["msrno"].ToString();
            cls.select_data_dt(@"Update tbl_Accountopens set Remarks='" + adminstatus + "',Requeststatus='Success'  Where  txnid='" + txnid + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('Account_Report.aspx');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload recepit and remark!');location.replace('Account_Report.aspx');", true);
        }


    }


    protected void btnSubmit_Reject(object sender, EventArgs e)
    {
        string adminstatus = txt_status.Text;
        if (adminstatus != "")
        {
            string txnid = HttpContext.Current.Session["msrno"].ToString();
            DataTable id = cls.select_data_dt(@"select * from  tbl_Accountopens where  txnid='" + txnid + "'");
            string RequestBymsrno = id.Rows[0]["msrno"].ToString();
            decimal Amount = Convert.ToDecimal(id.Rows[0]["Amount"].ToString());
            DataTable ids = cls.select_data_dt(@"select * from  tblmlm_membermaster where  msrno='" + RequestBymsrno + "'");
            string MemberID = ids.Rows[0]["MemberID"].ToString();
            if (MemberID != "")
            {
                cls.update_data("update tbl_Accountopens set RequestStatus='failed',Remarks='" + adminstatus + "' where txnid=" + txnid + "");
                cls_myMember clsm = new cls_myMember();
               clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse - Account Open Request txnid:-" + txnid);
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
        dt = cls.select_data_dtNew("Proc_accountopenss", _list);
        if (dt.Rows.Count > 0)
        {
            Common.Export.ExportToExcel(dt, "Udayog_Report");
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