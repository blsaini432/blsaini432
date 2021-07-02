using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsVideoCategory
    {
        public int AddEditVideoCategory(int VideoCategoryID, string VideoCategoryName, string VideoCategoryImage, string VideoCategoryIcon, string VideoCategoryDesc, string NavigateURL)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblVideoCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblVideoCategoryTableAdapter())
            {
                dt = obj.AddEditVideoCategory(VideoCategoryID, VideoCategoryName, VideoCategoryImage, VideoCategoryIcon, VideoCategoryDesc, NavigateURL);
            }
            id = Convert.ToInt32(dt.Rows[0]["VideoCategoryID"].ToString());
            return id;
        }

        public DataTable ManageVideoCategory(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblVideoCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblVideoCategoryTableAdapter())
            {
                dt = obj.ManageVideoCategory(Action, ID);
            } return dt;
        }
    }
}

