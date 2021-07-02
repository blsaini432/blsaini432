using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.IO;
public partial class Root_Admin_ManageFundRequest : System.Web.UI.Page
{
    #region [Properties]
    clsMLM_FundRequest objFundRequest = new clsMLM_FundRequest();
    DataTable dtFundRequest = new DataTable();
    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtFundRequestDetail = new DataTable();
    clsMLM_MemberBanker objMemberBanker = new clsMLM_MemberBanker();
    DataTable dtMemberBanker = new DataTable();
    cls_connection objConnection = new cls_connection();
    string PaymentProof = "";
    string RequestStatus = "";
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtretailer"] != null)
            {
                DataTable dtMember = (DataTable)Session["dtretailer"];
                ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                fillBankNameTo();
              //  FillData(Convert.ToInt32(dtMember.Rows[0]["MsrNo"]));
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }

    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtMember = (DataTable)Session["dtretailer"];
        string TXN_AMOUNT = txt_Amounts.Text;
        int AMOUNT = Convert.ToInt32(TXN_AMOUNT);
        if (AMOUNT >= 100 && AMOUNT <= 5000)
        {
           
            Session["Amount"] = AMOUNT;
            string USER_ID = ViewState["MemberId"].ToString();
            Session["userid"] = USER_ID;
            Response.Redirect("paytmpayment_js.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('minimum amount 100 and maximum Amount 5000 ');window.location ='managefundrequest.aspx';", true);
        }
       
    }

 
 
    public void fillBankNameTo()
    {
        dtMemberBanker = objMemberBanker.ManageMemberBanker("GetByMsrNo", 1);
        gvMemberBanker.DataSource = dtMemberBanker;
        gvMemberBanker.DataBind();

        //dtMemberBanker.Columns.Add("FullName", typeof(string), "BankerMasterName + '-' + BankBranch + '-' + AccountNumber");
        //ddlBankTo.DataSource = dtMemberBanker;
        //ddlBankTo.DataValueField = "BankerMasterID";
        //ddlBankTo.DataTextField = "FullName";
        //ddlBankTo.DataBind();
        //ddlBankTo.Items.Insert(0, new ListItem("Select To Bank Name", "0"));
    }



}