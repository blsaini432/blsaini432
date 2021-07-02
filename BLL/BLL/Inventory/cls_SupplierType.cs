using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_SupplierType
    {
        public int AddEditSupplierType(int SupplierTypeID, string SupplierTypeName, string SupplierTypeDesc)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_SupplierTypeTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_SupplierTypeTableAdapter())
            {
                dt = obj.AddEditSupplierType(SupplierTypeID,SupplierTypeName, SupplierTypeDesc);
            }
            id = Convert.ToInt32(dt.Rows[0]["SupplierTypeID"].ToString());
            return id;
        }

        public DataTable ManageSupplierType(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_SupplierTypeTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_SupplierTypeTableAdapter())
            {
                dt = obj.ManageSupplierType(Action, ID);
            } return dt;
        }
    }
}

