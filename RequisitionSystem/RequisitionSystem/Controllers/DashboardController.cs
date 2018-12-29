using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequisitionSystem.Controllers
{
    [SessionAuthorize]
    public class DashboardController : Controller
    {
        
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            
            if(GlobalSettings.oUserMaster.UserRole==1) // circle user
            {
                if (GlobalSettings.oUserMaster.Active == 0)
                {
                    return RedirectToAction("Index", "CircleProfile", new { area = "" });
                }
                
            }
            else if (GlobalSettings.oUserMaster.UserRole == 2) // district user
            {
                if (GlobalSettings.oUserMaster.Active == 0)
                {
                    return RedirectToAction("Index", "DistrictProfile", new { area = "" });
                }

            }
            else if (GlobalSettings.oUserMaster.UserRole == 3 || GlobalSettings.oUserMaster.UserRole == 4)// other user
            {
                if (GlobalSettings.oUserMaster.Active == 0)
                {
                    return RedirectToAction("Index", "UserProfile", new { area = "" });
                }

            }

            
                return View();
           
            
        }

    }
}
