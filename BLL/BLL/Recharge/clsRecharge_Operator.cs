using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_Operator
    {
        public int AddEditOperator(int OperatorID, string OperatorName, string OperatorCode, int ServiceTypeID, int ActiveAPI)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_OperatorTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_OperatorTableAdapter())
            {
                dt = obj.AddEditOperator(OperatorID, OperatorName, OperatorCode, ServiceTypeID, ActiveAPI);
            }
            id = Convert.ToInt32(dt.Rows[0]["OperatorID"].ToString());
            return id;
        }

        public DataTable ManageOperator(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_OperatorTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_OperatorTableAdapter())
            {
                dt = obj.ManageOperator(Action, ID);
            } return dt;
        }
    }
}

