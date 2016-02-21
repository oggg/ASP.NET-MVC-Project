using System.Web.Mvc;

namespace ApplicationStore.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}