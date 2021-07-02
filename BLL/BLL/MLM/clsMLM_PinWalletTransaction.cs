using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_PinWalletTransaction
    {
        public int AddEditPinWalletTransaction(int PinWalletTransactionID, int MsrNo, decimal Amount, decimal Balance, string Factor, string Narration)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinWalletTransactionTableAdapter())
            {
                dt = obj.AddEditPinWalletTransaction(PinWalletTransactionID, MsrNo, Amount, Balance, Factor, Narration);
            }
            id = Convert.ToInt32(dt.Rows[0]["PinWalletTransactionID"].ToString());
            return id;
        }

        public DataTable ManagePinWalletTransaction(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinWalletTransactionTableAdapter())
            {
                dt = obj.ManagePinWalletTransaction(Action, ID);
            } return dt;
        }


        public void PinWalletTransaction(string MemberID, decimal Amount, string Factor, string Narration)
        {
           
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinWalletTransactionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinWalletTransactionTableAdapter())
            {
              obj.PinWalletTransaction(MemberID,Amount, Factor, Narration);
            }
          
        }
    }
}

