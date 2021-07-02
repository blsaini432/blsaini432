using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Root_Administrator_Add_Vedio : System.Web.UI.Page
{
    cls_connection Cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    byte[] buffer;
    SqlConnection connection;
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile && FileUpload1.PostedFile != null
               && FileUpload1.PostedFile.FileName != "")
        {
            string Extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();

            var fileName = DateTime.Now.Ticks + Path.GetFileName(FileUpload1.FileName);
            var path = Path.Combine(Server.MapPath("../../Uploads/UploadFile/"), fileName);
            FileUpload1.SaveAs(path);
         
            HttpPostedFile file = FileUpload1.PostedFile;
            //retrieve the HttpPostedFile object
            buffer = new byte[file.ContentLength];
            int bytesReaded = file.InputStream.Read(buffer, 0,
            FileUpload1.PostedFile.ContentLength);
            if (bytesReaded > 0)
            {
                try
                {
                    connection = new SqlConnection("Data Source=147.139.34.181,1232; Initial Catalog=sivapay_db;User ID=sa; Password=sc^5rDV!bthacjVBa^S;");
                    SqlCommand cmd = new SqlCommand
                      ("INSERT INTO tblVedio (Video_Name, Video_Size, IsActive,title)" +
                       " VALUES ( @videoName, @videoSize, @IsActive,@title)", connection);
                    //cmd.Parameters.Add("@video",
                    //     SqlDbType.VarBinary, buffer.Length).Value = buffer;
                    cmd.Parameters.Add("@videoName", SqlDbType.NVarChar).Value = fileName;
                    cmd.Parameters.Add("@videoSize", SqlDbType.BigInt).Value = file.ContentLength;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = 1;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = txt_title.Text;
                   
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

        }
        else
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('WarningChoose valid vedio !');", true);
        }
    }
}