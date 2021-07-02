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
using System.Xml.Linq;
using System.Xml.Serialization;
public partial class Refundapi : System.Web.UI.Page
{
    cls_connection Cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string data = string.Empty;
            xml xml = new xml();
            string xml_data = new StreamReader(Request.InputStream).ReadToEnd();
            XElement trd = XElement.Parse(xml_data);
            string utiid = (string)trd.Element("utiitsl_id");
            string request_data = (string)trd.Element("request_data");
            string abvf = ((System.Xml.Linq.XElement)(trd.FirstNode)).Value;
            byte[] array = System.Text.Encoding.UTF8.GetBytes("ut!p@nxd.89j]-}(");
            string Result = Decrypt(request_data, array);
            Session["result"] = Result;
            string encdataa = Result.Replace(@"|", ",");
            string encdataas = encdataa.Replace(@"=", ",");
            string[] lines = encdataas.Split(',');
            foreach (string lifnes in lines)
            {
                string utiitsl_txnid = lines[0].ToString();
                Session["utiitsl_txnid"] = utiitsl_txnid; ;
                string utiitsl_txn = lines[1].ToString();
                Session["utiitsl_txn"] = utiitsl_txn;
                string refund_txn_no = lines[3].ToString();
                Session["refund_txn_no"] = refund_txn_no;
                string merchant_txn_status = lines[5].ToString();
                Session["merchant_txn_status"] = merchant_txn_status;
                string merchant_txn = lines[7].ToString();
                Session["merchant_txn"] = merchant_txn;
                string utiitsl_id = lines[9].ToString();
                Session["utiitsl_id"] = utiitsl_id;
                string refund_reason = lines[11].ToString();
                Session["refund_reason"] = refund_reason;
                string utiitsl_reference = lines[13].ToString();
                Session["utiitsl_reference"] = utiitsl_reference;
            }
            Random random = new Random();
            int ID = random.Next(10000000, 99999999);
            string MDOB = "";
            MDOB = String.Format("{0:yyyy-M-d HH:mm:ss}", DateTime.Now);
            string requestdata = Session["result"].ToString();
            xml.response_data = Encrypt(requestdata, array);
            xml.response_code = "900";
            xml.response_status = "Success";
            xml.response_server = "server=R|message=OK";
            xml.response_message = ID.ToString();
            xml.response_date = MDOB;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var serializer = new XmlSerializer(typeof(xml));
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8);
            serializer.Serialize(streamWriter, xml, ns);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(memoryStream, System.Text.Encoding.UTF8);
            var utf8EncodedXml = streamReader.ReadToEnd();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(utf8EncodedXml);
            data = doc.ToString();
            //Clear page
            Response.ContentType = "text/xml";
            Response.Write(utf8EncodedXml);
            Response.End();
        }
        catch (Exception ex)
        {
            Cls.select_data_dt("insert into mtest values('" + ex.ToString() + "')");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Some Error Found !');", true);
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

    public class xml
    {
        public string transID { get; set; }
        public string UTITSLTransID { get; set; }
        public string transStatus { get; set; }
        public string utiitsl_txn { get; set; }
        public string refund_txn_no { get; set; }
        public string merchant_txn_status { get; set; }
        public string merchant_txn { get; set; }
        public string refund_reason { get; set; }
        public string utiitsl_id { get; set; }
        public string utiitsl_reference { get; set; }
        public string refund_status { get; set; }
        public string response_code { get; set; }
        public string response_status { get; set; }
        public string response_message { get; set; }
        public string response_server { get; set; }
        public string response_date { get; set; }
        public string response_data { get; set; }
    }
    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
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
}

