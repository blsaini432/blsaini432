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
public partial class Root_Distributor_Ewalletdouble : System.Web.UI.Page
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
       
            if (Request.Params["EncData"] != null)
            {
                byte[] array = System.Text.Encoding.UTF8.GetBytes("ut!p@nxd.89j]-}(");
                string EncData = Request.Params["EncData"].ToString();
                string Result = Decrypt(EncData, array);
                string encdata = Result.Replace(@"&", ",");
                string[] lines = encdata.Split(',');
                foreach (string lifnes in lines)
                {
                    string transID = lines[0].ToString();
                    string UTITSLTransID = lines[1].ToString();
                    string transAmt = lines[2].ToString();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('UTI Payment Successfull');location.replace('utipan.aspx');", true);

            }
 
    }
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

