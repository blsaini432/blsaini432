using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsRecharge_PRANNominee
    {
        public int AddEditPRANNominee(int PRANNomineeID, int PRANID, string FirstName1, string MiddleName1, string LastName1, string DOB1, string RelationShip1, string Share1, string GuardianFirstName1, string GuardianMiddleName1, string GuardianLastName1, string FirstName2, string MiddleName2, string LastName2, string DOB2, string RelationShip2, string Share2, string GuardianFirstName2, string GuardianMiddleName2, string GuardianLastName2, string FirstName3, string MiddleName3, string LastName3, string DOB3, string RelationShip3, string Share3, string GuardianFirstName3, string GuardianMiddleName3, string GuardianLastName3)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetRechargeTableAdapters.tblPRANNomineeTableAdapter obj = new DAL.DataSetRechargeTableAdapters.tblPRANNomineeTableAdapter())
            {
                dt = obj.AddEditPRANNominee(PRANNomineeID, PRANID, FirstName1, MiddleName1, LastName1, DOB1, RelationShip1, Share1, GuardianFirstName1, GuardianMiddleName1, GuardianLastName1, FirstName2, MiddleName2, LastName2, DOB2, RelationShip2, Share2, GuardianFirstName2, GuardianMiddleName2, GuardianLastName2, FirstName3, MiddleName3, LastName3, DOB3, RelationShip3, Share3, GuardianFirstName3, GuardianMiddleName3, GuardianLastName3);
            }
            id = Convert.ToInt32(dt.Rows[0]["PRANNomineeID"].ToString());
            return id;
        }
    }
}

