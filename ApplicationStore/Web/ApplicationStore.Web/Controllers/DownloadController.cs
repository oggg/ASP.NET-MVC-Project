using System.Net.Mime;
using System.Web.Mvc;
using ApplicationStore.Services;
using ApplicationStore.Services.Contracts;
using Microsoft.AspNet.Identity;

namespace ApplicationStore.Web.Controllers
{
    public class DownloadController : BaseController
    {
        private readonly IApplicationsService applications;
        private readonly IUsersService users;

        public DownloadController(IApplicationsService applications, IUsersService users)
        {
            this.applications = applications;
            this.users = users;
        }

        public FileResult GetApplication(string id)
        {
            var application = this.applications.GetById(id);

            var currentUser = this.users.GetById(User.Identity.GetUserId());

            currentUser.Applications.Add(application);


            return File(application.Path, MediaTypeNames.Application.Octet, string.Format("{0}.exe", application.Name));
        }
    }
}