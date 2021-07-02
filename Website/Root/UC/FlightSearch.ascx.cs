using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;


public partial class Root_UC_FlightSearch : System.Web.UI.UserControl
{
    #region Properties
    private EzulixAir eAir = new EzulixAir();
    private string Result = string.Empty;
    public static int JourneyType = 0;
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

    #region Method
    private void SearchCriteria(string Depart, string Going, int Adults, int Childs, int Infants, string DeaprtDate, string ReturnDate)
    {
        try
        {
            Session["SearchCriteria"] = null;
            StringBuilder strbuildSearch = new StringBuilder();
            strbuildSearch.Append("" + Depart + "&nbsp;&nbsp;<img src='~/flight/Images/flight/arrow.gif'  runat='server' style='float:none; width: 33px; height: 18px;'></img>");
            strbuildSearch.Append("" + Going + ",&nbsp;&nbsp;" + DeaprtDate + "");
            if (Adults != 0)
            {
                strbuildSearch.Append(",&nbsp;" + Adults + "Adult(s)");
            }
            if (Childs != 0)
            {
                strbuildSearch.Append(",&nbsp;" + Childs + "Child(s)");
            }
            if (Infants != 0)
            {
                strbuildSearch.Append(",&nbsp;" + Infants + "Infant(s)");
            }
            if (ReturnDate != string.Empty)
            {
                strbuildSearch.Append(",&nbsp;Return&nbsp;" + ReturnDate + "");
            }
            Session["SearchCriteria"] = strbuildSearch.ToString();
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('mobilerecharge.aspx');", true);
        }
    }

