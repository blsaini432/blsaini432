using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Configuration;
using System.Net;

public partial class adminlogin : System.Web.UI.Page
{
    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpBrowserCapabilities httpBrowser = Request.Browser;
            bool enableJavascript = httpBrowser.JavaScript;
            if (enableJavascript == true)
            {
              
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Must enabled javascript from your browser !!');", true);
            }
        }
    }
    #endregion

    
}