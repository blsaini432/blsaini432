using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using BLL;
using System.Configuration;
using Common;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class Root_Administrator_Uploadearning : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region [Properties]
    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    cls_Universal objUniversal = new cls_Universal();
    DataTable dtMemberMaster = new DataTable();
    DataTable dtExport = new DataTable();
    DataTable dtPackage = new DataTable();
    cls_connection cls = new cls_connection();
    cls_myMember clsm = new cls_myMember();
    #endregion

    #region Method
    //[WebMethod]
    //public static List<TargetSheet> BindTarget()
    //{
    //    DataTable dt = new DataTable();
    //    List<TargetSheet> TargetSheetList = new List<TargetSheet>();
    //    cls_connection cls = new cls_connection();
    //    clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
    //    dt = cls.select_data_dt(@"select * from tblorders ");
    //    foreach (DataRow dtrow in dt.Rows)
    //    {
    //        TargetSheet TargetSheet = new TargetSheet();
    //        TargetSheet.MemberID = dtrow["MemberID"].ToString();
    //        TargetSheet.AddDate = dtrow["AddDate"].ToString();
           
    //        TargetSheetList.Add(TargetSheet);
    //    }
    //    return TargetSheetList.OrderByDescending(m => m.AddDate).ToList();
    //}


    public static string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(3, 2) + "-" + ddmmyy.Substring(0, 2) + "-" + ddmmyy.Substring(6, 4);
        return mmddyy;
    }
    //[WebMethod]
    //public static List<TargetSheet> Targetreportbydate(string fromdate, string todate)
    //{
    //    int MsrNo = Convert.ToInt32(0);
    //    DataTable dtRecord = new DataTable();
    //    List<TargetSheet> ArchievList = new List<TargetSheet>();
    //    cls_connection cls = new cls_connection();
    //    List<ParmList> _lstparm = new List<ParmList>();
    //    _lstparm.Add(new ParmList() { name = "@dtfrom", value = changedatetommddyy(fromdate) });
    //    _lstparm.Add(new ParmList() { name = "@dateto", value = changedatetommddyy(todate) });
    //    dtRecord = cls.select_data_dtNew("sp_Target_Filter", _lstparm);
    //    foreach (DataRow dtrow in dtRecord.Rows)
    //    {
    //        TargetSheet Target = new TargetSheet();
    //        Target.MemberID = dtrow["MemberID"].ToString();
    //        Target.Name = dtrow["FirstName"].ToString() + " " + dtrow["LastName"].ToString();
    //        Target.AddDate = dtrow["AddDate"].ToString();
    //        Target.Target = dtrow["Target"].ToString();
    //        Target.Achievement = dtrow["Achievement"].ToString();
    //        Target.DRR = dtrow["DRR"].ToString();
    //        Target.Pending = dtrow["Pending"].ToString();
    //        Target.Achievementpercent = dtrow["Achievementpercent"].ToString();
    //        Target.TimePeriod = dtrow["TimePeriod"].ToString();
    //        ArchievList.Add(Target);
    //    }
    //    return ArchievList.OrderByDescending(m => m.AddDate).ToList();
    //}


    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataTable dtExport = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
        dtExport = cls.select_data_dt(@"select * from tblearning");
        if (dtExport.Rows.Count > 0)
        {
            Common.Export.ExportToExcel(dtExport, "earning_Report");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records to genrate excel');", true);
        }
    }


    protected void btn_exportMember_Click(object sender, EventArgs e)
    {
        DataTable dtExport = new DataTable();
        List<Customer> custList = new List<Customer>();
        cls_connection cls = new cls_connection();
        clsMLM_MemberMaster objMemberMaster = new clsMLM_MemberMaster();
       // dtExport = cls.select_data_dt(@"select MemberID,FirstName,LastName from tblmlm_membermaster where MemberID != '100000'");
       // if (dtExport.Rows.Count > 0)
        
            dtExport.Columns.Add("title");
            //dtExport.Columns.Add("MemberID");
            dtExport.Columns.Add("AddDate");
            dtExport.Columns.Add("price");
            dtExport.Columns.Add("quantity");
            Common.Export.ExportToExcel(dtExport, "order");
        
       
    }


    protected void Upload(object sender, EventArgs e)
    {
        string filePath = string.Empty;
        string AddDate = System.DateTime.Now.ToString("dd-MM-yyyy");
        string path = Server.MapPath("~/Uploads/UploadFile/");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
        if (filename != "")
        {
            filePath = path + Path.GetFileName(FileUpload1.PostedFile.FileName);
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            if (extension == ".xlsx")
            {
                FileUpload1.SaveAs(filePath);
                string conString = string.Empty;
                switch (extension)
                {
                    case ".xlsx": //Excel 07 and above.
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);
                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                conString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.tblearnings";
                        sqlBulkCopy.ColumnMappings.Add("MemberID", "MemberID");
                        sqlBulkCopy.ColumnMappings.Add("title", "title");
                        sqlBulkCopy.ColumnMappings.Add("price", "price");
                        sqlBulkCopy.ColumnMappings.Add("quantity", "quantity");
                        sqlBulkCopy.ColumnMappings.Add("addfee", "addfee");
                        sqlBulkCopy.ColumnMappings.Add("AddDate", "AddDate");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Todays Earning Data Added Successfully');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('File Extension must be .xlsx');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select File!');", true);
        }
    }

    public class Customer
    {
        public string MsrNo { get; set; }
        public string MemberID { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string MemberType { get; set; }
        public string Owner { get; set; }
        public string Package { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string TransactionPassword { get; set; }
        public string isuti { get; set; }
        public string isdmr { get; set; }
        public string isxpressdmr { get; set; }
        public string isbbps { get; set; }
        public string isbus { get; set; }
        public string isflight { get; set; }
        public string ishotel { get; set; }
        public string isrecharge { get; set; }
        public string ispancard { get; set; }
        public string isaeps { get; set; }
        public string isprepaidcard { get; set; }
        public string bankname { get; set; }
        public string bankifsc { get; set; }
        public string bankac { get; set; }
        public string isaepspayout { get; set; }
        public string isemailverify { get; set; }
    }

    public class TargetSheet
    {
        public string MemberID { get; set; }
        public string AddDate { get; set; }
        public string Name { get; set; }
        public string Target { get; set; }
        public string Achievement { get; set; }
        public string DRR { get; set; }
        public string Pending { get; set; }
        public string Achievementpercent { get; set; }
        public string TimePeriod { get; set; }
    }
    #endregion
}