using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_TariffPlan
    {
        public int AddEditTariffPlan(int TariffPlanID, int OperatorID, int CircleID, string Category, string Price, string TalkTime, string Validity, string Description)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_TariffPlanTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_TariffPlanTableAdapter())
            {
                dt = obj.AddEditTariffPlan(TariffPlanID, OperatorID, CircleID, Category, Price, TalkTime, Validity, Description);
            }
            id = Convert.ToInt32(dt.Rows[0]["TariffPlanID"].ToString());
            return id;
        }

        public DataTable ManageTariffPlan(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_TariffPlanTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_TariffPlanTableAdapter())
            {
                dt = obj.ManageTariffPlan(Action, ID);
            } return dt;
        }
    }
}

