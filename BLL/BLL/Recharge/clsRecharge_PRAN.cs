using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_PRAN
    {
        public int AddEditPRAN(int PRANID, int MsrNo, string AcknowledgementNo, string PRAN, string Title, string FirstName, string MiddleName, string LastName, string Gender, string DOB, string FatherFirstName, string FatherMiddleName, string FatherLastName, string Mobile, string MemberShipNo, string AddressLine1, string AddressLine2, string AddressLine3, string City, string State, string Country, string Pin, string BankAccountType, string BankAccountNo, string BankName, string BankBranch, string BankAddress, string BankPin, string BankMICRCode, string BankIFSCCode, string Photo, string IDProof, string IDProof_Photo, string ResidenceProof, string ResidenceProof_Photo, string Status, decimal Amount)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblPRANTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblPRANTableAdapter())
            {
                dt = obj.AddEditPRAN(PRANID, MsrNo, AcknowledgementNo, PRAN, Title, FirstName, MiddleName, LastName, Gender, DOB, FatherFirstName, FatherMiddleName, FatherLastName, Mobile, MemberShipNo, AddressLine1, AddressLine2, AddressLine3, City, State, Country, Pin, BankAccountType, BankAccountNo, BankName, BankBranch, BankAddress, BankPin, BankMICRCode, BankIFSCCode, Photo, IDProof, IDProof_Photo, ResidenceProof, ResidenceProof_Photo, Status, Amount);
            }
            id = Convert.ToInt32(dt.Rows[0]["PRANID"].ToString());
            return id;
        }

        public DataTable ManagePRAN(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblPRANTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblPRANTableAdapter())
            {
                dt = obj.ManagePRAN(Action, ID);
            } return dt;
        }
    }
}

