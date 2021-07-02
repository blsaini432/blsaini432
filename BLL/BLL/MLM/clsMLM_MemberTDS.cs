using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MemberTDS
    {
        public int AddEditMemberTDS(int MemberTDSID, int MsrNo, decimal Multiplier, decimal Amount, string Flag)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberTDSTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberTDSTableAdapter())
            {
                dt = obj.AddEditMemberTDS(MemberTDSID, MsrNo, Multiplier, Amount, Flag);
            }
            id = Convert.ToInt32(dt.Rows[0]["MemberTDSID"].ToString());
            return id;
        }

        public DataTable ManageMemberTDS(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberTDSTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberTDSTableAdapter())
            {
                dt = obj.ManageMemberTDS(Action, ID);
            } return dt;
        }
    }
}

