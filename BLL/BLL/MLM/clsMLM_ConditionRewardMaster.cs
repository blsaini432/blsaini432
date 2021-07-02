using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_ConditionRewardMaster
    {
        public int AddEditConditionRewardMaster(int ConditionRewardMasterID, string ConditionRewardMasterName, string Designation, decimal LeftCount, decimal RightCount, decimal Amount, bool IsTimeLimit, int InDays)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionRewardMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionRewardMasterTableAdapter())
            {
                dt = obj.AddEditConditionRewardMaster(ConditionRewardMasterID, ConditionRewardMasterName, Designation, LeftCount, RightCount, Amount, IsTimeLimit, InDays);
            }
            id = Convert.ToInt32(dt.Rows[0]["ConditionRewardMasterID"].ToString());
            return id;
        }

        public DataTable ManageConditionRewardMaster(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionRewardMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionRewardMasterTableAdapter())
            {
                dt = obj.ManageConditionRewardMaster(Action, ID);
            } return dt;
        }
    }
}

