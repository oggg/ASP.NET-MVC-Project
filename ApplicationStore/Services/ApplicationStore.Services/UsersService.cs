using System;
using System.Linq;
using ApplicationStore.Data.Repositories;
using ApplicationStore.Models;
using ApplicationStore.Services.Contracts;

namespace ApplicationStore.Services
{
    public class UsersService : IUsersService
    {
        private IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public User Add(User user)
        {
            this.users.Add(user);
            this.users.SaveChanges();
            return user;
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public User GetById(string id)
        {
            return this.users.GetById(id);
        }

        public User GetByName(string name)
        {
            return this.users.All().Where(u => u.UserName == name).FirstOrDefault();
        }

        public IQueryable<Application> GetUserApplications(string userName)
        {
            var user = this.users.All().Where(u => u.UserName == userName).FirstOrDefault();
            var userId = user.Id;

            var apps = user.Applications.AsQueryable();

            return apps;
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
