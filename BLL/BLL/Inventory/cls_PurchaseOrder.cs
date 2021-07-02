using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_PurchaseOrder
    {
        public int AddEditPurchaseOrder(int PurchaseOrderID,string PurchaseOrderName,string PurchaseOrderDesc,DateTime PurchaseOrderDate,decimal ShippingCharge,decimal TransportCharge,decimal OtherCharge,decimal TotalProductAmount,decimal TotalProductTaxAmount,decimal GrandTotal,string GrandTotalAmountText,int SupplerID,int EmployeeID,string PurchaseOrderStatus,bool IsDone)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_PurchaseOrderTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_PurchaseOrderTableAdapter())
            {
                dt = obj.AddEditPurchaseOrder(PurchaseOrderID,PurchaseOrderName, PurchaseOrderDesc, PurchaseOrderDate, ShippingCharge, TransportCharge, OtherCharge, TotalProductAmount, TotalProductTaxAmount, GrandTotal, GrandTotalAmountText, SupplerID, EmployeeID, PurchaseOrderStatus, IsDone);
            }
            id = Convert.ToInt32(dt.Rows[0]["PurchaseOrderID"].ToString());
            return id;
        }

        public DataTable ManagePurchaseOrder(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_PurchaseOrderTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_PurchaseOrderTableAdapter())
            {
                dt = obj.ManagePurchaseOrder(Action, ID);
            } return dt;
        }
    }
}

