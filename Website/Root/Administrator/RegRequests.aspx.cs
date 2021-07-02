using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
public partial class Root_Admin_RegRequests : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    DataTable dtExport = new DataTable();
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();
    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    string condition = " SerialNo > 0";
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cls.fill_MemberType(ddlmymembertype, "");
            fillHistory();
            GridViewSortDirection = SortDirection.Descending;
        }
    }
    protected void ddlMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPackagebyTypeid(ddlmymembertype.SelectedValue);
        FillMembers(ddlmymembertype.SelectedValue);
        if (ddlmymembertype.SelectedValue == "5")
            mvw1.ActiveViewIndex = 0;
        else if (ddlmymembertype.SelectedValue == "4")
            mvw1.ActiveViewIndex = 1;
        else if (ddlmymembertype.SelectedValue == "3")
            mvw1.ActiveViewIndex = 2;
        else if (ddlmymembertype.SelectedValue == "2")
            mvw1.ActiveViewIndex = 3;
    }
    protected void FillPackagebyTypeid(string xx)
    {
        DataTable dtPackage = new DataTable();
        dtPackage = cls.select_data_dt("Exec getPackagebyTypeid 1," + xx + "");
        ddlmypackage.DataSource = dtPackage;
        ddlmypackage.DataValueField = "PackageID";
        ddlmypackage.DataTextField = "PackageName";
        ddlmypackage.DataBind();
        ddlmypackage.Items.Insert(0, new ListItem("Select Package", "0"));
    }
    protected void FillMembers(string xx)
    {
        DataTable dtPackage = new DataTable();
        dtPackage = cls.select_data_dt("Exec Proc_FillParentMembers " + xx + "");
        ddlparentmember.DataSource = dtPackage;
        ddlparentmember.DataValueField = "msrno";
        ddlparentmember.DataTextField = "membername";
        ddlparentmember.DataBind();
        ddlparentmember.Items.Insert(0, new ListItem("Select member", "0"));
    }
    public void fillPackage()
    {
        DataTable dtPackage = new DataTable();
        dtPackage = objPackage.ManagePackage("GetByMsrNo", 1);
        ddlmypackage.DataSource = dtPackage;
        ddlmypackage.DataValueField = "PackageID";
        ddlmypackage.DataTextField = "PackageName";
        ddlmypackage.DataBind();
        ddlmypackage.Items.Insert(0, new ListItem("Select Package", "0"));
    }
    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    #endregion

    #region [Function]

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
    protected void gvHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "process")
        {
            try
            {
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("Select * from tblmlm_tempMemberMaster where msrno='" + e.CommandArgument.ToString() + "'");

                if (dt.Rows.Count > 0)
                {
                    string returndiv = "<div style='width:100%; margin:0px; padding:0px; float:left;'><table width='100%' id='topopup2tbl'>";
                    returndiv += "<tr><td>Member ID :</td><td>" + dt.Rows[0]["MemberID"].ToString() + "</td></tr>";
                    returndiv += "<tr><td>Member Name :</td><td>" + dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString() + "</td></tr>";
                    returndiv += "</table></div>";
                    litMember.Text = returndiv;
                    hdnid.Value = dt.Rows[0]["msrno"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);
                }
            }
            catch
            {
                Function.MessageBox("Sorry.Error in loading transaction !!");
            }
        }
        if (e.CommandName == "Reject")
        {
            try
            {

                //cls.update_data("Delete from tblmlm_tempMemberMaster where msrno='" + hdnid.Value + "'");
                //cls.update_data("update tblmlm_tempMemberMaster set isactive='0',isdelete='1' where msrno=" + e.CommandArgument + "");
                cls.update_data("Delete from tblmlm_tempMemberMaster where msrno='" + e.CommandArgument + "'");
                fillHistory();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request declined !!');disablePopup();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('User Delete Successfully !!');", true);
            }
            catch
            {
                Function.MessageBox("Sorry.Error in loading transaction !!");
            }
        }
    }
    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gvHistory.PageIndex = e.NewPageIndex;
        fillHistory();
    }
    protected void gvHistory_RowDeleting(object sender, GridViewPageEventArgs e)
    {

        gvHistory.PageIndex = e.NewPageIndex;
        fillHistory();
    }
    protected void gvHistory_Sorting(object sender, GridViewSortEventArgs e)
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
            gvHistory.DataSource = dv;
            gvHistory.DataBind();
        }
        catch (Exception ex)
        { }
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
                dtExport.Columns.Remove("IsDelete");
                dtExport.Columns.Remove("IsActive");
                Common.Export.ExportToExcel(dtExport, "History_Report");
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
                dtExport.Columns.Remove("IsDelete");
                dtExport.Columns.Remove("IsActive");
                Common.Export.ExportToWord(dtExport, "History_Report");
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
                dtExport.Columns.Remove("IsDelete");
                dtExport.Columns.Remove("IsActive");
                Common.Export.ExportTopdf(dtExport, "History_Report");
            }
        }
        catch
        { }
    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillHistory();
    }
    private void fillHistory()
    {
        #region Condition
        string date1, date2;
        if (txtfromdate.Text.Trim() == "")
        {
            date1 = cls.select_data_scalar_string("Select Convert(varchar,Dateadd(d,-2,getdate()),101)").ToString();
        }
        else
        { date1 = Convert.ToDateTime(changedatetommddyy(txtfromdate.Text)).ToString("MM/dd/yyyy"); }
        if (txttodate.Text.Trim() == "")
        {
            date2 = cls.select_data_scalar_string("Select Convert(varchar,getdate(),101)").ToString();
        }
        else
        { date2 = Convert.ToDateTime(changedatetommddyy(txttodate.Text)).ToString("MM/dd/yyyy"); }
        #endregion
        dtHistory = cls.select_data_dt("Exec proc_listAllRequests '','','" + date1 + "','" + date2 + "'");
        gvHistory.DataSource = dtHistory;
        gvHistory.DataBind();
        ViewState["dtExport"] = dtHistory.DefaultView.ToTable();
    }
    protected void btnFail_Click(object sender, EventArgs e)
    {
        cls.update_data("Delete from tblmlm_tempMemberMaster where msrno='" + hdnid.Value + "'");
        fillHistory();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request declined !!');disablePopup();", true);
    }
    protected void gvHistory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("RegRequests.aspx");
    }
    protected void btnSuccess_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlparentmember.SelectedValue) > 0 && Convert.ToInt32(ddlmymembertype.SelectedValue) > 0 && Convert.ToInt32(ddlmypackage.SelectedValue) > 0 && Convert.ToInt32(hdnid.Value) > 0)
        {
            try
            {
                if (mvw1.ActiveViewIndex > 0)
                {
                    if (mvw1.ActiveViewIndex == 1)
                    {
                        DSO1_tot.Text = (Convert.ToDouble(DSO1_Admin.Text.Trim()) + Convert.ToDouble(DSO1_self.Text.Trim())).ToString();
                    }
                    else if (mvw1.ActiveViewIndex == 2)
                    {
                        DLC1_tot.Text = (Convert.ToDouble(DLC1_admin.Text.Trim()) + Convert.ToDouble(DLC1_self.Text.Trim())).ToString();
                        DLC2_tot.Text = (Convert.ToDouble(DLC2_admin.Text.Trim()) + Convert.ToDouble(DLC2_self.Text.Trim())).ToString();
                    }
                    else if (mvw1.ActiveViewIndex == 3)
                    {
                        sh1_tot.Text = (Convert.ToDouble(sh1_admin.Text.Trim()) + Convert.ToDouble(sh1_self.Text.Trim())).ToString();
                        sh2_tot.Text = (Convert.ToDouble(sh2_admin.Text.Trim()) + Convert.ToDouble(sh2_self.Text.Trim())).ToString();
                        sh3_tot.Text = (Convert.ToDouble(sh3_admin.Text.Trim()) + Convert.ToDouble(sh3_self.Text.Trim())).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Invalud input !!');disablePopup();", true);
                return;
            }
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@ID", value = 1 });
            _lstparm.Add(new ParmList() { name = "@Action", value = "GetAll" });
            cls_connection cls = new cls_connection();
            DataTable dtCompany = cls.select_data_dtNew("Proc_ManageCompany ", _lstparm);
            if (dtCompany.Rows.Count > 0)
            {

                Session["companyname"] = dtCompany.Rows[0]["CompanyName"].ToString();
                Session["Website"] = dtCompany.Rows[0]["Website"].ToString();

            }
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("Exec Update_RegRequest_Approve '" + hdnid.Value + "','" + ddlparentmember.SelectedValue + "','" + ddlmymembertype.SelectedValue + "','" + ddlmymembertype.SelectedItem.Text + "','" + ddlmypackage.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                dt = cls.select_data_dt("select top 1 msrno,memberid,firstname + ' ' + lastname as membername,email,mobile,password,transactionpassword from tblmlm_membermaster where msrno='" + dt.Rows[0][0].ToString() + "' order by msrno desc");
                if (mvw1.ActiveViewIndex > 0)
                {
                    if (mvw1.ActiveViewIndex == 1)
                    {
                        cls.select_data_dt("Exec ProcReg_ManageFees 0,'" + dt.Rows[0][0].ToString() + "','" + ddlmymembertype.SelectedValue + "','" + DSO1_Admin.Text.Trim() + "','" + DSO1_self.Text.Trim() + "','" + DSO1_tot.Text.Trim() + "',5");
                    }
                    else if (mvw1.ActiveViewIndex == 2)
                    {
                        cls.select_data_dt("Exec ProcReg_ManageFees 0,'" + dt.Rows[0][0].ToString() + "','" + ddlmymembertype.SelectedValue + "','" + DLC1_admin.Text.Trim() + "','" + DLC1_self.Text.Trim() + "','" + DLC1_tot.Text.Trim() + "',5");
                        cls.select_data_dt("Exec ProcReg_ManageFees 0,'" + dt.Rows[0][0].ToString() + "','" + ddlmymembertype.SelectedValue + "','" + DLC2_admin.Text.Trim() + "','" + DLC2_self.Text.Trim() + "','" + DLC2_tot.Text.Trim() + "',4");
                    }
                    else if (mvw1.ActiveViewIndex == 3)
                    {
                        cls.select_data_dt("Exec ProcReg_ManageFees 0,'" + dt.Rows[0][0].ToString() + "','" + ddlmymembertype.SelectedValue + "','" + sh1_admin.Text.Trim() + "','" + sh1_self.Text.Trim() + "','" + sh1_tot.Text.Trim() + "',5");
                        cls.select_data_dt("Exec ProcReg_ManageFees 0,'" + dt.Rows[0][0].ToString() + "','" + ddlmymembertype.SelectedValue + "','" + sh2_admin.Text.Trim() + "','" + sh2_self.Text.Trim() + "','" + sh2_tot.Text.Trim() + "',4");
                        cls.select_data_dt("Exec ProcReg_ManageFees 0,'" + dt.Rows[0][0].ToString() + "','" + ddlmymembertype.SelectedValue + "','" + sh3_admin.Text.Trim() + "','" + sh3_self.Text.Trim() + "','" + sh3_tot.Text.Trim() + "',3");
                    }
                }

                //   RegisterMail.SendRegistrationMail(dt.Rows[0]["memberid"].ToString() + " - " + dt.Rows[0]["membername"].ToString(), dt.Rows[0]["email"].ToString(), dt.Rows[0]["mobile"].ToString(), dt.Rows[0]["password"].ToString(), dt.Rows[0]["transactionpassword"].ToString());
                string[] valueArray = new string[6];
                valueArray[0] = dt.Rows[0]["membername"].ToString();
                valueArray[1] = Session["companyname"].ToString();
                valueArray[2] = dt.Rows[0]["memberid"].ToString();
                valueArray[3] = dt.Rows[0]["password"].ToString();
                valueArray[4] = dt.Rows[0]["transactionpassword"].ToString();
                valueArray[5] = Session["Website"].ToString();
              //  SMS.SendWithVar(dt.Rows[0]["mobile"].ToString(), 26, valueArray, 1);
                DLTSMS.SendWithVar(dt.Rows[0]["mobile"].ToString(), 2, valueArray, 1);
                fillHistory();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Registered Successfully !!');disablePopup();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error in registration process !!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Configuration error !!');", true);
        }
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {

        string date1, date2;
        if (txtfromdate.Text.Trim() == "")
        {
            date1 = cls.select_data_scalar_string("Select Convert(varchar,Dateadd(d,-2,getdate()),101)").ToString();
        }
        else
        { date1 = Convert.ToDateTime(changedatetommddyy(txtfromdate.Text)).ToString("MM/dd/yyyy"); }
        if (txttodate.Text.Trim() == "")
        {
            date2 = cls.select_data_scalar_string("Select Convert(varchar,getdate(),101)").ToString();
        }
        else
        { date2 = Convert.ToDateTime(changedatetommddyy(txttodate.Text)).ToString("MM/dd/yyyy"); }

        dtHistory = cls.select_data_dt("Exec proc_listAllRequests '','','" + date1 + "','" + date2 + "'");
        //dtExport = cls.select_data_dt("Exec proc_listAllRequests '','','" + todate + "','" + fromdate + "'");
        if (dtHistory.Rows.Count > 0)
        {
            Common.Export.ExportToExcel(dtHistory, "Regrequest");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);


        }


    }


}


