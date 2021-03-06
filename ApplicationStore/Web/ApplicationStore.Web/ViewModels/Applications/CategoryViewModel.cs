﻿namespace ApplicationStore.Web.ViewModels.Applications
{
    using ApplicationStore.Models;
    using ApplicationStore.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
