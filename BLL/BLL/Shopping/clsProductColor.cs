using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsProductColor
    {
        public int AddEditProductColor(int ProductColorID, int ProductID, int ColorID, int Quantity, string Extra1, string Extra2, string ProductColorDesc)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductColorTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductColorTableAdapter())
            {
                dt = obj.AddEditProductColor(ProductColorID, ProductID, ColorID, Quantity, Extra1, Extra2, ProductColorDesc);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProductColorID"].ToString());
            return id;
        }

        public DataTable ManageProductColor(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductColorTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductColorTableAdapter())
            {
                dt = obj.ManageProductColor(Action, ID);
            } return dt;
        }
    }
}

