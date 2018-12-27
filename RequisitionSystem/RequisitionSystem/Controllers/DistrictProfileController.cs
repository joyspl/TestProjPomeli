using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequisitionSystem.Controllers
{
    public class DistrictProfileController : Controller
    {
        //
        // GET: /DistrictProfile/

        public ActionResult Index()
        {
            var objDistrictProfile = DBOperations<DistrictProfile>.GetSpecific(new DistrictProfile() { Opmode = 0, DistrictId = GlobalSettings.oUserMaster.RefId }, Constant.usp_DistrictProfile);
            return View(objDistrictProfile);
        }

    }
}
