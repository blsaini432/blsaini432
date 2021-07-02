using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsAdsClickAndView
    {
        public int AddEditAdsClickAndView(int AdsClickAndViewID,string IPAddress,int ClickOrView,int AdsID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblAdsClickAndViewTableAdapter obj = new DAL.DataSet1TableAdapters.tblAdsClickAndViewTableAdapter())
            {
                dt = obj.AddEditAdsClickAndView(AdsClickAndViewID, IPAddress, ClickOrView, AdsID);
            }
            id = Convert.ToInt32(dt.Rows[0]["AdsClickAndViewID"].ToString());
            return id;
        }

        public DataTable ManageAdsClickAndView(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblAdsClickAndViewTableAdapter obj = new DAL.DataSet1TableAdapters.tblAdsClickAndViewTableAdapter())
            {
                dt = obj.ManageAdsClickAndView(Action, ID);
            } return dt;
        }
    }
}

