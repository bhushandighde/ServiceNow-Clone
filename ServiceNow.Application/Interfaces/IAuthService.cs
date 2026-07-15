using Microsoft.AspNetCore.Mvc;
using ServiceNow.ServiceNow.Application.DTOs;

namespace ServiceNow.ServiceNow.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(string email, string password);
        public Task<CreateUserResponse> CreateUserAsync(string username, string password, string email);
    }
}
