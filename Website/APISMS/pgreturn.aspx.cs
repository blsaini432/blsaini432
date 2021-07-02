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
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Collections.Specialized;

public partial class api_pgreturn : System.Web.UI.Page
{
    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtOperator = new DataTable();
    DataTable dtHistory = new DataTable();

    clsRecharge_PromoCode objPromoCode = new clsRecharge_PromoCode();
    DataTable dtPromoCode = new DataTable();

    clsRecharge_Circle objCircle = new clsRecharge_Circle();
    DataTable dtCircle = new DataTable();

    cls_Universal objUniversal = new cls_Universal();
    DataTable dtUniversal = new DataTable();

    clsMLM_Mix objMix = new clsMLM_Mix();
    DataTable dtMix = new DataTable();

    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    DataTable dtMemberMaster = new DataTable();

    clsMLM_MemberMasterLoginDetail objMemberMasterLoginDetail = new clsMLM_MemberMasterLoginDetail();
    DataTable dtMemberMasterLoginDetail = new DataTable();

    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();

    clsMLM_EWalletTransaction objEWalletTransaction = new clsMLM_EWalletTransaction();
    DataTable dtEWalletTransaction = new DataTable();

    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();

    clsBanner objBanner = new clsBanner();

    cls_connection objconnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["status"] != null)
            {
                if (Request.QueryString["status"].ToString() == "success")
                {
                    int cnt = objconnection.select_data_scalar_int("Select count(*) from tblRecharge_history where historyid='" + Convert.ToString(Request.QueryString["id"]).Replace("-","").Replace(";","") + "'");
                    if (cnt > 0)
                    {
                        int cntCount = objconnection.select_data_scalar_int("select Count(*) FROM tbl_paymentGateway_an where Hid='" + Convert.ToString(Request.QueryString["id"].Replace("-", "").Replace(";", "")) + "'");
                        if (cntCount == 0)
                        {
                            int a = objconnection.insert_data("insert into  tbl_paymentGateway_an(Hid,PstatusId) values('" + Convert.ToString(Request.QueryString["id"].Replace("-", "").Replace(";", "")) + "',1)");
                        }
                    }
                    else if (objconnection.select_data_scalar_int("Select count(*) from tbl_APP_WalletADD where widno='" + Convert.ToString(Request.QueryString["id"].Replace("-", "").Replace(";", "")) + "'") > 0)
                    {
                        DataTable dtd = new DataTable();
                        dtd = objconnection.select_data_dt("Select * from tbl_APP_WalletADD where widno='" + Convert.ToString(Request.QueryString["id"].Replace("-", "").Replace(";", "")) + "'");
                        int a = objconnection.insert_data("insert into  tbl_paymentGateway_an(Hid,PstatusId,rstatusid,createdate,isactive,isdelete,wadd,wamt) values('" + Convert.ToString(Request.QueryString["id"].Replace("-", "").Replace(";", "")) + "',1,0,getdate(),1,0,1,'" + dtd.Rows[0]["amount"].ToString() + "')");
                    }

                }
                else if (Request.QueryString["status"].ToString() == "fail" || Request.QueryString["status"].ToString() == "cancel")
                {
                    int cntCount = objconnection.select_data_scalar_int("select Count(*) FROM tbl_paymentGateway_an where Hid='" + Convert.ToString(Request.QueryString["id"].Replace("-", "").Replace(";", "")) + "'");
                    if (cntCount == 0)
                    {
                        //int msrno = objconnection.select_data_scalar_int("Select msrno from tblrecharge_history where historyid='" + Convert.ToString(Request.QueryString["id"].Replace("-", "").Replace(";", "")) + "'");
                        //objconnection.insert_data("Exec dbo.PromoCode_ReleaseUnused '" + msrno.ToString() + "'");
                        int a = objconnection.insert_data("insert into  tbl_paymentGateway_an(Hid,PstatusId) values('" + Convert.ToString(Request.QueryString["id"].Replace("-", "").Replace(";", "")) + "',0)");
                    }
                }
            }
        }
    }
}