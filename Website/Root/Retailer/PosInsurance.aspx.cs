using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
public partial class Root_Retailer_PosInsurance : System.Web.UI.Page
{
    #region Access_Class
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
   
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
                    ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                    Session["TransactionPassword"] = dtMember.Rows[0]["TransactionPassword"];
                    Session["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                    ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                    ViewState["dmtmobile"] = dtMember.Rows[0]["Mobile"].ToString();
                    FillData(Convert.ToInt32(Session["MsrNo"]));
                    DataTable dtChk = Cls.select_data_dt(@"SELECT * FROM tbl_posinsurance WHERE MsrNo='" + dtMember.Rows[0]["MsrNo"].ToString() + "'");
                    if (dtChk.Rows.Count > 0)
                    {

                        if (dtChk.Rows[0]["Status"].ToString() == "REJECTED")
                        {
                            lbl_Status.Visible = true;
                            lbl_Status.Text = "Your Pos Registration Request Rejected Reason -- " + dtChk.Rows[0]["adminstatus"].ToString() + "";
                        }
                        else
                        {
                            txt_name.Text = dtChk.Rows[0]["name"].ToString();
                            txt_email.Text = dtChk.Rows[0]["email"].ToString();
                            txt_mobile.Text = dtChk.Rows[0]["mobile"].ToString();
                            txt_aadhar.Text = dtChk.Rows[0]["adharnumber"].ToString();
                            txt_pan.Text = dtChk.Rows[0]["pannumber"].ToString();
                            lbl_Status.Visible = true;
                            DisableControl();
                            lbl_Status.Text = "Your Pos Registration status is " + dtChk.Rows[0]["Status"].ToString() + "";
                        }
                    }
                    else
                    {
                        lbl_Status.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
            }
        }
        else
        {
            Response.Redirect("~/userlogin");
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            cls_connection cls = new cls_connection();
            cls_myMember clsm = new cls_myMember();
            string txnid = clsm.Cyrus_GetTransactionID_New();
            string name = txt_name.Text.Trim();
            string email = txt_email.Text.Trim();
            string mobile = txt_mobile.Text.Trim();
            string aadharcard = txt_aadhar.Text;
            string pancard = txt_pan.Text;
            string pan = uploadPanImage(file_pan);
            string aadharfont = uploadPanImage(file_aadharfont);
            string aadharback = uploadPanImage(file_adharback);
            string marksheet = uploadPanImage(file_sheet);
            string passbook = uploadPanImage(file_passbook);
            string photo = uploadPanImage(file_photo);
            string noc = uploadPanImage(file_noc);
            DataTable dtMember = (DataTable)Session["dtRetailer"];
            DataTable dtChk = Cls.select_data_dt(@"SELECT * FROM tbl_posinsurance WHERE MsrNo='" + dtMember.Rows[0]["MsrNo"].ToString() + "'");
            if (dtChk.Rows.Count > 0)
            {
                cls.select_data_dt(@"Update tbl_posinsurance set name='"+name+"',email='"+email+"',mobile='"+mobile+"',adharnumber='"+ aadharcard + "',pannumber='"+pancard+ "',aadharfont='"+aadharfont+"',adharback='"+aadharback+"',pancard='"+pan+"',marksheet='"+marksheet+"',passbook='"+passbook+"',photo='"+photo+"',noc='"+noc+ "',status='Pending' Where  msrno='" + dtMember.Rows[0]["MsrNo"].ToString() + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your request send to admin team. ');window.location ='PosInsurance.aspx';", true);
            }
            else
            {
                //insertion start
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@memberid", value = ViewState["MemberId"].ToString() });
                _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(ViewState["MsrNo"]) });
                _lstparm.Add(new ParmList() { name = "@NAME", value = name });
                _lstparm.Add(new ParmList() { name = "@email", value = email });
                _lstparm.Add(new ParmList() { name = "@mobile", value = mobile });
                _lstparm.Add(new ParmList() { name = "@adharnumber", value = aadharcard });
                _lstparm.Add(new ParmList() { name = "@pannumber", value = pancard });
                _lstparm.Add(new ParmList() { name = "@aadharfont", value = aadharfont });
                _lstparm.Add(new ParmList() { name = "@adharback", value = aadharback });
                _lstparm.Add(new ParmList() { name = "@pancard", value = pan });
                _lstparm.Add(new ParmList() { name = "@marksheet", value = marksheet });
                _lstparm.Add(new ParmList() { name = "@passbook", value = passbook });
                _lstparm.Add(new ParmList() { name = "@photo", value = photo });
                _lstparm.Add(new ParmList() { name = "@noc", value = noc });
                _lstparm.Add(new ParmList() { name = "@txnid", value = txnid });
                _lstparm.Add(new ParmList() { name = "@status", value = "Pending" });
                _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                Cls.select_data_dtNew("SET_pos_insurance", _lstparm);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your request send to admin team. ');window.location ='PosInsurance.aspx';", true);


            }
        }

        catch (Exception EX)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('ERRER');", true);
        }
    }

    private string uploadPanImage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Uploads/Servicesimage/Actual/");
                string mpath = Server.MapPath("~/Uploads/Servicesimage/Medium/");
                string spath = Server.MapPath("~/Uploads/Servicesimage/Small/");

                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
                if (!Directory.Exists(mpath))
                {
                    Directory.CreateDirectory(mpath);
                }
                if (!Directory.Exists(spath))
                {
                    Directory.CreateDirectory(spath);
                }

                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(_fup.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".xls" || Extension == ".pdf" || Extension == ".xlsx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                    //   objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
                    //   objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);

                    return FileName;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select JPG/JPEG/PNG/GIF Images Only!');", true);

                }
            }
        }
        else
        {
            return "";
        }

        return "";
    }



    private void DisableControl()
    {
        txt_name.Enabled = false;
        txt_mobile.Enabled = false;
        txt_email.Enabled = false;
        txt_pan.Enabled = false;
        txt_aadhar.Enabled = false;
        file_adharback.Enabled = false;
        file_aadharfont.Enabled = false;
        file_noc.Enabled = false;
        file_passbook.Enabled = false;
        file_pan.Enabled = false;
        file_sheet.Enabled = false;
        file_photo.Enabled = false;
        btnSubmit.Enabled = false;
    }


    private void FillData(int id)
    {

        DataTable dt = new DataTable();
        dt = Cls.select_data_dt("select * from tblmlm_membermaster where msrno='" + id + "'");

        if (dt.Rows.Count > 0)
        {

            txt_name.Text = Convert.ToString(dt.Rows[0]["FirstName"]);
            txt_email.Text = Convert.ToString(dt.Rows[0]["Email"]);
            txt_mobile.Text = Convert.ToString(dt.Rows[0]["mobile"]);
        }
        else
        {

        }
    }


}