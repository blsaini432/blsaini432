using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_PinRequest
    {
        public int AddEditPinRequest(int PinRequestID, int MsrNo, string MemberID, decimal TotalAmount, DateTime RequestDate, string BankName, string PaymentMode, string ChequeOrDDNumber, DateTime ChequeDate, string Remark)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinRequestTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinRequestTableAdapter())
            {
                dt = obj.AddEditPinRequest(PinRequestID, MsrNo, MemberID, TotalAmount, RequestDate, BankName, PaymentMode, ChequeOrDDNumber, ChequeDate, Remark);
            }
            id = Convert.ToInt32(dt.Rows[0]["PinRequestID"].ToString());
            return id;
        }

        public DataTable ManagePinRequest(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinRequestTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinRequestTableAdapter())
            {
                dt = obj.ManagePinRequest(Action, ID);
            } return dt;
        }
    }
}

