using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script;
using System.Web.Services;
using BLL;
using System.Xml;
using System.Linq;

public partial class AddCash : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string tx = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
        Random random = new Random();
        int id = random.Next(100000, 999999);
        Session["tx"] = tx;
        Session["id"] = id;
        //Response.Redirect("Add_cash_pReturn.aspx");
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://203.114.240.183/paynetz/epi/fts");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://payment.atomtech.in/paynetz/epi/fts");
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; CK={CVxk71YSfgiE6+6P6ftT7lWzblrdvMbRqavYf/6OcMIH8wfE6iK7TNkcwFAsxeChX7qRAlQhvPWso3KI6Jthvnvls9scl+OnAEhsgv+tuvs=}; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        string path = HttpContext.Current.Request.Url.AbsolutePath;
        string str = url.Replace(path, "");
        str = str + "/Mycash.aspx?id=" + id + "&tx=" + tx;
        string strQueryString = Server.UrlEncode(str);
      string reqHashKey = "c4a32e4aeab7f672d8";
     //   string reqHashKey = "KEY123657234";
        string signature = "";
       string strsignature = "110742" + "61e54dcf" + "NBFundTransfer" + "SEVA" + tx + Session["Amount"].ToString() + "INR";
  //  string strsignature = "197" + "Test@123" + "NBFundTransfer" + "NSE" + tx + Session["Amount"].ToString() + "INR"; 
        byte[] bytes = Encoding.UTF8.GetBytes(reqHashKey);
        byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
        signature = byteToHexString(bt).ToLower();
    string postData = "login=110742&pass=61e54dcf&ttype=NBFundTransfer&prodid=SEVA&amt=" + Session["Amount"].ToString() + "&txncurr=INR&txnscamt=0&clientcode=007&txnid=" + tx + "&date=" + DateTime.Now + "&custacc=1234567890&ru=https://payseva.co/MemberSignup.aspx&signature=" + signature;
      // string postData = "login=197&pass=Test@123&ttype=NBFundTransfer&prodid=NSE&amt=" + Session["Amount"].ToString() + "&txncurr=INR&txnscamt=0&clientcode=007&txnid=" + tx + "&date=" + DateTime.Now + "&custacc=1234567890&ru=https://payseva.co/MemberSignup.aspx&signature=" + signature;
       Response.Redirect("https://payment.atomtech.in/paynetz/epi/fts?" + postData);
     //  Response.Redirect("https://paynetzuat.atomtech.in/paynetz/epi/fts?" + postData); 
    }

    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
}
