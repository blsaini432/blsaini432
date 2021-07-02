using System;
using System.Web.UI;

public partial class FlightPassengerDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try { 
        if (!IsPostBack)
        {

        }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
        }
    }
}