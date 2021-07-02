using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MemberTree
    {
        public int AddEditMemberTree(int MemberTreeID, int MsrNo, DateTime DOA, DateTime DOJ, int IntroMsrNo, int ParentMsrNo, string ParentStr, string IntroStr, int LeftMsrNo, int RightMsrNo, int TotalMembers, int TotalActive, int TotalDirect, decimal TotalPV, decimal PV, int Xside, int Yside, int DXside, int DYside, decimal XPV, decimal YPV, decimal DXPV, decimal DYPV, int PaidX, int PaidY, int CarryX, int CarryY, decimal PaidPairs, int LevelNumber, decimal LevelIncome, int PoolLevel, decimal PoolAmount, decimal Binary, decimal Direct, decimal DirectSpill, decimal SingleLeg, decimal RoyaltyI, decimal RoyaltyII, decimal Award, decimal Reward, string RewardStr, string Gen_In, decimal Generation, decimal Performance)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberTreeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberTreeTableAdapter())
            {
                dt = obj.AddEditMemberTree(MemberTreeID, MsrNo, DOA, DOJ, IntroMsrNo, ParentMsrNo, ParentStr, IntroStr, LeftMsrNo, RightMsrNo, TotalMembers, TotalActive, TotalDirect, TotalPV, PV, Xside, Yside, DXside, DYside, XPV, YPV, DXPV, DYPV, PaidX, PaidY, CarryX, CarryY, PaidPairs, LevelNumber, LevelIncome, PoolLevel, PoolAmount, Binary, Direct, DirectSpill, SingleLeg, RoyaltyI, RoyaltyII, Award, Reward, RewardStr, Gen_In, Generation, Performance);
            }
            id = Convert.ToInt32(dt.Rows[0]["MemberTreeID"].ToString());
            return id;
        }

        public DataTable ManageMemberTree(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MemberTreeTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MemberTreeTableAdapter())
            {
                dt = obj.ManageMemberTree(Action, ID);
            } return dt;
        }
    }
}

