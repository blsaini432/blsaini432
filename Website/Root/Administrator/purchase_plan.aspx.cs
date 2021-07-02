using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
public partial class root_admin_purchase_plan : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            memberidbind();
            Session["MsrNo"] = 1;
        }
    }
    public void memberidbind()
    {
        DataTable ddd = new DataTable();
        ddd=cls.select_data_dt("select * from tblmlm_membermaster where isactive=1 and isdelete=0 and membertypeid not in (5) and memberid!='100000'");
        ddlmemberid.DataSource = ddd;
        ddlmemberid.DataTextField = "MemberId";
        ddlmemberid.DataValueField = "msrno";
        ddlmemberid.DataBind();
        ddlmemberid.Items.Insert(0, "Select MemberId");

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (radiobuttonmethod.SelectedIndex == 0)
        {
            try
            {
                DataTable dtmember = new DataTable();
                dtmember = cls.select_data_dt("select * from tblmlm_membermaster where msrno='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "'");
                if (dtmember.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = cls.select_data_dt("select Remaningcount from tblmlm_memberplans_adminapprove where membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "' and msrno='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "' and isactive=1");
                    if (dt.Rows.Count > 0)
                    {
                        int count = Convert.ToInt32(dt.Rows[0]["Remaningcount"].ToString());
                        int sum = count + Convert.ToInt32(DSO1_totid.Text);
                        cls.update_data("update tblmlm_memberplans_adminapprove set Remaningcount='" + sum + "',Lastmodifieddate='" + DateTime.Now + "'  where MsrNo='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "'  and membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "'");
                          DataTable dmt = new DataTable();
                         dmt = cls.select_data_dt("select Remaningcount from tblmlm_memberplans_adminapprovefree where membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "' and msrno='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "' and isactive=1");
                         if (dmt.Rows.Count > 0)
                         {
                             cls.update_data("update tblmlm_memberplans_adminapprovefree set Remaningcount='" + sum + "',Lastmodifieddate='" + DateTime.Now + "'  where MsrNo='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "'  and membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "'");
                             cls.update_data("insert into tblmlm_memberplans_adminapprovenarration(membertype,MsrNo,narration,isactive,Lastmodifieddate)values('" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "','" + Convert.ToInt32(ddlmemberid.SelectedValue) + "','Credit ID" + DSO1_totid.Text + "',1,'" + DateTime.Now + "')");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Added Successfully.!');location.replace('purchase_plan.aspx');", true);
                        }
                         else
                         {
                             cls.update_data("insert into tblmlm_memberplans_adminapprovefree(membertype,MsrNo,Remaningcount,isactive,Lastmodifieddate)values('" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "','" + Convert.ToInt32(ddlmemberid.SelectedValue) + "','" + DSO1_totid.Text + "',1,'" + DateTime.Now + "')");
                             cls.update_data("insert into tblmlm_memberplans_adminapprovenarration(membertype,MsrNo,narration,isactive,Lastmodifieddate)values('" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "','" + Convert.ToInt32(ddlmemberid.SelectedValue) + "','Credit ID" + DSO1_totid.Text + "',1,'" + DateTime.Now + "')");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Added Successfully.!');location.replace('purchase_plan.aspx');", true);
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');location.replace('purchase_plan.aspx');", true);
                    }
                    else
                    {
                        DataTable dmt = new DataTable();
                        dmt = cls.select_data_dt("select Remaningcount from tblmlm_memberplans_adminapprovefree where membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "' and msrno='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "' and isactive=1");
                        if (dmt.Rows.Count > 0)
                        {
                            cls.update_data("update tblmlm_memberplans_adminapprovefree set Remaningcount='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "' ,Lastmodifieddate='" + DateTime.Now + "'  where MsrNo='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "'  and membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "'");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');location.replace('purchase_plan.aspx');", true);

                        }
                        else
                        {
                            cls.update_data("insert into tblmlm_memberplans_adminapprovefree(membertype,MsrNo,Remaningcount,isactive,Lastmodifieddate)values('" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "','" + Convert.ToInt32(ddlmemberid.SelectedValue) + "','" + DSO1_totid.Text + "',1,'" + DateTime.Now + "')");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');location.replace('purchase_plan.aspx');", true);
                        }
                        cls.update_data("insert into tblmlm_memberplans_adminapprove(membertype,MsrNo,Remaningcount,isactive,Lastmodifieddate)values('" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "','" + Convert.ToInt32(ddlmemberid.SelectedValue) + "','" + DSO1_totid.Text + "',1,'" + DateTime.Now + "')");
                        cls.update_data("insert into tblmlm_memberplans_adminapprovenarration(membertype,MsrNo,narration,isactive,Lastmodifieddate)values('" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "','" + Convert.ToInt32(ddlmemberid.SelectedValue) + "','Credit ID" + DSO1_totid.Text + "',1,'" + DateTime.Now + "')");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');location.replace('purchase_plan.aspx');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Function.MessageBox(ex.Message);
            }
        }
        else if (radiobuttonmethod.SelectedIndex == 1)
        {
            try
            {
                if (Convert.ToInt32(Session["maxid"]) > Convert.ToInt32(DSO1_totid.Text))
                {

                    DataTable dtmember = new DataTable();
                    dtmember = cls.select_data_dt("select * from tblmlm_membermaster where msrno='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "'");
                    if (dtmember.Rows.Count > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = cls.select_data_dt("select Remaningcount from tblmlm_memberplans_adminapprove where membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "' and msrno='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "' and isactive=1");
                        if (dt.Rows.Count > 0)
                        {
                            int count = Convert.ToInt32(dt.Rows[0]["Remaningcount"].ToString());
                            int sum = count - Convert.ToInt32(DSO1_totid.Text);
                            cls.update_data("update tblmlm_memberplans_adminapprove set Remaningcount='" + sum + "',Lastmodifieddate='" + DateTime.Now + "'  where MsrNo='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "'  and membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "'");
                            DataTable dmt = new DataTable();
                            dmt = cls.select_data_dt("select Remaningcount from tblmlm_memberplans_adminapprovefree where membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "' and msrno='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "' and isactive=1");
                            if (dmt.Rows.Count > 0)
                            {
                                int countm = Convert.ToInt32(dmt.Rows[0]["Remaningcount"].ToString());
                                int summ = countm - Convert.ToInt32(DSO1_totid.Text);
                                cls.update_data("update tblmlm_memberplans_adminapprovefree set Remaningcount='" + summ + "',Lastmodifieddate='" + DateTime.Now + "'  where MsrNo='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "'  and membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "'");
                                cls.update_data("insert into tblmlm_memberplans_adminapprovenarration(membertype,MsrNo,narration,isactive,Lastmodifieddate)values('" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "','" + Convert.ToInt32(ddlmemberid.SelectedValue) + "','Debit ID no " + DSO1_totid.Text + "',1,'" + DateTime.Now + "')");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');location.replace('purchase_plan.aspx');", true);
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');location.replace('purchase_plan.aspx');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Sorry you can not debit more than max id');", true);
                }
            }
            catch (Exception ex)
            {
                Function.MessageBox(ex.Message);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please select any method');location.replace('purchase_plan.aspx');", true);
        }
    }
    protected void ddlMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dddm = new DataTable();
        dddm = cls.select_data_dt("select * from tblmlm_memberplans_adminapprove where msrno='" + Convert.ToInt32(Session["msrno"]) + "' and membertype='" + ddlmymembertype.SelectedValue + "'");
        if (dddm.Rows.Count > 0)
        {
            string aa = dddm.Rows[0]["Remaningcount"].ToString();
            lblremainingid.Text = "Remaining id is:" + aa;
        }
        else
        {
            lblremainingid.Text = "Remaining id is:0";
        }
    }
    protected void DSO1_totid_TextChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(DSO1_totid.Text) > 0 && Convert.ToInt32(DSO1_totadmin.Text) > 0)
        //{
        //    decimal adminfee = Convert.ToDecimal(DSO1_totadmin.Text);
        //    decimal noid = Convert.ToDecimal(DSO1_totid.Text);
        //    decimal total = noid * adminfee;
        //    DSO1_totalamount.Text = total.ToString();
        //}
    }
    protected void ddlmemberid_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmemberid.SelectedIndex > 0)
        {
            lblremainingid.Text = "";
            DataTable d = new DataTable();
            d = cls.select_data_dt("select * from tblmlm_membermaster where memberid='" + ddlmemberid.SelectedItem.ToString() + "'");
            string membername = d.Rows[0]["FirstName"].ToString() + " " + d.Rows[0]["LastName"].ToString();
            lblMemberName.Visible = true;
            lblMemberName.Text = membername;
            Session["msrno"] = d.Rows[0]["msrno"];
            string membertypeid = d.Rows[0]["membertypeid"].ToString();
            if(membertypeid=="2")
            {
                cls.fill_MemberType(ddlmymembertype, "State Head");
            }
            if (membertypeid == "3")
            {
                cls.fill_MemberType(ddlmymembertype, "Master Distributor");

            }
            if (membertypeid == "4")
            {
                cls.fill_MemberType(ddlmymembertype, "Distributor");

            }
        }
        else
        {
            lblMemberName.Visible = false;
        }
    }
    protected void radiobuttonmethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radiobuttonmethod.SelectedIndex == 1)
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select Remaningcount from tblmlm_memberplans_adminapprovefree where membertype='" + Convert.ToInt32(ddlmymembertype.SelectedValue) + "' and msrno='" + Convert.ToInt32(ddlmemberid.SelectedValue) + "' and isactive=1");
            if (dt.Rows.Count > 0)
            {
                string rmcount = dt.Rows[0]["Remaningcount"].ToString();
                int f = Convert.ToInt32(rmcount) - 1;
                lblmaxid.Text = "you can debit max " + f.ToString() + "id";
                Session["maxid"] = rmcount;
            }
            else
            {
                lblmaxid.Text = "you can not  debit any id";
                DSO1_totid.Enabled = false;
            }
        }
        else
        {
            DSO1_totid.Enabled = true;
            lblmaxid.Text = "";
        }
    }
}