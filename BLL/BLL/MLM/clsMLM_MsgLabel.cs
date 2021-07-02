using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_MsgLabel
    {
        public int AddEditMsgLabel(int MsgLabelID, string MsgLabelName)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgLabelTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgLabelTableAdapter())
            {
                dt = obj.AddEditMsgLabel(MsgLabelID, MsgLabelName);
            }
            id = Convert.ToInt32(dt.Rows[0]["MsgLabelID"].ToString());
            return id;
        }

        public DataTable ManageMsgLabel(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_MsgLabelTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_MsgLabelTableAdapter())
            {
                dt = obj.ManageMsgLabel(Action, ID);
            } return dt;
        }
    }
}

