using System.Linq;
using ApplicationStore.Models;

namespace ApplicationStore.Services.Contracts
{
    public interface ICategoriesService
    {
        IQueryable<Category> GetAll();

        Category GetById(int id);

        void Remove(int id);
    }
}
