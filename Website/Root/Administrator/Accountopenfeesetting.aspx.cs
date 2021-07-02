using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.IO;
public partial class Portals_Admin_Accountopenfeesetting : System.Web.UI.Page
{ 
    #region [Properties]
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_connection objconnection = new cls_connection();
    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillPackage();
            //fillservice();
            package();
        }
    }

    public void package()
    {
        DataTable dd = new DataTable();
        dd = objconnection.select_data_dt("select PackageName ,Amount from Accountopenfeesettings inner join tblMLM_Package on tblMLM_Package.packageid=Accountopenfeesettings.packageid");
        GridView1.DataSource = dd;
        GridView1.DataBind();
    }
    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        DataTable dt = new DataTable();
        dt = objconnection.select_data_dt("select *  from [Accountopenfeesettings] where PackageID=" + Convert.ToInt32(ddlPackage.SelectedValue));
        if (dt.Rows.Count > 0)
        {
            //decimal commision = Convert.ToDecimal(dt.Rows[0]["Commision"].ToString());
            //txtcomission.Text = commision.ToString();
            objconnection.update_data("update Accountopenfeesettings set Amount='" + Convert.ToDecimal(txtcomission.Text) + "' where PackageID='" + Convert.ToInt32(ddlPackage.SelectedValue) + "'");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Save Successfully.!');location.replace('Accountopenfeesetting.aspx');", true);
        }
        else
        {
            objconnection.insert_data("insert into Accountopenfeesettings(Amount,serviceid,PackageID,IsActive,AddDate)values('" + Convert.ToDecimal(txtcomission.Text) + "',1,'" + Convert.ToInt32(ddlPackage.SelectedValue) + "',1,'" + DateTime.Now + "')");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Save Successfully.!');location.replace('Accountopenfeesetting.aspx');", true);
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
        txtcomission.Text = "";
        ddlPackage.SelectedIndex = 0;
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
   
    //{
    //    DataTable dtPackage = new DataTable();
    //    dtPackage = objconnection.select_data_dt("select * from tblMLM_Services");
    //    ddlservice.DataSource = dtPackage;
    //    ddlservice.DataValueField = "serviceID";
    //    ddlservice.DataTextField = "ServiceName";
    //    ddlservice.DataBind();
    //    ddlservice.Items.Insert(0, new ListItem("Select Service", "0"));
    //}
    #endregion
    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = objconnection.select_data_dt("Select * from Accountopenfeesettings where PackageID='" + Convert.ToInt32(ddlPackage.SelectedValue) + "'");
       
        if (dt.Rows.Count > 0)
        {
            decimal commision = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
            txtcomission.Text = commision.ToString();
        }
        else
        {
            txtcomission.Text = "0.00";
        }
    }
}