using System.Linq;
using ApplicationStore.Models;

namespace ApplicationStore.Services.Contracts
{
    public interface IUsersService
    {
        IQueryable<User> GetAll();

        User GetById(string id);

        User GetByName(string name);

        void Update(User user);

        IQueryable<Application> GetUserApplications(string userName);

        void Remove(string id);
    }
}
