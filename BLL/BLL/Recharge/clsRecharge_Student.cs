using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_Student
    {
        public int AddEditStudent(int StudentID, int MsrNo, string Name, string Email, string Mobile, string Qualification, string Password, string Package, DateTime ValidUpto, string Status)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblStudentTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblStudentTableAdapter())
            {
                dt = obj.AddEditStudent(StudentID, MsrNo, Name, Email, Mobile, Qualification, Password, Package, ValidUpto, Status);
            }
            id = Convert.ToInt32(dt.Rows[0]["StudentID"].ToString());
            return id;
        }

        public DataTable ManageStudent(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblStudentTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblStudentTableAdapter())
            {
                dt = obj.ManageStudent(Action, ID);
            } return dt;
        }
    }
}

