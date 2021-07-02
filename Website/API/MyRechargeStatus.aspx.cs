using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using System.Xml.Serialization;
using System.Text;
using System.Xml.Linq;
public partial class api_MyRechargeStatus : System.Web.UI.Page
{
    clsMLM_EWalletBalance objEWalletBalance = new clsMLM_EWalletBalance();
    DataTable dtEWalletBalance = new DataTable();

    cls_connection objconnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        XDocument stud;
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["memberid"] != null && Request.QueryString["pin"] != null && Request.QueryString["transid"] != null)
                {
                    string memberid = Request.QueryString["memberid"].ToString().Replace("'", "").Replace("-", "");
                    string pin = Request.QueryString["pin"].ToString().Replace("'", "").Replace("-", "");
                    DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where MemberID='" + memberid + "' and transactionpassword='" + pin + "'");
                    if (dtMemberMaster.Rows.Count > 0)
                    {
                        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                        string TransID = Request.QueryString["transid"].ToString().Replace("'", "").Replace("-", "");
                        DataTable dt = objconnection.select_data_dt("select * from tblRecharge_History where MsrNo=" + MsrNo + " and (TransID='" + TransID + "' or APItransid='" + TransID + "') and Convert(varchar,adddate,112)>=Convert(varchar,'20141223',112)");
                        //DataTable dt = objconnection.select_data_dt("select * from tblRecharge_History where MsrNo=" + MsrNo + " and (TransID='" + TransID + "' or APItransid='" + TransID + "')");
                        if (dt.Rows.Count > 0)
                        {
                            //Response.Write(Convert.ToString(dt.Rows[0]["Status"]) + ",");
                            string myresponse = "";
                            if (dt.Rows[0]["Status"].ToString().ToLower() == "failed")
                            {
                                //myresponse = "FAILURE," + dt.Rows[0]["APImessage"].ToString();
                                stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                new XElement("RechargeStatus",
                                new XElement("status", "FAILURE"),
                                new XElement("Message", dt.Rows[0]["APImessage"].ToString()),
                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                ));
                                string _encodedXML = stud.ToString();
                                Response.ClearHeaders();
                                Response.AddHeader("content-type", "text/xml");
                                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                            }
                            else
                            {
                                //myresponse = dt.Rows[0]["Status"].ToString() + "," + dt.Rows[0]["APImessage"].ToString();
                                stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                new XElement("RechargeStatus",
                                new XElement("status", dt.Rows[0]["Status"].ToString()),
                                new XElement("Message", dt.Rows[0]["APImessage"].ToString()),
                                new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                ));
                                string _encodedXML = stud.ToString();
                                Response.ClearHeaders();
                                Response.AddHeader("content-type", "text/xml");
                                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                            }
                            //Response.Write(myresponse + ",");
                        }
                        else
                        {
                            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                        new XElement("RechargeStatus",
                                        new XElement("status", "0"),
                                        new XElement("Message", "Record Not found"),
                                        new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                        ));
                            string _encodedXML = stud.ToString();
                            Response.ClearHeaders();
                            Response.AddHeader("content-type", "text/xml");
                            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                        }
                        //else
                        //{
                        //    dt = objconnection.select_data_dt("select * from tblRecharge_History1 where MsrNo=" + MsrNo + " and (TransID='" + TransID + "' or APItransid='" + TransID + "') and Convert(varchar,adddate,112)<Convert(varchar,'20141223',112)");
                        //    if (dt.Rows.Count > 0)
                        //    {
                        //        //Response.Write(Convert.ToString(dt.Rows[0]["Status"]) + ",");
                        //        string myresponse = "";
                        //        if (dt.Rows[0]["Status"].ToString().ToLower() == "failed")
                        //        {
                        //            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                        //            new XElement("RechargeStatus",
                        //            new XElement("status", "FAILURE"),
                        //            new XElement("Message", dt.Rows[0]["APImessage"].ToString()),
                        //            new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                        //            ));
                        //            string _encodedXML = stud.ToString();
                        //            Response.ClearHeaders();
                        //            Response.AddHeader("content-type", "text/xml");
                        //            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                        //        }
                        //        else
                        //        {
                        //            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                        //            new XElement("RechargeStatus",
                        //            new XElement("status", dt.Rows[0]["Status"].ToString()),
                        //            new XElement("Message", dt.Rows[0]["APImessage"].ToString()),
                        //            new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                        //            ));
                        //            string _encodedXML = stud.ToString();
                        //            Response.ClearHeaders();
                        //            Response.AddHeader("content-type", "text/xml");
                        //            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                        //        }
                        //        //Response.Write(myresponse + ",");
                        //    }
                        //    else
                        //    {
                        //        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                        //            new XElement("RechargeStatus",
                        //            new XElement("status", "0"),
                        //            new XElement("Message", ""),
                        //            new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                        //            ));
                        //        string _encodedXML = stud.ToString();
                        //        Response.ClearHeaders();
                        //        Response.AddHeader("content-type", "text/xml");
                        //        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                        //    }
                        //}
                    }
                    else
                    {
                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                    new XElement("RechargeStatus",
                                    new XElement("status", "0"),
                                    new XElement("Message", "Invalid member"),
                                    new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                    ));
                        string _encodedXML = stud.ToString();
                        Response.ClearHeaders();
                        Response.AddHeader("content-type", "text/xml");
                        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                    }
                }
                else
                {
                    stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                    new XElement("RechargeStatus",
                                    new XElement("status", "0"),
                                    new XElement("Message", "Invalid parameter"),
                                    new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                    ));
                    string _encodedXML = stud.ToString();
                    Response.ClearHeaders();
                    Response.AddHeader("content-type", "text/xml");
                    Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                }
            }
            else
            {
                stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                    new XElement("RechargeStatus",
                                    new XElement("status", "0"),
                                    new XElement("Message", ""),
                                    new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                    ));
                string _encodedXML = stud.ToString();
                Response.ClearHeaders();
                Response.AddHeader("content-type", "text/xml");
                Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
            }
        }
        catch (Exception ex)
        {
            stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                                    new XElement("RechargeStatus",
                                    new XElement("status", "0"),
                                    new XElement("Message", "Invalid Request !! Request has been forwarded to Admin."),
                                    new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                                    ));
            string _encodedXML = stud.ToString();
            Response.ClearHeaders();
            Response.AddHeader("content-type", "text/xml");
            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
        }
    }
}