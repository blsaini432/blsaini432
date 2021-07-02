using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Package
    {
        public int AddEditPackage(int PackageID, string PackageName, decimal Price, int MsrNo, decimal PV, decimal BV, decimal CapAt, decimal Direct, decimal Binary, string ModeName, string ModeFlag, string ImageFlag, bool IsForSpill, decimal Spill)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PackageTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PackageTableAdapter())
            {
                dt = obj.AddEditPackage(PackageID, PackageName, Price, MsrNo, PV, BV, CapAt, Direct, Binary, ModeName, ModeFlag, ImageFlag, IsForSpill, Spill);
            }
            id = Convert.ToInt32(dt.Rows[0]["PackageID"].ToString());
            return id;
        }

        public DataTable ManagePackage(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PackageTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PackageTableAdapter())
            {
                dt = obj.ManagePackage(Action, ID);
            } return dt;
        }
    }
}

