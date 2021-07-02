using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsCity
    {
        public int AddEditCity(int CityID,string CityName,int StateID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCityTableAdapter obj = new DAL.DataSet1TableAdapters.tblCityTableAdapter())
            {
                dt = obj.AddEditCity(CityID, CityName,StateID);
            }
            id = Convert.ToInt32(dt.Rows[0]["CityID"].ToString());
            return id;
        }

        public DataTable ManageCity(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCityTableAdapter obj = new DAL.DataSet1TableAdapters.tblCityTableAdapter())
            {
                dt = obj.ManageCity(Action, ID);
            } return dt;
        }
    }
}

