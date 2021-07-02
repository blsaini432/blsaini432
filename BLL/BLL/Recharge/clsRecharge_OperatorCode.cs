using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_OperatorCode
    {
        public int AddEditOperatorCode(int OperatorCodeID, int OperatorID, int APIID, string OperatorCode, decimal Commission, bool CommissionIsFlat, decimal Surcharge, bool SurchargeIsFlat)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_OperatorCodeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_OperatorCodeTableAdapter())
            {
                dt = obj.AddEditOperatorCode(OperatorCodeID, OperatorID, APIID, OperatorCode, Commission, CommissionIsFlat, Surcharge, SurchargeIsFlat);
            }
            id = Convert.ToInt32(dt.Rows[0]["OperatorCodeID"].ToString());
            return id;
        }

        public DataTable ManageOperatorCode(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_OperatorCodeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_OperatorCodeTableAdapter())
            {
                dt = obj.ManageOperatorCode(Action, ID);
            } return dt;
        }
    }
}

