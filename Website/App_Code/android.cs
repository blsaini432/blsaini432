using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for android
/// </summary>
public class android
{
	public android()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string SendNotification(string deviceid, string message)
    {
        try
        {
            var applicationID = "AAAArY5BNNQ:APA91bFwpL8DjtM0v0H6HI5Ph5tABo0UCf6QSTPm5sFNHsVX5J99x39rxMgeCcOC1vpzhCGxPcAc-i-hwLTgjbSlgBCUvNONPsdYTasxdw_8rw0hHvcLiapOG3K27lreOkAhgQaRHHo4";
            var senderId = "745415980244";
            string deviceId = deviceid;
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            var datas = new
            {
                to = deviceId,
                notification = new
                {
                    body = message,
                    title = "Namma Kendra",
                    icon = "https://sumitgroups.in/Images/Company/actual/uicon.png"
                },
                data = new
                {
                    body = message,
                    title = "Namma Kendra",
                    icon = "https://sumitgroups.in/Images/Company/actual/uicon.png"
                }
                //,
                //priority = "high"
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(datas);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentLength = byteArray.Length;

            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();
                            return sResponseFromServer;
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            return ex.Message;
        } 
    }
    public string SendNotification_GCM(string deviceid, string message)
    {
        string SERVER_API_KEY = "AIzaSyAfGdYZsMZC-A7Lv2ueodP_2am5uVS0SU4";
        var SENDER_ID = "225446226210";// "821035043967";
        var value = message;
        WebRequest tRequest;
        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        tRequest.Method = "post";
        tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
        tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

        string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + deviceid + "";
        Console.WriteLine(postData);
        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;

        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        WebResponse tResponse = tRequest.GetResponse();

        dataStream = tResponse.GetResponseStream();

        StreamReader tReader = new StreamReader(dataStream);

        String sResponseFromServer = tReader.ReadToEnd();


        tReader.Close();
        dataStream.Close();
        tResponse.Close();
        return sResponseFromServer;
    }
}