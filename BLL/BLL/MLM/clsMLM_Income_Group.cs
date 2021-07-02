using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Income_Group
    {
        public DataTable ManageIncome_Group(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.MLM_Income_GroupTableAdapter obj = new DAL.DataSetMLMTableAdapters.MLM_Income_GroupTableAdapter())
            {
                dt = obj.ManageIncome_Group(Action, ID);
            } return dt;
        }
    }
}

