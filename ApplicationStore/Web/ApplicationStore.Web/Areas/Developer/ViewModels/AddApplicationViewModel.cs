﻿namespace ApplicationStore.Web.Areas.Developer.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ApplicationStore.Models;
    using ApplicationStore.Web.Infrastructure.Mapping;
    using Web.ViewModels.Applications;

    public class AddApplicationViewModel : ApplicationBaseModel, IMapFrom<Application>, IHaveCustomMappings
    {
        //[Required]
        //[StringLength(50)]
        //[UIHint("SingleLineText")]
        //public string Name { get; set; }

        //[Required]
        //[StringLength(100)]
        //[UIHint("MultiLineText")]
        //public string Description { get; set; }

        //public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } // was selectedlistitem before

        public IEnumerable<SelectListItem> GetCategories
        {
            get
            {
                return new SelectList(Categories, "Id", "Name");
            }
        }

        //[Required]
        //[Display(Name = "Application")]
        //public HttpPostedFileBase UploadedImage { get; set; }

        //[Required]
        //[Display(Name = "Image")]
        //public HttpPostedFileBase UploadedApplication { get; set; }

        //public void CreateMappings(IMapperConfiguration configuration)
        //{
        //    configuration.CreateMap<AddApplicationViewModel, Application>()
        //        .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.UploadedImage))
        //        .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.UploadedApplication.FileName));
        //}
    }
}