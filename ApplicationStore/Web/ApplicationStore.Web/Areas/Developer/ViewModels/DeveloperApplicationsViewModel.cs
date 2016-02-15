namespace ApplicationStore.Web.Areas.Developer.ViewModels
{
    using System.Collections.Generic;
    using ApplicationStore.Web.ViewModels.Home;

    public class DeveloperApplicationsViewModel
    {
        public IList<ApplicationViewModel> Applications { get; set; }

        public IList<CategoryViewModel> Categories { get; set; }
    }
}
