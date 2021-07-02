using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Web.Services;
using BLL;
using System.Collections.Generic;
public partial class Root_Retailer_UPI_Payment_return : System.Web.UI.Page
{

    cls_myMember Clsm = new cls_myMember();
    DataTable dtMemberMaster = new DataTable();
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    protected void Page_Load(object sender, EventArgs e)
    {
      
        string key = "4dc44306-9ab4-48f2-8176-676900de52ee";
        DataTable dtMember = (DataTable)Session["dtRetailer"];     
        ViewState["mobile"] = dtMember.Rows[0]["Mobile"].ToString();
        ViewState["email"] = dtMember.Rows[0]["email"].ToString();
        ViewState["name"] = dtMember.Rows[0]["Firstname"].ToString();
        ViewState["key"] = key;
        ViewState["vpa"] = Session["UPIID"].ToString();
        ViewState["Amount"] = Session["Amount"].ToString();
        string Txnid = string.Empty;
        Txnid = Clsm.Cyrus_GetTransactionID_New();
        ViewState["txnid"] = Txnid;
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        string path = HttpContext.Current.Request.Url.AbsolutePath;
        string str = url.Replace(path, "");
        ViewState["url"] = "http://www.mysunshinenet.com/UPI_Paymentcallback.aspx";

    }
    [WebMethod]
    public static List<Customer> getdata(string client_vpa, string amount, string client_name, string client_email, string client_mobile, string client_txn_id, string p_info)
    {
        List<Customer> custList = new List<Customer>();
        cls_connection Cls = new cls_connection();
        DataTable dt = (DataTable)HttpContext.Current.Session["dtRetailer"];
        int MsrNo = Convert.ToInt32(dt.Rows[0]["MsrNo"]);
        int MemberTypeID = Convert.ToInt32(dt.Rows[0]["MemberTypeID"]);
        string  MemberID = dt.Rows[0]["MemberID"].ToString();
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@MemberId", value = MemberID });
        _lstparm.Add(new ParmList() { name = "@MemberTypeID", value = MemberTypeID });
        _lstparm.Add(new ParmList() { name = "@MsrNo", value = MsrNo });
        _lstparm.Add(new ParmList() { name = "@amount", value = Convert.ToDecimal(amount) });
        _lstparm.Add(new ParmList() { name = "@client_vpa", value = client_vpa });
        _lstparm.Add(new ParmList() { name = "@Statuss", value = "Pending" });
        _lstparm.Add(new ParmList() { name = "@mobile", value = client_mobile });
        _lstparm.Add(new ParmList() { name = "@email", value = client_email });
        _lstparm.Add(new ParmList() { name = "@client_name", value = client_name });
        _lstparm.Add(new ParmList() { name = "@client_txn_id", value = client_txn_id });
        // _lstparm.Add(new ParmList() { name = "@NetAmount", value = Convert.ToDecimal(NetAmount) });
        _lstparm.Add(new ParmList() { name = "@Action", value = "I" });
        Cls.select_data_dtNew("SET_Ezulix_UPI_Payment", _lstparm);
        string[] hashVarsSeq;
        string hash_string = string.Empty;
        hashVarsSeq = ConfigurationManager.AppSettings["hash"].Split('|'); // spliting hash sequence from config
        hash_string = "";
        foreach (string hash_var in hashVarsSeq)
        {
            if (hash_var == "key")
            {
                hash_string = hash_string + ConfigurationManager.AppSettings["KEY"];
                hash_string = hash_string + '|';
            }
            else if (hash_var == "client_vpa")
            {
                hash_string = hash_string + client_vpa;
                hash_string = hash_string + '|';
            }
            else if (hash_var == "client_txn_id")
            {
                hash_string = hash_string + client_txn_id;
                hash_string = hash_string + '|';
            }
            else if (hash_var == "amount")
            {
                hash_string = hash_string + amount;
                hash_string = hash_string + '|';
            }
            else if (hash_var == "p_info")
            {
                hash_string = hash_string + p_info;
                hash_string = hash_string + '|';
            }
            else if (hash_var == "client_name")
            {
                hash_string = hash_string + client_name;
                hash_string = hash_string + '|';
            }
            else if (hash_var == "client_email")
            {
                hash_string = hash_string + client_email;
                hash_string = hash_string + '|';
            }
            else if (hash_var == "client_mobile")
            {
                hash_string = hash_string + client_mobile;
                hash_string = hash_string + '|';
            }

            else if (hash_var == "udf1")
            {
                hash_string = hash_string + '|';
            }
            else if (hash_var == "udf2")
            {
                hash_string = hash_string + '|';
            }
            else if (hash_var == "udf3")
            {
                hash_string = hash_string + '|';
            }

        }

        hash_string += ConfigurationManager.AppSettings["SALT"];// appending SALT
        byte[] message = Encoding.UTF8.GetBytes(hash_string);
        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        string hash = hex;
        Customer cust = new Customer();
        cust.hash = hash;
        custList.Add(cust);
        return custList;
    }

    #region class
    public class Customer
    {
        public string hash { get; set; }
        public string url { get; set; }

    }



    #endregion
}