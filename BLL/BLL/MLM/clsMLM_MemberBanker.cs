using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MemberBanker
    {
        public int AddEditMemberBanker(int MemberBankerID, int BankerMasterID, string BankBranch, string AccountType, string AccountNumber, string IFSCCode, string BankDesc, int MsrNo, DateTime ApproveDate, string Status, bool IsApprove)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberBankerTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberBankerTableAdapter())
            {
                dt = obj.AddEditMemberBanker(MemberBankerID, BankerMasterID, BankBranch, AccountType, AccountNumber, IFSCCode, BankDesc, MsrNo, ApproveDate, Status, IsApprove);
            }
            id = Convert.ToInt32(dt.Rows[0]["MemberBankerID"].ToString());
            return id;
        }

        public DataTable ManageMemberBanker(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberBankerTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberBankerTableAdapter())
            {
                dt = obj.ManageMemberBanker(Action, ID);
            } return dt;
        }
    }
}

