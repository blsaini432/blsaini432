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

public partial class Root_Distributor_Vehicle_ownership : System.Web.UI.Page
{
    #region Access_Class
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();

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
                    {
                        ViewState["MemberId"] = null;
                        ViewState["MsrNo"] = null;
                        ViewState["dmtmobile"] = null;
                        ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                        ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                        string PackageID = Convert.ToString(dtMember.Rows[0]["PackageID"]);
                        decimal Amount = 0;
                        Amount = Convert.ToDecimal(cls.select_data_scalar_double("select  isnull(Amount,0.00) as Amount from[manageservices] where PackageID='" + Convert.ToInt32(PackageID) + "'and servicename='Vehicle Ownership Transfar'"));
                        lblamt.Text = Amount.ToString();
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
                dts = cls.select_data_dt("select MemberID,MemberTypeID,PackageID from tblmlm_membermaster where Msrno=" + Msrno + "");
                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                string PackageID = Convert.ToString(dts.Rows[0]["PackageID"]);
                decimal Amount = 0;
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select  isnull(Amount,0.00) as Amount from[manageservices] where PackageID='" + Convert.ToInt32(PackageID) + "'and servicename='Vehicle Ownership Transfar'"));

                if (memberID != "" && Amount > 0)
                {

                    int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                    if (result > 0)
                    {
                        string aadhar = uploadPanImage(file_adhar);
                        string insurance = uploadPanImage(fileisurance);
                        string photo = uploadPanImage(file_bank);
                        string form29 = uploadPanImage(fileform29);
                        string form30 = uploadPanImage(fileform30);
                        string rc = uploadPanImage(filerc);
                        string puc = uploadPanImage(filepuc);
                        //if (aadhar != "" && pan != "")
                        //{

                        int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                        List<ParmList> _list = new List<ParmList>();
                        string TxnID = clsm.Cyrus_GetTransactionID_New();
                        _list.Add(new ParmList() { name = "@TxnID", value = TxnID });
                        // _list.Add(new ParmList() { name = "@type", value = RadioButtonList1.SelectedValue.ToString() });
                        _list.Add(new ParmList() { name = "@MsrNo", value = Msrno });
                        _list.Add(new ParmList() { name = "@memberid", value = memberID });
                        _list.Add(new ParmList() { name = "@amount", value = Amount });
                        _list.Add(new ParmList() { name = "@fran_code", value = txt_Acno.Text });
                        _list.Add(new ParmList() { name = "@name", value = txt_name.Text });
                        _list.Add(new ParmList() { name = "@father_name", value = txt_fathername.Text });
                        _list.Add(new ParmList() { name = "@mobile", value = txt_mobile.Text });
                        _list.Add(new ParmList() { name = "@state", value = txt_state.Text });
                        _list.Add(new ParmList() { name = "@city", value = txt_city.Text });
                        //  _list.Add(new ParmList() { name = "@HP", value = txt_hp.Text });
                        _list.Add(new ParmList() { name = "@aadhar", value = aadhar });
                        _list.Add(new ParmList() { name = "@Vicialinsurance", value = insurance });
                        _list.Add(new ParmList() { name = "@photo", value = photo });
                        _list.Add(new ParmList() { name = "@Vicialrc", value = rc });
                        _list.Add(new ParmList() { name = "@puc", value = puc });
                        _list.Add(new ParmList() { name = "@form29", value = form29 });
                        _list.Add(new ParmList() { name = "@form30", value = form30 });
                        _list.Add(new ParmList() { name = "@Action", value = "I" });
                        DataTable dt = new DataTable();
                        dt = cls.select_data_dtNew("pro_VEHICLEOWNERSHIP", _list);
                        if (dt.Rows.Count > 0)
                        {

                            if (Convert.ToInt32(dt.Rows[0]["id"]) > 0)
                            {
                                clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "Vehicle ownership Request TxnID:-" + TxnID);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('Vehicleowner_report.aspx');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                        }
                        // }
                        //else { }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in Wallet!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Insufficient Balance in Wallet !');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Amount Not Set!');", true);
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