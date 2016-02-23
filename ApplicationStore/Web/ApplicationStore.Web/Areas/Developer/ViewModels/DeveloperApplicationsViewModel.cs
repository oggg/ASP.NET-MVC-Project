namespace ApplicationStore.Web.Areas.Developer.ViewModels
{
    using System.Collections.Generic;
    using ApplicationStore.Web.ViewModels.Applications;

    public class DeveloperApplicationsViewModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<ApplicationViewModel> Applications { get; set; }

    }
}
