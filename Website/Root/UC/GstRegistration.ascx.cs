using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;

public partial class Root_UC_GstRegistration : System.Web.UI.UserControl
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

                decimal Amount = 0;
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(MemberTypeID) + " and actiontype='GST Fee'"));
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
                     Amount = 0;
                    Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(MemberTypeID) + " and actiontype='GST Fee'"));
                    if (memberID != "")
                    {

                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {

                            string aadhar = uploadPanImage(fup_aadhar);
                            string pan = uploadPanImage(fup_pan);
                            string photo = uploadPanImage(fup_photos);
                            string sale = uploadPanImage(fup_sale);
                            string gst = uploadPanImage(fup_gst);
                            //if (IdentiyImg != "" && Addressproff != "" && Dobimg != "")
                            //{

                            int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                            List<ParmList> _list = new List<ParmList>();

                            _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                            _list.Add(new ParmList() { name = "@GStType", value = txt_gsttype.Text });
                            _list.Add(new ParmList() { name = "@EntityType", value = txt_ent.Text });
                            _list.Add(new ParmList() { name = "@BusinessPanCard ", value = txt_pannum.Text });
                            _list.Add(new ParmList() { name = "@BusinessType", value = txt_nat.Text });
                          //  _list.Add(new ParmList() { name = "@StateID", value = txt_sta.Text });
                            _list.Add(new ParmList() { name = "@District", value = txt_dis.Text });
                            _list.Add(new ParmList() { name = "@AccType", value = txt_banktype.Text });
                            _list.Add(new ParmList() { name = "@AccHolderName", value = txt_accname.Text });
                            _list.Add(new ParmList() { name = "@AccNum", value = txt_accnum.Text });
                            _list.Add(new ParmList() { name = "@BankPin", value = txt_ifsc.Text });
                            _list.Add(new ParmList() { name = "@BankName", value = txt_bankname.Text });
                            _list.Add(new ParmList() { name = "@AnnualTurnOver", value = txt_turn.Text });
                            _list.Add(new ParmList() { name = "@BusinessObj", value = txt_buss.Text });
                            _list.Add(new ParmList() { name = "@NameOnPan", value = txt_NamePan.Text });
                            _list.Add(new ParmList() { name = "@FatherName", value = txt_FatherName.Text });
                            _list.Add(new ParmList() { name = "@DOB", value = txt_dob.Text });
                            _list.Add(new ParmList() { name = "@Address", value = txt_presentaddress.Text });
                            _list.Add(new ParmList() { name = "@AddressDir", value = txt_permanentaddress.Text });
                            _list.Add(new ParmList() { name = "@MobileNo", value = txt_MobilePartner.Text });
                            _list.Add(new ParmList() { name = "@Email ", value = txtEmail.Text });
                            _list.Add(new ParmList() { name = "@PanCardImage", value = pan });
                            _list.Add(new ParmList() { name = "@AadharImage ", value = aadhar });
                            _list.Add(new ParmList() { name = "@PhotoImage ", value = photo });
                            _list.Add(new ParmList() { name = "@ChequeImage ", value = sale });
                            _list.Add(new ParmList() { name = "@OtherDocImage ", value = gst });

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
                            dt = cls.select_data_dtNew("Proc_GSTDetails_GetSet", _list);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["GSTkid"]) > 0)
                                {
                                    clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "GST Request TxnID:-" + TxnID);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('GstRegistration_Report.aspx');", true);
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
                if (Extension == ".jpg" || Extension == ".pdf" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".xls" || Extension == ".xlsx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                    // objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
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




}