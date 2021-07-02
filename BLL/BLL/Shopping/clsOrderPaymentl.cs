using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsOrderPayment
    {
        public int AddEditOrderPayment(int OrderPaymentID, int OrderID,string PaymentMode,string Number,string OrderPaymentDesc,DateTime ApproveDate,bool IsApprove )
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblOrderPaymentTableAdapter obj = new DAL.DataSet1TableAdapters.tblOrderPaymentTableAdapter())
            {
                dt = obj.AddEditOrderPayment(OrderPaymentID, OrderID,PaymentMode,Number,OrderPaymentDesc,ApproveDate,IsApprove );
            }
            id = Convert.ToInt32(dt.Rows[0]["OrderPaymentID"].ToString());
            return id;
        }

        public DataTable ManageOrderPayment(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblOrderPaymentTableAdapter obj = new DAL.DataSet1TableAdapters.tblOrderPaymentTableAdapter())
            {
                dt = obj.ManageOrderPayment(Action, ID);
            } return dt;
        }
    }
}

