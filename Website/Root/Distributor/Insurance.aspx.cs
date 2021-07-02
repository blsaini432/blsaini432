using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.IO;
using System.Configuration;
public partial class Root_Distributor_Insurance : System.Web.UI.Page
{
    #region MyRegion

    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {


                txt_Rdate.Text = DateTime.Today.ToShortDateString();
                DataTable dtm = (DataTable)Session["dtDistributor"];
                ViewState["DistrMsrNo"] = dtm.Rows[0]["MsrNo"].ToString();

                if (ViewState["DistrMsrNo"] != null)
                {
                    int Msrno = Convert.ToInt32(ViewState["DistrMsrNo"]);
                    DataTable dts = new DataTable();
                    dts = cls.select_data_dt("select MemberID,MemberTypeID from tblmlm_membermaster where Msrno=" + Msrno + "");
                    string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                    string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);

                }
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["DistrMsrNo"] != null)
                {
                    int Msrno = Convert.ToInt32(ViewState["DistrMsrNo"]);
                    DataTable dts = new DataTable();
                    dts = cls.select_data_dt("select MemberID,MemberTypeID from tblmlm_membermaster where Msrno=" + Msrno + "");
                    string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                    string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                    if (memberID != "")
                    {
                        string rcfrontimg = uploadPanImage(FileUploadrcfront);
                        string rcbackimg = uploadPanImage(FileUploadrcback);
                        string lastinsurance = uploadPanImage(filluploadlastinsurance);

                        if (rcfrontimg != "")
                        {

                            int MsrNo = Convert.ToInt32(ViewState["DistrMsrNo"]);
                            List<ParmList> _list = new List<ParmList>();

                            _list.Add(new ParmList() { name = "@Type", value = RadioButtonList1.SelectedItem.ToString() });
                            _list.Add(new ParmList() { name = "@Other", value = txt_other.Text });
                            _list.Add(new ParmList() { name = "@RequestBymsrno", value = MsrNo });
                            _list.Add(new ParmList() { name = "@Registrationno ", value = txt_registrationno.Text });
                            _list.Add(new ParmList() { name = "@Make ", value = txt_make.Text });
                            _list.Add(new ParmList() { name = "@Model", value = txt_model.Text });
                            _list.Add(new ParmList() { name = "@Year", value = txt_year.Text });
                            _list.Add(new ParmList() { name = "@TType", value = RadioButtonList2.SelectedItem.ToString() });
                            _list.Add(new ParmList() { name = "@Mobile", value = txt_mobile.Text });
                            _list.Add(new ParmList() { name = "@Email", value = txtEmail.Text });
                            _list.Add(new ParmList() { name = "@IDV", value = txt_idv.Text });
                            _list.Add(new ParmList() { name = "@LastNCP", value = txt_ncp.Text });

                            _list.Add(new ParmList() { name = "@RCImagefront", value = rcfrontimg });
                            _list.Add(new ParmList() { name = "@RCImageback", value = rcbackimg });
                            _list.Add(new ParmList() { name = "@LastInsurance", value = lastinsurance });
                            _list.Add(new ParmList() { name = "@test1", value = "" });
                            _list.Add(new ParmList() { name = "@test2", value = "" });
                            _list.Add(new ParmList() { name = "@Policyimage", value = "" });
                            _list.Add(new ParmList() { name = "@OD", value = 0.00 });
                            _list.Add(new ParmList() { name = "@TP", value = 0.00 });

                            _list.Add(new ParmList() { name = "@Discount", value = 0.00 });
                            _list.Add(new ParmList() { name = "@Tax", value = 0.00 });
                            _list.Add(new ParmList() { name = "@Primiumamt", value = 0.00 });

                            _list.Add(new ParmList() { name = "@Comissionamt", value = 0.00 });
                            _list.Add(new ParmList() { name = "@NetPay", value = 0.00 });
                            _list.Add(new ParmList() { name = "@RequestStatus", value = "PendingforQutoation" });

                            _list.Add(new ParmList() { name = "@RefNo", value = "" });
                            _list.Add(new ParmList() { name = "@Remarks", value = "" });

                            _list.Add(new ParmList() { name = "@DeclineOrSuccessdate", value = "" });



                            _list.Add(new ParmList() { name = "@Action", value = "I" });


                            // string TxnID = clsm.Cyrus_GetTransactionID_New();

                            _list.Add(new ParmList() { name = "@TxnID", value = "" });

                            DataTable dt = new DataTable();
                            dt = cls.select_data_dtNew("Proc_InsuranceDetails_GetSet", _list);
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["kid"]) > 0)
                                {
                                    clearall();
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Request Save Successfully.!');location.replace('Insurance.aspx');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select JPG/JPEG/PNG/GIF Images Only!');", true);
                        }


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('something went wrong');", true);
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
        txt_idv.Text = txt_make.Text = txt_mobile.Text = txt_model.Text = txt_ncp.Text = txt_other.Text = txt_Rdate.Text = txt_registrationno.Text = txt_year.Text = txtEmail.Text = "";
        RadioButtonList1.ClearSelection();
        RadioButtonList2.ClearSelection();
    }

    private string uploadPanImage(FileUpload _fup)
    {
 
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("../../Uploads/InsuranceRequest/Actual/");
                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
               
                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(_fup.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                    //objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
                    //objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);

                    return FileName;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select JPG/JPEG/PNG/GIF/pdf Images Only!');", true);

                }
            }
        }
        else
        {
            return "";
        }

        return "";
    }




    public void clearall()
    {
        txt_idv.Text = txt_make.Text = txt_mobile.Text = txt_model.Text = txt_ncp.Text = txt_other.Text = txt_Rdate.Text = txt_registrationno.Text = txt_year.Text = txtEmail.Text = "";
        RadioButtonList1.ClearSelection();
        RadioButtonList2.ClearSelection();
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 5)
        {
            other.Visible = true;
        }
        else
        {
            other.Visible = false;
        }
    }

}


