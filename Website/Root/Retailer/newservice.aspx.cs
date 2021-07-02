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
public partial class Root_Retailer_newservice : System.Web.UI.Page
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
            
                        string aadhar = uploadPanImage(file_adhar);
                        string pan = uploadPanImage(file_pan);
                        string bank = uploadPanImage(file_bank);
                        //if (aadhar != "" && pan != "")
                        //{

                        int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                        List<ParmList> _list = new List<ParmList>();
                        string TxnID = clsm.Cyrus_GetTransactionID_New();
                        _list.Add(new ParmList() { name = "@TxnID", value = TxnID });
                        _list.Add(new ParmList() { name = "@type", value = RadioButtonList1.SelectedValue.ToString() });
                      //  _list.Add(new ParmList() { name = "@MsrNo", value = Msrno });
                     //   _list.Add(new ParmList() { name = "@memberid", value = memberID });
                        _list.Add(new ParmList() { name = "@fran_code", value = txt_Acno.Text });
                        _list.Add(new ParmList() { name = "@name", value = txt_name.Text });
                        _list.Add(new ParmList() { name = "@father_name", value = txt_fathername.Text });
                        _list.Add(new ParmList() { name = "@mobile", value = txt_mobile.Text });
                        _list.Add(new ParmList() { name = "@state", value = txt_state.Text });
                        _list.Add(new ParmList() { name = "@city", value = txt_city.Text });
                        _list.Add(new ParmList() { name = "@HP", value = txt_hp.Text });
                        _list.Add(new ParmList() { name = "@aadhar", value = aadhar });
                        _list.Add(new ParmList() { name = "@pan", value = pan });
                        _list.Add(new ParmList() { name = "@bankpassbook", value = bank });
                        _list.Add(new ParmList() { name = "@Action", value = "I" });
                        DataTable dt = new DataTable();
                        dt = cls.select_data_dtNew("pro_solar", _list);
                        if (dt.Rows.Count > 0)
                        {

                            if (Convert.ToInt32(dt.Rows[0]["id"]) > 0)
                            {
                               // clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "solar energy Request TxnID:-" + TxnID);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('Solarenergy_report.aspx');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                        }
                        // }
                        //else { }


                 



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