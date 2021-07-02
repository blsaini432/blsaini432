using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsProductBelongsTO
    {
        public int AddEditProductBelongsTO(int ProductBelongsTOID,int ProductCategoryID,string ProductCategoryIDStr,int ProductID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductBelongsTOTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductBelongsTOTableAdapter())
            {
                dt = obj.AddEditProductBelongsTO(ProductBelongsTOID, ProductCategoryID, ProductCategoryIDStr, ProductID);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProductBelongsTOID"].ToString());
            return id;
        }

        public DataTable ManageProductBelongsTO(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductBelongsTOTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductBelongsTOTableAdapter())
            {
                dt = obj.ManageProductBelongsTO(Action, ID);
            } return dt;
        }
    }
}

