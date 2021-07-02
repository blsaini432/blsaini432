using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsBanner
    {
        public int AddEditBanner(int BannerID, string BannerName, string BannerImage, string BannerIcon, string BannerDesc, string NavigateURL)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblBannerTableAdapter obj = new DAL.DataSet1TableAdapters.tblBannerTableAdapter())
            {
                dt = obj.AddEditBanner(BannerID, BannerName, BannerImage, BannerIcon, BannerDesc, NavigateURL);
            }
            id = Convert.ToInt32(dt.Rows[0]["BannerID"].ToString());
            return id;
        }

        public DataTable ManageBanner(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblBannerTableAdapter obj = new DAL.DataSet1TableAdapters.tblBannerTableAdapter())
            {
                dt = obj.ManageBanner(Action, ID);
            } return dt;
        }
    }
}

