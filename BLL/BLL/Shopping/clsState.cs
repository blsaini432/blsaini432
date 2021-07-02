using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsState
    {
        public int AddEditState(int StateID,string StateName,int CountryID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblStateTableAdapter obj = new DAL.DataSet1TableAdapters.tblStateTableAdapter())
            {
                dt = obj.AddEditState(StateID, StateName,CountryID);
            }
            id = Convert.ToInt32(dt.Rows[0]["StateID"].ToString());
            return id;
        }

        public DataTable ManageState(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblStateTableAdapter obj = new DAL.DataSet1TableAdapters.tblStateTableAdapter())
            {
                dt = obj.ManageState(Action, ID);
            } return dt;
        }
    }
}

