using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MsgKeywords
    {
        public int AddEditMsgKeywords(int MsgKeywordsID, string MsgKeywordsName, string TableName, string ColumnName, int MsgTypeID)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgKeywordsTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgKeywordsTableAdapter())
            {
                dt = obj.AddEditMsgKeywords(MsgKeywordsID, MsgKeywordsName, TableName, ColumnName, MsgTypeID);
            }
            id = Convert.ToInt32(dt.Rows[0]["MsgKeywordsID"].ToString());
            return id;
        }

        public DataTable ManageMsgKeywords(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgKeywordsTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgKeywordsTableAdapter())
            {
                dt = obj.ManageMsgKeywords(Action, ID);
            } return dt;
        }
    }
}

