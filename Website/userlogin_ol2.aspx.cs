using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Configuration;
using System.Net;

public partial class userlogin : System.Web.UI.Page
{
    #region Load
    public static string adminurl = ConfigurationManager.AppSettings["adminurl"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // divlogin.Visible = false;
            bindbanner();
            loaddata(2);
        }
    }
    #endregion
    #region Utility Functions
    public static string genratestring()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";
        string characters = numbers;
        characters += alphabets + small_alphabets + numbers;
        int length = int.Parse("25");
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }
        return otp;
    }


    public void loaddata(int ID)
    {
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@ID", value = ID });
        _lstparm.Add(new ParmList() { name = "@Action", value = "GetAll" });
        cls_connection cls = new cls_connection();
        DataTable dtCompany = cls.select_data_dtNew("Proc_ManageCompany ", _lstparm);
        if (dtCompany.Rows.Count > 0)
        {
        //   imglogo.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dtCompany.Rows[0]["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dtCompany.Rows[0]["companylogo"]);
         //   imglogo.AlternateText = dtCompany.Rows[0]["CompanyName"].ToString();
       //     lblCopyright.Text = dtCompany.Rows[0]["Copyright"].ToString();
            Session["email"] = dtCompany.Rows[0]["Email"].ToString();
            Session["website"] = dtCompany.Rows[0]["website"].ToString();
            Session["Mobile"] = dtCompany.Rows[0]["Mobile"].ToString();

        }
    }

    private void InsertMemberMasterLoginDetail(int MsrNo, int isverified)
    {
        Int32 intresult = 0;
        cls_connection cls = new cls_connection();
        intresult = cls.insert_data("Exec ProcMLM_AddEditMemberMasterLoginDetail_otp 0,'" + Convert.ToString(Request.UserHostAddress) + "','" + MsrNo.ToString() + "','" + isverified.ToString() + "'");

    }

    private void UpdateMemberMasterLastLogin(int MsrNo)
    {
        cls_Universal objUniversal = new cls_Universal();
        objUniversal.UpdateLastLogin("UpdateMemberLastLogin", MsrNo, Convert.ToString(Request.UserHostAddress));
    }
    #endregion

    public void bindbanner()
    {
        DataTable dt = new DataTable();
        cls_connection cls = new cls_connection();
        dt = cls.select_data_dt("select top 1 BannerImage from tblbanner where isactive=1 and isdelete=0 order by  BannerID desc");
        if (dt.Rows.Count > 0)
        {
          //  repeater1.DataSource = dt;
          //  repeater1.DataBind();
        }

    }

    protected void linkbtnpin_Click(object sender, EventArgs e)
    {
       // div_forgotpassword.Visible = false;
        divotp.Visible = false;
     //   divlogin.Visible = false;
     //   div_forgotpin.Visible = true;

    }
    protected void linkbtnforgot_Click(object sender, EventArgs e)
    {
       // div_forgotpassword.Visible = true;
        divotp.Visible = false;
        //divlogin.Visible = false;
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      //  div_forgotpin.Visible = false;
      //  div_forgotpassword.Visible = false;
        divotp.Visible = false;
      //  divlogin.Visible = true;
    }
}
