using System.Web.Mvc;

namespace ApplicationStore.Web.Controllers
{
    public class ApplicationController : BaseController
    {
        // GET: Application
        public ActionResult Index()
        {
            return View();
        }
    }
}