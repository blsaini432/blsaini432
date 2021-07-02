using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsTSS_Dept
    {
        public int AddEditDept(int DeptID, string DeptName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetTSSTableAdapters.tblTSS_DeptTableAdapter obj = new DAL.DataSetTSSTableAdapters.tblTSS_DeptTableAdapter())
            {
                dt = obj.AddEditDept(DeptID, DeptName);
            }
            id = Convert.ToInt32(dt.Rows[0]["DeptID"].ToString());
            return id;
        }

        public DataTable ManageDept(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetTSSTableAdapters.tblTSS_DeptTableAdapter obj = new DAL.DataSetTSSTableAdapters.tblTSS_DeptTableAdapter())
            {
                dt = obj.ManageDept(Action, ID);
            } return dt;
        }
    }
}

