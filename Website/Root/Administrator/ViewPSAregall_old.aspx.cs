using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Portals_Admin_ViewPSAregall : System.Web.UI.Page
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
    public static List<Customer> fillutiregistration()
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
            cust.Name = dtrow["Name"].ToString();
            cust.Email = dtrow["Email"].ToString();
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
    public static List<Customer> fillutiregistrationbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
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
            cust.Name = dtrow["Name"].ToString();
            cust.Email = dtrow["Email"].ToString();
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
    }
    #endregion
}