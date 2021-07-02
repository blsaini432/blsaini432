using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_Transporter
    {
        public int AddEditTransporter(int TransporterID, string TransporterName, string TransporterDesc, int CountryID, int StateID, int CityID, string AddressType, string Address, string Email, string PhoneNo, string MobileNo, string FaxNo, string PinCode, string Website, string BST, string CST, decimal DuePaymant, string DuePaymantType, string CPName, string CPPhoneNo, string CPMobileNo, string CPEmail, string CPDesignation, string CPResiNo, string CPDesc)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_TransporterTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_TransporterTableAdapter())
            {
                dt = obj.AddEditTransporter(TransporterID, TransporterName, TransporterDesc, CountryID, StateID, CityID, AddressType, Address, Email, PhoneNo, MobileNo, FaxNo, PinCode, Website, BST, CST, DuePaymant, DuePaymantType, CPName, CPPhoneNo, CPMobileNo, CPEmail, CPDesignation, CPResiNo, CPDesc);
            }
            id = Convert.ToInt32(dt.Rows[0]["TransporterID"].ToString());
            return id;
        }

        public DataTable ManageTransporter(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_TransporterTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_TransporterTableAdapter())
            {
                dt = obj.ManageTransporter(Action, ID);
            } return dt;
        }
    }
}

