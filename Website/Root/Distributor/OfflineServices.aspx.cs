using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;

public partial class Root_Distributor_OfflineServices : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    public static string adminurl = ConfigurationManager.AppSettings["adminurl"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtDistributor"] != null)
            {
                FillServices();
            }

        }
    }
    public void FillServices()
    {
        cls_connection objconnection = new cls_connection();
        DataTable dd = new DataTable();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@Action", value = "AI" });
        cls_connection cls = new cls_connection();
        dd = cls.select_data_dtNew("Sp_ServiceFeeSettings", _lstparm);
        if (dd.Rows.Count > 0)
        {
            ddlservice.DataSource = dd;
            ddlservice.DataValueField = "Id";
            ddlservice.DataTextField = "Name";
            ddlservice.DataBind();
            ddlservice.Items.Insert(0, new ListItem("Select Service", "0"));

        }
        else
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string files = string.Empty;
        if (Session["UploadFiles"] != null)
        {
            files = Session["UploadFiles"].ToString();
        }
        try
        {
            DataTable dtMember = new DataTable();
            dtMember = (DataTable)Session["dtDistributor"];
            ViewState["MemberMsrNo"] = dtMember.Rows[0]["MsrNo"].ToString();
            if (ViewState["MemberMsrNo"] != null)
            {
                int Msrno = Convert.ToInt32(ViewState["MemberMsrNo"]);
                DataTable dts = new DataTable();
                dts = cls.select_data_dt("select MemberID,PackageId from tblmlm_membermaster where Msrno=" + Msrno + "");
                string memberID = Convert.ToString(dts.Rows[0]["MemberID"]);
                string PackageId = Convert.ToString(dts.Rows[0]["PackageId"]);
                decimal Amount = 0;
                DataTable dtminbal = new DataTable();
                string TxnID = clsm.Cyrus_GetTransactionID_New();
                Amount = Convert.ToDecimal(AdminFees(Convert.ToInt32(PackageId)));
                if (memberID != "" && Amount > 0)
                {
                    string Narration = "Service Request TxnID:-" + TxnID;
                    int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(Amount), Msrno);
                    if (result > 0)
                    {
                        int memberdeduct = clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Amount), "dr", Narration);
                        if (memberdeduct > 0)
                        {
                            List<ParmList> _list = new List<ParmList>();
                            _list.Add(new ParmList() { name = "@Name", value = txt_Name.Text });
                            _list.Add(new ParmList() { name = "@ServiceId", value = Convert.ToInt32(ddlservice.SelectedValue) });
                            _list.Add(new ParmList() { name = "@Phone", value = txt_Mobile.Text });
                            _list.Add(new ParmList() { name = "@Email", value = txtEmail.Text });
                            _list.Add(new ParmList() { name = "@Ezulixtranid", value = TxnID });

                            _list.Add(new ParmList() { name = "@Address", value = txt_address.Text });
                            _list.Add(new ParmList() { name = "@CompanyName", value = txt_companyname.Text });
                            _list.Add(new ParmList() { name = "@CompanyAddress", value = txt_comnpanyaddress.Text });
                            _list.Add(new ParmList() { name = "@Docs", value = files });
                            _list.Add(new ParmList() { name = "@Amount ", value = Convert.ToDecimal(Amount) });
                          //  _list.Add(new ParmList() { name = "@AdminAmount", value = Convert.ToDecimal(AdminAmount) });
                            _list.Add(new ParmList() { name = "@Memberid", value = memberID });
                           // _list.Add(new ParmList() { name = "@AdminMemberId", value = Adminmemberid });
                            _list.Add(new ParmList() { name = "@Action", value = "I" });
                            cls.select_data_dtNew("Sp_OfflineServices", _list);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Request Saved Success');location.replace('OfflineServices.aspx');", true);
                            Session["UploadFiles"] = null;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Problem In Request processing.Please try Again Or Contact To Admin.!');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in Wallet3 !');", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Fees Amount Not Set !');", true);
                }
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(" + ex.ToString() + ");", true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
        {
            int iUploadedCnt = 0;
            string filename = string.Empty;
            int iFailedCnt = 0;
            HttpFileCollection hfc = Request.Files;
            if (hfc.Count <= 15)    // 15 FILES RESTRICTION.
            {
                for (int i = 0; i <= hfc.Count - 1; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        if (!File.Exists(Server.MapPath("~/Uploads/") +
                            Path.GetFileName(hpf.FileName)))
                        {
                            DirectoryInfo objDir =
                                new DirectoryInfo(Server.MapPath("~/Uploads/"));
                            string sFileName = Path.GetFileName(hpf.FileName);
                            string sFileExt = Path.GetExtension(hpf.FileName);
                            // CHECK FOR DUPLICATE FILES.
                            FileInfo[] objFI =
                                objDir.GetFiles(sFileName.Replace(sFileExt, "") + ".*");
                            if (objFI.Length > 0)
                            {
                                iFailedCnt += 1;        // NOT ALLOWING DUPLICATE.
                                break;
                            }
                            else
                            {
                                // SAVE THE FILE IN A FOLDER.
                                string filenamse = DateTime.Now.Ticks + Path.GetFileName(hpf.FileName);
                                hpf.SaveAs(Server.MapPath("~/Uploads/") +
                                    filenamse);
                                iUploadedCnt += 1;
                                string fname = adminurl + "/Uploads/" + filenamse;
                                filename = filename + fname + ",";
                            }
                        }
                    }
                }
                Session["UploadFiles"] = filename;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Files Uploaded Successfully!');", true);
            }
            else
            {

            }
        }
        else
        {

        }
    }
    protected void ddlservice_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        List<ParmList> _lstparm = new List<ParmList>();
        DataTable dtMember = new DataTable();
        dtMember = (DataTable)Session["dtDistributor"];
        _lstparm.Add(new ParmList() { name = "@serviceid", value = Convert.ToInt32(ddlservice.SelectedValue) });
        _lstparm.Add(new ParmList() { name = "@packageid", value = Convert.ToInt32(dtMember.Rows[0]["PackageId"]) });
        _lstparm.Add(new ParmList() { name = "@Action", value = "L" });
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dtNew("Sp_ServiceFeeSettings", _lstparm);
        if (dt.Rows.Count > 0)
        {
            decimal commision = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
            lblamt.Text = commision.ToString();
            DataTable dtt = new DataTable();
            dtt = cls.select_data_dt("select * from tbl_Service where Id=" + Convert.ToInt32(ddlservice.SelectedValue) + "");
            if (dtt.Rows.Count > 0)
            {
                lblinstructions.Text = dtt.Rows[0]["Description"].ToString();
            }
        }
        else
        {
            lblamt.Text = "0.00";
        }
    }
    public string AdminFees(int Packageid)
    {
        DataTable dt = new DataTable();
        List<ParmList> _lstparm = new List<ParmList>();
        DataTable dtMember = new DataTable();
        dtMember = (DataTable)Session["dtDistributor"];
        _lstparm.Add(new ParmList() { name = "@serviceid", value = Convert.ToInt32(ddlservice.SelectedValue) });
        _lstparm.Add(new ParmList() { name = "@packageid", value = Convert.ToInt32(Packageid) });
        _lstparm.Add(new ParmList() { name = "@Action", value = "L" });
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dtNew("Sp_ServiceFeeSettings", _lstparm);
        if (dt.Rows.Count > 0)
        {
            decimal commision = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
            return commision.ToString();
        }
        else
        {
            return "0.00";
        }
    }
}