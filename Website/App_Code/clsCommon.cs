using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Data;
using System.Web.SessionState;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Configuration;


/// <summary>
/// Summary description for clsCommon
/// </summary>
namespace Common
{
    #region Function
    public class Function
    {
        private Function() { }
        private static Hashtable m_executingPages = new Hashtable();

        #region [Remove Illegal Characters]

        public static string RemoveIllegalCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("'", string.Empty);
            text = text.Replace(" ", "-");
            //text =RemoveDiacritics(text);
            //text = RemoveExtraHyphen(text);
            return HttpUtility.HtmlEncode(text).Replace("%", string.Empty);
        }
        public static string changedatetommddyy(string ddmmyy)
        {
            string mmddyy = "";
            mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
            return mmddyy;
        }
        #endregion

        #region Switch Master Page

        public static string ReturnMasterPage(string usertype)
        {
            string Ratval = "!";
            if (usertype == "Agent")
            {
                Ratval = "~/Root/MasterPages/ClientMaster.master";
            }
            else if (usertype == "Member")
            {
                Ratval = "~/Root/MasterPages/ClientMaster.master";
            }
            else if (usertype == "Guest")
            {
                Ratval = "~/Root/MasterPages/GuestMaster.master";
            }
            else if (usertype == "Customer")
            {
                Ratval = "~/Root/MasterPages/CustomerMaster.master";
            }
            else
            {
                Ratval = "~/Root/MasterPages/AdminMaster.master";
            }
            return Ratval;
        
        }
        #endregion

