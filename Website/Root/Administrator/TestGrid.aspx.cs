using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
public partial class TestGrid : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["Ezulixtranid"] !=null)
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select Docs from tbl_offlineservices where Ezulixtranid='" + Convert.ToString(Request.QueryString["Ezulixtranid"]) +"'");
            if (dt.Rows.Count > 0)
            {
                string Jurisdiction = dt.Rows[0]["Docs"].ToString();
                string[] jurisdictionData = Jurisdiction.Split(',');
                grd.DataSource = jurisdictionData;
                grd.DataBind();
            }

        }
       

    }
    
}