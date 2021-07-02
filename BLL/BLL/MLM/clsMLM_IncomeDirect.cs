using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_IncomeDirect
    {
        public int AddEditIncomeDirect(int IncomeDirectID, int MsrNo, int DueToMsrNo, decimal Amount, int IncomePayoutTypeID, bool IsPaid)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_IncomeDirectTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_IncomeDirectTableAdapter())
            {
                dt = obj.AddEditIncomeDirect(IncomeDirectID, MsrNo, DueToMsrNo, Amount, IncomePayoutTypeID, IsPaid);
            }
            id = Convert.ToInt32(dt.Rows[0]["IncomeDirectID"].ToString());
            return id;
        }

        public DataTable ManageIncomeDirect(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_IncomeDirectTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_IncomeDirectTableAdapter())
            {
                dt = obj.ManageIncomeDirect(Action, ID);
            } return dt;
        }
    }
}

