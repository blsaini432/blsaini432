using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Performance
    {
        public int AddEditPerformance(int PerformanceID, decimal LeftCount, decimal RightCount, string CalculationFlag, string MultiplierFlag, decimal Multiplier)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PerformanceTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PerformanceTableAdapter())
            {
                dt = obj.AddEditPerformance(PerformanceID, LeftCount, RightCount, CalculationFlag, MultiplierFlag, Multiplier);
            }
            id = Convert.ToInt32(dt.Rows[0]["PerformanceID"].ToString());
            return id;
        }

        public DataTable ManagePerformance(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PerformanceTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PerformanceTableAdapter())
            {
                dt = obj.ManagePerformance(Action, ID);
            } return dt;
        }
    }
}

