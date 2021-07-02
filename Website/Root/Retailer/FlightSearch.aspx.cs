using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FlightSearch : System.Web.UI.Page
{
    #region Properties
    private DataSet ds = new DataSet();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('mobilerecharge.aspx');", true);
        }
    }

    #region WebMethod
    #region GetAirportList
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAirportList(string prefixText)
    {
        List<string> Output = new List<string>();
        DataTable dt = new DataTable();
        cls_connection ClsDa = new cls_connection();
        dt = ClsDa.select_data_dt("SELECT TOP 10 * FROM m_Airport_List WHERE AirportName LIKE '" + prefixText + "%'");
        
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Output.Add(dt.Rows[i][1].ToString());
        }
        return Output;
    }
    #endregion
    #endregion
}