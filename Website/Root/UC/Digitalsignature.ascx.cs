using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;

public partial class Root_UC_Digitalsignature : System.Web.UI.UserControl
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
                Session["MemberTypeID"] = MemberTypeID;
                decimal Amount = 0;
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(MemberTypeID) + " and actiontype='Digitalsignature'"));
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
                    int service = Convert.ToInt32(Session["serviceid"]);
                    decimal Amount = 0;

                    // Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(MemberTypeID) + " and GSTFee_ID=" + service));
                   // Session["Amount"] = Amount;
                    Amount = Convert.ToDecimal(Session["Amount"]);


                    if (memberID != "" && Amount > 0)
                    {


                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {
                            string Photo = uploadPanImage(fup_photo);
                            string age = uploadPanImage(fup_age);
                            string file1 = uploadPanImage(fup_file1);
                            string file2 = uploadPanImage(fup_file2);
                            int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                            List<ParmList> _list = new List<ParmList>();
                            _list.Add(new ParmList() { name = "@Amount", value = Amount });
                            _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                             _list.Add(new ParmList() { name = "@STATE", value = DropDownList1.SelectedItem.ToString() });
                           
                            _list.Add(new ParmList() { name = "@Assembly", value = txt_par.Text });
                            _list.Add(new ParmList() { name = "@Applicantname", value = txt_name.Text });
                       
                            _list.Add(new ParmList() { name = "@Relation", value = txt_rel.Text });
                            _list.Add(new ParmList() { name = "@Dateofbirth ", value = txt_date.Text });
                            _list.Add(new ParmList() { name = "@Gender", value = RadioButtonList1.SelectedItem.ToString() });
                            _list.Add(new ParmList() { name = "@Address ", value = txt_add.Text });
                            _list.Add(new ParmList() { name = "@Family ", value = txt_fem.Text });
                            _list.Add(new ParmList() { name = "@Mobile ", value = txt_mob.Text });
                            _list.Add(new ParmList() { name = "@Email ", value = txt_email.Text });
                            _list.Add(new ParmList() { name = "@Photo ", value = Photo });
                            _list.Add(new ParmList() { name = "@ageProof ", value = age });
                            _list.Add(new ParmList() { name = "@AddressProof ", value = file1 });
                            _list.Add(new ParmList() { name = "@Familyid ", value = file2 });
                            _list.Add(new ParmList() { name = "@Action", value = "I" });

                            string TxnID = clsm.Cyrus_GetTransactionID_New();

                            _list.Add(new ParmList() { name = "@txnid", value = TxnID });

                            DataTable dt = new DataTable();
                            dt = cls.select_data_dtNew("Proc_voteridcard_new", _list);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["voterid"]) > 0)
                                {
                                    
                                    clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", " Digital signature Request TxnID:-" + TxnID);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('Digitalsignature_Report.aspx');", true);
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
                if (Extension == ".jpg"   || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf" || Extension == ".xls" || Extension == ".xlsx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                 // objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
                //  objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);

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

        if (DropDownList1.SelectedIndex == 0)
        {
        }
        else if (DropDownList1.SelectedIndex == 1)
        {
            
            int pacj = Convert.ToInt32(Session["MemberTypeID"]);
            decimal Amount = 0;
            Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(pacj) + " and actiontype='Class -2, 2years Individual'"));
            Amount = Convert.ToDecimal(Amount);
            Session["Amount"] = Amount;
            lblamt.Text = Amount.ToString();
        }
        else if (DropDownList1.SelectedIndex == 2)
        {
           // Session["serviceid"] = 11;
            int pacj = Convert.ToInt32(Session["MemberTypeID"]);
            decimal Amount = 0;
            Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(pacj) + " and actiontype='Class-2, 2years with encryption (combo)'"));
            Amount = Convert.ToDecimal(Amount);
            Session["Amount"] = Amount;
            lblamt.Text = Amount.ToString();
        }
        else if (DropDownList1.SelectedIndex == 3)
        {
           // Session["serviceid"] = 12;
            int pacj = Convert.ToInt32(Session["MemberTypeID"]);
            decimal Amount = 0;
            Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(pacj) + " and actiontype='Class-2, 2years with encryption (combo) govt'"));
            Amount = Convert.ToDecimal(Amount);
            Session["Amount"] = Amount;
            lblamt.Text = Amount.ToString();
        }
        else if (DropDownList1.SelectedIndex == 4)
        {
           // Session["serviceid"] = 13;
            int pacj = Convert.ToInt32(Session["MemberTypeID"]);
            decimal Amount = 0;
            Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(pacj) + " and actiontype='Class-3, 2years with encryption (combo)'"));
            Amount = Convert.ToDecimal(Amount);
            Session["Amount"] = Amount;
            lblamt.Text = Amount.ToString();
        }
    }
}