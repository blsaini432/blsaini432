using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRatingAndReview
    {
        public int AddEditRatingAndReview(int RatingAndReviewID, string ReviewTitle,string YourReview,decimal YourRating,string IPAddress,int CustomerID,int ProductID, string UserName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblRatingAndReviewTableAdapter obj = new DAL.DataSet1TableAdapters.tblRatingAndReviewTableAdapter())
            {
                dt = obj.AddEditRatingAndReview(RatingAndReviewID, ReviewTitle, YourReview, YourRating, IPAddress, CustomerID, ProductID, UserName);
            }
            id = Convert.ToInt32(dt.Rows[0]["RatingAndReviewID"].ToString());
            return id;
        }

        public DataTable ManageRatingAndReview(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblRatingAndReviewTableAdapter obj = new DAL.DataSet1TableAdapters.tblRatingAndReviewTableAdapter())
            {
                dt = obj.ManageRatingAndReview(Action, ID);
            } return dt;
        }
    }
}

