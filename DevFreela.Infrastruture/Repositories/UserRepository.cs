﻿using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastruture.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastruture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(User user)
        {
            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
        }
    }
}
