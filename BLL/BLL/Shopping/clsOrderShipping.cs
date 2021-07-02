using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsOrderShipping
    {
        public int AddEditOrderShipping(int OrderShippingID,int OrderID,string CustomerID,string ContactPerson,string Email,string Mobile,string Mobile1,string Address,string Landmark,string Country,string State,string City,string ZIP,string Extra1,string Extra2,string Extra3,string Extra4,string ShippingDesc,bool IsVerified)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblOrderShippingTableAdapter obj = new DAL.DataSet1TableAdapters.tblOrderShippingTableAdapter())
            {
                dt = obj.AddEditOrderShipping(OrderShippingID, OrderID, CustomerID, ContactPerson, Email, Mobile, Mobile1, Address, Landmark, Country, State, City, ZIP, Extra1, Extra2, Extra3, Extra4, ShippingDesc, IsVerified);
            }
            id = Convert.ToInt32(dt.Rows[0]["OrderShippingID"].ToString());
            return id;
        }

        public DataTable ManageOrderShipping(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblOrderShippingTableAdapter obj = new DAL.DataSet1TableAdapters.tblOrderShippingTableAdapter())
            {
                dt = obj.ManageOrderShipping(Action, ID);
            } return dt;
        }
    }
}

