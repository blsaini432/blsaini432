using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_Circle
    {
        public int AddEditCircle(int CircleID, string CircleName, string CircleCode)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CircleTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CircleTableAdapter())
            {
                dt = obj.AddEditCircle(CircleID, CircleName, CircleCode);
            }
            id = Convert.ToInt32(dt.Rows[0]["CircleID"].ToString());
            return id;
        }

        public DataTable ManageCircle(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CircleTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CircleTableAdapter())
            {
                dt = obj.ManageCircle(Action, ID);
            } return dt;
        }
    }
}

