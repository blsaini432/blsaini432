using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;

public partial class Root_UC_SHOPACT : System.Web.UI.UserControl


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
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select isnull(Amount,0.00) as Amount from [service_feesettings] where PackageID=" + Convert.ToInt32(PackageID) + " and serviceid='3'"));
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
                    Amount = Convert.ToDecimal(cls.select_data_scalar_double("select isnull(Amount,0.00) as Amount from [service_feesettings] where PackageID=" + Convert.ToInt32(PackageID) + " and serviceid='3'"));
                    Amount = Convert.ToDecimal(Amount);


                    if (memberID != "" && Amount > 0)
                    {


                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {
                            
                            string Photo = uploadPanImage(fup_photo);
                            string aadhar = uploadPanImage(fup_aadhar);
                            string self = uploadPanImage(fup_self);
                            string oldshop = uploadPanImage(fup_oldshop);
                            string file = uploadPanImage(Actualphoto);
                            string file2 = uploadPanImage(fup_file2);
                            string file3 = uploadPanImage(fup_file3);
                            string file4 = uploadPanImage(fup_file4);
                            string file5 = uploadPanImage(fup_file5);
                            string sign = uploadPanImage(fup_sign);
                            int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                            List<ParmList> _list = new List<ParmList>();
                            _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                            _list.Add(new ParmList() { name = "@Amount", value = Amount });
                            _list.Add(new ParmList() { name = "@Name_of_ect", value = txt_nameofest.Text });
                            _list.Add(new ParmList() { name = "@Address_of_ect", value = txt_addofest.Text });
                            _list.Add(new ParmList() { name = "@Pin", value = txt_pincode.Text });
                            _list.Add(new ParmList() { name = "@buss_date", value = buss_date.Text });
                            _list.Add(new ParmList() { name = "@Ownership", value = RadioButtonList3.Text });
                            _list.Add(new ParmList() { name = "@Date_of_comm", value = txt_daofcommbus.Text });
                            _list.Add(new ParmList() { name = "@Nature", value = txt_natofbus.Text });

                            _list.Add(new ParmList() { name = "@sign ", value = sign });
                            _list.Add(new ParmList() { name = "@no_of_emp ", value = txt_noofemp.Text });
                            _list.Add(new ParmList() { name = "@Name_of_emp", value = txt_nameofemp.Text });
                            _list.Add(new ParmList() { name = "@Aadharnumber ", value = txt_aadhar.Text });
                            _list.Add(new ParmList() { name = "@Residential ", value = txt_resaddofemp.Text });
                            _list.Add(new ParmList() { name = "@Mobile ", value = txt_mob.Text });
                            _list.Add(new ParmList() { name = "@Email ", value = txt_email.Text });
                            _list.Add(new ParmList() { name = "@Photo ", value = Photo });
                            _list.Add(new ParmList() { name = "@Aadhar ", value = aadhar });
                            _list.Add(new ParmList() { name = "@SelfDeclaration ", value = self });
                            _list.Add(new ParmList() { name = "@Actualphoto ", value = file });
                            _list.Add(new ParmList() { name = "@Oldshop ", value = oldshop });
                            _list.Add(new ParmList() { name = "@file2 ", value = file2 });
                            _list.Add(new ParmList() { name = "@file3 ", value = file3});
                            _list.Add(new ParmList() { name = "@file4 ", value = file4 });
                            _list.Add(new ParmList() { name = "@file5 ", value = file5});
                            _list.Add(new ParmList() { name = "@Action", value = "I" });

                            string TxnID = clsm.Cyrus_GetTransactionID_New();

                            _list.Add(new ParmList() { name = "@txnid", value = TxnID });

                            DataTable dt = new DataTable();
                            dt = cls.select_data_dtNew("Proc_SHOPACT ", _list);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["shopact_id"]) > 0)
                                {

                                    clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "SHOP ACT Request TxnID:-" + TxnID);
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
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf" || Extension == ".xls" || Extension == ".xlsx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);


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
        
    }
}