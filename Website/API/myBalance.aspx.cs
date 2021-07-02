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
public partial class api_myBalance : System.Web.UI.Page
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

                if (Request.QueryString["memberid"] != null && Request.QueryString["pin"] != null)
                {
                    string memberid = Request.QueryString["memberid"].ToString().Replace("'", "").Replace("-", "");
                    string pin = Request.QueryString["pin"].ToString().Replace("'", "").Replace("-", "");
                    DataTable dtMemberMaster = objconnection.select_data_dt("select * from tblMLM_MemberMaster where MemberID='" + memberid + "' and transactionpassword='" + pin + "'");
                    if (dtMemberMaster.Rows.Count > 0)
                    {
                        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                        dtEWalletBalance = objEWalletBalance.ManageEWalletBalance("GetBalanceByMsrNo", MsrNo);
                        //Response.Write(Convert.ToString(dtEWalletBalance.Rows[0]["Balance"]) + ",");
                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                            new XElement("ApiBalance",
                            new XElement("Balance", Convert.ToString(dtEWalletBalance.Rows[0]["Balance"])),
                            new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                        ));
                        string _encodedXML = stud.ToString();
                        Response.ClearHeaders();
                        Response.AddHeader("content-type", "text/xml");
                        Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
                    }
                    else
                    {
                        stud = new XDocument(new XDeclaration("1.0", "utf-8", "true"),
                            new XElement("ApiBalance",
                            new XElement("Balance", "0"),
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
                        new XElement("ApiBalance",
                        new XElement("Balance", "0"),
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
                    new XElement("ApiBalance",
                    new XElement("Balance", "0"),
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
                    new XElement("ApiBalance",
                    new XElement("Balance", "Invalid Request !! Request has been forwarded to Admin."),
                   new XElement("time", String.Format("{0:dd-MM-yyyy hh:mm:ss tt}", DateTime.Now))
                ));
            string _encodedXML = stud.ToString();
            Response.ClearHeaders();
            Response.AddHeader("content-type", "text/xml");
            Response.Write("<?xml version='1.0' encoding='UTF-8'?>" + _encodedXML.ToString());
        }
    }
}