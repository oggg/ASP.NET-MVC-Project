using System;
using ApplicationStore.Models;
using ApplicationStore.Web.Infrastructure.Mapping;
using AutoMapper;

namespace ApplicationStore.Web.ViewModels.Home
{
    public class RateViewModel : IMapFrom<Rating>, IHaveCustomMappings
    {
        public int Value { get; set; }

        public Guid Application { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Rating, RateViewModel>()
                .ForMember(dest => dest.Application, opt => opt.MapFrom(src => src.ApplicationId));
        }
    }
}
