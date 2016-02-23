namespace ApplicationStore.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using ApplicationStore.Services;
    using ApplicationStore.Services.Contracts;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using ViewModels.Applications;

    public class UserApplicationsController : Controller
    {
        private readonly IApplicationsService applications;
        private readonly IUsersService users;

        public UserApplicationsController(IApplicationsService applications, IUsersService users)
        {
            this.applications = applications;
            this.users = users;
        }

        public ActionResult Index()
        {
            var userId = this.User.Identity.GetUserId();
            var currentUser = this.users.GetById(userId);

            var apps = currentUser.Applications.AsQueryable().To<ApplicationViewModel>().ToList();

            return View(apps);
        }
    }
}