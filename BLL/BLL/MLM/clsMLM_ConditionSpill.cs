using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_ConditionSpill
    {
        public int AddEditConditionSpill(int ConditionSpillID, int LeftCount, int RightCount, decimal SpillAmount, string Flag)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionSpillTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionSpillTableAdapter())
            {
                dt = obj.AddEditConditionSpill(ConditionSpillID, LeftCount, RightCount, SpillAmount, Flag);
            }
            id = Convert.ToInt32(dt.Rows[0]["ConditionSpillID"].ToString());
            return id;
        }

        public DataTable ManageConditionSpill(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionSpillTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionSpillTableAdapter())
            {
                dt = obj.ManageConditionSpill(Action, ID);
            } return dt;
        }
    }
}

