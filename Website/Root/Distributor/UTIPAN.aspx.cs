using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class Root_Distributor_UTIPAN : System.Web.UI.Page
{
    #region Access_Class
     public string M_Uri = "https://ezulix.in/api/Utipan/";
   // public string M_Uri = "http://localhost:49530/api/Utipan/";
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    private static string MemberId = "EZ479539";
    private int checksumValue;
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
                    ViewState["MsrNo"] = dtMember.Rows[0]["MsrNo"];
                    Session["MsrNo"] = dtMember.Rows[0]["MsrNo"];
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
        try
        {
            string utiid = txt_userid.Text;
            DataTable dt = new DataTable();
            int Msrno = Convert.ToInt32(ViewState["MsrNo"]);
            string member = ViewState["MemberId"].ToString();
            string txn = Clsm.Cyrus_GetTransactionID_New();
            int result = Clsm.Cyrus_ChkEwalletBalance_BeforeTransaction(Convert.ToDecimal(1), Msrno);
            if (result > 0)
            {
                dt = Cls.select_data_dt("select * from TBL_UTIPANS where userid='" + utiid + "'");
                if (dt.Rows.Count < 1)
                {
                    List<ParmList> _list = new List<ParmList>();
                    _list.Add(new ParmList() { name = "@MsrNo", value = Msrno });
                    _list.Add(new ParmList() { name = "@memberID", value = member });
                    _list.Add(new ParmList() { name = "@userid", value = utiid });
                    _list.Add(new ParmList() { name = "@txn", value = txn });
                    _list.Add(new ParmList() { name = "@Action", value = "I" });
                    dt = Cls.select_data_dtNew("Proc_utipancards", _list);
                }
                string Results = string.Empty;
                Results = checkinlogin(MemberId, utiid);
                if (Results != "")
                {
                    if (Results != "ERROR")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Open", "window.open('" + Results + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Insufficient Balance in Wallet !');", true);
            }
        }
        catch (Exception EX)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
        }
    }
    static class Adler32
    {
        public static uint checksum = 1;

        /// <summary>Performs the hash algorithm on given data array.</summary>
        /// <param name="bytesArray">Input data.</param>
        /// <param name="byteStart">The position to begin reading from.</param>
        /// <param name="bytesToRead">How many bytes in the bytesArray to read.</param>
        public static uint ComputeHash(byte[] bytesArray, int byteStart, int bytesToRead)
        {
            int n;
            uint s1 = checksum & 0xFFFF;
            uint s2 = checksum >> 16;

            while (bytesToRead > 0)
            {
                n = (3800 > bytesToRead) ? bytesToRead : 3800;
                bytesToRead -= n;

                while (--n >= 0)
                {
                    s1 = s1 + (uint)(bytesArray[byteStart++] & 0xFF);
                    s2 = s2 + s1;
                }

                s1 %= 65521;
                s2 %= 65521;
            }

            checksum = (s2 << 16) | s1;
            return checksum;
        }

    }
    public string encrypt(string encryptString)
    {
        string EncryptionKey = "ut!p@nxd.89j]-}(";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }
    public static string Encrypt(string input, string key)
    {
        byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
        TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
        tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
        tripleDES.Mode = CipherMode.ECB;
        tripleDES.Padding = PaddingMode.PKCS7;
        ICryptoTransform cTransform = tripleDES.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
        tripleDES.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public string checkinlogin(string memberid, string utiid)
    {
        try
        {
            string json = string.Empty;
            return HTTP_POST(M_Uri + "logincheckin.aspx?memberid=" + memberid + "&utiid=" + utiid + "", json);
        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }
    #region WebServiceConsumeMethod
    public static string HTTP_POST(string Url, object Data)
    {
        string Out = string.Empty;
        try
        {
            cls_connection objconnection = new cls_connection();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Timeout = 10000000;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Data);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Out = streamReader.ReadToEnd();
            }
        }
        catch (WebException err)
        {
            if (err.Status == WebExceptionStatus.Timeout)
            {
                Out = err.Status.ToString();
            }
            else
            {
                Out = err.Message.ToString();
            }
        }
        string myresponse = Out.ToString();
        return myresponse;
    }
    #endregion

    #region deserialise method
    public DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        try
        {
            ds.Clear();
            XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
            StringReader theReader = new StringReader(doc.InnerXml.ToString());
            ds.ReadXml(theReader);
            return ds;
        }
        catch (Exception err)
        {
            return ds;
        }
    }


    #endregion
}

