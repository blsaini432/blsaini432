using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

public partial class Api_testewallet : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();
    // public string M_Uri = "https://localhost:59492/";
    public string M_Uri = "https://paydeer.in/";
    private int checksumValue;
    private static object serverMac;
    string desckey = "ut!p@npp.05p]-}(";

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["userID"] == null)
            {
                string userID = "12332324";
                string applicationNo = "Q002844514";
                string UTITSLTransID = "PY0041659682";
                string transAmt = "107.00";
                string transStatus = "S";
                string Result = string.Empty;
                try
                {
                    byte[] array = System.Text.Encoding.UTF8.GetBytes("ut!p@npp.05p]-}(");
                    string id = "12332324";
                    string application = "Q002844514";
                    string transid = "PY0041659682";
                    string transamount = "107.00";
                    DataTable dt = new DataTable();
                    var Results = string.Empty;
                    string TxnID = Clsm.Cyrus_GetTransactionID_New();
                    string Status = Encrypt(transStatus, array);
                    string transID = Encrypt(TxnID, array);
                    string Utiid = Encrypt(transid, array);
                    string applications = Encrypt(application, array);
                    string amounts = Encrypt(transamount, array);
                    string url = "https://www.myutiitsl.com/panonlineservices/paymentProcess.jsp" + "?" + "&transID=" + transID + "&transStatus=" + Status + "&applicationNo=" + applications + "&UTITSLTransID=" + Utiid + "&transAmt=" + amounts;
                    //string url = "https://www.myutiitsl.com/panonlineservices/loginCheckin";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Open", "window.open('" + url + "');", true);


                }

                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
                }
            }
            else
            {
                Response.Redirect("http://www.google.com");
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Found');", true);
        }

    }
    public string Paymentprocess(string transID, string Status, string applications, string Utiid, string amounts, string id)
    {
        try
        {
            string Json = string.Empty;
            return HTTP_POST(M_Uri + "Ewalletutipan.aspx?transID=" + transID + "&transStatus=" + Status + "&applicationNo=" + applications + "&UTITSLTransID=" + Utiid + "&transAmt=" + amounts + "&id=" + id + "", Json);

        }
        catch (Exception err)
        {
            return err.Message.ToString();
        }
    }
    public static string HTTP_POST(string Url, object Data)
    {
        string Out = String.Empty;
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
                streamWriter.Write(Url);
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

            };
        }
        string myresponse = Out.ToString();
        return myresponse;
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
        string EncryptionKey = "ut!p@npp.05p]-}(";
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