namespace ApplicationStore.Web.Areas.Developer.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using ApplicationStore.Models;
    using AutoMapper;

    using Infrastructure.Mapping;

    public class ApplicationBaseModel : IMapFrom<Application>, IHaveCustomMappings
    {
        [Required]
        [StringLength(50)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        [Display(Name = "Category")]
        [UIHint("DropDownList")]
        public int CategoryId { get; set; }

        //[Required]
        //public HttpPostedFileBase UploadedImage { get; set; }

        [Required]
        public HttpPostedFileBase UploadedApplication { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<AddApplicationViewModel, Application>()
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.UploadedApplication.FileName));
        }
    }
}
