using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudyDirectionDbWebApp.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Purpose()
        {
            return View();
        }

        // GET: Help
        public ActionResult RuntimeConditions()
        {
            return View();
        }
        // GET: Help
        public ActionResult Running()
        {
            return View();
        }
        // GET: Help
        public ActionResult ExecutionOrder()
        {
            return View();
        }
        // GET: Help
        public ActionResult FunctionsDescription()
        {
            return View();
        }
        // GET: Help
        public ActionResult OperatorMessages()
        {
            return View();
        }
    }
}