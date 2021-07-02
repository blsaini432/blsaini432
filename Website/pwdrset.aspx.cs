using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Data;
public partial class pwdrset : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["utken"] !=null)
        {
            string utken = Request.QueryString["utken"].ToString();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@LoginID", value = utken.Trim() });
            _lstparm.Add(new ParmList() { name = "@Password", value = "" });
            _lstparm.Add(new ParmList() { name = "@Action", value = "checktoken" });
            cls_connection cls = new cls_connection();
            DataTable dt = cls.select_data_dtNew("Proc_UniversalLogin ", _lstparm);
            if (dt.Rows.Count > 0)
            {
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Link has been expired.2');window.location ='userlogin.aspx';", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Link has been expired.3');window.location ='userlogin.aspx';", true);
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["utken"] != null)
        {
            string utken = Request.QueryString["utken"].ToString();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@LoginID", value = utken.Trim() });
            _lstparm.Add(new ParmList() { name = "@Password", value = "" });
            _lstparm.Add(new ParmList() { name = "@Action", value = "checktoken" });
            cls_connection cls = new cls_connection();
            DataTable dt = cls.select_data_dtNew("Proc_UniversalLogin ", _lstparm);
            if (dt.Rows.Count > 0)
            {

                string memberid = dt.Rows[0]["Memberid"].ToString();
                int id = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
                Int32 intresult = 0;
                cls_Universal objUniversal = new cls_Universal();
                intresult = objUniversal.UpdatePassword("UpdateMemberPassword", id, txtconfirmpassword.Text, dt.Rows[0]["Password"].ToString());
                if (intresult > 0)
                {
                    cls.update_data("update tblmlm_membermaster set passwordtoken='' where msrno=" + id + "");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password Changed Successfully');window.location ='userlogin.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Occured Try To reset passwor again');window.location ='userlogin.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Link has been expired.');window.location ='userlogin.aspx';", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This Link has been expired.');window.location ='userlogin.aspx';", true);
        }
    }
}