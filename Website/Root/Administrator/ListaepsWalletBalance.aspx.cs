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
using BLL.MLM;

public partial class Root_Admin_ListRWalletBalance : System.Web.UI.Page
{
    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region [Function]
    [WebMethod]
    public static List<Customer> fillaepsWalletBalance()
    {
        DataTable dtEWalletBalance = new DataTable();
        clsMLM_RWalletBalance objEWalletBalance = new clsMLM_RWalletBalance();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dtEWalletBalance = objEWalletBalance.ManageRWalletBalance("GetAll", 0);
        foreach (DataRow dtrow in dtEWalletBalance.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.Debit = dtrow["Debit"].ToString();
            cust.Credit = dtrow["Credit"].ToString();
            cust.Balance = dtrow["Balance"].ToString();
            custList.Add(cust);
        }
        return custList;
    }
    #endregion

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string Balance { get; set; }

    }

    #endregion



}