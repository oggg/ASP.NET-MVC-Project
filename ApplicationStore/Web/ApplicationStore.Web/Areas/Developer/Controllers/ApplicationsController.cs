namespace ApplicationStore.Web.Areas.Developer.Controllers
{
    using System;
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

        [HttpGet]
        public ActionResult AddApplication()
        {
            var cat = this.categories.GetAll().To<CategoryViewModel>().ToList();
            var addApplicationViewModel = new AddApplicationViewModel
            {
                Categories = cat
            };
            var imageModel = new AppImageModel();
            var addViewModel = new AddViewModel
            {
                Application = addApplicationViewModel,
                Image = imageModel
            };

            //TODO: choose wiser method for passing addViewModel
            this.HttpContext.Cache["addViewModel"] = addViewModel;

            return View(addViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddApplication(AddViewModel app)
        {
            //TODO: ADD CATEGORIES TO APP, BEFORE RETURNING IT TO A VIEW!!! THROWS ARGUMENT NULL EXCEPTION FOR THE COLLECTION OF CATEGORIES 
            if (app != null && ModelState.IsValid)
            {
                var newApplication = this.Mapper.Map<Application>(app.Application);

                //check if image file exists
                if (app.Image.UploadedImage != null)
                {
                    if (!FileSystemChecker.DirectoryExists(WebConstants.ImageFolder))
                    {
                        Directory.CreateDirectory(Server.MapPath(WebConstants.ImageFolder));
                    }

                    if (!FileSystemChecker.FileExists(WebConstants.ImageFolder, app.Image.UploadedImage.FileName))
                    {
                        app.Image.UploadedImage.SaveAs(Server.MapPath(string.Format("{0}/{1}", WebConstants.ImageFolder, app.Image.UploadedImage.FileName)));
                        var image = new AppImage
                        {
                            Name = app.Image.UploadedImage.FileName,
                            Path = string.Format("{0}/{1}", WebConstants.ImageFolder, app.Image.UploadedImage.FileName)
                        };

                        this.images.Add(image);
                        var dbImage = this.images.GetByName(image.Name);
                        if (dbImage.Path.IndexOf("\\") > -1)
                        {
                            dbImage.Path.Replace("\\", "/");
                        }
                        // newApplication.AppImageId = dbImage.Id; just changed and uncomment newApplication.Image = image;
                        newApplication.Image = dbImage;
                    }
                    else
                    {
                        var storedImage = this.images.GetByName(app.Image.UploadedImage.FileName);
                        // newApplication.AppImageId = storedImage.Id; just commented and added line below
                        newApplication.Image = storedImage;
                    }
                }
                else
                {
                    // return view in case of missing application image file
                    return View(this.HttpContext.Cache["addViewModel"]);
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
                        app.Application.UploadedApplication.SaveAs(Server.MapPath(string.Format("{0}/{1}", WebConstants.ApplicationFolder, app.Application.UploadedApplication.FileName)));
                        newApplication.Path = string.Format("{0}/{1}", WebConstants.ApplicationFolder, app.Application.UploadedApplication.FileName);
                    }
                    else
                    {
                        newApplication.Path = string.Format("{0}/{1}", WebConstants.ApplicationFolder, app.Application.UploadedApplication.FileName);
                    }

                    newApplication.CreatorId = User.Identity.GetUserId();
                    newApplication.CaterogyId = app.Application.CategoryId;
                    var savedApp = this.applications.Add(newApplication);
                    var curentDeveloper = this.users.GetById(newApplication.CreatorId);
                    curentDeveloper.Applications.Add(savedApp);
                    // return RedirectToAction("Index", "Home", new { area = "" });
                    return RedirectToAction("Uploaded");
                }
                else
                {
                    // return view in case of missing application file
                    return View(this.HttpContext.Cache["addViewModel"]);
                }
            }

            // return View(app);
            return View(this.HttpContext.Cache["addViewModel"]);
        }
    }
}