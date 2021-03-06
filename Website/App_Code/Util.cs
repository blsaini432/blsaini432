using System;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data;

/// <summary>
/// Summary description for Util
/// </summary>
public static class Util
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

    public static string GetTransactionID_New()
    {
        Random rnd = new Random();
        Int64 month = rnd.Next(10000, 99999);
        month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(10000, 99999)));
        return month.ToString();
    }

    public static RetrunResult HttpGet(string url, string Keyname, string Keyval)
    {
        string result = "";
        object data = null;
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        httpreq.Headers[Keyname] = Keyval;
        httpreq.Timeout = 60000;
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            data = results;
            result = "OK";
        }
        catch (Exception ex)
        {
            result = ex.Message.ToString();
        }
        return new RetrunResult() { Result = result, Data = data };
    }

    public static RetrunResult HTTP_POST(string Url, string Data)
    {
        string result = "";
        object data = null;
        string Out = String.Empty;
        System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
        try
        {
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.UTF8.GetBytes(Data);
            req.ContentLength = sentData.Length;
            using (System.IO.Stream sendStream = req.GetRequestStream())
            {
                sendStream.Write(sentData, 0, sentData.Length);
                sendStream.Close();
            }
            System.Net.WebResponse res = req.GetResponse();
            System.IO.Stream ReceiveStream = res.GetResponseStream();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
            {
                Char[] read = new Char[256];
                int count = sr.Read(read, 0, 256);

                while (count > 0)
                {
                    String str = new String(read, 0, count);
                    Out += str;
                    count = sr.Read(read, 0, 256);
                }
            }
            data = Out;
            result = "OK";
        }
        catch (ArgumentException ex)
        {
            result = string.Format("HTTP_ERROR :: The second HttpWebRequest object has raised an Argument Exception as 'Connection' Property is set to 'Close' :: {0}", ex.Message);
        }
        catch (WebException ex)
        {
            result = string.Format("HTTP_ERROR :: WebException raised! :: {0}", ex.Message);
        }
        catch (Exception ex)
        {
            result = string.Format("HTTP_ERROR :: Exception raised! :: {0}", ex.Message);
        }

        return new RetrunResult() { Result = result, Data = data };

    }
}