using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_Earning
    {
        public int AddEditEarning(int EarningID, int MsrNo, int HistoryID, decimal Amount, int ByMsrNo)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_EarningTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_EarningTableAdapter())
            {
                dt = obj.AddEditEarning(EarningID, MsrNo, HistoryID, Amount, ByMsrNo);
            }
            id = Convert.ToInt32(dt.Rows[0]["EarningID"].ToString());
            return id;
        }

        public DataTable ManageEarning(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_EarningTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_EarningTableAdapter())
            {
                dt = obj.ManageEarning(Action, ID);
            } return dt;
        }
    }
}

