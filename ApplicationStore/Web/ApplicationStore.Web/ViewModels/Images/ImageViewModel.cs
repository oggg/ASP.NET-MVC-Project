using ApplicationStore.Models;
using ApplicationStore.Web.Infrastructure.Mapping;

namespace ApplicationStore.Web.ViewModels.Images
{
    public class ImageViewModel : IMapFrom<AppImage>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
