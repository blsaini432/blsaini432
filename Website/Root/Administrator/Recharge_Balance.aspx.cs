using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Net;
using System.IO;

public partial class Root_Admin_Balance : System.Web.UI.Page
{
    clsRecharge_API objAPI = new clsRecharge_API();
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillAPI();
        }
    }

    private void fillAPI()
    {
        DataTable dt = objAPI.ManageAPI("Get", 0);
        gvAPI.DataSource = dt;
        gvAPI.DataBind();

    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        catch
        {
            return "0";
        }
    }
    protected void gvAPI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //DataRowView drview = e.Row.DataItem as DataRowView;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Literal litAPIID = (Literal)e.Row.FindControl("litAPIID");
            Literal litBalanceURL = (Literal)e.Row.FindControl("litBalanceURL");

            if (Convert.ToInt32(litAPIID.Text) > 0)
            {
                DataTable dtAPI = new DataTable();
                dtAPI = objAPI.ManageAPI("GetAll", Convert.ToInt32(litAPIID.Text));
                //if (Convert.ToInt32(litAPIID.Text) == 18)
                //{
                //    Literal litBalance = (Literal)e.Row.FindControl("litBalance");
                //    litBalanceURL.Text = "Not available";
                //    litBalance.Text = "0";
                //}
                //else
                //{
                if (dtAPI.Rows[0]["BalanceURL"].ToString() != "")
                {
                    string strBalanceAPI = "";
                    if (dtAPI.Rows[0]["B_prm1"].ToString() != "" && dtAPI.Rows[0]["B_prm1val"].ToString() != "")
                    {
                        strBalanceAPI = dtAPI.Rows[0]["BalanceURL"].ToString() + dtAPI.Rows[0]["B_prm1"].ToString() + "=" + dtAPI.Rows[0]["B_prm1val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm2"].ToString() != "" && dtAPI.Rows[0]["B_prm2val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm2"].ToString() + "=" + dtAPI.Rows[0]["B_prm2val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm3"].ToString() != "" && dtAPI.Rows[0]["B_prm3val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm3"].ToString() + "=" + dtAPI.Rows[0]["B_prm3val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm4"].ToString() != "" && dtAPI.Rows[0]["B_prm4val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm4"].ToString() + "=" + dtAPI.Rows[0]["B_prm4val"].ToString();
                    }
                    if (strBalanceAPI.EndsWith("&"))
                        strBalanceAPI = strBalanceAPI.Substring(0, strBalanceAPI.Length - 1);
                    //litBalanceURL.Text = strBalanceAPI;
                    string result = apicall(strBalanceAPI);
                    Literal litBalance = (Literal)e.Row.FindControl("litBalance");
                    litBalanceURL.Text = "";
                    try
                    {

                        string[] split = result.Split(',');
                        litBalance.Text = split[Convert.ToInt32(dtAPI.Rows[0]["B_BalancePosition"].ToString())];
                    }
                    catch
                    {
                        litBalance.Text = result;
                    }
                    //litBalanceURL.Text = result;
                }
                //}
            }
            else
            {
                //EtravleSmartApi ee = new EtravleSmartApi();
                //string outputdata = ee.ApiCallByWebClient_auth("http://agent.etravelsmart.com/etsAPI/api/getMyPlanAndBalance");
                //DataTable dtResult = cls.ConvertJSONToDataTable(outputdata.Replace("balanceAmount\":", "balanceAmount\":\"").Replace(",\"lowBalanceAmount", "\",\"lowBalanceAmount"));
                //Literal litBalance = (Literal)e.Row.FindControl("litBalance");
                //litBalance.Text = dtResult.Rows[0]["balanceAmount"].ToString();
            }
        }
    }
    protected void gvAPI_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}