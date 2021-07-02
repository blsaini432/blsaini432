using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BLL;

public partial class Root_Admin_ManageEmployee : System.Web.UI.Page
{
    #region [Properties]
    clsEmployee objEmployee = new clsEmployee();
    clsEmployeeType objEmployeeType = new clsEmployeeType();
    clsImageResize objImageResize = new clsImageResize();
    DataTable TreeDT = new DataTable();
    cls_connection cls = new cls_connection();
    #endregion

    #region [Page Load]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            fillEmployeeType();
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                lblAddEdit.Text = "Update Employee";
                BindTree(Convert.ToInt32(Request.QueryString["id"]));
            }
            else
            {
                lblAddEdit.Text = "Add Employee";
                BindTree(0);
            }
        }
    }
    #endregion
    public void BindTree(int id)
    {

        //DataTable TreeDT = new DataTable();
        TreeMenu.Nodes.Clear();
        string MenuStr = 1.ToString();
        DataTable rightstable = new DataTable();
        if (id > 0)
        {
            rightstable = cls.select_data_dt("Select MenuIDStr from tblEmployee where EmployeeID='" + Convert.ToString(id) + "'");
        }
        TreeDT = cls.select_data_dt("exec ProcMLM_ManageMenuAdmin1 'GetAll'," + Convert.ToInt32(id));
        DataRow[] dr = TreeDT.Select("MenuLevel=1");
        for (int i = 0; i < dr.Length; i++)
        {
            TreeNode mNode = new TreeNode();
            mNode.Expanded = false;
            mNode.Text = dr[i]["MenuName"].ToString();
            mNode.Value = dr[i]["MenuID"].ToString();
            mNode.Checked = Convert.ToBoolean(dr[i]["Checked"].ToString());
            //if (rightstable.Rows.Count > 0)
            //{
            //    if (Convert.ToString("," + rightstable.Rows[0]["MenuIDStr"].ToString() + ",").ToString().IndexOf("," + dr[i]["MenuID"].ToString() + ",") > -1)
            //    {
            //        mNode.Checked = true;
            //    }
            //    else
            //    {
            //        mNode.Checked = false;
            //    }
            //}
            mNode.SelectAction = TreeNodeSelectAction.Expand;
            mNode.PopulateOnDemand = true;
            TreeMenu.Nodes.Add(mNode);
            mNode.ExpandAll();
        }

    }
    public void ShowData(TreeNode Tnode)
    {
        //TreeMenu.Nodes.Clear();
        DataRow[] drnod = TreeDT.Select("ParentID='" + Tnode.Value + "'");
        DataTable rightstable = new DataTable();
        //if (Request.QueryString["id"] != null)
        //{
        //    rightstable = cls.select_data_dt("Select MenuIDStr from tblEmployee where EmployeeId='" + Request.QueryString["id"] + "'");
        //}
        for (int j = 0; j < drnod.Length; j++)
        {
            TreeNode nod = new TreeNode();
            nod.Value = drnod[j]["MenuID"].ToString();
            nod.Text = drnod[j]["MenuName"].ToString();
            nod.Checked = Convert.ToBoolean(drnod[j]["Checked"]);
            //if (rightstable.Rows.Count > 0)
            //{
            //    if (Convert.ToString("," + rightstable.Rows[0]["MenuIDStr"].ToString() + ",").ToString().IndexOf("," + drnod[j]["MenuID"].ToString() + ",") > -1)
            //    {
            //        nod.Checked = true;
            //    }
            //    else
            //    {
            //        nod.Checked = false;
            //    }
            //}
            nod.PopulateOnDemand = true;
            nod.Expanded = false;
            nod.SelectAction = TreeNodeSelectAction.Expand;
            Tnode.ChildNodes.Add(nod);
            nod.ExpandAll();
        }

    }
    protected void TreeMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNodeCollection nodes = this.TreeMenu.SelectedNode.ChildNodes;

        foreach (TreeNode n in nodes)
        {
            GetNodeRecursive(n);
        }
    }
    private void GetNodeRecursive(TreeNode treeNode)
    {

        if (treeNode.Checked == true)
        {

            //Your Code Goes Here to perform any action on checked node
        }
        foreach (TreeNode tn in treeNode.ChildNodes)
        {
            GetNodeRecursive(tn);
        }

    }

    #region [Submit Button]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string MenuIDStr = "";
            foreach (TreeNode node in TreeMenu.CheckedNodes)
            {
                if (node.Checked)
                    MenuIDStr = MenuIDStr + node.Value + ",";
            }

            if (Request.QueryString["id"] == null)
            {
                int EmployeeTypeID = Convert.ToInt32(ddlEmployeeType.SelectedValue);
                string RegisterSources = "By Super Admin Panel";
                string LastLoginIP = Convert.ToString(Request.UserHostAddress);
                DateTime LastLoginDate = DateTime.Now;
                string EmployeeImage = "";// uploadEmployeeImage();
                string CompanyName = "";
                string BranchName = "";
                Int32 intresult = 0;
                intresult = objEmployee.AddEditEmployee(0, EmployeeTypeID, txtEmployeeName.Text, ckEmployeeDesc.Text, EmployeeImage, txtEmail.Text, Convert.ToInt32(txtAge.Text), txtLoginID.Text, txtPassword.Text, txtMobile.Text, txtSTDCode.Text, txtLadline.Text, txtAddress.Text, RegisterSources, CompanyName, BranchName, MenuIDStr, LastLoginIP, LastLoginDate);
                if (intresult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Record updated successfully');location.replace('ListEmployee.aspx');", true);
                   
                    clear();
                }
                else

                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|LoginID Already Exists !');", true);
                   
                }
            }
            else
            {
                int EmployeeTypeID = Convert.ToInt32(ddlEmployeeType.SelectedValue);
                string RegisterSources = " ";
                string LastLoginIP = Convert.ToString(Request.UserHostAddress);
                DateTime LastLoginDate = DateTime.Now;
                string EmployeeImage = "";//uploadEmployeeImage();
                string CompanyName = "";
                string BranchName = "";
                Int32 intresult = 0;
                intresult = objEmployee.AddEditEmployee(Convert.ToInt32(Request.QueryString["id"]), EmployeeTypeID, txtEmployeeName.Text, ckEmployeeDesc.Text, EmployeeImage, txtEmail.Text, Convert.ToInt32(txtAge.Text), txtLoginID.Text, txtPassword.Text, txtMobile.Text, txtSTDCode.Text, txtLadline.Text, txtAddress.Text, RegisterSources, CompanyName, BranchName, MenuIDStr, LastLoginIP, LastLoginDate);
                if (intresult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Record updated successfully');location.replace('ListEmployee.aspx');", true);
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|LoginID Already Exists !');", true);
                }
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Sorry for the inconvenience caused, Please enter valid values !');", true);
            
        }
    }
    #endregion

    #region [Reset]
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();

    }
    #endregion
    #region [Clear Button]
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        clear();
    }
    #endregion

    #region [All Function-FillDate,Clear,UplaodImageUrl]
    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = objEmployee.ManageEmployee("GetAll", id);
        if (dt.Rows.Count > 0)
        {
            txtEmployeeName.Text = Convert.ToString(dt.Rows[0]["EmployeeName"]);
            txtEmail.Text = Convert.ToString(dt.Rows[0]["Email"]);
            txtAge.Text = Convert.ToString(dt.Rows[0]["Age"]);
            txtLoginID.Text = Convert.ToString(dt.Rows[0]["LoginID"]);
            txtPassword.Attributes.Add("value", Convert.ToString(dt.Rows[0]["Password"]));
            txtMobile.Text = Convert.ToString(dt.Rows[0]["Mobile"]);
            txtSTDCode.Text = Convert.ToString(dt.Rows[0]["STDCode"]);
            txtLadline.Text = Convert.ToString(dt.Rows[0]["Ladline"]);
            txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
            ckEmployeeDesc.Text = Convert.ToString(dt.Rows[0]["EmployeeDesc"]);
            fillEmployeeType();
            ddlEmployeeType.SelectedValue = Convert.ToString(dt.Rows[0]["EmployeeTypeID"]);
        }
    }

    private void clear()
    {
        ddlEmployeeType.SelectedIndex = 0;
        txtEmployeeName.Text = "";
        ckEmployeeDesc.Text = "";
        txtEmail.Text = "";
        txtAge.Text = "0";
        txtLoginID.Text = "";
        txtPassword.Text = "";
        txtMobile.Text = "";
        txtSTDCode.Text = "";
        txtLadline.Text = "";
        txtAddress.Text = "";
    }

 
    public void fillEmployeeType()
    {
        DataTable dtEmployeeType = new DataTable();
        dtEmployeeType = objEmployeeType.ManageEmployeeType("GetForAdmin", 0);
        ddlEmployeeType.DataSource = dtEmployeeType;
        ddlEmployeeType.DataValueField = "EmployeeTypeID";
        ddlEmployeeType.DataTextField = "EmployeeTypeName";
        ddlEmployeeType.DataBind();
        ddlEmployeeType.Items.Insert(0, new ListItem("Select Employee Type", "0"));
    }

    #endregion

    protected void TreeMenu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {

    }
    protected void TreeMenu_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        ShowData(e.Node);
    }
}