        #region Encrypt / Decrypt
        public static string Encrypt(string Message)
        {
            //string Password = "cyrustechnoedge";
            string Password = "ABCDEF";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Password));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public static string Decrypt(string Message)
        {
            string Password = "ABCDEF";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Password));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }
        public static string Encrypt1(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",
                                                             typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt1(string cipherString)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        #endregion

        #region Get Distinct Values

        public static string[] GetDistinctValues(string[] array)
        {
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            for (int i = 0; i <= array.Length - 1; i++)
            {
                if (list.Contains(array[i]))
                {
                    continue;
                }
                list.Add(array[i]);
            }
            return list.ToArray();
        }

        #region Message Box
        public static void MessageBox(string sMessage)
        {

            // If this is the first time a page has called this method then
            if (!m_executingPages.Contains(HttpContext.Current.Handler))
            {
                // Attempt to cast HttpHandler as a Page.
                Page executingPage = HttpContext.Current.Handler as Page;

                if (executingPage != null)
                {
                    // Create a Queue to hold one or more messages.
                    Queue messageQueue = new Queue();

                    // Add our message to the Queue
                    messageQueue.Enqueue(sMessage);

                    // Add our message queue to the hash table. Use our page reference
                    // (IHttpHandler) as the key.
                    m_executingPages.Add(HttpContext.Current.Handler, messageQueue);

                    // Wire up Unload event so that we can inject some JavaScript for the alerts.
                    executingPage.Unload += new EventHandler(ExecutingPage_Unload);
                }
            }
            else
            {
                // If were here then the method has allready been called from the executing Page.
                // We have allready created a message queue and stored a reference to it in our hastable. 
                Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];

                // Add our message to the Queue
                queue.Enqueue(sMessage);
            }
        }
        // Our page has finished rendering so lets output the JavaScript to produce the alert's
        private static void ExecutingPage_Unload(object sender, EventArgs e)
        {
            // Get our message queue from the hashtable
            Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];

            if (queue != null)
            {
                StringBuilder sb = new StringBuilder();

                // How many messages have been registered?
                int iMsgCount = queue.Count;

                // Use StringBuilder to build up our client slide JavaScript.
                sb.Append("<script language='javascript'>");

                // Loop round registered messages
                string sMsg;
                while (iMsgCount-- > 0)
                {
                    sMsg = (string)queue.Dequeue();
                    sMsg = sMsg.Replace("\n", "\\n");
                    sMsg = sMsg.Replace("\"", "'");
                    sb.Append(@"alert( """ + sMsg + @""" );");
                }

                // Close our JS
                sb.Append(@"</script>");

                // Were done, so remove our page reference from the hashtable
                m_executingPages.Remove(HttpContext.Current.Handler);

                // Write the JavaScript to the end of the response stream.
                HttpContext.Current.Response.Write(sb.ToString());
            }
        }
        #endregion

        #region Convert Date Formet
        public static DateTime ConvertDateFormat(string str)
        {
            int dd, mm, yy;
            string[] strarr = new string[3];
            strarr = str.Split(new char[] { '/' }, str.Length);
            dd = Int32.Parse(strarr[0]);
            mm = Int32.Parse(strarr[1]);
            yy = Int32.Parse(strarr[2]);

            
           // string returnstr = mm + "/" + dd + "/" + yy;

          //  return Convert.ToDateTime(returnstr);
            DateTime dt = new DateTime(yy,mm,dd);
        string returnstr= String.Format("{0:MM/dd/yyyy}", dt);
          //  return (dt);
        return (Convert.ToDateTime(returnstr));
        }
        #endregion

    }
        #endregion

        #region Export Data
    public class Export
    {
        public static void ExportTopdf(DataTable dt, string filename)
        {

            GridView gv = new GridView();
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".pdf");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gv.DataSource = dt;
            gv.AllowPaging = false;
            gv.DataBind();
            gv.RenderControl(hw);

            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            HttpContext.Current.Response.Write(pdfDoc);

            HttpContext.Current.Response.End();
        }
        public static void ExportToWord(DataTable dt, string filename)
        {
            GridView GridView1 = new GridView();
            HttpContext.Current.Response.Clear();

            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".doc");

            HttpContext.Current.Response.Charset = "";

            HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.DataSource = dt;
            GridView1.AllowPaging = false;

            GridView1.DataBind();

            GridView1.RenderControl(hw);

            HttpContext.Current.Response.Output.Write(sw.ToString());

            HttpContext.Current.Response.Flush();

            HttpContext.Current.Response.End();

        }
        public static void ExportTocsv(DataTable dt, string filename)
        {
            GridView GridView1 = new GridView();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".csv");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < GridView1.Columns.Count; k++)
            {
                //add separator
                sb.Append(GridView1.Columns[k].HeaderText + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                for (int k = 0; k < GridView1.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(GridView1.Rows[i].Cells[k].Text + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            HttpContext.Current.Response.Output.Write(sb.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        public static void ExportToExcel(DataTable dt, string filename)
        {
            GridView GridView1 = new GridView();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.DataSource = dt;
            GridView1.AllowPaging = false;
            GridView1.DataBind();

            //Change the Header Row back to white color
            GridView1.CssClass = "ExportGridViewStyle";
            //  GridView1.HeaderRow.Style.Add("background-color", "green");
            //GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
            GridView1.HeaderStyle.CssClass = "ExportHeaderStyle";

            //Apply style to Individual Cells
            //GridView1.HeaderRow.Cells[0].Style.Add("background-color", "green");
            //GridView1.HeaderRow.Cells[1].Style.Add("background-color", "green");
            //GridView1.HeaderRow.Cells[2].Style.Add("background-color", "green");
            //GridView1.HeaderRow.Cells[3].Style.Add("background-color", "green");
            //Apply Class to all Rows
            GridView1.RowStyle.CssClass = "ExportRowStyle";
            GridView1.AlternatingRowStyle.CssClass = "ExRoottRowStyle";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow row = GridView1.Rows[i];

                //Change Color back to white
                //row.BackColor = System.Drawing.Color.White;

                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");

                //Apply style to Individual Cells of Alternating Row
                //if (i % 2 != 0)
                //{
                //    row.Cells[0].Style.Add("background-color", "#C2D69B");
                //    row.Cells[1].Style.Add("background-color", "#C2D69B");
                //    row.Cells[2].Style.Add("background-color", "#C2D69B");
                //    row.Cells[3].Style.Add("background-color", "#C2D69B");
                //}
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

    }
    #endregion

        #region Print Helper
    public class PrintHelper
    {
        public static void PrintWebControl(Control ctrl)
        {
            PrintWebControl(ctrl, string.Empty);
        }

        public static void PrintWebControl(Control ctrl, string Script)
        {
            
            StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
            if (ctrl is WebControl)
            {
                Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
            }
            Page pg = new Page();
            pg.EnableEventValidation = false;
            if (Script != string.Empty)
            {
                pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
            }
            HtmlForm frm = new HtmlForm();
            pg.Controls.Add(frm);
            frm.Attributes.Add("runat", "server");
            
            frm.Controls.Add(ctrl);
            
            pg.DesignerInitialize();
            pg.RenderControl(htmlWrite);
            string strHTML = stringWrite.ToString();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(strHTML);

            HttpContext.Current.Response.Write("<script>window.print();</script>");
            HttpContext.Current.Response.End();
        }
    }
    #endregion

        #region [Image Function]
    public class ImageFunction
    {
        #region [ ResizeImage ]..
        /*****************************************************************
		'Function : ResizeImage
		'Purpose  : To resize the image 
		'Algorithm: 
		'Input    : uploadedImageName : name of the image (like test.jpg, test.gif etc.)
					imgWidth : desired width of the image
					imgHeight: desired height of the image
					imgPath : path of the uploaded image
					imgUploadPath: path of the image to be uploaded
		'Output   :
		'Author   : virendra yogi
		
		'*****************************************************************/
        public static string ResizeImage(string uploadedImageName, int imgWidth, int imgHeight, int bigImgWidth, int bigImgHeight, string imgPath, string imgUploadPath)
        {

            string strUploadedFileName = System.Guid.NewGuid().ToString() + Path.GetExtension(uploadedImageName);

            try
            {

                //Read in the image filename whose thumbnail has to be created
                string imageUrl = uploadedImageName;
                //Read in the width and height
                int imageHeight = imgHeight;
                int imageWidth = imgWidth;
                int biginageHeight = bigImgHeight;
                int biginageWeight = bigImgWidth;
                if (imageUrl.IndexOf("/") >= 0 || imageUrl.IndexOf("\\") >= 0)
                {
                    //We found a / or \

                    // HttpContext.Current.Response.End();
                }
                //'Add on the appropriate directory
                imageUrl = imgPath + imageUrl;
                System.Drawing.Image fullSizeImg; //= new System.Drawing.Image();
                fullSizeImg = System.Drawing.Image.FromFile(imageUrl);
                //'Do we need to create a thumbnail?
                //HttpContext.Current.Response.ContentType = "image/gif";
                if (imageHeight > 0 && imageWidth > 0)
                {
                    System.Drawing.Image.GetThumbnailImageAbort dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                    System.Drawing.Image thumbNailImg;
                    System.Drawing.Image bigImg;
                    //
                    //bigImg = fullSizeImg.GetThumbnailImage(biginageWeight, biginageHeight, dummyCallBack, IntPtr.Zero);
                    //bigImg.Save(imgUploadPath + strUploadedFileName, ImageFormat.Jpeg);
                    fullSizeImg.Save(imgUploadPath + strUploadedFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //
                    thumbNailImg = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight, dummyCallBack, IntPtr.Zero);
                    thumbNailImg.Save(imgUploadPath + "thumb_" + strUploadedFileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                }
                else
                    fullSizeImg.Save(imgUploadPath + strUploadedFileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                fullSizeImg.Dispose();
            }
            catch (Exception ex)
            {

            }

            return strUploadedFileName;
        }
        #endregion

        #region [Resize Product Image]
        public static string ResizeProductImage(string uploadedImageName, int imgWidth, int imgHeight, string imgPath, string imgUploadPath)
        {

            string strUploadedFileName = uploadedImageName;

            try
            {
                //Read in the image filename whose thumbnail has to be created
                string imageUrl = uploadedImageName;
                //Read in the width and height
                int imageHeight = imgHeight;
                int imageWidth = imgWidth;
                if (imageUrl.IndexOf("/") >= 0 || imageUrl.IndexOf("\\") >= 0)
                {
                    //We found a / or \

                    // HttpContext.Current.Response.End();
                }
                //'Add on the appropriate directory
                imageUrl = imgPath + imageUrl;
                System.Drawing.Image fullSizeImg; //= new System.Drawing.Image();
                fullSizeImg = System.Drawing.Image.FromFile(imageUrl);
                //'Do we need to create a thumbnail?
                //HttpContext.Current.Response.ContentType = "image/gif";
                if (imageHeight > 0 && imageWidth > 0)
                {
                    System.Drawing.Image.GetThumbnailImageAbort dummyCallBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                    System.Drawing.Image thumbNailImg;
                    thumbNailImg = fullSizeImg.GetThumbnailImage(imageWidth, imageHeight, dummyCallBack, IntPtr.Zero);
                    thumbNailImg.Save(imgUploadPath + strUploadedFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                    fullSizeImg.Save(imgUploadPath + strUploadedFileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                fullSizeImg.Dispose();
            }
            catch (Exception ex)
            {

            }

            return strUploadedFileName;
        }
        #endregion

        public static bool ThumbnailCallback()
        {
            return false;
        }
        
    }
    #endregion

        #region Mail Sender
    public class MailSender
    {
        #region Send_Mail
        //public static Boolean Send_Mail(String Subject, String MailFrom, String MailTo, String MailBody)
        //{

        //    try
        //    {
               // string host = "", username = "", password = "";
               //DataAccess objDataAccess = new DataAccess();
               // DataTable dtInv = new DataTable();
               //dtInv = objDataAccess.GetDataTable("select * from dbo.tbl_support_email_config where FromMail='" + MailFrom + "'");
               // host = dtInv.Rows[0]["SMTP"].ToString();
               // username = dtInv.Rows[0]["FromMail"].ToString();
               // password = dtInv.Rows[0]["Password"].ToString();
               // System.Net.Mail.AlternateView plainView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(MailBody, @"<(.|\n)*?>", string.Empty), null, "text/plain");
               // System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(MailBody, null, "text/html");

               // MailMessage mailMessage = new MailMessage(MailFrom, MailTo, Subject, MailBody);

               // mailMessage.AlternateViews.Add(plainView);
               // mailMessage.AlternateViews.Add(htmlView);
               // SmtpClient smtpClient = new SmtpClient();
               // smtpClient.Host = host;
               // mailMessage.IsBodyHtml = true;
               // smtpClient.Port = 587;
               // smtpClient.EnableSsl = true;
               // smtpClient.UseDefaultCredentials = false;
               // smtpClient.Credentials = new NetworkCredential(username, password);
               // smtpClient.Send(mailMessage);
               // return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        #endregion
    }
    #endregion

    public class RegisterMail
    {
        public static void SendRegistrationMail(string Name, string companyname,string Email, string MobileNo, string Password, string TransactionPassword)
        {
            try
            {
                string[] valueArray = new string[7];
                valueArray[0] = Name;
                valueArray[1] = companyname;
                valueArray[2] = Email;
                valueArray[3] = MobileNo;
                valueArray[4] = Password;
                valueArray[5] = TransactionPassword;
                valueArray[6] = Name;
                FlexiMail objSendMail = new FlexiMail();
                objSendMail.To = Email;
                objSendMail.CC = "";
                objSendMail.BCC = "support@srdealonline.com";
                objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                objSendMail.FromName = "srdealonline.com";
                objSendMail.MailBodyManualSupply = false;
                objSendMail.EmailTemplateFileName = "welcome.htm";
                objSendMail.Subject = "Registration";
                objSendMail.ValueArray = valueArray;
                objSendMail.Send();
            }
            catch(Exception ex)
            {

            }
        }

        public static void Customer(string Name, string Email, string MobileNo, string Password, string TransactionPassword,string memberid)
        {
            try
            {
                string[] valueArray = new string[6];
                valueArray[0] = Name;
                valueArray[1] = Email;
                valueArray[2] = MobileNo;
                valueArray[3] = Password;
                valueArray[4] = TransactionPassword;
                valueArray[5] = memberid;
                FlexiMail objSendMail = new FlexiMail();
                objSendMail.To = Email;
                objSendMail.CC = "";
                objSendMail.BCC = "";
                objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                objSendMail.FromName = "sumitgroups.in";
                objSendMail.MailBodyManualSupply = false;
                objSendMail.EmailTemplateFileName = "welcome.htm";
                objSendMail.Subject = "Registration";
                objSendMail.ValueArray = valueArray;
                objSendMail.Send();
            }
            catch (Exception ex)
            { }
        }

        public static void FundRequest(string Name, string Email, string FromMemberID, string Amount)
        {
            try
            {
                string[] valueArray = new string[3];
                valueArray[0] = Name;
                valueArray[1] = FromMemberID;
                valueArray[2] = Amount;
                FlexiMail objSendMail = new FlexiMail();
                objSendMail.To = Email;
                objSendMail.CC = "";
                objSendMail.BCC = "";
                objSendMail.From = Convert.ToString(ConfigurationManager.AppSettings["mailFrom"]);
                objSendMail.FromName = "sumitgroups.in";
                objSendMail.MailBodyManualSupply = false;
                objSendMail.EmailTemplateFileName = "../../../EmailTemplates/Fund.htm";
                objSendMail.Subject = "Fund Request";
                objSendMail.ValueArray = valueArray;
                objSendMail.Send();
            }
            catch (Exception ex)
            { }
        }
    }

   
    #endregion

    #region Static Variable
    public class StaticVariable
    {
        public static int CompanyID;
        public static string UserType;
        public static int EditID = -1;
    }
    #endregion
}