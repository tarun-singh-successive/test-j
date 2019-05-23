using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.Helpers;
using static TestApplication.Helpers.Enumerations;

namespace TestApplication.Controllers
{
    public class HomeController : BaseController
    {
        [LoggedOrAuthorizedAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = UserRoles.Admin)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [LoggedOrAuthorizedAttribute]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [LoggedOrAuthorizedAttribute]
        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your dashboard page.";

            return View();
        }
    }
}