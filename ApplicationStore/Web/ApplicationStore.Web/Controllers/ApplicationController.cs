namespace ApplicationStore.Web.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using ApplicationStore.Services;

    public class ApplicationController : BaseController
    {
        private readonly IApplicationsService applications;

        public ApplicationController(IApplicationsService applications)
        {
            this.applications = applications;
        }

        public ActionResult Details(string id)
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return this.Content("This action can be invoke only by AJAX call");
            }

            var app = this.applications.GetById(id);

            if (app == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return this.Content("Application not found");
            }

            return this.Content(app.Description);
        }
    }
}