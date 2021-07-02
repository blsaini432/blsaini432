using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_ConditionPair
    {
        public int AddEditConditionPair(int ConditionPairID, int ConditionPairMasterID, decimal BinaryRate, string PlanCalculation, decimal CappingPair, int CappingTime, int TotalDirect, int LeftPV, int RightPV, bool IsLeftRight, bool IsCapping, bool IsDirect)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionPairTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionPairTableAdapter())
            {
                dt = obj.AddEditConditionPair(ConditionPairID, ConditionPairMasterID, BinaryRate, PlanCalculation, CappingPair, CappingTime, TotalDirect, LeftPV, RightPV, IsLeftRight, IsCapping, IsDirect);
            }
            id = Convert.ToInt32(dt.Rows[0]["ConditionPairID"].ToString());
            return id;
        }

        public DataTable ManageConditionPair(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_ConditionPairTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_ConditionPairTableAdapter())
            {
                dt = obj.ManageConditionPair(Action, ID);
            } return dt;
        }
    }
}

