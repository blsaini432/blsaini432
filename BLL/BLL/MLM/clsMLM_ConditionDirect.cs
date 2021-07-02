using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_ConditionDirect
    {
        public int AddEditConditionDirect(int ConditionDirectID, string ConditionDirectName, bool IsLimit, int Limit, bool IsPackage, decimal Multiplier)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionDirectTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionDirectTableAdapter())
            {
                dt = obj.AddEditConditionDirect(ConditionDirectID, ConditionDirectName, IsLimit, Limit, IsPackage, Multiplier);
            }
            id = Convert.ToInt32(dt.Rows[0]["ConditionDirectID"].ToString());
            return id;
        }

        public DataTable ManageConditionDirect(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionDirectTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionDirectTableAdapter())
            {
                dt = obj.ManageConditionDirect(Action, ID);
            } return dt;
        }
    }
}

