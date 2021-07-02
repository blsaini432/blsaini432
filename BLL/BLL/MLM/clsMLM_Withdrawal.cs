using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Withdrawal
    {
        public int AddEditWithdrawal(int WithdrawalID, decimal Amount)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_WithdrawalTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_WithdrawalTableAdapter())
            {
                dt = obj.AddEditWithdrawal(WithdrawalID, Amount);
            }
            id = Convert.ToInt32(dt.Rows[0]["WithdrawalID"].ToString());
            return id;
        }

        public DataTable ManageWithdrawal(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_WithdrawalTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_WithdrawalTableAdapter())
            {
                dt = obj.ManageWithdrawal(Action, ID);
            } return dt;
        }
    }
}

