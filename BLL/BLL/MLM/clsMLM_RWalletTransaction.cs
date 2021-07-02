using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL.MLM
{
    public class clsMLM_RWalletTransaction
    {
        public int AddEditEWalletTransaction(int RWalletTransactionID, int MsrNo, decimal Amount, decimal Balance, string Factor, string Narration)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_RWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_RWalletTransactionTableAdapter())
            {
                dt = obj.AddEditRWalletTransaction(RWalletTransactionID, MsrNo, Amount, Balance, Factor, Narration);
            }
            id = Convert.ToInt32(dt.Rows[0]["RWalletTransactionID"].ToString());
            return id;
        }

        public DataTable ManageRWalletTransaction(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_RWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_RWalletTransactionTableAdapter())
            {
                dt = obj.ManageRWalletTransaction(Action, ID);
            } return dt;
        }
        public DataTable RWalletTransaction(string MemberID, decimal Amount, string Factor, string Narration)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_RWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_RWalletTransactionTableAdapter())
            {
                dt = obj.RWalletTransaction(MemberID, Amount, Factor, Narration);
            }
            return dt;
        }
    }
}