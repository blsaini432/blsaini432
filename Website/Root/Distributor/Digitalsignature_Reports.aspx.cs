using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Root_Distributor_Digitalsignature_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
            if(dtMemberMaster != null) 
                {
                int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                Root_UC_Digitalsignature_Reports.msrno = MsrNo;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('null');location.replace('dasboard.aspx');", true);

            }
           
        }
    }
}