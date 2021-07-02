using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using Common;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;

public partial class Portal_Admin_Accountipen_Request : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    cls_myMember clsm = new cls_myMember();
    string condition = " MsrNo > 0";
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
            condition = condition + " and addDate >= '" + txtfromdate.Text + "' AND addDate <= '" + txttodate.Text + "'";
        }
        //if (txt_orderID.Text.Trim() != "")
        //{
        //    condition = condition + " and [txnID] like '%" + txt_orderID.Text.Trim() + "%'";
        //}

        //if (ddl_status.SelectedValue.ToString() != "0")
        //{
        //    condition = condition + " and [RequestStatus] ='" + ddl_status.SelectedValue.ToString() + "'";
        //}

        #endregion


        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Action", value = "ADMIN" });
        dtEmployee = cls.select_data_dtNew("Proc_accountopenss", _list);
        if (dtEmployee.Rows.Count > 0)
        {
            dtEmployee.DefaultView.RowFilter = condition;
            gvBookedBusList.DataSource = dtEmployee;
            gvBookedBusList.DataBind();
            if (dtEmployee.Rows.Count > 0)
            {
                litrecordcount.Text = gvBookedBusList.Rows.Count.ToString();
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
    protected void gvBookedBusList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [Approve]
        if (e.CommandName == "Approve")
        {

            try
            {
               // ressf.Visible = true;
               // reqrefno.Enabled = true;
               // recp.Visible = true;
               // reqrecipt.Enabled = true;
                btnFail.Enabled = false;
                btnFail.Visible = false;
                btnSuccess.Visible = true;
               
                btnSuccess.Enabled = true;

                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                DataTable dt = new DataTable();
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@Action", value = "B" });
                _list.Add(new ParmList() { name = "@passport_id", value = Convert.ToInt32(idno) });
                dt = cls.select_data_dtNew("Proc_loan_list", _list);
                if (dt.Rows.Count > 0)
                {
                    litMember.Text = dt.Rows[0]["memberInfo"].ToString();
                    LitTransaction.Text = dt.Rows[0]["Traninfo"].ToString();
                    hdnid.Value = dt.Rows[0]["passport_id"].ToString();
                    hdn_memberID.Value = dt.Rows[0]["MemberID"].ToString();
                    hdn_amount.Value = dt.Rows[0]["Amount"].ToString();
                    hdn_txnid.Value = dt.Rows[0]["TxnID"].ToString();
                    litOpname.Text = "Status:-" + dt.Rows[0]["RequestStatus"].ToString();
                }
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
               // ressf.Visible = false;
               // reqrefno.Enabled = false;
               // recp.Visible = false;
                //reqrecipt.Enabled = false;
                btnFail.Enabled = true;
                btnFail.Visible = true;
                btnSuccess.Visible = false;
                btnSuccess.Enabled = false;
              
                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                DataTable dt = new DataTable();
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@Action", value = "B" });
                _list.Add(new ParmList() { name = "@passport_id", value = Convert.ToInt32(idno) });
                dt = cls.select_data_dtNew("Proc_loan_list", _list);
                if (dt.Rows.Count > 0)
                {
                    litMember.Text = dt.Rows[0]["memberInfo"].ToString();
                    LitTransaction.Text = dt.Rows[0]["Traninfo"].ToString();
                    hdnid.Value = dt.Rows[0]["passport_id"].ToString();
                    hdn_memberID.Value = dt.Rows[0]["MemberID"].ToString();
                    hdn_amount.Value = dt.Rows[0]["Amount"].ToString();
                    hdn_txnid.Value = dt.Rows[0]["TxnID"].ToString();
                    litOpname.Text = "Status:-" + dt.Rows[0]["RequestStatus"].ToString();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
        #endregion
        #region[Client Data in Word]

        if (e.CommandName == "downloadmerge")
        {
            string path = e.CommandArgument.ToString();
            string[] array = path.Split('|');
            string path1 = "http://moneyrecharge.co/Root/Upload/PanCardrecipt/Actual/" + array[0];
            string path2 = "http://moneyrecharge.co/Root/Upload/PanCardRequest/Actual/" + array[1];
            string refno = array[2];
            PdfReader pdfReader1 = new PdfReader(path1);
            PdfReader pdfReader2 = new PdfReader(path2);
            List<PdfReader> readerList = new List<PdfReader>();
            readerList.Add(pdfReader1);
            readerList.Add(pdfReader2);
            //Define a new output document and its size, type
            Document document = new Document(PageSize.A4, 0, 0, 0, 0);
            //Get instance response output stream to write output file.
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();

            foreach (PdfReader reader in readerList)
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    document.Add(iTextSharp.text.Image.GetInstance(page));
                }
            }
            document.Close();
            string ffname = refno + ".pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + ffname + "");
            Response.ContentType = "application/pdf";
            Response.End();

        }
        if (e.CommandName == "WordDownload")
        {

            try
            {

                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                DataTable dt = new DataTable();
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@Action", value = "ClientData" });
                _list.Add(new ParmList() { name = "@kid", value = Convert.ToInt32(idno) });
                dt = cls.select_data_dtNew("Proc_PanCardDetails_GetSet", _list);
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

    protected void gvBookedBusList_Sorting(object sender, GridViewSortEventArgs e)
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
            gvBookedBusList.DataSource = dv;
            gvBookedBusList.DataBind();
        }
        catch (Exception ex)
        { }
    }
    #endregion

    #region [ddlPaging]

    protected void gvBookedBusList_PageIndexChanging(object sender, EventArgs e)
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

    protected void gvDispute_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
    string Img = string.Empty;
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //string Img = uploadPanImage(fupRcpt);
        DataTable dtgetmsrno = cls.select_data_dt("select * from  tblloan where food_id='" + Convert.ToInt32(hdnid.Value) + "'");
        cls.update_data("update tblloan set RequestStatus='Success',RefNo='" + txt_refno.Text + "',ReciptImg='" + Img + "',Remarks='" + txtadminRemark.Text.Trim() + "' where passport_id=" + Convert.ToInt32(hdnid.Value) + "");
        fillEmployee();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);


    }


    protected void btnFail_Click(object sender, EventArgs e)
    {
        Decimal Amount = Convert.ToDecimal(hdn_amount.Value);
        string MemberID = Convert.ToString(hdn_memberID.Value);
        string TxnID = hdn_txnid.Value.ToString();
        if (MemberID != "" && MemberID != null)
        {
            cls.update_data("update tblloan  set RequestStatus='failed',RefNo='0',ReciptImg='',Remarks='" + txtadminRemark.Text.Trim() + "' where passport_id=" + Convert.ToInt32(hdnid.Value) + "");
            cls_myMember clsm = new cls_myMember();
            clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse - Loan Request TxnID:-" + TxnID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);
            fillEmployee();
            btnFail.Enabled = true;
        }
    }

    private string uploadPanImage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Root/Upload/PanCardrecipt/Actual/");
                string mpath = Server.MapPath("~/Root/Upload/PanCardrecipt/Medium/");
                string spath = Server.MapPath("~/Root/Upload/PanCardrecipt/Small/");

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
                    //objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
                    //objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);
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



    //public bool getcomission(int pankid)
    //{
    //    int comissionamount = 0;
    //    int cnt = 0;
    //    DataTable dtgetmsrno = cls.select_data_dt("select RequestBymsrno from  PanCardDetails where Pankid='" + pankid + "'");
    //    int msrnodistributor = Convert.ToInt32(dtgetmsrno.Rows[0]["RequestBymsrno"].ToString());
    //    int Amoutpancard = Convert.ToInt32(dtgetmsrno.Rows[0]["Amount"].ToString());
    //    DataTable dtgetdistrfees = cls.select_data_dt("SELECT MemberID,MsrNo,STDCode FROM tblmlm_membermaster WHERE MsrNO='" + msrnodistributor + "'");
    //    if (dtgetdistrfees.Rows.Count > 0)
    //    {
    //        int fees = Convert.ToInt32(dtgetdistrfees.Rows[0]["STDCode"].ToString());
    //        string memberid=dtgetdistrfees.Rows[0]["MemberID"].ToString();
    //        comissionamount = fees - Amoutpancard;
            
    //        cnt = clsm.Wallet_Addfund(Convert.ToInt32(msrnodistributor), memberid, Convert.ToDecimal(comissionamount), "Admin - Add Fund With Credit for PANKID" + pankid, "0");
    //    }
    //    if (cnt > 1)
    //    {
    //        bool status = true;
    //    }
    //    return status;
    //}
}