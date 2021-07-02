using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_FranchiseLoginDetail
    {
        public int AddEditFranchiseLoginDetail(int FranchiseLoginDetailID, string LoginIP, int FranchiseID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_FranchiseLoginDetailTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_FranchiseLoginDetailTableAdapter())
            {
                dt = obj.AddEditFranchiseLoginDetail(FranchiseLoginDetailID, LoginIP, FranchiseID);
            }
            id = Convert.ToInt32(dt.Rows[0]["FranchiseLoginDetailID"].ToString());
            return id;
        }

        public DataTable ManageFranchiseLoginDetail(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_FranchiseLoginDetailTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_FranchiseLoginDetailTableAdapter())
            {
                dt = obj.ManageFranchiseLoginDetail(Action, ID);
            } return dt;
        }
    }
}

