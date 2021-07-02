using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_ConditionPairMaster
    {
        public int AddEditConditionPairMaster(int ConditionPairMasterID, string ConditionPairMasterName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionPairMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionPairMasterTableAdapter())
            {
                dt = obj.AddEditConditionPairMaster(ConditionPairMasterID, ConditionPairMasterName);
            }
            id = Convert.ToInt32(dt.Rows[0]["ConditionPairMasterID"].ToString());
            return id;
        }

        public DataTable ManageConditionPairMaster(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionPairMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionPairMasterTableAdapter())
            {
                dt = obj.ManageConditionPairMaster(Action, ID);
            } return dt;
        }
    }
}

