using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
public partial class UPI_callback : System.Web.UI.Page
{
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    string key = "4dc44306-9ab4-48f2-8176-676900de52ee";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["status"] == "success")
            {
                string txnid = Request.QueryString["txnid"];
                string client_txn_id = Request.QueryString["customerid"];
                string bankref_no = Request.QueryString["bankref_no"];
                string qrcode = Request.QueryString["qrcode"];
                string productinof = Request.QueryString["productinof"];
                string amount = Request.QueryString["amount"];
                DataTable orderid = cls.select_data_dt("select * from tbl_UPI_paymentGateway where client_txn_id='" + client_txn_id + "' and statuss='Pending'");
                if (orderid.Rows.Count > 0)
                {
                    string MemberId = orderid.Rows[0]["MemberId"].ToString();
                    string MemberTypeID = orderid.Rows[0]["MemberTypeID"].ToString();
                    List<ParmList> _lstparms = new List<ParmList>();
                    _lstparms.Add(new ParmList() { name = "@qrcode", value = qrcode });
                    _lstparms.Add(new ParmList() { name = "@Statuss", value = "success" });
                    _lstparms.Add(new ParmList() { name = "@upi_txn_id", value = bankref_no });
                    _lstparms.Add(new ParmList() { name = "@amount", value = amount });
                    _lstparms.Add(new ParmList() { name = "@client_txn_id", value = client_txn_id });
                    _lstparms.Add(new ParmList() { name = "@txnid", value = txnid });
                    _lstparms.Add(new ParmList() { name = "@Action", value = "hdfc" });
                    cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                    int balance = clsm.Wallet_MakeTransaction_Ezulix(MemberId, Convert.ToDecimal(amount), "Cr", "Add Fund UPI Payment Order ID : " + client_txn_id);
                    if (MemberTypeID == "5")
                    {
                        Response.Redirect("~/Root/Retailer/DashBoard.aspx");
                    }
                    else if (MemberTypeID == "4")
                    {
                        Response.Redirect("~/Root/Distributor/DashBoard.aspx");
                    }

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction Success !!');", true);
                }
                else
                {
                    //List<ParmList> _lstparms = new List<ParmList>();
                    //_lstparms.Add(new ParmList() { name = "@upi_txn_id", value = ds.Tables["data"].Rows[0]["upi_txn_id"].ToString() });
                    //_lstparms.Add(new ParmList() { name = "@Statuss", value = ds.Tables["data"].Rows[0]["status"].ToString() });
                    //_lstparms.Add(new ParmList() { name = "@remark", value = ds.Tables["data"].Rows[0]["remark"].ToString() });
                    //_lstparms.Add(new ParmList() { name = "@client_txn_id", value = client_txn_id });
                    //_lstparms.Add(new ParmList() { name = "@Action", value = "U" });
                    //cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparms);
                    //if (MemberTypeID == "5")
                    //{
                    //    Response.Redirect("~/Root/Retailer/DashBoard.aspx");
                    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction failure !!');", true);
                    //}
                    //else if (MemberTypeID == "4")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction failure !!');", true);
                    //    Response.Redirect("~/Root/Distributor/DashBoard.aspx");

                    //}

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transaction failure !!');", true);
            }
           
        }
    }

}
               