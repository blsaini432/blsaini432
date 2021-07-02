using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_WareHouse
    {
        public int AddEditWareHouse(int WareHouseID,string WareHouseName,string WareHouseDesc,int ParentID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_WareHouseTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_WareHouseTableAdapter())
            {
                dt = obj.AddEditWareHouse(WareHouseID,WareHouseName, WareHouseDesc, ParentID);
            }
            id = Convert.ToInt32(dt.Rows[0]["WareHouseID"].ToString());
            return id;
        }

        public DataTable ManageWareHouse(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_WareHouseTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_WareHouseTableAdapter())
            {
                dt = obj.ManageWareHouse(Action, ID);
            } return dt;
        }
    }
}

