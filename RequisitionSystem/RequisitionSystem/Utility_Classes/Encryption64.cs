using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Encryption64
/// </summary>
public class Encryption64
{
    private byte[] key;
    private byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

    public Encryption64()
    {

    }

    public string Decrypt(string stringToDecrypt, string sEncryptionKey)
    {
        stringToDecrypt = HttpUtility.UrlDecode(stringToDecrypt);
        stringToDecrypt = stringToDecrypt.Replace(" ", "+");
        byte[] inputByteArray = new byte[stringToDecrypt.Length];

        key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        inputByteArray = Convert.FromBase64String(stringToDecrypt);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return System.Text.Encoding.UTF8.GetString(ms.ToArray());
    }

    public string Encrypt(string stringToEncrypt, string sEncryptionKey)
    {
        key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return HttpUtility.UrlEncode(Convert.ToBase64String(ms.ToArray()));
    }

    public static string EncryptQueryString(string strQueryString)
    {
        Encryption64 oES = new Encryption64();
        return oES.Encrypt(strQueryString, "!#$a54?3");
    }

    public static string DecryptQueryString(string strQueryString)
    {
        Encryption64 oES = new Encryption64();
        return oES.Decrypt(strQueryString, "!#$a54?3");
    }

    public static string EncryptString(string str)
    {
        Encryption64 oES = new Encryption64();
        return oES.Encrypt(str, "@n!k$en1");
    }

    public static string DecryptString(string str)
    {
        Encryption64 oES = new Encryption64();
        return oES.Decrypt(str, "@n!k$en1");
    }
}