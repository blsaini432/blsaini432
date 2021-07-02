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
using BLL;
public partial class Root_Retailer_Emitra_form : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    //EzulixBBPSAPI eBbps = new EzulixBBPSAPI();
    cls_connection oBJCONNECTION = new cls_connection();
    clsState objState = new clsState();
    DataTable dt = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtRetailer"] != null)
            {
                DataTable dtMemberMaster = new DataTable();
                dtMemberMaster = (DataTable)Session["dtRetailer"];
                string Memberid = dtMemberMaster.Rows[0]["MemberId"].ToString();
                int Msrno = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                ViewState["Msrno"] = Msrno;
                ViewState["Memberid"] = Memberid;
                string mac = GetMACAddress();
                string ipaddress = GetIPAddress();
                string PackageID = Convert.ToString(dtMemberMaster.Rows[0]["PackageID"]);
                decimal Amount = 0;
                Amount = Convert.ToDecimal(Cls.select_data_scalar_double("select  isnull(Amount,0.00) as Amount from[tblmlm_manageservice] where PackageID=" + Convert.ToInt32(PackageID) + " and servicename ='emitra'"));
                lblamount.Text = Amount.ToString();
            }
            else
            {
                Response.Redirect("~/userlogin.aspx");
            }
        }
    }
    protected void btn_submit(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Msrno"] != null)
            {
                int Msrno = Convert.ToInt32(ViewState["Msrno"]);
                DataTable dts = new DataTable();
                dts = Cls.select_data_dt("select MemberID,MemberTypeID,PackageID from tblmlm_membermaster where Msrno=" + Msrno + "");
                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                string MemberTypeID = Convert.ToString(dts.Rows[0]["MemberTypeID"]);
                string PackageID = Convert.ToString(dts.Rows[0]["PackageID"]);
                decimal Amount = 0;
                Amount = Convert.ToDecimal(Cls.select_data_scalar_double("select  isnull(Amount,0.00) as Amount from[tblmlm_manageservice] where PackageID=" + Convert.ToInt32(PackageID) + " and servicename ='emitra'"));
                if (memberID != "" && Amount > 0)
                {
                    int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                    if (result > 0)
                    {
                        // int Msrno = Convert.ToInt32(ViewState["Msrno"]);
                        string agentid = clsm.Cyrus_GetTransactionID_New();
                        string photo = uploadPanImage(file_photo);
                        string aadhar = uploadPanImage(file_aadhar);
                        string pan = uploadPanImage(file_pan);
                        string marksheet = uploadPanImage(file_marsheet);
                        string Police = uploadPanImage(file_Police);
                        string bank = uploadPanImage(file_bank);
                        string janaadhar = uploadPanImage(file_jan);
                        DataTable dtchkm = new DataTable();
                        List<ParmList> _lstparm = new List<ParmList>();
                        _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
                        _lstparm.Add(new ParmList() { name = "@aadhar", value = aadhar });
                        _lstparm.Add(new ParmList() { name = "@photo", value = photo });
                        _lstparm.Add(new ParmList() { name = "@pancard", value = pan });
                        _lstparm.Add(new ParmList() { name = "@10marksheet", value = marksheet });
                        _lstparm.Add(new ParmList() { name = "@police_verification", value = Police });
                        _lstparm.Add(new ParmList() { name = "@Bank_details", value = bank });
                        _lstparm.Add(new ParmList() { name = "@jan_aadhar", value = janaadhar });
                        _lstparm.Add(new ParmList() { name = "@amount", value = Amount });
                        _lstparm.Add(new ParmList() { name = "@statu", value = "Pending" });
                        _lstparm.Add(new ParmList() { name = "@email", value = txt_email.Text });
                        _lstparm.Add(new ParmList() { name = "@ssoid", value = txt_ssoid.Text });
                        _lstparm.Add(new ParmList() { name = "@txnid", value = agentid });
                        _lstparm.Add(new ParmList() { name = "@mobile", value = txt_mobile.Text });
                        _lstparm.Add(new ParmList() { name = "@shopname", value = txt_shopname.Text });
                        _lstparm.Add(new ParmList() { name = "@shopaddress", value = txt_address.Text });
                        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(ViewState["Msrno"]) });
                        _lstparm.Add(new ParmList() { name = "@memberid", value = ViewState["Memberid"].ToString() });
                        Cls.select_data_dtNew("Set_emitra", _lstparm);
                        clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "Emitra Request TxnID:-" + agentid);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Send Admin Team!');location.replace('Emiter_report.aspx');", true);
                        clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Insufficient Balance in Wallet !');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Insufficient Balance in Wallet !');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Insufficient Balance in Wallet !');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('contact your admin!');", true);
            clear();
        }
    }


    private string uploadPanImage(FileUpload _fup)
    {
        clsImageResize objImageResize = new clsImageResize();
        if (_fup.HasFile == true)
        {
            if (_fup.PostedFile.FileName != "")
            {
                string opath = Server.MapPath("~/Uploads/Servicesimage/Actual/");
                string mpath = Server.MapPath("~/Uploads/Servicesimage/Medium/");
                string spath = Server.MapPath("~/Uploads/Servicesimage/Small/");

                if (!Directory.Exists(opath))
                {
                    Directory.CreateDirectory(opath);
                }
                if (!Directory.Exists(mpath))
                {
                    Directory.CreateDirectory(mpath);
                }
                if (!Directory.Exists(spath))
                {
                    Directory.CreateDirectory(spath);
                }

                //Check file extension (must be PDF)
                string Extension = System.IO.Path.GetExtension(_fup.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif" || Extension == ".pdf")
                {
                    string FileName = DateTime.Now.Ticks + _fup.FileName.ToString();
                    _fup.PostedFile.SaveAs(opath + FileName);
                    if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png" || Extension == ".gif")
                    {
                        objImageResize.FixedSize(FileName, opath + FileName, mpath + FileName, 300, 200);
                        objImageResize.FixedSize(FileName, opath + FileName, spath + FileName, 50, 50);
                    }

                    return FileName;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Select JPG/JPEG/PNG/GIF/PDF File Only!');", true);

                }
            }
        }
        else
        {
            return "";
        }

        return "";
    }
    #region methods
    public void clear()
    {

        ViewState["aadhar"] = null;
        ViewState["photo"] = null;
        //  tr_service.Visible = false;
        // btn_Getbill.Visible = false;
        // GridView1.Visible = false;
    }
    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }
        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
    public string GetMACAddress()
    {
        string macAddresses = "";

        foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }
    #endregion
}