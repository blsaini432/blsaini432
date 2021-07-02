using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsTSS_Message
    {
        public int AddEditMessage(int MessageID, int TicketID, string Message, string Attachment, bool ByUser, int AdminID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetTSSTableAdapters.tblTSS_MessageTableAdapter obj = new DAL.DataSetTSSTableAdapters.tblTSS_MessageTableAdapter())
            {
                dt = obj.AddEditMessage(MessageID, TicketID, Message, Attachment, ByUser, AdminID);
            }
            id = Convert.ToInt32(dt.Rows[0]["MessageID"].ToString());
            return id;
        }

        public DataTable ManageMessage(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetTSSTableAdapters.tblTSS_MessageTableAdapter obj = new DAL.DataSetTSSTableAdapters.tblTSS_MessageTableAdapter())
            {
                dt = obj.ManageMessage(Action, ID);
            } return dt;
        }
    }
}

