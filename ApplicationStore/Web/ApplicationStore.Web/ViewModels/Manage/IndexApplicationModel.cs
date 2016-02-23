namespace ApplicationStore.Web.ViewModels.Manage
{
    using System.Collections.Generic;
    using ApplicationStore.Web.ViewModels.Applications;

    public class IndexApplicationModel
    {
        public IndexViewModel IndexModel { get; set; }

        public ICollection<ApplicationViewModel> Application { get; set; }
    }
}
