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
public partial class AddCash : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string tx = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
        Random random = new Random();
        int id = random.Next(100000, 999999);
        Session["tx"] = tx;
        Session["id"] = id;
        Response.Redirect("Add_cash_pReturn.aspx");
        ////HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://203.114.240.183/paynetz/epi/fts");
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://payment.atomtech.in/paynetz/epi/fts");
        //request.Method = "POST";
        //request.ContentType = "application/x-www-form-urlencoded";
        //request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; CK={CVxk71YSfgiE6+6P6ftT7lWzblrdvMbRqavYf/6OcMIH8wfE6iK7TNkcwFAsxeChX7qRAlQhvPWso3KI6Jthvnvls9scl+OnAEhsgv+tuvs=}; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";


        //string url = HttpContext.Current.Request.Url.AbsoluteUri;
        //string path = HttpContext.Current.Request.Url.AbsolutePath;
        //string str = url.Replace(path, "");
        //str = str + "/Mycash.aspx?id=" + id + "&tx=" + tx;
        //string strQueryString = Server.UrlEncode(str);

        //string postData = "login=1315&pass=PITAM@123&ttype=NBFundTransfer&prodid=PITAMBARA&amt=" + Session["Amount"].ToString() + "&txncurr=INR&txnscamt=0&clientcode=007&txnid=" + tx + "&date=" + DateTime.Now + "&custacc=123456789012&ru=" + strQueryString;

        ////string postData = "login=160&pass=Test@123&ttype=NBFundTransfer&prodid=NSE&amt=" + Session["Amount"].ToString() + "&txncurr=INR&txnscamt=0&clientcode=007&txnid=" + tx + "&date=" + DateTime.Now + "&custacc=123456789012&&ru=" + strQueryString;


        //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //request.ContentType = "application/x-www-form-urlencoded";
        //request.ContentLength = byteArray.Length;
        //request.AllowAutoRedirect = true;
        //request.Proxy.Credentials = CredentialCache.DefaultCredentials;
        //Stream dataStream = request.GetRequestStream();
        //dataStream.Write(byteArray, 0, byteArray.Length);
        //dataStream.Close();
        //WebResponse response = request.GetResponse();
        //XmlDocument objXML = new XmlDocument();
        //dataStream = response.GetResponseStream();
        //objXML.Load(dataStream);
        //string TxnId = objXML.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[2].InnerText;
        //string Token = objXML.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[3].InnerText;
        //string txnData = "ttype=NBFundTransfer&txnStage=1&tempTxnId=" + TxnId + "&token=" + Token;
        //dataStream.Close();
        //response.Close();
        ////Response.Redirect("http://203.114.240.183/paynetz/epi/fts?" + txnData);
        //Response.Redirect("https://payment.atomtech.in/paynetz/epi/fts?" + txnData);
    }
}
