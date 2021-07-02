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

public partial class Root_DistributorFast_TagRecharge : System.Web.UI.Page
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
            DataTable dtMember = (DataTable)Session["dtRetailer"];
            ViewState["MsrNo"] = null;
            ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"].ToString();
            Session["MemberId"] = dtMember.Rows[0]["MemberId"].ToString();
         

        }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (ViewState["MsrNo"] != null)
        {
            if (Convert.ToDecimal(txt_rechargeamunt.Text) > 0 )
            {
                if (Convert.ToDecimal(txt_rechargeamunt.Text) >= 100)
                {

                    cls_myMember clsm = new cls_myMember();
                    string TxnID = clsm.Cyrus_GetTransactionID_New();
                    int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(txt_rechargeamunt.Text), Convert.ToInt32(ViewState["MsrNo"]));
                    if (result > 0)
                    {
                        clsm.Wallet_MakeTransaction(Session["MemberId"].ToString(), -Convert.ToDecimal(txt_rechargeamunt.Text), "Dr", "Fasttag Recharge with transactionid:-" + TxnID);
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@Mobile", value = txt_moible.Text });
                        _lstparm.Add(new ParmList() { name = "@Vehicle", value = txt_vehicle.Text.Trim() });
                        _lstparm.Add(new ParmList() { name = "@Bank", value = txt_bankname.Text.Trim() });
                        _lstparm.Add(new ParmList() { name = "@RechargeAmount", value = txt_rechargeamunt.Text.Trim() });
                        _lstparm.Add(new ParmList() { name = "@ezulixtranid", value = TxnID });
                        _lstparm.Add(new ParmList() { name = "@RequestByMsrNo", value = Convert.ToInt32(ViewState["MsrNo"]) });
                        _lstparm.Add(new ParmList() { name = "@MemberId", value = Session["memberid"].ToString() });
                        _lstparm.Add(new ParmList() { name = "@action", value = 'I' });
                        Cls.select_data_dtNew("sp_FasttagRecharge", _lstparm);
                        clear();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('FastTagRechargeReport.aspx');", true);
                      
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Wallet Balance!');", true);
                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Recharge amount should be minimum 100 RS');", true);
                }




            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Recharge amount should be greater than zero');", true);
            }
        }
    }


    public void clear()
    {
        txt_bankname.Text = txt_moible.Text = txt_rechargeamunt.Text = txt_vehicle.Text = "";
    }
   
}


