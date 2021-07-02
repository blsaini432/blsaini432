<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<script RunAt="server">

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    static void RegisterRoutes(RouteCollection routes)
    {
        routes.MapPageRoute("aeps", "aeps", "~/api/Aeps.aspx");
    }


    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        //if (!Request.IsSecureConnection
        //   {
        //        string path = string.Format("https{0}", Request.Url.AbsoluteUri.Substring(4));
        //       Response.Redirect(path);
        //    }

        String strpath = Convert.ToString(Request.Url);
        char[] separator = new char[] { '/' };
        string[] strspliturl = strpath.Split(separator);
        String strpageName = strspliturl[strspliturl.Length - 1];
        cls_connection objConnection = new cls_connection();

        if (strpageName == "aboutus")
        {
            strpageName = "About_Us";
        }
        if (strpageName == "contactus")
        {
            HttpContext.Current.RewritePath("Contactus.aspx");
        }
        if (strpageName == "offers")
        {
            strpageName = "LatestOffers.aspx";
            HttpContext.Current.RewritePath("LatestOffers.aspx");
        }


        System.Data.DataTable dt = objConnection.select_data_dt("select * from tblPage where REPLACE(PageName,'','')='" + strpageName + "'");
        if (dt.Rows.Count > 0)
        {
            if (strpath.ToUpper().Contains("IMAGES") == false && strpath.ToUpper().Contains("IMG") == false && strpath.ToUpper().Contains("THEMES") == false && strpath.ToUpper().Contains("CSS") == false && strpath.ToUpper().Contains("JS") == false && strpath.ToUpper().Contains("CAPTCHA") == false)
                HttpContext.Current.RewritePath("StaticData.aspx?id=" + Convert.ToString(dt.Rows[0]["PageID"]));

        }
        else if (strpath.ToUpper().Contains("LOGIN") == true && strpath.ToUpper().Contains("IMAGES") == false && strpath.ToUpper().Contains("IMG") == false && strpath.ToUpper().Contains("THEMES") == false && strpath.ToUpper().Contains("CSS") == false && strpath.ToUpper().Contains("JS") == false && strpath.ToUpper().Contains("CAPTCHA") == false)
        {

            if (strpageName.ToLower() == "adminlogin")
            {
                HttpContext.Current.RewritePath("~/Root/Default.aspx");
            }
            //else if (strpageName.ToLower() == "dtlogin")
            //{
            //    HttpContext.Current.RewritePath("~/Root/Distributor/Default.aspx");
            //}
            //else if (strpageName.ToLower() == "mdlogin")
            //{
            //    HttpContext.Current.RewritePath("~/Root/MD/Default.aspx");
            //}
            //else if (strpageName.ToLower() == "rtlogin")
            //{
            //    HttpContext.Current.RewritePath("~/Root/retailer/Default.aspx");
            //}
            else if (strpageName.ToLower() == "login")
            {
                HttpContext.Current.RewritePath("~/Root/MyMemberLogin.aspx");
            }

        }
    }

</script>
