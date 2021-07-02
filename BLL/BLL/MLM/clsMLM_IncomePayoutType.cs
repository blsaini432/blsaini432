using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_IncomePayoutType
    {
        public int AddEditIncomePayoutType(int IncomePayoutTypeID, string IncomePayoutTypeName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_IncomePayoutTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_IncomePayoutTypeTableAdapter())
            {
                dt = obj.AddEditIncomePayoutType(IncomePayoutTypeID, IncomePayoutTypeName);
            }
            id = Convert.ToInt32(dt.Rows[0]["IncomePayoutTypeID"].ToString());
            return id;
        }

        public DataTable ManageIncomePayoutType(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_IncomePayoutTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_IncomePayoutTypeTableAdapter())
            {
                dt = obj.ManageIncomePayoutType(Action, ID);
            } return dt;
        }
    }
}

