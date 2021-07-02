using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class cls_UnitConversionDetail
    {
        public int AddEditUnitConversionDetail(int UnitConversionDetailID, int ProductID, int UnitID, decimal UnitRate, bool UnitMark, int UnitID1, int ConversionQty1, int UnitID2, int ConversionQty2, int UnitID3, int ConversionQty3)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_UnitConversionDetailTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_UnitConversionDetailTableAdapter())
            {
                dt = obj.AddEditUnitConversionDetail(UnitConversionDetailID,ProductID, UnitID, UnitRate, UnitMark, UnitID1, ConversionQty1, UnitID2, ConversionQty2, UnitID3, ConversionQty3);
            }
            id = Convert.ToInt32(dt.Rows[0]["UnitConversionDetailID"].ToString());
            return id;
        }

        public DataTable ManageUnitConversionDetail(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSet2TableAdapters.tbl_UnitConversionDetailTableAdapter obj = new DAL.DataSet2TableAdapters.tbl_UnitConversionDetailTableAdapter())
            {
                dt = obj.ManageUnitConversionDetail(Action, ID);
            } return dt;
        }
    }
}

