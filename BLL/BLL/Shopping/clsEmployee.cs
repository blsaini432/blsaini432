using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsEmployee
    {
        public int AddEditEmployee(int EmployeeID,int EmployeeTypeID,string EmployeeName,string EmployeeDesc,string EmployeeImage,string Email,int Age,string LoginID,string Password,string Mobile,string STDCode,string Ladline,string Address,string RegisterSources,string CompanyName,string BranchName,string MenuIDStr,string LastLoginIP,DateTime LastLoginDate)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblEmployeeTableAdapter obj = new DAL.DataSet1TableAdapters.tblEmployeeTableAdapter())
            {
                dt = obj.AddEditEmployee(EmployeeID, EmployeeTypeID, EmployeeName, EmployeeDesc, EmployeeImage, Email, Age, LoginID, Password, Mobile, STDCode, Ladline, Address, RegisterSources, CompanyName, BranchName, MenuIDStr, LastLoginIP, LastLoginDate);
            }
            id = Convert.ToInt32(dt.Rows[0]["EmployeeID"].ToString());
            return id;
        }

        public DataTable ManageEmployee(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblEmployeeTableAdapter obj = new DAL.DataSet1TableAdapters.tblEmployeeTableAdapter())
            {
                dt = obj.ManageEmployee(Action, ID);
            } return dt;
        }

       
    }
}

