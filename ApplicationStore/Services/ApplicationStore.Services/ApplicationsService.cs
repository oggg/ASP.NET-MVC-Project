using System;
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

        public IQueryable<Application> GetAll()
        {
            return this.applications.All();
        }

        public Application GetById(string id)
        {
            return this.applications.GetById(id);
        }

        public void Remove(string id)
        {
            this.applications.Delete(id);
        }

        public void Update(Application application)
        {
            throw new NotImplementedException();
        }
    }
}
