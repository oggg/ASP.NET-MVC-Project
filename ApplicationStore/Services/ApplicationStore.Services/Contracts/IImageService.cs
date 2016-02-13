using System.Linq;
using ApplicationStore.Models;

namespace ApplicationStore.Services.Contracts
{
    public interface IImageService
    {
        AppImage Add(AppImage image);

        IQueryable<AppImage> GetAll();

        AppImage GetById(int id);

        AppImage GetByName(string name);

        void Remove(int id);
    }
}
