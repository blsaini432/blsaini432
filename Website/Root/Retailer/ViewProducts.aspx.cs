using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Root_Retailer_ViewProducts : System.Web.UI.Page
{
    #region Properties
    cls_connection Cls = new cls_connection();
    DataTable dtEWalletTransaction = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    public static string mssrno { get; set; }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DataTable dtmembermaster = new DataTable();
            dtmembermaster = (DataTable)Session["dtRetailer"];
            mssrno = dtmembermaster.Rows[0]["MsrNo"].ToString();
            Session["msrno"] = mssrno;
            filldata();
        }
    }
    #region Method
    private void filldata()
    {
        DataTable dt = new DataTable();
        dt = Cls.select_data_dt("select * from [Shooping_Cart_Admin]");
        gv_Transaction.DataSource = dt;
        gv_Transaction.DataBind();
    }
    #endregion
    protected void txtnoofproduct_TextChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).NamingContainer;
        Label lblminiumorder = (Label)currentRow.FindControl("lblminiumorder");
        TextBox txtnoofproduct = (TextBox)currentRow.FindControl("txtnoofproduct");
        if (txtnoofproduct.Text != "" && Convert.ToDecimal(txtnoofproduct.Text) > 0)
        {
            if (Convert.ToDecimal(txtnoofproduct.Text) >= Convert.ToDecimal(lblminiumorder.Text))
            {
                TextBox txttotalamount = (TextBox)currentRow.FindControl("txttotalamount");
                Label lblpriceperunit = (Label)currentRow.FindControl("lblpriceperunit");
                decimal noofproduct = Convert.ToDecimal(txtnoofproduct.Text);
                decimal total = noofproduct * Convert.ToDecimal(lblpriceperunit.Text);
                txttotalamount.Text = total.ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please add minimum order value!');", true);
            }
        }
        else
        {
            TextBox txttotalamount = (TextBox)currentRow.FindControl("txttotalamount");
            txttotalamount.Text = "";
            txttotalamount.Text = "";
        }
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = Cls.select_data_dt("select * from tblmlm_membermaster where msrno='" + Convert.ToInt32(Session["msrno"]) + "'");
        if (dt.Rows.Count > 0)
        {
            string memberID = dt.Rows[0]["MemberId"].ToString();

            GridViewRow currentRow = (GridViewRow)((Button)sender).NamingContainer;
            Label lblminiumorder = (Label)currentRow.FindControl("lblminiumorder");
            TextBox txtnoofproduct = (TextBox)currentRow.FindControl("txtnoofproduct");
            TextBox txttotalamount = (TextBox)currentRow.FindControl("txttotalamount");
            Label lblpriceperunit = (Label)currentRow.FindControl("lblpriceperunit");
            Label lblproductname = (Label)currentRow.FindControl("lblProductName");
            HiddenField hdfid = (HiddenField)currentRow.FindControl("lblitemid");
            if (txtnoofproduct.Text != "" && txttotalamount.Text != "")
            {
                if (Convert.ToDecimal(txtnoofproduct.Text) > 0 && Convert.ToDecimal(txttotalamount.Text) > 0)
                {
                    cls_myMember clsm = new cls_myMember();
                    int result = clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(txttotalamount.Text), Convert.ToInt32(Session["msrno"].ToString()));
                    if (result > 0)
                    {
                        string TxnID = clsm.Cyrus_GetTransactionID_New();
                        decimal remaningitem = 0;
                        DataTable dtm = new DataTable();
                        dtm = Cls.select_data_dt("select * from [Shooping_Cart_Admin] where itemid='" + hdfid.Value + "'");
                        if (dtm.Rows.Count > 0)
                        {
                            Cls.insert_data("insert into [Shooping_Cart_MemberDetails](ProductName,Priceperunit,Quantity,itemid,AddDate,RequestBy,RequestStatus,TxnId,Totalamount)values('" + lblproductname.Text + "','" + lblpriceperunit.Text + "','" + txtnoofproduct.Text + "','" + hdfid.Value + "','" + DateTime.Now + "','" + memberID + "','Pending','" + TxnID + "','" + txttotalamount.Text + "')");
                            clsm.Wallet_MakeTransaction(memberID, Convert.ToDecimal("-" + Convert.ToDecimal(txttotalamount.Text)), "Dr", "Purchase" + txtnoofproduct.Text + " " + lblproductname.Text + " Request TxnID:-" + TxnID);
                            decimal quantity = Convert.ToDecimal(dtm.Rows[0]["Quantity"].ToString());
                            remaningitem = quantity - Convert.ToDecimal(txtnoofproduct.Text);
                            Cls.update_data("update [Shooping_Cart_Admin] set Quantity='" + remaningitem + "' where itemid='" + hdfid.Value+ "'");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Your Product Purchased Successfully.!');location.replace('ViewProducts.aspx');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Wallet Balance to buy this product please refill and continue');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter the no of product to buy');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter the no of product to buy');", true);
            }
        }
    }
}