using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsVideo
    {
        public int AddEditVideo(int VideoID, string VideoName, string VideoImage, string VideoIcon, string VideoDesc, string NavigateURL, string VideoYouTube, string VideoOther, int VideoCategoryID, string VideoCategoryName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblVideoTableAdapter obj = new DAL.DataSet1TableAdapters.tblVideoTableAdapter())
            {
                dt = obj.AddEditVideo(VideoID, VideoName, VideoImage, VideoIcon, VideoDesc, NavigateURL, VideoYouTube, VideoOther, VideoCategoryID, VideoCategoryName);
            }
            id = Convert.ToInt32(dt.Rows[0]["VideoID"].ToString());
            return id;
        }

        public DataTable ManageVideo(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblVideoTableAdapter obj = new DAL.DataSet1TableAdapters.tblVideoTableAdapter())
            {
                dt = obj.ManageVideo(Action, ID);
            } return dt;
        }
    }
}

