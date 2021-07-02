using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_Profile
    {
        public int AddEditProfile(int ProfileID, string ProfileName, string Desc, decimal BusinessPartnerFees, decimal SuperDistributorFees, decimal MasterDistributorFees, decimal DistributorFees, decimal RetailerFees)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_ProfileTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_ProfileTableAdapter())
            {
                dt = obj.AddEditProfile(ProfileID, ProfileName, Desc, BusinessPartnerFees, SuperDistributorFees, MasterDistributorFees, DistributorFees, RetailerFees);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProfileID"].ToString());
            return id;
        }

        public DataTable ManageProfile(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_ProfileTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_ProfileTableAdapter())
            {
                dt = obj.ManageProfile(Action, ID);
            } return dt;
        }
    }
}

