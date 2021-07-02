using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for ChkSum
/// </summary>
public static class ChkSum
{
    public static string HmacSha512Digest_CheckSum(string text, string key)
    {
        byte[] keyByte = Enumerable.Range(0, key.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(key.Substring(x, 2), 16)).ToArray();
        byte[] inputBytes = Encoding.UTF8.GetBytes(text);
        using (var hmac = new HMACSHA512(keyByte))
        {
            byte[] hashValue = hmac.ComputeHash(inputBytes);
            return System.Convert.ToBase64String(hashValue);
        }
    }
}