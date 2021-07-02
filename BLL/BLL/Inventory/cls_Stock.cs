using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_Stock
    {
        public int AddEditStock(int StockID,int SupplierID,int WareHouseID,int ProductCategoryID,string ProductCategoryIDStr,int ProductID,int Quantity,int MinStock,decimal Price,string StockDesc,string Extra1,string Extra2,bool IsApproved,bool IsStock)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_StockTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_StockTableAdapter())
            {
                dt = obj.AddEditStock(StockID,SupplierID, WareHouseID, ProductCategoryID, ProductCategoryIDStr, ProductID, Quantity, MinStock, Price, StockDesc, Extra1, Extra2, IsApproved, IsStock);
            }
            id = Convert.ToInt32(dt.Rows[0]["StockID"].ToString());
            return id;
        }

        public DataTable ManageStock(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_StockTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_StockTableAdapter())
            {
                dt = obj.ManageStock(Action, ID);
            } return dt;
        }
    }
}

