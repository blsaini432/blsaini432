using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsProductFeatureCategory
    {
        public int AddEditProductFeatureCategory(int ProductFeatureCategoryID, string ProductFeatureCategoryName, int ProductCategoryID, string ProductCategoryIDStr, bool IsHighlight, bool IsFilter, bool IsCompare)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductFeatureCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductFeatureCategoryTableAdapter())
            {
                dt = obj.AddEditProductFeatureCategory(ProductFeatureCategoryID, ProductFeatureCategoryName, ProductCategoryID, ProductCategoryIDStr, IsHighlight, IsFilter, IsCompare);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProductFeatureCategoryID"].ToString());
            return id;
        }

        public DataTable ManageProductFeatureCategory(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductFeatureCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductFeatureCategoryTableAdapter())
            {
                dt = obj.ManageProductFeatureCategory(Action, ID);
            } return dt;
        }
    }
}

