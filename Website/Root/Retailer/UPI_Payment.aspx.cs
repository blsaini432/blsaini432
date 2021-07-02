using BLL;
using System;
using System.Data;
using System.Web.UI;

public partial class Root_Retailer_UPI_Payment : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();

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
                   // dtMemberMaster = objMemberMaster.ManageMemberMaster("Get", Convert.ToInt32(Session["DistributorMsrNo"]));
                    int msrno = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]);
                    dt = Cls.select_data_dt(@"exec Set_EzulixDmr @action='utigateway', @msrno=" + msrno + "");
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dt.Rows[0]["utigateway"]) == true)
                        {
                            ViewState["MemberId"] = null;
                            ViewState["MsrNo"] = null;
                            ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                            Session["TransactionPassword"] = dtMember.Rows[0]["TransactionPassword"];
                            Session["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                            ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                            ViewState["dmtmobile"] = dtMember.Rows[0]["Mobile"].ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('UTI Gateway service Not active, Contact to your admin');window.location ='DashBoard.aspx';", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('UTI Gateway service Not active');window.location ='DashBoard.aspx';", true);
                    }
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
        DataTable dt = new DataTable();
        DataTable dtMember = (DataTable)Session["dtRetailer"];
        string TXN_AMOUNT = txt_Amount.Text;
        string UPIID = txt_upi.Text;
        Session["UPIID"] = UPIID;
        string AMOUNT = TXN_AMOUNT;
        Session["Amount"] = AMOUNT;
        string USER_ID = ViewState["MemberId"].ToString();
        Session["userid"] = USER_ID;
        Response.Redirect("UPI_Payment_return.aspx");
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
    }

}