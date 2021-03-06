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


public partial class Root_Retailer_ManageMemberMaster : System.Web.UI.Page
{
    #region [Properties]

    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();
    clsCountry objCountry = new clsCountry();
    clsState objState = new clsState();
    clsCity objCity = new clsCity();
    clsMLM_Package objPackage = new clsMLM_Package();
    clsMLM_MemberTree objMemberTree = new clsMLM_MemberTree();
    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();
    cls_connection cls = new cls_connection();

    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillCountry();
            if (Session["dtRetailer"] != null)
            {
                dtMemberMaster = (DataTable)Session["dtRetailer"];
                if (dtMemberMaster.Rows.Count > 0)
                {
                    fillPackage();
                    FillData(Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]));
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }
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
        try
        {
            if (Session["dtRetailer"] != null)
            {
                dtMemberMaster = (DataTable)Session["dtRetailer"];

                DataTable dmt = new DataTable();
                dmt = objMemberMaster.ManageMemberMaster("Get", Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]));
                if (dmt.Rows.Count > 0)
                {
                    string strimage = profilepicupload(fupmppic);
                    DataTable dt = new DataTable();
                    List<ParmList> _lstparm = new List<ParmList>();
                    _lstparm.Add(new ParmList() { name = "@FirstName", value = txtFirstName.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@ShopName", value = txt_ShopName.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@LastName", value = txtLastName.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@STDCode", value = txtSTDCode.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@Landline", value = txtLadline.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@Address", value = txtAddress.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@CountryID", value = Convert.ToInt32(ddlCountryName.SelectedValue) });
                    _lstparm.Add(new ParmList() { name = "@StateID", value = Convert.ToInt32(ddlStateName.SelectedValue) });
                    _lstparm.Add(new ParmList() { name = "@CityID", value = Convert.ToInt32(ddlCityName.SelectedValue) });
                    _lstparm.Add(new ParmList() { name = "@CityName", value = ddlCityName.SelectedItem.ToString() });
                    _lstparm.Add(new ParmList() { name = "@ZIP", value = txtZIP.Text.Trim() });
                    if (strimage == "")
                    {
                        _lstparm.Add(new ParmList() { name = "@memberImage", value = Convert.ToString(dtMemberMaster.Rows[0]["MemberImage"]) });
                    }
                    else
                    {
                        _lstparm.Add(new ParmList() { name = "@memberImage", value = strimage });
                    }
                    _lstparm.Add(new ParmList() { name = "@aadhar", value = txt_aadhar.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@pan", value = txt_PAN.Text.Trim() });
                  //  _lstparm.Add(new ParmList() { name = "@bankac", value = txt_accountnumber.Text.Trim() });
                  //  _lstparm.Add(new ParmList() { name = "@bankifsc", value = txt_ifsc.Text.Trim() });
                  //  _lstparm.Add(new ParmList() { name = "@bankname", value = txt_bankname.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@companypan", value = txt_businesspan.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@gstno", value = txt_gstnumber.Text.Trim() });
                    _lstparm.Add(new ParmList() { name = "@action", value = "U" });
                    _lstparm.Add(new ParmList() { name = "@MsrNo", value = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"])});
                    dt = cls.select_data_dtNew("ProcMLM_AddNewMemberMaster ", _lstparm);
                    if (dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record Updated successfully');location.replace('ManageMemberMaster.aspx');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Does not Exists !');", true);
                }
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }

        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Occured Please Try after reload it.');", true);
        }

    }
    #region [All Function-FillDate,Clear]
    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objMemberMaster.ManageMemberMaster("Get", id);
        if (dt.Rows.Count > 0)
        {

            txtFirstName.Text = Convert.ToString(dt.Rows[0]["FirstName"]);
            txtLastName.Text = Convert.ToString(dt.Rows[0]["LastName"]);
            txtEmail.Text = Convert.ToString(dt.Rows[0]["Email"]);
            txtMobile.Text = Convert.ToString(dt.Rows[0]["Mobile"]);
            txtSTDCode.Text = Convert.ToString(dt.Rows[0]["STDCode"]);
            txtLadline.Text = Convert.ToString(dt.Rows[0]["Ladline"]);
            txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
            txtZIP.Text = Convert.ToString(dt.Rows[0]["ZIP"]);
            ddlCountryName.SelectedValue = Convert.ToString(dt.Rows[0]["CountryID"]);
            fillState(Convert.ToInt32(dt.Rows[0]["CountryID"]));
            ddlStateName.SelectedValue = Convert.ToString(dt.Rows[0]["StateID"]);
            fillCity(Convert.ToInt32(dt.Rows[0]["StateID"]));
            ddlCityName.SelectedValue = Convert.ToString(dt.Rows[0]["CityID"]);
            lblmembertype.Text = Convert.ToString(dt.Rows[0]["MemberType"]);
            ddlPackage.SelectedValue = dt.Rows[0]["PackageID"].ToString();
            txtMobile.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtEmail.Enabled = false;
            imgUser.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["MemberImage"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/User/Profile/" + Convert.ToString(dt.Rows[0]["MemberImage"]);
          //  txt_accountnumber.Text = Convert.ToString(dt.Rows[0]["bankac"]);
          //  txt_ifsc.Text = Convert.ToString(dt.Rows[0]["bankifsc"]);
          //  txt_bankname.Text = Convert.ToString(dt.Rows[0]["bankname"]);

        }
    }

    public void fillPackage()
    {
        DataTable dtPackage = new DataTable();
        dtPackage = objPackage.ManagePackage("GetByMsrNo", Convert.ToInt32(Session["RetailerMhsrNo"]));
        ddlPackage.DataSource = dtPackage;
        ddlPackage.DataValueField = "PackageID";
        ddlPackage.DataTextField = "PackageName";
        ddlPackage.DataBind();
        ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
    }

    public void fillCountry()
    {
        DataTable dtCountry = new DataTable();
        dtCountry = objCountry.ManageCountry("Get", 0);
        ddlCountryName.DataSource = dtCountry;
        ddlCountryName.DataValueField = "CountryID";
        ddlCountryName.DataTextField = "CountryName";
        ddlCountryName.DataBind();
        //ddlCountryName.Items.Insert(0, new ListItem("Select Country", "0"));
        fillState(1);
    }

    public void fillState(int CountryID)
    {
        DataTable dtState = new DataTable();
        dtState = objState.ManageState("GetByCountryID", CountryID);
        ddlStateName.DataSource = dtState;
        ddlStateName.DataValueField = "StateID";
        ddlStateName.DataTextField = "StateName";
        ddlStateName.DataBind();
        ddlStateName.Items.Insert(0, new ListItem("Select State", "0"));
    }

    public void fillCity(int StateID)
    {
        DataTable dtCity = new DataTable();
        dtCity = objCity.ManageCity("GetByStateID", StateID);
        ddlCityName.DataSource = dtCity;
        ddlCityName.DataValueField = "CityID";
        ddlCityName.DataTextField = "CityName";
        ddlCityName.DataBind();
        ddlCityName.Items.Insert(0, new ListItem("Select City", "0"));
    }

    #endregion
    private string profilepicupload(FileUpload fpup)
    {
        string fullpath = string.Empty;
        if (fpup.HasFile == true)
        {
            if (fpup.PostedFile.FileName != "")
            {
                string filename = System.IO.Path.GetFileName(fpup.FileName);
                string opath = Server.MapPath("../../Uploads/User/Profile/");
                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(fpup.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                {
                    fullpath = DateTime.Now.Ticks.ToString() + Extension;
                    fpup.PostedFile.SaveAs(opath + fullpath);
                    return fullpath;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload valid file type');", true);
                    return fullpath;
                }
            }
        }
        return "";
    }


    protected void ddlCountryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillState(Convert.ToInt32(ddlCountryName.SelectedValue));
    }
    protected void ddlStateName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlStateName.SelectedItem.Value) != 0)
        {
            fillCity(Convert.ToInt32(ddlStateName.SelectedValue));
        }
        else
        {
            ddlCityName.Items.Clear();
            ddlCityName.Items.Insert(0, new ListItem("Select City", "0"));
        }
    }



}