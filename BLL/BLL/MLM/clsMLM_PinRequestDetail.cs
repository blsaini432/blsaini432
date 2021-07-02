using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_PinRequestDetail
    {
        public int AddEditPinRequestDetail(int PinRequestDetailID, int PinRequestID, int PackageID, int Quantity)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinRequestDetailTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinRequestDetailTableAdapter())
            {
                dt = obj.AddEditPinRequestDetail(PinRequestDetailID, PinRequestID, PackageID, Quantity);
            }
            id = Convert.ToInt32(dt.Rows[0]["PinRequestDetailID"].ToString());
            return id;
        }

        public DataTable ManagePinRequestDetail(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinRequestDetailTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinRequestDetailTableAdapter())
            {
                dt = obj.ManagePinRequestDetail(Action, ID);
            } return dt;
        }
    }
}

