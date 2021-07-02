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

public partial class Root_Retailer_Vistore : System.Web.UI.Page
{
    #region Access_Class
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();

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
                    {
                        ViewState["MemberId"] = null;
                        ViewState["MsrNo"] = null;
                        ViewState["dmtmobile"] = null;
                        ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                        ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                      
                        string PackageID = Convert.ToString(dtMember.Rows[0]["PackageID"]);

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
        try
        {
            if (ViewState["MsrNo"] != null)
            {
                int Msrno = Convert.ToInt32(ViewState["MsrNo"]);
                DataTable dts = new DataTable();
                dts = cls.select_data_dt("select * from tblmlm_membermaster where Msrno=" + Msrno + "");
                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);            
                string mobile = Convert.ToString(dts.Rows[0]["Mobile"]);
                string member_name = Convert.ToString(dts.Rows[0]["FirstName"]);
                int MsrNo = Convert.ToInt32(ViewState["MsrNo"]);
                List<ParmList> _list = new List<ParmList>();                
                _list.Add(new ParmList() { name = "@product_name", value = ddlservice.SelectedValue.ToString() });
                _list.Add(new ParmList() { name = "@MsrNo", value = Msrno });
                _list.Add(new ParmList() { name = "@memberid", value = memberID });
                _list.Add(new ParmList() { name = "@mobile", value = mobile });
                _list.Add(new ParmList() { name = "@member_name", value = member_name });
                _list.Add(new ParmList() { name = "@Action", value = "I" });
                DataTable dt = new DataTable();
                dt = cls.select_data_dtNew("pro_vistore", _list);
                if (dt.Rows.Count > 0)
                {

                    if (Convert.ToInt32(dt.Rows[0]["id"]) > 0)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Received Admin Team Successfully.!');location.replace('Vistore.aspx');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('id Not Set!');", true);
            }



        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
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


}