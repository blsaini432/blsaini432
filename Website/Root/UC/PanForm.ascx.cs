using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Root_UC_PanForm : System.Web.UI.UserControl
{
    public static int msrno { get; set; }

    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txt_Rdate.Text = DateTime.Today.ToShortDateString();
            
            ViewState["MemberMsrNo"] = msrno;

            if (ViewState["MemberMsrNo"] != null)
            {
                int Msrno = Convert.ToInt32(ViewState["MemberMsrNo"]);
                DataTable dts = new DataTable();
                dts = cls.select_data_dt("select MemberID,MemberTypeID,PackageID from tblmlm_membermaster where Msrno=" + Msrno + "");

                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                string PackageID = Convert.ToString(dts.Rows[0]["PackageID"]);
                decimal Amount = 0;
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select  isnull(Amount,0.00) as Amount from[panfeesettings] where PackageID=" + Convert.ToInt32(PackageID)));
                lblamt.Text = Amount.ToString();
            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["MemberMsrNo"] != null)
                {
                    int Msrno = Convert.ToInt32(ViewState["MemberMsrNo"]);
                    DataTable dts = new DataTable();
                    dts = cls.select_data_dt("select MemberID,MemberTypeID,PackageID from tblmlm_membermaster where Msrno=" + Msrno + "");

                    string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                    string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                    string PackageID = Convert.ToString(dts.Rows[0]["PackageID"]);
                    decimal Amount = 0;
                    Amount = Convert.ToDecimal(cls.select_data_scalar_double("select  isnull(Amount,0.00) as Amount from[panfeesettings] where PackageID=" + Convert.ToInt32(PackageID)));

                    if (memberID != "" && Amount > 0)
                    {

                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {

                            string IdentiyImg = uploadPanImage(FileUploadIdentityImage);
                         //   string Addressproff = uploadPanImage(FileUploadadressImage);
                           string Dobimg = "";
                          //  string Form = uploadPanImage(fupForm49);
                          string FormA = "";
                            if (IdentiyImg != ""  && IdentiyImg != "")
                            {
                                //if (ddl_addresproff.SelectedValue != ddl_dobproff.SelectedValue)
                                //        {
                                if (txtTxnNo.Text == "" && txtTxnNo.Text == string.Empty)
                                {
                                    string TxnID = clsm.Cyrus_GetTransactionID_New();
                                    clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "PanCard Request TxnID:-" + TxnID);
                                    int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                                    List<ParmList> _list = new List<ParmList>();
                                    _list.Add(new ParmList() { name = "@TxnID", value = TxnID });
                                  //  clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "PanCard Request TxnID:-" + TxnID);
                                   // _list.Add(new ParmList() { name = "@Gender", value = rdoBtnLstGender.SelectedItem.Text });
                                    _list.Add(new ParmList() { name = "@RequestType", value = ddl_pancat.SelectedValue.ToString() });
                                    _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                                    _list.Add(new ParmList() { name = "@PanNo", value = txt_panNo.Text });
                                    _list.Add(new ParmList() { name = "@NameOnPAN", value = txt_nameonpan.Text });
                                    //_list.Add(new ParmList() { name = "@AOCODE", value = TXT_AOCODE.Text });
                                    _list.Add(new ParmList() { name = "@ApplicantFristName", value = txt_fristname.Text });
                                    _list.Add(new ParmList() { name = "@ApplicantMiddleName", value = txt_Middlename.Text });
                                    _list.Add(new ParmList() { name = "@ApplicantLastName", value = txt_lastname.Text });
                                   _list.Add(new ParmList() { name = "@FatherFristName", value = txt_ffristname.Text });
                                   _list.Add(new ParmList() { name = "@FatherMiddleName", value = txt_Fmiddlename.Text });
                                 _list.Add(new ParmList() { name = "@FatherLastName", value = txt_flastname.Text });

                                  //  _list.Add(new ParmList() { name = "@ApplicantFristName1", value = txt_oldfristname.Text });
                                  //  _list.Add(new ParmList() { name = "@ApplicantMiddleName1", value = txt_oldmiddlename.Text });
                                  //  _list.Add(new ParmList() { name = "@ApplicantLastName1", value = txt_oldlastname.Text });
                                  //  _list.Add(new ParmList() { name = "@FatherFristName1", value = txt_ffristname1.Text });
                                  //  _list.Add(new ParmList() { name = "@FatherMiddleName1", value = txt_Fmiddlename1.Text });
                                  //  _list.Add(new ParmList() { name = "@FatherLastName1", value = txt_flastname1.Text });
                                    _list.Add(new ParmList() { name = "@DOB", value = txt_dob.Text });
                                  //  _list.Add(new ParmList() { name = "@ContactISD", value = txt_isdcode.Text });

                                  //  _list.Add(new ParmList() { name = "@ContactSTD", value = txt_std.Text });
                                    _list.Add(new ParmList() { name = "@ContactNo", value = txt_telno.Text });
                                    _list.Add(new ParmList() { name = "@Email", value = txtEmail.Text });

                                    _list.Add(new ParmList() { name = "@AdharNo", value = txt_adhar.Text });
                                    _list.Add(new ParmList() { name = "@ResidenceAdd", value = txt_resiadd.Text });
                                    _list.Add(new ParmList() { name = "@CommunicationAdd", value = txt_cadd.Text });

                                  //  _list.Add(new ParmList() { name = "@IdentityType", value = ddl_Identitytype.SelectedValue.ToString() });
                                   _list.Add(new ParmList() { name = "@IdentityImage", value = IdentiyImg });
                                  //  _list.Add(new ParmList() { name = "@AddressType", value = ddl_addresproff.SelectedValue.ToString() });
                                  //  _list.Add(new ParmList() { name = "@AddressImage", value = Addressproff });
                                  _list.Add(new ParmList() { name = "@DOBType", value = '1' });
                                  _list.Add(new ParmList() { name = "@DOBImage", value = Dobimg });
                                   // _list.Add(new ParmList() { name = "@form", value = Form });
                                   _list.Add(new ParmList() { name = "@formA", value = FormA });
                                  

                                    if (lblamt.Text != "")
                                    {
                                        _list.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(Amount) });
                                    }
                                    else
                                    {
                                        _list.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(Amount) });
                                    }
                                
                                    if (ddl_applicationtype.SelectedValue.ToString() == "update")
                                    {
                                        _list.Add(new ParmList() { name = "@Action", value = "U" });
                                    }
                                    else
                                    {
                                        _list.Add(new ParmList() { name = "@Action", value = "I" });
                                    }
                                    DataTable dt = new DataTable();
                                    dt = cls.select_data_dtNew("Proc_PanCardDetails_GetSet", _list);
                                    if (dt.Rows.Count > 0)
                                    {
                                      
                                        if (Convert.ToInt32(dt.Rows[0]["kid"]) > 0)
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request send Successfully.!');location.replace('PanReport.aspx');", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                                    }
                                }
                                else
                                {
                                    List<ParmList> _list = new List<ParmList>();
                                    int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);

                                    _list.Add(new ParmList() { name = "@Gender", value = rdoBtnLstGender.SelectedItem.Text });
                                    _list.Add(new ParmList() { name = "@RequestType", value = ddl_pancat.SelectedValue.ToString() });
                                    _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                                    _list.Add(new ParmList() { name = "@PanNo", value = txt_panNo.Text });
                                    _list.Add(new ParmList() { name = "@NameOnPAN", value = txt_nameonpan.Text });
                                    _list.Add(new ParmList() { name = "@ApplicantFristName", value = txt_fristname.Text });
                                    _list.Add(new ParmList() { name = "@ApplicantMiddleName", value = txt_Middlename.Text });
                                    _list.Add(new ParmList() { name = "@ApplicantLastName", value = txt_lastname.Text });
                                   _list.Add(new ParmList() { name = "@FatherFristName", value = txt_ffristname.Text });
                                   _list.Add(new ParmList() { name = "@FatherMiddleName", value = txt_Fmiddlename.Text });
                                    _list.Add(new ParmList() { name = "@FatherLastName", value = txt_flastname.Text });

                                    _list.Add(new ParmList() { name = "@DOB", value = txt_dob.Text });
                                  //  _list.Add(new ParmList() { name = "@ContactISD", value = txt_isdcode.Text });

                                 //   _list.Add(new ParmList() { name = "@ContactSTD", value = txt_std.Text });
                                    _list.Add(new ParmList() { name = "@ContactNo", value = txt_telno.Text });
                                    _list.Add(new ParmList() { name = "@Email", value = txtEmail.Text });

                                    _list.Add(new ParmList() { name = "@AdharNo", value = txt_adhar.Text });
                                    _list.Add(new ParmList() { name = "@ResidenceAdd", value = txt_resiadd.Text });
                                    _list.Add(new ParmList() { name = "@CommunicationAdd", value = txt_cadd.Text });

                                   // _list.Add(new ParmList() { name = "@IdentityType", value = ddl_Identitytype.SelectedValue.ToString() });
                                    _list.Add(new ParmList() { name = "@IdentityImage", value = IdentiyImg });
                                 //   _list.Add(new ParmList() { name = "@AddressType", value = ddl_addresproff.SelectedValue.ToString() });
                                 //   _list.Add(new ParmList() { name = "@AddressImage", value = Addressproff });
                                //    _list.Add(new ParmList() { name = "@DOBType", value = ddl_dobproff.SelectedValue.ToString() });
                                 //   _list.Add(new ParmList() { name = "@DOBImage", value = Dobimg });
                               //     _list.Add(new ParmList() { name = "@form", value = Form });
                                  //  _list.Add(new ParmList() { name = "@formA", value = FormA });
                                    _list.Add(new ParmList() { name = "@TxnID", value = txtTxnNo.Text });

                                    _list.Add(new ParmList() { name = "@Action", value = "Update" });

                                    DataTable dt = new DataTable();
                                    dt = cls.select_data_dtNew("Proc_PanCardDetails_GetSet", _list);
                                    if (dt.Rows.Count > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Data Updated Successfully.!');location.replace('PanCorrectionForm.aspx');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                                    }
                                }
                                //}
                                //else
                                //{
                                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Address and DOB proof cannot be same, please select a different type of document.!');", true);
                                //}                                           

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select PDF File Only!');", true);
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


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
            }


        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Sefdownlod_Click(object sender, EventArgs e)
    {
        Response.ContentType = "Application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=Form49A(1)");
        Response.TransmitFile(Server.MapPath("~/Download/Form49A(1)"));
        Response.End();
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

    //protected void chk_check(object sender, EventArgs e)
    //{
    //    if (chk_same.Checked && txt_resiadd.Text != "")
    //    {
    //        txt_cadd.Text = txt_resiadd.Text;
    //    }
    //}
    protected void ddl_applicationtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //pan1.Visible = false;
        //pan2.Visible = false;
        //pan3.Visible = false;
        //pan5.Visible = false;
        //panval1.Enabled = false;
        //panval2.Enabled = false;
        //panval3.Enabled = false;
        //panval4.Enabled = false;
        //panval5.Enabled = false;
        trTxnNo.Visible = false;

        if (ddl_applicationtype.SelectedValue.ToString() == "update")
        {
            //pan1.Visible = true;
            //pan2.Visible = true;
            //pan3.Visible = true;
            //pan5.Visible = true;
            //panval1.Enabled = true;
            //panval2.Enabled = true;
            //panval3.Enabled = true;
            //panval4.Enabled = true;
            //panval5.Enabled = true;
            trTxnNo.Visible = true;
        }


    }
    protected void txtTxnNo_TextChanged(object sender, EventArgs e)
    {
        if (txtTxnNo.Text != "" && txtTxnNo.Text != string.Empty)
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("Select * from pancarddetails where txnID='" + Convert.ToInt64(txtTxnNo.Text) + "' and RequestStatus in ('Pending','Temp Rejected')");
            if (dt.Rows.Count > 0)
            {
                txt_Rdate.Text = DateTime.Today.ToShortDateString();
              //  txt_panNo.Text = Convert.ToString(dt.Rows[0]["PanNo"]);
                txt_fristname.Text = Convert.ToString(dt.Rows[0]["ApplicantFristName"]);
                txt_Middlename.Text = Convert.ToString(dt.Rows[0]["ApplicantMiddleName"]);
                txt_lastname.Text = Convert.ToString(dt.Rows[0]["ApplicantLastName"]);
                txt_nameonpan.Text = Convert.ToString(dt.Rows[0]["NameOnPAN"]);
              //  txt_ffristname.Text = Convert.ToString(dt.Rows[0]["FatherFristName"]);
              //  txt_Fmiddlename.Text = Convert.ToString(dt.Rows[0]["FatherMiddleName"]);
                txt_flastname.Text = Convert.ToString(dt.Rows[0]["FatherLastName"]);
                txt_dob.Text = Convert.ToString(dt.Rows[0]["DOB"]);
              //  txt_isdcode.Text = Convert.ToString(dt.Rows[0]["ContactISD"]);
             //   txt_std.Text = Convert.ToString(dt.Rows[0]["ContactSTD"]);
                txt_telno.Text = Convert.ToString(dt.Rows[0]["ContactNo"]);
                txtEmail.Text = Convert.ToString(dt.Rows[0]["Email"]);
                txt_adhar.Text = Convert.ToString(dt.Rows[0]["AdharNo"]);
                txt_resiadd.Text = Convert.ToString(dt.Rows[0]["ResidenceAdd"]);
                txt_cadd.Text = Convert.ToString(dt.Rows[0]["CommunicationAdd"]);
             //   ddl_Identitytype.SelectedValue = Convert.ToString(dt.Rows[0]["IdentityType"]);
              //  ddl_addresproff.SelectedValue = Convert.ToString(dt.Rows[0]["AddressType"]);
              //  ddl_dobproff.SelectedValue = Convert.ToString(dt.Rows[0]["DOBType"]);
                if (Convert.ToString(dt.Rows[0]["Gender"]) == "Male")
                    rdoBtnLstGender.SelectedValue = "0";
                else
                    rdoBtnLstGender.SelectedValue = "1";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('No record for update,please check your acknowledgement no.!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter your acknowledgement no!');", true);
        }
    }
    protected void ddl_pancat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_pancat.SelectedValue == "Individual")
        {
          //  trFathName.Visible = true;
          //  trAadhar.Visible = true;
            rfvAadhar.Enabled = true;
        }
        else if(ddl_pancat.SelectedValue == "Goverment")
        {
            txt_flastname.Visible = false;
             txt_ffristname.Visible = false;
            txt_Middlename.Visible = false;
            txt_fristname.Visible = false;
            txt_Fmiddlename.Visible = false;
            txt_Middlename.Visible = false;
            rdoBtnLstGender.Visible = false;
            rfvAadhar.Enabled = false;
        }
        else if (ddl_pancat.SelectedValue == "Individual")
        {
            // trFathName.Visible = false;
            rdoBtnLstGender.Visible = false;
            rfvAadhar.Enabled = false;
        }
    }
}