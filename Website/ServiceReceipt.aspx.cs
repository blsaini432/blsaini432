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

public partial class ServiceReceipt : System.Web.UI.Page
{
    #region [Properties]
    DataTable dtHistory = new DataTable();
    public static string mssrno { get; set; }
    cls_connection cls = new cls_connection();
    string condition = " MsrNo > 0";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["txnid"] != null || Request.QueryString["id"] != null)
        {
            string txnid = Request.QueryString["txnid"].ToString();
           // string resellerid = Request.QueryString["id"].ToString();
            DataTable dtEWalletTransaction = new DataTable();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "loadofflinereceipt" });
            _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
           // _lstparm.Add(new ParmList() { name = "@Id", value = Convert.ToInt32(resellerid) });
            dtEWalletTransaction = cls.select_data_dtNew("Set_Ezulix_Ele_report", _lstparm);
            foreach (DataRow dtrow in dtEWalletTransaction.Rows)
            {
                lblServiceName.Text = dtrow["ServiceName"].ToString();
                lblStatus.Text = "Success";
                lblAddDate.Text = dtrow["AddDate"].ToString();
                lblPhone.Text = dtrow["Phone"].ToString();
                lbltransid.Text = dtrow["Ezulixtranid"].ToString();
                lblTransId1.Text = dtrow["Ezulixtranid"].ToString();
                lblAmount.Text = dtrow["Amount"].ToString();
            }
        }
     
    }
}