using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_History
    {
        public int AddEditHistory(int HistoryID, int MsrNo, string MobileNo, string caNumber, decimal RechargeAmount, int OperatorID, int CircleID, string TransID, string Cycle, string DueDate, string Status)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_HistoryTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_HistoryTableAdapter())
            {
                dt = obj.AddEditHistory(HistoryID, MsrNo, MobileNo, caNumber, RechargeAmount, OperatorID, CircleID, TransID, Cycle, DueDate, Status);
            }
            id = Convert.ToInt32(dt.Rows[0]["HistoryID"].ToString());
            return id;
        }

        public DataTable UpdateHistory(string Action, int HistoryID, int MsrNo, decimal CoupanAmount, decimal UtilityChargeAmount, decimal DiscountAmount, decimal NetAmount, string CoupanCodeStr, string Response, string Status, string APITransID, string APIErrorCode, string APIMessage)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_HistoryTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_HistoryTableAdapter())
            {
                dt = obj.UpdateHistory(Action, HistoryID, MsrNo, CoupanAmount, UtilityChargeAmount, DiscountAmount, NetAmount, CoupanCodeStr, Response, Status, APITransID, APIErrorCode, APIMessage);
            }
            return dt;
        }

        public DataTable ManageHistory(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_HistoryTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_HistoryTableAdapter())
            {
                dt = obj.ManageHistory(Action, ID);
            } return dt;
        }
    }
}

