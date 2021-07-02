using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsPageHit
    {
        public int AddEditPageHit(int PageHitID,string ReferFrom,string IPAddress,int PageID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblPageHitTableAdapter obj = new DAL.DataSet1TableAdapters.tblPageHitTableAdapter())
            {
                dt = obj.AddEditPageHit(PageHitID, ReferFrom, IPAddress, PageID);
            }
            id = Convert.ToInt32(dt.Rows[0]["PageHitID"].ToString());
            return id;
        }

        public DataTable ManagePageHit(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblPageHitTableAdapter obj = new DAL.DataSet1TableAdapters.tblPageHitTableAdapter())
            {
                dt = obj.ManagePageHit(Action, ID);
            } return dt;
        }
    }
}

