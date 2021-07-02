using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_UtilityCharge
    {
        public int AddEditUtilityCharge(int UtilityChargeID, int ServiceTypeID, decimal MinValue, decimal MaxValue, decimal Margin, bool IsFlat)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_UtilityChargeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_UtilityChargeTableAdapter())
            {
                dt = obj.AddEditUtilityCharge(UtilityChargeID, ServiceTypeID, MinValue, MaxValue, Margin, IsFlat);
            }
            id = Convert.ToInt32(dt.Rows[0]["UtilityChargeID"].ToString());
            return id;
        }

        public DataTable ManageUtilityCharge(string Action, int ID, decimal Amount)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_UtilityChargeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_UtilityChargeTableAdapter())
            {
                dt = obj.ManageUtilityCharge(Action, ID, Amount);
            } return dt;
        }
    }
}