    public static void AjaxMessageBox(Control page, string msg)
    {
        try
        {
            string script = "alert('" + msg + "')";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "UserSecurity", script, true);
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('mobilerecharge.aspx');", true);
        }
    }
    #endregion

    #region Evetns
    protected void btn_flightonewaysearch_Click(object sender, EventArgs e)
    {
        try
        {
            int paxcount = Convert.ToInt32(txt_Adults.Value) + Convert.ToInt32(txt_Child.Value) + Convert.ToInt32(txt_Infant.Value);

            Session["AdultCount"] = Convert.ToInt32(txt_Adults.Value);
            Session["ChildCount"] = Convert.ToInt32(txt_Child.Value);
            Session["InfantCount"] = Convert.ToInt32(txt_Infant.Value);
            Session["TotalPax"] = paxcount;

            if (paxcount > 9)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + "Total number of passenger count can not be more than Nine 9" + "');location.replace('FlightSearch.aspx');", true);
                
            }
            else
            {
                if (Convert.ToInt32(txt_Adults.Value) != 0 || Convert.ToInt32(txt_Child.Value) != 0)
                {
                    Result = string.Empty;
                    string Origin = txt_DepartFrom.Text.Substring(txt_DepartFrom.Text.IndexOf("(") + 1, 3);
                    string Destination = txt_GoingTo.Text.Substring(txt_GoingTo.Text.IndexOf("(") + 1, 3);
                    string DateDeparture = txt_DepartDate.Text + "T00:00:00";
                    bool Isdirect = false;
                    if (chk_OnewayDirect.Checked)
                    {
                        Isdirect = true;
                    }
                    Result = eAir.FlightSearch(Convert.ToInt32(txt_Adults.Value), Convert.ToInt32(txt_Child.Value), Convert.ToInt32(txt_Infant.Value), 1, Origin, Destination, ddl_Class.SelectedIndex, DateDeparture, DateDeparture, Isdirect, false, false);
                    if (Result != string.Empty)
                    {
                        DataSet ds = eAir.Deserialize(Result);
                        if (ds.Tables.Contains("Response"))
                        {
                            if (ds.Tables["Response"].Rows[0]["ResponseStatus"].ToString() == "1")
                            {
                                Session["JourneyType"] = null;
                                Session["JourneyType"] = 1;
                                Session["FlightList"] = null;
                                Session["Origin"] = Origin;
                                Session["Destination"] = Destination;
                                Session["FlightList"] = ds;
                                SearchCriteria(txt_DepartFrom.Text.Trim(), txt_GoingTo.Text.Trim(),
                                    Convert.ToInt32(txt_Adults.Value), Convert.ToInt32(txt_Child.Value), Convert.ToInt32(txt_Infant.Value), txt_DepartDate.Text, string.Empty);

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "window.location ='FlightList.aspx';", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString() + "');location.replace('FlightSearch.aspx');", true);
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('"+ Result +"');location.replace('FlightSearch.aspx');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + "Empty Response" + "');location.replace('FlightSearch.aspx');", true);
                        
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + "Please select one adult or one child"  + "');location.replace('FlightSearch.aspx');", true);
                    
                    return;
                }
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
            return;
        }
    }

    protected void btn_flightreturnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            int paxcount = Convert.ToInt32(txt_AdultsReturn.Value) + Convert.ToInt32(txt_ChildReturn.Value) + Convert.ToInt32(txt_InfantReturn.Value);
            Session["AdultCount"] = Convert.ToInt32(txt_Adults.Value);
            Session["ChildCount"] = Convert.ToInt32(txt_Child.Value);
            Session["InfantCount"] = Convert.ToInt32(txt_Infant.Value);
            Session["TotalPax"] = paxcount;
            if (paxcount > 9)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "failed", "alert('" + "Total number of passenger count can not be more than Nine 9" + "');location.replace('FlightSearch.aspx');", true);
                
            }
            else
            {
                if (Convert.ToInt32(txt_AdultsReturn.Value) != 0 || Convert.ToInt32(txt_ChildReturn.Value) != 0)
                {
                    Result = string.Empty;
                    string Origin = txt_DepartFromReturn.Text.Substring(txt_DepartFromReturn.Text.IndexOf("(") + 1, 3);
                    string Destination = txt_GoingtoReturn.Text.Substring(txt_GoingtoReturn.Text.IndexOf("(") + 1, 3);
                    string DateDeparture = txt_DepartdateReturn.Text + "T00:00:00";
                    string DateReturn = txt_Returndate.Text + "T00:00:00";
                    bool Isdirect = false;
                    if (chk_OnewayDirect.Checked)
                    {
                        Isdirect = true;
                    }
                    Result = eAir.FlightSearch(Convert.ToInt32(txt_AdultsReturn.Value), Convert.ToInt32(txt_ChildReturn.Value), Convert.ToInt32(txt_InfantReturn.Value), 2, Origin, Destination, ddl_ClassReturn.SelectedIndex, DateDeparture, DateReturn, Isdirect, false, false);
                    if (Result != string.Empty)
                    {
                        DataSet ds = eAir.Deserialize(Result);
                        if (ds.Tables.Contains("Response"))
                        {
                            if (ds.Tables["Response"].Rows[0]["ResponseStatus"].ToString() == "1")
                            {
                                Session["JourneyType"] = null;
                                Session["JourneyType"] = 2;
                                Session["Origin"] = Origin;
                                Session["Destination"] = Destination;
                                Session["FlightList"] = null;
                                Session["FlightList"] = ds;
                                SearchCriteria(txt_DepartFromReturn.Text.Trim(), txt_GoingtoReturn.Text.Trim(), Convert.ToInt32(txt_AdultsReturn.Value), Convert.ToInt32(txt_ChildReturn.Value), Convert.ToInt32(txt_InfantReturn.Value), txt_DepartdateReturn.Text, txt_Returndate.Text);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "location.replace('FlightList.aspx');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('"+ ds.Tables["Error"].Rows[0]["ErrorMessage"].ToString()  + "');location.replace('FlightSearch.aspx');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('"+  Result +"');location.replace('FlightSearch.aspx');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select one adult or one child');location.replace('FlightSearch.aspx');", true);
                    return;
                }
            }
        }
        catch (Exception err)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + err.Message.ToString() + "');location.replace('FlightSearch.aspx');", true);
            return;
        }
    }
    #endregion
}
