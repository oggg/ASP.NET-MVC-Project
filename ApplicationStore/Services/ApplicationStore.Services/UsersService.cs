﻿using System;
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

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public User GetById(string id)
        {
            return this.users.GetById(id);
        }

        public User GetByUserName(string userName)
        {
            return this.users.All().FirstOrDefault(u => u.UserName == userName);
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