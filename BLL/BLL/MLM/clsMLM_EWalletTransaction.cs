using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_EWalletTransaction
    {
        public int AddEditEWalletTransaction(int EWalletTransactionID, int MsrNo, decimal Amount, decimal Balance, string Factor, string Narration)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_EWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_EWalletTransactionTableAdapter())
            {
                dt = obj.AddEditEWalletTransaction(EWalletTransactionID, MsrNo, Amount, Balance, Factor, Narration);
            }
            id = Convert.ToInt32(dt.Rows[0]["EWalletTransactionID"].ToString());
            return id;
        }

        public DataTable ManageEWalletTransaction(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_EWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_EWalletTransactionTableAdapter())
            {
                dt = obj.ManageEWalletTransaction(Action, ID);
            } return dt;
        }
        public void EWalletTransaction(string MemberID, decimal Amount, string Factor, string Narration)
        {

            using (DAL.DataSetMLMTableAdapters.tblMLM_EWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_EWalletTransactionTableAdapter())
            {
                obj.EWalletTransaction(MemberID, Amount, Factor, Narration);
            }

        }
        public DataTable EWalletTransaction_Ezulix(string MemberID, decimal Amount, string Factor, string Narration)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_EWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_EWalletTransactionTableAdapter())
            {
                dt = obj.EWalletTransaction(MemberID, Amount, Factor, Narration);
            }
            return dt;
        }
    }
}

