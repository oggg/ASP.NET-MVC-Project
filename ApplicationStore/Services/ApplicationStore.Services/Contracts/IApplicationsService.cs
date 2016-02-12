using System.Linq;
using ApplicationStore.Models;

namespace ApplicationStore.Services
{
    public interface IApplicationsService
    {
        IQueryable<Application> GetAll();

        Application GetById(string id);

        void Update(Application application);

        void Remove(string id);
    }
}