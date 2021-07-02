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

public partial class Root_Administrator_Services : System.Web.UI.Page
{
    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region [Function]
    [WebMethod]
    public static List<Customer> fillservices()
    {
        DataTable dtEWalletBalance = new DataTable();
        clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetAllServices", 0);
        foreach (DataRow dtrow in dtEWalletBalance.Rows)
        {
            Customer cust = new Customer();
            cust.ServiceTypeName = dtrow["ServiceTypeName"].ToString();
            cust.IsActive = dtrow["IsActive"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion

    #region recharge
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updateservices(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        if (action == "OFF")
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "updateservices" });
            _lstparm.Add(new ParmList() { name = "@servicename", value = msrno });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(1) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        else
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "updateservices" });
            _lstparm.Add(new ParmList() { name = "@servicename", value = msrno });
            _lstparm.Add(new ParmList() { name = "@act", value = Convert.ToInt32(0) });
            dt = cls.select_data_dtNew("ProcMLM_ManageMemberMaster ", _lstparm);
            actions = "success";
        }
        return actions;
    }
    #endregion

    #region class
    public class Customer
    {
        public string ServiceTypeName { get; set; }
        public string IsActive { get; set; }
    }

    #endregion
}