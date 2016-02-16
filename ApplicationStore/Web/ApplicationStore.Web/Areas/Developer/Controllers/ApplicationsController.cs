namespace ApplicationStore.Web.Areas.Developer.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using ApplicationStore.Common;
    using Infrastructure.Helpers;
    using Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
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
            var imageModel = new AppImageModel();
            var addViewModel = new AddViewModel
            {
                Application = addApplicationViewModel,
                Image = imageModel
            };
            // addApplicationViewModel.Categories = new SelectList(categories, "Id", "Name");
            return View(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddApplication(AddViewModel app) // AddApplicationViewModel app
        {
            //TODO: ADD CATEGORIES TO APP, BEFORE RETURNING IT TO A VIEW!!! THROWS ARGUMENT NULL EXCEPTION FOR THE COLLECTION OF CATEGORIES 
            if (app != null && ModelState.IsValid)
            {
                var newApplication = this.Mapper.Map<Application>(app.Application);
                newApplication.CreatorId = User.Identity.GetUserId();
                newApplication.CaterogyId = app.Application.CategoryId;

                //check if image file exists
                if (app.Image.UploadedImage != null)
                {
                    if (!FileSystemChecker.DirectoryExists(WebConstants.ImageFolder))
                    {
                        Directory.CreateDirectory(Server.MapPath(WebConstants.ImageFolder));
                    }

                    if (!FileSystemChecker.FileExists(WebConstants.ImageFolder, app.Image.UploadedImage.FileName))
                    {
                        app.Image.UploadedImage.SaveAs(Server.MapPath(Path.Combine(WebConstants.ImageFolder, app.Image.UploadedImage.FileName)));
                        var image = new AppImage
                        {
                            Name = app.Image.UploadedImage.FileName,
                            Path = Path.Combine(WebConstants.ImageFolder, app.Image.UploadedImage.FileName)
                        };

                        // newApplication.Image = image;

                        AppImage dbImage = this.images.Add(image);
                        newApplication.AppImageId = dbImage.Id;
                    }
                    else
                    {
                        var storedImage = this.images.GetByName(app.Image.UploadedImage.FileName);
                        newApplication.AppImageId = storedImage.Id;
                        //TODO: maybe add other image properties
                    }
                }
                else
                {
                    // return view in case of missing application image file
                    return View(app);
                }

                //check if application file exists
                if (app.Application.UploadedApplication != null)
                {
                    if (!FileSystemChecker.DirectoryExists(WebConstants.ApplicationFolder))
                    {
                        Directory.CreateDirectory(Server.MapPath(WebConstants.ApplicationFolder));
                    }

                    if (!FileSystemChecker.FileExists(WebConstants.ApplicationFolder, app.Application.UploadedApplication.FileName))
                    {
                        app.Application.UploadedApplication.SaveAs(Server.MapPath(Path.Combine(WebConstants.ApplicationFolder, app.Application.UploadedApplication.FileName)));
                        newApplication.Path = Path.Combine(WebConstants.ApplicationFolder, app.Application.UploadedApplication.FileName);
                    }
                    else
                    {
                        newApplication.Path = Path.Combine(WebConstants.ApplicationFolder, app.Application.UploadedApplication.FileName);
                    }

                    var savedApp = this.applications.Add(newApplication);
                    var curentDeveloper = this.users.GetById(newApplication.CreatorId);
                    curentDeveloper.Applications.Add(savedApp);
                    return RedirectToAction("Uploaded", "Applications");
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