using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsAds
    {
        public int AddEditAds(int AdsID, string AdsName,string ImageUrl,string NavigateUrl,string AlternateText,string Keyword,string Impressions,string Caption,string Target,string AdsLocation,string AdsType,decimal PerClick,decimal PerView,DateTime AdsFrom,DateTime AdsTo,string AdsDesc,string AdsRemark,int TotalClick,int TotalView,decimal TotalClickAmount,decimal TotalViewAmount)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblAdsTableAdapter obj = new DAL.DataSet1TableAdapters.tblAdsTableAdapter())
            {
                dt = obj.AddEditAds(AdsID, AdsName, ImageUrl, NavigateUrl, AlternateText, Keyword, Impressions, Caption, Target, AdsLocation, AdsType, PerClick, PerView, AdsFrom, AdsTo, AdsDesc, AdsRemark, TotalClick, TotalView, TotalClickAmount, TotalViewAmount);
            }
            id = Convert.ToInt32(dt.Rows[0]["AdsID"].ToString());
            return id;
        }

        public DataTable ManageAds(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblAdsTableAdapter obj = new DAL.DataSet1TableAdapters.tblAdsTableAdapter())
            {
                dt = obj.ManageAds(Action, ID);
            } return dt;
        }
    }
}

