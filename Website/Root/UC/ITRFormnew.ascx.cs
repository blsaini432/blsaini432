using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;

public partial class Root_UC_ITRFormnew : System.Web.UI.UserControl
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
                dts = cls.select_data_dt("select MemberID,MemberTypeID from tblmlm_membermaster where Msrno=" + Msrno + "");
                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                //string PackageID = Convert.ToString(dts.Rows[0]["PackageID"]);
                Session["package"] = MemberTypeID;
                decimal Amount = 0;
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(MemberTypeID) + " and actiontype='ITR Fee'"));
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
                    dts = cls.select_data_dt("select MemberID,MemberTypeID from tblmlm_membermaster where Msrno=" + Msrno + "");
                    string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                    string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                    decimal Amount = 0;
                   Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(MemberTypeID) + " and actiontype='ITR Fee'"));
                    if (memberID != "" && Amount > 0)
                    {
                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {
                            string Photo = uploadPanImage(fup_Photo);
                            string Aadhar = uploadPanImage(fup_Aadhar);
                            string bankstatementfinacialyear = uploadPanImage(fup_bankstatementfinani);
                            string bankaccount = uploadPanImage(fup_bankaccountdetails);
                            string form16 = uploadPanImage(fup_form16);
                            string file1 = uploadPanImage(fup_File1);
                            string file2 = uploadPanImage(fup_File2);
                            int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                            List<ParmList> _list = new List<ParmList>();
                            _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                            _list.Add(new ParmList() { name = "@ITRType", value = "1" });
                            _list.Add(new ParmList() { name = "@NameOnPan", value = txt_NamePan.Text });
                           // _list.Add(new ParmList() { name = "@FatherName", value = txt_FatherName.Text });
                            _list.Add(new ParmList() { name = "@DOB", value = txt_dob.Text });
                            _list.Add(new ParmList() { name = "@itrpassword", value = TXT_PASS.Text });
                            _list.Add(new ParmList() { name = "@currentaddress", value = txt_presentaddress.Text });
                            _list.Add(new ParmList() { name = "@permanentaddress", value = txt_permanentaddress.Text });
                            _list.Add(new ParmList() { name = "@Mobieno", value = txt_MobilePartner.Text });
                            _list.Add(new ParmList() { name = "@EmailId ", value = txtEmail.Text });
                            _list.Add(new ParmList() { name = "@pancardimage", value = Photo });
                            _list.Add(new ParmList() { name = "@aadharcardimage ", value = Aadhar });
                            _list.Add(new ParmList() { name = "@file1", value = file1 });
                            _list.Add(new ParmList() { name = "@file2 ", value = file2 });
                            _list.Add(new ParmList() { name = "@bankstmtfyr ", value = bankstatementfinacialyear });
                            _list.Add(new ParmList() { name = "@bankactdetailifsc ", value = bankaccount });
                            _list.Add(new ParmList() { name = "@form16 ", value = form16 });
                            if (lblamt.Text != "")
                            {
                                _list.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(Amount) });
                            }
                            else
                            {
                                _list.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(Amount) });
                            }
                            _list.Add(new ParmList() { name = "@Action", value = "I" });
                            string TxnID = clsm.Cyrus_GetTransactionID_New();
                            _list.Add(new ParmList() { name = "@txnid", value = TxnID });
                            DataTable dt = new DataTable();
                            dt = cls.select_data_dtNew("Proc_ITRDetails_GetSet_new", _list);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["ITRkid"]) > 0)
                                {
                                    // clsm.Wallet_MakeTransaction(Session["adminmemberid"].ToString(), Convert.ToDecimal("-" + adminamount), "Dr", "ITR Request TxnID:-" + TxnID);
                                    clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "ITR Request TxnID:-" + TxnID);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('ITRReport.aspx');", true);
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


    //protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (RadioButtonList1.SelectedIndex == 0)
    //    {
    //    }
    //    else if (RadioButtonList1.SelectedIndex == 1)
    //    {

    //        Session["serviceid"] = 1;
    //        int pacj = Convert.ToInt32(Session["package"]);
    //        decimal Amount = 0;
    //        Amount = Convert.ToDecimal(cls.select_data_scalar_double("select isnull(FeeAmount,0.00) as FeeAmount from [tbl_GSTFeeSettings] where membertypeid=" + Convert.ToInt32(pacj) + " and actiontype='With Balane Sheet'"));
    //        Amount = Convert.ToDecimal(Amount);
    //        lblamt.Text = Amount.ToString();
    //    }
    //    else if (RadioButtonList1.SelectedIndex == 2)
    //    {
    //        Session["serviceid"] = 6;
    //        int pacj = Convert.ToInt32(Session["package"]);
    //        decimal Amount = 0;
    //        Amount = Convert.ToDecimal(cls.select_data_scalar_double("select isnull(FeeAmount,0.00) as FeeAmount from [tbl_GSTFeeSettings] where membertypeid=" + Convert.ToInt32(pacj) + " and actiontype='Without Balane Sheet'"));
    //        Amount = Convert.ToDecimal(Amount);
    //        lblamt.Text = Amount.ToString();
    //    }
    //}
}