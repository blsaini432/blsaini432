using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.IO;
using System.Net;

public partial class Root_Distributor_Fast_TagPurchase : System.Web.UI.Page
{
    #region MyRegion
    cls_connection Cls = new cls_connection();
    clsState objState = new clsState();
    clsCity objCity = new clsCity();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtMember = (DataTable)Session["dtDistributor"];
            ViewState["MsrNo"] = null;
            ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"].ToString();
            Session["MemberId"] = dtMember.Rows[0]["MemberId"].ToString();
           
            DataTable dt = new DataTable();
            dt = Cls.select_data_dt("select * from tblmlm_membermaster where msrno='" + Convert.ToInt32(dtMember.Rows[0]["MsrNo"].ToString()) + "'");
            int packageid = Convert.ToInt32(dt.Rows[0]["PackageId"]);
            Session["memberid"] = dt.Rows[0]["MemberId"].ToString();
            DataTable dd = new DataTable();
            dd = Cls.select_data_dt("select * from [fasttag_feesettings] where PackageID='" + packageid + "'");
            if (dd.Rows.Count > 0)
            {
                int amount = Convert.ToInt32(dd.Rows[0]["Amount"]);
               lblamt.Text = amount.ToString();
            }
            else
            {
                lblamt.Text = "0.00";
            }

        }
    }



    #region Events



   

    #endregion
 

    protected void txt_couponbuy_TextChanged(object sender, EventArgs e)
    {

        decimal nocoupn = Convert.ToInt32(txt_couponbuy.Text);
        if (nocoupn > 0)
        {
            decimal fees = Convert.ToDecimal(lblamt.Text);
            decimal totalfees = nocoupn * fees;
            Label2.Visible = true;
            Label2.Text = totalfees.ToString();
        }

    }
    protected void btnpurchase_Click(object sender, EventArgs e)
    {
        if (lblamt.Text != "" && Label2.Text != "")
        {
            if (Convert.ToInt32(lblamt.Text) > 0 && Convert.ToInt32(Label2.Text) > 0)
            {
                cls_myMember clsm = new cls_myMember();
                string TxnID = clsm.Cyrus_GetTransactionID_New();
                int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Label2.Text), Convert.ToInt32(ViewState["MsrNo"]));
                if (result > 0)
                {
                    clsm.Wallet_MakeTransaction(Session["MemberId"].ToString(), -Convert.ToDecimal(Label2.Text), "Dr", "Fasttag Purchase with transactionid:-" + TxnID);
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(Label2.Text) });
                    _lstparm.Add(new ParmList() { name = "@EzulixTranid", value = TxnID });
                    _lstparm.Add(new ParmList() { name = "@RequestByMsrNo", value = Convert.ToInt32(ViewState["MsrNo"]) });
                    _lstparm.Add(new ParmList() { name = "@MemberId", value = Session["memberid"].ToString() });
                    _lstparm.Add(new ParmList() { name = "@Nooffasttag", value = Convert.ToInt32(txt_couponbuy.Text) });
                    _lstparm.Add(new ParmList() { name = "@Address", value = txt_delieveryaddress.Text });
                    _lstparm.Add(new ParmList() { name = "@action", value = 'I' });
                    Cls.select_data_dtNew("sp_FasttagPurchase", _lstparm);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('FastTagPurchaseReport.aspx');", true);
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in your wallet !');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Amount Should be greater than zero to proceed !');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Mandatory Fields are required');", true);
        }
    }

}


