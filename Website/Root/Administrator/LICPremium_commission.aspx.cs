using BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Root_Admin_LICPremium_commission : System.Web.UI.Page
{

    #region [Properties]
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_connection objconnection = new cls_connection();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["mySurcharge"] = null;
            fillPackage();
            dvbutton.Visible = false;
            dvadd.Visible = false;
            gvOperator.Visible = false;
        }
    }
    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        Int32 intresult = 0;
        int i = objconnection.delete_data("delete from licpremium_commission where PackageID=" + Convert.ToInt32(ddlPackage.SelectedValue));
        DataTable dt = new DataTable();
        dt = (DataTable)Session["mySurcharge"];
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            intresult = intresult + objconnection.insert_data("insert into licpremium_commission (packageid, startval, endval, surcharge, isFlat) values (" + ddlPackage.SelectedValue + ",'" + dt.Rows[j][2].ToString() + "','" + dt.Rows[j][3].ToString() + "','" + dt.Rows[j][4].ToString() + "','" + dt.Rows[j][5].ToString() + "')");
        }
        if (intresult > 0)
        {
          ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|There are some problem, Please try again !');", true);
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

    }
    //private void fillOperator()
    //{
    //    dtOperator = objOperator.ManageOperator("GetAll", 0);
    //    DataView dv = dtOperator.DefaultView;
    //    dv.Sort = "OperatorID asc";
    //    DataTable sortedDT = dv.ToTable();
    //    gvOperator.DataSource = sortedDT;
    //    gvOperator.DataBind();
    //}

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
            dtOperator = objconnection.select_data_dt("Exec MM_LICPREMIUM_Surcharge 0,'" + ddlPackage.SelectedValue + "','0',0");
            DataView dv = dtOperator.DefaultView;
            DataTable sortedDT = dv.ToTable();
            Session["mySurcharge"] = sortedDT;
            gvOperator.DataSource = sortedDT;
            gvOperator.DataBind();
            dvadd.Visible = true;
            gvOperator.Visible = true;
            dvbutton.Visible = true;
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (Session["mySurcharge"] != null)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["mySurcharge"];
            if (hdnidd.Value == "")
            {

                DataRow NewRow = dt.NewRow();
                NewRow[0] = "0";
                NewRow[1] = ddlPackage.SelectedValue;
                NewRow[2] = txtfromd.Text;
                NewRow[3] = txttod.Text;
                NewRow[4] = txtsurcharged.Text;
                NewRow[5] = chkflatd.Checked;//.ToString();
                dt.Rows.Add(NewRow);
                Session["mySurcharge"] = dt;
                gvOperator.DataSource = dt;
                gvOperator.DataBind();
                txtfromd.Text = "0"; txttod.Text = "0"; txtsurcharged.Text = "0"; chkflatd.Checked = false;
            }
            else
            {
                //DataRow NewRow = dt.NewRow();
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(2, txtfromd.Text);
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(3, txttod.Text);
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(4, txtsurcharged.Text);
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(5, chkflatd.Checked);
                Session["mySurcharge"] = dt;
                gvOperator.DataSource = dt;
                gvOperator.DataBind();
                hdnidd.Value = ""; txtfromd.Text = "0"; txttod.Text = "0"; txtsurcharged.Text = "0"; chkflatd.Checked = false;
            }
        }
    }
    protected void gvOperator_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "medit")
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["mySurcharge"];
            int x = Convert.ToInt32(e.CommandArgument.ToString());
            x = x - 1;
            hdnidd.Value = x.ToString();
            txtfromd.Text = dt.Rows[x][2].ToString();
            txttod.Text = dt.Rows[x][3].ToString();
            txtsurcharged.Text = dt.Rows[x][4].ToString();
            chkflatd.Checked = Convert.ToBoolean(dt.Rows[x][5]);
        }
        if (e.CommandName == "mdelete")
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["mySurcharge"];
            int x = Convert.ToInt32(e.CommandArgument.ToString());
            x = x - 1;
            dt.Rows.RemoveAt(x);
            Session["mySurcharge"] = dt;
            gvOperator.DataSource = dt;
            gvOperator.DataBind();
        }
    }
}