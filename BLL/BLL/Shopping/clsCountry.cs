using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsCountry
    {
        public int AddEditCountry(int CountryID, string CountryName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCountryTableAdapter obj = new DAL.DataSet1TableAdapters.tblCountryTableAdapter())
            {
                dt = obj.AddEditCountry(CountryID,CountryName);
            }
            id = Convert.ToInt32(dt.Rows[0]["CountryID"].ToString());
            return id;
        }

        public DataTable ManageCountry(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCountryTableAdapter obj = new DAL.DataSet1TableAdapters.tblCountryTableAdapter())
            {
                dt = obj.ManageCountry(Action, ID);
            } return dt;
        }
    }
}

