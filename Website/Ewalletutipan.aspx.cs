using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
public partial class Ewalletutipan : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    private int checksumValue;
    private static object serverMac;
    string desckey = "ut!p@npp.05p]-}(";
   // public string M_Uri = "https://ezulix.in/api/Utipan/";
     public string M_Uri = "http://localhost:49530/api/Utipan/";
    private static string MemberId = "EZ479539";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //string EncDatas = "Sdog+tN9NzPIKEvXdQmVD7oo7A+zLFKpuxoeacVhoTtIY0YSi9dnP5TOpuOfRweTocnJXobBDmN4jjCvZfxGgg==";
                if (Request.QueryString["transID"] != null && (Request.QueryString["transStatus"] != null) && (Request.QueryString["applicationNo"] != null) && (Request.QueryString["UTITSLTransID"] != null))
                {
                    DataTable dt = new DataTable();
                    string txnid = Request.QueryString["transID"].ToString();
                    string transStatus = Request.QueryString["transStatus"].ToString();
                    string applicationNo = Request.QueryString["applicationNo"].ToString();
                    string UTITSLTransID = Request.QueryString["UTITSLTransID"].ToString();
                    string id = Request.QueryString["id"].ToString();
                    string transAmt = Request.QueryString["transAmt"].ToString();
                    dt = Cls.select_data_dt("select * from TBL_UTIPANS where userid='" + id + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string memberid = dt.Rows[0]["memberID"].ToString();
                        string txn = dt.Rows[0]["txn"].ToString();
                        Cls.select_data_dt(@"Update TBL_UTIPANS set applicationNo='" + applicationNo + "',UTITSLTransID='" + UTITSLTransID + "',transAmt='" + transAmt + "',transStatus='" + transStatus + "' Where userid='" + id + "'");
                        Clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal("-" + transAmt), "Dr", "Uti Pan card Request:'" + txn + "'");
                        // Response.Clear();
                        //Response.ClearHeaders();
                        Response.Write("Success");
                       // string Results = string.Empty;
                       // Results = paymentprocess(MemberId, applicationNo, UTITSLTransID, transAmt, transStatus,txnid);
                       // ScriptManager.RegisterStartupScript(this, this.GetType(), "Open", "window.open('" + Results + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some issuse contact admin team');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
            }
        }
    }
    public string paymentprocess(string memberid, string applicationNo, string UTITSLTransID, string transAmt, string transStatus,string txnid)
    {
        try
        {
            string json = string.Empty;
            return HTTP_POST(M_Uri + "Paymentprocess.aspx?memberid=" + memberid + "&applicationNo=" + applicationNo + " &UTITSLTransID=" + UTITSLTransID + "&transAmt=" + transAmt + "&transStatus=" + transStatus + "&TxnID=" + txnid + "", json);
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
    static class sha256
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
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    public static string Encrypt(string textToEncrypt, byte[] keyValue)
    {
        //if (!VerifyMac())
        //{
        //    throw new Exception("Authentication Failed");
        //}
        RijndaelManaged managed = new RijndaelManaged
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            KeySize = 0x80,
            BlockSize = 0x80
        };
        byte[] sourceArray = keyValue;
        byte[] destinationArray = new byte[0x10];
        int length = sourceArray.Length;
        if (length > destinationArray.Length)
        {
            length = destinationArray.Length;
        }
        Array.Copy(sourceArray, destinationArray, length);
        managed.Key = destinationArray;
        managed.IV = destinationArray;
        ICryptoTransform transform = managed.CreateEncryptor();
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
        return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
    }
    //public static string Encrypt(string input, string key)
    //{
    //    byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
    //    TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
    //    tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
    //    tripleDES.Mode = CipherMode.ECB;
    //    tripleDES.Padding = PaddingMode.PKCS7;
    //    ICryptoTransform cTransform = tripleDES.CreateEncryptor();
    //    byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
    //    tripleDES.Clear();
    //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    //}
    public static string Decrypt(string textToDecrypt, byte[] keyValue)
    {
        //if (!VerifyMac())
        //{
        //    throw new Exception("Authentication Failed");
        //}
        RijndaelManaged managed = new RijndaelManaged
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            KeySize = 0x80,
            BlockSize = 0x80
        };
        int length = textToDecrypt.Length;
        byte[] inputBuffer = Convert.FromBase64String(textToDecrypt.Replace(" ", "+"));
        byte[] sourceArray = keyValue;
        byte[] destinationArray = new byte[0x10];
        int num2 = sourceArray.Length;
        if (num2 > destinationArray.Length)
        {
            num2 = destinationArray.Length;
        }
        Array.Copy(sourceArray, destinationArray, num2);
        managed.Key = destinationArray;
        managed.IV = destinationArray;
        byte[] bytes = managed.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
        return Encoding.UTF8.GetString(bytes);
    }
    public static bool VerifyMac()
    {
        String SystemMac = getMac();
        return serverMac.Equals(SystemMac);
    }
    public static string getMac()
    {
        try
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }
        catch (Exception)
        {
            return "";
        }
    }
    private DataSet Deserialize(string result)
    {
        DataSet ds = new DataSet();
        ds.Clear();
        XmlDocument doc = JsonConvert.DeserializeXmlNode(result, "root");
        StringReader theReader = new StringReader(doc.InnerXml.ToString());
        ds.ReadXml(theReader);
        return ds;
    }
    public class response
    {
        public string transID { get; set; }
        public string UTITSLTransID { get; set; }
        public string transStatus { get; set; }
        public string transAmt { get; set; }
    }

}

