using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class Portal_Administrator_GSTFeeSettings : System.Web.UI.Page
{
    #region [Properties]
    cls_connection cls = new cls_connection();
   // cls_connection objconnection = new cls_connection();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cls.fill_MemberType(ddlSourceMember, "");
            package();
        }
    }
    public void package()
    {
        DataTable dd = new DataTable();
        //dd = objconnection.select_data_dt("select PackageName,Amount from service_loanfeesettings inner join tblMLM_Package on tblMLM_Package.packageid=service_loanfeesettings.packageid");
        dd = cls.select_data_dt("select membertype,FeeAmount,actiontype from tbl_GSTFeeSettings inner join tblmlm_membership on tblmlm_membership.membertypeid=tbl_GSTFeeSettings.membertypeid");
        GridView1.DataSource = dd;
        GridView1.DataBind();
    }
    protected void ddlSourceMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_amount.Text = Convert.ToString(cls.select_data_scalar_long("select FeeAmount from [tbl_GSTFeeSettings] where  memberTypeID=" + Convert.ToInt32(ddlSourceMember.SelectedValue) + " and actiontype='" + ddlaction.SelectedItem.ToString() + "'"));
    }

    protected void ddlmemberid_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txt_amount.Text != "")
        {
            DataTable dm = new DataTable();
            dm=cls.select_data_dt("select * from tbl_GSTFeeSettings  where memberTypeID=" + Convert.ToInt32(ddlSourceMember.SelectedValue) + " and actiontype='" + ddlaction.SelectedItem.ToString() + "'");
            if(dm.Rows.Count >0)
            {
                cls.update_data("update [tbl_GSTFeeSettings] set FeeAmount=" + Convert.ToDecimal(txt_amount.Text) + " where  memberTypeID=" + Convert.ToInt32(ddlSourceMember.SelectedValue) + " and actiontype='" + ddlaction.SelectedItem.ToString() + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Save Successfully.!');location.replace('GSTFeeSettings.aspx');", true);
            }
            else
            {
                cls.update_data("insert into [tbl_GSTFeeSettings] (FeeAmount,memberTypeID,actiontype)values(" + Convert.ToDecimal(txt_amount.Text) + " ," + Convert.ToInt32(ddlSourceMember.SelectedValue) + ",'" + ddlaction.SelectedItem.ToString() + "')");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Save Successfully.!');location.replace('GSTFeeSettings.aspx');", true);
            }


            
        }
    }
}