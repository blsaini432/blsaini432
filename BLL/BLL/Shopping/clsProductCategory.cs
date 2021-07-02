using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsProductCategory
    {
        public int AddEditProductCategory(int ProductCategoryID, int ParentID, string ProductCategoryIDStr, string ProductCategoryName, string ProductCategoryIcon, string ProductCategoryBanner, string ProductCategoryDesc, string MetaTitle, string MetaKeywords, string MetaDesc, decimal Discount, decimal Tax, bool IsHighlight, bool IsFeatured, bool IsSpecial, bool IsPopular, bool IsSale, bool IsOccasion, bool IsNew, string DiscountType, decimal DiscountValue, int DiscountID, string Extra1, string Extra2, string Extra3)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductCategoryTableAdapter())
            {
                dt = obj.AddEditProductCategory(ProductCategoryID, ParentID, ProductCategoryIDStr, ProductCategoryName, ProductCategoryIcon, ProductCategoryBanner, ProductCategoryDesc, MetaTitle, MetaKeywords, MetaDesc, Discount, Tax, IsHighlight, IsFeatured, IsSpecial, IsPopular, IsSale, IsOccasion, IsNew, DiscountType, DiscountValue, DiscountID, Extra1, Extra2, Extra3);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProductCategoryID"].ToString());
            return id;
        }

        public DataTable ManageProductCategory(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductCategoryTableAdapter())
            {
                dt = obj.ManageProductCategory(Action, ID);
            } return dt;
        }
    }
}

