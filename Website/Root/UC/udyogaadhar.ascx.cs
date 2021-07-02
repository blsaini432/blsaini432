using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;

public partial class Root_UC_udyogaadhar : System.Web.UI.UserControl
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
                dts = cls.select_data_dt("select MemberID,MemberTypeID,PackageID from tblmlm_membermaster where Msrno=" + Msrno + "");

                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                string PackageID = Convert.ToString(dts.Rows[0]["PackageID"]);
                decimal Amount = 0;
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select isnull(Amount,0.00) as Amount from [service_feesettings] where PackageID=" + Convert.ToInt32(PackageID) + " and serviceid='2'"));
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
                    
                    Amount = Convert.ToDecimal(cls.select_data_scalar_double("select isnull(Amount,0.00) as Amount from [service_feesettings] where PackageID=" + Convert.ToInt32(PackageID) + " and serviceid='2'"));
                    Amount = Convert.ToDecimal(Amount);

                    if (memberID != "" && Amount > 0)
                    {


                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {
                            string pan = uploadPanImage(fup_PAN);
                            string Aadhar = uploadPanImage(fup_Aadhar);
                            string bankpassbook = uploadPanImage(fup_bkpass);
                            string file = uploadPanImage(fup_file1);
                            string file2 = uploadPanImage(fup_file2);
                            string file3 = uploadPanImage(fup_file3);
                            string file4 = uploadPanImage(fup_file4);
                            int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                            List<ParmList> _list = new List<ParmList>();
                            _list.Add(new ParmList() { name = "@Amount", value = Amount });
                            _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                            _list.Add(new ParmList() { name = "@Gender", value = RadioButtonList1.SelectedItem.ToString() });
                            _list.Add(new ParmList() { name = "@Aadhar_no", value = txt_aadhar.Text });
                            _list.Add(new ParmList() { name = "@Full_name", value = txt_fullname.Text });
                            _list.Add(new ParmList() { name = "@Social_Category", value = txt_soccat.Text });
                            _list.Add(new ParmList() { name = "@Mobile", value = txt_mob.Text });
                            _list.Add(new ParmList() { name = "@Email ", value = txt_email.Text });
                            _list.Add(new ParmList() { name = "@Physically", value = ddl_phyhan.Text });
                            _list.Add(new ParmList() { name = "@Name_of_Enterprise ", value = txt_nameofent.Text });
                            _list.Add(new ParmList() { name = "@Type_of_Organization ", value = txt_tyoforg.Text });
                            _list.Add(new ParmList() { name = "@Pan", value = txt_panno.Text });
                            _list.Add(new ParmList() { name = "@shop_address", value = txt_shop.Text });
                            _list.Add(new ParmList() { name = "@Residential_address ", value = txt_resadd.Text });
                            _list.Add(new ParmList() { name = "@Date_of_Commencement ", value = txt_daofcomm.Text });
                            _list.Add(new ParmList() { name = "@Account_No ", value = txt_accno.Text });
                            _list.Add(new ParmList() { name = "@IFSC_Code", value = txt_ifsc.Text });
                        
                            _list.Add(new ParmList() { name = "@No_of_Employees ", value = txt_noemp.Text });
                            _list.Add(new ParmList() { name = "@Investment ", value = txt_invt.Text });
                            _list.Add(new ParmList() { name = "@Aadhar ", value = Aadhar });
                            _list.Add(new ParmList() { name = "@PANcard ", value = pan });
                            _list.Add(new ParmList() { name = "@Bank ", value = bankpassbook });
                            _list.Add(new ParmList() { name = "@files ", value = file });
                            _list.Add(new ParmList() { name = "@file2 ", value = file2 });
                            _list.Add(new ParmList() { name = "@file3 ", value = file3 });
                            _list.Add(new ParmList() { name = "@file4 ", value = file4 });
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
                            dt = cls.select_data_dtNew("Proc_udyogaadhar", _list);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["udyog_id"]) > 0)
                                {
                                   
                                   clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "Udyog Aadhar Request TxnID:-" + TxnID);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('');", true);
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
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".xls" || Extension == ".pdf" || Extension == ".xlsx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                   // objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
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


    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (RadioButtonList1.SelectedIndex == 0)
        //{
        //    pancardds.Visible = true;
        //    aadharcard.Visible = true;
        //    bankfinanicialst.Visible = false;
        //    form16tr.Visible = true;
        //    bankaccountstt.Visible = true;
        //}
        //else
        //{
        //    pancardds.Visible = true;
        //    aadharcard.Visible = true;
        //    form16tr.Visible = false;
        //    bankaccountstt.Visible = false;
        //    bankfinanicialst.Visible = true;
        //}
    }
}