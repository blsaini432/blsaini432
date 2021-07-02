using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_CapTier1Tier2
    {
        public int AddEditCapTier1Tier2(int CapTier1Tier2ID, decimal Tier1CapAmount, decimal Tier2CapAmount)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_CapTier1Tier2TableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_CapTier1Tier2TableAdapter())
            {
                dt = obj.AddEditCapTier1Tier2(CapTier1Tier2ID, Tier1CapAmount, Tier2CapAmount);
            }
            id = Convert.ToInt32(dt.Rows[0]["CapTier1Tier2ID"].ToString());
            return id;
        }

        public DataTable ManageCapTier1Tier2(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_CapTier1Tier2TableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_CapTier1Tier2TableAdapter())
            {
                dt = obj.ManageCapTier1Tier2(Action, ID);
            } return dt;
        }
    }
}

