using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequisitionSystem.Controllers
{
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult UpdateProfile(Login jsonData)
        {
            int result = default(int);
            string msg = string.Empty;
            try
            {
                jsonData.Opmode = 3;

                result = DBOperations<Login>.DMLOperation(jsonData, Constant.usp_Login);
                var objLogin = DBOperations<Login>.GetSpecific(new Login() { Opmode = 2, UserId = GlobalSettings.oUserMaster.UserId }, Constant.usp_Login);
                GlobalSettings.oUserMaster = objLogin;

                if (result > 0)
                {
                    msg = "Profile updated successfully";
                }
                else
                {
                    msg = "Data saving failed";
                }

                return Json(new { Success = result, Message = msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}
