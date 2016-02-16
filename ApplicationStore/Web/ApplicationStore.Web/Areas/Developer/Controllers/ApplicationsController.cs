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
        public ActionResult Uploaded()
        {
            string devName = this.User.Identity.Name;
            var apps = this.users.GetUserApplications(devName).To<ApplicationViewModel>();
            var applications = apps.ToList();

            // TODO: check this.Cache service work

            //var categories =
            //    this.Cache.Get(
            //        "categories",
            //        () => this.categories.GetAll(),
            //        5 * 60);
            var cat = this.categories.GetAll().To<CategoryViewModel>(); //.ToList();
            var categories = cat.ToList();
            var devAppsModel = new DeveloperApplicationsViewModel { Applications = applications, Categories = categories };
            return View(devAppsModel);
        }

        [HttpGet]
        public ActionResult AddApplication()
        {
            var cat = this.categories.GetAll(); //.To<CategoryViewModel>(); //.ToList();
            var categories = cat.To<CategoryViewModel>().ToList();
            var addApplicationViewModel = new AddApplicationViewModel
            {
                Categories = categories
            };
            // addApplicationViewModel.Categories = new SelectList(categories, "Id", "Name");
            return View(addApplicationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddApplication(AddApplicationViewModel app) // AddApplicationViewModel app
        {

            if (app != null && ModelState.IsValid)
            {
                AppImage dbImage;
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
                        app.UploadedImage.SaveAs(Server.MapPath(Path.Combine(WebConstants.ImageFolder, app.UploadedImage.FileName)));
                        var image = new AppImage
                        {
                            Name = app.UploadedImage.FileName,
                            Path = Path.Combine(WebConstants.ImageFolder, app.UploadedImage.FileName)
                        };

                        newApplication.Image = image;

                        //dbImage = this.images.Add(image);
                        //newApplication.AppImageId = dbImage.Id;
                    }
                    else
                    {
                        var storedImage = this.images.GetByName(app.UploadedImage.FileName);
                        newApplication.Image = storedImage;
                        //TODO: maybe add other image properties
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
                        app.UploadedApplication.SaveAs(Server.MapPath(Path.Combine(WebConstants.ApplicationFolder, app.UploadedApplication.FileName)));
                        newApplication.Path = Path.Combine(WebConstants.ApplicationFolder, app.UploadedApplication.FileName);
                    }
                    else
                    {
                        newApplication.Path = Path.Combine(WebConstants.ApplicationFolder, app.UploadedApplication.FileName);
                    }

                    var savedApp = this.applications.Add(newApplication);
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