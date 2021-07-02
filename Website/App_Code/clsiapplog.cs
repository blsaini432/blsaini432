using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for clslog
/// </summary>
public class clsiapplog
{
    public clsiapplog()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void PrintLog(string Data)
    {
        string url = HttpContext.Current.Server.MapPath("~/applogfile/");
        string fileName = url + "Applog" + DateTime.Now.ToString("dd MMM yyyy") + ".txt";
        FileStream fs = null;
        if (!File.Exists(fileName))
        {
            fs = File.Create(fileName);
        }
        try
        {
            string optime = System.DateTime.Now.ToString();
            string ip = "123455";
            FileStream fs1 = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs1);
            writer.WriteLine(Data + "$" + optime + "$" + ip);
            writer.WriteLine("=======================================================================================");
            writer.Close();
        }
        catch (Exception ex)
        {
        }
    }
    public static string GetIPAddress()
    {
        String strHostName = string.Empty;
        strHostName = Dns.GetHostName();
        Console.WriteLine("Local Machine's Host Name: " + strHostName);
        IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
        IPAddress[] addr = ipEntry.AddressList;
        return addr[2].ToString();
    }
    public void logfile()
    {
        string url = HttpContext.Current.Server.MapPath("~/LogFile/");
        string fileLoc = url + "Applog" + DateTime.Now.ToString("dd MMM yyyy") + ".txt";
        FileStream fs = null;
        if (!File.Exists(fileLoc))
        {
            fs = File.Create(fileLoc);
        }
    }
}