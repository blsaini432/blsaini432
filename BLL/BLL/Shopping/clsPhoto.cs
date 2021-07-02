using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsPhoto
    {
        public int AddEditPhoto(int PhotoID, string PhotoName, string PhotoImage, string PhotoIcon, string PhotoDesc, string NavigateURL, int PhotoCategoryID, string PhotoCategoryName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblPhotoTableAdapter obj = new DAL.DataSet1TableAdapters.tblPhotoTableAdapter())
            {
                dt = obj.AddEditPhoto(PhotoID, PhotoName, PhotoImage, PhotoIcon, PhotoDesc, NavigateURL, PhotoCategoryID, PhotoCategoryName);
            }
            id = Convert.ToInt32(dt.Rows[0]["PhotoID"].ToString());
            return id;
        }

        public DataTable ManagePhoto(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblPhotoTableAdapter obj = new DAL.DataSet1TableAdapters.tblPhotoTableAdapter())
            {
                dt = obj.ManagePhoto(Action, ID);
            } return dt;
        }
    }
}

