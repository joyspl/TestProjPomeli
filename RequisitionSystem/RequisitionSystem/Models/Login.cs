using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Login
{
    public long UserId { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public string FullName { get; set; }
    public int UserRole { get; set; }
    public long RefId { get; set; }
    public string MobileNo { get; set; }
    public string EmailId { get; set; }
    public int  Active { get; set; }
    public int Opmode { get; set; }
}
