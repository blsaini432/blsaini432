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

public partial class Root_DistributorFast_TagActivation : System.Web.UI.Page
{
    #region MyRegion
    cls_connection Cls = new cls_connection();
    clsState objState = new clsState();
    clsCity objCity = new clsCity();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtMember = (DataTable)Session["dtRetailer"];
            ViewState["MsrNo"] = null;
            ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"].ToString();
            Session["MemberId"] = dtMember.Rows[0]["MemberId"].ToString();
         

        }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (ViewState["MsrNo"] != null)
        {
            string frontimage = uploadimage(fup_frontimage);
            string backimage = uploadimage(fup_backimage);
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@TID", value = txt_tid.Text });
            _lstparm.Add(new ParmList() { name = "@FirstName", value = txt_firstname.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@LastName", value = txt_lastname.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@Mobile", value = txt_mobile.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@Email", value = txt_email.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@Vehicleclass", value = ddl_vehcileclass.SelectedValue });
            _lstparm.Add(new ParmList() { name = "@VehicleType", value = ddl_vehciletype.SelectedValue });
            _lstparm.Add(new ParmList() { name = "@VehicleReg", value = txt_vehiclereg.Text });
            _lstparm.Add(new ParmList() { name = "@ChaisisNumber", value = txt_vehichaissnumber.Text });
            _lstparm.Add(new ParmList() { name = "@Rcfront", value = frontimage });
            _lstparm.Add(new ParmList() { name = "@Rcback", value = backimage });
            _lstparm.Add(new ParmList() { name = "@RequestByMsrNo", value = Convert.ToInt32(ViewState["MsrNo"]) });
            _lstparm.Add(new ParmList() { name = "@MemberId", value = Session["memberid"].ToString() });
            _lstparm.Add(new ParmList() { name = "@action", value = 'I' });
            Cls.select_data_dtNew("sp_FasttagActivation", _lstparm);
            clear();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('FastTagActivationReport.aspx');", true);
          
        }
    }


    public void clear()
    {
        txt_email.Text = txt_firstname.Text = txt_lastname.Text = txt_mobile.Text = txt_tid.Text = txt_vehichaissnumber.Text = txt_vehiclereg.Text = "";
        ddl_vehcileclass.SelectedIndex = 0;
        ddl_vehciletype.SelectedIndex = 0;
    }
    private string uploadimage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Uploads/FastTagRequest/Actual/");
            
                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }

                //Check file extension (must be PDF)
                string Extension = System.IO.Path.GetExtension(_fup.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
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


