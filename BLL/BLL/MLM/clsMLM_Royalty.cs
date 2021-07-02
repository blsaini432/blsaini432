using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Royalty
    {
        public int AddEditRoyalty(int RoyaltyID, string RoyaltyName, string PrintName, decimal LeftCount, decimal RightCount, int FreshPair, string CalculationFlag, string MultiplierFlag, decimal Multiplier)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_RoyaltyTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_RoyaltyTableAdapter())
            {
                dt = obj.AddEditRoyalty(RoyaltyID, RoyaltyName, PrintName, LeftCount, RightCount, FreshPair, CalculationFlag, MultiplierFlag, Multiplier);
            }
            id = Convert.ToInt32(dt.Rows[0]["RoyaltyID"].ToString());
            return id;
        }

        public DataTable ManageRoyalty(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_RoyaltyTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_RoyaltyTableAdapter())
            {
                dt = obj.ManageRoyalty(Action, ID);
            } return dt;
        }
    }
}

