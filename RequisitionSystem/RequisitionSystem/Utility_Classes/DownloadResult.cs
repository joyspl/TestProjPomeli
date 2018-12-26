using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

public class DownloadResult : ActionResult
{
    public DownloadResult() { }
    public DownloadResult(string path)
    {
        this.Path = path;
    }
    public string Path { get; set; }
    public string FileDownloadName { get; set; }

    public override void ExecuteResult(ControllerContext context) {
        if (!String.IsNullOrEmpty(FileDownloadName)) {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + this.FileDownloadName);
            HttpContext.Current.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet; // "application/octet-stream";
        }

        //HttpContext.Current.Response.TransmitFile(this.Path);
        HttpContext.Current.Response.WriteFile(this.Path);
        //HttpContext.Current.Response.BinaryWrite(File.ReadAllBytes(this.Path));
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}