using Microsoft.AspNetCore.Mvc;
using ServiceNow.ServiceNow.Domain.Entities;
namespace ServiceNow.ServiceNow.Application.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> getEmailbyAsync(string email);
        public Task<bool> createUserAsyncDb(string username, string password, string email);
    }
}
