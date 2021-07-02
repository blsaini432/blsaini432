using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsFAQCategory
    {
        public int AddEditFAQCategory(int FAQCategoryID, string FAQCategoryName, string FAQCategoryImage, string FAQCategoryIcon, string FAQCategoryDesc, string NavigateURL)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblFAQCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblFAQCategoryTableAdapter())
            {
                dt = obj.AddEditFAQCategory(FAQCategoryID, FAQCategoryName, FAQCategoryImage, FAQCategoryIcon, FAQCategoryDesc, NavigateURL);
            }
            id = Convert.ToInt32(dt.Rows[0]["FAQCategoryID"].ToString());
            return id;
        }

        public DataTable ManageFAQCategory(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblFAQCategoryTableAdapter obj = new DAL.DataSet1TableAdapters.tblFAQCategoryTableAdapter())
            {
                dt = obj.ManageFAQCategory(Action, ID);
            } return dt;
        }
    }
}

