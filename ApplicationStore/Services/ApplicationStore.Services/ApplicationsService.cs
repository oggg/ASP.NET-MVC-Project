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

        public Application GetById(string id)
        {
            return this.applications.GetById(id);
        }

        public void Remove(string id)
        {
            this.applications.Delete(id);
        }

        //public void UpdateById(string id, Application updatedApp)
        //{
        //    var appToUpdate = this.applications.GetById(id);
        //    appToUpdate.CaterogyId = updatedApp.CaterogyId;
        //    appToUpdate.Description = updatedApp.Description;
        //    appToUpdate.
        //}
    }
}
