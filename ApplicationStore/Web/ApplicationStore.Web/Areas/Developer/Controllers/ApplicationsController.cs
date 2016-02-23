namespace ApplicationStore.Web.Areas.Developer.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using ApplicationStore.Common;
    using Infrastructure.Mapping;
    using Services;
    using Services.Contracts;
    using ViewModels;
    using Web.Controllers;
    using Web.ViewModels.Applications;

    [Authorize(Roles = DbConstants.DeveloperRole)]
    public class ApplicationsController : BaseController
    {
        private readonly IApplicationsService applications;
        private readonly ICategoriesService categories;
        private readonly IUsersService users;
        private readonly IImageService images;

        public ApplicationsController(
            IApplicationsService applications,
            ICategoriesService categories,
            IUsersService users,
            IImageService images)
        {
            this.applications = applications;
            this.categories = categories;
            this.users = users;
            this.images = images;
        }

        [HttpGet]
        public ActionResult Uploaded(int id = 1)
        {
            int page = id;
            string devName = this.User.Identity.Name;
            int allApps = this.applications.GetByCreator(devName).Count();
            var totalPages = (int)Math.Ceiling(allApps / (decimal)WebConstants.AppsPerPage);

            var cat = this.categories.GetAll().To<CategoryViewModel>().ToList();

            var apps = this.applications.GetAll()
                .Where(x => x.Creator.UserName == devName)
                .OrderBy(x => x.Id)
                .Skip((page - 1) * WebConstants.AppsPerPage)
                .Take(WebConstants.AppsPerPage)
                .To<ApplicationViewModel>()
                .ToList();

            // TODO: check this.Cache service work

            //var categories =
            //    this.Cache.Get(
            //        "categories",
            //        () => this.categories.GetAll(),
            //        5 * 60);

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