using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MemberMasterLoginDetail
    {
        public int AddEditMemberMasterLoginDetail(int MemberMasterLoginDetailID, string LoginIP, int MsrNo)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterLoginDetailTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterLoginDetailTableAdapter())
            {
                dt = obj.AddEditMemberMasterLoginDetail(MemberMasterLoginDetailID, LoginIP, MsrNo);
            }
            id = Convert.ToInt32(dt.Rows[0]["MemberMasterLoginDetailID"].ToString());
            return id;
        }

        public DataTable ManageMemberMasterLoginDetail(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterLoginDetailTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterLoginDetailTableAdapter())
            {
                dt = obj.ManageMemberMasterLoginDetail(Action, ID);
            } return dt;
        }
    }
}

