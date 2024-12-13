using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicalAutomation.Controllers
{
    public class PhysicianController : Controller
    {
        // GET: Physician
        public ActionResult Index()
        {
            
            return View();
            
        }
       public ActionResult currentUsr()
       {
            string currentUserName = User.Identity.IsAuthenticated ? User.Identity.Name : "Guest";

            // Optionally, pass it to the ViewBag
            ViewBag.CurrentUser = currentUserName;
            return View();
        }
    }
}