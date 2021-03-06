using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using Newtonsoft.Json;

/// <summary>
/// Summary description for ListToJson
/// </summary>
public static class ListToJson
{
    public static string ToJSON(this object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }

    public static string ToJSON(this object obj, int recursionDepth)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.RecursionLimit = recursionDepth;
        return serializer.Serialize(obj);
    }

    public static string DataTableToJsonWithJsonNet(DataTable table)
    {
        string jsonString = string.Empty;
        if (table.Rows.Count > 0)
        {
            jsonString = JsonConvert.SerializeObject(table);
        }
        return jsonString;
    }
}