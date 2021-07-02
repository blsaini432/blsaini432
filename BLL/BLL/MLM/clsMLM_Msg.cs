using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_Msg
    {
        public int AddEditMsg(int MsgID, int FromMsrNo, string Subject, string Message, string Attachment, bool IsRead, bool IsDraft)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgTableAdapter())
            {
                dt = obj.AddEditMsg(MsgID, FromMsrNo, Subject, Message, Attachment, IsRead, IsDraft);
            }
            id = Convert.ToInt32(dt.Rows[0]["MsgID"].ToString());
            return id;
        }

        public DataTable ManageMsg(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgTableAdapter())
            {
                dt = obj.ManageMsg(Action, ID);
            } return dt;
        }
    }
}

