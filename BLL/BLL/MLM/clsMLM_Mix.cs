using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Mix
    {
        

        public DataTable GetAdminDashBoard(int MsrNo)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberMaster1TableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberMaster1TableAdapter())
            {
                dt = obj.GetAdminDashBoard(MsrNo);
            } return dt;
        }

        public DataTable GetMemberDashBoard(int flag, int MsrNo)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberMaster1TableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberMaster1TableAdapter())
            {
                dt = obj.GetMemberDashBoard(flag,MsrNo);
            } return dt;
        }
        public DataTable MLM_UniversalSearch(string Action,string Para1,string Para2,string Para3,int ID1,int ID2, int ID3)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberMaster1TableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberMaster1TableAdapter())
            {
                dt = obj.MLM_UniversalSearch(Action,Para1,Para2,Para3,ID1,ID2,ID3);
            } return dt;
        }
        public DataTable WalletToWallet(string Action, string FromMemberID, string ToMemberID, decimal Amount)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberMaster1TableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberMaster1TableAdapter())
            {
                dt = obj.WalletToWallet(Action, FromMemberID, ToMemberID, Amount);
            } return dt;
        }
    }
}

