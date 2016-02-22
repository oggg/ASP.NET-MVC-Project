namespace ApplicationStore.Web.ViewModels.Applications
{
    using ApplicationStore.Models;
    using ApplicationStore.Web.Infrastructure.Mapping;
    using AutoMapper;
    public class ApplicationViewModel : IMapFrom<Application>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Creator { get; set; }

        public string AppImage { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Application, ApplicationViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.AppImage, opt => opt.MapFrom(src => src.Image.Path));
        }
    }
}
