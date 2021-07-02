using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Xml;
using System.IO;
using Newtonsoft.Json;


public partial class Root_Distributor_Manageservice : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    EzulixBBPSAPI eBbps = new EzulixBBPSAPI();
    cls_connection oBJCONNECTION = new cls_connection();
    DataTable dt = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Session["dtDistributor"]!=null)
            {
                DataTable dtMemberMaster = new DataTable();
                dtMemberMaster = (DataTable)Session["dtDistributor"];
                BindServices();

            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }

    public void BindServices()
    {
        if (HttpContext.Current.Session["dtDistributor"] != null)
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["dtDistributor"];
            string memberid = dt.Rows[0]["MemberId"].ToString();
            DataTable dtEWalletTransaction = new DataTable();
            cls_connection cls = new cls_connection();
            List<ParmList> _lstparm = new List<ParmList>();
            _lstparm.Add(new ParmList() { name = "@memberid", value = memberid });
            dtEWalletTransaction = cls.select_data_dtNew("Sp_getservices", _lstparm);
            rep.DataSource = dtEWalletTransaction;
            rep.DataBind();
        }
    }
}