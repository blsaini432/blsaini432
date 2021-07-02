using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsCompany
    {
        public int AddEditCompany(int CompanyID, string CompanyName, string CompanyLogo, string CompanyOwner, string Phone, string Mobile, string Email, string Website, string Fax, string Line1, string Line2, string Line3, string Description, string Copyright, string Address, int PIN, int CountryID, int StateID, int CityID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCompanyTableAdapter obj = new DAL.DataSet1TableAdapters.tblCompanyTableAdapter())
            {
                dt = obj.AddEditCompany(CompanyID, CompanyName, CompanyLogo, CompanyOwner, Phone, Mobile, Email, Website, Fax, Line1, Line2, Line3, Description, Copyright, Address, PIN, CountryID, StateID, CityID);
            }
            id = Convert.ToInt32(dt.Rows[0]["CompanyID"].ToString());
            return id;
        }

        public DataTable ManageCompany(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCompanyTableAdapter obj = new DAL.DataSet1TableAdapters.tblCompanyTableAdapter())
            {
                dt = obj.ManageCompany(Action, ID);
            } return dt;
        }
    }
}

