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

public partial class Root_Retailer_Licpremium : System.Web.UI.Page
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
                    DataTable dtMember = (DataTable)Session["dtDistributor"];
                    dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='LICSER'");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["LIC"]) == false)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Lic service is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                        }
                        else
                        {
                            ViewState["MemberId"] = null;
                            ViewState["MsrNo"] = null;
                            ViewState["dmtmobile"] = null;
                            ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                            ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                            ViewState["mobile"] = dtMember.Rows[0]["mobile"];
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Dmr Panel is not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                    }
                }
            }

            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error ');window.location ='DashBoard.aspx';", true);
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
                int Amount = Convert.ToInt32(txt_amount.Text.Trim());
                double NetAmount = TotupAmount(Convert.ToDouble(Amount), memberID);
                if (memberID != "" && Amount > 0)
                {
                    int result = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                    if (result > 0)
                    {
                        int MsrNo = Convert.ToInt32(ViewState["MsrNo"]);
                        List<ParmList> _list = new List<ParmList>();
                        _list.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
                        _list.Add(new ParmList() { name = "@memberID", value = memberID });
                        _list.Add(new ParmList() { name = "@ploicy_number", value = txt_userid.Text });
                        _list.Add(new ParmList() { name = "@Name", value = txt_cardname.Text });
                        //_list.Add(new ParmList() { name = "@DOB", value = txt_dob.Text });
                        //_list.Add(new ParmList() { name = "@policy_paylastdate", value = txt_date.Text });
                        //_list.Add(new ParmList() { name = "@email", value = txt_email.Text });
                        //_list.Add(new ParmList() { name = "@mobile", value = txt_mobile.Text });
                        _list.Add(new ParmList() { name = "@mode", value = DROPE.SelectedItem.ToString() });
                        _list.Add(new ParmList() { name = "@AMOUNT", value = Convert.ToDecimal(Amount) });
                        _list.Add(new ParmList() { name = "@Action", value = "I" });
                        string TxnID = Clsm.Cyrus_GetTransactionID_New();
                        _list.Add(new ParmList() { name = "@txn", value = TxnID });
                        DataTable dt = new DataTable();
                        dt = Cls.select_data_dtNew("Proc_LIC", _list);
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["ID"]) > 0)
                            {
                                string[] valueArray = new string[3];
                                valueArray[0] = txt_amount.Text;
                                valueArray[1] = txt_userid.Text;
                                valueArray[2] = txt_cardname.Text;
                                SendWithVarpan(ViewState["mobile"].ToString(), 1, valueArray);
                                Clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "Life Insurance Premium Request:'" + TxnID + "'");
                                //  Cls.select(@"EXEC ProcMLM__EWalletTransaction '" + ViewState["MemberId"].ToString() + "','" + NetAmount + "','lictxn:'"+TxnID+"','Cr'");
                                Cls.select_data_dt(@"EXEC ProcMLM__EWalletTransaction '" + ViewState["MemberId"].ToString() + "','" + NetAmount + "','Cr','Life Insurance Premium Commission:" + TxnID + "'");
                                // Cls.select_data_dt(@"EXEC_data_dEC ProcMLM__EWalletTransaction '" + ViewState["MemberId"].ToString() + "','" + NetAmount + "',Lic Premium Commission:" + TxnID + "','Cr'");
                                // Cls.select_data_dt(@"EXEC ProcMLM__EWalletTransaction @MemberId='" + ViewState["Memberid"].ToString() + "',@amount='" + NetAmount + "',@Factor='Cr',@Narration='Lic Premium Commission:" + TxnID+"'");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Successfull!');location.replace('licpremium_report.aspx');", true);
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
            dtsr = cls.select_data_dt(@"EXEC Set_EzulixDmr @action='LIC',@amount='" + Convert.ToDecimal(amount) + "',@packageid=" + PackageID + "");
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
        "Dear Customer, your Life Insurance Premium amount Rs.@v0@ and policy number @v1@ and policy Holder Name @v2@"//1
    };
}





