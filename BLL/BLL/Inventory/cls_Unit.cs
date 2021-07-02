using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_Unit
    {
        public int AddEditUnit(int UnitID, string UnitName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_UnitTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_UnitTableAdapter())
            {
                dt = obj.AddEditUnit(UnitID,UnitName);
            }
            id = Convert.ToInt32(dt.Rows[0]["UnitID"].ToString());
            return id;
        }

        public DataTable ManageUnit(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_UnitTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_UnitTableAdapter())
            {
                dt = obj.ManageUnit(Action, ID);
            } return dt;
        }
    }
}

