using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_BankerMaster
    {
        public int AddEditBankerMaster(int BankerMasterID, string BankerMasterName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_BankerMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_BankerMasterTableAdapter())
            {
                dt = obj.AddEditBankerMaster(BankerMasterID, BankerMasterName);
            }
            id = Convert.ToInt32(dt.Rows[0]["BankerMasterID"].ToString());
            return id;
        }

        public DataTable ManageBankerMaster(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_BankerMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_BankerMasterTableAdapter())
            {
                dt = obj.ManageBankerMaster(Action, ID);
            } return dt;
        }
    }
}

