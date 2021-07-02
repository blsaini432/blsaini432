using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.IO;
using System.Net;
using System.Configuration;
public partial class Root_Retailer_psa_reg : System.Web.UI.Page
{
    #region MyRegion
    cls_connection Cls = new cls_connection();
    clsState objState = new clsState();
    clsCity objCity = new clsCity();
    public static string adminmemberid = ConfigurationManager.AppSettings["adminmemberid"];
    public static string adminurl = ConfigurationManager.AppSettings["adminurl"];
    public static string initial = ConfigurationManager.AppSettings["initial"];
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                ViewState["MsrNo"] = null;
                ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"].ToString();
                Session["MemberId"] = dtMember.Rows[0]["MemberId"].ToString();

                DataTable dtChk = new DataTable();
                List<ParmList> _lstparms = new List<ParmList>();
                _lstparms.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(dtMember.Rows[0]["MsrNo"].ToString()) });
                _lstparms.Add(new ParmList() { name = "@Action", value = "CheckStatus" });
                dtChk = Cls.select_data_dtNew("set_Psa_Reg", _lstparms);

                if (dtChk.Rows.Count > 0)
                {
                    if (dtChk.Rows[0]["Statu"].ToString() == "Rejected")
                    {
                        lbl_Status.Visible = true;
                        lbl_Status.Text = "Your PSALoginid is Rejected and reason is " + dtChk.Rows[0]["rejection"].ToString();
                    }
                    else if (dtChk.Rows[0]["Statu"].ToString() == "Approved")
                    {
                        txtFirstName.Text = dtChk.Rows[0]["Name"].ToString();
                        txt_Email.Text = dtChk.Rows[0]["Email"].ToString();
                        txt_Mno.Text = dtChk.Rows[0]["Contact_Number"].ToString();
                        DisableControl();
                        lbl_Status.Visible = true;
                        lbl_Status.Text = "Your PSALoginid is:-" + dtChk.Rows[0]["psaloginid"].ToString() + "and status is " + dtChk.Rows[0]["Statu"].ToString() + "";
                    }
                    else
                    {
                        txtFirstName.Text = dtChk.Rows[0]["Name"].ToString();
                        txt_Email.Text = dtChk.Rows[0]["Email"].ToString();
                        txt_Mno.Text = dtChk.Rows[0]["Contact_Number"].ToString();
                        DisableControl();
                        lbl_Status.Visible = true;
                        lbl_Status.Text = "Your PSALoginid is Pending";
                    }
                }
                else
                {
                    lbl_Status.Visible = false;
                }
            }
        }
    }

    #region Method
    private void DisableControl()
    {
        txtFirstName.Enabled = false;
        txt_Email.Enabled = false;
        txt_Mno.Enabled = false;
        btn_Address.Visible = false;
        btn_Identity.Visible = false;
        btn_Submit.Visible = false;
        btn_Reset.Visible = false;
    }


    #endregion

    #region Events


    protected void btn_Identity_Click(object sender, EventArgs e)
    {
        string filepath = string.Empty;
        if (fu_Identity.HasFile == true)
        {
            if (fu_Identity.PostedFile.FileName != "")
            {
                string filename = System.IO.Path.GetFileName(fu_Identity.FileName);
                filepath = Server.MapPath("../../Uploads/User/UTI/");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(fu_Identity.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg")
                {
                    if (fu_Identity.PostedFile.ContentType == "image/jpeg")
                    {
                        if (fu_Identity.PostedFile.ContentLength <= 1024000)
                        {
                            fu_Identity.SaveAs(filepath +ViewState["MsrNo"].ToString() + Path.GetFileName(fu_Identity.FileName));
                            filepath = ViewState["MsrNo"].ToString() + Path.GetFileName(fu_Identity.FileName);
                            ViewState["IdenFileName"] = filepath;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File Upload a successfully!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Image has to be less than or equal 1 Mb!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only JPEG files are accepted!');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload valid file type');", true);

                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload a file to upload');", true);
        }
    }
    protected void btn_Address_Click(object sender, EventArgs e)
    {
        string filepath = string.Empty;
        if (fu_Address.HasFile == true)
        {
            if (fu_Address.PostedFile.FileName != "")
            {
                string filename = System.IO.Path.GetFileName(fu_Address.FileName);
                filepath = Server.MapPath("../../Uploads/User/UTI/");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(fu_Address.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg")
                {
                    if (fu_Address.PostedFile.ContentType == "image/jpeg")
                    {
                        if (fu_Address.PostedFile.ContentLength <= 1024000)
                        {
                            fu_Address.SaveAs(filepath + ViewState["MsrNo"].ToString() + Path.GetFileName(fu_Address.FileName));
                            filepath = ViewState["MsrNo"].ToString() + Path.GetFileName(fu_Address.FileName);
                            ViewState["AddrFileName"] = filepath;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File Upload a successfully!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Image has to be less than or equal 1 Mb!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only JPEG files are accepted!');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload valid file type');", true);

                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload a file to upload');", true);
        }

    }


    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        txtFirstName.Text = string.Empty;
        txt_Email.Text = string.Empty;
        txt_Mno.Text = string.Empty;
        ViewState["IdenFileName"] = null;
        ViewState["AddrFileName"] = null;
    }

    #endregion
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (Session["dtRetailer"] != null)
        {
            if (ViewState["IdenFileName"] != null && ViewState["AddrFileName"] != null)
            {
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                List<ParmList> _lstparm = new List<ParmList>();
                _lstparm.Add(new ParmList() { name = "@Name", value = txtFirstName.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Contact_Number", value = txt_Mno.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Email", value = txt_Email.Text.Trim() });
                _lstparm.Add(new ParmList() { name = "@Iden_Proof_Filename", value = ViewState["IdenFileName"].ToString() });
                _lstparm.Add(new ParmList() { name = "@Addr_Proof_Filename", value = ViewState["AddrFileName"].ToString() });
                _lstparm.Add(new ParmList() { name = "@MsrNo", value = dtMember.Rows[0]["MsrNo"].ToString() });
                _lstparm.Add(new ParmList() { name = "@initial", value = initial });
                _lstparm.Add(new ParmList() { name = "@MemberID", value = dtMember.Rows[0]["MemberID"].ToString() });
                _lstparm.Add(new ParmList() { name = "@action", value = "insert" });
                string identityurl1 = string.Empty;
                if (ViewState["IdenFileName"] != null)
                {
                    string[] idpth = ViewState["IdenFileName"].ToString().Split('~');
                    identityurl1 = adminurl + "Uploads/User/UTI/" + idpth[0];
                }
                string adpthurl1 = string.Empty;
                if (ViewState["AddrFileName"] != null)
                {
                    string[] adpth = ViewState["AddrFileName"].ToString().Split('~');
                    adpthurl1 = adminurl + "Uploads/User/UTI/" + adpth[0];
                }
                else
                {
                    adpthurl1 = null;
                }
                string opname = "insertkyc";
                string Url = "https://ezulix.in/api/psareg_app.aspx?mobile=" + txt_Mno.Text + "&operationname=" + opname + "&MsrNo=" + dtMember.Rows[0]["MsrNo"].ToString() + "&MemberID=" + dtMember.Rows[0]["MemberID"].ToString() + "&AdminMemberID=" + adminmemberid + "&Name=" + txtFirstName.Text.Trim() + "&Email=" + txt_Email.Text.Trim() + "&Iden_Proof_Filename=" + identityurl1 + "&Addr_Proof_Filename=" + adpthurl1 + "&weburl=" + adminurl + "&initial=" + initial + "";
                WebClient wC = new WebClient();
                wC.Headers.Add("User-Agent: Other");
                string str = wC.DownloadString(Url);
                if (str.Contains("successfully"))
                {
                    Cls.select_data_dtNew("set_Psa_Reg", _lstparm);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                    DisableControl();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Occured Please Try later!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload Aadhar OR Pancard');", true);
            }
        }
    }





}


