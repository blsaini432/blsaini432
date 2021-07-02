using Newtonsoft.Json;
using Paytm;
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
public partial class Paytmcallback : System.Web.UI.Page
{
    #region
    cls_myMember clsm = new cls_myMember();
    cls_connection cls = new cls_connection();
    public string status;
    public string wallet;
    public string method;
    public string cardid;
    public string vpa;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["ORDERID"] != null)
        {
            //if (Session["userid"] != null && Session["Amount"] != null && Session["orderId"] != null)          
            //   {
            string ORDERID = Request.Form["ORDERID"];
            DataTable orderid = cls.select_data_dt("select * from tbl_paymentGateway where POrderid='" + ORDERID + "' and statuss='Pending'");
            if (orderid.Rows.Count > 0)
            {
                // string userid = orderid.Rows[0]["Memberid"].ToString();
                string xml_data = new StreamReader(Request.InputStream).ReadToEnd();
                string MID = Request.Form["MID"];
                string TXNID = Request.Form["TXNID"];
                string TXNAMOUNT = Request.Form["TXNAMOUNT"];
                string PAYMENTMODE = Request.Form["PAYMENTMODE"];
                string STATUS = Request.Form["STATUS"];
                string RESPMSG = Request.Form["RESPMSG"];
                string GATEWAYNAME = Request.Form["GATEWAYNAME"];
                string BANKTXNID = Request.Form["BANKTXNID"];
                string BANKNAME = Request.Form["BANKNAME"];
                string CHECKSUMHASH = Request.Form["CHECKSUMHASH"];
                string TXNDATE = Request.Form["TXNDATE"];
                String paytmChecksum = "";

                /* Create a Dictionary from the parameters received in POST */
                Dictionary<String, String> paytmParams = new Dictionary<String, String>();

                foreach (string key in Request.Form.Keys)
                {
                    if (key.Equals("CHECKSUMHASH"))
                    {
                        paytmChecksum = Request.Form[key];
                    }
                    else
                    {
                        paytmParams.Add(key.Trim(), Request.Form[key].Trim());
                    }
                }

                bool isVerifySignature = Paytm.Checksum.verifySignature(paytmParams, "bXi4gTq1@&QglAqB", paytmChecksum);
                if (isVerifySignature == true)
                {
                    try
                    {
                        if (STATUS == "TXN_SUCCESS")
                        {
                            Dictionary<string, string> body = new Dictionary<string, string>();
                            Dictionary<string, string> head = new Dictionary<string, string>();
                            Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();
                            body.Add("mid", "Myymke27224961376376");
                            body.Add("orderId", ORDERID);
                            string paytmchecksums = Checksum.generateSignature(JsonConvert.SerializeObject(body), "bXi4gTq1@&QglAqB");
                            head.Add("signature", paytmchecksums);
                            requestBody.Add("body", body);
                            requestBody.Add("head", head);
                            string post_data = JsonConvert.SerializeObject(requestBody);
                            //For  Staging
                           // string url = "https://securegw-stage.paytm.in/v3/order/status";
                            //For  Production 

                           string  url  =  "https://securegw.paytm.in/v3/order/status";
                            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                            webRequest.Method = "POST";
                            webRequest.ContentType = "application/json";
                            webRequest.ContentLength = post_data.Length;
                            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                            {
                                requestWriter.Write(post_data);
                            }
                            string responseData = string.Empty;
                            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                            {
                                responseData = responseReader.ReadToEnd();
                                if (responseData != string.Empty)
                                {
                                    DataSet ds = Deserialize(responseData);
                                    if (ds.Tables.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows[0]["signature"] != null)
                                        {
                                            if (ds.Tables[2].Rows[0]["resultStatus"].ToString() == "TXN_SUCCESS")
                                            {
                                                DataTable wallet = cls.select_data_dt("select * from tblMLM_EWalletTransaction where narration Like '%" + ORDERID + "%' and Factor='Cr'");
                                                if (wallet.Rows.Count > 0)
                                                {
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Status Arleady Updated.!');location.replace('Membersignup.aspx');", true);
                                                }
                                                else
                                                {
                                                    double NetAmount = Convert.ToDouble(TXNAMOUNT);
                                                    DataTable dtresult = new DataTable();
                                                  //  cls.update_data("update tbl_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',feerate='" + null + "',totalamount='" + NetAmount + "',Statuss='Success',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                                    #region [Insert]
                                                    Int32 intresult = 0;
                                                    Random random = new Random();
                                                    int SixDigit = random.Next(100000, 999999);
                                                    string MemberID = "";
                                                    MemberID = SixDigit.ToString();
                                                    string DOJ = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
                                                    string MDOB = "";
                                                    MDOB = String.Format("{0:dd-MM-yyyy}", DateTime.Now.Date);
                                                    try
                                                    {
                                                        //string strimage = profilepicupload(fupmppic);
                                                        string strimage = "";
                                                        DataTable dtrjesult = new DataTable();
                                                        int pwd4digit = random.Next(1000, 9999);
                                                        int transpin = random.Next(1000, 9999);
                                                        string password = pwd4digit.ToString();
                                                        string transpassord = transpin.ToString();
                                                        string hdfvalue = "";
                                                        string firstname = Session["FirstName"].ToString();
                                                        string lastname = Session["LastName"].ToString();
                                                        string Name = firstname + "" + lastname;
                                                        string email = Session["Email"].ToString();
                                                        string mobile = Session["mobile"].ToString();
                                                        string stdcode = Session["Stdcode"].ToString();
                                                        string landline = Session["landline"].ToString();
                                                        string address = Session["address"].ToString();
                                                        string country = Session["country"].ToString();
                                                        string state = Session["state"].ToString();
                                                        string city = Session["city"].ToString();
                                                        string cityname = Session["cityname"].ToString();
                                                        string type = Session["type"].ToString();
                                                        string zip = Session["zip"].ToString();
                                                        string ss = "Received";
                                                        DateTime dd = DateTime.Now;
                                                        dtresult = cls.select_data_dt("Exec ProcMLM_AddEditMemberMaster_temp_reg 0,'" + MemberID + "','" + firstname + "','" + lastname + "','" + email + "','" + "" + "','','" + password + "','" + transpassord + "','" + mobile + "','" + stdcode + "','" + landline + "','" + address + "','" + type + "','" + Convert.ToInt32(country) + "','" + Convert.ToInt32(state) + "','" + Convert.ToInt32(city) + "','" + cityname + "','" + zip + "','','0', '" + hdfvalue + "', '0', '" + TXNID + "'");
                                                      // cls.update_data("insert into regpaymentdetails(Payment,Name,RequestDate,Amount,txnID,membertype,mobile)values(1,'" + Name + "','" + dd + "','" + TXNAMOUNT + "','" + TXNID + "','" + type + "','" + mobile + "')");
                                                        intresult = Convert.ToInt32(dtresult.Rows[0][0]);
                                                        if (intresult > 0)
                                                        {

                                                            Session["FirstName"] = null;
                                                            Session["LastName"] = null;
                                                            Session["Email"] = null;
                                                            Session["mobile"] = null;
                                                            Session["Stdcode"] = null;
                                                            Session["landline"] = null;
                                                            Session["address"] = null;
                                                            Session["type"] = null;
                                                            Session["country"] = null;
                                                            Session["state"] = null;
                                                            Session["city"] = null;
                                                            Session["cityname"] = null;
                                                            Session["zip"] = null;
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Success|Your request has been sent to admin for approval. Concerning team will contact you soon.');location.replace('Signup.aspx');", true);
                                                        }
                                                        else
                                                        {
                                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !');location.replace('Signup.aspx');", true);

                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        cls.select_data_dt("insert into mtest values('" + ex.ToString() + "')");
                                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|Member Already Exists !')location.replace('Signup.aspx');", true);
                                                    }
                                                    #endregion

                                                    //   int balance = clsm.Wallet_MakeTransaction_Ezulix(memberid, Convert.ToDecimal(NetAmount), "Cr", "Add Found PG Order ID : " + ORDERID);
                                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction sucessfully.!');location.replace('Membersignup.aspx');", true);

                                                }

                                            }
                                            else
                                            {
                                                string memberid = Session["userid"].ToString();
                                                cls.update_data("update tbl_paymentGateway set TXNID='" + TXNID + "',PAYMENTMODE='" + PAYMENTMODE + "', RESPMSG='" + RESPMSG + "',GATEWAYNAME='" + GATEWAYNAME + "',BANKTXNID='" + BANKTXNID + "',Statuss='Failed',BANKNAME='" + BANKNAME + "',TXNDATE='" + TXNDATE + "' where TransactionId='" + ORDERID + "'");
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Please Try After Some Time!');window.location ='Membersignup.aspx';", true);
                                            }
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Please Try After Some Time!');window.location ='Membersignup.aspx';", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Data Found Please Check After Some Time!');window.location ='Membersignup.aspx';", true);
                                    }


                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Please Try After Some Time!');window.location ='Membersignup.aspx';", true);
                                }
                            }
                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Transaction Failed Please Try After Some Time');location.replace('Membersignup.aspx');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        cls.select_data_dt("insert into ErrorLog values('" + ex.ToString() + "')");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Some Error Found Please Try After Some Time !')location.replace('Membersignup.aspx');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Again Payment process');location.replace('Membersignup.aspx');", true);
                    return;
                }

                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Again Payment process');window.location ='Membersignup.aspx';", true);
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Again Payment process!! ');window.location ='Membersignup.aspx';", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Some Error Found Your side Again Payment process!! ');window.location ='DashBoard.aspx';", true);
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
    private void setControl()
    {
        Session["PayOrderId"] = null;
        Session["tx"] = null;
        Session["txtAmount"] = null;
        Session["Returnurl"] = null;
    }
    public double TotupAmount(double amount, double rate)
    {
        double NetAmount = 0;
        double surcharge_amt = 0; double surcharge_rate = 0;
        if (amount > 0)
        {
            surcharge_rate = rate;
            if (surcharge_rate > 0)
            {
                surcharge_amt = (Convert.ToDouble(amount) * surcharge_rate) / 100;
            }
            NetAmount = surcharge_amt;
        }
        else
        {
            NetAmount = 0;
        }
        return NetAmount;
    }

}

