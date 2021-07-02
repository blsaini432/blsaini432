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
public partial class Root_Retailer_InsuranceRequest : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    string condition = " kid > 0";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtmembermaster = new DataTable();
            dtmembermaster = (DataTable)Session["dtRetailer"];
            Session["DistributorMsrNo"] = dtmembermaster.Rows[0]["MsrNo"].ToString();
            fillEmployee();
            GridViewSortDirection = SortDirection.Descending;
        }
    }
    #region [Function]
    private void fillEmployee()
    {
        // int MsrNo = Convert.ToInt32(ViewState["msnoid"]);
        #region Condition
        if (!(string.IsNullOrEmpty(txtfromdate.Text) | string.IsNullOrEmpty(txttodate.Text)))
        {
            condition = condition + " and RequestDate >= '" + txtfromdate.Text + "' AND RequestDate <= '" + txttodate.Text + "'";
        }

        if (ddl_status.SelectedValue.ToString() != "0")
        {
            condition = condition + " and [RequestStatus] ='" + ddl_status.SelectedValue.ToString() + "'";
        }
        if (txt_orderID.Text.Trim() != "")
        {
            condition = condition + " and [txnID] like '%" + txt_orderID.Text.Trim() + "%'";
        }

        if (ddl_status.SelectedValue.ToString() != "0")
        {
            condition = condition + " and [RequestStatus] ='" + ddl_status.SelectedValue.ToString() + "'";
        }
        #endregion
        int msrrno = Convert.ToInt32(Session["DistributorMsrNo"]);

        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Action", value = "L" });
        _list.Add(new ParmList() { name = "@RequestByMsrNo", value = msrrno });
        dtEmployee = cls.select_data_dtNew("Proc_InsuranceDetails_GetSet", _list);
        if (dtEmployee.Rows.Count > 0)
        {
            dtEmployee.DefaultView.RowFilter = condition;
            gvBookedBusList.DataSource = dtEmployee;
            gvBookedBusList.DataBind();
            if (dtEmployee.Rows.Count > 0)
            {
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
        if (e.CommandName == "Show")
        {
            try
            {
                ressf.Visible = true;
                reqrefno.Enabled = true;
                btnSuccess.Visible = true;
                btnSuccess.Enabled = true;
                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                DataTable dt = new DataTable();
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@Action", value = "show" });
                _list.Add(new ParmList() { name = "@kid", value = Convert.ToInt32(idno) });
                dt = cls.select_data_dtNew("Proc_InsuranceDetails_GetSet", _list);
                if (dt.Rows.Count > 0)
                {
                    litMember.Text = dt.Rows[0]["memberInfo"].ToString();
                    hdnid.Value = dt.Rows[0]["kid"].ToString();
                    hdn_memberID.Value = dt.Rows[0]["MemberID"].ToString();
                    litOpname.Text = "Status:-" + dt.Rows[0]["RequestStatus"].ToString();
                    txt_od.Text = dt.Rows[0]["OD"].ToString();
                    txt_od.Enabled = false;
                    txt_prminum.Text = dt.Rows[0]["Primiumamt"].ToString();
                    txt_prminum.Enabled = false;
                    txt_tax.Text = dt.Rows[0]["Tax"].ToString();
                    txt_tax.Enabled = false;
                    txt_tp.Text = dt.Rows[0]["TP"].ToString();
                    txt_tp.Enabled = false;
                    txt_comission.Text = dt.Rows[0]["Comissionamt"].ToString();
                    txt_comission.Enabled = false;
                    txt_discount.Text = dt.Rows[0]["Discount"].ToString();
                    txt_discount.Enabled = false;
                    txt_netpayable.Text = dt.Rows[0]["NetPay"].ToString();
                    txt_netpayable.Enabled = false;
                    txt_refno.Text = dt.Rows[0]["refno"].ToString();
                    txt_refno.Enabled = false;
                    if (dt.Rows[0]["RequestStatus"].ToString() == "Awaitingforpayment" || dt.Rows[0]["RequestStatus"].ToString() == "Payment Done")
                    {
                        btnFail.Enabled = false;
                        btnSuccess.Enabled = false;
                        txtrtremaraks.Enabled = false;
                        btnpayment.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);
                    }


                    if (dt.Rows[0]["Policyimage"] != "" && dt.Rows[0]["RequestStatus"].ToString() == "Success")
                    {

                        txt_comission.Enabled = false;
                        txt_od.Enabled = false;
                        txt_refno.Enabled = false;
                        txt_tax.Enabled = false;
                        txt_tp.Enabled = false;
                        txt_netpayable.Enabled = false;
                        txt_prminum.Enabled = false;
                        txt_discount.Enabled = false;
                        //btnFail.Enabled = false;
                        btnSuccess.Enabled = false;

                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);
                }


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
                btnFail.Enabled = true;
                btnFail.Visible = true;
                btnSuccess.Visible = false;
                btnSuccess.Enabled = false;
                string idno = "0";
                idno = Convert.ToString(e.CommandArgument);
                DataTable dt = new DataTable();
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@Action", value = "B" });
                _list.Add(new ParmList() { name = "@kid", value = Convert.ToInt32(idno) });
                dt = cls.select_data_dtNew("Proc_PanCardDetails_GetSet", _list);
                if (dt.Rows.Count > 0)
                {
                    litMember.Text = dt.Rows[0]["memberInfo"].ToString();
                    LitTransaction.Text = dt.Rows[0]["Traninfo"].ToString();
                    hdnid.Value = dt.Rows[0]["Pankid"].ToString();
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
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillEmployee();
    }

    protected void btnpayment_Click(object sender, EventArgs e)
    {
        DataTable dtMemberMaster = (DataTable)Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
        cls_myMember clsm = new cls_myMember();
        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(txt_prminum.Text), MsrNo);
        if (result > 0)
        {
            string TxnID = clsm.Cyrus_GetTransactionID_New();
            clsm.Wallet_MakeTransaction(dtMemberMaster.Rows[0]["MemberId"].ToString(), Convert.ToDecimal("-" + txt_prminum.Text), "Dr", "Insurance Request TxnID:-" + TxnID);
            int cnt = 0;
            cnt = clsm.Wallet_Addfund(MsrNo, dtMemberMaster.Rows[0]["MemberId"].ToString(), Convert.ToDecimal(txt_comission.Text), "Insurance Comission from id " + hdnid.Value + "and Transactionid is:" + TxnID, "0");
            if (cnt > 0)
            {
                cls.update_data("update [tblInsurance] set RequestStatus='Payment Done',RefNo='" + txt_refno.Text + "',OD='" + Convert.ToDecimal(txt_od.Text) + "',TP='" + Convert.ToDecimal(txt_tp.Text) + "',Discount='" + Convert.ToDecimal(txt_discount.Text) + "',Tax='" + Convert.ToDecimal(txt_tax.Text) + "', Primiumamt='" + Convert.ToDecimal(txt_prminum.Text) + "', Comissionamt='" + Convert.ToDecimal(txt_comission.Text) + "',NetPay='" + Convert.ToDecimal(txt_netpayable.Text) + "' ,Remarks='" + txtrtremaraks.Text + "',txnID='" + TxnID + "',  DeclineOrSuccessdate=getdate() where kid=" + Convert.ToInt32(hdnid.Value) + "");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Payment Done successfully !!');disablePopup();", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Wallet Balance!!');disablePopup();", true);
        }
    }
    protected void gvDispute_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        cls.update_data("update [tblInsurance] set RequestStatus='ReSubmitQuotation',RefNo='" + txt_refno.Text + "',OD='" + Convert.ToDecimal(txt_od.Text) + "',TP='" + Convert.ToDecimal(txt_tp.Text) + "',Discount='" + Convert.ToDecimal(txt_discount.Text) + "',Tax='" + Convert.ToDecimal(txt_tax.Text) + "', Primiumamt='" + Convert.ToDecimal(txt_prminum.Text) + "', Comissionamt='" + Convert.ToDecimal(txt_comission.Text) + "',NetPay='" + Convert.ToDecimal(txt_netpayable.Text) + "' ,Remarks='" + txtrtremaraks.Text + "',  DeclineOrSuccessdate=getdate() where kid=" + Convert.ToInt32(hdnid.Value) + "");

        //cls.update_data("update [PanCardDetails] set RequestStatus='Success',RefNo='" + txt_refno.Text + "',ReciptImg='" + Img + "',Remarks='" + txtadminRemark.Text.Trim() + "',DeclineOrSuccessdate=getdate() where Pankid=" + Convert.ToInt32(hdnid.Value) + "");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);
        fillEmployee();

    }

    protected void btnFail_Click(object sender, EventArgs e)
    {
        cls.update_data("update [tblInsurance] set RequestStatus='Awaitingforpayment',RefNo='" + txt_refno.Text + "',OD='" + Convert.ToDecimal(txt_od.Text) + "',TP='" + Convert.ToDecimal(txt_tp.Text) + "',Discount='" + Convert.ToDecimal(txt_discount.Text) + "',Tax='" + Convert.ToDecimal(txt_tax.Text) + "', Primiumamt='" + Convert.ToDecimal(txt_prminum.Text) + "', Comissionamt='" + Convert.ToDecimal(txt_comission.Text) + "',NetPay='" + Convert.ToDecimal(txt_netpayable.Text) + "' ,Remarks='" + txtrtremaraks.Text + "',  DeclineOrSuccessdate=getdate() where kid=" + Convert.ToInt32(hdnid.Value) + "");

        //cls.update_data("update [PanCardDetails] set RequestStatus='Success',RefNo='" + txt_refno.Text + "',ReciptImg='" + Img + "',Remarks='" + txtadminRemark.Text.Trim() + "',DeclineOrSuccessdate=getdate() where Pankid=" + Convert.ToInt32(hdnid.Value) + "");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Updated successfully !!');disablePopup();", true);
        fillEmployee();
    }

    private string uploadPanImage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
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
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf" || Extension == ".doc" || Extension == ".docx")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                 
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