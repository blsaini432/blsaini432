using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsPage
    {
        public int AddEditPage(int PageID, string PageName, string PageHeading, string PageDesc, string PageBanner, string PageIcon, string MetaTitle, string MetaKeywords, string MetaDesc, int ParentID, int CompanyID, int Position, bool IsShow, bool IsLeft)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblPageTableAdapter obj = new DAL.DataSet1TableAdapters.tblPageTableAdapter())
            {
                dt = obj.AddEditPage(PageID, PageName, PageHeading, PageDesc, PageBanner, PageIcon, MetaTitle, MetaKeywords, MetaDesc, ParentID, CompanyID, Position, IsShow, IsLeft);
            }
            id = Convert.ToInt32(dt.Rows[0]["PageID"].ToString());
            return id;
        }

        public DataTable ManagePage(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblPageTableAdapter obj = new DAL.DataSet1TableAdapters.tblPageTableAdapter())
            {
                dt = obj.ManagePage(Action, ID);
            } return dt;
        }
    }
}

