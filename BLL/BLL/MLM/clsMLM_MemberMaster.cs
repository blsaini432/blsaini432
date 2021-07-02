using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MemberMaster
    {
        public int AddEditMemberMaster(int MsrNo, string MemberID, string FirstName, string ShopName, string LastName, string Email, DateTime DOB, string Gender, string Password, string TransactionPassword, string Mobile, string STDCode, string Ladline, string Address, string Landmark, int CountryID, int StateID, int CityID, string CityName, string ZIP, string MemberType, int MemberTypeID, int ParentMsrNo, int PackageID, string Memberimage)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterTableAdapter())
            {
                dt = obj.AddEditMemberMaster(MsrNo, MemberID, FirstName,ShopName, LastName, Email, DOB, Gender, Password, TransactionPassword, Mobile, STDCode, Ladline, Address, Landmark, CountryID, StateID, CityID, CityName, ZIP, MemberType, MemberTypeID, ParentMsrNo, PackageID, Memberimage);
            }
            id = Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString());
            return id;
        }
        public int ProcMLM_RAddEditMemberMaster(int MsrNo, string MemberID, string FirstName, string LastName, string Email, DateTime DOB, string Gender, string Password, string TransactionPassword, string Mobile, string STDCode, string Ladline, string Address, string Landmark, int CountryID, int StateID, int CityID, string CityName, string ZIP, string MemberType, int MemberTypeID, int ParentMsrNo, int PackageID,string weburl)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterTableAdapter())
            {
                dt = obj.ProcMLM_RAddEditMemberMaster(MsrNo, MemberID, FirstName, LastName, Email, DOB, Gender, Password, TransactionPassword, Mobile, STDCode, Ladline, Address, Landmark, CountryID, StateID, CityID, CityName, ZIP, MemberType, MemberTypeID, ParentMsrNo, PackageID,weburl);
            }
            id = Convert.ToInt32(dt.Rows[0]["MsrNo"].ToString());
            return id;
        }
        
        public DataTable ManageMemberMaster(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberMasterTableAdapter())
            {
                dt = obj.ManageMemberMaster(Action, ID);
            } return dt;
        }
    }
}

