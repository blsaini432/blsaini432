using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MemberPanCard
    {
        public int AddEditMemberPanCard(int MemberPanCardID, string PanNumber, string PanImage, int MsrNo, DateTime ApproveDate, string Status)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberPanCardTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberPanCardTableAdapter())
            {
                dt = obj.AddEditMemberPanCard(MemberPanCardID, PanNumber, PanImage, MsrNo, ApproveDate, Status);
            }
            id = Convert.ToInt32(dt.Rows[0]["MemberPanCardID"].ToString());
            return id;
        }

        public DataTable ManageMemberPanCard(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberPanCardTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberPanCardTableAdapter())
            {
                dt = obj.ManageMemberPanCard(Action, ID);
            } return dt;
        }
    }
}

