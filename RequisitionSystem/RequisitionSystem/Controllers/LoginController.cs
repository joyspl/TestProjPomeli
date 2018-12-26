using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace MvcApplication1.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Validate(string username, string password)
        {
            try
            {
                Login obj = new Login();
                obj.Opmode = 1;
                obj.UserName = username;
                obj.UserPassword = password;
                var objLogin = DBOperations<Login>.GetSpecific(obj, Constant.usp_Login);

                if (objLogin != null && objLogin.UserPassword == password)
                {

                    GlobalSettings.oUserMaster = objLogin;

                }
                else
                {
                    throw new Exception("Invalid login credential!");
                }

                return Json(new { Success = 1, Message = "Login Successfull", Role = GlobalSettings.oUserMaster.UserRole }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [AllowAnonymous]

        public ActionResult Logout()
        {
            GlobalSettings.oUserMaster = (Login)null;
            this.Session.Clear();
            this.Session.Abandon();
            this.Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }

    }
}
