using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using BLL;
using System.Configuration;
using System.Net;
//using QRCoder;
using System.Drawing;
using System.IO;
public partial class Id : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataTable dtMember = (DataTable)Session["dtDistributor"];
            string msrno = dtMember.Rows[0]["Msrno"].ToString();
            genrateqrcode();
            bind(Convert.ToInt32(msrno));
                     
        }
    }
    public void genrateqrcode()
    {

       // string url = "http://" + Request.Url.Host + "/order.pdf";
       // string code = url;
       //// QRCodeGenerator qrGenerator = new QRCodeGenerator();
       //// QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
       // System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
       // imgBarCode.Height = 150;
       // imgBarCode.Width = 150;
       // using (Bitmap bitMap = qrCode.GetGraphic(20))
       // {
       //     using (MemoryStream ms = new MemoryStream())
       //     {
       //         bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
       //         byte[] byteImage = ms.ToArray();
       //         imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
       //     }
       //     plBarCode.Controls.Add(imgBarCode);
       // }
    }
    public void bind(int msrno)
    {
        List<ParmList> _lstparm = new List<ParmList>();
        _lstparm.Add(new ParmList() { name = "@msrno", value = Convert.ToInt32(msrno) });
        cls_connection cls = new cls_connection();
        DataTable dt = cls.select_data_dtNew("TotalIdDashboard ", _lstparm);
        if (dt.Rows.Count > 0)
        {
            lblname.Text = dt.Rows[0]["FullName"].ToString();
            lbldoj.Text = dt.Rows[0]["DOJ"].ToString();
            lblcompanyname.Text = dt.Rows[0]["companyname"].ToString();
            lblcname.Text = dt.Rows[0]["companyname"].ToString();
            lbladdress.Text = dt.Rows[0]["Address"].ToString();
            lbldepartment.Text = dt.Rows[0]["Department"].ToString();
            lblmerchantid.Text = dt.Rows[0]["MemberId"].ToString();
            lblstate.Text = dt.Rows[0]["StateName"].ToString();
            lblwebsite.Text = dt.Rows[0]["Website"].ToString();
            imgcompany.ImageUrl = "../../img/logo.png";
            Image2.ImageUrl = "../../img/logo.png";
        imgcompany.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dt.Rows[0]["companylogo"]);
          Image2.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["companylogo"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/Company/Logo/actual/" + Convert.ToString(dt.Rows[0]["companylogo"]);
         // Image1.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["MemberImage"])) ? "../../Images/Profile/Actual/dummy.png" : "../../Images/Profile/Actual/" + Convert.ToString(dt.Rows[0]["MemberImage"]);
          //  Image1.ImageUrl = "../../images/Profile/actual/" + Convert.ToString(dt.Rows[0]["MemberImage"]);
           Image1.ImageUrl = string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["MemberImage"])) ? "../../Uploads/User/Profile/dummy.png" : "../../Uploads/User/Profile/" + Convert.ToString(dt.Rows[0]["MemberImage"]);
        }
    }
}