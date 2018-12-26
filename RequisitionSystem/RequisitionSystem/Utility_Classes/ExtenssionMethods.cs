using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

public static class ExtenssionMethods
{
    public static string GetStringSha256Hash(this string text)
    {
        if (String.IsNullOrEmpty(text))
            return String.Empty;

        using (var sha = new System.Security.Cryptography.SHA256Managed())
        {
            byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] hash = sha.ComputeHash(textData);
            return BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();
        }
    }

    public static string FilterSpecialSymbol(this String str)
    {
        string filteredString = string.Empty;
        filteredString = str.Replace("'", "").Replace("<", "").Replace(">", "").Replace('"', ' ').Replace("?", "").Replace("~", "");
        return filteredString;
    }

    public static string FilterSpecialSymbol(this String str, string removeSymbol)
    {
        string filteredString = string.Empty;
        filteredString = str.Replace(removeSymbol, "");
        return filteredString;
    }

    public static string FilterSpecialSymbol(this String str, string[] removeSymbolList)
    {
        string filteredString = string.Empty;
        foreach (string removeSymbol in removeSymbolList)
        {
            str = str.Replace(removeSymbol, "");
        }
        filteredString = str;
        return filteredString;
    }

    public static bool ValidateEmail(this String str)
    {
        bool isEmail = Regex.IsMatch(str, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        return isEmail;
    }

    public static bool ValidateIndianMobileNumber(this String str)
    {
        //Regex mobilePattern = new Regex(@"^[1-9]\d{10}$");
        Regex mobilePattern = new Regex("^[7-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$");
        //Regex mobilePattern = new Regex(@"^\d{10}$");
        //Regex mobilePattern = new Regex(@"^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}9[0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}[0-9]{7}$");
        return mobilePattern.IsMatch(str);
    }

    public static IEnumerable<T> Select<T>(this System.Data.SqlClient.SqlDataReader reader, Func<System.Data.SqlClient.SqlDataReader, T> projection)
    {
        while (reader.Read())
        {
            yield return projection(reader);
        }
    }
}