using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using BLL.MLM;
using Newtonsoft.Json;
using System.Xml;
using System.IO;


public partial class Root_Retailer_AepsWallet : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    AEPS_Wallet Awallet = new AEPS_Wallet();
    clsMLM_RWalletTransaction objRWalletTransaction = new clsMLM_RWalletTransaction();
    DataTable dtExport = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dtm = new DataTable();
                dtm = (DataTable)Session["dtRetailer"];
                if (dtm.Rows.Count > 0)
                {
                    int msrno = Convert.ToInt32(dtm.Rows[0]["MsrNo"]);
                    filltabhome(msrno);
                }
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }

    #region Methods
    private void filltabhome(int msrno)
    {
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        DataTable dmm = objMemberMaster.ManageMemberMaster("GetBymsrno", Convert.ToInt32(msrno));
        if (dmm.Rows.Count > 0)
        {

            lblbankaccount.Text = dmm.Rows[0]["bankac"].ToString();
            lblbankifsc.Text = dmm.Rows[0]["bankifsc"].ToString();
            lblbankname.Text = dmm.Rows[0]["bankname"].ToString();
            DataTable dt = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(msrno) + ",@action='sum'");
            if (dt.Rows.Count > 0)
            {
                lbl_usebal.Text = "Rs." + dt.Rows[0]["usebal"].ToString();
                lbl_walletbal.Text = "Rs." + dt.Rows[0]["walletbal"].ToString();
                lbl_fzbal.Text = "Rs." + dt.Rows[0]["fzbal"].ToString();
            }
            else
            {

                lbl_usebal.Text = "Rs.0.00";
                lbl_walletbal.Text = "Rs.0.00";
                lbl_fzbal.Text = "Rs.0.00";
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }

    }

    #endregion

    protected void btn_transferbank_Click(object sender, EventArgs e)
    {
        if (Session["dtRetailer"] != null)
        {
            if (rbttransfertype.SelectedValue == "TB")
            {
                DataTable dtretailer = new DataTable();
                dtretailer = (DataTable)Session["dtRetailer"];
                if (dtretailer.Rows.Count > 0)
                {
                    if (Convert.ToString(dtretailer.Rows[0]["bankname"]) != "" || Convert.ToString(dtretailer.Rows[0]["bankifsc"]) != "" || Convert.ToString(dtretailer.Rows[0]["bankac"]) != "")
                    {
                        int msrno = Convert.ToInt32(dtretailer.Rows[0]["MsrNo"]);
                        DataTable dtck = Cls.select_data_dt(@"EXEC AEPS_Wallet @action='chkwd',@msrno=" + Convert.ToInt32(msrno) + "");
                        if (dtck.Rows.Count == 0)
                        {
                            DataTable dt = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(msrno) + ",@action='sum'");
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToDecimal(dt.Rows[0]["usebal"]) >= Convert.ToDecimal(txt_Amount.Text.Trim()))
                                {
                                    DataTable dtc = Cls.select_data_dt(@"EXEC AEPS_Wallet @action='chkwd',@msrno=" + Convert.ToInt32(msrno) + "");
                                    if (dtc.Rows.Count == 0)
                                    {
                                        if (Convert.ToString(dtretailer.Rows[0]["bankname"]) != null || Convert.ToString(dtretailer.Rows[0]["bankifsc"]) != null || Convert.ToString(dtretailer.Rows[0]["bankac"]) != null)
                                        {
                                            string txn = Clsm.Cyrus_GetTransactionID_New();
                                            List<ParmList> _listparam = new List<ParmList>();
                                            _listparam.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(dtretailer.Rows[0]["MsrNo"]) });
                                            _listparam.Add(new ParmList() { name = "@memberid", value = dtretailer.Rows[0]["MemberId"].ToString() });
                                            _listparam.Add(new ParmList() { name = "@bank", value = dtretailer.Rows[0]["bankname"].ToString() });
                                            _listparam.Add(new ParmList() { name = "@ifsc", value = dtretailer.Rows[0]["bankifsc"].ToString() });
                                            _listparam.Add(new ParmList() { name = "@ac", value = dtretailer.Rows[0]["bankac"].ToString() });
                                            _listparam.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(txt_Amount.Text.Trim()) });
                                            _listparam.Add(new ParmList() { name = "@txnid", value = txn });
                                            _listparam.Add(new ParmList() { name = "@action", value = "wd" });
                                            Cls.select_data_dtNew(@"AEPS_Wallet", _listparam);
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');window.location ='AepsWallet.aspx';", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Upload bank details first !');", true);

                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your one withdrawal request is under processing !');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transcation Amount could not be grater than Useable amount !');", true);
                                }
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your one withdrawal request is under processing !');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Upload bank details first !');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Bank Details not updated contact your admin');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select Correct Option');", true);
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }

    }

    protected void btn_transferwallet_Click(object sender, EventArgs e)
    {
        if (Session["dtRetailer"] != null)
        {
            if (rbttransfertype.SelectedValue == "TM")
            {
                DataTable ddd = new DataTable();
                ddd = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='wallett', @msrno=0");
                if (ddd.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(ddd.Rows[0]["iswallettransfer"]) == true)
                    {
                        DataTable dtretailer = new DataTable();
                        dtretailer = (DataTable)Session["dtRetailer"];
                        if (dtretailer.Rows.Count > 0)
                        {
                            int msrno = Convert.ToInt32(dtretailer.Rows[0]["MsrNo"]);
                            DataTable dt = Cls.select_data_dt(@"EXEC AEPS_Wallet @msrno=" + Convert.ToInt32(msrno) + ",@action='sum'");
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToDecimal(dt.Rows[0]["usebal"]) >= Convert.ToDecimal(txt_Amount.Text.Trim()))
                                {
                                    string Result = string.Empty;
                                    string txn = Clsm.Cyrus_GetTransactionID_New();
                                    int resaeps = Clsm.AEPSWallet_MakeTransaction_Ezulix(dtretailer.Rows[0]["MemberId"].ToString(), Convert.ToDecimal("-" + txt_Amount.Text.Trim()), "Dr", "E-Wallet Topup TXN:" + txn + "");
                                    if (resaeps == 1)
                                    {
                                        int resewallet = Clsm.Wallet_MakeTransaction_Ezulix(dtretailer.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(txt_Amount.Text.Trim()), "Cr", "AEPS Wallet Topup TXN:" + txn + "");
                                        if (resewallet == 1)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');window.location ='AepsWallet.aspx';", true);
                                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transcation Successfully !');window.location ='AepsWallet.aspx';", true);

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transcation Fail !');", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transcation Fail !');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transcation Amount could not be grater than Useable amount !');", true);
                                }
                            }
                        }



                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet Transfer Serivces is disabled, Please Try with another option');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Wallet Transfer Serivces is disabled, Please Try with another option');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select Correct Option');", true);
            }
        }

        else
        {
            Response.Redirect("~/userlogin.aspx");
        }
    }

    protected void rbttransfertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbttransfertype.SelectedValue == "TM")
        {
            btn_transferwallet.Visible = true;
            btn_transferbank.Visible = false;
            divbb.Visible = false;
        }
        else if (rbttransfertype.SelectedValue == "TB")
        {
            divbb.Visible = true;
            btn_transferwallet.Visible = false;
            btn_transferbank.Visible = true;
        }
    }
}