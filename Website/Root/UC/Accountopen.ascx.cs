using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;
public partial class Root_UC_Accountopen : System.Web.UI.UserControl
{
    public static int msrno { get; set; }

    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    clsState objState = new clsState();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txt_Rdate.Text = DateTime.Today.ToShortDateString();

            ViewState["MemberMsrNo"] = msrno;
            if (ViewState["MemberMsrNo"] != null)
            {
                int Msrno = Convert.ToInt32(ViewState["MemberMsrNo"]);
                DataTable dts = new DataTable();
                dts = cls.select_data_dt("select MemberID,PackageID, MemberTypeID from tblmlm_membermaster where Msrno=" + Msrno + "");

                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                string PackageID = Convert.ToString(dts.Rows[0]["PackageID"]);
                decimal Amount = 0;
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select  Amount from [Accountopenfeesettings] where PackageID=" + Convert.ToInt32(PackageID)));
                lblamt.Text = Amount.ToString();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
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
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select  Amount from [Accountopenfeesettings] where PackageID=" + Convert.ToInt32(PackageID)));
                Amount = Convert.ToDecimal(Amount);

                if (memberID != "")
                {

                    int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                    if (result > 0)
                    {
                        string aadhar = uploadPanImage(fup_aadcard);
                        string pan = uploadPanImage(fup_age);
                        string PHOTO = uploadPanImage(txt_photo);
                        int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                        List<ParmList> _list = new List<ParmList>();
                        _list.Add(new ParmList() { name = "@Amount", value = Amount });
                        _list.Add(new ParmList() { name = "@msrno", value = MsrNo });
                        //  _list.Add(new ParmList() { name = "@Gender", value = RadioButtonList1.SelectedItem.ToString() });
                        //   _list.Add(new ParmList() { name = "@bankname", value = txt_bank.Text });
                        _list.Add(new ParmList() { name = "@bank", value = RadioButtonList1.SelectedItem.ToString() });
                        _list.Add(new ParmList() { name = "@branchname ", value = txt_branch.Text });
                        _list.Add(new ParmList() { name = "@Customeetype ", value = tadlist.SelectedItem.ToString() });
                        _list.Add(new ParmList() { name = "@work ", value = txt_work.Text });
                        _list.Add(new ParmList() { name = "@fname ", value = txt_fristname.Text });
                        _list.Add(new ParmList() { name = "@mname ", value = txt_Middlename.Text });
                        _list.Add(new ParmList() { name = "@lname ", value = txt_lastname.Text });
                        _list.Add(new ParmList() { name = "@faname ", value = txt_fname.Text });
                        _list.Add(new ParmList() { name = "@fmname ", value = txt_fmiddle.Text });
                        _list.Add(new ParmList() { name = "@flname ", value = txt_flast.Text });
                        _list.Add(new ParmList() { name = "@DOB ", value = txt_dateofb.Text });
                        _list.Add(new ParmList() { name = "@SEX ", value = rdoBtnLstGender.SelectedItem.ToString() });
                        _list.Add(new ParmList() { name = "@mothername ", value = txt_lname.Text });
                        //  _list.Add(new ParmList() { name = "@Gender ", value = rdoBtnLstGender.SelectedItem.ToString() });
                        _list.Add(new ParmList() { name = "@Marital ", value = RadioButtonList2.SelectedItem.ToString() });
                        //  _list.Add(new ParmList() { name = "@wifename ", value = ITR });
                        _list.Add(new ParmList() { name = "@mobile ", value = txt_mob.Text });
                        _list.Add(new ParmList() { name = "@email ", value = txt_email.Text });
                        _list.Add(new ParmList() { name = "@aadhar ", value = txt_aadhar.Text });
                        _list.Add(new ParmList() { name = "@pan ", value = txt_pan.Text });
                        _list.Add(new ParmList() { name = "@address ", value = txt_add.Text });
                        _list.Add(new ParmList() { name = "@pincode ", value = txt_pin.Text });
                        _list.Add(new ParmList() { name = "@city ", value = txt_city.Text });
                        _list.Add(new ParmList() { name = "@state ", value = txt_state.Text });
                        _list.Add(new ParmList() { name = "@paddress ", value = txt_addes.Text });
                        _list.Add(new ParmList() { name = "@ppincode ", value = txt_pins.Text });
                        _list.Add(new ParmList() { name = "@pcity ", value = txt_citys.Text });
                        _list.Add(new ParmList() { name = "@pstate ", value = txt_states.Text });
                        _list.Add(new ParmList() { name = "@nfname ", value = txt_nname.Text });
                        _list.Add(new ParmList() { name = "@nmname ", value = txt_nmname.Text });
                        _list.Add(new ParmList() { name = "@nlname ", value = txt_nlast.Text });
                        _list.Add(new ParmList() { name = "@ndob ", value = txt_dname.Text });
                        _list.Add(new ParmList() { name = "@Naadhar ", value = txt_naadhar.Text });
                        _list.Add(new ParmList() { name = "@naddress ", value = txt_naddress.Text });
                        _list.Add(new ParmList() { name = "@ncity ", value = txt_ncity.Text });
                        _list.Add(new ParmList() { name = "@nstate ", value = txt_nstate.Text });
                        _list.Add(new ParmList() { name = "@aadhar_image ", value = aadhar });
                        _list.Add(new ParmList() { name = "@pan_image ", value = pan });
                        _list.Add(new ParmList() { name = "@photo ", value = PHOTO });
                        _list.Add(new ParmList() { name = "@Action", value = "I" });
                        string TxnID = clsm.Cyrus_GetTransactionID_New();

                        _list.Add(new ParmList() { name = "@txnid", value = TxnID });

                        DataTable dt = new DataTable();
                        dt = cls.select_data_dtNew("Proc_accountopenss", _list);

                        clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "Account Open Request TxnID:-" + TxnID);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('Accountopen_report.aspx');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in Wallet !');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in Wallet !');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Amount Not Set!');", true);
            }
        }


        // }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' " + ex.Message.ToString() + " ');", true);
        }



    }

    protected void btnReset_Click(object sender, EventArgs e)
    {

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
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".xls" || Extension == ".xlsx" || Extension == ".pdf")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                    //objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
                    // objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);

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


    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (RadioButtonList1.SelectedIndex == 0)
    //    {
    //        fup_aadcard.Visible = false;
    //        fup_age.Visible = false;
    //        txt_fathername.Visible = false;
    //        txt_mothername.Visible = false;
    //        txt_name.Visible = false;
    //        txt_address.Visible = false;
    //        TXT_Insurance.Visible = false;
    //        gst.Visible = false;
    //    }
    //    else if (RadioButtonList1.SelectedIndex == 1)
    //    {
    //        fup_aadcard.Visible = true;
    //        fup_age.Visible = true;
    //        txt_fathername.Visible = true;
    //        txt_mothername.Visible = true;
    //        txt_name.Visible = false;
    //        txt_address.Visible = false;
    //        TXT_Insurance.Visible = false;
    //        gst.Visible = false;

    //    }
    //    else if(RadioButtonList1.SelectedIndex == 2)
    //    {
    //        fup_aadcard.Visible = true;
    //        fup_age.Visible = true;
    //        txt_fathername.Visible = true;
    //        txt_mothername.Visible = true;
    //        txt_name.Visible = true;
    //        txt_address.Visible = false;
    //        TXT_Insurance.Visible = false;
    //        gst.Visible = true;
    //    }
    //    else if (RadioButtonList1.SelectedIndex == 3)
    //    {
    //        fup_aadcard.Visible = true;
    //        fup_age.Visible = true;
    //        txt_fathername.Visible = true;
    //        txt_mothername.Visible = true;
    //        txt_name.Visible = false;
    //        txt_address.Visible = true;
    //        gst.Visible = false;
    //        TXT_Insurance.Visible = true;
    //    }
    //}
}