using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Web.Services;

public partial class Root_Retailer_Amozonstore : System.Web.UI.Page
{
    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtRetailer"] != null)
        {
            DataTable dtmember = (DataTable)Session["dtRetailer"];
            if (dtmember.Rows.Count > 0)
            {
                int msrno = Convert.ToInt32(dtmember.Rows[0]["MsrNo"]);
                banner1();
                banner2();
                banner3();
                banner4();
                banner5();
                banner6();

            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }

    }

    #endregion

    #region banner
    public void banner1()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners where bannertype=1 order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }

    }
    public void banner2()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners where bannertype=2 order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater2.DataSource = dt;
            repeater2.DataBind();
        }

    }
    public void banner3()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners where bannertype=3 order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater3.DataSource = dt;
            repeater3.DataBind();
        }

    }
    public void banner4()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners where bannertype=4 order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater4.DataSource = dt;
            repeater4.DataBind();
        }

    }
    public void banner5()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners where bannertype=5 order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater5.DataSource = dt;
            repeater5.DataBind();
        }

    }
    public void banner6()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners where bannertype=6 order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater6.DataSource = dt;
            repeater6.DataBind();
        }


    }
    #endregion

}