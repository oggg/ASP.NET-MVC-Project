namespace ApplicationStore.Web.ViewModels.Applications
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;
    using ApplicationStore.Models;
    using ApplicationStore.Web.Infrastructure.Mapping;

    public class AddApplicationViewModel : IMapFrom<Application>
    {
        [Required]
        [StringLength(50)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [StringLength(100)]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        [Display(Name = "Category")]
        [UIHint("DropDownList")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public HttpPostedFileBase UploadedImage { get; set; }

        public HttpPostedFileBase UploadedApplication { get; set; }
    }
}
