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

public partial class Root_Admin_Bus_History : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    DataTable dtExport = new DataTable();

    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();

    clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();

    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();

    cls_connection cls = new cls_connection();

    string condition = " SerialNo > 0";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmployeeID"] != null || Session["dtEmployee"] != null)
            {

                if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                }
            }
            else
            {
                Response.Redirect("~/adminlogin.aspx");
            }
        }
    }



    //#region [Refund]
    //        if (e.CommandName == "refund")
    //        {
    //            try
    //            {
    //                int idno = 0;
    //    idno = Convert.ToInt32(e.CommandArgument);
    //                int i = 0;
    //    i = cls.select_data_scalar_int("Exec Cyrus_Recharge_Refund '" + e.CommandArgument.ToString() + "'");
    //                if (i > 0)
    //                {
    //                    fillHistory();
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Successfully Refunded !');", true);
    //                }
    //                else
    //                {
    //                    fillHistory();
    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error !! Not Refunded !');", true);
    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Successfully Refunded !');", true);
    //}
    //            }
    //            catch (Exception ex)
    //            {
    //                Function.MessageBox(ex.Message);
    //            }
    //        }




    #region [Export To Excel/Word/Pdf]
    protected void btnexportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = (DataTable)ViewState["dtExport"];
            if (dtExport.Rows.Count > 0)
            {
                dtExport.Columns.Remove("IsDelete");
                dtExport.Columns.Remove("IsActive");
                Common.Export.ExportToExcel(dtExport, "Bus_Report");
            }
        }
        catch
        { }

    }
    #endregion

    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillrechargereport()
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "ADMIN" });
        _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("BUS_History", _lstparm);
        //ViewState["dtExport"] = dtHistory.DefaultView.ToTable();
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["byMsrNo"].ToString();
           
            cust.MobileNo = dtrow["Mobile"].ToString();
            cust.transactionid = dtrow["transactionid"].ToString();
            cust.pickuplocationaddress = dtrow["pickuplocationaddress"].ToString();
            cust.seatname = dtrow["seatname"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.droplocation = dtrow["droplocation"].ToString();
            cust.droptime = dtrow["droptime"].ToString();
            cust.pnrno = dtrow["pnrno"].ToString();
            cust.bookingstatus = dtrow["bookingstatus"].ToString();
            cust.travelername = dtrow["travelername"].ToString();
            cust.Updatedatetime = dtrow["Updatedatetime"].ToString();
            cust.pemail = dtrow["pemail"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    [WebMethod]
    public static List<Customer> fillrechargereportbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "ADMIN" });
        _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("BUS_History", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["byMsrNo"].ToString();
          
            cust.MobileNo = dtrow["Mobile"].ToString();
            cust.transactionid = dtrow["transactionid"].ToString();
            cust.pickuplocationaddress = dtrow["pickuplocationaddress"].ToString();
            cust.seatname = dtrow["seatname"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.droplocation = dtrow["droplocation"].ToString();
            cust.droptime = dtrow["droptime"].ToString();
            cust.pnrno = dtrow["pnrno"].ToString();
            cust.bookingstatus = dtrow["bookingstatus"].ToString();
            cust.travelername = dtrow["travelername"].ToString();
            cust.Updatedatetime = dtrow["Updatedatetime"].ToString();
            cust.pemail = dtrow["pemail"].ToString();
            custList.Add(cust);
        }
        return custList;
    }



    [WebMethod]
    public static List<Customer> BindRechargeRE(string msrno)
    {
        DataTable dt = new DataTable();
        List<Customer> custLists = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dt = objMemberMaster.ManageMemberMaster("GetBymsrno", Convert.ToInt32(msrno));
        foreach (DataRow dtrow in dt.Rows)
        {
            Customer cust = new Customer();
            //cust.MemberID = dtrow["MemberID"].ToString();
            //cust.StateName = dtrow["StateName"].ToString();
            //cust.Email = dtrow["Email"].ToString();
            //cust.Mobile = dtrow["Mobile"].ToString();
            //cust.CityName = dtrow["CityName"].ToString();
            //cust.Address = dtrow["Address"].ToString();
            //cust.TransactionPassword = dtrow["TransactionPassword"].ToString();
            //cust.Password = dtrow["Password"].ToString();
            //cust.bankac = dtrow["bankac"].ToString();
            //cust.bankname = dtrow["bankname"].ToString();
            //cust.bankifsc = dtrow["bankifsc"].ToString();
            //cust.MsrNo = dtrow["MsrNo"].ToString();
            custLists.Add(cust);
        }
        return custLists;
    }

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MobileNo { get; set; }
        public string pnrno { get; set; }
        public string transactionid { get; set; }
        public string bookingstatus { get; set; }
        public string booking_id { get; set; }
        public string Amount { get; set; }
        public string pickuplocationaddress { get; set; }
      
        public string seatname { get; set; }
        public string travelername { get; set; }
        public string droplocation { get; set; }
        public string pemail { get; set; }
        public string droptime { get; set; }
        public string Updatedatetime { get; set; }
    }

    #endregion

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
            _lstparm.Add(new ParmList() { name = "@Action", value = "ADMIN" });
            _lstparm.Add(new ParmList() { name = "@ID", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("BUS_History", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "Bus_Report");
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
}


