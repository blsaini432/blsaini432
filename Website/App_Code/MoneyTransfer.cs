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
using System.Collections.Specialized;
/// <summary>
/// Summary description for MoneyTransfer
/// </summary>
public class MoneyTransfer
{
    private static string mm_token = "3529BE9E66";
    private static string mm_api_memberid = "AP552262";
    public string mm_URL = "http://api.Root.com/api/DMR.aspx?memberid=" + mm_api_memberid + "&apikey=" + mm_token + "";
    public MoneyTransfer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet MakeTransaction(string URL)
    {
        string myresponse = apicall(URL);
        DataSet ds = new DataSet();
        StringReader theReader = new StringReader(myresponse);
        ds.ReadXml(theReader);
        DataSet dt = new DataSet();
        dt = ds;
        return dt;
    }
    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        httpreq.Timeout = 30000;
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        catch (Exception ex)
        {
            return "0";
        }
    }
    public string SetPostData_GETSENDER(string usermobileno)
    {
        string mypost = mm_URL + "&trantype=GETSENDER&sender_number=" + usermobileno + "";
        return mypost.ToString();
    }
    public string SetPostData_ADDSENDER(string sender_number, string sender_name)
    {
        string mypost = mm_URL + "&trantype=ADDSENDER&sender_number=" + sender_number + "&sender_name=" + sender_name + "";
        return mypost.ToString();
    }
    public string SetPostData_VERIFYSENDER(string sender_number, string otp)
    {
        string mypost = mm_URL + "&trantype=VERIFYSENDER&sender_number=" + sender_number + "&otp=" + otp + "";
        return mypost.ToString();
    }
    public string SetPostData_GETRECEPIENT(string sender_number)
    {
        string mypost = mm_URL + "&trantype=GETRECEPIENT&sender_number=" + sender_number + "";
        return mypost;
    }
    public string SetPostData_ADDRECEPIENT(string sender_number, string RecepientAccountNumber, string RecepientIFSC, string RecepientMobile, string RecepientName)
    {
        string mypost = mm_URL + "&trantype=ADDRECEPIENT&sender_number=" + sender_number + "&RecepientAccountNumber=" + RecepientAccountNumber + "&RecepientIFSC=" + RecepientIFSC + "&RecepientMobile=" + RecepientMobile + "&RecepientName=" + RecepientName;
        return mypost;
    }
    public string SetPostData_DELETERECEPIENT(string sender_number, string reciepid)
    {
        string mypost = mm_URL + "&trantype=DELETERECEPIENT&sender_number=" + sender_number + "&reciepid=" + reciepid + "";
        return mypost;
    }
    public string SetPostData_RECEPIENTACCOUNTVERIFICATION(string sender_number, string recipientAccountNo, string recipientIfsc)
    {
        string mypost = mm_URL + "&trantype=RECEPIENTACCOUNTVERIFICATION&sender_number=" + sender_number + "&recipientAccountNo=" + recipientAccountNo + "&recipientIfsc=" + recipientIfsc + "";
        return mypost;
    }
    public string SetPostData_PROCESSMONEYTRANSFER(string sender_number, string recipientID, string transamount, string clientrefid, int channel, string recifsc)
    {
        string mypost = mm_URL + "&trantype=PROCESSMONEYTRANSFER&sender_number=" + sender_number + "&recipientID=" + recipientID + "&transamount=" + transamount + "&clientrefid=" + clientrefid + "&channel=" + channel + "&recifsc=" + recifsc;
        return mypost;
    }

    public string SetPostData_PROCESSMONEYTRANSFER_new(string sender_number, string bankname, string amount, string clientrefid, string accountno, string beneficiary, string type)
    {
        string mypost = "memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&trantype=PROCESSMONEYTRANSFER&usermobileno=" + sender_number + "&bankname=" + bankname + "&amount=" + amount + "&clientrefid=" + clientrefid + "&accountno=" + accountno + "&beneficiary=" + beneficiary + "&type=" + type;
        return mypost;
    }
    public string SetPostData_GenerateOTPRefund(string transactionid, string referenceid, string type)
    {
        string mypost = "memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&trantype=OTPREGENERATE&transactionid=" + transactionid + "&referenceid=" + referenceid + "&type=" + type;
        return mypost;
    }
    public string SetPostData_GenerateUdmrRefund(string transactionid, string referenceid, string type, string otp, string depositor)
    {
        string mypost = "memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&trantype=DMRREFUND&transactionid=" + transactionid + "&referenceid=" + referenceid + "&type=" + type + "&otp=" + otp + "&depositor=" + depositor;
        return mypost;
    }
    public string SetPostData_DmrVerify(string bankName, string accno, string cmob)
    {
        string mypost = "memberid=" + mm_api_memberid + "&apikey=" + mm_token + "&trantype=DMRVERIFY&accno=" + accno + "&cmob=" + cmob + "&bankName=" + bankName;
        return mypost;
    }
    public string SetPostData_GETBANKS()
    {
        string mypost = mm_URL + "&trantype=GETBANKS";
        return mypost;
    }
    public DataSet MakeTransaction_new(string postdata)
    {
        string mURL = "";
        mURL = "http://api.Ezulix.in/api/UDmr.aspx";
        //mURL = "http://localhost:51756/Website/api/UDmr.aspx";
        string URL = mURL;
        string poststring = postdata;
        string outputdata = HTTP_POST(URL, poststring);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(outputdata);
        DataSet ds = new DataSet();
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);

        return ds;
    }
    public static string HTTP_POST(string Url, string Data)
    {
        


        string Out = String.Empty;
        System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
        try
        {
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.UTF8.GetBytes(Data);
            req.ContentLength = sentData.Length;
            using (System.IO.Stream sendStream = req.GetRequestStream())
            {
                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();
            }
            System.Net.WebResponse res = req.GetResponse();
            System.IO.Stream ReceiveStream = res.GetResponseStream();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
            {
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);

                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    Out += str;
                    count = sr.Read(read, 0, 256);
                }
            }
        }
        catch (ArgumentException ex)
        {
            Out = string.Format("HTTP_ERROR :: The second HttpWebRequest object has raised an Argument Exception as 'Connection' Property is set to 'Close' :: {0}", ex.Message);
        }
        catch (WebException ex)
        {
            Out = string.Format("HTTP_ERROR :: WebException raised! :: {0}", ex.Message);
        }
        catch (Exception ex)
        {
            Out = string.Format("HTTP_ERROR :: Exception raised! :: {0}", ex.Message);
        }
        string myresponse = Out.ToString();

        return myresponse;
    }
}