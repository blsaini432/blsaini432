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
public partial class Root_Admin_LICPremium_Report : System.Web.UI.Page
{
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    string condition = " ewallettransactionid > 0";
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
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(1);
            DataTable dtExport = new DataTable();
            clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("sp_lic_trans", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "insurance_Report");
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
    #region [Function]
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillnewdmrreport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        int MsrNo = Convert.ToInt32(1);
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("sp_lic_trans", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["NAME"].ToString();
            cust.policy_number = dtrow["ploicy_number"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.DOB = dtrow["DOB"].ToString();
            cust.mode = dtrow["mode"].ToString();
            cust.TxnID = dtrow["txn"].ToString();
            cust.policy_paylastdate = dtrow["policy_paylastdate"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.adminstatus = dtrow["adminstatus"].ToString();
            custList.Add(cust);
        }
        return custList;
    }





    [WebMethod]
    public static List<Customer> fillnewdmrreportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(1);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("sp_lic_trans", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["Name"].ToString();
            cust.policy_number = dtrow["ploicy_number"].ToString();
            cust.mobile = dtrow["mobile"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.email = dtrow["email"].ToString();
            cust.DOB = dtrow["DOB"].ToString();
            cust.mode = dtrow["mode"].ToString();
            cust.TxnID = dtrow["txn"].ToString();
            cust.policy_paylastdate = dtrow["policy_paylastdate"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.Createdate = dtrow["Createdate"].ToString();
            cust.adminstatus = dtrow["adminstatus"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Customer> ApproveRequest(string appid)
    {
        int MsrNo = Convert.ToInt32(1);
        DataTable dt = new DataTable();
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        cls_connection cls = new cls_connection();
        string msrnoid = appid;
        HttpContext.Current.Session["msrno"] = msrnoid;
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = appid });
        dtEWalletTransaction = cls.select_data_dtNew("sp_licpremium_report_admin", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberName = dtrow["Name"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string RejectRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        string txn = fundid;
        DataTable dt = new DataTable();
        DataTable dts = new DataTable();
        dt = cls.select_data_dt("select * from tbl_lics where txn='" + txn + "' and adminstatus='pending'");
        string msrno = string.Empty;
        msrno = dt.Rows[0]["msrno"].ToString();
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
            string amounts = dt.Rows[0]["AMOUNT"].ToString();
            string memberid = dt.Rows[0]["memberID"].ToString();
            cls_myMember clsm = new cls_myMember();
            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(amounts)), "Cr", "Reverse Life Insurance  Premium TxnID:-" + txn);
            cls.select_data_dt(@"Update tbl_lics set status='Fail',adminstatus='Fail' Where txn='" + txn + "'");
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string id = HttpContext.Current.Session["msrno"].ToString();
        string adminstatus = txt_status.Text;
        string receipt = uploadPanImage(RECEPT);
        if (receipt != "" && adminstatus != "")
        {
            double NetAmount = 0;
            double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
            dt = cls.select_data_dt("select * from tbl_lics where txn='" + id + "' and adminstatus='pending'");
            string amount = dt.Rows[0]["AMOUNT"].ToString();
            string memberid = dt.Rows[0]["memberID"].ToString();
            string Msrno = dt.Rows[0]["Msrno"].ToString();
            DataTable dtsr = new DataTable();
            DataTable dtMemberMaster = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='chk', @msrno=" + Msrno + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='LIC',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
            if (dtsr.Rows.Count > 0)
            {
                surcharge_rate = Convert.ToDouble(dtsr.Rows[0]["surcharge"].ToString());
                isFlat = Convert.ToInt32(dtsr.Rows[0]["isflat"].ToString());
                if (surcharge_rate > 0)
                {
                    if (isFlat == 0)
                        surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
                    else
                        surcharge_amt = surcharge_rate;
                }
                NetAmount = surcharge_amt;
            }
            if (dt.Rows.Count > 0)
            {
                cls_myMember clsm = new cls_myMember();
                cls.select_data_dt(@"Update tbl_lics set status='SUCCESS',adminstatus='"+adminstatus+"',adminreceipt='"+ receipt + "' Where txn='" + id + "'");
                clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(NetAmount)), "Cr", "Life Insurance Premium Commission TxnID:-" + id);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('LICPremium_Report.aspx');", true);



            }
            else
            {

            }


        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please Upload Receipt!');location.replace('LICPremium_Report.aspx');", true);
        }
    }

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string policy_number { get; set; }
        public string policy_paylastdate { get; set; }
        public string Amount { get; set; }
        public string Name { get; set; }
        public string mode { get; set; }
        public string DOB { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string Status { get; set; }
        public string Createdate { get; set; }
        public string msg { get; set; }
        public string TxnID { get; set; }
        public string adminstatus { get; set; }
    }

    #endregion


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