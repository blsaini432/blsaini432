using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsPhotoCategory
    {
        public int AddEditPhotoCategory(int PhotoCategoryID, string PhotoCategoryName, string PhotoCategoryImage, string PhotoCategoryIcon, string PhotoCategoryDesc, string NavigateURL)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblPhotoCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblPhotoCategoryTableAdapter())
            {
                dt = obj.AddEditPhotoCategory(PhotoCategoryID, PhotoCategoryName, PhotoCategoryImage, PhotoCategoryIcon, PhotoCategoryDesc, NavigateURL);
            }
            id = Convert.ToInt32(dt.Rows[0]["PhotoCategoryID"].ToString());
            return id;
        }

        public DataTable ManagePhotoCategory(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblPhotoCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblPhotoCategoryTableAdapter())
            {
                dt = obj.ManagePhotoCategory(Action, ID);
            } return dt;
        }
    }
}

