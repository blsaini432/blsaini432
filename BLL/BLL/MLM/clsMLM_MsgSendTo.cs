using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MsgSendTo
    {
        public int AddEditMsgSendTo(int MsgSendToID, int MsgID, int ToMsrNo)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgSendToTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgSendToTableAdapter())
            {
                dt = obj.AddEditMsgSendTo(MsgSendToID, MsgID, ToMsrNo);
            }
            id = Convert.ToInt32(dt.Rows[0]["MsgSendToID"].ToString());
            return id;
        }

        public DataTable ManageMsgSendTo(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgSendToTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgSendToTableAdapter())
            {
                dt = obj.ManageMsgSendTo(Action, ID);
            } return dt;
        }
    }
}

