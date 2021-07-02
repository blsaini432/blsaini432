using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using BLL;
using System.IO;
using System.Text;
using Common;
using System.Net;

public partial class Recharge_Receipt : System.Web.UI.Page
{
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    cls_connection cls = new cls_connection();
    private void Page_Init(object sender, EventArgs e)
    {
        string domainName = Request.Url.Host.ToLower();
        string[] hostParts = new System.Uri(Request.Url.ToString()).Host.Split('.');

        string domain = String.Join(".", hostParts.Skip(Math.Max(0, hostParts.Length - 2)).Take(2));
        //domain = "http://ezulix.com";
        if (domain.StartsWith("http://"))
        {
            domain = domain.Substring(7, domain.Length - 7);
        }
        if (domain.StartsWith("www."))
        {
            domain = domain.Substring(4, domain.Length - 4);
        }
        //Response.Write(domain);
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select * from tblmlm_membermaster where (s_address like '%" + domain + "%')");
        string resellerID = "";
        if (dt.Rows.Count > 0)
        {
            resellerID = dt.Rows[0]["msrno"].ToString();
            Session.Add("reseller", resellerID);
        }
        else
        {
            resellerID = "1";
            Session.Add("reseller", resellerID);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["OrderID"] = 58;
        if (Request["OrderID"] != null)
        {
            DisplayInvoice2(Convert.ToInt32(Request["OrderID"]));
        }
        if (!IsPostBack)
        {
            string resellerID = Session["reseller"].ToString();
            DataTable dtReseller = new DataTable();
            dtReseller = cls.select_data_dt("Exec ProcMLM_ManageMemberMaster 'Get','" + resellerID + "'");
            rptmycompany.DataSource = dtReseller;
            rptmycompany.DataBind();
            DataTable dtCompany = cls.select_data_dt("select * from tblmlm_ResellerWebsite where resellerid='" + Session["reseller"].ToString() + "'");
            if (dtCompany.Rows.Count > 0)
            {
                imgCompanyLogo.ImageUrl = "~/" + dtCompany.Rows[0]["logourl"].ToString();//"../../Images/Company/actual/" + dtCompany.Rows[0]["CompanyLogo"].ToString();
                
            }
            else
            {
                imgCompanyLogo.ImageUrl = "~/images/dummylogo.png";
                
            }
        }
    }

    protected void DisplayInvoice(int id)
    {
        dtHistory = objHistory.ManageHistory("GetInv_new", id);
        if (dtHistory.Rows.Count > 0)
        {
            repPage.DataSource = dtHistory;
            repPage.DataBind();
            DataTable dtMember = new System.Data.DataTable();
            if (Session["dtDistributor"] != null)
            {
                dtMember = (DataTable)Session["dtDistributor"];
            }
            else if (Session["dtMasterDistributor"] != null)
            {
                dtMember = (DataTable)Session["dtMasterDistributor"];
            }
            else if (Session["dtRetailer"] != null)
            {
                dtMember = (DataTable)Session["dtRetailer"];
            }
            Session["OrderID"] = null;
        }
    }

    protected void DisplayInvoice2(int id)
    {
        dtHistory = objHistory.ManageHistory("GetInv_new", id);
        if (dtHistory.Rows.Count > 0)
        {
            repPage.DataSource = dtHistory;
            repPage.DataBind();
        }
    }

   

    protected void repPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
    }
}