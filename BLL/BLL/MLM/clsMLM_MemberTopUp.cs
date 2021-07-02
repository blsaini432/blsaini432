using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MemberTopUp
    {
        public int AddEditMemberTopUp(int MemberTopUpID, int MsrNo, int PackageID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberTopUpTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberTopUpTableAdapter())
            {
                dt = obj.AddEditMemberTopUp(MemberTopUpID, MsrNo, PackageID);
            }
            id = Convert.ToInt32(dt.Rows[0]["MemberTopUpID"].ToString());
            return id;
        }

        public DataTable ManageMemberTopUp(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberTopUpTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberTopUpTableAdapter())
            {
                dt = obj.ManageMemberTopUp(Action, ID);
            } return dt;
        }
    }
}

