using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_PinTransfered
    {
        public int AddEditPinTransfered(int PinTransferedID, int PinMasterID, int FromMsrNo, int ToMsrNo, DateTime TransferDate, string Narration)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinTransferedTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinTransferedTableAdapter())
            {
                dt = obj.AddEditPinTransfered(PinTransferedID, PinMasterID, FromMsrNo, ToMsrNo, TransferDate, Narration);
            }
            id = Convert.ToInt32(dt.Rows[0]["PinTransferedID"].ToString());
            return id;
        }

        public DataTable ManagePinTransfered(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinTransferedTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinTransferedTableAdapter())
            {
                dt = obj.ManagePinTransfered(Action, ID);
            } return dt;
        }
    }
}

