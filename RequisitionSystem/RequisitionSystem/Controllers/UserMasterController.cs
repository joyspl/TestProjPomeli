using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequisitionSystem.Controllers
{
    [SessionAuthorize]
    public class UserMasterController : Controller
    {
        //
        // GET: /UserMaster/

        public ActionResult Index()
        {
            List<Login> lst;
            try
            {
                lst = DBOperations<Login>.GetAllOrByRange(new Login() { Opmode = 0 }, Constant.usp_Login);
            }
            catch (Exception ex)
            {
                lst = new List<Login>();

            }
            
            return View(lst);
        }

        [HttpGet]
        public ActionResult AddEdit(long id)
        {
            Login obj;
            try
            {
                if (id > default(long))
                {
                    obj = DBOperations<Login>.GetSpecific(new Login() { UserId = id, Opmode = 1 }, Constant.usp_Page);
                }
                else
                {
                    obj = new Login();
                }
            }
            catch (Exception)
            {
                obj = new Login();
            }
            return View("~/Views/UserMaster/AddEdit.cshtml", obj);
        }
    }
}
