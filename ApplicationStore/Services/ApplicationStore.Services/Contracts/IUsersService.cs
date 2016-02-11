﻿using System.Linq;
using ApplicationStore.Models;

namespace ApplicationStore.Services.Contracts
{
    public interface IUsersService
    {
        IQueryable<User> GetAll();

        User GetById(string id);

        User GetByUserName(string userName);

        void Update(User user);

        void Remove(string id);
    }
}