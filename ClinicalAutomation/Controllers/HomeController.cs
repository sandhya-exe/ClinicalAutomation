using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicalAutomation.Controllers
{
    public class HomeController : Controller
    {
        // GET: Homepg
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult About()
        //{
        //    return View();
        //}
        public ActionResult Features()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Support()
        {
            return View();
        }
    }
}