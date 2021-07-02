using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MsgType
    {
        public int AddEditMsgType(int MsgTypeID, string MsgTypeName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgTypeTableAdapter())
            {
                dt = obj.AddEditMsgType(MsgTypeID, MsgTypeName);
            }
            id = Convert.ToInt32(dt.Rows[0]["MsgTypeID"].ToString());
            return id;
        }

        public DataTable ManageMsgType(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgTypeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgTypeTableAdapter())
            {
                dt = obj.ManageMsgType(Action, ID);
            } return dt;
        }
    }
}

