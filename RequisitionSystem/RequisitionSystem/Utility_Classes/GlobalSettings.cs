using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.IO;
using System.Globalization;
using System.Net;
using System.Xml;

public sealed class GlobalSettings
{
    //static GlobalSettings() { }
    #region [Singleton Implementation]
    private static readonly GlobalSettings instance = new GlobalSettings();

    static GlobalSettings()
    {
    }

    private GlobalSettings()
    {
    }

    public static GlobalSettings Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion [Singleton Implementation]

    public static string CaptchaText
    {
        get
        {
            if (HttpContext.Current.Session["CaptchaText"] != null)
                return HttpContext.Current.Session["CaptchaText"].ToString();
            else
                return "";
        }
        set
        {
            HttpContext.Current.Session["CaptchaText"] = value;
        }
    }
    public static string pDateTimeFormat
    {
        get
        {
            if (HttpContext.Current.Session["pDateTimeFormat"] != null)
                return HttpContext.Current.Session["pDateTimeFormat"].ToString();
            else
                return "";
        }
        set
        {
            HttpContext.Current.Session["pDateTimeFormat"] = value;
        }
    }
    public static string pSPName
    {
        get
        {
            if (HttpContext.Current.Session["pSPName"] != null)
                return HttpContext.Current.Session["pSPName"].ToString();
            else
                return "";
        }
        set
        {
            HttpContext.Current.Session["pSPName"] = value;
        }
    }
    public static string pID
    {
        get
        {
            if (HttpContext.Current.Session["pID"] != null)
                return HttpContext.Current.Session["pID"].ToString();
            else
                return "";
        }
        set
        {
            HttpContext.Current.Session["pID"] = value;
        }
    }
    public static string pUserName
    {
        get
        {
            if (HttpContext.Current.Session["pUserName"] != null)
                return HttpContext.Current.Session["pUserName"].ToString();
            else
                return "";
        }
        set
        {
            HttpContext.Current.Session["pUserName"] = value;
        }
    }
    public static UserSec oUserMaster
    {
        get
        {
            if (HttpContext.Current.Session["oUserMaster"] != null)
                return HttpContext.Current.Session["oUserMaster"] as UserSec;
            else
                return new UserSec();
        }
        set
        {
            HttpContext.Current.Session["oUserMaster"] = value;
        }
    }
}