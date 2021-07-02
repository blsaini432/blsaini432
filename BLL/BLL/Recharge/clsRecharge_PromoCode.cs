using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_PromoCode
    {
        public int AddEditPromoCode(int PromoCodeID, string PromoCode, decimal Amount, string Desc, DateTime ValidFrom, DateTime ValidTo, bool IsUsed, int UsedBy, DateTime UsedDate)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_PromoCodeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_PromoCodeTableAdapter())
            {
                dt = obj.AddEditPromoCode(PromoCodeID, PromoCode, Amount, Desc, ValidFrom, ValidTo, IsUsed, UsedBy, UsedDate);
            }
            id = Convert.ToInt32(dt.Rows[0]["PromoCodeID"].ToString());
            return id;
        }

        public DataTable ManagePromoCode(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_PromoCodeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_PromoCodeTableAdapter())
            {
                dt = obj.ManagePromoCode(Action, ID);
            } return dt;
        }
    }
}

