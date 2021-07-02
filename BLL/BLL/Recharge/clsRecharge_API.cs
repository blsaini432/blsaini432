using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_API
    {
        public int AddEditAPI(int APIID, string APIName, string URL, string Splitter, string prm1, string prm1val, string prm2, string prm2val, string prm3, string prm4, string prm5, string prm6, string prm7, string prm8, string prm9, string prm9val, string prm10, string prm10val, string TxIDPosition, string StatusPosition, string Success, string Failed, string Pending, string OperatorRefPosition, string ErrorCodePosition, string BalanceURL, string B_prm1, string B_prm1val, string B_prm2, string B_prm2val, string B_prm3, string B_prm3val, string B_prm4, string B_prm4val, string B_BalancePosition, string StatusURL, string S_prm1, string S_prm1val, string S_prm2, string S_prm2val, string S_prm3, string S_prm4, string S_StatusPosition)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_APITableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_APITableAdapter())
            {
                dt = obj.AddEditAPI(APIID, APIName, URL, Splitter, prm1, prm1val, prm2, prm2val, prm3, prm4, prm5, prm6, prm7, prm8, prm9, prm9val, prm10, prm10val, TxIDPosition, StatusPosition, Success, Failed, Pending, OperatorRefPosition, ErrorCodePosition, BalanceURL, B_prm1, B_prm1val, B_prm2, B_prm2val, B_prm3, B_prm3val, B_prm4, B_prm4val, B_BalancePosition, StatusURL, S_prm1, S_prm1val, S_prm2, S_prm2val, S_prm3, S_prm4, S_StatusPosition);
            }
            id = Convert.ToInt32(dt.Rows[0]["APIID"].ToString());
            return id;
        }

        public DataTable ManageAPI(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_APITableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_APITableAdapter())
            {
                dt = obj.ManageAPI(Action, ID);
            } return dt;
        }
    }
}

