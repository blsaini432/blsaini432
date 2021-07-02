using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsOrder
    {
        public int AddEditOrder(int OrderID,int CustomerID,string TransactionID,string OrderStatus,decimal ProductAmount,string ShippingType,decimal ShippingAmount,decimal CashVoucherDiscount,decimal RewardPointDiscount,string Discount1,string Discount2,decimal ByPaypal,decimal ByEWallet,decimal ByCash,decimal TotalPay,int TotalBV,int TotalPV,string OrderDesc,string ShippingDesc,string ReceiverDesc,string OrderRemark,string Extra1,string Extra2,bool IsApproved,bool IsDone,string IPAddress,string OrderFor,string StockType)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblOrderTableAdapter obj = new DAL.DataSet1TableAdapters.tblOrderTableAdapter())
            {
                dt = obj.AddEditOrder(OrderID, CustomerID, TransactionID, OrderStatus, ProductAmount, ShippingType, ShippingAmount, CashVoucherDiscount, RewardPointDiscount, Discount1, Discount2, ByPaypal, ByEWallet, ByCash, TotalPay, TotalBV, TotalPV, OrderDesc, ShippingDesc, ReceiverDesc, OrderRemark, Extra1, Extra2, IsApproved, IsDone, IPAddress,OrderFor,StockType);
            }
            id = Convert.ToInt32(dt.Rows[0]["OrderID"].ToString());
            return id;
        }

        public DataTable ManageOrder(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblOrderTableAdapter obj = new DAL.DataSet1TableAdapters.tblOrderTableAdapter())
            {
                dt = obj.ManageOrder(Action, ID);
            } return dt;
        }
    }
}

