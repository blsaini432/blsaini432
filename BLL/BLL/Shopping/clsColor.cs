using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsColor
    {
        public int AddEditColor(int ColorID, string ColorName, string ColorHashCode)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblColorTableAdapter obj = new DAL.DataSet1TableAdapters.tblColorTableAdapter())
            {
                dt = obj.AddEditColor(ColorID, ColorName, ColorHashCode);
            }
            id = Convert.ToInt32(dt.Rows[0]["ColorID"].ToString());
            return id;
        }

        public DataTable ManageColor(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblColorTableAdapter obj = new DAL.DataSet1TableAdapters.tblColorTableAdapter())
            {
                dt = obj.ManageColor(Action, ID);
            } return dt;
        }
    }
}

