using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Income_Franchise_Level
    {
        public DataTable ManageIncome_Franchise_Level(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.MLM_Income_Franchise_LevelTableAdapter obj = new DAL.DataSetMLMTableAdapters.MLM_Income_Franchise_LevelTableAdapter())
            {
                dt = obj.ManageIncome_Franchise_Level(Action, ID);
            } return dt;
        }
    }
}

