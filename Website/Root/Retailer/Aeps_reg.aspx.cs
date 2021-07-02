using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.IO;
using System.Configuration;
public partial class Root_Retailer_Aeps_reg : System.Web.UI.Page
{
    #region MyRegion
    cls_connection Cls = new cls_connection();
    clsState objState = new clsState();
    clsCity objCity = new clsCity();
    public static string adminmemberid = ConfigurationManager.AppSettings["adminmemberid"];
    private static string adminurl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
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
                FillState();
                FillIdentity_Address();
                DataTable dtChk = Cls.select_data_dt(@"SELECT * FROM Tbl_Aeps_Reg WHERE MsrNo='" + dtMember.Rows[0]["MsrNo"].ToString() + "'");
                if (dtChk.Rows.Count > 0)
                {
                    if (dtChk.Rows[0]["Statu"].ToString() == "Rejected")
                    {
                        lbl_Status.Visible = true;
                        lbl_Status.Text = "Your AEPS KYC is Rejected Reason " + dtChk.Rows[0]["rejection"].ToString() + "";
                    }
                    else
                    {
                        txtFirstName.Text = dtChk.Rows[0]["F_Name"].ToString();
                        txtLastName.Text = dtChk.Rows[0]["L_Name"].ToString();
                        txt_Shopname.Text = dtChk.Rows[0]["Shop_Name"].ToString();
                        txt_Pan.Text = dtChk.Rows[0]["Pan_Number"].ToString();
                        txt_Mno.Text = dtChk.Rows[0]["Contact_Number"].ToString();
                        ddtPriState.SelectedItem.Text = dtChk.Rows[0]["P_State"].ToString();
                        ddlPriCity.SelectedItem.Text = dtChk.Rows[0]["P_City"].ToString();
                        txt_PriAddress.Text = dtChk.Rows[0]["P_Address"].ToString();
                        txt_PriPin.Text = dtChk.Rows[0]["P_Pin"].ToString();
                        ddl_Address.SelectedItem.Text = dtChk.Rows[0]["Addr_Proof_Type"].ToString();
                        txt_AddProofnumber.Text = dtChk.Rows[0]["Addr_Proof_Num"].ToString();
                        txt_SelftDeclaNumber.Text = dtChk.Rows[0]["Self_Decl_Num"].ToString();
                        DisableControl();
                        lbl_Status.Visible = true;
                        lbl_Status.Text = "Your AEPS KYC status is " + dtChk.Rows[0]["Statu"].ToString() + "";
                    }
                }
                else
                {
                    lbl_Status.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }


    private void DisableControl()
    {
        txtFirstName.Enabled = false;
        txtLastName.Enabled = false;
        txt_Shopname.Enabled = false;
        txt_Pan.Enabled = false;
        txt_Mno.Enabled = false;
        ddtPriState.Enabled = false;
        ddlPriCity.Enabled = false;
        txt_PriAddress.Enabled = false;
        txt_PriPin.Enabled = false;
        ddl_Address.Enabled = false;
        txt_AddProofnumber.Enabled = false;
        txt_SelftDeclaNumber.Enabled = false;
        btn_Address.Visible = false;
        btn_Sefdownlod.Visible = false;
        btn_SelDec.Visible = false;
        btn_Reset.Visible = false;
    }
    private void FillIdentity_Address()
    {
        DataTable dt = new DataTable();
        dt = Cls.select_data_dt(@"SELECT * FROM Tbl_Aeps_Identity WHERE IsActive=1");
        ddl_Address.DataSource = dt;
        ddl_Address.DataValueField = "Id";
        ddl_Address.DataTextField = "Id_Type";
        ddl_Address.DataBind();
        ddl_Address.Items.Insert(0, new ListItem("Select", "0"));
    }
    private void FillState()
    {
        DataTable dtState = new DataTable();
        dtState = objState.ManageState("GetByCountryID", 1);
        ddtPriState.DataSource = dtState;
        ddtPriState.DataValueField = "StateID";
        ddtPriState.DataTextField = "StateName";
        ddtPriState.DataBind();
        ddtPriState.Items.Insert(0, new ListItem("Select State", "0"));
    }

    public void fillCity(int StateID, DropDownList ddl)
    {
        DataTable dtCity = new DataTable();
        dtCity = objCity.ManageCity("GetByStateID", StateID);
        ddl.DataSource = dtCity;
        ddl.DataValueField = "CityID";
        ddl.DataTextField = "CityName";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select City", "0"));
    }

