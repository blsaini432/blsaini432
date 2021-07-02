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
public partial class Root_Distributor_Jobsforyou : System.Web.UI.Page
{

    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    private int checksumValue;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtDistributor"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataTable dtMember = (DataTable)Session["dtDistributor"];
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
                List<ParmList> _list = new List<ParmList>();
                if (ViewState["IdenFileName"] != null)
                {
                    string photo = ViewState["IdenFileName"].ToString();
                    _list.Add(new ParmList() { name = "@MsrNo", value = Msrno });
                    _list.Add(new ParmList() { name = "@memberID", value = memberID });
                    _list.Add(new ParmList() { name = "@Working", value = txt_working.Text });
                    _list.Add(new ParmList() { name = "@NAME", value = txt_cardname.Text });
                    _list.Add(new ParmList() { name = "@DOB", value = txt_date.Text });
                    _list.Add(new ParmList() { name = "@Experinces", value = txt_experance.Text });
                    _list.Add(new ParmList() { name = "@mobile", value = txt_mobile.Text });
                    _list.Add(new ParmList() { name = "@Last_Salary", value = txt_salary.Text });
                    _list.Add(new ParmList() { name = "@full_address", value = txt_address.Text });
                    _list.Add(new ParmList() { name = "@Want_Salary", value = txt_wantsalary.Text });
                    _list.Add(new ParmList() { name = "@doing_currently", value = rdoBtnsLstGender.SelectedValue.ToString() });
                    _list.Add(new ParmList() { name = "@Photo", value = photo });
                    _list.Add(new ParmList() { name = "@Action", value = "I" });
                    string TxnID = Clsm.Cyrus_GetTransactionID_New();
                    _list.Add(new ParmList() { name = "@txn", value = TxnID });
                    DataTable dt = new DataTable();
                    dt = Cls.select_data_dtNew("Proc_jobsforyou", _list);
                    if (dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Successfull!');location.replace('Jobus_report.aspx');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Upload Photo!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('session null!');", true);
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    protected void btn_Identity_Click(object sender, EventArgs e)
    {
        string filepath = string.Empty;
        if (fu_Identity.HasFile == true)
        {
            if (fu_Identity.PostedFile.FileName != "")
            {
                string filename = System.IO.Path.GetFileName(fu_Identity.FileName);
                filepath = Server.MapPath("../../Uploads/Servicesimage/Actual/");
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
                            fu_Identity.SaveAs(filepath + ViewState["MsrNo"].ToString() + Path.GetFileName(fu_Identity.FileName));
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

                //Check file extension (must be PDF)
                string Extension = System.IO.Path.GetExtension(_fup.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                    if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                    {
                        objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
                        objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);
                    }

                    return FileName;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select JPG/JPEG/PNG/GIF/PDF File Only!');", true);

                }
            }
        }
        else
        {
            return "";
        }

        return "";
    }

}