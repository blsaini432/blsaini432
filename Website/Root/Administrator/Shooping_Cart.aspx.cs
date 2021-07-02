using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;

public partial class Root_Administrator_Shooping_Cart : System.Web.UI.Page
{
    #region [Properties]
    cls_Universal objUniversal = new cls_Universal();
    DataTable dtUniversal = new DataTable();
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();

    DataTable dtMemberMaster = new DataTable();
    clsEmployee objEmployee = new clsEmployee();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblAddEdit.Text = "Add/Update Product Details";
            if (Request.QueryString["itemid"] != null)
            {
                btnSubmit.Text = "Update";
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select * from Shooping_Cart_Admin where itemid='" + Convert.ToInt32(Request.QueryString["itemid"]) + "'");
                if(dt.Rows.Count>0)
                {
                    txtdescription.Text = dt.Rows[0]["Description"].ToString();
                    txtminiumorderperunit.Text= dt.Rows[0]["Miniumorder"].ToString();
                    txtproductname.Text= dt.Rows[0]["ProductName"].ToString();
                    txt_priceperunit.Text= dt.Rows[0]["Priceperunit"].ToString();
                    txt_quanitity.Text= dt.Rows[0]["Quantity"].ToString();
                }
              
            }
            else
            {
                clear();
                btnSubmit.Text = "Submit";
            }
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["itemid"] == null)
        {
            if (Page.IsValid)
            {
                try
                {
                   
                    string img = uploadproductimage(FileUpload1);
                    List<ParmList> _list = new List<ParmList>();
                    _list.Add(new ParmList() { name = "@ProductName", value = txtproductname.Text });
                    _list.Add(new ParmList() { name = "@Img", value = img });
                    _list.Add(new ParmList() { name = "@Priceperunit", value = Convert.ToDecimal(txt_priceperunit.Text) });
                    _list.Add(new ParmList() { name = "@Quantity", value = Convert.ToDecimal(txt_quanitity.Text) });
                    _list.Add(new ParmList() { name = "@Description", value = txtdescription.Text });
                    _list.Add(new ParmList() { name = "@Miniumorder", value = txtminiumorderperunit.Text });
                    _list.Add(new ParmList() { name = "@RequestBy", value = "Admin" });
                    _list.Add(new ParmList() { name = "@Action", value = "I" });
                    string TxnID = clsm.Cyrus_GetTransactionID_New();
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dtNew("Proc_Shoopingcart_details_GetSet", _list);
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["itemid"]) > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Product  Added Successfully.!');location.replace('Shooping_Cart.aspx');", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again!');", true);
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again!');", true);

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter Valid Data');", true);
            }
        }
        else
        {
            string img = uploadproductimage(FileUpload1);
            cls.update_data("update [Shooping_Cart_Admin] set ProductName='" + txtproductname.Text + "',Img='" + img + "',Priceperunit='" + Convert.ToDecimal(txt_priceperunit.Text) + "',Quantity='" + Convert.ToDecimal(txt_quanitity.Text) + "',Description='" + txtdescription.Text + "',Miniumorder='" + txtminiumorderperunit.Text + "' where itemid='" + Convert.ToInt32(Request.QueryString["itemid"]) + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Product  Updated Successfully.!');location.replace('ViewProducts.aspx');", true);
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
        txtdescription.Text = "";
        txtminiumorderperunit.Text = "";
        txtproductname.Text = "";
        txt_priceperunit.Text = "";
        txt_quanitity.Text = "";
    }
    #endregion


    private string uploadproductimage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Uploads/ProductImage/Actual/");

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