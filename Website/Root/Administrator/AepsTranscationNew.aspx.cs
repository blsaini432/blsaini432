﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class Root_Admin_AepsTranscationNew : System.Web.UI.Page
{
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    public DataTable dtMemberMaster = new DataTable();
    cls_myMember Clsm = new cls_myMember();
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
    public static List<Customer> fillaepsbanktransaction()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(0) });
        _lstparm.Add(new ParmList() { name = "@rtype", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("AEPS_Transaction_New", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["memberid"].ToString();
            cust.Member_Name = dtrow["Member_Name"].ToString();
            cust.Custmer_Number = dtrow["Custmer_Number"].ToString();
            cust.order_id = dtrow["order_id"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Adm_Com = dtrow["Adm_Com"].ToString();
            cust.CG = dtrow["CG"].ToString();
            cust.creted = dtrow["creted"].ToString();
            cust.RESPONSE = dtrow["RESPONSE"].ToString();
            cust.RESP_MSG = dtrow["RESP_MSG"].ToString();
            cust.Aadhar = dtrow["Aadhar"].ToString();
            cust.RRN = dtrow["RRN"].ToString();
            custList.Add(cust);
        }
        return custList;
    }





    [WebMethod]
    public static List<Customer> fillaepsbanktransactionbydate(string fromdate, string todate)
    {
        int MsrNo = Convert.ToInt32(0);
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@rtype", value = "admin" });
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("AEPS_Transaction_New", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.memberid = dtrow["memberid"].ToString();
            cust.Member_Name = dtrow["Member_Name"].ToString();
            cust.Custmer_Number = dtrow["Custmer_Number"].ToString();
            cust.order_id = dtrow["order_id"].ToString();
            cust.Amount = dtrow["Amount"].ToString();
            cust.Adm_Com = dtrow["Adm_Com"].ToString();
            cust.CG = dtrow["CG"].ToString();
            cust.RESPONSE = dtrow["RESPONSE"].ToString();
            cust.RESP_MSG = dtrow["RESP_MSG"].ToString();
            cust.creted = dtrow["creted"].ToString();
            cust.Aadhar = dtrow["Aadhar"].ToString();
            cust.RRN = dtrow["RRN"].ToString();
            custList.Add(cust);
        }
        return custList;
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
            _lstparm.Add(new ParmList() { name = "@rtype", value = "admin" });
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("AEPS_Transaction_New", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "AEPSTransaction_Report");
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

    #region class
    public class Customer
    {
        public string memberid { get; set; }
        public string Member_Name { get; set; }
        public string Custmer_Number { get; set; }
        public string order_id { get; set; }
        public string Amount { get; set; }
        public string Adm_Com { get; set; }
        public string CG { get; set; }
        public string RESPONSE { get; set; }
        public string RESP_MSG { get; set; }
        public string creted { get; set; }
        public string Aadhar { get; set; }
        public string RRN { get; set; }
    }
    #endregion

}