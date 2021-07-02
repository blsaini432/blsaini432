using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Franchise
    {
        public int AddEditFranchise(int FranchiseID, string FranchiseName, string FranchiseDesc, string FranchiseType, string Email, DateTime DOB, string Gender, string LoginID, string Password, string TransactionPassword, string Mobile, string STDCode, string Ladline, string FranchiseImage, string Address, string Landmark, int CountryID, int StateID, int CityID, string CityName, string ZIP, string CPName, string CPPhoneNo, string CPMobileNo, string CPEmail, string CPDesignation, string MenuIDStr, string BST, string CST,string FranchiseAutoID,string LastLoginIP,DateTime LastLoginDate, int Parentid)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_FranchiseTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_FranchiseTableAdapter())
            {
                dt = obj.AddEditFranchise(FranchiseID, FranchiseName, FranchiseDesc, FranchiseType, Email, DOB, Gender, LoginID, Password, TransactionPassword, Mobile, STDCode, Ladline, FranchiseImage, Address, Landmark, CountryID, StateID, CityID, CityName, ZIP, CPName, CPPhoneNo, CPMobileNo, CPEmail, CPDesignation, MenuIDStr, BST, CST,FranchiseAutoID,LastLoginIP,LastLoginDate,Parentid);
            }
            id = Convert.ToInt32(dt.Rows[0]["FranchiseID"].ToString());
            return id;
        }

        public DataTable ManageFranchise(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_FranchiseTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_FranchiseTableAdapter())
            {
                dt = obj.ManageFranchise(Action, ID);
            } return dt;
        }
    }
}

