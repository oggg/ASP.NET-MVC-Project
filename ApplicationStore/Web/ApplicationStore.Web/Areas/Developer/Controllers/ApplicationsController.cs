namespace ApplicationStore.Web.Areas.Developer.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using ApplicationStore.Common;
    using Infrastructure.Helpers;
    using Infrastructure.Mapping;
    using Models;
    using Services;
    using Services.Contracts;
    using ViewModels.Applications;
    using ViewModels.Home;
    using Web.Controllers;

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
        public ActionResult Uploaded()
        {
            string devName = this.User.Identity.Name;
            var applications = this.applications.GetByCreator(devName).To<AddApplicationViewModel>().ToList();
            var categories = this.Cache.Get(
                "categories",
                () => this.categories.GetAll().To<CategoryViewModel>().ToList(),
                10 * 60);

            var uploadedModel = new { applicationsView = applications, categoriesView = categories };
            return View(uploadedModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var addApplicationViewModel = new AddApplicationViewModel();

            return View(addApplicationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddApplicationViewModel app)
        {
            if (app != null && ModelState.IsValid)
            {
                var newApplication = this.Mapper.Map<Application>(app);
                newApplication.Creator = this.users.GetByName(this.User.Identity.Name);
                newApplication.CaterogyId = app.CategoryId;

                //check if image file exists
                if (app.UploadedImage != null)
                {
                    if (!FileSystemChecker.DirectoryExists(WebConstants.ImageFolder))
                    {
                        Directory.CreateDirectory(Server.MapPath(WebConstants.ImageFolder));
                    }

                    if (!FileSystemChecker.FileExists(WebConstants.ImageFolder, app.UploadedImage.FileName))
                    {
                        app.UploadedImage.SaveAs(Path.Combine(WebConstants.ImageFolder, app.UploadedImage.FileName));
                        newApplication.Image = new AppImage
                        {
                            Name = app.UploadedImage.FileName,
                            Path = Path.Combine(WebConstants.ImageFolder, app.UploadedImage.FileName)
                        };

                        this.images.Add(newApplication.Image);
                    }
                }
                else
                {
                    // return view in case of missing application image file
                    return View(app);
                }

                //check if application file exists
                if (app.UploadedApplication != null)
                {
                    if (!FileSystemChecker.DirectoryExists(WebConstants.ApplicationFolder))
                    {
                        Directory.CreateDirectory(Server.MapPath(WebConstants.ApplicationFolder));
                    }

                    if (!FileSystemChecker.FileExists(WebConstants.ApplicationFolder, app.UploadedApplication.FileName))
                    {
                        app.UploadedApplication.SaveAs(Path.Combine(WebConstants.ApplicationFolder, app.UploadedApplication.FileName));
                        newApplication.Path = Path.Combine(WebConstants.ApplicationFolder, app.UploadedApplication.FileName);
                    }

                    this.applications.Add(newApplication);
                    RedirectToAction("Uploaded", "Applications");
                }
                else
                {
                    // return view in case of missing application file
                    return View(app);
                }
            }

            return View(app);
        }
    }
}