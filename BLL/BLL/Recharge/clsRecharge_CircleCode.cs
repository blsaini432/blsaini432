using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_CircleCode
    {
        public int AddEditCircleCode(int CircleCodeID, int CircleID, int APIID, string CircleCode)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CircleCodeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CircleCodeTableAdapter())
            {
                dt = obj.AddEditCircleCode(CircleCodeID, CircleID, APIID, CircleCode);
            }
            id = Convert.ToInt32(dt.Rows[0]["CircleCodeID"].ToString());
            return id;
        }

        public DataTable ManageCircleCode(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CircleCodeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CircleCodeTableAdapter())
            {
                dt = obj.ManageCircleCode(Action, ID);
            } return dt;
        }
    }
}

