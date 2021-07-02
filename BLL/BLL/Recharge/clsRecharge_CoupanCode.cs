using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_CoupanCode
    {
        public int AddEditCoupanCode(int CoupanCodeID, int CoupanID, string CoupanCode, int HistoryID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanCodeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanCodeTableAdapter())
            {
                dt = obj.AddEditCoupanCode(CoupanCodeID, CoupanID, CoupanCode, HistoryID);
            }
            id = Convert.ToInt32(dt.Rows[0]["CoupanCodeID"].ToString());
            return id;
        }

        public DataTable ManageCoupanCode(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanCodeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanCodeTableAdapter())
            {
                dt = obj.ManageCoupanCode(Action, ID);
            } return dt;
        }
    }
}

