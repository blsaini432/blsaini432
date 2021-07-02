using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsOrderDetail
    {
        public int AddEditOrderDetail(int OrderDetailID, int OrderID, int ProductID, string ProductName, int ProductBV, int ProductPV, int ProductQuantity, decimal ProductPrice, string Discount1, string Discount2, string Extra1, string Extra2, string OrderDetailDesc)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblOrderDetailTableAdapter obj = new DAL.DataSet1TableAdapters.tblOrderDetailTableAdapter())
            {
                dt = obj.AddEditOrderDetail(OrderDetailID, OrderID, ProductID, ProductName, ProductBV, ProductPV, ProductQuantity, ProductPrice, Discount1, Discount2, Extra1, Extra2, OrderDetailDesc);
            }
            id = Convert.ToInt32(dt.Rows[0]["OrderDetailID"].ToString());
            return id;
        }

        public DataTable ManageOrderDetail(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblOrderDetailTableAdapter obj = new DAL.DataSet1TableAdapters.tblOrderDetailTableAdapter())
            {
                dt = obj.ManageOrderDetail(Action, ID);
            } return dt;
        }
    }
}

