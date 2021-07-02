using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_ConditionPool
    {
        public int AddEditConditionPool(int ConditionPoolID, int MemberCount, decimal Distribution, decimal GrossIncome)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionPoolTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionPoolTableAdapter())
            {
                dt = obj.AddEditConditionPool(ConditionPoolID, MemberCount, Distribution, GrossIncome);
            }
            id = Convert.ToInt32(dt.Rows[0]["ConditionPoolID"].ToString());
            return id;
        }

        public DataTable ManageConditionPool(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionPoolTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionPoolTableAdapter())
            {
                dt = obj.ManageConditionPool(Action, ID);
            } return dt;
        }
    }
}

