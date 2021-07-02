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

public partial class Temp_Receipt : System.Web.UI.Page
{
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["OrderID"] = 58;
        if (Request["Transactionid"] != null)
        {
            DisplayInvoice2((Request["Transactionid"]));
        }
        if (!IsPostBack)
        {
            DataTable dtt = new DataTable();
            dtt = cls.select_data_dt("Select * from tblcompany where companyid=2");
            rptmycompany.DataSource = dtt;
            rptmycompany.DataBind();
        }
    }

    protected void DisplayInvoice2(string id)
    {
        DataTable dthistory = new DataTable();
        dtHistory = cls.select_data_dt("select * from t_Ezulix_PayOut_MoneyTransfer where AgentOrderId ='" + id + "'");
        if (dtHistory.Rows.Count > 0)
        {
            repPage.DataSource = dtHistory;
            repPage.DataBind();
        }
    }

    protected void repPage_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    Literal litName = e.Item.FindControl("litName") as Literal;
        //    DataTable dtMember = new System.Data.DataTable();
        //    if (Session["dtDistributor"] != null)
        //    {
        //        dtMember = (DataTable)Session["dtDistributor"];
        //        litName.Text = Convert.ToString(dtMember.Rows[0]["FirstName"]) + " " + Convert.ToString(dtMember.Rows[0]["LastName"]);
        //    }
        //    else if (Session["dtMasterDistributor"] != null)
        //    {
        //        dtMember = (DataTable)Session["dtMasterDistributor"];
        //        litName.Text = Convert.ToString(dtMember.Rows[0]["FirstName"]) + " " + Convert.ToString(dtMember.Rows[0]["LastName"]);
        //    }
        //    else if (Session["dtRetailer"] != null)
        //    {
        //        dtMember = (DataTable)Session["dtRetailer"];
        //        litName.Text = Convert.ToString(dtMember.Rows[0]["FirstName"]) + " " + Convert.ToString(dtMember.Rows[0]["LastName"]);
        //    }
        //    else if (Session["dtEmployee"] != null)
        //    {
        //        dtMember = (DataTable)Session["dtEmployee"];
        //        litName.Text = Convert.ToString(dtMember.Rows[0]["EmployeeName"]);
        //    }
            
        //}
    }
}