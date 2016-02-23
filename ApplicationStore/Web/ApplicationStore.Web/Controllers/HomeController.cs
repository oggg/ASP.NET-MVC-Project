namespace ApplicationStore.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using ApplicationStore.Services;
    using ApplicationStore.Services.Contracts;
    using Areas.Developer.ViewModels;
    using Common;
    using Infrastructure.Mapping;
    using ViewModels.Applications;
    public class HomeController : BaseController
    {
        private readonly IApplicationsService applications;
        private readonly ICategoriesService categories;
        private readonly IUsersService users;
        private readonly IImageService images;

        public HomeController(IApplicationsService applications,
            ICategoriesService categories,
            IUsersService users,
            IImageService images)
        {
            this.applications = applications;
            this.categories = categories;
            this.users = users;
            this.images = images;
        }

        public ActionResult Index(int id = 1)
        {
            int page = id;

            var apps = this.applications.GetAll()
                .OrderBy(x => x.Id)
                //.Skip((page - 1) * WebConstants.AppsPerPage)
                //.Take(WebConstants.AppsPerPage)
                .To<ApplicationViewModel>()
                .ToList();

            var totalPages = (int)Math.Ceiling(apps.Count / (decimal)WebConstants.AppsPerPage);

            var devAppsModel = new DeveloperApplicationsViewModel
            {
                Applications = apps,
                CurrentPage = page,
                TotalPages = totalPages
            };
            return View(devAppsModel);
        }
    }
}