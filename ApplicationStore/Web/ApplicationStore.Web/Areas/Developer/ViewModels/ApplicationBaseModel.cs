namespace ApplicationStore.Web.Areas.Developer.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using ApplicationStore.Models;
    using AutoMapper;

    public class ApplicationBaseModel
    {
        [Required]
        [StringLength(50)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Application")]
        public HttpPostedFileBase UploadedImage { get; set; }

        [Required]
        [Display(Name = "Image")]
        public HttpPostedFileBase UploadedApplication { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<AddApplicationViewModel, Application>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.UploadedImage))
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.UploadedApplication.FileName));
        }
    }
}
