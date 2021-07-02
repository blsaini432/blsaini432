using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_WalletBinaryDetail
    {
        public int AddEditWalletBinaryDetail(int WalletBinaryDetailID, int MsrNo, decimal XPV, decimal YPV, decimal PaidX, decimal PaidY, decimal CarryX, decimal CarryY, decimal Amount, decimal Deduction, decimal NetAmount, int IsLast, string ClosingType, DateTime ClosingDate, int IncomePayoutTypeID, decimal XAmount, decimal YAmount, decimal MatchAmount, bool IsPaid)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_WalletBinaryDetailTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_WalletBinaryDetailTableAdapter())
            {
                dt = obj.AddEditWalletBinaryDetail(WalletBinaryDetailID, MsrNo, XPV, YPV, PaidX, PaidY, CarryX, CarryY, Amount, Deduction, NetAmount, IsLast, ClosingType, ClosingDate, IncomePayoutTypeID, XAmount, YAmount, MatchAmount, IsPaid);
            }
            id = Convert.ToInt32(dt.Rows[0]["WalletBinaryDetailID"].ToString());
            return id;
        }

        public DataTable ManageWalletBinaryDetail(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_WalletBinaryDetailTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_WalletBinaryDetailTableAdapter())
            {
                dt = obj.ManageWalletBinaryDetail(Action, ID);
            } return dt;
        }
    }
}

