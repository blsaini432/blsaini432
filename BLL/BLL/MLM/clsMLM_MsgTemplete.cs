using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MsgTemplete
    {
        public int AddEditMsgTemplete(int MsgTempleteID, string MsgTempleteName, int MsgTypeID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgTempleteTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgTempleteTableAdapter())
            {
                dt = obj.AddEditMsgTemplete(MsgTempleteID, MsgTempleteName, MsgTypeID);
            }
            id = Convert.ToInt32(dt.Rows[0]["MsgTempleteID"].ToString());
            return id;
        }

        public DataTable ManageMsgTemplete(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgTempleteTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgTempleteTableAdapter())
            {
                dt = obj.ManageMsgTemplete(Action, ID);
            } return dt;
        }
    }
}

