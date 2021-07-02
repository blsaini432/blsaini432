using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Root_Distributor_All_Vedio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        VedioList();
    }
    public void VedioList()
    {
        cls_myMember clsm = new cls_myMember();
        cls_connection cls = new cls_connection();
        List<ParmList> _list = new List<ParmList>();
        _list.Add(new ParmList() { name = "@Action", value = "video" });
        _list.Add(new ParmList() { name = "@ID", value = 0 });
        string TxnID = clsm.Cyrus_GetTransactionID_New();
        DataTable dt = new DataTable();
        dt = cls.select_data_dtNew("Proc_ManageVedio", _list);
        gvVideo.DataSource = dt;
        gvVideo.DataBind();
    }
}