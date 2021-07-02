﻿using Newtonsoft.Json;
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
public partial class Root_Distributor_Business : System.Web.UI.Page
{

    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    private int checksumValue;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtDistributor"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dt = new DataTable();
                    DataTable dtMember = (DataTable)Session["dtDistributor"];
                    ViewState["MemberId"] = null;
                    ViewState["MsrNo"] = null;
                    ViewState["dmtmobile"] = null;
                    ViewState["MemberId"] = dtMember.Rows[0]["MemberID"];
                    ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                    ViewState["mobile"] = dtMember.Rows[0]["mobile"];
                }
            }

            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
            }
        }
        else
        {
            Response.Redirect("~/userlogin.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["MsrNo"] != null)
            {
                int Msrno = Convert.ToInt32(ViewState["MsrNo"]);
                DataTable dts = new DataTable();
                dts = Cls.select_data_dt("select MemberID,MemberTypeID from tblmlm_membermaster where Msrno=" + Msrno + "");
                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                List<ParmList> _list = new List<ParmList>();
                _list.Add(new ParmList() { name = "@MsrNo", value = Msrno });
                _list.Add(new ParmList() { name = "@memberID", value = memberID });
                _list.Add(new ParmList() { name = "@Business_Partner", value = DROPE.SelectedValue.ToString() });
                _list.Add(new ParmList() { name = "@NAME", value = txt_cardname.Text });
                _list.Add(new ParmList() { name = "@Present_Business", value = txt_business.Text });
                _list.Add(new ParmList() { name = "@mobile", value = txt_mobile.Text });
                _list.Add(new ParmList() { name = "@Present_income", value = txt_income.Text });
                _list.Add(new ParmList() { name = "@full_address", value = txt_address.Text });
                _list.Add(new ParmList() { name = "@Experinces", value = txt_experance.Text });
                _list.Add(new ParmList() { name = "@invert_rent", value = txt_rent.Text });
                _list.Add(new ParmList() { name = "@Business", value = rdoBtnLstGender.SelectedValue.ToString() });
                _list.Add(new ParmList() { name = "@Action", value = "I" });
                string TxnID = Clsm.Cyrus_GetTransactionID_New();
                _list.Add(new ParmList() { name = "@txn", value = TxnID });
                DataTable dt = new DataTable();
                dt = Cls.select_data_dtNew("Proc_Business", _list);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Successfull!');location.replace('Businesswithus_Report.aspx');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('session null!');", true);
            }
        }
        // }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
        }
    }
   

}