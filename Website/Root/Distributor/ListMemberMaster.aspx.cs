using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Configuration;
using Common;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Services;


public partial class Root_Distributor_ListMemberMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static List<Customer> BindCustomers()
    {
        DataTable dt = new DataTable();
        DataTable dtMember = (DataTable)HttpContext.Current.Session["dtDistributor"];
        int msrno = Convert.ToInt32(dtMember.Rows[0]["MsrNo"]);
        List<Customer> custList = new List<Customer>();
        int MemberTypeID = Convert.ToInt32(dtMember.Rows[0]["MemberTypeID"]);
        if (MemberTypeID == 2)
        {

            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            dt = objMemberMaster.ManageMemberMaster("GetByView", msrno);
            foreach (DataRow dtrow in dt.Rows)
            {

                Customer cust = new Customer();
                cust.MemberID = dtrow["MemberID"].ToString();
                cust.MemberName = dtrow["MemberName"].ToString();
                cust.Email = dtrow["Email"].ToString();
                cust.Mobile = dtrow["Mobile"].ToString();
                cust.Ownerid = dtrow["Ownerid"].ToString();
                cust.MemberType = dtrow["MemberType"].ToString();
                cust.Owner = dtrow["Owner ID"].ToString();
                cust.Package = dtrow["PackageName"].ToString();
                cust.IsActive = dtrow["IsActive"].ToString();
                cust.Msrno = dtrow["Msrno"].ToString();
                cust.parid = "2";
                cust.joiningdate = dtrow["Joining Date"].ToString();
                custList.Add(cust);
            }
        }
        else
        {

            cls_connection cls = new cls_connection();
            clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
            dt = objMemberMaster.ManageMemberMaster("GetByView", msrno);
            foreach (DataRow dtrow in dt.Rows)
            {

                Customer cust = new Customer();
                cust.MemberID = dtrow["MemberID"].ToString();
                cust.MemberName = dtrow["MemberName"].ToString();
                cust.Email = dtrow["Email"].ToString();
                cust.Mobile = dtrow["Mobile"].ToString();
               // cust.Ownerid = dtrow["Ownerid"].ToString();
                cust.MemberType = dtrow["MemberType"].ToString();
                cust.Owner = dtrow["Owner ID"].ToString();
                cust.Package = dtrow["PackageName"].ToString();
                cust.IsActive = dtrow["IsActive"].ToString();
                cust.Msrno = dtrow["Msrno"].ToString();
                cust.joiningdate = dtrow["Joining Date"].ToString();
                custList.Add(cust);
            }

        }
        return custList;
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string updateservice(string msrno, string action)
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        string actions = "";
        if (action == "True")
        {
            cls.select_data_dt(@"Update tblmlm_membermaster set isactive='0'  Where  msrno='" + msrno + "'");
        }
        else
        {
            cls.select_data_dt(@"Update tblmlm_membermaster set isactive='1'  Where  msrno='" + msrno + "'");
        }

        actions = "success";


        return actions;

    }

    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string MemberType { get; set; }
        public string Owner { get; set; }
        public string Package { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string TransactionPassword { get; set; }
        public string isuti { get; set; }
        public string isdmr { get; set; }
        public string isxpressdmr { get; set; }
        public string isbbps { get; set; }
        public string isbus { get; set; }
        public string isflight { get; set; }
        public string ishotel { get; set; }
        public string isrecharge { get; set; }
        public string ispancard { get; set; }
        public string isaeps { get; set; }
        public string isprepaidcard { get; set; }
        public string bankname { get; set; }
        public string bankifsc { get; set; }
        public string bankac { get; set; }
        public string isaepspayout { get; set; }
        public string joiningdate { get; set; }
        public string IsActive { get; set; }
        public string Msrno { get; set; }
        public string Ownerid { get; set; }
        public string parid { get; set; }
    }
}