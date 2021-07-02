using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMenu
    {
        public int AddEditMenu(int MenuID,string MenuName,string MenuLink,int MenuLevel,int ParentID,string MenuStr,bool IsMLM,int Position)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblMenuTableAdapter obj = new DAL.DataSet1TableAdapters.tblMenuTableAdapter())
            {
                dt = obj.AddEditMenu(MenuID, MenuName, MenuLink, MenuLevel, ParentID, MenuStr,IsMLM,Position);
            }
            id = Convert.ToInt32(dt.Rows[0]["MenuID"].ToString());
            return id;
        }

        public DataTable ManageMenu(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblMenuTableAdapter obj = new DAL.DataSet1TableAdapters.tblMenuTableAdapter())
            {
                dt = obj.ManageMenu(Action, ID);
            } return dt;
        }
    }
}

