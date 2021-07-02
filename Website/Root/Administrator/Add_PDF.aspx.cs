using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Root_Administrator_Add_PDF : System.Web.UI.Page
{
    cls_connection Cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    //byte[] buffer;
    //protected void BtnUpload_Click(object sender, EventArgs e)
    //{
    //    if (FileUpload1.HasFile && FileUpload1.PostedFile != null
    //           && FileUpload1.PostedFile.FileName != "")
    //    {
    //        HttpPostedFile file = FileUpload1.PostedFile;

    //        buffer = new byte[file.ContentLength];
    //        buffer = Convert();
    //        int bytesReaded = file.InputStream.Read(buffer, 0,
    //        FileUpload1.PostedFile.ContentLength);
    //        if (bytesReaded > 0)
    //        {
    //            try
    //            {
    //                cls_connection Cls = new cls_connection();
    //                Cls.select_data_dt("INSERT INTO tblVedio (Video, Video_Name, Video_Size)" +
    //                   " VALUES ('" + buffer + "', '" + FileUpload1.FileName + "', '" + file.ContentLength + "')");

    //            }
    //            catch (Exception ex)
    //            {
    //                Label1.Text = ex.Message.ToString();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Label1.Text = "Choose a valid video file";
    //    }
    //}
    byte[] buffer;
    SqlConnection connection;
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        if (pdf_file.HasFile && pdf_file.PostedFile != null
              && pdf_file.PostedFile.FileName != "")
        {
            var pdfName2 = DateTime.Now.Ticks + Path.GetFileName(pdf_file.FileName);
            var path2 = Path.Combine(Server.MapPath("../../Uploads/UploadFile/"), pdfName2);
            pdf_file.SaveAs(path2);
            try
            {
                connection = new SqlConnection("Data Source=147.139.34.181,1232; Initial Catalog=sivapay_db;User ID=sa; Password=sc^5rDV!bthacjVBa^S;");
                SqlCommand cmd = new SqlCommand
                  ("INSERT INTO tblVedio (IsActive,title,Pdf_file)" +
                   " VALUES ( @IsActive,@title,@Pdf_file)", connection);
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = 1;
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = txt_title.Text;
                //   cmd.Parameters.Add("@Link", SqlDbType.NVarChar).Value = txt_link.Text;
                cmd.Parameters.Add("@Pdf_file", SqlDbType.NVarChar).Value = pdfName2;
                using (connection)
                {
                    connection.Open();
                    cmd.ExecuteReader();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showSwal('success-message');", true);

                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message.ToString() + "');", true);
            }
            finally
            {
                connection.Close();
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('please Upload PDF !');", true);
        }
        
    }
}