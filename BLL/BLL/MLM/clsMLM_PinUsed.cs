using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_PinUsed
    {
        public int AddEditPinUsed(int PinUsedID, int PinMasterID, int UsedMsrNo, DateTime UsedDate, string Narration)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinUsedTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinUsedTableAdapter())
            {
                dt = obj.AddEditPinUsed(PinUsedID, PinMasterID, UsedMsrNo, UsedDate, Narration);
            }
            id = Convert.ToInt32(dt.Rows[0]["PinUsedID"].ToString());
            return id;
        }

        public DataTable ManagePinUsed(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinUsedTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinUsedTableAdapter())
            {
                dt = obj.ManagePinUsed(Action, ID);
            } return dt;
        }
    }
}

