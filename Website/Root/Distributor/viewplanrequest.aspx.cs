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

public partial class root_dis_viewplanrequest : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {

                if (txt_fromdate.Text.Trim() == "" || txttodate.Text.Trim() == "")
                {
                    txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                    txt_fromdate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
                }
                DataTable dtmembermaster = new DataTable();
                dtmembermaster = (DataTable)Session["dtDistributor"];
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    [WebMethod]
    public static List<Customer> fillplanrequest()
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@fromdate", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        _lstparm.Add(new ParmList() { name = "@todate", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
        dtEWalletTransaction = cls.select_data_dtNew("sp_getmemberplanrequest_member", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberId = dtrow["MemberId"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.membertype = dtrow["membertype"].ToString();
            cust.idpurchase = dtrow["idpurchase"].ToString();
            cust.amount = dtrow["amount"].ToString();
            cust.TranId = dtrow["TranId"].ToString();
            cust.ActiveStatus = dtrow["ActiveStatus"].ToString();
            cust.RequestDate = dtrow["RequestDate"].ToString();
            cust.ActiveDate = dtrow["ActiveDate"].ToString();
            cust.mm = dtrow["mm"].ToString();
            cust.requestbymsrno= dtrow["requestbymsrno"].ToString();
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
            DataTable dtExport = new DataTable();
            List<Customer> custList = new List<Customer>();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
            _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
            _lstparm.Add(new ParmList() { name = "@fromdate", value = changedatetommddyy(fromdate) });
            _lstparm.Add(new ParmList() { name = "@todate", value = changedatetommddyy(todate) });
            dtExport = cls.select_data_dtNew("sp_getmemberplanrequest_member", _lstparm);
            if (dtExport.Rows.Count > 0)
            {
                Common.Export.ExportToExcel(dtExport, "MemberPlanReqest_Report");
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
    [WebMethod]
    public static List<Customer> fillplanrequestbydate(string fromdate, string todate)
    {
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        _lstparm.Add(new ParmList() { name = "@msrno", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@fromdate", value = changedatetommddyy(fromdate) });
        _lstparm.Add(new ParmList() { name = "@todate", value = changedatetommddyy(todate) });
        dtEWalletTransaction = cls.select_data_dtNew("sp_getmemberplanrequest_member", _lstparm);
        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MemberId = dtrow["MemberId"].ToString();
            cust.MemberName = dtrow["MemberName"].ToString();
            cust.membertype = dtrow["membertype"].ToString();
            cust.idpurchase = dtrow["idpurchase"].ToString();
            cust.amount = dtrow["amount"].ToString();
            cust.TranId = dtrow["TranId"].ToString();
            cust.ActiveStatus = dtrow["ActiveStatus"].ToString();
            cust.RequestDate = dtrow["RequestDate"].ToString();
            cust.ActiveDate = dtrow["ActiveDate"].ToString();
            cust.mm = dtrow["mm"].ToString();
            cust.requestbymsrno = dtrow["requestbymsrno"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ApproveRequest(string fundid, string bankid,string eztxnid,string remainingcount,string pric)
    {
        cls_connection cls = new cls_connection();
        string actions = "";
        if (bankid != "" && eztxnid!="" && remainingcount!="" && pric!="")
        {
            string price = pric;
            decimal prc = Convert.ToDecimal(price);
            int prcm = Convert.ToInt32(prc);
            string Remaningcount = remainingcount;
            string txnid = eztxnid;
            int idno = 0;
            idno = Convert.ToInt32(fundid);
            int membertype = Convert.ToInt32(bankid);
            DataTable dtmember = new DataTable();

            DataTable dd = new DataTable();
            dd = cls.select_data_dt("select * from tblmlm_memberplans_adminreq where ActiveStatus='Pending' and TranId='"+ txnid + "'");
            if (dd.Rows.Count > 0)
            {
                dtmember = cls.select_data_dt("select * from tblmlm_membermaster where msrno='" + idno + "'");
                if (dtmember.Rows.Count > 0)
                {
                    int parentmsrno = 1;
                    if (parentmsrno == 1)
                    {
                        DataTable dt = new DataTable();
                        dt = cls.select_data_dt("select Remaningcount from tblmlm_memberplans_adminapprove where membertype='" + membertype + "' and msrno='" + idno + "' and isactive=1");
                        if (dt.Rows.Count > 0)
                        {
                            int count = Convert.ToInt32(dt.Rows[0]["Remaningcount"].ToString());
                            int sum = count + Convert.ToInt32(Remaningcount);
                            cls.update_data("update tblmlm_memberplans_adminreq set ActiveStatus='Approved' ,ActiveDate='" + DateTime.Now + "',IsActive=1 where requestbymsrno='" + idno + "' and TranId='" + txnid + "'");
                            cls.update_data("update tblmlm_memberplans_adminapprove set Remaningcount='" + sum + "',Lastmodifieddate='" + DateTime.Now + "'  where MsrNo='" + idno + "'  and membertype='" + membertype + "'");
                            actions = "success";
                            return actions;
                        }
                        else
                        {
                            cls.update_data("insert into tblmlm_memberplans_adminapprove(membertype,MsrNo,Remaningcount,isactive,Lastmodifieddate)values('" + membertype + "','" + idno + "','" + Remaningcount + "',1,'" + DateTime.Now + "')");
                            cls.update_data("update tblmlm_memberplans_adminreq set ActiveStatus='Approved' ,ActiveDate='" + DateTime.Now + "',IsActive=1 where requestbymsrno='" + idno + "' and TranId='" + txnid + "'");
                            actions = "success";
                            return actions;

                        }
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
        public string MemberId { get; set; }
        public string MemberName { get; set; }
        public string membertype { get; set; }
        public string idpurchase { get; set; }
        public string amount { get; set; }
        public string TranId { get; set; }
        public string ActiveStatus { get; set; }
        public string RequestDate { get; set; }
        public string ActiveDate { get; set; }
        public string requestbymsrno { get; set; }
        public string mm { get; set; }

    }
    #endregion



}
