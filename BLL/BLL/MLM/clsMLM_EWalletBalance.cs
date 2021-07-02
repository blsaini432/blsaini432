using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_EWalletBalance
    {
        public int AddEditEWalletBalance(int EWalletBalanceID, int MsrNo, decimal Debit, decimal Credit)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_EWalletBalanceTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_EWalletBalanceTableAdapter())
            {
                dt = obj.AddEditEWalletBalance(EWalletBalanceID, MsrNo, Debit, Credit);
            }
            id = Convert.ToInt32(dt.Rows[0]["EWalletBalanceID"].ToString());
            return id;
        }

        public DataTable ManageEWalletBalance(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_EWalletBalanceTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_EWalletBalanceTableAdapter())
            {
                dt = obj.ManageEWalletBalance(Action, ID);
            } return dt;
        }
    }
}

