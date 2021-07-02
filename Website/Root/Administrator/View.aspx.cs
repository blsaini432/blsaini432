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

public partial class Root_Administrator_View : System.Web.UI.Page
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
    public static List <Customer>BindCustomers()
    {
        DataTable dt = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dt = objMemberMaster.ManageMemberMaster("GetByViewallnew", 0);
        foreach (DataRow dtrow in dt.Rows)
        {
            Customer cust = new Customer();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.Email = dtrow["Email"].ToString();
            cust.Mobile = dtrow["Mobile"].ToString();
            cust.MemberType = dtrow["MemberType"].ToString();
            cust.Owner = dtrow["Owner ID"].ToString();
            cust.Package = dtrow["PackageName"].ToString();
            custList.Add(cust);
        }
        return custList;
    }


    public class Customer
    {
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string MemberType { get; set; }
        public string Owner { get; set; }
        public string Package { get; set; }
        //public string MemberType { get; set; }
        //public string PackageName { get; set; }
    }


    public static string Get()
    {
        string userid = "nk";
        string pwd = "123";
        string resp = userid + "" + pwd;
        JsonSerializer ser = new JsonSerializer();
        string jsonresp = JsonConvert.SerializeObject(resp);
        return resp;
    }

}