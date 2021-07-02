using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsProductFeature
    {
        public int AddEditProductFeature(int ProductFeatureID, string ProductFeatureName, int ProductCategoryID, string ProductCategoryIDStr,int ProductFeatureCategoryID, bool IsHighlight, bool IsFilter, bool IsCompare)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductFeatureTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductFeatureTableAdapter())
            {
                dt = obj.AddEditProductFeature(ProductFeatureID, ProductFeatureName, ProductCategoryID, ProductCategoryIDStr,ProductFeatureCategoryID, IsHighlight, IsFilter, IsCompare);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProductFeatureID"].ToString());
            return id;
        }

        public DataTable ManageProductFeature(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductFeatureTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductFeatureTableAdapter())
            {
                dt = obj.ManageProductFeature(Action, ID);
            } return dt;
        }
    }
}

