using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_Vendor
    {
        public int AddEditVendor(int VendorID, string VendorName, string VendorDesc, string VendorImage, string Email, string Mobile, string Address, string CompanyName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_VendorTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_VendorTableAdapter())
            {
                dt = obj.AddEditVendor(VendorID, VendorName, VendorDesc, VendorImage, Email, Mobile, Address, CompanyName);
            }
            id = Convert.ToInt32(dt.Rows[0]["VendorID"].ToString());
            return id;
        }

        public DataTable ManageVendor(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblRecharge_VendorTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblRecharge_VendorTableAdapter())
            {
                dt = obj.ManageVendor(Action, ID);
            } return dt;
        }
    }
}

