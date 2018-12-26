using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

public static class UrlGenerator
{
    private static ConcurrentDictionary<string, string> _cacheForStaticContent = new ConcurrentDictionary<string, string>();

    public static string DatedContent(this UrlHelper urlHelper, string contentPath)
    {
        string tag = "";
        var datedPath = new StringBuilder(contentPath);
        if (!_cacheForStaticContent.ContainsKey(contentPath))
        {
            tag = getModifiedDate(contentPath);
            _cacheForStaticContent.GetOrAdd(contentPath, tag);
        }
        else
            _cacheForStaticContent.TryGetValue(contentPath, out tag);
        datedPath.AppendFormat("{0}m={1}",
                               contentPath.IndexOf('?') >= 0 ? '&' : '?',
                               tag);
        return urlHelper.Content(datedPath.ToString());
    }

    public static string VersionedContent(this UrlHelper urlHelper, string contentPath)
    {
        string cdnBaseUri = ConfigurationManager.AppSettings["IsCDNEnabled"] != null ? (ConfigurationManager.AppSettings["IsCDNEnabled"].ToString() == "true" ? (ConfigurationManager.AppSettings["CDNBaseURL"] != null && !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["CDNBaseURL"].ToString()) ? ConfigurationManager.AppSettings["CDNBaseURL"].ToString() : string.Empty) : string.Empty) : string.Empty;
        string strVersion = ConfigurationManager.AppSettings["AppContentVersion"] != null && !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["AppContentVersion"].ToString()) ? Convert.ToString(ConfigurationManager.AppSettings["AppContentVersion"]) : "1.0.0.0";

        if (!string.IsNullOrWhiteSpace(cdnBaseUri))
            contentPath = string.Format("{0}{1}", cdnBaseUri, contentPath.Replace("~", "").ToLower());

        StringBuilder vPath = new StringBuilder(contentPath);

        vPath.AppendFormat("{0}v={1}", contentPath.IndexOf('?') >= 0 ? '&' : '?', strVersion);
        return urlHelper.Content(vPath.ToString());
    }

    private static string getModifiedDate(string contentPath)
    {
        return System.IO.File.GetLastWriteTime(HostingEnvironment.MapPath(contentPath)).ToString("yyyyMMddhhmmss");
    }
}