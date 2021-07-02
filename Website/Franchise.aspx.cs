using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Franchise : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    clsState objState = new clsState();
    clsCity objCity = new clsCity();
    private int checksumValue;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
      

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           
               
                DataTable dts = new DataTable();
               
                List<ParmList> _list = new List<ParmList>();
              //  _list.Add(new ParmList() { name = "@msrno", value = MsrNo });
             //   _list.Add(new ParmList() { name = "@memberID", value = memberID });
                _list.Add(new ParmList() { name = "@Name", value = txt_cardname.Text });
                _list.Add(new ParmList() { name = "@mobile", value = txt_mobiles.Text });
                _list.Add(new ParmList() { name = "@education", value = DROPE.SelectedItem.ToString() });
                _list.Add(new ParmList() { name = "@email", value = txt_email.Text });
                _list.Add(new ParmList() { name = "@pin", value = txt_pin.Text });
                _list.Add(new ParmList() { name = "@area", value = txt_area.Text });
                _list.Add(new ParmList() { name = "@age", value = txt_age.Text });
                _list.Add(new ParmList() { name = "@express", value = txt_exp.Text });
                _list.Add(new ParmList() { name = "@abouts", value = drop_abouts.Text });
                _list.Add(new ParmList() { name = "@amazon", value = drop_amazon.Text });
                _list.Add(new ParmList() { name = "@business", value = drop_business.Text });
                _list.Add(new ParmList() { name = "@address", value = txt_address.Text });
                _list.Add(new ParmList() { name = "@message", value = txt_message.Text });
                _list.Add(new ParmList() { name = "@Action", value = "I" });
                string TxnID = Clsm.Cyrus_GetTransactionID_New();
                _list.Add(new ParmList() { name = "@txn", value = TxnID });
                DataTable dt = new DataTable();
                dt = Cls.select_data_dtNew("Proc_Franchis", _list);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["ID"]) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Successfull!');location.replace('http://atmstore.in');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                }


           
        }
        // }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
        }
    }


    protected void ddlCountryName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillState(Convert.ToInt32(ddlCountryName.SelectedValue));
    }
    protected void ddlStateName_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillCity(Convert.ToInt32(ddlStateName.SelectedValue));
    }
    public void fillCity(int StateID)
    {
        DataTable dtCity = new DataTable();
        dtCity = objCity.ManageCity("GetByStateID", StateID);
        ddlCityName.DataSource = dtCity;
        ddlCityName.DataValueField = "CityID";
        ddlCityName.DataTextField = "CityName";
        ddlCityName.DataBind();
        ddlCityName.Items.Insert(0, new ListItem("Select City", "0"));
    }
    public void fillState(int CountryID)
    {
        DataTable dtState = new DataTable();
        dtState = objState.ManageState("GetByCountryID", CountryID);
        ddlStateName.DataSource = dtState;
        ddlStateName.DataValueField = "StateID";
        ddlStateName.DataTextField = "StateName";
        ddlStateName.DataBind();
        ddlStateName.Items.Insert(0, new ListItem("Select State", "0"));
    }
}




