using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsCustomerLoginDetail
    {
        public int AddEditCustomerLoginDetail(int CustomerLoginDetailID,string LoginIP,int CustomerID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCustomerLoginDetailTableAdapter obj = new DAL.DataSet1TableAdapters.tblCustomerLoginDetailTableAdapter())
            {
                dt = obj.AddEditCustomerLoginDetail(CustomerLoginDetailID, LoginIP, CustomerID);
            }
            id = Convert.ToInt32(dt.Rows[0]["CustomerLoginDetailID"].ToString());
            return id;
        }

        public DataTable ManageCustomerLoginDetail(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCustomerLoginDetailTableAdapter obj = new DAL.DataSet1TableAdapters.tblCustomerLoginDetailTableAdapter())
            {
                dt = obj.ManageCustomerLoginDetail(Action, ID);
            } return dt;
        }
    }
}

