using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsFAQ
    {
        public int AddEditFAQ(int FAQID,string FAQQuestion,string FAQAnswer,string FAQImage,string FAQIcon,string NavigateURL,int FAQCategoryID, string FAQCategoryName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblFAQTableAdapter obj = new DAL.DataSet1TableAdapters.tblFAQTableAdapter())
            {
                dt = obj.AddEditFAQ(FAQID, FAQQuestion, FAQAnswer, FAQImage, FAQIcon, NavigateURL, FAQCategoryID, FAQCategoryName);
            }
            id = Convert.ToInt32(dt.Rows[0]["FAQID"].ToString());
            return id;
        }

        public DataTable ManageFAQ(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblFAQTableAdapter obj = new DAL.DataSet1TableAdapters.tblFAQTableAdapter())
            {
                dt = obj.ManageFAQ(Action, ID);
            } return dt;
        }
    }
}

