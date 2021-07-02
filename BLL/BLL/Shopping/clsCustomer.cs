using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsCustomer
    {
        public int AddEditCustomer(int CustomerID, string Title, string FirstName, string MiddleName, string LastName, string CareOf, string CareOfName, string Email, DateTime DOB, string Gender, int Age, string LoginID, string Password, string Mobile, string Mobile1, string STDCode, string Ladline, string CustomerImage, string Address, string Landmark, string ZIP, string S_ContactNo, string S_Address, string S_Landmark, string S_ZIP, string IPAddress, string CustomerDesc, int CustomerType, string RegisterSources, bool IsEmailSubscribe, bool IsSMSSubscribe, bool IsEmailVerify, bool IsMobileVerify, string LastLoginIP, DateTime LastLoginDate, int CountryID, int StateID, int CityID, int S_CountryID, int S_StateID, int S_CityID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCustomerTableAdapter obj = new DAL.DataSet1TableAdapters.tblCustomerTableAdapter())
            {
                dt = obj.AddEditCustomer(CustomerID, Title, FirstName, MiddleName, LastName, CareOf, CareOfName, Email, DOB, Gender, Age, LoginID, Password, Mobile, Mobile1, STDCode, Ladline, CustomerImage, Address, Landmark, ZIP, S_ContactNo, S_Address, S_Landmark, S_ZIP, IPAddress, CustomerDesc, CustomerType, RegisterSources, IsEmailSubscribe, IsSMSSubscribe, IsEmailVerify, IsMobileVerify, LastLoginIP, LastLoginDate, CountryID, StateID, CityID, S_CountryID, S_StateID, S_CityID);
            }
            id = Convert.ToInt32(dt.Rows[0]["CustomerID"].ToString());
            return id;
        }

        public DataTable ManageCustomer(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblCustomerTableAdapter obj = new DAL.DataSet1TableAdapters.tblCustomerTableAdapter())
            {
                dt = obj.ManageCustomer(Action, ID);
            } return dt;
        }

      
    }
}

