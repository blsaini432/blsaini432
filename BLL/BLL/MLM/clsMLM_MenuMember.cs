using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MenuMember
    {
        public int AddEditMenuMember(int MenuID, string MenuName, string MenuLink, int MenuLevel, int ParentID, string MenuStr, int Position)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MenuMemberTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MenuMemberTableAdapter())
            {
                dt = obj.AddEditMenuMember(MenuID, MenuName, MenuLink, MenuLevel, ParentID, MenuStr,Position);
            }
            id = Convert.ToInt32(dt.Rows[0]["MenuID"].ToString());
            return id;
        }

        public DataTable ManageMenuMember(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MenuMemberTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MenuMemberTableAdapter())
            {
                dt = obj.ManageMenuMember(Action, ID);
            } return dt;
        }
    }
}

