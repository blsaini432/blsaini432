using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_CoupanCategory
    {
        public int AddEditCoupanCategory(int CoupanCategoryID, string CoupanCategoryName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanCategoryTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanCategoryTableAdapter())
            {
                dt = obj.AddEditCoupanCategory(CoupanCategoryID, CoupanCategoryName);
            }
            id = Convert.ToInt32(dt.Rows[0]["CoupanCategoryID"].ToString());
            return id;
        }

        public DataTable ManageCoupanCategory(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanCategoryTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanCategoryTableAdapter())
            {
                dt = obj.ManageCoupanCategory(Action, ID);
            } return dt;
        }
    }
}

