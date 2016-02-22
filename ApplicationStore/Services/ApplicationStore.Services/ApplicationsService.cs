using System.Linq;
using ApplicationStore.Data.Repositories;
using ApplicationStore.Models;

namespace ApplicationStore.Services
{
    public class ApplicationsService : IApplicationsService
    {
        IRepository<Application> applications;

        public ApplicationsService(IRepository<Application> applications)
        {
            this.applications = applications;
        }

        public Application Add(Application application)
        {
            this.applications.Add(application);
            this.applications.SaveChanges();
            return application;
        }

        public IQueryable<Application> GetAll()
        {
            return this.applications.All();
        }

        public IQueryable<Application> GetByCreator(string creatorName)
        {
            var result = this.applications.All().Where(x => x.Creator.UserName == creatorName);
            return result;
        }

        public IQueryable<Application> GetByCreatorId(string Id)
        {
            var result = this.applications.All().Where(x => x.CreatorId == Id);
            return result;
        }

        public Application GetById(string id)
        {
            return this.applications.GetById(id);
        }

        public void Remove(string id)
        {
            this.applications.Delete(id);
        }
    }
}
