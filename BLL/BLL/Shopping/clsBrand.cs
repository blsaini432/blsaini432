using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsBrand
    {
        public int AddEditBrand(int BrandID, string BrandName, string BrandBanner, string BrandIcon, string BrandDesc, string BrandWebsite, string BrandTollFree, string MetaTitle, string MetaKeywords, string MetaDesc, bool IsHighlight, bool IsFeatured, bool IsSpecial, bool IsPopular, bool IsSale, bool IsOccasion, bool IsNew, int ProductCategoryID, string ProductCategoryIDStr, decimal Tax, string ProductCategoryName, string DiscountType, decimal DiscountValue, int DiscountID, string Extra1, string Extra2, string Extra3)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblBrandTableAdapter obj = new DAL.DataSet1TableAdapters.tblBrandTableAdapter())
            {
                dt = obj.AddEditBrand(BrandID, BrandName, BrandBanner, BrandIcon, BrandDesc, BrandWebsite, BrandTollFree, MetaTitle, MetaKeywords, MetaDesc, IsHighlight, IsFeatured, IsSpecial, IsPopular, IsSale, IsOccasion, IsNew, ProductCategoryID, ProductCategoryIDStr, Tax, ProductCategoryName, DiscountType, DiscountValue, DiscountID, Extra1, Extra2, Extra3);
            }
            id = Convert.ToInt32(dt.Rows[0]["BrandID"].ToString());
            return id;
        }

        public DataTable ManageBrand(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblBrandTableAdapter obj = new DAL.DataSet1TableAdapters.tblBrandTableAdapter())
            {
                dt = obj.ManageBrand(Action, ID);
            } return dt;
        }
    }
}

