using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class clsMLM_PinMaster
    {
        public int AddEditPinMaster(int PinMasterID, int SerialNumber, string PinNumber,int PackageID,int ForMsrNo, string PinStatus,int UseByMsrNo, DateTime UseDate)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinMasterTableAdapter())
            {
                dt = obj.AddEditPinMaster(PinMasterID, SerialNumber, PinNumber, PackageID, ForMsrNo, PinStatus, UseByMsrNo, UseDate);
            }
            id = Convert.ToInt32(dt.Rows[0]["PinMasterID"].ToString());
            return id;
        }

        public DataTable ManagePinMaster(string Action, int ID)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinMasterTableAdapter())
            {
                dt = obj.ManagePinMaster(Action, ID);
            } return dt;
        }

        public int AddPinGenerate(int PackageID, int Quantity, int MsrNo)
        {
            Int32 id = 0;
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinMasterTableAdapter())
            {
                dt = obj.AddPinGenerate(PackageID,Quantity,MsrNo);
            }
            id = Convert.ToInt32(dt.Rows[0]["PinMasterID"].ToString());
            return id;
        }

        public DataTable PinValidate(int PackageID, long SerialNumber, string PinNumber)
        {
            DataTable dt = new DataTable();
            using (DAL.DataSetMLMTableAdapters.tblMLM_PinMasterTableAdapter obj = new DAL.DataSetMLMTableAdapters.tblMLM_PinMasterTableAdapter())
            {
                dt = obj.PinValidate(PackageID,SerialNumber,PinNumber);
            } return dt;
        }
    }
}

