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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
public partial class Root_Distributor_Ewalletutipan : System.Web.UI.Page
{
    #region Access_Class
    cls_connection Cls = new cls_connection();
    cls_myMember Clsm = new cls_myMember();


    private int checksumValue;
    private static object serverMac;
    string desckey = "ut!p@nxd.89j]-}(";

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["userID"] != null)
            {
                string userID = Request.Params["userID"].ToString();
                string applicationNo = Request.Params["applicationNo"].ToString();
                string UTITSLTransID = Request.Params["UTITSLTransID"].ToString();
                string transAmt = Request.Params["transAmt"].ToString();
                string transStatus = "S";
                string Result = string.Empty;
                try
                {
                    byte[] array = System.Text.Encoding.UTF8.GetBytes("ut!p@nxd.89j]-}(");
                    string id = Decrypt(userID, array);
                    string application = Decrypt(applicationNo, array);
                    string transid = Decrypt(UTITSLTransID, array);
                    string transamount = Decrypt(transAmt, array);

                    DataTable dt = new DataTable();
                    dt = Cls.select_data_dt("select * from TBL_UTIPANS where userid='" + id + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string memberid = dt.Rows[0]["memberid"].ToString();
                        string txn = dt.Rows[0]["txn"].ToString();
                        Cls.select_data_dt(@"Update TBL_UTIPANS set applicationNo='" + application + "',UTITSLTransID='" + transid + "',transAmt='" + transamount + "',transStatus='" + transStatus + "' Where userid='" + id + "'");
                        Clsm.Wallet_MakeTransaction(memberid, Convert.ToDecimal("-" + 1), "Dr", "Uti Pan card Request:'" + txn + "'");
                        string TxnID = Clsm.Cyrus_GetTransactionID_New();
                        string Status = Encrypt(transStatus, array);
                        string transID = Encrypt(TxnID, array);
                        string Utiid = Encrypt(transid, array);
                        string applications = Encrypt(application, array);
                        string amounts = Encrypt(transamount, array);
                         string url = "http://203.153.46.10:8080/panonlineservices/paymentProcess.jsp" + "?" + "&transID=" + transID + "&transStatus="+ Status + "&applicationNo=" + applications + "&UTITSLTransID="+ Utiid + "&transAmt="+ amounts;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Open", "window.open('" + url + "');", true);
                    }
                }

                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error');", true);
                }
            }
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
}

