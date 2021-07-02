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

public partial class Root_Retailer_Franchise : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();

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
                    ViewState["mobile"] = dtMember.Rows[0]["mobile"];
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
        try
        {
            if (ViewState["MsrNo"] != null)
            {
                int Msrno = Convert.ToInt32(ViewState["MsrNo"]);
                DataTable dts = new DataTable();
                dts = Cls.select_data_dt("select MemberID,MemberTypeID from tblmlm_membermaster where Msrno=" + Msrno + "");
                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                int MsrNo = Convert.ToInt32(ViewState["MsrNo"]);
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@msrno", value = MsrNo });
                _list.Add(new ParmList() { name = "@memberID", value = memberID });
                _list.Add(new ParmList() { name = "@Name", value = txt_cardname.Text });
                _list.Add(new ParmList() { name = "@mobile", value = txt_mobiles.Text });
                _list.Add(new ParmList() { name = "@education", value = DROPE.SelectedItem.ToString() });
                _list.Add(new ParmList() { name = "@email", value = txt_email.Text });
                _list.Add(new ParmList() { name = "@pin", value = txt_pin.Text });
                _list.Add(new ParmList() { name = "@area", value = txt_area.Text });
                _list.Add(new ParmList() { name = "@age", value = txt_age.Text });
                _list.Add(new ParmList() { name = "@express", value = txt_exp.Text });
                _list.Add(new ParmList() { name = "@abouts", value = drop_abouts.Text });
                _list.Add(new ParmList() { name = "@amazon", value = drop_amazon.Text });
                _list.Add(new ParmList() { name = "@business", value = drop_business.Text });
                _list.Add(new ParmList() { name = "@address", value = txt_address.Text });
                _list.Add(new ParmList() { name = "@message", value = txt_message.Text });
                _list.Add(new ParmList() { name = "@Action", value = "I" });
                string TxnID = Clsm.Cyrus_GetTransactionID_New();
                _list.Add(new ParmList() { name = "@txn", value = TxnID });
                DataTable dt = new DataTable();
                dt = Cls.select_data_dtNew("Proc_Franchis", _list);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["ID"]) > 0)
                    {

                        //Clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "Credit_card Bill Payment Request:'" + TxnID + "'");
                        //Cls.select_data_dt(@"EXEC ProcMLM__EWalletTransaction '" + ViewState["MemberId"].ToString() + "','" + NetAmount + "','Cr','Credit Card Commission:" + TxnID + "'");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Successfull!');location.replace('Franchise_report.aspx');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
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
    public double TotupAmount(double amount, string memberid)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0; int isFlat = 0;
        if (amount > 0)
        {
            DataTable dtsr = new DataTable();
            cls_connection cls = new cls_connection();
            DataTable dtMemberMaster = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='chk', @msrno=" + Convert.ToInt32(ViewState["MsrNo"]) + "");
            string PackageID = dtMemberMaster.Rows[0]["packageid"].ToString();
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='CRE',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
            if (dtsr.Rows.Count > 0)
            {
                surcharge_rate = Convert.ToDouble(dtsr.Rows[0]["surcharge"].ToString());
                isFlat = Convert.ToInt32(dtsr.Rows[0]["isflat"].ToString());
                if (surcharge_rate > 0)
                {
                    if (isFlat == 0)
                        surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
                    else
                        surcharge_amt = surcharge_rate;
                }
                NetAmount = surcharge_amt;
            }
            else
            {
                NetAmount = 0;
            }
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }
    public static void SendWithVarpan(string Mobile, int Template, string[] ValueArray)
    {
        try
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string smsMessage = GetString(Template, ValueArray);
            string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=225506ABUcOJxsJHy5b45b4e0&route=4&sender=PYDEER&mobiles=" + Mobile + "& message=" + smsMessage + "";
            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();
        }
        catch (Exception ex)
        {

        }
    }
    public static string GetString(int Template, string[] ValueArray)
    {
        string fileData = arrTemplate[Template];
        if ((ValueArray == null))
        {
            return fileData;
        }
        else
        {
            for (int i = ValueArray.GetLowerBound(0); i <= ValueArray.GetUpperBound(0); i++)
            {
                fileData = fileData.Replace("@v" + i.ToString() + "@", (string)ValueArray[i]);
            }
            return fileData;
        }
    }

    public static string[] arrTemplate = new string[]
    {
        "Zero",
        "Dear Customer, your Credit Card Bill Payment  amount Rs.@v0@ and Credit Card Number @v1@. and Card Holder Name @v2@"//1
    };
}




