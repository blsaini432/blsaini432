using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsReviewAbuse
    {
        public int AddEditReviewAbuse(int ReviewAbuseID,bool ReviewAbuse,string ReviewAbuseDesc,string IPAddress,int CustomerID,int RatingAndReviewID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblReviewAbuseTableAdapter obj = new DAL.DataSet1TableAdapters.tblReviewAbuseTableAdapter())
            {
                dt = obj.AddEditReviewAbuse(ReviewAbuseID, ReviewAbuse, ReviewAbuseDesc, IPAddress, CustomerID, RatingAndReviewID);
            }
            id = Convert.ToInt32(dt.Rows[0]["ReviewAbuseID"].ToString());
            return id;
        }

        public DataTable ManageReviewAbuse(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblReviewAbuseTableAdapter obj = new DAL.DataSet1TableAdapters.tblReviewAbuseTableAdapter())
            {
                dt = obj.ManageReviewAbuse(Action, ID);
            } return dt;
        }
    }
}

