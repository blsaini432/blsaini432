using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_OrderMsg
    {
        public int AddEditOrderMsg(int OrderMsgID,int ProductID,string ProductIDStr,string Subject,string Message,int FromEmployeeID,int ToEmployeeID,string FromEmployeeEmail,string ToEmployeeEmail,string StoreOrderType,string Comment,string OrderMsgStatus,bool IsDone)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_OrderMsgTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_OrderMsgTableAdapter())
            {
                dt = obj.AddEditOrderMsg(OrderMsgID,ProductID, ProductIDStr, Subject, Message, FromEmployeeID, ToEmployeeID, FromEmployeeEmail, ToEmployeeEmail, StoreOrderType, Comment, OrderMsgStatus, IsDone);
            }
            id = Convert.ToInt32(dt.Rows[0]["OrderMsgID"].ToString());
            return id;
        }

        public DataTable ManageOrderMsg(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_OrderMsgTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_OrderMsgTableAdapter())
            {
                dt = obj.ManageOrderMsg(Action, ID);
            } return dt;
        }
    }
}

