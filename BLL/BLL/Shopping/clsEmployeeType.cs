using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsEmployeeType
    {
        public int AddEditEmployeeType(int EmployeeTypeID,string EmployeeTypeName,string MenuIDStr)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblEmployeeTypeTableAdapter obj = new DAL.DataSet1TableAdapters.tblEmployeeTypeTableAdapter())
            {
                dt = obj.AddEditEmployeeType(EmployeeTypeID, EmployeeTypeName, MenuIDStr);
            }
            id = Convert.ToInt32(dt.Rows[0]["EmployeeTypeID"].ToString());
            return id;
        }

        public DataTable ManageEmployeeType(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblEmployeeTypeTableAdapter obj = new DAL.DataSet1TableAdapters.tblEmployeeTypeTableAdapter())
            {
                dt = obj.ManageEmployeeType(Action, ID);
            } return dt;
        }
    }
}

