using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Download
    {
        public int AddEditDownload(int DownloadID, string DownloadName, string DownloadURL, string DownloadIcon, int DownloadTypeID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_DownloadTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_DownloadTableAdapter())
            {
                dt = obj.AddEditDownload(DownloadID, DownloadName, DownloadURL, DownloadIcon, DownloadTypeID);
            }
            id = Convert.ToInt32(dt.Rows[0]["DownloadID"].ToString());
            return id;
        }

        public DataTable ManageDownload(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_DownloadTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_DownloadTableAdapter())
            {
                dt = obj.ManageDownload(Action, ID);
            } return dt;
        }
    }
}

