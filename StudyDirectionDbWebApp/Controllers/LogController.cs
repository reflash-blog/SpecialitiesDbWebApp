using System.Net;
using System.Web.Mvc;

namespace StudyDirectionDbWebApp.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Index(int? page, int? id)
        {
            if (page == null)
                page = 1;

            ViewBag.Page = page;

            if (id == null)
            {
                return View(page);
            }

            return Redirect("#"+id);
        }
    }
}