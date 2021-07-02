using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;
public partial class Root_UC_LoanForm : System.Web.UI.UserControl
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
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select  Amount from [service_loanfeesettings] where PackageID=" + Convert.ToInt32(PackageID)));               
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
                    Amount = Convert.ToDecimal(cls.select_data_scalar_double("select  Amount from [service_loanfeesettings] where PackageID="+  Convert.ToInt32(PackageID )));
                    Amount = Convert.ToDecimal(Amount);
                   
                    if (memberID != "" && Amount > 0)
                    {


                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {
                            string aadhar = uploadPanImage(fup_aadcard);
                            string age = uploadPanImage(fup_age);
                            string Bankstatement = uploadPanImage(txt_fathername);
                            string ITR = uploadPanImage(txt_mothername);
                            string GST = uploadPanImage(txt_name);
                            string Insurance = uploadPanImage(txt_address);
                            //string Addressproff = uploadPanImage(FileUploadadressImage);
                            //string Dobimg = uploadPanImage(FiledobressImage);
                            int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                            List<ParmList> _list = new List<ParmList>();
                            _list.Add(new ParmList() { name = "@Amount", value = Amount });
                            _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                            _list.Add(new ParmList() { name = "@Gender", value = RadioButtonList1.SelectedItem.ToString() });
                            _list.Add(new ParmList() { name = "@fullName", value = txt_fullname.Text });
                            _list.Add(new ParmList() { name = "@Address", value = txt_add.Text });
                            _list.Add(new ParmList() { name = "@Mobile ", value = txt_mob.Text });
                            _list.Add(new ParmList() { name = "@Email ", value = txt_email.Text });
                            _list.Add(new ParmList() { name = "@aadharcard ", value = aadhar });
                            _list.Add(new ParmList() { name = "@ageProof ", value = age });
                            _list.Add(new ParmList() { name = "@fathername ", value = Bankstatement });
                            _list.Add(new ParmList() { name = "@mothername ", value = ITR });
                            _list.Add(new ParmList() { name = "@Emergencyname ", value = GST });
                            _list.Add(new ParmList() { name = "@Emergencyaddress ", value = Insurance });
                            _list.Add(new ParmList() { name = "@Action", value = "I" });
                            //if (ddl_applicationtype.SelectedValue.ToString() == "update")
                            //{
                            //    _list.Add(new ParmList() { name = "@Action", value = "U" });
                            //}
                            //else
                            //{
                            //    
                            //}

                            string TxnID = clsm.Cyrus_GetTransactionID_New();

                            _list.Add(new ParmList() { name = "@txnid", value = TxnID });

                            DataTable dt = new DataTable();
                            dt = cls.select_data_dtNew("Proc_loan_list", _list);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["passport_id"]) > 0)
                                {
                                 
                                    clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "Loan Request TxnID:-" + TxnID);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('LoanForm_Report.aspx');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                            }
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
            }


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
                string opath = Server.MapPath("~/Root/Upload/PanCardRequest/Actual/");
                string mpath = Server.MapPath("~/Root/Upload/PanCardRequest/Medium/");
                string spath = Server.MapPath("~/Root/Upload/PanCardRequest/Small/");

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


    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (RadioButtonList1.SelectedIndex == 0)
        {
            fup_aadcard.Visible = false;
            fup_age.Visible = false;
            txt_fathername.Visible = false;
            txt_mothername.Visible = false;
            txt_name.Visible = false;
            txt_address.Visible = false;
            TXT_Insurance.Visible = false;
            gst.Visible = false;
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            fup_aadcard.Visible = true;
            fup_age.Visible = true;
            txt_fathername.Visible = true;
            txt_mothername.Visible = true;
            txt_name.Visible = false;
            txt_address.Visible = false;
            TXT_Insurance.Visible = false;
            gst.Visible = false;

        }
        else if(RadioButtonList1.SelectedIndex == 2)
        {
            fup_aadcard.Visible = true;
            fup_age.Visible = true;
            txt_fathername.Visible = true;
            txt_mothername.Visible = true;
            txt_name.Visible = true;
            txt_address.Visible = false;
            TXT_Insurance.Visible = false;
            gst.Visible = true;
        }
        else if (RadioButtonList1.SelectedIndex == 3)
        {
            fup_aadcard.Visible = true;
            fup_age.Visible = true;
            txt_fathername.Visible = true;
            txt_mothername.Visible = true;
            txt_name.Visible = false;
            txt_address.Visible = true;
            gst.Visible = false;
            TXT_Insurance.Visible = true;
        }
    }
}