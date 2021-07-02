using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_IncomePayout
    {
        public int AddEditIncomePayout(int IncomePayoutID, int MsrNo, string MemberID, decimal BinaryIncome, decimal ReferralIncome, decimal SponsorAutoPoolIncome, decimal ClubIncome, decimal RoyaltyIncome, decimal TotalAmount, decimal TDS, decimal CompanyCharge, decimal AdminCharge, decimal NetAmount, int Closing, int IncomePayoutTypeID, decimal BinaryTier2, decimal ReferralTier2, bool IsPaid, bool IsCapping)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_IncomePayoutTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_IncomePayoutTableAdapter())
            {
                dt = obj.AddEditIncomePayout(IncomePayoutID, MsrNo, MemberID, BinaryIncome, ReferralIncome, SponsorAutoPoolIncome, ClubIncome, RoyaltyIncome, TotalAmount, TDS, CompanyCharge, AdminCharge, NetAmount, Closing, IncomePayoutTypeID, BinaryTier2, ReferralTier2, IsPaid, IsCapping);
            }
            id = Convert.ToInt32(dt.Rows[0]["IncomePayoutID"].ToString());
            return id;
        }

        public DataTable ManageIncomePayout(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_IncomePayoutTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_IncomePayoutTableAdapter())
            {
                dt = obj.ManageIncomePayout(Action, ID);
            } return dt;
        }
    }
}

