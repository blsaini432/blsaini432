using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Root_Distributor_Creditcard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
            Root_UC_Creditcard.msrno = MsrNo;
        }
    }
}