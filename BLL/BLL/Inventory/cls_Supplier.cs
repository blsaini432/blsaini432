using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_Supplier
    {
        public int AddEditSupplier(int SupplierID,string SupplierName,string SupplierDesc,int CountryID,int StateID,int CityID,string AddressType,string Address,string Email,string PhoneNo,string MobileNo,string FaxNo,string PinCode,string Website,string BST,string CST,decimal DuePaymant,string DuePaymantType,string CPName,string CPPhoneNo,string CPMobileNo,string CPEmail,string CPDesignation,string CPResiNo,string CPDesc,string WorkingWith,int SupplierTypeID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_SupplierTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_SupplierTableAdapter())
            {
                dt = obj.AddEditSupplier(SupplierID,SupplierName, SupplierDesc, CountryID, StateID, CityID, AddressType, Address, Email, PhoneNo, MobileNo, FaxNo, PinCode, Website, BST, CST, DuePaymant, DuePaymantType, CPName, CPPhoneNo, CPMobileNo, CPEmail, CPDesignation, CPResiNo, CPDesc, WorkingWith, SupplierTypeID);
            }
            id = Convert.ToInt32(dt.Rows[0]["SupplierID"].ToString());
            return id;
        }

        public DataTable ManageSupplier(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_SupplierTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_SupplierTableAdapter())
            {
                dt = obj.ManageSupplier(Action, ID);
            } return dt;
        }
    }
}

