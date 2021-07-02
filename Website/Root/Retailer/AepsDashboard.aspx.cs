using System;
using System.Web.UI;
using System.Data;
using System.Xml;
using System.IO;
using Newtonsoft.Json;

public partial class Root_Retailer_AepsDashboard : System.Web.UI.Page
{
    #region Properties
    EzulixAepsV1 EAeps = new EzulixAepsV1();
    private DataTable dtMember = new DataTable();
    private string Result = string.Empty;
    cls_connection Cls = new cls_connection();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Btn_Scan);
        if (!IsPostBack)
        {
            try
            {
                bindbanner();
                DataTable dtMember = (DataTable)Session["dtRetailer"];
                DataTable dt = new DataTable();
                dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='AEPS', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["isaeps"]))
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Aeps Service is not active!');window.location ='DashBoard.aspx';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Aeps Service is not active!');window.location ='DashBoard.aspx';", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Aeps Service is not active!');window.location ='DashBoard.aspx';", true);
            }
        }
    }

    #region bind
    public void bindbanner()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblserviceBanners where isactive=1 and ServiceName=' AEPS ' order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }

    }
    #endregion

    #region Method
    private DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
    #endregion


    protected void Btn_Scan_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtMember = (DataTable)Session["dtRetailer"];
            DataTable dt = new DataTable();
            dt = Cls.select_data_dt(@"EXEC Set_EzulixDmr @action='AEPS', @msrno=" + dtMember.Rows[0]["MsrNo"] + "");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["isaeps"]))
                {
                    if (Session["dtRetailer"] == null)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Time Out, Please login again!');window.location ='DashBoard.aspx';", true);
                        return;
                    }
                    dtMember = (DataTable)Session["dtRetailer"];
                    Result = string.Empty;
                    Result = EAeps.SSORequest(Txt_Amount.Text.Trim(), Txt_Mobile.Text.Trim(), Rb_ServiceType.SelectedValue, dtMember.Rows[0]["MemberID"].ToString());
                    if (Result != string.Empty)
                    {
                        Response.Clear();
                        Response.Write(Result);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Aeps Service is not active!');window.location ='DashBoard.aspx';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Aeps Service is not active!');window.location ='DashBoard.aspx';", true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('!Error');", true);
        }
    }

    protected void Rb_ServiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Rb_ServiceType.SelectedValue == "10" || Rb_ServiceType.SelectedValue == "004" || Rb_ServiceType.SelectedValue == "002")
        {
            Txt_Amount.Text = "0";
            Txt_Amount.Enabled = false;
            RangeValidator_Amount.Enabled = false;
        }
        else
        {
            RangeValidator_Amount.Enabled = true;
            Txt_Amount.Enabled = true;
        }
    }
}