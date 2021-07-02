using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Deduction
    {
        public int AddEditDeduction(int DeductionID, string DeductionName, decimal Multiplier, string Factor)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_DeductionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_DeductionTableAdapter())
            {
                dt = obj.AddEditDeduction(DeductionID, DeductionName, Multiplier, Factor);
            }
            id = Convert.ToInt32(dt.Rows[0]["DeductionID"].ToString());
            return id;
        }

        public DataTable ManageDeduction(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_DeductionTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_DeductionTableAdapter())
            {
                dt = obj.ManageDeduction(Action, ID);
            } return dt;
        }
    }
}

