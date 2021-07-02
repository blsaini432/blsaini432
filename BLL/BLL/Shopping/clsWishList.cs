using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsWishList
    {
        public int AddEditWishList(int WishListID,int CustomerID,int  ProductID,string IPAddress,bool IsBuy)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblWishListTableAdapter obj = new DAL.DataSet1TableAdapters.tblWishListTableAdapter())
            {
                dt = obj.AddEditWishList(WishListID,CustomerID, ProductID, IPAddress, IsBuy);
            }
            id = Convert.ToInt32(dt.Rows[0]["WishListID"].ToString());
            return id;
        }

        public DataTable ManageWishList(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblWishListTableAdapter obj = new DAL.DataSet1TableAdapters.tblWishListTableAdapter())
            {
                dt = obj.ManageWishList(Action, ID);
            } return dt;
        }
    }
}

