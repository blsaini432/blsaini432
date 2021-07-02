using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsNews
    {
        public int AddEditNews(int NewsID,string NewsName,string NewsImage,string NewsIcon,string NewsDesc,string NavigateURL,DateTime NewsDate)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblNewsTableAdapter obj = new DAL.DataSet1TableAdapters.tblNewsTableAdapter())
            {
                dt = obj.AddEditNews(NewsID, NewsName, NewsImage, NewsIcon, NewsDesc, NavigateURL, NewsDate);
            }
            id = Convert.ToInt32(dt.Rows[0]["NewsID"].ToString());
            return id;
        }

        public DataTable ManageNews(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblNewsTableAdapter obj = new DAL.DataSet1TableAdapters.tblNewsTableAdapter())
            {
                dt = obj.ManageNews(Action, ID);
            } return dt;
        }
    }
}

