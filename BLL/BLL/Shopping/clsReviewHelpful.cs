using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsReviewHelpful
    {
        public int AddEditReviewHelpful(int ReviewHelpfulID,bool ReviewHelpful,string ReviewHelpfulDesc,string IPAddress,int CustomerID,int RatingAndReviewID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblReviewHelpfulTableAdapter obj = new DAL.DataSet1TableAdapters.tblReviewHelpfulTableAdapter())
            {
                dt = obj.AddEditReviewHelpful(ReviewHelpfulID, ReviewHelpful, ReviewHelpfulDesc, IPAddress, CustomerID, RatingAndReviewID);
            }
            id = Convert.ToInt32(dt.Rows[0]["ReviewHelpfulID"].ToString());
            return id;
        }

        public DataTable ManageReviewHelpful(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet1TableAdapters.tblReviewHelpfulTableAdapter obj = new DAL.DataSet1TableAdapters.tblReviewHelpfulTableAdapter())
            {
                dt = obj.ManageReviewHelpful(Action, ID);
            } return dt;
        }
    }
}

