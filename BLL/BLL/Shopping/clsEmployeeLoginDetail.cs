using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsEmployeeLoginDetail
    {
        public int AddEditEmployeeLoginDetail(int EmployeeLoginDetailID,string LoginIP,int EmployeeID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblEmployeeLoginDetailTableAdapter obj = new DAL.DataSet1TableAdapters.tblEmployeeLoginDetailTableAdapter())
            {
                dt = obj.AddEditEmployeeLoginDetail(EmployeeLoginDetailID, LoginIP, EmployeeID);
            }
            id = Convert.ToInt32(dt.Rows[0]["EmployeeLoginDetailID"].ToString());
            return id;
        }

        public DataTable ManageEmployeeLoginDetail(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblEmployeeLoginDetailTableAdapter obj = new DAL.DataSet1TableAdapters.tblEmployeeLoginDetailTableAdapter())
            {
                dt = obj.ManageEmployeeLoginDetail(Action, ID);
            } return dt;
        }
    }
}

