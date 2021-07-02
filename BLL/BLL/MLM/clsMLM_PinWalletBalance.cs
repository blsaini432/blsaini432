using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_PinWalletBalance
    {
        public int AddEditPinWalletBalance(int PinWalletBalanceID, int MsrNo, decimal Debit, decimal Credit)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinWalletBalanceTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinWalletBalanceTableAdapter())
            {
                dt = obj.AddEditPinWalletBalance(PinWalletBalanceID, MsrNo, Debit, Credit);
            }
            id = Convert.ToInt32(dt.Rows[0]["PinWalletBalanceID"].ToString());
            return id;
        }

        public DataTable ManagePinWalletBalance(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinWalletBalanceTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinWalletBalanceTableAdapter())
            {
                dt = obj.ManagePinWalletBalance(Action, ID);
            } return dt;
        }
    }
}

