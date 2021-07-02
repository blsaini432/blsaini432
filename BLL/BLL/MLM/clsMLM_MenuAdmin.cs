using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MenuAdmin
    {
        public int AddEditMenuAdmin(int MenuID, string MenuName, string MenuLink, int MenuLevel, int ParentID, string MenuStr, int Position)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MenuAdminTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MenuAdminTableAdapter())
            {
                dt = obj.AddEditMenuAdmin(MenuID, MenuName, MenuLink, MenuLevel, ParentID, MenuStr,Position);
            }
            id = Convert.ToInt32(dt.Rows[0]["MenuID"].ToString());
            return id;
        }

        public DataTable ManageMenuAdmin(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MenuAdminTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MenuAdminTableAdapter())
            {
                dt = obj.ManageMenuAdmin(Action, ID);
            } return dt;
        }
    }
}

