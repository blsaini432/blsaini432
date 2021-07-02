using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_ServiceType
    {
        public int AddEditServiceType(int ServiceTypeID, string ServiceTypeName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_ServiceTypeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_ServiceTypeTableAdapter())
            {
                dt = obj.AddEditServiceType(ServiceTypeID, ServiceTypeName);
            }
            id = Convert.ToInt32(dt.Rows[0]["ServiceTypeID"].ToString());
            return id;
        }

        public DataTable ManageServiceType(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_ServiceTypeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_ServiceTypeTableAdapter())
            {
                dt = obj.ManageServiceType(Action, ID);
            } return dt;
        }
    }
}

