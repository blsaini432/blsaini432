using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Configuration;
using System.Web.Services;
using System.Net;
using System.IO;
public partial class Root_Admin_AddFundInEWallet : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtEWalletBalance = new DataTable();
    DataTable dtEWalletTransaction = new DataTable();
    DataTable dtMix = new DataTable();
    cls_myMember clsm = new cls_myMember();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                DataTable dtemployee = (DataTable)Session["dtEmployee"];
                if (dtemployee.Rows.Count > 0)
                {
                    ViewState["name"] = dtemployee.Rows[0]["EmployeeName"].ToString();
                    ViewState["fundmobile"] = dtemployee.Rows[0]["Mobile"].ToString();
                    BindDropdown();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Please try Later !');", true);
                }
            }
            else
            {
                Response.Redirect("~/adminlogin.aspx");
            }
        }
    }
    #endregion
    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        try
        {
            if (Session["dtEmployee"] != null)
            {
                if (txt_dmrotp.Text.Trim() == Session["chdmtOTP"].ToString())
                {
                    int cnt = 0;
                    DataTable dtemployee = (DataTable)Session["dtEmployee"];
                    if (dtemployee.Rows.Count > 0)
                    {

                        if (ddl_members.SelectedIndex > 0)
                        {
                            if (Convert.ToDecimal(txtAmount.Text) > 0)
                            {
                                string sms = "You have successfully Credit Rs. "+ txtAmount.Text + " Now your total amount is Rs. 122 Thanks.Team Fritware";
                                DataTable dt = new DataTable();
                                cls_connection cls = new cls_connection();
                                dt = cls.select_data_dt("select mobile from tblmlm_membermaster where memberid='" + ddl_members.SelectedValue +"'");
                                string narrationcredit = "Admin - Add Fund With Credit " + txtNarration.Text;
                                string narration = "Admin- Add Fund " + txtNarration.Text;
                                if (chkCredit.Checked == true)
                                {
                                    cnt = clsm.Wallet_Addfund(Convert.ToInt32(hidMsrNo.Value), ddl_members.SelectedValue, Convert.ToDecimal(txtAmount.Text), narrationcredit, hidMobile.Value);

                                }
                                else
                                {
                                    cnt = clsm.Wallet_Addfund(Convert.ToInt32(hidMsrNo.Value), ddl_members.SelectedValue, Convert.ToDecimal(txtAmount.Text), narration, hidMobile.Value);
                                }
                                if (cnt > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');;location.replace('AddFund.aspx');", true);
                                    clear();
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Member Id not exists !');", true);
                                }


                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Amount Should Be greater than Zero');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select MemberId');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Please try Later !');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter valid OTP ');", true);
                    mpe_dmrotp.Show();
                  
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error| Please try Later');", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Database Error !');", true);
        }
        #endregion

    }
    #endregion
    protected void btn_dmrotp_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Session["dtEmployee"] != null) 
        {
            Random random = new Random();
            string mobile = ViewState["fundmobile"].ToString();
            int SixDigit = random.Next(1000, 9999);
            Session["chdmtOTP"] = SixDigit.ToString();
            string[] valueArray = new string[2];
          //  valueArray[0] = txtAmount.Text;
            valueArray[0] = ViewState["name"].ToString();
            valueArray[1] = Session["chdmtOTP"].ToString();
           // SMS.SendWithVar(mobile, 21, valueArray, 1);
           // SendWithVarpan("9166396947", 1, valueArray);
            mpe_dmrotp.Show();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some error found please try again !');", true);
        }
    }

    #region [Reset]
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();

    }
    #endregion

    #region [All Functions]
    private void clear()
    {
        ddl_members.SelectedIndex = 0;
        hidMsrNo.Value = "";
        lblMemberName.Text = "";
        lblEWalletBalance.Text = "0.0";
        txtAmount.Text = "";
        txtNarration.Text = "";
        lblEWalletBalance.Text = "";
    }
    #endregion

    #region class
    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
    }
    #endregion

    [WebMethod]
    public static List<Customer> getmember()
    {
        string data = "";
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@data", value = data });
        dtEWalletTransaction = cls.select_data_dtNew("get_member", _lstparm);

        foreach (DataRow dtrow in dtEWalletTransaction.Rows)
        {
            Customer cust = new Customer();
            cust.MsrNo = dtrow["MsrNo"].ToString();
            cust.MemberID = dtrow["MemberID"].ToString();
            cust.MemberName = dtrow["MemberID"].ToString() + "-" + dtrow["MemberName"].ToString() + "-" + dtrow["Mobile"].ToString();
            custList.Add(cust);
        }
        return custList;
    }

    protected void btn_Closedmr_Click(object sender, EventArgs e)
    {
        mpe_dmrotp.Hide();
    }

    public void BindDropdown()
    {
        string data = "";
        DataTable dtEWalletTransaction = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@data", value = data });
        dtEWalletTransaction = cls.select_data_dtNew("get_member", _lstparm);
        ddl_members.DataSource = dtEWalletTransaction;
        ddl_members.DataTextField = "MemberName";
        ddl_members.DataValueField = "MemberId";
        ddl_members.DataBind();
        ddl_members.Items.Insert(0, new ListItem("Select Member", "0"));
    }
    protected void ddl_members_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_members.SelectedValue != "" && ddl_members.SelectedIndex > 0)
        {
            dtMix = objMix.MLM_UniversalSearch("GetMemberMasterByMemberID", ddl_members.SelectedValue, "", "", 0, 0, 0);
            if (dtMix.Rows.Count > 0)
            {
                lblMemberName.Text = Convert.ToString(dtMix.Rows[0]["MemberName"]);
                hidMsrNo.Value = Convert.ToString(dtMix.Rows[0]["MsrNo"]);
                hidMobile.Value = Convert.ToString(dtMix.Rows[0]["Mobile"]);
                #region GetBalanceByMsrNo
                dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", Convert.ToInt32(hidMsrNo.Value));

                if (dtEWalletBalance.Rows.Count > 0)
                {
                    lblEWalletBalance.Text = Convert.ToString(dtEWalletBalance.Rows[0]["Balance"]);
                }
                #endregion
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Please Enter Valid MemberID !');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Please Select MemberID !');", true);

        }
    }

    public static void SendWithVarpan(string Mobile, int Template, string[] ValueArray)
    {
        try
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            WebClient client = new WebClient();
            string smsMessage = GetString(Template, ValueArray);
            string baseurl = "http://api.msg91.com/api/sendhttp.php?authkey=350021A7vlWaaHRl2X5fe1f471P1&route=4&sender=SUNNET&mobiles=" + Mobile + "& message=" + smsMessage + "";
            string baseurlw = "http://api.msg91.com/api/sendhttp.php?authkey=331233AGDc1URj5ed8901bP1&route=4&sender=SMONEY&mobiles=" + Mobile + "& message=" + smsMessage + "";
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
        "Dear Admin, you need an OTP  to access Fund Transafer for Rs.@v0@ and OTP is @v1@. Never Share it with anyone.Bank Never calls to verify it."//1
    };

    public static string get_SMSBaseURL(string Mobile, string smsMessage, int ApiID, string Route)
    {
        cls_connection cls = new cls_connection();
        DataTable dtAPI = new DataTable();

        dtAPI = cls.select_data_dt("Proc_Recharge_SMSApi 'getDataById'," + ApiID + "");
        string str = "";
        str = dtAPI.Rows[0]["URL"].ToString() + dtAPI.Rows[0]["prm1"].ToString() + "=" + dtAPI.Rows[0]["prm1val"].ToString() + "&";
        if (dtAPI.Rows[0]["prm2"].ToString() != "" && dtAPI.Rows[0]["prm2val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm2"].ToString() + "=" + dtAPI.Rows[0]["prm2val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm3"].ToString() != "" && dtAPI.Rows[0]["prm3val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm3"].ToString() + "=" + dtAPI.Rows[0]["prm3val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm4"].ToString() != "" && dtAPI.Rows[0]["prm4val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm4"].ToString() + "=" + dtAPI.Rows[0]["prm4val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm5"].ToString() != "" && dtAPI.Rows[0]["prm5val"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm5"].ToString() + "=" + dtAPI.Rows[0]["prm5val"].ToString() + "&";
        }
        if (dtAPI.Rows[0]["prm6"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm6"].ToString() + "=" + Mobile + "&";
        }
        if (dtAPI.Rows[0]["prm7"].ToString() != "")
        {
            str = str + dtAPI.Rows[0]["prm7"].ToString() + "=" + smsMessage + "";
        }

        //str = str + "route=" + Route;

        return str;
    }




}