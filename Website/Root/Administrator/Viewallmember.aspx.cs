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
using System.Text;

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
            cust.MemberType = dtrow["MemberType"].ToString();
            cust.Owner = dtrow["Owner ID"].ToString();
            cust.Package = dtrow["PackageName"].ToString();
            cust.isaeps = dtrow["isaeps"].ToString();
            cust.isdmr = dtrow["isdmr"].ToString();
            cust.isaepspayout = dtrow["isaepspayout"].ToString();
            cust.isrecharge = dtrow["isrecharge"].ToString();
            cust.isemailverify = dtrow["isemailverify"].ToString();
            cust.status = dtrow["activedactivetext"].ToString();
            cust.isuti = dtrow["isuti"].ToString();
            cust.New_BBPS = dtrow["New_BBPS"].ToString();
            cust.CentralKYC = dtrow["CentralKYC"].ToString();
           // cust.CentralKYC1 = dtrow["CentralKYC1"].ToString();
            cust.isxpresspayout = dtrow["isxpresspayout"].ToString();
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
    public static List<VideoKYC> BindKYCByMsrno(string msrno)
    {
        DataTable dt = new DataTable();
        List<VideoKYC> custLists = new List<VideoKYC>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dt = objMemberMaster.ManageMemberMaster("GetKYC", Convert.ToInt32(msrno));
        foreach (DataRow dtrow in dt.Rows)
        {
            VideoKYC cust = new VideoKYC();
            cust.Address = dtrow["Address"].ToString();
            cust.address1 = dtrow["Address1"].ToString();
            cust.agentstatus = dtrow["agentstatus"].ToString();
            cust.D_DOB = dtrow["D_DOB"].ToString();
            cust.D_documentnumber = dtrow["D_documentnumber"].ToString();
            cust.D_FatherName = dtrow["D_FatherName"].ToString();
            cust.D_Name = dtrow["D_Name"].ToString();
            cust.Gender = dtrow["Gender"].ToString();
            cust.KycDate = dtrow["KycDate"].ToString();
            cust.kycimage1 = "data:image/jpeg;base64," + dtrow["kycimage1"].ToString();
            cust.kycimage2 = "data:image/jpeg;base64," + dtrow["kycimage2"].ToString();
            cust.livepic = "data:image/jpeg;base64," + dtrow["livepic"].ToString();
            cust.orignalpic = "data:image/jpeg;base64," + dtrow["orignalpic"].ToString();
            cust.Mobile = dtrow["Mobile"].ToString();
            cust.mobile1 = dtrow["mobile1"].ToString();
            cust.O_DOB = dtrow["O_DOB"].ToString();
            cust.O_documentnumber = dtrow["O_documentnumber"].ToString();
            cust.O_FatherName = dtrow["O_FatherName"].ToString();
            cust.O_Name = dtrow["O_Name"].ToString();
            cust.status = dtrow["status"].ToString();
            cust.Email = dtrow["Email"].ToString();
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
            cust.New_BBPS = dtrow["New_BBPS"].ToString();
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
    public static string updatecentralkyc(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activecentralkyc" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activecentralkyc" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updatexpresspayout(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activexpresspayout" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activexpresspayout" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updateuti(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeuti" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeuti" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ServiceBBPS(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activebbps" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activebbps" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updateaccount(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "Deactive")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeaccount" });
            _lstparm.Add(new ParmList() { name = "@ID", value = Convert.ToInt32(msrno) });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "activeaccount" });
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
        public string CentralKYC1 { get; set; }
        public string CentralKYC { get; set; }
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
        public string isxpresspayout { get; set; }
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
        public string status { get; set; }
        public string New_BBPS { get; set; }

    }

    public class VideoKYC
    {
        public string orignalpic { get; set; }
        public string livepic { get; set; }
        public string status { get; set; }
        public string agentstatus { get; set; }
        public string kycimage1 { get; set; }
        public string kycimage2 { get; set; }
        public string O_documentnumber { get; set; }
        public string O_Name { get; set; }
        public string O_FatherName { get; set; }
        public string O_DOB { get; set; }

        public string D_documentnumber { get; set; }
        public string D_Name { get; set; }
        public string D_FatherName { get; set; }
        public string D_DOB { get; set; }
        public string Mobile { get; set; }
        public string mobile1 { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string KycDate { get; set; }
        public string address1 { get; set; }
        public string Email { get; set; }

    }
}