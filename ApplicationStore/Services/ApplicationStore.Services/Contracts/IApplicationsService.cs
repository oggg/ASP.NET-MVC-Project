using System.Linq;
using ApplicationStore.Models;

namespace ApplicationStore.Services
{
    public interface IApplicationsService
    {
        Application Add(Application application);

        IQueryable<Application> GetAll();

        IQueryable<Application> GetByCreator(string creatorName);

        IQueryable<Application> GetByCreatorId(string Id);

        Application GetById(string id);

        // void UpdateById(int id, Application application);

        void Remove(string id);
    }
}