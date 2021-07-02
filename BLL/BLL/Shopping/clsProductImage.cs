using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsProductImage
    {
        public int AddEditProductImage(int ProductImageID, string ProductImageName, string ProductImage, int ProductID, int ProductColorID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductImageTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductImageTableAdapter())
            {
                dt = obj.AddEditProductImage(ProductImageID, ProductImageName, ProductImage, ProductID,ProductColorID);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProductImageID"].ToString());
            return id;
        }

        public DataTable ManageProductImage(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductImageTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductImageTableAdapter())
            {
                dt = obj.ManageProductImage(Action, ID);
            } return dt;
        }
    }
}

