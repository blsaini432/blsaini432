using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsProduct
    {
        public int AddEditProduct(int ProductID, string ProductName, string ProductImage, string ProductDesc, int BrandID, string MetaTitle, string MetaKeywords, string MetaDesc, int Quantity, int Hits, decimal Tax, bool IsStock, bool IsHighlight, bool IsFeatured, bool IsSpecial, bool IsPopular, bool IsSale, bool IsOccasion, bool IsNew, int ProductCategoryID, string ProductCategoryIDStr, string DiscountType, decimal DiscountValue, int DiscountID, string Extra1, string Extra2, string Extra3, int BV, int PV, decimal Width, decimal Height, decimal Weight, string WeightUnit, string ColorName, string BrandName, decimal Price1, decimal Price2,decimal Price3,string ProductImage2,string TaxIDStr,decimal Margin1,decimal Margin2,string ProductCode,int UnitID,decimal OPrice,decimal OTax ,decimal OProfit,int OBV,int OPV,int OMinStock,int OQuantity,decimal NPrice,decimal NTax,decimal NProfit,int NBV,int NPV,int NMinStock,int NQuantity)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductTableAdapter())
            {
                dt = obj.AddEditProduct(ProductID, ProductName, ProductImage, ProductDesc, BrandID, MetaTitle, MetaKeywords, MetaDesc, Quantity, Hits, Tax, IsStock, IsHighlight, IsFeatured, IsSpecial, IsPopular, IsSale, IsOccasion, IsNew, ProductCategoryID, ProductCategoryIDStr, DiscountType, DiscountValue, DiscountID, Extra1, Extra2, Extra3, BV, PV, Width, Height, Weight, WeightUnit, ColorName, BrandName, Price1, Price2, Price3, ProductImage2, TaxIDStr, Margin1, Margin2, ProductCode, UnitID, OPrice, OTax, OProfit, OBV, OPV, OMinStock, OQuantity, NPrice, NTax, NProfit, NBV, NPV, NMinStock, NQuantity);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProductID"].ToString());
            return id;
        }

        public DataTable ManageProduct(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductTableAdapter())
            {
                dt = obj.ManageProduct(Action, ID);
            } return dt;
        }
    }
}

