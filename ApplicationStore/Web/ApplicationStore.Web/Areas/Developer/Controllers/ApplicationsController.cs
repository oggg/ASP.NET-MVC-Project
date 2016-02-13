using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApplicationStore.Web.Areas.Developer.Controllers
{
    public class ApplicationsController : Controller
    {
        // GET: Developer/Applications
        public ActionResult Index()
        {
            return View();
        }
    }
}