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
            if (Session["dtRetailer"] != null)
            {
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                fillBankName();
                fillBankNameTo();
                FillData(Convert.ToInt32(dtMember.Rows[0]["MsrNo"]));
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(txtTotalAmount.Text) < 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Minimum Fund Resquest is 220 /-');", true);
            lblredmsg.Visible = true;
            return;
        }
        if (Session["dtRetailer"] != null)
        {
            #region [Insert]
            DataTable dtMember = (DataTable)Session["dtRetailer"];

            Int32 intresult = 0;
            DateTime RequestDate = DateTime.Now;
            DateTime ChequeDate;
            PaymentProof = uploadFundRequestImage();
            RequestStatus = "Pending";
            if (txtChequeDate.Text == "" || txtChequeDate.Text == null)
            {
                ChequeDate = DateTime.Now;
            }
            else
            {
                ChequeDate = Convert.ToDateTime(txtChequeDate.Text);
            }
            string FromBank = ddlBankName.SelectedItem.Text;
            string ToBank = ddlBankTo.SelectedItem.Text;
            intresult = objFundRequest.AddEditFundRequest(0, "E Wallet", Convert.ToInt32(dtMember.Rows[0]["MsrNo"]),dtMember.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(txtTotalAmount.Text), ddlBankName.SelectedItem.Text, ddlPaymentMode.SelectedItem.Text, PaymentProof, txtChequeOrDDNumber.Text, ChequeDate, RequestStatus, txtRemark.Text, "", FromBank, ToBank);
            if (intresult > 0)
            {
                DataTable dt = objConnection.select_data_dt("Select * from tblMLM_MemberMaster where MsrNo=1");
                string[] valueArray = new string[2];
                valueArray[0] = txtTotalAmount.Text;
                valueArray[1] = dtMember.Rows[0]["MemberId"].ToString();
                DLTSMS.SendWithVar(dt.Rows[0]["Mobile"].ToString(), 5, valueArray, 1);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Record inserted successfully');location.replace('ManageFundRequest.aspx');", true);
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Fund Request Already Pending !');", true);

            }
            #endregion
        }
        else
        {
            Response.Redirect("DashBoard.aspx");
        }
    }
    #endregion

    #region [Reset]
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();

    }
    #endregion

    #region [All Functions]
    private void clear()
    {
        txtTotalAmount.Text = "0.0";
        ddlBankName.SelectedIndex = 0;
        ddlBankTo.SelectedIndex = 0;
        ddlPaymentMode.SelectedIndex = 0;
        txtChequeOrDDNumber.Text = "";
        txtChequeDate.Text = "";
        txtRemark.Text = "";
    }

    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objFundRequest.ManageFundRequest("GetAll", id);
        if (dt.Rows.Count > 0)
        {
            txtTotalAmount.Text = Convert.ToString(dt.Rows[0]["TotalAmount"]);
            fillBankName();
            ddlBankName.SelectedItem.Text = Convert.ToString(dt.Rows[0]["FromBank"]);
            fillBankNameTo();
            ddlBankTo.SelectedItem.Text = Convert.ToString(dt.Rows[0]["ToBank"]);
            hidPaymentProof.Value = Convert.ToString(dt.Rows[0]["PaymentProof"]);
            ddlPaymentMode.SelectedValue = Convert.ToString(dt.Rows[0]["PaymentMode"]);
            txtChequeOrDDNumber.Text = Convert.ToString(dt.Rows[0]["ChequeOrDDNumber"]);
            txtChequeDate.Text = Convert.ToString(dt.Rows[0]["ChequeDate"]);
            txtRemark.Text = Convert.ToString(dt.Rows[0]["Remark"]);
        }
    }

    public void fillBankName()
    {
        clsMLM_BankerMaster objBankerMaster = new clsMLM_BankerMaster();
        DataTable dtBankerMaster = new DataTable();
        dtBankerMaster = objBankerMaster.ManageBankerMaster("Get", 0);
        ddlBankName.DataSource = dtBankerMaster;
        ddlBankName.DataValueField = "BankerMasterID";
        ddlBankName.DataTextField = "BankerMasterName";
        ddlBankName.DataBind();
        ddlBankName.Items.Insert(0, new ListItem("Select Bank Name", "0"));

    }
    public void fillBankNameTo()
    {
        dtMemberBanker = objMemberBanker.ManageMemberBanker("GetByMsrNo", 1);
        gvMemberBanker.DataSource = dtMemberBanker;
        gvMemberBanker.DataBind();

        dtMemberBanker.Columns.Add("FullName", typeof(string), "BankerMasterName + '-' + BankBranch + '-' + AccountNumber");
        ddlBankTo.DataSource = dtMemberBanker;
        ddlBankTo.DataValueField = "BankerMasterID";
        ddlBankTo.DataTextField = "FullName";
        ddlBankTo.DataBind();
        ddlBankTo.Items.Insert(0, new ListItem("Select To Bank Name", "0"));
    }
    #endregion

    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedValue == "Cash")
        {
            txtChequeDate.Text = "";
            txtChequeOrDDNumber.Text = "";
            txtChequeDate.Enabled = false;
            rfvChequeDate.Enabled = false;
            txtChequeOrDDNumber.Enabled = false;
            rfvChequeOrDDNumber.Enabled = false;
            lblTransactionCharge.Text = "(Note: You have to pay 1% Transaction Charge for each request.)";
        }
        else
        {
            if (ddlPaymentMode.SelectedValue == "IMPS" || ddlPaymentMode.SelectedValue == "NEFT")
            {
                lblchqtitle.Text = "Transaction No.";
                lbldttitle.Text = "Transaction Date";
            }
            else
            {
                lblchqtitle.Text = "Cheque Or DDNumber";
                lbldttitle.Text = "ChequeDate";
            }
            txtChequeDate.Enabled = true;
            rfvChequeDate.Enabled = true;
            txtChequeOrDDNumber.Enabled = true;
            rfvChequeOrDDNumber.Enabled = true;
            lblTransactionCharge.Text = "";
        }
    }

    private string uploadFundRequestImage()
    {
        string fullpath = string.Empty;
        if (FileUpload1.HasFile == true)
        {
            if (FileUpload1.PostedFile.FileName != "")
            {
                string filename = System.IO.Path.GetFileName(FileUpload1.FileName);
                string opath = Server.MapPath("../../Uploads/FundRequest/Actual/");
                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                {
                    fullpath = DateTime.Now.Ticks.ToString() + Extension;
                    FileUpload1.PostedFile.SaveAs(opath + fullpath);
                    return fullpath;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload valid file type');", true);
                    return fullpath;
                }
            }
        }
        else if (hidPaymentProof.Value.Trim() != "")
        {
            return hidPaymentProof.Value;
        }
        return "";
    }
}