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
public partial class Root_Distributor_Pancard : System.Web.UI.Page
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
            if (Session["dtDistributor"] != null)
            {
                DataTable dtMemberMaster = new DataTable();
                dtMemberMaster = (DataTable)Session["dtDistributor"];
                string Memberid = dtMemberMaster.Rows[0]["MemberId"].ToString();
                int Msrno = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                ViewState["Msrno"] = Msrno;
                ViewState["Memberid"] = Memberid;
                string mac = GetMACAddress();
                string ipaddress = GetIPAddress();
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
            if (ViewState["photo"] != null && ViewState["aadhar"] != null)
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
                    Amount = Convert.ToDecimal(Cls.select_data_scalar_double("select  isnull(Amount,0.00) as Amount from[nsdl_couponfeesettings] where PackageID=" + Convert.ToInt32(PackageID)));

                    if (memberID != "" && Amount > 0)
                    {
                        int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                        if (result > 0)
                        {
                           // int Msrno = Convert.ToInt32(ViewState["Msrno"]);
                            string agentid = clsm.Cyrus_GetTransactionID_New();
                            string name = txt_name.Text;
                            string fathername = txt_father.Text;
                            string email = txt_email.Text;
                            string date = txt_date.Text;
                            string mobile = txt_mobile.Text;
                            DataTable dtchkm = new DataTable();
                            List<ParmList> _lstparm = new List<ParmList>();
                            _lstparm.Add(new ParmList() { name = "@aadhar", value = ViewState["aadhar"] });
                            _lstparm.Add(new ParmList() { name = "@photo", value = ViewState["photo"] });
                            _lstparm.Add(new ParmList() { name = "@statu", value = "Pending" });
                            _lstparm.Add(new ParmList() { name = "@email", value = email });
                            _lstparm.Add(new ParmList() { name = "@date", value = date });
                            _lstparm.Add(new ParmList() { name = "@txnid", value = agentid });
                            _lstparm.Add(new ParmList() { name = "@customername", value = txt_name.Text });
                            _lstparm.Add(new ParmList() { name = "@customermobile", value = txt_mobile.Text });
                            _lstparm.Add(new ParmList() { name = "@fathername", value = fathername });
                            _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(ViewState["Msrno"]) });
                            _lstparm.Add(new ParmList() { name = "@memberid", value = ViewState["Memberid"].ToString() });
                            Cls.select_data_dtNew("Set_pancards", _lstparm);
                            clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "Dr", "PanCard Request TxnID:-" + agentid);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Successfull!');location.replace('Pancard_Report.aspx');", true);
                            clear();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in Wallet');", true);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in Wallet!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Insufficient Balance in Wallet !');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Please Select PDF File Only!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('contact your admin!');", true);
            clear();
        }
    }
    protected void btn_aadhar_Click(object sender, EventArgs e)
    {
        string filepath = string.Empty;
        if (fu_Identity.HasFile == true)
        {
            if (fu_Identity.PostedFile.FileName != "")
            {
                string filename = System.IO.Path.GetFileName(fu_Identity.FileName);
                filepath = Server.MapPath("../../Uploads/Servicesimage/pan/");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(fu_Identity.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg")
                {
                    if (fu_Identity.PostedFile.ContentType == "image/jpeg")
                    {
                        if (fu_Identity.PostedFile.ContentLength <= 1024000)
                        {
                            string FileName = DateTime.Now.Ticks + fu_Identity.FileName.ToString();
                            fu_Identity.PostedFile.SaveAs(filepath + FileName);
                            ViewState["aadhar"] = FileName;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File Upload a successfully!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Image has to be less than or equal 1 Mb!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only JPEG files are accepted!');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload valid file type');", true);

                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload a file to upload');", true);
        }
    }
    protected void btn_photo_Click(object sender, EventArgs e)
    {
        string filepath = string.Empty;
        if (fu_Address.HasFile == true)
        {
            if (fu_Address.PostedFile.FileName != "")
            {
                string filename = System.IO.Path.GetFileName(fu_Address.FileName);
                filepath = Server.MapPath("../../Uploads/Servicesimage/pan/");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                //Check file extension (must be JPG)
                string Extension = System.IO.Path.GetExtension(fu_Address.FileName).ToLower();
                if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".Pdf")
                {
                    if (fu_Address.PostedFile.ContentType == "image/jpeg")
                    {
                        if (fu_Address.PostedFile.ContentLength <= 1024000)
                        {
                            string FileName = DateTime.Now.Ticks + fu_Address.FileName.ToString();
                            fu_Address.PostedFile.SaveAs(filepath + FileName);
                            ViewState["photo"] = FileName;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('File Upload a successfully!');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Image has to be less than or equal 1 Mb!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Only JPEG files are accepted!');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload valid file type');", true);

                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please upload a file to upload');", true);
        }
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