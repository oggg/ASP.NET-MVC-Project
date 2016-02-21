namespace ApplicationStore.Web.Areas.Developer.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.ViewModels.Applications;

    public class AddApplicationViewModel : ApplicationBaseModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; } // was selectedlistitem before

        public IEnumerable<SelectListItem> GetCategories
        {
            get
            {
                return new SelectList(Categories, "Id", "Name");
            }
        }
    }
}