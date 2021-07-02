using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_PurchaseOrderDetail
    {
        public int AddEditPurchaseOrderDetail(int PurchaseOrderDetailID,int Quantity,decimal Price,int ProductID,string ProductCode,decimal TaxCharge,decimal OtherCharge,int UnitID,int PurchaseOrderID,string PurchaseOrderDetailDesc)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_PurchaseOrderDetailTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_PurchaseOrderDetailTableAdapter())
            {
                dt = obj.AddEditPurchaseOrderDetail(PurchaseOrderDetailID,Quantity, Price, ProductID, ProductCode, TaxCharge, OtherCharge, UnitID, PurchaseOrderID, PurchaseOrderDetailDesc);
            }
            id = Convert.ToInt32(dt.Rows[0]["PurchaseOrderDetailID"].ToString());
            return id;
        }

        public DataTable ManagePurchaseOrderDetail(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_PurchaseOrderDetailTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_PurchaseOrderDetailTableAdapter())
            {
                dt = obj.ManagePurchaseOrderDetail(Action, ID);
            } return dt;
        }
    }
}

