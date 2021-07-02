using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsTSS_Ticket
    {
        public int AddEditTicket(int TicketID, int MemberID, string Subject, string Message, int DeptID, int PriorityID, string Status)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetTSSTableAdapters.tblTSS_TicketTableAdapter obj = new DAL.DataSetTSSTableAdapters.tblTSS_TicketTableAdapter())
            {
                dt = obj.AddEditTicket(TicketID, MemberID, Subject, Message, DeptID, PriorityID, Status);
            }
            id = Convert.ToInt32(dt.Rows[0]["TicketID"].ToString());
            return id;
        }

        public DataTable ManageTicket(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetTSSTableAdapters.tblTSS_TicketTableAdapter obj = new DAL.DataSetTSSTableAdapters.tblTSS_TicketTableAdapter())
            {
                dt = obj.ManageTicket(Action, ID);
            } return dt;
        }
    }
}

