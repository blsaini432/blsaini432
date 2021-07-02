using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Root_Admin_ViewCouponRequest : System.Web.UI.Page
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
    public static List<Customer> fillcouponreport()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_UTIcoupon_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.PSAId = dtrow["PSAId"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.referance = dtrow["referance"].ToString();
            cust.EzulixTranid = dtrow["EzulixTranid"].ToString();
            cust.NoofCoupon = dtrow["NoofCoupon"].ToString();
            cust.Adddate = dtrow["Adddate"].ToString();
            cust.Status = dtrow["Status"].ToString();
            cust.ID= dtrow["ID"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    public static List<Customer> fillcouponreportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
        dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_UTIcoupon_report", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.PSAId = dtrow["PSAId"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.referance = dtrow["referance"].ToString();
            cust.EzulixTranid = dtrow["EzulixTranid"].ToString();
            cust.NoofCoupon = dtrow["NoofCoupon"].ToString();
            cust.Adddate = dtrow["Adddate"].ToString();
            cust.ID = dtrow["ID"].ToString();
            cust.Status = dtrow["Status"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveUTIRequest(string fundid,string input)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        if (fundid != "" && input !="")
        {
            int id = Convert.ToInt32(fundid);
            DataTable dtt = new DataTable();
            dtt = cls.select_data_dt("select * from psa_couponPurchase where Status='Pending' and ID="+Convert.ToInt32(id)+"");
            if(dtt.Rows.Count > 0)
            {
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(fundid) });
                _lstparm.Add(new ParmList() { name = "@action", value = "Approve" });
                _lstparm.Add(new ParmList() { name = "@referance", value = input });
                DataTable dmt = new DataTable();
                dmt = cls.select_data_dtNew("sp_manageuticouponreg", _lstparm);
                cls.select_data_dt(@"EXEC PROC_UTI_PSA_Comission '" + dtt.Rows[0]["MemberId"].ToString() + "','" + dtt.Rows[0]["EzulixTranid"].ToString() + "'," + dtt.Rows[0]["Noofcoupon"].ToString() + "");
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
    public static string RejectUTIRequest(string fundid)
    {
        cls_connection cls = new cls_connection();
        cls_myMember clsm = new cls_myMember();
        string actions = "";
        if (fundid != "")
        {

            int id = Convert.ToInt32(fundid);
            DataTable dtt = new DataTable();
            dtt = cls.select_data_dt("select * from psa_couponPurchase where Status='Pending' and ID=" + Convert.ToInt32(id) + "");
            if (dtt.Rows.Count > 0)
            {
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(fundid) });
                _lstparm.Add(new ParmList() { name = "@action", value = "Reject" });
                DataTable dmt = new DataTable();
                dmt = cls.select_data_dtNew("sp_manageuticouponreg", _lstparm);
                clsm.Wallet_MakeTransaction(dtt.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(Convert.ToDecimal(dtt.Rows[0]["Amount"])), "Cr", "Reverse UTI Coupon TxnID:-" + dtt.Rows[0]["EzulixTranid"].ToString());
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

    #region class
    public class Customer
    {
        public string MemberID { get; set; }
        public string PSAId { get; set; }
        public string Amount { get; set; }
        public string EzulixTranid { get; set; }
        public string NoofCoupon { get; set; }
        public string Adddate { get; set; }
        public string ID { get; set; }
        public string Status { get; set; }
        public string  referance { get; set; }
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
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate)});
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            _lstparm.Add(new ParmList() { name = "@action", value = "admin" });
            dtExport = cls.select_data_dtNew("Set_Ezulix_UTIcoupon_report", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "ViewUTICoupon_Report");
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