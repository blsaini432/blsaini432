using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_IncomeType
    {
        public int AddEditIncomeType(int IncomeTypeID, string IncomeTypeName, string FlagName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_IncomeTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_IncomeTypeTableAdapter())
            {
                dt = obj.AddEditIncomeType(IncomeTypeID, IncomeTypeName, FlagName);
            }
            id = Convert.ToInt32(dt.Rows[0]["IncomeTypeID"].ToString());
            return id;
        }

        public DataTable ManageIncomeType(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_IncomeTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_IncomeTypeTableAdapter())
            {
                dt = obj.ManageIncomeType(Action, ID);
            } return dt;
        }
    }
}

