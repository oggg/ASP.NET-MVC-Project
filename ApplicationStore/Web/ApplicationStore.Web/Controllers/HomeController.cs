using System.Web.Mvc;
using ApplicationStore.Common;

namespace ApplicationStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole(DbConstants.AdminRole))
            {
                // TODO: redirect to appropriate routing
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}