using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DAL;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Collections.Generic;
/// <summary>
/// Summary description for cls_connection
/// </summary>
/// 

public class cls_connection
{
    SqlConnection con = new SqlConnection();
    SqlDataAdapter ad = null;
    SqlCommand cmd = null;
    int result = 0;

    public cls_connection()
    {
        try
        {
            DAL.Class1 xx = new Class1();
            string str = xx.connstr();
            //   con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            con.ConnectionString = str;
        }
        catch (Exception ex)
        {
            //  RMG.Functions.MsgBox(ex.Message);       
        }

        // TODO: Add constructor logic here

    }

    //  Function for open & close the connection
    #region connection_check

    public void open_connection()
    {
        try
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }
        catch (Exception ex)
        {
            //  RMG.Functions.MsgBox(ex.Message);
        }
    }

    public void close_connection()
    {
        try
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        catch (Exception ex)
        {
            //  RMG.Functions.MsgBox(ex.Message);
        }
    }



    #endregion

    //  Function for fetch the data 
    #region data_functions

    public DataSet select_data_ds(string str)
    {

        open_connection();
        DataSet ds = new DataSet();
        ad = new SqlDataAdapter(str, con);
        ad.Fill(ds);
        close_connection();
        return ds;

    }
    public DataTable select_data_dt(string str)
    {

        DataTable dt = new DataTable();
        try
        {
            open_connection();
            ad = new SqlDataAdapter(str, con);
            ad.Fill(dt);
            close_connection();
        }
        catch (Exception ex)
        {

        }
        return dt;

    }

    public SqlDataReader select_data_dr(string str)
    {

        open_connection();
        cmd = new SqlCommand(str, con);
        SqlDataReader dtr = cmd.ExecuteReader();
        // close_connection();
        return dtr;

    }

    public int select_data_scalar_int(string str)
    {

        open_connection();
        cmd = new SqlCommand(str, con);

        if (cmd.ExecuteScalar() != DBNull.Value)
            result = Convert.ToInt32(cmd.ExecuteScalar());
        else
            result = 0;

        close_connection();
        return result;

    }

    public double select_data_scalar_double(string str)
    {
        double result1;
        open_connection();
        cmd = new SqlCommand(str, con);
        if (cmd.ExecuteScalar() != DBNull.Value)
            result1 = Convert.ToDouble(cmd.ExecuteScalar());
        else
            result1 = 0;

        close_connection();
        return result1;

    }

    public long select_data_scalar_long(string str)
    {
        long result1;
        open_connection();
        cmd = new SqlCommand(str, con);
        if (cmd.ExecuteScalar() != DBNull.Value)
            result1 = Convert.ToInt64(cmd.ExecuteScalar());
        else
            result1 = 0;

        close_connection();
        return result1;

    }

    public string select_data_scalar_string(string str)
    {
        string result1;
        open_connection();
        cmd = new SqlCommand(str, con);
        if (cmd.ExecuteScalar() != DBNull.Value)
            result1 = Convert.ToString(cmd.ExecuteScalar());
        else
            result1 = "";

        close_connection();
        return result1;

    }

    public int insert_data(string str)
    {

        open_connection();
        cmd = new SqlCommand(str, con);
        int x = cmd.ExecuteNonQuery();
        close_connection();
        return x;
    }

    public int delete_data(string str)
    {
        open_connection();
        cmd = new SqlCommand(str, con);
        int x = cmd.ExecuteNonQuery();
        close_connection();
        return x;

    }

    public int update_data(string str)
    {

        open_connection();
        cmd = new SqlCommand(str, con);
        int x = cmd.ExecuteNonQuery();
        close_connection();
        return x;

    }
    #endregion
    public void fill_MemberType(DropDownList ddl, string tp)
    {
        ddl.Items.Clear();
        DataTable dt = new DataTable();
        dt = select_data_dt("exec procmlm_GetActiveMemberType '" + tp + "'");
        ddl.DataSource = dt;
        ddl.DataTextField = "membertype";
        ddl.DataValueField = "membertypeid";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("Select Member Type", "0"));

    }
    public string Get_PG_postData(string loginID, string password, string ProdID, string amount, string txnID, string ReturnURL)
    {
        string pg_data = "login=" + loginID + "&pass=" + password + "&ttype=NBFundTransfer&prodid=" + ProdID + "&amt=" + amount + "&txncurr=INR&txnscamt=0&clientcode=007&txnid=" + txnID + "&date=" + DateTime.Now + "&custacc=1234567890&ru=" + ReturnURL + "";
        return pg_data;
    }
    public DataTable ConvertJSONToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        //strip out bad characters
        string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

        //hold column names
        List<string> dtColumns = new List<string>();

        //get columns
        foreach (string jp in jsonParts)
        {
            //only loop thru once to get column names
            string jps = jp.Replace(@""",""", @"#");
            jps = jps.Replace(",", "$");
            jps = jps.Replace(@"#", @""",""");
            string[] propData = Regex.Split(jps.Replace("{", "").Replace("}", ""), ",");
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1);
                    string v = rowData.Substring(idx + 1);
                    if (!dtColumns.Contains(n))
                    {
                        dtColumns.Add(n.Replace("\"", ""));
                    }
                }
                catch (Exception ex)
                {
                    //throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                }

            }
            break; // TODO: might not be correct. Was : Exit For
        }

        //build dt
        foreach (string c in dtColumns)
        {
            dt.Columns.Add(c);
        }
        //get table data
        foreach (string jp in jsonParts)
        {
            string jps = jp.Replace(@""",""", @"#");
            jps = jps.Replace(",", "$");
            jps = jps.Replace(@"#", @""",""");
            string[] propData = Regex.Split(jps.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string v = rowData.Substring(idx + 1).Replace("\"", "").Replace(@"\", "");
                    nr[n] = v;
                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            dt.Rows.Add(nr);
        }
        return dt;
    }

    public DataTable select_data_dtNew(string ProcedureName, List<ParmList> parameterlist)
    {
        DAL.Class1 xx = new Class1();
        string consString = xx.connstr();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        using (SqlConnection connn = new SqlConnection(consString))
        {
            using (SqlCommand cmd = new SqlCommand(ProcedureName))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = connn;

                foreach (var _list in parameterlist)
                {
                    cmd.Parameters.AddWithValue(_list.name, _list.value);
                }

                open_connection();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                close_connection();
            }
        }
        if (ds != null && ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
        }

        return dt;

    }
}