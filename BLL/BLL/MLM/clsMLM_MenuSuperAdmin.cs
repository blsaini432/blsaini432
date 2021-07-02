using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MenuSuperAdmin
    {
        public int AddEditMenuSuperAdmin(int MenuID, string MenuName, string MenuLink, int MenuLevel, int ParentID, string MenuStr, int Position)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MenuSuperAdminTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MenuSuperAdminTableAdapter())
            {
                dt = obj.AddEditMenuSuperAdmin(MenuID, MenuName, MenuLink, MenuLevel, ParentID, MenuStr,Position);
            }
            id = Convert.ToInt32(dt.Rows[0]["MenuID"].ToString());
            return id;
        }

        public DataTable ManageMenuSuperAdmin(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MenuSuperAdminTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MenuSuperAdminTableAdapter())
            {
                dt = obj.ManageMenuSuperAdmin(Action, ID);
            } return dt;
        }
    }
}

