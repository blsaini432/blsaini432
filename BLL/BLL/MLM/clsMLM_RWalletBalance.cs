using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL.MLM
{
    public class clsMLM_RWalletBalance
    {
        public int AddEditRWalletBalance(int RWalletBalanceID, int MsrNo, decimal Debit, decimal Credit)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_RWalletBalanceTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_RWalletBalanceTableAdapter())
            {
                dt = obj.AddEditRWalletBalance(RWalletBalanceID, MsrNo, Debit, Credit);
            }
            id = Convert.ToInt32(dt.Rows[0]["RWalletBalanceID"].ToString());
            return id;
        }

        public DataTable ManageRWalletBalance(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_RWalletBalanceTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_RWalletBalanceTableAdapter())
            {
                dt = obj.ManageRWalletBalance(Action, ID);
            } return dt;
        }
    }
}
