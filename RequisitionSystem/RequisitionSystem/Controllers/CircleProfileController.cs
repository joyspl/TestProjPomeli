using DataLayer;
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

    }
}
