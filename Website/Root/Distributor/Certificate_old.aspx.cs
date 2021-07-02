using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Root_Retailer_Certificate : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
            lblmemberid.Text = dtMemberMaster.Rows[0]["MemberId"].ToString();
          //  MemberType.Text = dtMemberMaster.Rows[0]["MemberType"].ToString();
          lbladdress.Text = dtMemberMaster.Rows[0]["Address"].ToString();
            lbldateofissue.Text = dtMemberMaster.Rows[0]["DOJ"].ToString();
            lblmobile.Text = dtMemberMaster.Rows[0]["mobile"].ToString();
            lblname.Text = dtMemberMaster.Rows[0]["FirstName"].ToString() + " " + dtMemberMaster.Rows[0]["LastName"].ToString();
            DataTable dtt = new DataTable();
            dtt = cls.select_data_dt("select * from tblCompany where CompanyID=2");
            if (dtt.Rows.Count > 0)
            {
               // lblcompanyname.Text = dtt.Rows[0]["CompanyName"].ToString();
              //lblcompanynm.Text = dtt.Rows[0]["CompanyName"].ToString();

                img1.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtt.Rows[0]["CompanyLogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtt.Rows[0]["CompanyLogo"]);
                
            }

        }
    }
}