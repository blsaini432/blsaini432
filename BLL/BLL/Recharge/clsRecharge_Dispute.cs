using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_Dispute
    {
        public int AddEditDispute(int DisputeID, int HistoryID, string Message, bool IsAdmin)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_DisputeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_DisputeTableAdapter())
            {
                dt = obj.AddEditDispute(DisputeID, HistoryID, Message, IsAdmin);
            }
            id = Convert.ToInt32(dt.Rows[0]["DisputeID"].ToString());
            return id;
        }

        public DataTable ManageDispute(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_DisputeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_DisputeTableAdapter())
            {
                dt = obj.ManageDispute(Action, ID);
            } return dt;
        }
    }
}

