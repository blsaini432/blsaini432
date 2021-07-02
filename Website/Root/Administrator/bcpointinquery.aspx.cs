using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Common;
public partial class Root_Administrator_bcpointinquery : System.Web.UI.Page
{
   
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dtExport = new DataTable();
    string condition = " msrno > 0";
 

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillEmployee();
            GridViewSortDirection = SortDirection.Descending;
        }
    }

   

   
    private void fillEmployee()
    {
        int MsrNo = Convert.ToInt32(ViewState["msnoid"]);

        if (!(string.IsNullOrEmpty(txtfromdate.Text) | string.IsNullOrEmpty(txttodate.Text)))
        {
            condition = condition + " and createdate >= '" + txtfromdate.Text + "' AND createdate <= '" + txttodate.Text + "'";
        }
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Action", value = "u" });
        dtEmployee = cls.select_data_dtNew("Proc_bcpoint", _list);
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
    

   
    protected void gvGST_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "deletesuser")
        {

        }
       
        if (e.CommandName == "Approve")
        {

            try
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);
            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
      
        if (e.CommandName == "Reject")
        {

            try
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);

            }
            catch (Exception ex)
            {

                Function.MessageBox(ex.Message);
            }
        }
       
        if (e.CommandName == "WordDownload")
        {
        }
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
  

 

    protected void gvGST_PageIndexChanging(object sender, EventArgs e)
    {
        fillEmployee();
    }

  

   
    protected void btnexportExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            dtExport = (DataTable)ViewState["dtExport"];
            if (dtExport.Rows.Count > 0)
            {
                dtExport.Columns.Remove("msrno");
                Common.Export.ExportToExcel(dtExport, "Report");
            }
        }
        catch
        { }

    }

    

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