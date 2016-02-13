using System.Linq;
using ApplicationStore.Models;

namespace ApplicationStore.Services
{
    public interface IApplicationsService
    {
        IQueryable<Application> GetAll();

        IQueryable<Application> GetByCreator(string creatorName);

        Application GetById(string id);

        void Update(Application application);

        void Remove(string id);
    }
}