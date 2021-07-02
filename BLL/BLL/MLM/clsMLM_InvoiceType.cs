using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_InvoiceType
    {
        public int AddEditInvoiceType(int InvoiceTypeID, string InvoiceTypeName,decimal Discount,decimal Tax,decimal Profit)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_InvoiceTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_InvoiceTypeTableAdapter())
            {
                dt = obj.AddEditInvoiceType(InvoiceTypeID, InvoiceTypeName, Discount, Tax, Profit);
            }
            id = Convert.ToInt32(dt.Rows[0]["InvoiceTypeID"].ToString());
            return id;
        }

        public DataTable ManageInvoiceType(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_InvoiceTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_InvoiceTypeTableAdapter())
            {
                dt = obj.ManageInvoiceType(Action, ID);
            } return dt;
        }
    }
}