    #region Events
    protected void ddtPriState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddtPriState.SelectedItem.Value) != 0)
        {
            fillCity(Convert.ToInt32(ddtPriState.SelectedValue), ddlPriCity);
        }
    }
    protected void btn_Address_Click(object sender, EventArgs e)
    {
        string filepath = "";
        ViewState["AddrFileName"] = null;
        string folderPath = Server.MapPath("../../Uploads/AEPS/");
        if (fu_Address.HasFile)
        {
            if (fu_Address.PostedFile.ContentType == "image/jpeg")
            {
                if (fu_Address.PostedFile.ContentLength <= 1024000)
                {
                    fu_Address.SaveAs(folderPath + ViewState["MsrNo"].ToString() + ddl_Address.SelectedValue + ".jpeg");
                    filepath = adminurl+ "/Uploads/AEPS/" + ViewState["MsrNo"].ToString() + ddl_Address.SelectedValue + ".jpeg";
                   
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select a file!');", true);
        }
    }
    protected void btn_SelDec_Click(object sender, EventArgs e)
    {
        string filepath = "";
        ViewState["Self"] = null;
        string folderPath = Server.MapPath("../../Uploads/AEPS/");
        if (fu_SelfDec.HasFile)
        {
            if (fu_SelfDec.PostedFile.ContentType == "application/pdf")
            {
                if (fu_SelfDec.PostedFile.ContentLength <= 512000)
                {
                    fu_SelfDec.SaveAs(folderPath + ViewState["MsrNo"].ToString() + ".pdf");
                    filepath = adminurl + "/Uploads/AEPS/" + ViewState["MsrNo"].ToString() + ".pdf";
                    ViewState["Self"] = filepath;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File Upload a successfully!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Image has to be less than or equal 500 kb!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only pdf files are accepted!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select a file!');", true);
        }
    }
    protected void btn_Sefdownlod_Click(object sender, EventArgs e)
    {
        Response.ContentType = "Application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Self_Declaration_Form.pdf");
        Response.TransmitFile(Server.MapPath("~/Download/Self_Declaration_Form.pdf"));
        Response.End();
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        txtFirstName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        txt_Shopname.Text = string.Empty;
        txt_Pan.Text = string.Empty;
        txt_Mno.Text = string.Empty;
        ddtPriState.SelectedValue = "0";
        ddlPriCity.SelectedValue = "0";
        txt_PriAddress.Text = string.Empty;
        txt_PriPin.Text = string.Empty;
        ddl_Address.SelectedValue = "0";
        txt_AddProofnumber.Text = string.Empty;
        txt_SelftDeclaNumber.Text = string.Empty;
        ViewState["IdenFileName"] = null;
        ViewState["AddrFileName"] = null;
        ViewState["Self"] = null;
    }



    #endregion
    

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        if (ViewState["AddrFileName"] != null)
        {
            DataTable dtMember = (DataTable)Session["dtRetailer"];
            DataTable dt = Cls.select_data_dt(@"SELECT * FROM Tbl_Aeps_Reg WHERE MsrNo=" + dtMember.Rows[0]["MsrNo"].ToString() + "");
            if (dt.Rows.Count > 0)
            {
                Cls.select_data_dt(@"DELETE FROM Tbl_Aeps_Reg WHERE MsrNo=" + dtMember.Rows[0]["MsrNo"].ToString() + "");
            }
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@F_Name", value = txtFirstName.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@L_Name", value = txtLastName.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@Shop_Name", value = txt_Shopname.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@Pan_Number", value = txt_Pan.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@Contact_Number", value = txt_Mno.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@P_State", value = ddtPriState.SelectedItem.Text });
            _lstparm.Add(new ParmList() { name = "@P_City", value = ddlPriCity.SelectedItem.Text });
            _lstparm.Add(new ParmList() { name = "@P_Address", value = txt_PriAddress.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@P_Pin", value = txt_PriPin.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@Addr_Proof_Type", value = ddl_Address.SelectedItem.Text });
            _lstparm.Add(new ParmList() { name = "@Addr_Proof_Num", value = txt_AddProofnumber.Text.Trim() });
            _lstparm.Add(new ParmList() { name = "@Addr_Proof_Filename", value = ViewState["AddrFileName"].ToString() });
            _lstparm.Add(new ParmList() { name = "@Self_Decl_Num", value = txt_SelftDeclaNumber.Text.Trim() });
            if (ViewState["Self"] != null)
                _lstparm.Add(new ParmList() { name = "@Self_Decl_Filename", value = ViewState["Self"].ToString() });
            else
                _lstparm.Add(new ParmList() { name = "@Self_Decl_Filename", value = "" });
            _lstparm.Add(new ParmList() { name = "@MsrNo", value = dtMember.Rows[0]["MsrNo"].ToString() });
            _lstparm.Add(new ParmList() { name = "@MemberID", value = dtMember.Rows[0]["MemberID"].ToString() });
            Cls.select_data_dtNew("Set_Aeps_Reg", _lstparm);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('AEPS Registrantion is successfully, Your Registrantion status is pending!');", true);
            DisableControl();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Upload Address proof!');", true);
        }
    }
}


