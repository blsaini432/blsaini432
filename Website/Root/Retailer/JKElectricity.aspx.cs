using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Root_Retailer_Downloadcard : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    PayOut PayOuts = new PayOut();
    private int checksumValue;
   
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtRetailer"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataTable dtMember = (DataTable)Session["dtRetailer"];
                    ViewState["MemberId"] = null;
                    ViewState["MsrNo"] = null;
                    ViewState["dmtmobile"] = null;
                    ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                    ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                }
            }

            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["MsrNo"] != null)
                {
                    int Msrno = Convert.ToInt32(ViewState["MsrNo"]);
                    DataTable dts = new DataTable();
                    dts = Cls.select_data_dt("select MemberID,MemberTypeID from tblmlm_membermaster where Msrno=" + Msrno + "");

                    string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                    string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);

                    int Amounts = Convert.ToInt32(txt_bill.Text);
                    int Amount = Amounts + 5;
                    if (memberID != "" && Amount > 0)
                    {
                        int result = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {
                            int MsrNo = Convert.ToInt32(ViewState["MsrNo"]);
                            List<ParmList> _list = new List<ParmList>();
                            _list.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
                            _list.Add(new ParmList() { name = "@memberID", value = memberID });
                            _list.Add(new ParmList() { name = "@Consumer", value = txt_userid.Text });
                            _list.Add(new ParmList() { name = "@Name", value = txt_cardname.Text });
                            _list.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(Amount) });
                            _list.Add(new ParmList() { name = "@Action", value = "I" });
                            string TxnID = Clsm.Cyrus_GetTransactionID_New();
                            _list.Add(new ParmList() { name = "@txnno", value = TxnID });
                            DataTable dt = new DataTable();
                            dt = Cls.select_data_dtNew("Proc_jkelectrictybills_new", _list);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["id"]) > 0)
                                {
                                    Clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "bill Request TxnID:-" + TxnID);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Successfull, Dear Partner You will received Payment Slip or Confirmation via Emails shortly.!');location.replace('JKElectricity.aspx');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in Wallet !');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in Wallet !');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Amount Not Set!');", true);
                }
            }
            // }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
            }
        }
    }


}

