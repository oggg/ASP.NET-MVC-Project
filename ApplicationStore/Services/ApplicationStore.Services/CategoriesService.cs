using System.Linq;
using ApplicationStore.Data.Repositories;
using ApplicationStore.Models;
using ApplicationStore.Services.Contracts;

namespace ApplicationStore.Services
{
    public class CategoriesService : ICategoriesService
    {
        private IRepository<Category> categories;

        public CategoriesService(IRepository<Category> categories)
        {
            this.categories = categories;
        }

        public Category Add(Category category)
        {
            this.categories.Add(category);
            this.categories.SaveChanges();
            return category;
        }

        public IQueryable<Category> GetAll()
        {
            return this.categories.All();
        }

        public Category GetById(int id)
        {
            return this.categories.GetById(id);
        }

        public void Remove(int id)
        {
            this.categories.Delete(id);
        }
    }
}
