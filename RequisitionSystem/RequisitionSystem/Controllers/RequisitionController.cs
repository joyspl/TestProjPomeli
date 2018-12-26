using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequisitionSystem.Controllers
{
    public class RequisitionController : Controller
    {
        //
        // GET: /Requisition/
        [SessionAuthorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
