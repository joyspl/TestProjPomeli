﻿using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequisitionSystem.Controllers
{
    public class CircleProfileController : Controller
    {
        //
        // GET: /CircleProfile/

        public ActionResult Index()
        {
            var objCircleProfile = DBOperations<CircleProfile>.GetSpecific(new CircleProfile() { Opmode = 0, CircleMasterId=GlobalSettings.oUserMaster.RefId }, Constant.usp_CircleProfile);

            return View(objCircleProfile);
        }

        [HttpPost]

        public ActionResult UpdateProfile(CircleProfile jsonData)
        {
            int result = default(int);
            string msg = string.Empty;
            try
            {
                jsonData.Opmode = 1;
               
                result = DBOperations<CircleProfile>.DMLOperation(jsonData, Constant.usp_CircleProfile);
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
