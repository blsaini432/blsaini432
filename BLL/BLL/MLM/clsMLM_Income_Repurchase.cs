using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Income_Repurchase
    {
        public DataTable ManageIncome_Repurchase(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.MLM_Income_RepurchaseTableAdapter obj = new DAL.DataSetMLMTableAdapters.MLM_Income_RepurchaseTableAdapter())
            {
                dt = obj.ManageIncome_Repurchase(Action, ID);
            } return dt;
        }
    }
}

