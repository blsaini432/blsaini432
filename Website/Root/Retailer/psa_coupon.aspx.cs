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
using System.Configuration;
public partial class Root_Retailer_psa_coupon : System.Web.UI.Page
{
    #region MyRegion
    cls_connection Cls = new cls_connection();
    clsState objState = new clsState();
    clsCity objCity = new clsCity();
    public static string adminmemberid = ConfigurationManager.AppSettings["adminmemberid"];
    public static string adminurl = ConfigurationManager.AppSettings["adminurl"];
    public static string initial = ConfigurationManager.AppSettings["initial"];
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                ViewState["MsrNo"] = null;
                ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"].ToString();
                Session["MemberId"] = dtMember.Rows[0]["MemberId"].ToString();
                DataTable dtChk = new DataTable();
                List<ParmList> _lstparms = new List<ParmList>();
                _lstparms.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(dtMember.Rows[0]["MsrNo"].ToString()) });
                _lstparms.Add(new ParmList() { name = "@Action", value = "CheckStatus" });
                dtChk = Cls.select_data_dtNew("set_Psa_Reg", _lstparms);
                if (dtChk.Rows.Count > 0)
                {
                    if (dtChk.Rows[0]["Statu"].ToString() == "Approved")
                    {
                        txt_psaid.Text = dtChk.Rows[0]["PsaLoginId"].ToString();
                        txt_psapassword.Text = dtChk.Rows[0]["PsaLoginId"].ToString();

                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@PackageID", value = Convert.ToInt32(dtMember.Rows[0]["packageid"].ToString()) });
                        _lstparm.Add(new ParmList() { name = "@Action", value = "getpsafees" });
                        DataTable dt = new DataTable();
                        dt = Cls.select_data_dtNew("set_Psa_Reg", _lstparm);
                        if (dt.Rows.Count > 0)
                        {
                            decimal amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                            lblamt.Text = amount.ToString();
                        }
                        else
                        {
                            lblamt.Text = "0.00";
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your UTI Id is not activated yet!');window.location ='psa_reg.aspx';", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your UTI Id is not activated yet!');window.location ='psa_reg.aspx';", true);
                }
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }

    protected void btnpurchase_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtRetailer"] != null)
            {

                decimal value;
                if (Decimal.TryParse(txt_couponbuy.Text, out value))
                {
                    string amt = lblamt.Text;
                    if (Convert.ToDecimal(amt) > 0 && txt_psaid.Text != "")
                    {
                        cls_myMember clsm = new cls_myMember();
                        string TxnID = clsm.Cyrus_GetTransactionID_New();
                        DataTable dtMember = (DataTable)Session["dtRetailer"];
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@PackageID", value = Convert.ToInt32(dtMember.Rows[0]["packageid"].ToString()) });
                        _lstparm.Add(new ParmList() { name = "@Action", value = "getpsafees" });
                        DataTable dt = new DataTable();
                        dt = Cls.select_data_dtNew("set_Psa_Reg", _lstparm);
                        if (dt.Rows.Count > 0 && Convert.ToDecimal(dt.Rows[0]["Amount"]) > 0)
                        {
                            decimal nocoupn = Convert.ToInt32(txt_couponbuy.Text);
                            decimal amount = Convert.ToDecimal(dt.Rows[0]["Amount"]);
                            decimal totalfees = nocoupn * amount;
                            int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(totalfees), Convert.ToInt32(dtMember.Rows[0]["MsrNo"]));
                            if (result > 0)
                            {
                                int i = clsm.Wallet_MakeTransaction(dtMember.Rows[0]["MemberId"].ToString(), -Convert.ToDecimal(totalfees), "Dr", "PSA Coupon Purchase for psaloginid" + txt_psaid.Text + "and transactionid:-" + TxnID);
                                if (i > 0)

                                {
                                    //string opname = "Deductamount";
                                    //string Url = "https://ezulix.in/api/psaregcallback_app.aspx?AdminMemberId=" + adminmemberid + "&operationname=" + opname + "&noofcoupon=" + txt_couponbuy.Text + "&psaloginid=" + txt_psaid.Text + "";
                                    //WebClient wC = new WebClient();
                                    //wC.Headers.Add("User-Agent: Other");
                                    //string str = wC.DownloadString(Url);
                                    //if (str.Contains("successfully"))
                                    //{
                                        List<ParmList> _lstparms = new List<ParmList>();
                                        _lstparms.Add(new ParmList() { name = "@RequestByMsrNo", value = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]) });
                                        _lstparms.Add(new ParmList() { name = "@PSAId", value = txt_psaid.Text.Trim() });
                                        _lstparms.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(totalfees) });
                                        _lstparms.Add(new ParmList() { name = "@EzulixTranid", value = TxnID });
                                        _lstparms.Add(new ParmList() { name = "@MemberId", value = dtMember.Rows[0]["MemberId"].ToString() });
                                        _lstparms.Add(new ParmList() { name = "@Noofcoupon", value = Convert.ToInt32(txt_couponbuy.Text) });
                                        Cls.select_data_dtNew("set_Psa_couponpurchase", _lstparms);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                                        clear();
                                    //}
                                    //else
                                    //{
                                    //    clsm.Wallet_MakeTransaction(dtMember.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(totalfees), "Cr", " Reverse PSA Coupon Purchase for psaloginid" + txt_psaid.Text + "and transactionid:-" + TxnID);
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Occured !');", true);
                                    //}
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in your wallet !');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in your wallet !');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Fees Not Set Contact your admin!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Amount Should be greater than zero to proceed !');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Number Coupon Should be greater than zero!');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(" + ex.ToString() + ");", true);

        }
    }

    public void clear()
    {
        txt_couponbuy.Text = "";
        txt_psaid.Text = "";

    }
}


