using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_Coupan
    {
        public int AddEditCoupan(int CoupanID, string CoupanName, int VendorID, int CoupanCategoryID, string CoupanCode, string CoupanImage, decimal Price, string Discount, string RedeemOption, string Desc, int Qty, DateTime ValidFrom, DateTime ValidTo)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanTableAdapter())
            {
                dt = obj.AddEditCoupan(CoupanID, CoupanName, VendorID, CoupanCategoryID, CoupanCode, CoupanImage, Price, Discount, RedeemOption, Desc, Qty, ValidFrom, ValidTo);
            }
            id = Convert.ToInt32(dt.Rows[0]["CoupanID"].ToString());
            return id;
        }

        public DataTable ManageCoupan(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_CoupanTableAdapter())
            {
                dt = obj.ManageCoupan(Action, ID);
            } return dt;
        }
    }
}

