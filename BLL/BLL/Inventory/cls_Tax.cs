using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_Tax
    {
        public int AddEditTax(int TaxID,string TaxName,decimal Tax,string TaxDesc,int ParentID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_TaxTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_TaxTableAdapter())
            {
                dt = obj.AddEditTax(TaxID,TaxName, Tax, TaxDesc, ParentID);
            }
            id = Convert.ToInt32(dt.Rows[0]["TaxID"].ToString());
            return id;
        }

        public DataTable ManageTax(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_TaxTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_TaxTableAdapter())
            {
                dt = obj.ManageTax(Action, ID);
            } return dt;
        }
    }
}

