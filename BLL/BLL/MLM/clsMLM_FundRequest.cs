using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_FundRequest
    {
        public int AddEditFundRequest(int FundRequestID,string WalletName,int MsrNo,string MemberID,decimal Amount, string BankName,string  PaymentMode,string PaymentProof,string ChequeOrDDNumber,DateTime ChequeDate,string RequestStatus,string Remark,string RCode,string FromBank,string ToBank)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_FundRequestTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_FundRequestTableAdapter())
            {
                dt = obj.AddEditFundRequest(FundRequestID, WalletName, MsrNo, MemberID, Amount, BankName, PaymentMode, PaymentProof, ChequeOrDDNumber, ChequeDate, RequestStatus, Remark, RCode, FromBank, ToBank);
            }
            id = Convert.ToInt32(dt.Rows[0]["FundRequestID"].ToString());
            return id;
        }

        public DataTable ManageFundRequest(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_FundRequestTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_FundRequestTableAdapter())
            {
                dt = obj.ManageFundRequest(Action, ID);
            } return dt;
        }
    }
}

