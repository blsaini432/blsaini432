using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Common;

public partial class Root_Administrator_GSTReturnReport : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    string condition = " RequestBymsrno > 0";
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillEmployee();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

    #endregion

    #region [Function]
    private void fillEmployee()
    {
        int MsrNo = Convert.ToInt32(ViewState["msnoid"]);
        #region Condition
        if (!(string.IsNullOrEmpty(txtfromdate.Text) | string.IsNullOrEmpty(txttodate.Text)))
        {
            condition = condition + " and AddDate >= '" + txtfromdate.Text + "' AND AddDate <= '" + txttodate.Text + "'";
        }        

        #endregion


        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Action", value = "L" });
        dtEmployee = cls.select_data_dtNew("Proc_GSTReturnDetails_GetSet_new", _list);
        if (dtEmployee.Rows.Count > 0)
        {
            dtEmployee.DefaultView.RowFilter = condition;
            gvGST.DataSource = dtEmployee;
            gvGST.DataBind();
            if (dtEmployee.Rows.Count > 0)
            {
                litrecordcount.Text = gvGST.Rows.Count.ToString();
                ViewState["dtExport"] = dtEmployee.DefaultView.ToTable();
            }
        }

    }
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
            {
                ViewState["sortDirection"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["sortDirection"];
        }
        set
        {
            ViewState["sortDirection"] = value;
        }
    }
    #endregion

    #region [GridViewEvents]
    protected void gvGST_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "deletesuser")
        {
            string idno = "0";
            idno = Convert.ToString(e.CommandArgument);
            cls.delete_data("delete from GSTReturn_new where GSTreturnkid='" + idno + "'");
            fillEmployee();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Deleted successfully !!');", true);
        }
        #region [Approve]
        if (e.CommandName == "Approve")
        {
            try
            {
                ressf.Visible = true;
                reqrefno.Enabled = true;
                btnFail.Enabled = false;
                btnFail.Visible = false;
                btnSuccess.Visible = true;
                btnSuccess.Enabled = true;
                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                hdnid.Value = idno;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);
            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion
        #region [Reject]
        if (e.CommandName == "Reject")
        {

            try
            {
                ressf.Visible = false;
                reqrefno.Enabled = false;
                //recp.Visible = false;
                //reqrecipt.Enabled = false;
                btnFail.Enabled = true;
                btnFail.Visible = true;
                btnSuccess.Visible = false;
                btnSuccess.Enabled = false;

                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                hdnid.Value = idno;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion
        #region[Client Data in Word]
        if (e.CommandName == "WordDownload")
        {
            try
            {

                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                DataTable dt = new DataTable();
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@Action", value = "ClientData" });
                _list.Add(new ParmList() { name = "@GSTreturnkid", value = Convert.ToInt32(idno) });
                dt = cls.select_data_dtNew("Proc_GSTReturnDetails_GetSet_new", _list);
                if (dt.Rows.Count > 0)
                {
                    GridView GridView1 = new GridView();
                    HttpContext.Current.Response.Clear();

                    HttpContext.Current.Response.Buffer = true;

                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=User " + dt.Rows[0]["MemberName"] + ".doc");

                    HttpContext.Current.Response.Charset = "";

                    HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";

                    StringWriter sw = new StringWriter();

                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GridView1.DataSource = dt;
                    GridView1.AllowPaging = false;

                    GridView1.DataBind();

                    GridView1.RenderControl(hw);

                    HttpContext.Current.Response.Output.Write(sw.ToString());

                    HttpContext.Current.Response.Flush();

                    HttpContext.Current.Response.End();
                }

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion
    }

    protected void gvGST_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["dtExport"];
            DataView dv = new DataView(dt);
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                dv.Sort = e.SortExpression + " DESC";
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                dv.Sort = e.SortExpression + " ASC";
            }
            gvGST.DataSource = dv;
            gvGST.DataBind();
        }
        catch (Exception ex)
        { }
    }
    #endregion

    #region [ddlPaging]

    protected void gvGST_PageIndexChanging(object sender, EventArgs e)
    {
        fillEmployee();
    }

    #endregion

    #region [Export To Excel/Word/Pdf]
    protected void btnexportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = (DataTable)ViewState["dtExport"];
            if (dtExport.Rows.Count > 0)
            {
                dtExport.Columns.Remove("RequestBymsrno");                
                Common.Export.ExportToExcel(dtExport, "Report");
            }
        }
        catch
        { }

    }
    protected void btnexportWord_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = (DataTable)ViewState["dtExport"];
            if (dtExport.Rows.Count > 0)
            {
                dtExport.Columns.Remove("RequestBymsrno");               
                Common.Export.ExportToWord(dtExport, "Report");
            }
        }
        catch
        { }

    }
    protected void btnexportPdf_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = (DataTable)ViewState["dtExport"];
            if (dtExport.Rows.Count > 0)
            {
                dtExport.Columns.Remove("RequestBymsrno");                
                Common.Export.ExportTopdf(dtExport, "Report");
            }
        }
        catch
        { }
    }

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillEmployee();
    }

    protected void gvGST_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int comissionamount = 0;
        int cnt = 0;
        DataTable dtgetmsrno = cls.select_data_dt("select * from  GSTReturn_new where GSTreturnkid='" + Convert.ToInt32(hdnid.Value) + "'");
        int msrnoretailer = Convert.ToInt32(dtgetmsrno.Rows[0]["RequestBymsrno"].ToString());
        decimal Amoutpancardretailer = Convert.ToDecimal(dtgetmsrno.Rows[0]["Amount"].ToString());
        DataTable dtgetmemeberidretailer = cls.select_data_dt("select * from tblmlm_membermaster where MsrNo='" + msrnoretailer + "'");
        DataTable dtgetdistrimsrno = cls.select_data_dt("select * from ViewMLM_MemberSearch where MemberID='" + dtgetmemeberidretailer.Rows[0]["MemberID"] + "'");
        if (dtgetdistrimsrno.Rows.Count > 0)
        {
            DataTable dtgetdistrfees = cls.select_data_dt("SELECT MemberID,MsrNo,STDCode FROM tblmlm_membermaster WHERE MsrNO='" + Convert.ToInt32(dtgetdistrimsrno.Rows[0]["ParentMsrNo"]) + "'");
            string memberidss = dtgetdistrfees.Rows[0]["MemberID"].ToString();
            if (memberidss == "100000")
            {

               // string Img = uploadPanImage(FileUploadadressImage);
                cls.update_data("update [GSTReturn_new] set RequestStatus='Success',RefNo='" + txt_refno.Text + "',Remarks='" + txtadminRemark.Text.Trim() + "',ModifiedDate=getdate() where GSTreturnkid=" + Convert.ToInt32(hdnid.Value) + "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);
                fillEmployee();
            }
            else
            {
                if (dtgetdistrfees.Rows.Count > 0)
                {

                    decimal fees = Convert.ToDecimal(cls.select_data_scalar_double("select FeeAmount from [tbl_GSTFeeSettings] where memberTypeID=3 and actiontype='Gst Return Fee'"));
                    //   int fees = Convert.ToInt32(dtgetdistrfees.Rows[0]["STDCode"].ToString());
                    string memberid = dtgetdistrfees.Rows[0]["MemberID"].ToString();
                    comissionamount = Convert.ToInt32(Amoutpancardretailer) - Convert.ToInt32(fees);
                    cls_myMember clsm = new cls_myMember();
                    cnt = clsm.Wallet_Addfund(Convert.ToInt32(dtgetdistrimsrno.Rows[0]["ParentMsrNo"]), memberid, Convert.ToDecimal(comissionamount), "GST Return Comission from member id " + dtgetmemeberidretailer.Rows[0]["MemberID"] + "and Transactionid is:" + dtgetmsrno.Rows[0]["txnID"].ToString(), "0");
                    if (cnt > 0)
                    {
                       // string Img = uploadPanImage(FileUploadadressImage);
                        cls.update_data("update [GSTReturn_new] set RequestStatus='Success',RefNo='" + txt_refno.Text + "',Remarks='" + txtadminRemark.Text.Trim() + "',ModifiedDate=getdate() where GSTreturnkid=" + Convert.ToInt32(hdnid.Value) + "");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);
                        fillEmployee();
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some error Occured Please Try Again!!');disablePopup();", true);

        }


    }

    protected void btnFail_Click(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select MemberId ,* from GSTReturn_new inner join  tblmlm_membermaster  on tblmlm_membermaster.msrno=GSTReturn_new.RequestByMsrno where GSTreturnkid='" + hdnid.Value + "'");
        if (dt.Rows.Count > 0)
        {
            Decimal Amount = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
            string MemberID = Convert.ToString(dt.Rows[0]["MemberId"].ToString());
            string TxnID = dt.Rows[0]["txnid"].ToString();
            if (MemberID != "" && MemberID != null)
            {
                cls.update_data("update [GSTReturn_new] set RequestStatus='failed',RefNo='0',receipt='',Remarks='" + txtadminRemark.Text.Trim() + "',ModifiedDate=getdate() where GSTreturnkid=" + Convert.ToInt32(hdnid.Value) + "");
                cls_myMember clsm = new cls_myMember();
                clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse - GST Return Request TxnID:-" + TxnID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);
                fillEmployee();
            }
        }
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
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf" || Extension == ".doc" || Extension == ".docx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                    objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
                    objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);

                    return FileName;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select JPG/JPEG/PNG/GIF/PDF/Word/Text File Only!');", true);

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