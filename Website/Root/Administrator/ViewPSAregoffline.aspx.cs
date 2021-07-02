using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Portals_Admin_ViewPSAregoffline : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    DataTable dtEWalletTransaction = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
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
    public static List<Customer> fillpsareport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_UTIReg_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.PsaLoginId = dtrow["PsaLoginId"].ToString();
            cust.Contact_Number = dtrow["Contact_Number"].ToString();
            cust.Statu = dtrow["Statu"].ToString();
            cust.rejection = dtrow["rejection"].ToString();
            cust.Iden_Proof_Filename = dtrow["Iden_Proof_Filename"].ToString();
            cust.Addr_Proof_Filename = dtrow["Addr_Proof_Filename"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> fillpsareportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_UTIReg_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {

            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.Name = dtrow["Name"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.PsaLoginId = dtrow["PsaLoginId"].ToString();
            cust.Contact_Number = dtrow["Contact_Number"].ToString();
            cust.Statu = dtrow["Statu"].ToString();
            cust.rejection = dtrow["rejection"].ToString();
            cust.Iden_Proof_Filename = dtrow["Iden_Proof_Filename"].ToString();
            cust.Addr_Proof_Filename = dtrow["Addr_Proof_Filename"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion


    [WebMethod]
    public static List<Customer> ApprovePSARequest(string memberid)
    {
        cls_connection cls = new cls_connection();
        DataTable dt = new DataTable();
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        List<ParmList> _lstparm = new List<ParmList>();
        HttpContext.Current.Session["msrno"] = memberid;
        _lstparm.Add(new ParmList() { name = "@memberid", value = memberid });
        _lstparm.Add(new ParmList() { name = "@action", value = "reg" });
        DataTable dmt = new DataTable();
        dmt = cls.select_data_dtNew("set_Psa_Reg", _lstparm);
        foreach (DataRow dtrow in dmt.Rows)
        {
            Customer cust = new Customer();
            cust.Email = dtrow["Email"].ToString();
            custList.Add(cust);
        }
        return custList;

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string RejectPSARequest(string fundid, string rejection)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        if (rejection != "")
        {
            int id = Convert.ToInt32(fundid);
            cls_myMember Clsm = new cls_myMember();
            string Txn = Clsm.Cyrus_GetTransactionID_New();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(fundid) });
            _lstparm.Add(new ParmList() { name = "@rejection", value = rejection });
            _lstparm.Add(new ParmList() { name = "@action", value = "Reject" });
            DataTable dmt = new DataTable();
            dmt = cls.select_data_dtNew("sp_manageutireg", _lstparm);
            actions = "success";
            return actions;
        }
        else
        {
            return actions;
        }
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string memberid = HttpContext.Current.Session["msrno"].ToString();
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string remark = txt_Reject.Text;
        string psaid = txt_psaid.Text;
        if (psaid != "")
        {
            string txnid = HttpContext.Current.Session["msrno"].ToString();
            cls.select_data_dt(@"Update Tbl_Psa_Reg set Adminstatus='" + remark + "',psaloginid='" + psaid + "',statu='Approved'  Where  MemberID='" + memberid + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('ViewPSAregoffline.aspx');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload recepit and remark!');location.replace('ViewPSAregoffline.aspx');", true);
        }


    }


    protected void btnSubmit_Reject(object sender, EventArgs e)
    {
        string memberid = HttpContext.Current.Session["msrno"].ToString();
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string remark = txt_Reject.Text;
        string psaid = txt_psaid.Text;
        if (remark != "")
        {
            string txnid = HttpContext.Current.Session["msrno"].ToString();
            cls.select_data_dt(@"Update Tbl_Psa_Reg set rejection='" + remark + "',psaloginid='" + psaid + "',statu='Rejected'  Where  memberid='" + memberid + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Status Update Successfull!');location.replace('ViewPSAregoffline.aspx');", true);

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please upload remark!');location.replace('ViewPSAregoffline.aspx');", true);
        }

    }


    #region class
    public class Customer
    {
        public string MemberID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact_Number { get; set; }
        public string Statu { get; set; }
        public string rejection { get; set; }
        public string Iden_Proof_Filename { get; set; }
        public string Addr_Proof_Filename { get; set; }
        public string MsrNo { get; set; }
        public string PsaLoginId { get; set;}
    }


    protected void btn_export_Click(object sender, EventArgs e)
    {
        string fromdate = hdnfromdate.Value;
        string todate = hdntodate.Value;
        if (fromdate != "" && todate != "")
        {
            int MsrNo = Convert.ToInt32(0);
            DataTable dtExport = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
            dtExport = cls.select_data_dtNew("Set_Ezulix_UTIReg_report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "ViewPSA_Report");
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
    #endregion
}