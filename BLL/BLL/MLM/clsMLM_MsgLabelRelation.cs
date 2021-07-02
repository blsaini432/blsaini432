using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MsgLabelRelation
    {
        public int AddEditMsgLabelRelation(int MsgLabelRelationID, int MsgLabelID, int MsgID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgLabelRelationTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgLabelRelationTableAdapter())
            {
                dt = obj.AddEditMsgLabelRelation(MsgLabelRelationID, MsgLabelID, MsgID);
            }
            id = Convert.ToInt32(dt.Rows[0]["MsgLabelRelationID"].ToString());
            return id;
        }

        public DataTable ManageMsgLabelRelation(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgLabelRelationTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgLabelRelationTableAdapter())
            {
                dt = obj.ManageMsgLabelRelation(Action, ID);
            } return dt;
        }
    }
}

