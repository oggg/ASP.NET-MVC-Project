namespace ApplicationStore.Services
{
    using System.Linq;
    using ApplicationStore.Services.Contracts;
    using Data.Repositories;
    using Models;

    public class ImageService : IImageService
    {
        private IRepository<AppImage> images;

        public ImageService(IRepository<AppImage> images)
        {
            this.images = images;
        }

        public IQueryable<AppImage> GetAll()
        {
            return this.images.All();
        }

        public AppImage GetById(int id)
        {
            return this.images.GetById(id);
        }

        public AppImage GetByName(string name)
        {
            return this.images.All().Where(img => img.Name == name).FirstOrDefault();
        }

        public void Remove(int id)
        {
            var image = this.images.GetById(id);
            this.images.Delete(image);
            this.images.SaveChanges();
        }

        public void Update(AppImage image)
        {

        }
    }
}
