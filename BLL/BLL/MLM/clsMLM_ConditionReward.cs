using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_ConditionReward
    {
        public int AddEditConditionReward(int ConditionRewardID, string ConditionRewardName, bool IsUnitBased, bool IsNext)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionRewardTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionRewardTableAdapter())
            {
                dt = obj.AddEditConditionReward(ConditionRewardID, ConditionRewardName, IsUnitBased, IsNext);
            }
            id = Convert.ToInt32(dt.Rows[0]["ConditionRewardID"].ToString());
            return id;
        }

        public DataTable ManageConditionReward(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionRewardTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionRewardTableAdapter())
            {
                dt = obj.ManageConditionReward(Action, ID);
            } return dt;
        }
    }
}

