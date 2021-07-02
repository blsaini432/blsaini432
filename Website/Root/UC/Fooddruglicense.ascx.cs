using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BLL;

public partial class Root_UC_Fooddruglicense : System.Web.UI.UserControl
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
                dts = cls.select_data_dt("select MemberID,MemberTypeID ,PackageID from tblmlm_membermaster where Msrno=" + Msrno + "");

                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                string PackageID = Convert.ToString(dts.Rows[0]["PackageID"]);
                Session["package"] = PackageID;
                decimal Amount = 0;
                Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(MemberTypeID) + " and actiontype='Food Registration Fee'"));
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
                    Amount = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=" + Convert.ToInt32(MemberTypeID) + " and actiontype='Food Registration Fee'"));
                    Amount = Convert.ToDecimal(Amount);              
                    if (memberID != "" && Amount > 0)
                    {
                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {
                            string Photo = uploadPanImage(fup_photo);
                            string Aadhar = uploadPanImage(fup_aadcard);
                            string declfrom = uploadPanImage(fup_decform);
                           // string file = uploadPanImage(fup_file);
                          //  string file2 = uploadPanImage(fup_file2);
                          //  string file3 = uploadPanImage(fup_file3);
                          //  string file4 = uploadPanImage(fup_file4);
                          //  string file5 = uploadPanImage(fup_file5);
                            int MsrNo = Convert.ToInt32(ViewState["MemberMsrNo"]);
                            List<ParmList> _list = new List<ParmList>();
                            _list.Add(new ParmList() { name = "@Amount", value = Amount });
                            _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                            _list.Add(new ParmList() { name = "@Type_of_business ", value = txt_tyofbus.Text });
                            _list.Add(new ParmList() { name = "@Yearly_Turnover", value = txt_yearly.Text });
                            _list.Add(new ParmList() { name = "@Name_of_applicant", value = txt_nameofapp.Text });
                            _list.Add(new ParmList() { name = "@Name_of_company", value = txt_nameofcop.Text });
                            _list.Add(new ParmList() { name = "@Kind_of_Business", value = txt_kindofbus.Text });
                            _list.Add(new ParmList() { name = "@Business_start_date", value = txt_busstrdate.Text });
                            _list.Add(new ParmList() { name = "@Address_Of_Business ", value = txt_addofbus.Text });
                            _list.Add(new ParmList() { name = "@Correspondence_address", value = txt_corressadd.Text });
                            _list.Add(new ParmList() { name = "@Mobile ", value = txt_mob.Text });
                            _list.Add(new ParmList() { name = "@Email ", value = txt_email.Text });
                            _list.Add(new ParmList() { name = "@photo", value = Photo });
                            _list.Add(new ParmList() { name = "@aadhar ", value = Aadhar });
                         //   _list.Add(new ParmList() { name = "@foodlictype ", value = DropDownList1.Text });
                          //  _list.Add(new ParmList() { name = "@files ", value = file });
                          //  _list.Add(new ParmList() { name = "@file2 ", value = file2 });
                          //  _list.Add(new ParmList() { name = "@file3 ", value = file3 });
                          //  _list.Add(new ParmList() { name = "@file4 ", value = file4 });
                          //  _list.Add(new ParmList() { name = "@file5 ", value = file5 });
                            _list.Add(new ParmList() { name = "@Declaration_Form ", value = declfrom });
                           
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
                            dt = cls.select_data_dtNew("Proc_Food_license", _list);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["food_id"]) > 0)
                                {
                                    // clsm.Wallet_MakeTransaction(Session["adminmemberid"].ToString(), Convert.ToDecimal("-" + adminamount), "Dr", "ITR Request TxnID:-" + TxnID);
                                    clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "Food License Request TxnID:-" + TxnID);
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
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".pdf" || Extension == ".gif" || Extension == ".xls" || Extension == ".xlsx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                  //  objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
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
    //    if (DropDownList1.SelectedIndex == 0)
    //    {
    //    }
    //    else if (DropDownList1.SelectedIndex == 1)
    //    {

    //        Session["serviceid"] = 1;
    //        int pacj = Convert.ToInt32(Session["package"]);
    //        decimal Amount = 0;
    //        Amount = Convert.ToDecimal(cls.select_data_scalar_double("select isnull(Amount,0.00) as Amount from [service_feesettings] where PackageID=" + Convert.ToInt32(pacj) + " and serviceid='1'"));
    //        Amount = Convert.ToDecimal(Amount);
    //        lblamt.Text = Amount.ToString();
    //    }
    //    else if (DropDownList1.SelectedIndex == 2)
    //    {
    //        Session["serviceid"] = 6;
    //        int pacj = Convert.ToInt32(Session["package"]);
    //        decimal Amount = 0;
    //        Amount = Convert.ToDecimal(cls.select_data_scalar_double("select isnull(Amount,0.00) as Amount from [service_feesettings] where PackageID=" + Convert.ToInt32(pacj) + " and serviceid='6'"));
    //        Amount = Convert.ToDecimal(Amount);
    //        lblamt.Text = Amount.ToString();
    //    }
    //    else if (DropDownList1.SelectedIndex == 3)
    //    {
    //        Session["serviceid"] = 7;
    //        int pacj = Convert.ToInt32(Session["package"]);
    //        decimal Amount = 0;
    //        Amount = Convert.ToDecimal(cls.select_data_scalar_double("select isnull(Amount,0.00) as Amount from [service_feesettings] where PackageID=" + Convert.ToInt32(pacj) + " and serviceid='7'"));
    //        Amount = Convert.ToDecimal(Amount);
    //        lblamt.Text = Amount.ToString();
    //    }
    //}
}