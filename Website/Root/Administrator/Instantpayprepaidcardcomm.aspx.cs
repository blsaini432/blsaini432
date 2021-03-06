using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;

public partial class Root_Admin_Instantpayprepaidcardcomm : System.Web.UI.Page
{
    #region [Properties]
    DataTable dtOperator = new DataTable();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_connection objconnection = new cls_connection();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["myCommission"] = null;
            fillPackage();
            dvadd.Visible = false;
            gvOperator.Visible = false;
            dvbutton.Visible = false;
        }
    }

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        try
        {
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@Action", value = "D" });
            _lstparm.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
            objconnection.select_data_dtNew("sp_set_Instantpayprepaidcard_commission ", _lstparm);
            DataTable dt = new DataTable();
            dt = (DataTable)Session["myCommission"];
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                List<ParmList> _lstparmS = new List<ParmList>();
                _lstparmS.Add(new ParmList() { name = "@Action", value = "I" });
                _lstparmS.Add(new ParmList() { name = "@PackageId", value = Convert.ToInt32(ddlPackage.SelectedValue) });
                _lstparmS.Add(new ParmList() { name = "@startval", value = Convert.ToDecimal(dt.Rows[j][2].ToString()) });
                _lstparmS.Add(new ParmList() { name = "@endval", value = Convert.ToDecimal(dt.Rows[j][3].ToString()) });
                _lstparmS.Add(new ParmList() { name = "@Amount", value = Convert.ToDecimal(dt.Rows[j][4].ToString()) });
                _lstparmS.Add(new ParmList() { name = "@IsFlat", value = Convert.ToDecimal(dt.Rows[j][5].ToString()) });
                objconnection.select_data_dtNew("sp_set_Instantpayprepaidcard_commission ", _lstparmS);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
                clear();
            }
        }
        catch (Exception ex)
        {

        }
        #endregion
    }
    #endregion

    #region [Reset]
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
    }
    #endregion

    #region [All Functions]
    private void clear()
    {
        dvadd.Visible = false;
        dvbutton.Visible = false;
        ddlPackage.SelectedIndex = 0;
        gvOperator.Visible = false;

    }


    public void fillPackage()
    {
        DataTable dtPackage = new DataTable();
        dtPackage = objPackage.ManagePackage("GetByMsrNo", 1);
        ddlPackage.DataSource = dtPackage;
        ddlPackage.DataValueField = "PackageID";
        ddlPackage.DataTextField = "PackageName";
        ddlPackage.DataBind();
        ddlPackage.Items.Insert(0, new ListItem("Select Package", "0"));
    }
    #endregion

    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlPackage.SelectedItem.Value) == 0)
        {
            gvOperator.DataSource = dtOperator;
            gvOperator.DataBind();
            dvadd.Visible = false;
            gvOperator.Visible = false;
            dvbutton.Visible = false;
        }
        else
        {
            dtOperator = objconnection.select_data_dt("Exec MM_InstantPayprepaidcommision 0,'" + ddlPackage.SelectedValue + "','0',0");
            DataView dv = dtOperator.DefaultView;
            DataTable sortedDT = dv.ToTable();
            Session["myCommission"] = sortedDT;
            gvOperator.DataSource = sortedDT;
            gvOperator.DataBind();
            dvadd.Visible = true;
            gvOperator.Visible = true;
            dvbutton.Visible = true;
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (Session["myCommission"] != null)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["myCommission"];
            if (hdnidd.Value == "")
            {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = "0";
                NewRow[1] = ddlPackage.SelectedValue;
                NewRow[2] = txtfromd.Text;
                NewRow[3] = txttod.Text;
                NewRow[4] = txtCommission.Text;
                NewRow[5] = chkflatd.Checked;//.ToString();
                dt.Rows.Add(NewRow);
                Session["myCommission"] = dt;
                gvOperator.DataSource = dt;
                gvOperator.DataBind();
                txtfromd.Text = "0"; txttod.Text = "0"; txtCommission.Text = "0"; chkflatd.Checked = false;
            }
            else
            {
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(2, txtfromd.Text);
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(3, txttod.Text);
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(4, txtCommission.Text);
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(5, chkflatd.Checked);
                Session["myCommission"] = dt;
                gvOperator.DataSource = dt;
                gvOperator.DataBind();
                hdnidd.Value = ""; txtfromd.Text = "0"; txttod.Text = "0"; txtCommission.Text = "0"; chkflatd.Checked = false;
            }
        }
    }

    protected void gvOperator_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "medit")
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["myCommission"];
            int x = Convert.ToInt32(e.CommandArgument.ToString());
            x = x - 1;
            hdnidd.Value = x.ToString();
            txtfromd.Text = dt.Rows[x][2].ToString();
            txttod.Text = dt.Rows[x][3].ToString();
            txtCommission.Text = dt.Rows[x][4].ToString();
            chkflatd.Checked = Convert.ToBoolean(dt.Rows[x][5]);
        }
        if (e.CommandName == "mdelete")
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["myCommission"];
            int x = Convert.ToInt32(e.CommandArgument.ToString());
            x = x - 1;
            dt.Rows.RemoveAt(x);
            Session["myCommission"] = dt;
            gvOperator.DataSource = dt;
            gvOperator.DataBind();
        }
    }
}