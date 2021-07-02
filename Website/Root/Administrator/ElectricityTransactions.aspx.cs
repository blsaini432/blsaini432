using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Root_Admin_ElectricityTransactions : System.Web.UI.Page
{

    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public DataTable dtMemberMaster = new DataTable();
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
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(0);
            DataTable dtEWalletTransaction = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
            dtExport = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "ElectricityTransactions_Report");
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
    public static List<Customer> fillelectricityreport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(0) });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Member_Name = dtrow["Member_Name"].ToString();
            cust.servicename = dtrow["servicename"].ToString();
            cust.account_no = dtrow["account_no"].ToString();
            cust.trans_amt = dtrow["trans_amt"].ToString();
            cust.opr_id = dtrow["opr_id"].ToString();
            cust.agent_id = dtrow["agent_id"].ToString();
            cust.adm_com = dtrow["adm_com"].ToString();
            cust.cg = dtrow["cg"].ToString();
            cust.paydate = dtrow["paydate"].ToString();
            cust.statu = dtrow["statu"].ToString();
            cust.customername = dtrow["customername"].ToString();
            cust.duedate = dtrow["duedate"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> fillelectricityreportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.Member_Name = dtrow["Member_Name"].ToString();
            cust.servicename = dtrow["servicename"].ToString();
            cust.account_no = dtrow["account_no"].ToString();
            cust.trans_amt = dtrow["trans_amt"].ToString();
            cust.opr_id = dtrow["opr_id"].ToString();
            cust.agent_id = dtrow["agent_id"].ToString();
            cust.adm_com = dtrow["adm_com"].ToString();
            cust.cg = dtrow["cg"].ToString();
            cust.paydate = dtrow["paydate"].ToString();
            cust.statu = dtrow["statu"].ToString();
            cust.customername = dtrow["customername"].ToString();
            cust.duedate = dtrow["duedate"].ToString();
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
        public string opr_id { get; set; }
        public string agent_id { get; set; }
        public string adm_com { get; set; }
        public string cg { get; set; }
        public string paydate { get; set; }
        public string statu { get; set; }
        public string duedate { get; set; }
        public string customername { get; set; }
    }
    #endregion

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static string ApprovceRequest(string fundid, string bankid)
    //{
    //    cls_connection cls = new cls_connection();
    //    string actions = "";
    //    if (bankid != "")
    //    {
    //        string id = fundid;
    //        DataTable dt = new DataTable();
    //        dt = cls.select_data_dt("select * from Tbl_Ezulix_Ele where agent_id='" + id + "' and statu='Pending'");
    //        string status = string.Empty;
    //        string txnid = string.Empty;
    //        string totalamt = string.Empty;
    //        string memberid = string.Empty;
    //        string spkey = string.Empty;
    //        String Result = string.Empty;
    //        if (dt.Rows.Count > 0)
    //        {
    //            txnid = dt.Rows[0]["agent_id"].ToString();
    //            totalamt = dt.Rows[0]["trans_amt"].ToString();
    //            memberid = dt.Rows[0]["memberid"].ToString();
    //            spkey = dt.Rows[0]["sp_key"].ToString();
    //            cls_myMember clsm = new cls_myMember();
    //            cls.select_data_dt(@"Update Tbl_Ezulix_Ele set statu='SUCCESS', opr_id='" + bankid + "' Where  agent_id='" + id + "'");
    //            cls.select_data_dt(@"EXEC PROC_BBPS_ELE_COM_New @txnamount=" + Convert.ToDecimal(dt.Rows[0]["trans_amt"].ToString()) + ",@CMemberId='" + memberid.ToString() + "',@TxnId='" + txnid + "',@ServiceKey='" + spkey + "'");
    //            actions = "success";
    //            return actions;
    //        }
    //        else
    //        {
    //            return actions;
    //        }
    //    }
    //    else
    //    {
    //        return actions;
    //    }

    //}
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static string RejectRequest(string fundid)
    //{
    //    cls_connection cls = new cls_connection();
    //    string actions = "";
    //    string txn = fundid;
    //    DataTable dt = new DataTable();
    //    dt = cls.select_data_dt("select * from Tbl_Ezulix_Ele where agent_id='" + txn + "' and statu='Pending'");
    //    string status = string.Empty;
    //    string txnid = string.Empty;
    //    string totalamt = string.Empty;
    //    string memberid = string.Empty;
    //    String Result = string.Empty;
    //    if (dt.Rows.Count > 0)
    //    {
    //        txnid = dt.Rows[0]["agent_id"].ToString();
    //        totalamt = dt.Rows[0]["trans_amt"].ToString();
    //        memberid = dt.Rows[0]["memberid"].ToString();
    //        cls_myMember clsm = new cls_myMember();
    //        clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(totalamt)), "Cr", "Reverse Pay Bill TxnID:-" + txnid);
    //        cls.select_data_dt(@"Update Tbl_Ezulix_Ele set statu='Fail' Where  agent_id='" + txn + "'");
    //        actions = "success";
    //        return actions;
    //    }
    //    else
    //    {
    //        return actions;
    //    }
    //}


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
        _lstparm.Add(new ParmList() { name = "@Action", value = "client" });
        _lstparm.Add(new ParmList() { name = "@txnid", value = msrno });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string txnid = HttpContext.Current.Session["msrno"].ToString();
        dt = cls.select_data_dt("select * from Tbl_Ezulix_Ele where agent_id='" + txnid + "' and statu='Pending'");
        string status = string.Empty;
        string totalamt = string.Empty;
        string memberid = string.Empty;
        string spkey = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
            string adminstatus = txt_status.Text;
            string receipt = uploadPanImage(RECEPT);
            if (receipt != "" && adminstatus != "")
            {
                txnid = dt.Rows[0]["agent_id"].ToString();
                totalamt = dt.Rows[0]["trans_amt"].ToString();
                memberid = dt.Rows[0]["memberid"].ToString();
                spkey = dt.Rows[0]["sp_key"].ToString();
                cls_myMember clsm = new cls_myMember();
                cls.select_data_dt(@"Update Tbl_Ezulix_Ele set statu='SUCCESS',receipt='" + receipt + "', opr_id='" + adminstatus + "' Where  agent_id='" + txnid + "'");
                cls.select_data_dt(@"EXEC PROC_BBPS_ELE_COM_New @txnamount=" + Convert.ToDecimal(dt.Rows[0]["trans_amt"].ToString()) + ",@CMemberId='" + memberid.ToString() + "',@TxnId='" + txnid + "',@ServiceKey='" + spkey + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('ElectricityTransactions.aspx');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload recepit and remark!');location.replace('ElectricityTransactions.aspx');", true);
            }


        }
    }

    protected void btnSubmit_Reject(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string txnid = HttpContext.Current.Session["msrno"].ToString();
        dt = cls.select_data_dt("select * from Tbl_Ezulix_Ele where agent_id='" + txnid + "' and statu='Pending'");
        string status = string.Empty;
        string totalamt = string.Empty;
        string memberid = string.Empty;
        string spkey = string.Empty;
        String Result = string.Empty;
        if (dt.Rows.Count > 0)
        {
            string adminstatus = txt_status.Text;
            if (adminstatus != "")
            {

                txnid = dt.Rows[0]["agent_id"].ToString();
                totalamt = dt.Rows[0]["trans_amt"].ToString();
                memberid = dt.Rows[0]["memberid"].ToString();
                cls_myMember clsm = new cls_myMember();
                clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(totalamt)), "Cr", "Reverse Pay Bill TxnID:-" + txnid);
                cls.select_data_dt(@"Update Tbl_Ezulix_Ele set statu='Fail' Where  agent_id='" + txnid + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('ElectricityTransactions.aspx');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload remark!');location.replace('ElectricityTransactions.aspx');", true);
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

    //protected void gvEWalletTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "success")
    //    {
    //        string txn = e.CommandArgument.ToString();
    //        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
    //        TextBox txt = (TextBox)row.FindControl("txt_banktxn");
    //        DataTable dt = new DataTable();
    //        dt = cls.select_data_dt("select * from Tbl_Ezulix_Ele where agent_id='" + txn + "' and statu='Pending'");
    //        string status = string.Empty;
    //        string txnid = string.Empty;
    //        string totalamt = string.Empty;
    //        string memberid = string.Empty;
    //        string spkey = string.Empty;
    //        String Result = string.Empty;
    //        if (dt.Rows.Count > 0)
    //        {
    //            txnid = dt.Rows[0]["agent_id"].ToString();
    //            totalamt = dt.Rows[0]["trans_amt"].ToString();
    //            memberid = dt.Rows[0]["memberid"].ToString();
    //            spkey = dt.Rows[0]["sp_key"].ToString();
    //            cls_myMember clsm = new cls_myMember();
    //            cls.select_data_dt(@"Update Tbl_Ezulix_Ele set statu='SUCCESS', opr_id='" + txt.Text + "' Where  agent_id='" + txn + "'");
    //            cls.select_data_dt(@"EXEC PROC_BBPS_ELE_COM @Action='DIS',@CMemberId='" + memberid.ToString() + "',@TxnId='" + txnid + "',@ServiceKey='" + spkey + "'");
    //            fillEWalletTransaction(1);
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Updated Successfully!');", true);

    //        }
    //    }
    //    else if (e.CommandName == "Fail")
    //    {
    //        string txn = e.CommandArgument.ToString();
    //        GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
    //        TextBox txt = (TextBox)row.FindControl("txt_banktxn");
    //        DataTable dt = new DataTable();
    //        dt = cls.select_data_dt("select * from Tbl_Ezulix_Ele where agent_id='" + txn + "' and statu='Pending'");
    //        string status = string.Empty;
    //        string txnid = string.Empty;
    //        string totalamt = string.Empty;
    //        string memberid = string.Empty;
    //        String Result = string.Empty;
    //        if (dt.Rows.Count > 0)
    //        {
    //            txnid = dt.Rows[0]["agent_id"].ToString();
    //            totalamt = dt.Rows[0]["trans_amt"].ToString();
    //            memberid = dt.Rows[0]["memberid"].ToString();
    //            cls_myMember clsm = new cls_myMember();
    //            clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal(Convert.ToDecimal(totalamt)), "Cr", "Reverse Pay Bill TxnID:-" + txnid);
    //            cls.select_data_dt(@"Update Tbl_Ezulix_Ele set statu='Fail', opr_id='" + txt.Text + "' Where  agent_id='" + txn + "'");
    //            fillEWalletTransaction(1);
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Updated Successfully!');", true);
    //        }
    //        else
    //        {

    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Occured ! Please Contact to admin');", true);
    //        }
    //    }

    //}


}