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
using System.IO;

public partial class Root_Administrator_OfflineSerivceReport : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    DataTable dtExport = new DataTable();
    public static string mssrno { get; set; }
    //  clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    // clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();
    // clsRecharge_Dispute objDispute = new clsRecharge_Dispute();
    DataTable dtDispute = new DataTable();
    cls_connection cls = new cls_connection();
    string condition = " MsrNo > 0";
    #endregion

    #region [PageLoad]
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
    #endregion

    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequest(string fundid, string psaid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";

        if (psaid != "")
        {
            cls_myMember Clsm = new cls_myMember();
            string Txn = Clsm.Cyrus_GetTransactionID_New();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@EzulixTxnid", value = fundid });
            _lstparm.Add(new ParmList() { name = "@RefrenceNumber", value = psaid });
            _lstparm.Add(new ParmList() { name = "@action", value = "approve" });
            DataTable dmt = new DataTable();
            dmt = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }

    }


    [WebMethod]
    public static List<Customer> uploads(string id)
    {
        {
            cls_connection cls = new cls_connection();
            string actions = "";

            cls_myMember Clsm = new cls_myMember();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            string Txn = Clsm.Cyrus_GetTransactionID_New();
            List<ParmList> _lstparm = new List<ParmList>();
            HttpContext.Current.Session["id"] = id;
            _lstparm.Add(new ParmList() { name = "@EzulixTxnid", value = id });
            _lstparm.Add(new ParmList() { name = "@action", value = "upload" });
            DataTable dmt = new DataTable();
            dmt = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
            //actions = "success";
            foreach (DataRow dtrow in dmt.Rows)
            {
                Customer cust = new Customer();
                cust.MemberId = dtrow["MemberId"].ToString();

                custList.Add(cust);
            }
            return custList;




        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        String Result = string.Empty;
        
            string adminstatus = txt_status.Text;
            string receipt = uploadPanImage(RECEPT);
            if (receipt != "" && adminstatus != "")
            {
                cls_myMember clsm = new cls_myMember();
                //cls_myMember Clsm = new cls_myMember();
                string Txn = clsm.Cyrus_GetTransactionID_New();
                List<ParmList> _lstparm = new List<ParmList>();
                string txnid = HttpContext.Current.Session["id"].ToString();
                _lstparm.Add(new ParmList() { name = "@EzulixTxnid", value = txnid });
                _lstparm.Add(new ParmList() { name = "@receipt", value = receipt });
                _lstparm.Add(new ParmList() { name = "@RefrenceNumber", value = adminstatus });
                _lstparm.Add(new ParmList() { name = "@action", value = "approve" });
                DataTable dmt = new DataTable();
                dmt = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
                // actions = "success";
                // return actions;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('OfflineSerivceReport.aspx');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload recepit and remark!');location.replace('OfflineSerivceReport.aspx');", true);
            }


        
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string RejectRequest(string fundid, string psaid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        if (psaid != "")
        {
            cls_myMember Clsm = new cls_myMember();
            string Txn = Clsm.Cyrus_GetTransactionID_New();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@EzulixTxnid", value = fundid });
            _lstparm.Add(new ParmList() { name = "@RefrenceNumber", value = psaid });
            _lstparm.Add(new ParmList() { name = "@action", value = "reject" });
            DataTable dmt = new DataTable();
            cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
            DataTable dtmem = new DataTable();

            List<ParmList> _lstparms = new List<ParmList>();
            _lstparms.Add(new ParmList() { name = "@EzulixTxnid", value = fundid.Trim() });
            _lstparms.Add(new ParmList() { name = "@action", value = "CheckD" });
            dtmem = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparms);
            if (dtmem.Rows.Count > 0)
            {
                string Narration = "Reverse Service Request TxnID:-" + fundid;
                string MemberId = dtmem.Rows[0]["MemberId"].ToString().Trim();
                string AdminMemberId = dtmem.Rows[0]["AdminMemberId"].ToString().Trim();
                string Amount = dtmem.Rows[0]["Amount"].ToString().Trim();
                string AdminAmount = dtmem.Rows[0]["AdminAmount"].ToString().Trim();
                Clsm.Wallet_MakeTransaction(MemberId, Convert.ToDecimal(Amount), "Cr", Narration);
            }
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }

    }

    [WebMethod]
    public static List<Customer> fillofflinereport()
    {
        cls_connection cls = new cls_connection();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberId = dtrow["MemberId"].ToString();
            cust.ServiceName = dtrow["ServiceName"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.Phone = dtrow["Phone"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.Address = dtrow["Address"].ToString();
            cust.CompanyName = dtrow["CompanyName"].ToString();
            cust.CompanyAddress = dtrow["CompanyAddress"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.TransID = dtrow["Ezulixtranid"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.RefrenceNumber = dtrow["RefrenceNumber"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> fillofflinereportbydate(string fromdate, string todate)
    {
        cls_connection cls = new cls_connection();


        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberId = dtrow["MemberId"].ToString();
            cust.ServiceName = dtrow["ServiceName"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.Phone = dtrow["Phone"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.Address = dtrow["Address"].ToString();
            cust.CompanyName = dtrow["CompanyName"].ToString();
            cust.CompanyAddress = dtrow["CompanyAddress"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.TransID = dtrow["Ezulixtranid"].ToString();
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.RefrenceNumber = dtrow["RefrenceNumber"].ToString();
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
            cls_connection cls = new cls_connection();
            DataTable dtExport = new DataTable();
            List<Customer> custList = new List<Customer>();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "admin" });
            _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("Sp_offlineservices_Report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "OfflineService_Report");
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
    public static string ShowDocs(string txnid)
    {
        Page page = (Page)HttpContext.Current.Handler as Page;
        GridView grd = (GridView)page.FindControl("grd");
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select Docs from tbl_offlineservices where Ezulixtranid='" + txnid + "'");
        if (dt.Rows.Count > 0)
        {
            string Jurisdiction = dt.Rows[0]["Docs"].ToString();
            string[] jurisdictionData = Jurisdiction.Split(',');
            grd.DataSource = jurisdictionData;
            grd.DataBind();
        }
        return "Test";
    }

    [WebMethod]
    public static List<Customer> loadofflinereceipt(string txnid)
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        int resellerid = Convert.ToInt32(HttpContext.Current.Session["resellerrt"]);
        _lstparm.Add(new ParmList() { name = "@Action", value = "loadofflinereceipt" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
        _lstparm.Add(new ParmList() { name = "@Id", value = resellerid });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.Id = dtrow["Id"].ToString();
            cust.ServiceName = dtrow["ServiceName"].ToString();
            cust.Status = "Success";
            cust.AddDate = dtrow["AddDate"].ToString();
            cust.Phone = dtrow["Phone"].ToString();
            cust.TransID = dtrow["Ezulixtranid"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.Address = dtrow["Address"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.logo = string.IsNullOrEmpty(Convert.ToString(dtrow["Companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtrow["Companylogo"]);
            custList.Add(cust);
        }
        return custList;
    }

    #region class
    public class Customer
    {
        public string MemberId { get; set; }
        public string ServiceName { get; set; }
        public string Id { get; set; }
        public string AddDate { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Status { get; set; }
        public string logo { get; set; }
        public string Amount { get; set; }
        public string TransID { get; set; }
        public string RefrenceNumber { get; set; }

    }

    #endregion

    private string uploadPanImage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Uploads/");
                string mpath = Server.MapPath("~/Uploads/");
                string spath = Server.MapPath("~/Uploads/");

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