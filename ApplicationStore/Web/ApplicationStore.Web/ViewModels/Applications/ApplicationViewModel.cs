namespace ApplicationStore.Web.ViewModels.Applications
{
    using ApplicationStore.Models;
    using ApplicationStore.Web.Infrastructure.Mapping;
    using AutoMapper;
    public class ApplicationViewModel : IMapFrom<Application>, IHaveCustomMappings
    {
        //public Guid Id { get; set; }
        public string Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Creator { get; set; }

        public string AppImage { get; set; } // was int before pointing to Image id

        //public int? Rating { get; set; }

        //public int? Rated { get; set; }


        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Application, ApplicationViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.UserName))
                .ForMember(dest => dest.AppImage, opt => opt.MapFrom(src => src.Image.Path));
            //.ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Ratings == null ? 0 : src.Ratings.Sum(r => r.Value)))// .Sum(r => r.Value)))
            //.ForMember(dest => dest.Rated, opt => opt.MapFrom(src => src.Ratings.Count == 0 ? 1 : src.Ratings.Count));
        }
    }
}
