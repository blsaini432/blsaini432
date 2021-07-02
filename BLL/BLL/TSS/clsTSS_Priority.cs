using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsTSS_Priority
    {
        public int AddEditPriority(int PriorityID, string PriorityName, string SolutionTime)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetTSSTableAdapters.tblTSS_PriorityTableAdapter obj = new DAL.DataSetTSSTableAdapters.tblTSS_PriorityTableAdapter())
            {
                dt = obj.AddEditPriority(PriorityID, PriorityName, SolutionTime);
            }
            id = Convert.ToInt32(dt.Rows[0]["PriorityID"].ToString());
            return id;
        }

        public DataTable ManagePriority(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetTSSTableAdapters.tblTSS_PriorityTableAdapter obj = new DAL.DataSetTSSTableAdapters.tblTSS_PriorityTableAdapter())
            {
                dt = obj.ManagePriority(Action, ID);
            } return dt;
        }
    }
}

