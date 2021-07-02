using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;

    public partial class Root_Administrator_FileUpload : System.Web.UI.Page
    {
    OleDbConnection Econ;
    SqlConnection con;

    string constr, Query, sqlconn;
    protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindGridview();
            }
        }
        //private void BindGridview()
        //{
        //    string CS = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        SqlCommand cmd = new SqlCommand("spGetAllEmployee", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        con.Open();
        //        GridView1.DataSource = cmd.ExecuteReader();
        //        GridView1.DataBind();
        //    }
        //}
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    if (FileUpload1.PostedFile != null)
    //    {
    //        try
    //        {

    //        Import_To_Grid();
    //string path = string.Concat(Server.MapPath("~/UploadFile/" + FileUpload1.FileName));
    //FileUpload1.SaveAs(path);
    // Connection String to Excel Workbook  

    //switch (fileExtension)
    //{
    //    case ".xls": //Excel 1997-2003
    //        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
    //        break;
    //    case ".xlsx": //Excel 2007-2010
    //        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0 xml;HDR=Yes;IMEX=1\"";
    //        break;
    //}


    //string excelCS = string.Format("Provider=Microsoft.ACE.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 xml;HDR=Yes;IMEX=1\"");
    //    using (OleDbConnection con = new OleDbConnection(excelCS))
    //    {
    //        OleDbCommand cmd = new OleDbCommand("select * from [Employee]", con);
    //        con.Open();
    //        // Create DbDataReader to Data Worksheet  
    //        DbDataReader dr = cmd.ExecuteReader();
    //        // SQL Server Connection String  
    //        string CS = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
    //        // Bulk Copy to SQL Server   
    //        SqlBulkCopy bulkInsert = new SqlBulkCopy(CS);
    //        bulkInsert.DestinationTableName = "Employee";
    //        bulkInsert.WriteToServer(dr);
    //BindGridview();
    //lblMessage.Text = "Your file uploaded successfully";
    //lblMessage.ForeColor = System.Drawing.Color.Green;
    //}
    //        }
    //        catch (Exception ex)
    //        {
    //            lblMessage.Text = "Your file not uploaded";
    //            lblMessage.ForeColor = System.Drawing.Color.Red;
    //        }
    //    }
    //}





    private void ExcelConn(string FilePath)
    {

        constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", FilePath);
        Econ = new OleDbConnection(constr);

    }
    private void connection()
    {
        sqlconn = ConfigurationManager.ConnectionStrings["SqlCom"].ConnectionString;
        con = new SqlConnection(sqlconn);

    }


    private void InsertExcelRecords(string FilePath)
    {
        ExcelConn(FilePath);

        Query = string.Format("Select [Name],[City],[Address],[Designation] FROM [{0}]", "Sheet1$");
        OleDbCommand Ecom = new OleDbCommand(Query, Econ);
        Econ.Open();

        DataSet ds = new DataSet();
        OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
        Econ.Close();
        oda.Fill(ds);
        DataTable Exceldt = ds.Tables[0];
        connection();
        //creating object of SqlBulkCopy  
        SqlBulkCopy objbulk = new SqlBulkCopy(con);
        //assigning Destination table name  
        objbulk.DestinationTableName = "Employee";
        //Mapping Table column  
        objbulk.ColumnMappings.Add("Name", "Name");
        objbulk.ColumnMappings.Add("City", "City");
        objbulk.ColumnMappings.Add("Address", "Address");
        objbulk.ColumnMappings.Add("Designation", "Designation");
        //inserting Datatable Records to DataBase  
        con.Open();
        objbulk.WriteToServer(Exceldt);
        con.Close();


    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string CurrentFilePath = Path.GetFullPath(FileUpload1.PostedFile.FileName);
        InsertExcelRecords(CurrentFilePath);
    }

    protected void Upload(object sender, EventArgs e)
    {
        //Upload and save the file
        
        string excelPath = Server.MapPath("~/UploadFile/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.SaveAs(excelPath);


        string fileLocation = Server.MapPath("upload/pos/" + Path.GetFileName(FileUpload1.PostedFile.FileName));

        string conString = string.Empty;
        string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        switch (extension)
        {
            case ".xls": //Excel 97-03
                conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                break;
            case ".xlsx": //Excel 07 or higher
                conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                break;

        }
        conString = string.Format(conString, excelPath);
        using (OleDbConnection excel_con = new OleDbConnection(conString))
        {
            excel_con.Open();
            string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
            DataTable dtExcelData = new DataTable();

            //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
            dtExcelData.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Salary", typeof(decimal)) });

            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
            {
                oda.Fill(dtExcelData);
            }
            excel_con.Close();

            string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.tblPersons";

                    //[OPTIONAL]: Map the Excel columns with that of the database table
                    sqlBulkCopy.ColumnMappings.Add("Id", "PersonId");
                    sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                    sqlBulkCopy.ColumnMappings.Add("Salary", "Salary");
                    con.Open();
                    sqlBulkCopy.WriteToServer(dtExcelData);
                    con.Close();
                }
            }
        }
    }
    private void Import_To_Grid()
    {

        string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

        string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            
        if (fileExtension != ".xls" && fileExtension != ".xlsx")
        { return; }


        string fileLocation = Server.MapPath("uploadFile/" + FileName);


        if (File.Exists(fileLocation))
        {
            File.Delete(fileLocation);
        }

        FileUpload1.SaveAs(fileLocation);


        string strConn = "";
        switch (fileExtension)
        {
            case ".xls": //Excel 1997-2003
                strConn = "Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                break;
            case ".xlsx": //Excel 2007-2010
                strConn = "Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0 xml;HDR=Yes;IMEX=1\"";
                break;
        }

        //Get the data from the excel sheet1 which is default
        string query = "select * from [Book1]";
        OleDbConnection objConn;
        OleDbDataAdapter oleDA;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objConn = new OleDbConnection(strConn);
        objConn.Open();
        oleDA = new OleDbDataAdapter(query, objConn);
        oleDA.Fill(ds, "ExcelTable");
        DataTable dtable = ds.Tables["ExcelTable"];
        objConn.Close();
        oleDA.Dispose();
        objConn.Dispose();

        //if (dt.Rows.Count > 0)
        //{
        //    DivShow.Style.Add("display", "block");
        //    DivError.Style.Add("display", "none");
        //    lblrecordcount.Text = dt.Rows.Count.ToString();
        //    gv.DataSource = dt;
        //    gv.DataBind();
        //    ViewState["dtExport"] = dt;
        //}
        //else
        //{
        //    DivError.Style.Add("display", "block");
        //    DivShow.Style.Add("display", "none");
        //    dt.Rows.Add(dt.NewRow());
        //    gv.DataSource = dt;
        //    gv.DataBind();
        //    int TotalColumns = gv.Rows[0].Cells.Count;
        //    gv.Rows[0].Cells.Clear();
        //    gv.Rows[0].Cells.Add(new TableCell());
        //    gv.Rows[0].Cells[0].ColumnSpan = TotalColumns;
        //    gv.Rows[0].Cells[0].Text = "No Record Imported";
        //    gv.Rows[0].Cells[0].CssClass = "infoline";

        //}
        //Bind the datatable to the Grid
        //GridView1.DataSource = dt;
        //GridView1.DataBind();

        //Delete the excel file from the server
        File.Delete(fileLocation);
    }



}
