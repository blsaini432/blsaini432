using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_AddOnNumber
    {
        public int AddEditAddOnNumber(int AddOnNumberID, int MsrNo, string MobileNo, string Name, int OperatorID, int CircleID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_AddOnNumberTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_AddOnNumberTableAdapter())
            {
                dt = obj.AddEditAddOnNumber(AddOnNumberID, MsrNo, MobileNo, Name, OperatorID, CircleID);
            }
            id = Convert.ToInt32(dt.Rows[0]["AddOnNumberID"].ToString());
            return id;
        }

        public DataTable ManageAddOnNumber(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_AddOnNumberTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_AddOnNumberTableAdapter())
            {
                dt = obj.ManageAddOnNumber(Action, ID);
            } return dt;
        }
    }
}

