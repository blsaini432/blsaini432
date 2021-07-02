using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_DownloadType
    {
        public int AddEditDownloadType(int DownloadTypeID, string DownloadTypeName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_DownloadTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_DownloadTypeTableAdapter())
            {
                dt = obj.AddEditDownloadType(DownloadTypeID, DownloadTypeName);
            }
            id = Convert.ToInt32(dt.Rows[0]["DownloadTypeID"].ToString());
            return id;
        }

        public DataTable ManageDownloadType(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_DownloadTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_DownloadTypeTableAdapter())
            {
                dt = obj.ManageDownloadType(Action, ID);
            } return dt;
        }
    }
}

