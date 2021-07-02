using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MemberNominee
    {
        public int AddEditMemberNominee(int MemberNomineeID, int MsrNo, string Title, string FirstName, string MiddleName, string LastName, DateTime DOB, int Age, string Relation, string Address, string MemberNomineeDesc)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberNomineeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberNomineeTableAdapter())
            {
                dt = obj.AddEditMemberNominee(MemberNomineeID, MsrNo, Title, FirstName, MiddleName, LastName, DOB, Age, Relation, Address, MemberNomineeDesc);
            }
            id = Convert.ToInt32(dt.Rows[0]["MemberNomineeID"].ToString());
            return id;
        }

        public DataTable ManageMemberNominee(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberNomineeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberNomineeTableAdapter())
            {
                dt = obj.ManageMemberNominee(Action, ID);
            } return dt;
        }
    }
}

