using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsWebsite
    {
        public int AddEditWebsite(int WebsiteID,string WebsiteName,string TagLine,string WebsiteLogo,string WebsiteVideo,string WebsiteOwner,string WebsiteDesc,string TopLine,string BottomLine,string MarqueeLine,string Phone,string Mobile,string EmailSales,string EmailBilling,string EmailSupport,string EmailInfo,string EmailContact,string DesignBy,string DesignByURL,string Copyright,string Facebook,string Twitter,string GooglePlus,string Linkdin,string Youtube,string Pinterest,string Extra1,string Extra2,string Extra3,string Extra4,string Extra5,string Extra6,string Extra7,string DiscountType,decimal DiscountValue,int DiscountID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblWebsiteTableAdapter obj = new DAL.DataSet1TableAdapters.tblWebsiteTableAdapter())
            {
                dt = obj.AddEditWebsite(WebsiteID, WebsiteName, TagLine, WebsiteLogo, WebsiteVideo, WebsiteOwner, WebsiteDesc, TopLine, BottomLine, MarqueeLine, Phone, Mobile, EmailSales, EmailBilling, EmailSupport, EmailInfo, EmailContact, DesignBy, DesignByURL, Copyright, Facebook, Twitter, GooglePlus, Linkdin, Youtube, Pinterest, Extra1, Extra2, Extra3, Extra4, Extra5, Extra6, Extra7, DiscountType, DiscountValue, DiscountID);
            }
            id = Convert.ToInt32(dt.Rows[0]["WebsiteID"].ToString());
            return id;
        }

        public DataTable ManageWebsite(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblWebsiteTableAdapter obj = new DAL.DataSet1TableAdapters.tblWebsiteTableAdapter())
            {
                dt = obj.ManageWebsite(Action, ID);
            } return dt;
        }
    }
}

