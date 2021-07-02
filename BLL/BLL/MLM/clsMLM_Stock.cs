using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Stock
    {
        public int AddEditStock(int StockID, int ProductID, int FranchiseID, int Quantity, string StockType, int RequestNo)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_StockTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_StockTableAdapter())
            {
                dt = obj.AddEditStock(StockID, ProductID, FranchiseID, Quantity, StockType, RequestNo);
            }
            id = Convert.ToInt32(dt.Rows[0]["StockID"].ToString());
            return id;
        }

        public DataTable ManageStock(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_StockTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_StockTableAdapter())
            {
                dt = obj.ManageStock(Action, ID);
            } return dt;
        }
    }
}

