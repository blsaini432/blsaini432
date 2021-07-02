using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Configuration;
using Common;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Services;

public partial class Root_Administrator_Viewallmember : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    clsState objState = new clsState();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_Universal objUniversal = new cls_Universal();
    DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    DataTable dtPackage = new DataTable();
    cls_connection cls = new cls_connection();
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    string condition = " msrno > 0";
    cls_myMember clsm = new cls_myMember();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //List<ParmList> _lstparm = new List<ParmList>();
        //DataTable dtmember = new DataTable();
        //_lstparm.Add(new ParmList() { name = "@rtype", value = "totalsuccrs" });
        //dtmember = cls.select_data_dtNew("totalmember", _lstparm);
        //foreach (DataRow dtrow in dtmember.Rows)
        //{
        //    HttpContext.Current.Session["totalamount1"] = dtrow["dis"].ToString();
        //    HttpContext.Current.Session["totalamount2"] = dtrow["rt"].ToString();
        //    HttpContext.Current.Session["totalamount3"] = dtrow["masdt"].ToString();
        //    HttpContext.Current.Session["totalamount4"] = dtrow["statet"].ToString();
        //}
    }
    [WebMethod]
    public static List<Customer> BindCustomers()
    {
        DataTable dt = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dt = objMemberMaster.ManageMemberMaster("GetByViewallnew", 0);
        foreach (DataRow dtrow in dt.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.Mobile = dtrow["Mobile"].ToString();
            cust.Isactive = dtrow["activedactivetext"].ToString();
            cust.MemberType = dtrow["MemberType"].ToString();
            cust.Owner = dtrow["Owner ID"].ToString();
            cust.Package = dtrow["PackageName"].ToString();
            cust.isaeps = dtrow["isaeps"].ToString();
            cust.isdmr = dtrow["isdmr"].ToString();
            cust.isaepspayout = dtrow["isaepspayout"].ToString();
            cust.isrecharge = dtrow["isrecharge"].ToString();
            cust.isemailverify = dtrow["isemailverify"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    [WebMethod]
    public static List<Customer> BindCustomersByMsrno(string msrno)
    {
        DataTable dt = new DataTable();
        List<Customer> custLists = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dt = objMemberMaster.ManageMemberMaster("GetBymsrno", Convert.ToInt32(msrno));
        foreach (DataRow dtrow in dt.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.StateName = dtrow["StateName"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.Mobile = dtrow["Mobile"].ToString();
            cust.CityName = dtrow["CityName"].ToString();
            cust.Address = dtrow["Address"].ToString();
            cust.TransactionPassword = dtrow["TransactionPassword"].ToString();
            cust.Password = dtrow["Password"].ToString();
            cust.bankac = dtrow["bankac"].ToString();
            cust.bankname = dtrow["bankname"].ToString();
            cust.bankifsc = dtrow["bankifsc"].ToString();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            custLists.Add(cust);
        }
        return custLists;
    }
    [WebMethod]
    public static List<Customer> BindCustomerServicesByMsrno(string msrno)
    {
        DataTable dt = new DataTable();
        List<Customer> custLists = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dt = objMemberMaster.ManageMemberMaster("GetServicesByMsrno", Convert.ToInt32(msrno));
        foreach (DataRow dtrow in dt.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberType = dtrow["MemberType"].ToString();
            cust.isuti = dtrow["isuti"].ToString();
            cust.isdmr = dtrow["isdmr"].ToString();
            cust.isxpressdmr = dtrow["isxpressdmr"].ToString();
            cust.isbbps = dtrow["isbbps"].ToString();
            cust.isbus = dtrow["isbus"].ToString();
            cust.isflight = dtrow["isflight"].ToString();
            cust.ishotel = dtrow["ishotel"].ToString();
            cust.isrecharge = dtrow["isrecharge"].ToString();
            cust.ispancard = dtrow["ispancard"].ToString();
            cust.isaeps = dtrow["isaeps"].ToString();
            cust.isprepaidcard = dtrow["isprepaidcard"].ToString();
            custLists.Add(cust);
        }
        return custLists;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updatedmr(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activedmr" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activedmr" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updateinstantdmr(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeinstantdmr" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeinstantdmr" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }

    #region recharge
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updaterecharge(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activerecharge" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activerecharge" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }
    #endregion



    #region updatebank
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updatebankdetails(string msrno, string bankac, string bankname, string bankifsc)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "updatebank" });
        _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
        _lstparm.Add(new ParmList() { name = "@bankac", value = bankac });
        _lstparm.Add(new ParmList() { name = "@bankname", value = bankname });
        _lstparm.Add(new ParmList() { name = "@bankifsc", value = bankifsc });
        dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
        actions = "success";
        return actions;
    }
    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updateaeps(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeaeps" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeaeps" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }





    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updateaepspayout(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeaepspayout" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeaepspayout" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updateservice(string msrno ,string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        if (action == "Active")
        {
            cls.select_data_dt(@"Update tblmlm_membermaster set isactive='0'  Where  msrno='" + msrno + "'");
        }
        else if(action == "Deactive")
        {
            cls.select_data_dt(@"Update tblmlm_membermaster set isactive='1'  Where  msrno='" + msrno + "'");
        }
        actions = "success";


        return actions;

    }


    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataTable dtExport = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dtExport = objMemberMaster.ManageMemberMaster("ExportAll", 0);
        if (dtExport.Rows.Count > 0)
        {

            Common.Export.ExportToExcel(dtExport, "AllMember_Report");

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);
        }
    }
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string MemberType { get; set; }
        public string Owner { get; set; }
        public string Package { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string TransactionPassword { get; set; }
        public string isuti { get; set; }
        public string isdmr { get; set; }
        public string isxpressdmr { get; set; }
        public string isbbps { get; set; }
        public string isbus { get; set; }
        public string isflight { get; set; }
        public string ishotel { get; set; }
        public string isrecharge { get; set; }
        public string ispancard { get; set; }
        public string isaeps { get; set; }
        public string isprepaidcard { get; set; }
        public string bankname { get; set; }
        public string bankifsc { get; set; }
        public string bankac { get; set; }
        public string isaepspayout { get; set; }
        public string isemailverify { get; set; }
        public string Isactive { get; set; }
    }

}