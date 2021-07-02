using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsProductFeatureValue
    {
        public int AddEditProductFeatureValue(int ProductFeatureValueID,string ProductFeatureName,string ProductFeatureValue,int ProductID,int ProductFeatureID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductFeatureValueTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductFeatureValueTableAdapter())
            {
                dt = obj.AddEditProductFeatureValue(ProductFeatureValueID, ProductFeatureName, ProductFeatureValue, ProductID, ProductFeatureID);
            }
            id = Convert.ToInt32(dt.Rows[0]["ProductFeatureValueID"].ToString());
            return id;
        }

        public DataTable ManageProductFeatureValue(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblProductFeatureValueTableAdapter obj = new DAL.DataSet1TableAdapters.tblProductFeatureValueTableAdapter())
            {
                dt = obj.ManageProductFeatureValue(Action, ID);
            } return dt;
        }
    }
}

