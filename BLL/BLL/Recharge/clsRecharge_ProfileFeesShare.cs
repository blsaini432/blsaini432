using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_ProfileFeesShare
    {
        public int AddEditProfileFeesShare(int ProfileFeesShareID, string MemberType, int OrganizationShare, int BusinessPartnerShare, int SuperDistributorShare, int MasterDistributorShare, int DistributorShare, int RetailerShare, int TotalShare)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_ProfileFeesShareTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_ProfileFeesShareTableAdapter())
            {
                dt = obj.AddEditProfileFeesShare(ProfileFeesShareID, MemberType, OrganizationShare, BusinessPartnerShare, SuperDistributorShare, MasterDistributorShare, DistributorShare, RetailerShare, TotalShare);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProfileFeesShareID"].ToString());
            return id;
        }

        public DataTable ManageProfileFeesShare(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_ProfileFeesShareTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_ProfileFeesShareTableAdapter())
            {
                dt = obj.ManageProfileFeesShare(Action, ID);
            } return dt;
        }
    }
}

