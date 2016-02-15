namespace ApplicationStore.Web.Areas.Developer.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
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

        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        [UIHint("DropDownList")]
        public IEnumerable<Category> Categories { get; set; } // was selectedlistitem before

        [Display(Name = "Application")]
        public HttpPostedFileBase UploadedImage { get; set; }

        [Display(Name = "Image")]
        public HttpPostedFileBase UploadedApplication { get; set; }
    }
}
