using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Collections.Specialized;

public partial class Root_Admin_AddFund_PaymentGateway : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();

    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();

    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();
    string PaymentProof = "";
    string RequestStatus = "";
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["status"] != null)
            {
                if (Request.QueryString["status"].ToString() == "success")
                {
                    if (Session["tx"].ToString() == Request.QueryString["tx"].ToString() && Session["id"].ToString() == Request.QueryString["id"].ToString())
                    {
                        DataTable dtMember = (DataTable)Session["dtDistributor"];
                        objEWalletTransaction.EWalletTransaction(dtMember.Rows[0]["MemberID"].ToString(), Convert.ToDecimal(Session["Amount"]), "Cr", "Add cash via PG");
                        Session["id"] = null;
                        Session["tx"] = null;
                        Session["Amount"] = null;
                        Response.Redirect("ListEWalletTransaction.aspx?Personal=1");
                    }
                }
            }
        }
    }
    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Session["Amount"] = txtTotalAmount.Text;
        Response.Redirect("AddCash.aspx");
    }
    #endregion

    #region [Reset]
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();       
    }
    #endregion

    #region [All Functions]
    private void clear()
    {
        txtTotalAmount.Text="0.0";
    }
    #endregion
}