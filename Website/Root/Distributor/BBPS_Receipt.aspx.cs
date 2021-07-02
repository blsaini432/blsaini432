using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data; 
public partial class Root_Distributor_BBPS_Receipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["refidforreceipt"]!=null)
        {
            cls_connection cls = new cls_connection();
            DataTable dta = (DataTable)HttpContext.Current.Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dta.Rows[0]["MsrNo"]);
            DataTable dtEWalletTransaction = new DataTable();
         
             dtEWalletTransaction = cls.select_data_dt("select Website,Email,CompanyLogo from  tblcompany where CompanyId=2");
            if (dtEWalletTransaction.Rows.Count > 0)
            {
                imgcompanymini.Src = string.IsNullOrEmpty(Convert.ToString(dtEWalletTransaction.Rows[0]["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtEWalletTransaction.Rows[0]["companylogo"]);
               // lblemail.Text = dtEWalletTransaction.Rows[0]["Email"].ToString();
              //  lblat.Text = dtEWalletTransaction.Rows[0]["Website"].ToString();
            
            }
            string refno = Request.QueryString["refidforreceipt"].ToString();
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from Tbl_Ezulix_Ele_paytm  where agent_id='" + refno + "'");
            if (dt.Rows.Count > 0)
            {
                string customername = dt.Rows[0]["customername"].ToString();
                string account_no = dt.Rows[0]["account_no"].ToString();
                string billerid = dt.Rows[0]["billerid"].ToString();
                string billdate = dt.Rows[0]["paydate"].ToString();
                string Amount = dt.Rows[0]["trans_amt"].ToString();
                string transid = dt.Rows[0]["agent_id"].ToString();
                string rrefno = dt.Rows[0]["opr_id"].ToString();
                string status = dt.Rows[0]["statu"].ToString();
                string biloperator = dt.Rows[0]["biloperator"].ToString();
                string memberid = dt.Rows[0]["memberid"].ToString();
                string customermobile = dt.Rows[0]["customermobile"].ToString();
                lblcustomername.Text = customername;
                lblbillerid.Text = billerid;
                lblbiller_id.Text = billerid;
                lblbillereoperator.Text = biloperator;
                lbltxndate.Text = billdate;
                lbltxndatea.Text = billdate;
                lbltxnid.Text = transid;
                lblorder.Text = transid;
                lbltotalamount.Text = Amount;
                lblamount.Text = Amount;
                lbltotamount.Text = Amount;
             //   lblbilldate.Text = billdate;
                lblstatus.Text = status;
                lblservicefee.Text = account_no;
                lblmemberid.Text = memberid;
                lblcustomermobile.Text = customermobile;
                lbloprefno.Text = rrefno;
               // lblaccount.Text = account_no;
            }
        }
    }
}