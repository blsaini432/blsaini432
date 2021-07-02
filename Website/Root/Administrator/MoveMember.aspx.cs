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
public partial class Root_Admin_MoveMember : System.Web.UI.Page
{

    #region [Properties]
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    clsState objState = new clsState();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_Universal objUniversal = new cls_Universal();
    DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    DataTable dtPackage = new DataTable();
    cls_connection cls = new cls_connection();
    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    string condition = " msrno > 0";
    cls_myMember clsm = new cls_myMember();
    #endregion
    #region [Properties]
    DataTable dtUniversal = new DataTable();

    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();

    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            if (Request.QueryString["Change"] == null)
            {

            }
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
        //txtConfirmPassword.Text = "";
        //txtCurrentPassword.Text = "";
        //txtNewPassword.Text = "";
    }

 
    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt1 = new DataTable();
        DataTable dtParent = new DataTable();
        //dt1 = cls.select_data_dt("Select * from tblmlm_membermaster where mobile='" + txtMobile.Text.Trim() + "' and isactive=1 and isdelete=0");
        dt1 = cls.select_data_dt("Select *,(Select memberid + '-' + firstname + ' ' + lastname as Memberid from tblmlm_membermaster as a where a.msrno=tblmlm_membermaster.parentmsrno) as parent from tblmlm_membermaster where memberid='" + txtMobile.Text.Trim() + "' and isactive=1 and isdelete=0");
        if (dt1.Rows.Count > 0)
        {
            lblDesignation.Text = dt1.Rows[0]["MemberType"].ToString();
            dtParent = cls.select_data_dt("Select * from tblmlm_membermaster where msrno='" + dt1.Rows[0]["parentmsrno"].ToString() + "' and isactive=1 and isdelete=0");
            if (dtParent.Rows.Count > 0)
            {
                string returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup9tbl'>";
                returndiv += "<tr><td>Member ID</td><td>:</td><td>" + dtParent.Rows[0]["MemberID"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Member Name</td><td>:</td><td>" + dtParent.Rows[0]["firstname"].ToString() + " " + dtParent.Rows[0]["lastname"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Member Type</td><td>:</td><td>" + dtParent.Rows[0]["MemberType"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Mobile</td><td>:</td><td>" + dtParent.Rows[0]["Mobile"].ToString() + "</td></tr>";
                //returndiv += "<tr><td>Parent/Upline</td><td>:</td><td>" + dtParent.Rows[0]["Parent"].ToString() + "</td></tr>";
                returndiv += "</table></div>";
                litParentMemberDetails.Text = returndiv;
                btnOTPsubmit.Visible = true;
            }
            else
            {
                string returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup9tbl'>";
                returndiv += "<tr><td colspan='3'>Upline Member not found !!</td></tr>";
                returndiv += "</table></div>";
                litParentMemberDetails.Text = returndiv;
                btnOTPsubmit.Visible = false;
            }
            //if (Convert.ToInt32(dt1.Rows[0]["MemberTypeid"]) == Convert.ToInt32(dtParent.Rows[0]["MemberTypeid"]))
            //{
            //    btnOTPsubmit.Visible = false;
            //}
            //else
            //{
            //    DataTable dtdesignation = new DataTable();
            //    dtdesignation = cls.select_data_dt("Select * from tblmlm_membership where membertypeid>'" + dtParent.Rows[0]["MemberTypeid"].ToString() + "' and membertypeid<'" + dt1.Rows[0]["MemberTypeid"].ToString() + "' order by membertypeid");
            //    ddlMembertype.DataSource = dtdesignation;
            //    ddlMembertype.DataTextField = "membertype";
            //    ddlMembertype.DataValueField = "membertypeid";
            //    ddlMembertype.DataBind();
            //    ddlMembertype.Items.Insert(0, new ListItem("--Select New Designation--", "0"));
            //    btnOTPsubmit.Visible = true;
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("Exec MoveMember_Step1 '" + dt1.Rows[0]["msrno"].ToString() + "','" + dtParent.Rows[0]["msrno"].ToString() + "'");
           // dt.DefaultView.RowFilter = "membertypeid<>11";
            ddlNewOwnerID.DataSource = dt.DefaultView.ToTable();
            ddlNewOwnerID.DataTextField = "membername";
            ddlNewOwnerID.DataValueField = "msrno";
            ddlNewOwnerID.DataBind();
            ddlNewOwnerID.Items.Insert(0, new ListItem("--Select New Owner--", "0"));
            mvw.ActiveViewIndex = 1;
            //}
        }
    }
    #endregion

    protected void btnOTPsubmit_Click(object sender, EventArgs e)
    {
        DataTable dt1 = new DataTable();
        dt1 = cls.select_data_dt("Select * from tblmlm_membermaster where memberid='" + txtMobile.Text.Trim() + "' and isactive=1 and isdelete=0");
        if (dt1.Rows.Count > 0)
        {
            DataTable dtmembership = new DataTable();
            DataTable dt= cls.select_data_dt("Exec Move_Member '" + dt1.Rows[0]["msrno"].ToString() + "','" + ddlNewOwnerID.SelectedValue + "'");
            if (dt1.Rows.Count > 0)
            {
                string returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup9tbl'>";
                returndiv += "<tr><td>Member ID</td><td>:</td><td>" + dt1.Rows[0]["MemberID"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Member Name</td><td>:</td><td>" + dt1.Rows[0]["firstname"].ToString() + " " + dt1.Rows[0]["lastname"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Member Type</td><td>:</td><td>" + dt1.Rows[0]["MemberType"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Mobile</td><td>:</td><td>" + dt1.Rows[0]["Mobile"].ToString() + "</td></tr>";
                returndiv += "</table></div>";
                lblNewMemberDetails.Text = returndiv;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Member Account has been updated accordingly !!');", true);
            }
            mvw.ActiveViewIndex = 2;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Member Account has been updated accordingly !!');", true);
        }
    }
    protected void btnNewMobile_Click(object sender, EventArgs e)
    {
        //DataTable dtMember = cls.select_data_dt("Select count(*) from tblmlm_membermaster where mobile='" + txtNewMobile.Text.Trim() + "'");
        //if (dtMember.Rows.Count == 0)
        //{
        //    dtMember = (DataTable)Session["dtDistributor"];
        //    Random random = new Random();
        //    int SixDigit = random.Next(1000, 9999);
        //    Session["chOTP"] = SixDigit.ToString();
        //    string[] valueArray = new string[2];
        //    valueArray[0] = dtMember.Rows[0]["FirstName"].ToString();
        //    valueArray[1] = SixDigit.ToString();
        //    SMS.SendWithVar(dtMember.Rows[0]["Mobile"].ToString(), 21, valueArray, Convert.ToInt32(dtMember.Rows[0]["msrno"]));
        //    mvw.ActiveViewIndex = 3;
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Mobile Number already registered !!');", true);
        //}
    }
    protected void txtMobile_TextChanged(object sender, EventArgs e)
    {
        if (txtMobile.Text.Trim() != "")
        {
            //if (txtMobile.Text.Length == 10 && Convert.ToInt64(txtMobile.Text.Trim()) > 0)
            //{
            DataTable dt1 = new DataTable();
            dt1 = cls.select_data_dt("Select *,(Select memberid + '-' + firstname + ' ' + lastname as Memberid from tblmlm_membermaster as a where a.msrno=tblmlm_membermaster.parentmsrno) as parent from tblmlm_membermaster where memberid='" + txtMobile.Text.Trim() + "' and isactive=1 and isdelete=0");
            if (dt1.Rows.Count > 0)
            {
                string returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup9tbl'>";
                returndiv += "<tr><td>Member ID</td><td>:</td><td>" + dt1.Rows[0]["MemberID"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Member Name</td><td>:</td><td>" + dt1.Rows[0]["firstname"].ToString() + " " + dt1.Rows[0]["lastname"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Member Type</td><td>:</td><td>" + dt1.Rows[0]["MemberType"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Mobile</td><td>:</td><td>" + dt1.Rows[0]["Mobile"].ToString() + "</td></tr>";
                returndiv += "<tr><td>Parent/Upline</td><td>:</td><td>" + dt1.Rows[0]["parent"].ToString() + "</td></tr>";
                if (Convert.ToInt32(dt1.Rows[0]["MemberTypeid"]) == 2)
                    returndiv += "<tr><td colspan='3'><b>Note:You can not upgrade this member..as he already have top level !!</b></td></tr>";
                else
                    btnSubmit.Visible = true;
                returndiv += "</table></div>";
                litMemberDetails1.Text = returndiv;
                litMemberInfo.Text = returndiv;
            }
            else
            {
                string returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup9tbl'>";
                returndiv += "<tr><td colspan='3'>Member not found !! Invalid Member Mobile Number.</td></tr>";
                returndiv += "</table></div>";
                litMemberInfo.Text = returndiv;
                btnSubmit.Visible = false;
            }
        }
        else
        {
            string returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup9tbl'>";
            returndiv += "<tr><td colspan='3'>Invalid Member Mobile Number.</td></tr>";
            returndiv += "</table></div>";
            litMemberInfo.Text = returndiv;
            btnSubmit.Visible = false;
        }
        //}
        //else
        //{
        //    string returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup9tbl'>";
        //    returndiv += "<tr><td colspan='3'>Invalid Member Mobile Number.</td></tr>";
        //    returndiv += "</table></div>";
        //    litMemberInfo.Text = returndiv;
        //    btnSubmit.Visible = false;
        //}
    }
    protected void ddlMembertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNewOwnerID.SelectedValue != "0")
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select *,(Select memberid + '-' + firstname + ' ' + lastname as Memberid from tblmlm_membermaster as a where a.msrno=tblmlm_membermaster.parentmsrno) as parent from tblmlm_membermaster where msrno='" + ddlNewOwnerID.SelectedValue + "'");
            string returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup9tbl'>";
            returndiv += "<tr><td>Member ID</td><td>:</td><td>" + dt.Rows[0]["MemberID"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Member Name</td><td>:</td><td>" + dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Member Type</td><td>:</td><td>" + dt.Rows[0]["MemberType"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Mobile</td><td>:</td><td>" + dt.Rows[0]["Mobile"].ToString() + "</td></tr>";
            returndiv += "<tr><td>Parent/Upline</td><td>:</td><td>" + dt.Rows[0]["parent"].ToString() + "</td></tr>";
            returndiv += "</table></div>";
            LitNewOwner.Text = returndiv;
        }
        else
        {
            LitNewOwner.Text = "No member Selected !!";
        }
    }
    //protected void FillPackagebyTypeid(string xx)
    //{
    //    DataTable dtPackage = new DataTable();
    //    dtPackage = cls.select_data_dt("Exec getPackagebyTypeid 1," + xx + "");
    //    ddlPackage.DataSource = dtPackage;
    //    ddlPackage.DataValueField = "PackageID";
    //    ddlPackage.DataTextField = "PackageName";
    //    ddlPackage.DataBind();
    //    ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
    //}
}