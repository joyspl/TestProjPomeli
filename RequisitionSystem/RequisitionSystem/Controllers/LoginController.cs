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

                UserSec objsession = new UserSec();

                if (objLogin != null && objLogin.UserPassword == password)
                {

                    objsession.UserId = objLogin.UserId;
                    objsession.UserName = objLogin.UserName;
                    objsession.FullName = objLogin.FullName;
                    objsession.UserRole = objLogin.UserRole;
                    objsession.RefId = objLogin.RefId;
                    objsession.MobileNo = objLogin.MobileNo;
                    objsession.EmailId = objLogin.EmailId;
                    objsession.Active = objLogin.Active;

                    if (objLogin.UserRole==1) // circle user
                    {
                        var objCircleProfile = DBOperations<CircleProfile>.GetSpecific(new CircleProfile() { Opmode = 0, CircleMasterId = objLogin.RefId }, Constant.usp_CircleProfile);
                        objsession.CircleMasterId = objCircleProfile.CircleMasterId;
                        objsession.CircleName = objCircleProfile.CircleName;
                        objsession.DistrictId = objCircleProfile.DistrictId;

                        var objDistrictProfile = DBOperations<DistrictProfile>.GetSpecific(new DistrictProfile() { Opmode = 0, DistrictId = objCircleProfile.DistrictId }, Constant.usp_DistrictProfile);
                        objsession.DIstrictName = objDistrictProfile.DIstrictName;
                    }
                    if (objLogin.UserRole == 2) // district user
                    {
                        var objDistrictProfile = DBOperations<DistrictProfile>.GetSpecific(new DistrictProfile() { Opmode = 0, DistrictId = objLogin.RefId }, Constant.usp_DistrictProfile);
                        objsession.DistrictId = objDistrictProfile.DistrictId;
                        objsession.DIstrictName = objDistrictProfile.DIstrictName;
                        
                    }
                    

                    GlobalSettings.oUserMaster = objsession;

                   

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
            GlobalSettings.oUserMaster = (UserSec)null;
            this.Session.Clear();
            this.Session.Abandon();
            this.Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }

    }
}
