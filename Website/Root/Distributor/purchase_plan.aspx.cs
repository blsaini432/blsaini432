using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;

public partial class root_Distributor_purchase_plan : System.Web.UI.Page
{
    #region [Properties]
    clsRecharge_History objHistory = new clsRecharge_History();
    DataTable dtHistory = new DataTable();
    DataTable dtExport = new DataTable();

    clsRecharge_Operator objOperator = new clsRecharge_Operator();
    DataTable dtOperator = new DataTable();

    clsRecharge_ServiceType objServiceType = new clsRecharge_ServiceType();
    DataTable dtServiceType = new DataTable();

    clsRecharge_API objAPI = new clsRecharge_API();
    DataTable dtAPI = new DataTable();
    clsMLM_Package objPackage = new clsMLM_Package();
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    string condition = " SerialNo > 0";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
            int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
            Session["MsrNo"] = MsrNo;
            bindgrid();
            Session["MemberId"] = dtMemberMaster.Rows[0]["MemberId"];
            if (dtMemberMaster.Rows[0]["MemberTypeID"].ToString() == "2")
            {

                cls.fill_MemberType(ddlmymembertype, "State Head");
            }
            if (dtMemberMaster.Rows[0]["MemberTypeID"].ToString() == "3")
            {
                cls.fill_MemberType(ddlmymembertype, "Master Distributor");
               
            }
            if (dtMemberMaster.Rows[0]["MemberTypeID"].ToString() == "4")
            {
                cls.fill_MemberType(ddlmymembertype, "Distributor");

            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
        int parentmsrno = Convert.ToInt32(dtMemberMaster.Rows[0]["ParentMsrNo"]);
      //  int membertyprid = Convert.ToInt32(dtMemberMaster.Rows[0]["MemberTypeId"]);
        string actions = "";
        if (Convert.ToInt32(DSO1_totalamount.Text) > 0 && Convert.ToInt32(DSO1_totadmin.Text) > 0 && Convert.ToInt32(DSO1_totalamount.Text) > 0)
        {
            int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(DSO1_totalamount.Text), Convert.ToInt32(Session["MsrNo"]));
            if (result > 0)
            {
                
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select Remaningcount from tblmlm_memberplans_adminapprove where membertype='" + ddlmymembertype.SelectedValue + "' and msrno='" + MsrNo + "' and isactive=1");
                if (dt.Rows.Count > 0)
                {
                    int count = Convert.ToInt32(dt.Rows[0]["Remaningcount"].ToString());
                    int sum = count + Convert.ToInt32(DSO1_totid.Text);
                    string TxnID = clsm.Cyrus_GetTransactionID_New();
                    clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberId"]), Convert.ToDecimal("-" + DSO1_totalamount.Text), "Dr", "Member Plan Purchase TxnID:-" + TxnID);
                    cls.insert_data("insert into [tblmlm_memberplans_adminreq](membertype,idpurchase,amount,TranId,requestbymsrno,adminmsrno,RequestDate,ActiveStatus,IsActive)values('" + ddlmymembertype.SelectedValue + "','" + DSO1_totid.Text + "','" + Convert.ToDecimal(DSO1_totalamount.Text) + "','" + TxnID + "','" + Convert.ToInt32(Session["MsrNo"]) + "',1 , '" + DateTime.Now + "','Approved','1')");
                    cls.update_data("update tblmlm_memberplans_adminapprove set Remaningcount='" + sum + "',Lastmodifieddate='" + DateTime.Now + "'  where MsrNo='" + MsrNo + "'  and membertype='" + ddlmymembertype.SelectedValue + "'");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('ID Purchage Successfully.!');location.replace('purchase_plan.aspx');", true);
                }
                else
                {
                    string TxnID = clsm.Cyrus_GetTransactionID_New();
                    clsm.Wallet_MakeTransaction(Convert.ToString(Session["MemberId"]), Convert.ToDecimal("-" + DSO1_totalamount.Text), "Dr", "Member Plan Purchase TxnID:-" + TxnID);
                    cls.insert_data("insert into [tblmlm_memberplans_adminreq](membertype,idpurchase,amount,TranId,requestbymsrno,adminmsrno,RequestDate,ActiveStatus,IsActive)values('" + ddlmymembertype.SelectedValue + "','" + DSO1_totid.Text + "','" + Convert.ToDecimal(DSO1_totalamount.Text) + "','" + TxnID + "','" + Convert.ToInt32(Session["MsrNo"]) + "',1 , '" + DateTime.Now + "','Approved','1')");
                    cls.update_data("insert into tblmlm_memberplans_adminapprove(membertype,MsrNo,Remaningcount,isactive,Lastmodifieddate)values('" + ddlmymembertype.SelectedValue + "','" + MsrNo + "','" + DSO1_totid.Text + "',1,'" + DateTime.Now + "')");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('ID Purchage Successfully.!');location.replace('purchase_plan.aspx');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insucfficent Wallet Balance!');location.replace('purchase_plan.aspx');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('configuration error!');location.replace('purchase_plan.aspx');", true);

        }
    }

    protected void ddlMemberType_SelectedIndexChanged(object sender, EventArgs e)
    {
    
        DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
        DataTable dd = new DataTable();

        dd=cls.select_data_dt("select * from tblmlm_memberplan_settings_mm where membertype='" + Convert.ToInt32(dtMemberMaster.Rows[0]["MemberTypeId"]) + "' and targetmembertype='" + ddlmymembertype.SelectedValue + "' and msrno='" + Convert.ToInt32(Session["MsrNo"]) + "'");
        if (dd.Rows.Count > 0)
        {
            //Session["memberplanamt"] = 1;
            DSO1_totadmin.Text = (Convert.ToDouble(dd.Rows[0]["Netamount"]).ToString()).ToString();
        }
        else
        {
            DataTable ddd = new DataTable();
            ddd = cls.select_data_dt("select * from tblmlm_memberplan_settings where membertype='" + Convert.ToInt32(dtMemberMaster.Rows[0]["MemberTypeId"]) + "' and targetmembertype='" + ddlmymembertype.SelectedValue + "'");
            if (ddd.Rows.Count > 0)
            {
                //Session["adminplanamt"] = 1;
                DSO1_totadmin.Text = (Convert.ToDouble(ddd.Rows[0]["Netamount"]).ToString()).ToString();
            }
            else
            {
                DSO1_totadmin.Text = "0.00";
            }

        }
}
    protected void DSO1_totid_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(DSO1_totid.Text) > 0 && Convert.ToInt32(DSO1_totadmin.Text) > 0)
        {
            decimal adminfee = Convert.ToDecimal(DSO1_totadmin.Text);
            decimal noid = Convert.ToDecimal(DSO1_totid.Text);
            decimal total = noid * adminfee;
            DSO1_totalamount.Text = total.ToString();
        }
    }


    public void bindgrid()
    {
        DataTable dtMemberMaster = (DataTable)Session["dtDistributor"];
        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select tblmlm_membership.membertype as membertype,tblmlm_memberplans_adminreq.membertype as mm,requestbymsrno,ReqId,adminmsrno,RequestDate,ActiveDate,TranId,ActiveStatus,amount,idpurchase,MemberId,FirstName +LastName as MemberName from tblmlm_memberplans_adminreq inner join  tblmlm_membermaster on tblmlm_membermaster.MsrNo=tblmlm_memberplans_adminreq.RequestbyMsrno inner join  tblmlm_membership on tblmlm_membership.membertypeid=tblmlm_memberplans_adminreq.membertype where requestbymsrno='" + MsrNo + "'");
        //gvState.DataSource = dt;
        //gvState.DataBind();
    }
}