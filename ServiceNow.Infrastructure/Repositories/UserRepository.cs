using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceNow.Infrastructure.Persistence;
using ServiceNow.ServiceNow.Application.Interfaces;
using ServiceNow.ServiceNow.Domain.Entities;
using System.Security.Claims;

namespace ServiceNow.ServiceNow.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserRepository userRepository_;
        private readonly IConfiguration _configuration;

        public UserRepository(ApplicationDbContext db,IConfiguration config)
        {
            _db = db;
            _configuration = config;

        }
        public async Task<User?> getEmailbyAsync(string email)
        
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);


        }

        public async Task<bool> createUserAsyncDb(string username, string password, string email)
        {
            User newUser = new User();
            newUser.Name = username;
            newUser.Email = email;
            newUser.Password = password;

           await _db.Users.AddAsync(newUser);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
