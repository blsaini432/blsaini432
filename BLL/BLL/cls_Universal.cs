using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_Universal
    {
        public DataTable ManageTables()
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet3TableAdapters.tblCustomerTableAdapter obj = new DAL.DataSet3TableAdapters.tblCustomerTableAdapter())
            {
                dt = obj.UniversalGetTables();
            } return dt;
        }
        public DataTable ManageColumns(string TableName)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet3TableAdapters.tblCustomerTableAdapter obj = new DAL.DataSet3TableAdapters.tblCustomerTableAdapter())
            {
                dt = obj.UniversalGetColumns(TableName);
            } return dt;
        }

        public void UpdateLastLogin(string Action, int ID, string LastLoginIP)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet3TableAdapters.tblCustomerTableAdapter obj = new DAL.DataSet3TableAdapters.tblCustomerTableAdapter())
            {
                dt = obj.UpdateLastLogin(Action,ID, LastLoginIP);
            }
        }
        public int UpdatePassword(string Action, int ID, string NewPassword, string OldPassword)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet3TableAdapters.tblCustomerTableAdapter obj = new DAL.DataSet3TableAdapters.tblCustomerTableAdapter())
            {
                dt = obj.UpdatePassword(Action,ID, NewPassword, OldPassword);
            }
            id = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
            return id;
        }


        public DataTable UniversalLogin(string Action, string LoginID, string Password)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet3TableAdapters.tblCustomerTableAdapter obj = new DAL.DataSet3TableAdapters.tblCustomerTableAdapter())
            {
                dt = obj.UniversalLogin(Action,LoginID, Password);
            } return dt;
        }
        public DataTable UniversalForgotPassword(string Action, string LoginID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet3TableAdapters.tblCustomerTableAdapter obj = new DAL.DataSet3TableAdapters.tblCustomerTableAdapter())
            {
                dt = obj.UniversalForgotPassword(Action,LoginID);
            } return dt;
        }

        public DataTable Income_ShowReward()
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet3TableAdapters.tblCustomerTableAdapter obj = new DAL.DataSet3TableAdapters.tblCustomerTableAdapter())
            {
                dt = obj.Income_ShowReward();
            } return dt;
        }
       
    }
}

