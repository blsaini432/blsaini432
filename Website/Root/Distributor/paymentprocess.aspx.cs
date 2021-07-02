using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Root_Distributor_paymentprocess : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    
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
                    Session["MemberId"] = dtMember.Rows[0]["MemberID"];
                    ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                    // ViewState["dmtmobile"] = dt.Rows[0]["Mobile"].ToString();


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
       
        decimal TXN_AMOUNT = Convert.ToDecimal(txt_Amount.Text);
        Session["Amount"] = TXN_AMOUNT+ ".00";
        if(TXN_AMOUNT <= 5000 && TXN_AMOUNT >= 100)
        {
            DataTable dtEWalletTransaction = new DataTable();
            List<ParmList> _lstparm = new List<ParmList>();
            List<Customer> custList = new List<Customer>();
            _lstparm.Add(new ParmList() { name = "@msrno", value = ViewState["MsrNo"] });
            _lstparm.Add(new ParmList() { name = "@datefrom", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
            _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(System.DateTime.Now.ToString("dd-MM-yyyy")) });
            dtEWalletTransaction = Cls.select_data_dtNew("chackamount", _lstparm);
            foreach (DataRow dtrow in dtEWalletTransaction.Rows)
            {
                Customer cust = new Customer();
                cust.amount = dtrow["amount"].ToString();

                if(cust.amount == "")
                {
                    ViewState["amounts"] =0;
                    
                }
                else
                {
                    ViewState["amounts"] = cust.amount;
                }
               
                custList.Add(cust);
            }
            int amount = Convert.ToInt32(ViewState["amounts"]);
            if (amount <= 25000)
            {
                string tx = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
                Random random = new Random();
                int id = random.Next(100000000, 999999999);
                Session["tx"] = tx;
                Session["txnid"] = id;
                Session["txtAmount"] = TXN_AMOUNT;
                Session["Returnurl"] = "addwallet";
                Session["checkout"] = "yes";
                Response.Redirect("payment.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Your Limit Over :DAY LIMIT - Maximum  25000/- PER DAY  ')", true);
            }
          
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('maximum transction Amount 5000 and minimum 100')", true);
        }
       
    }
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }

    public class Customer
    {
        public string MsrNo { get; set; }
        public string amount { get; set; } 
        
    }
}