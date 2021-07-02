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

public partial class Root_Administrator_ViewProductsdetails : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    cls_myMember clsm = new cls_myMember();
    string condition = " purchaseid > 0";
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
        //if (txt_orderID.Text.Trim() != "")
        //{
        //    condition = condition + " and [TxnId] like '%" + txt_orderID.Text.Trim() + "%'";
        //}

        if (ddl_status.SelectedValue.ToString() != "0")
        {
            condition = condition + " and [RequestStatus] ='" + ddl_status.SelectedValue.ToString() + "'";
        }

        #endregion
        dtEmployee = cls.select_data_dt("select *,cast((case when RequestStatus ='Pending' then 1 else 0 end )as bit) as IsTempEnable,cast((case when RequestStatus ='Pending' or RequestStatus='Temp Rejected' then 1 else 0 end )as bit) as  IsEnable, cast((case when RequestStatus = 'Pending' then 0 else 1 end) as bit) as StatusSwow, cast((case when RequestStatus = 'Pending' or RequestStatus = 'Temp Rejected' or RequestStatus = 'failed' then 0 else 1 end) as bit) as StatusDTSwow,RequestStatus + ' On ' + convert(nvarchar, isnull(ModifiedDate, '')) as Staussss from[Shooping_Cart_MemberDetails]");
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
                ressf.Visible = true;
                reqrefno.Enabled = true;
                recp.Visible = true;
                //reqrecipt.Enabled = true;
                btnFail.Enabled = false;
                btnFail.Visible = false;
                btnSuccess.Visible = true;
                //btnPending.Visible = true;
                btnSuccess.Enabled = true;
                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select(MemberID + '' + MemberName) as memberInfo,AddDate as RequestDate, RequestStatus,TotalAmount, msrno, MemberID,TxnID, purchaseid from Shooping_Cart_MemberDetails p inner join ViewMLM_MemberSearch s on p.RequestBy=s.MemberID where RequestStatus = 'Pending' and purchaseid =" + idno + "");
                if (dt.Rows.Count > 0)
                {
                  //  litMember.Text = dt.Rows[0]["memberInfo"].ToString();
                    hdnid.Value = dt.Rows[0]["purchaseid"].ToString();
                    hdn_amount.Value = dt.Rows[0]["TotalAmount"].ToString();
                    hdn_memberID.Value = dt.Rows[0]["MemberID"].ToString();
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
                ressf.Visible = false;
                reqrefno.Enabled = false;
                recp.Visible = false;
                //reqrecipt.Enabled = false;
                btnFail.Enabled = true;
                btnFail.Visible = true;
                btnSuccess.Visible = false;
                btnSuccess.Enabled = false;
                //btnPending.Visible = false;
                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select(MemberID + '' + MemberName) as memberInfo,AddDate as RequestDate, RequestStatus, msrno, MemberID,TxnID, purchaseid,TotalAmount from Shooping_Cart_MemberDetails p inner join ViewMLM_MemberSearch s on p.RequestBy=s.MemberID where RequestStatus = 'Pending' and purchaseid =" + idno + "");
                if (dt.Rows.Count > 0)
                {
                  //  litMember.Text = dt.Rows[0]["memberInfo"].ToString();
                    hdnid.Value = dt.Rows[0]["purchaseid"].ToString();
                    hdn_amount.Value = dt.Rows[0]["TotalAmount"].ToString();
                    hdn_memberID.Value = dt.Rows[0]["MemberID"].ToString();
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
                dtExport.Columns.Remove("MsrNo");
                dtExport.Columns.Remove("IsActive");
                dtExport.Columns.Remove("BookingKid");
                dtExport.Columns.Remove("Compct");
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
      //  string Img = uploadPanImage(fupRcpt);
        cls.update_data("update [Shooping_Cart_MemberDetails] set RequestStatus='Success',Remarks='" + txtadminRemark.Text.Trim() + "',ModifiedDate=getdate() where purchaseid=" + Convert.ToInt32(hdnid.Value) + "");
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
            cls.update_data("update [Shooping_Cart_MemberDetails] set RequestStatus='failed',RefNo='0',ReciptImg='',Remarks='" + txtadminRemark.Text.Trim() + "',ModifiedDate=getdate() where purchaseid=" + Convert.ToInt32(hdnid.Value) + "");
            cls_myMember clsm = new cls_myMember();
            clsm.Wallet_MakeTransaction(MemberID, Amount, "Cr", "Reverse - Product Purchase Request TxnID:-" + TxnID);
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
                string opath = Server.MapPath("~/Uploads/ProductImage/Actual/");
                string mpath = Server.MapPath("~/Uploads/ProductImage/Actual/");
                string spath = Server.MapPath("~/Uploads/ProductImage/Actual/");

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
}